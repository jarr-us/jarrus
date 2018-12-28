using Jarrus.Models;
using Kanan;
using System;
using System.Collections.Generic;
using System.Deployment.Application;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace Jarrus
{
    public partial class MainForm : Form
    {
        private MLBCircleIteration _mlbIteration;
        public static int RunNumber;
        private static Stopwatch _sw = new Stopwatch();
        private static int _lastGenSeen;

        public MainForm()
        {
            UpdateChecker.Check();

            InitializeComponent();
            UpdateVersionLabel();
            StartProcessing();
        }

        private void StartProcessing()
        {
            var procThread = new Thread(ProcessLoop);
            procThread.Start();

            var kananThread = new Thread(KananLoop);
            kananThread.Start();
        }

        private void KananLoop()
        {
            while (true)
            {
                RunNumber++;
                RunKanan();
                UpdateChecker.Check();
            }
        }

        private void RunKanan()
        {
            _mlbIteration = new MLBCircleIteration();
            _mlbIteration.Run();
        }

        private void ProcessLoop()
        {
            while (true)
            {
                Process();
                Thread.Sleep(50);
            }
        }

        private void Process()
        {
            if (_mlbIteration == null || _mlbIteration.GARun == null) { return; }

            DrawIterationDetails();
            DrawFamilyDetails();
        }

        private void DrawIterationDetails()
        {
            _sw.Stop();
            var elapsedMs = _sw.ElapsedMilliseconds;
            var msPerGeneration = elapsedMs / (1.0 * (_mlbIteration.GARun.CurrentGeneration - _lastGenSeen));

            TextUpdater.SetText(this, generationLbl, _mlbIteration.GARun.CurrentGeneration + "");
            TextUpdater.SetText(this, firstNameLbl, _mlbIteration.GARun.LowestChromosome.FirstName + "");
            TextUpdater.SetText(this, lastNameLbl, _mlbIteration.GARun.LowestChromosome.LastName + "");
            TextUpdater.SetText(this, lowestScoreLbl, _mlbIteration.GARun.LowestChromosome.FitnessScore + "");
            TextUpdater.SetText(this, runsCompletedLbl, RunNumber + "");
            TextUpdater.SetText(this, msPerGenLbl, msPerGeneration.ToString("#,##0.00"));

            _sw.Reset();
            _sw.Start();
            _lastGenSeen = _mlbIteration.GARun.CurrentGeneration;
        }

        private void DrawFamilyDetails()
        {

            var dict = GetFamilyDetails();
            if (dict == null) { return; }

            var totalLineagesPossible = _mlbIteration.GARun.Population.Chromosomes.Length * 2;
            var ordered = dict.OrderByDescending(x => x.Value);
            var orderedTopTen = ordered.Take(10);

            var divisor = 0.01 * totalLineagesPossible;

            UpdateFamily(family1Lbl, lastName1ProgressBar, orderedTopTen, 0, divisor);
            UpdateFamily(family2Lbl, lastName2ProgressBar, orderedTopTen, 1, divisor);
            UpdateFamily(family3Lbl, lastName3ProgressBar, orderedTopTen, 2, divisor);
            UpdateFamily(family4Lbl, lastName4ProgressBar, orderedTopTen, 3, divisor);
            UpdateFamily(family5Lbl, lastName5ProgressBar, orderedTopTen, 4, divisor);
            UpdateFamily(family6Lbl, lastName6ProgressBar, orderedTopTen, 5, divisor);
            UpdateFamily(family7Lbl, lastName7ProgressBar, orderedTopTen, 6, divisor);
            UpdateFamily(family8Lbl, lastName8ProgressBar, orderedTopTen, 7, divisor);
            UpdateFamily(family9Lbl, lastName9ProgressBar, orderedTopTen, 8, divisor);
            UpdateFamily(family10Lbl, lastName10ProgressBar, orderedTopTen, 9, divisor);
        }

        private void UpdateFamily(Label familyLabel, ProgressBar progressBar, IEnumerable<KeyValuePair<string, int>> topTen, int familyRanking, double divisor)
        {
            var familyName = "";
            var percentage = 0.0;

            if (topTen.Count() > familyRanking)
            {
                var family = topTen.ElementAt(familyRanking);
                familyName = family.Key;
                percentage = family.Value / divisor;
            }


            TextUpdater.SetText(this, familyLabel, familyName);
            TextUpdater.SetProgressBar(this, progressBar, percentage);
        }

        private Dictionary<string, int> GetFamilyDetails()
        {
            var totalLineages = _mlbIteration.GARun.Population.Chromosomes.Length * 2;
            var dictionary = new Dictionary<string, int>();
            var popToCheck = _mlbIteration.GARun.Population;

            if (popToCheck.Chromosomes[0].ParentsLastNames[0] == null) { return null; }

            foreach (var chromosome in popToCheck.Chromosomes)
            {
                if (chromosome.ParentsLastNames[0] == null) { continue; }

                if (!dictionary.ContainsKey(chromosome.ParentsLastNames[0]))
                {
                    dictionary.Add(chromosome.ParentsLastNames[0], 0);
                }

                if (!dictionary.ContainsKey(chromosome.ParentsLastNames[1]))
                {
                    dictionary.Add(chromosome.ParentsLastNames[1], 0);
                }

                dictionary[chromosome.ParentsLastNames[0]] = dictionary[chromosome.ParentsLastNames[0]] + 1;
                dictionary[chromosome.ParentsLastNames[1]] = dictionary[chromosome.ParentsLastNames[1]] + 1;
            }

            return dictionary;
        }

        private void UpdateVersionLabel()
        {
            if (ApplicationDeployment.IsNetworkDeployed)
            {
                versionLbl.Text = ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
