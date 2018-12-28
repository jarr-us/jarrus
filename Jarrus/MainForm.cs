using Baseball.Models;
using GeneticAlgorithms;
using GeneticAlgorithms.Enums;
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

            var allTimeBest = _mlbIteration.GARun.LowestChromosome;

            var currentLowestScore = _mlbIteration.GARun.Population.Chromosomes.Min(o => o.FitnessScore);
            var currentLowest = _mlbIteration.GARun.Population.Chromosomes.Where(o => o.FitnessScore == currentLowestScore).First();
            
            TextUpdater.SetText(this, currentBestLowestScoreLbl, currentLowest.FitnessScore + "");
            TextUpdater.SetText(this, currentBestFirstNameLbl, currentLowest.FirstName + "");
            TextUpdater.SetText(this, currentBestLastNameLbl, currentLowest.LastName + "");

            TextUpdater.SetText(this, generationLbl, _mlbIteration.GARun.CurrentGeneration + "");
            TextUpdater.SetText(this, allTimeBestFirstNameLbl, allTimeBest.FirstName + "");
            TextUpdater.SetText(this, allTimeBestLastNameLbl, allTimeBest.LastName + "");
            TextUpdater.SetText(this, allTimeBestLowestScoreLbl, allTimeBest.FitnessScore + "");


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

        private void UpdateFamily(Label familyLabel, ProgressBar progressBar, IEnumerable<KeyValuePair<LastName, int>> topTen, int familyRanking, double divisor)
        {
            var familyName = "";
            var percentage = 0.0;

            if (topTen.Count() > familyRanking)
            {
                var family = topTen.ElementAt(familyRanking);
                familyName = family.Key.ToString();
                percentage = family.Value / divisor;
            }


            TextUpdater.SetText(this, familyLabel, familyName);
            TextUpdater.SetProgressBar(this, progressBar, percentage);
        }

        private Dictionary<LastName, int> GetFamilyDetails()
        {
            var totalLineages = _mlbIteration.GARun.Population.Chromosomes.Length * 2;
            var dictionary = new Dictionary<LastName, int>();
            var popToCheck = _mlbIteration.GARun.Population;
            
            foreach (var chromosome in popToCheck.Chromosomes)
            {
                if (chromosome.ParentsLastNames.Count() == 0) { continue; }

                foreach(var parent in chromosome.ParentsLastNames)
                {
                    if (!dictionary.ContainsKey(parent)) { dictionary.Add(parent, 0); }
                    dictionary[parent] = dictionary[parent] + 1;
                }
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
