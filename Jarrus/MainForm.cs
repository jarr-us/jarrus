using Baseball.Models;
using GeneticAlgorithms;
using GeneticAlgorithms.Data;
using Jarrus.Metadata;
using Jarrus.Models;
using System;
using System.Deployment.Application;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace Jarrus
{
    public partial class MainForm : Form
    {
        private static GARun<Team> GARun;
        private static GAConfiguration<Team> Config;
        public static int RunNumber;
        private static Stopwatch _sw = new Stopwatch();
        private static int _lastGenSeen;

        private int _poolScoreMaxYSeen = 0;
        private double _minScoreSeen, _maxScoreSeen;

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
                RunKanan();
                UpdateChecker.Check();
            }
        }

        private void RunKanan()
        {
            var dao = new JarrusDAO();
            var task = dao.CheckoutATaskToRun<Team>();
            if (task.ParentSelection == null) { Thread.Sleep(1000); return; }

            var config = new GAConfiguration<Team>(task);
            Config = config;

            var teamdao = new TeamDAO();
            var data = teamdao.FetchAllGenes().ToArray();

            var ga = new GeneticAlgorithm<Team>(config, data);
            GARun = ga.GARun;
            UIUpdater.SetText(this, sessionNameLbl, config.Session);

            _minScoreSeen = ga.GARun.Population.Chromosomes.Select(o => o.FitnessScore).Min();
            _maxScoreSeen = ga.GARun.Population.Chromosomes.Select(o => o.FitnessScore).Max();

            var runDetails = ga.Run();
            dao.InsertCompletedRun(config, runDetails);
            dao.DeleteTask(task.UUID);
            RunNumber++;
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
            if (GARun == null || GARun == null) { return; }

            DrawIterationDetails();
            DrawMetadataDetails();
            DrawFamilyDetails();
            DrawConfigurationDetails();
            DrawCharts();
        }

        private void DrawCharts()
        {
            var chromosomes = GARun.Population.Chromosomes;
            var min = GARun.Population.Chromosomes.Select(o => o.FitnessScore).Min();
            var max = GARun.Population.Chromosomes.Select(o => o.FitnessScore).Max();

            if (_minScoreSeen > min) { _minScoreSeen = min; }
            if (_maxScoreSeen < max) { _maxScoreSeen = max; }

            var poolScoreGenerator = new PoolScoreGenerator<Team>(GARun.Population, _minScoreSeen, _maxScoreSeen);

            var maxScore = poolScoreGenerator.Points.Max(o => o.Value);
            if (maxScore > _poolScoreMaxYSeen)
            {
                _poolScoreMaxYSeen = (int)maxScore + 1;
            }

            UIUpdater.SetChart(this, poolScoreChart, poolScoreGenerator.Points, _minScoreSeen, _maxScoreSeen, _poolScoreMaxYSeen);
        }

        private void DrawIterationDetails()
        {
            if (GARun.Population.Chromosomes.Count() == 0) { return; }

            var allTimeBest = GARun.BestChromosome;
            var population = GARun.Population;
            if (population.Chromosomes.Count() == 0) { return; }

            var currentLowestScore = population.Chromosomes.Min(o => o.FitnessScore);
            var currentLowest = population.Chromosomes.Where(o => o.FitnessScore == currentLowestScore).First();

            UIUpdater.SetText(this, currentBestLowestScoreLbl, currentLowest.FitnessScore + "");
            UIUpdater.SetText(this, currentBestFirstNameLbl, currentLowest.FirstName + "");
            UIUpdater.SetText(this, currentBestLastNameLbl, currentLowest.LastName + "");
            UIUpdater.SetText(this, currentBestDirectDescendentsLbl, currentLowest.Children + "");

            UIUpdater.SetText(this, generationLbl, GARun.CurrentGeneration + "");
            UIUpdater.SetText(this, goatBestFirstNameLbl, allTimeBest.FirstName + "");
            UIUpdater.SetText(this, goatBestLastNameLbl, allTimeBest.LastName + "");
            UIUpdater.SetText(this, goatBestLowestScoreLbl, allTimeBest.FitnessScore + "");
            UIUpdater.SetText(this, goatDirectDescendentsLbl, allTimeBest.Children + "");

            UIUpdater.SetText(this, runsCompletedLbl, RunNumber + "");
        }

        private void DrawMetadataDetails()
        {
            _sw.Stop();
            var elapsedMs = _sw.ElapsedMilliseconds;
            var msPerGeneration = elapsedMs / (1.0 * (GARun.CurrentGeneration - _lastGenSeen));

            UIUpdater.SetText(this, retiredNumberLbl, GARun.Population.Retired.Count + "");
            UIUpdater.SetText(this, msPerGenLbl, msPerGeneration.ToString("#,##0.00"));

            _sw.Reset();
            _sw.Start();
            _lastGenSeen = GARun.CurrentGeneration;
        }

        private void DrawConfigurationDetails()
        {
            if (Config == null) { return; }

            UIUpdater.SetText(this, configPoolSizeLbl, Config.MaxPopulationSize + "");
            UIUpdater.SetText(this, configIterationsLbl, Config.MaxGenerations + "");
            UIUpdater.SetText(this, configCrossoverRateLbl, Config.CrossoverRate + "");
            UIUpdater.SetText(this, configMutationRateLbl, Config.MutationRate + "");
            UIUpdater.SetText(this, configElitismRateLbl, Config.ElitismRate + "");
            UIUpdater.SetText(this, configMaxLifeLbl, Config.MaximumLifeSpan + "");
            UIUpdater.SetText(this, configChildrenPerCoupleLbl, Config.ChildrenPerCouple + "");

            UIUpdater.SetText(this, configParentSelectionLbl, Config.ParentSelection.GetType().Name.Replace("Selection", "").ToString());
            UIUpdater.SetText(this, configCrossoverLbl, Config.Crossover.GetType().Name.Replace("Crossover", "").ToString());
            UIUpdater.SetText(this, configMutationLbl, Config.Mutation.GetType().Name.Replace("Mutation", "").ToString());
            UIUpdater.SetText(this, configRetirementLbl, "true");
        }

        private void DrawFamilyDetails()
        {
            var familyLineage = new FamilyLineage<Team>(GARun.Population);

            UpdateFamily(family1Lbl, lastName1ProgressBar, 0, familyLineage);
            UpdateFamily(family2Lbl, lastName2ProgressBar, 1, familyLineage);
            UpdateFamily(family3Lbl, lastName3ProgressBar, 2, familyLineage);
            UpdateFamily(family4Lbl, lastName4ProgressBar, 3, familyLineage);
            UpdateFamily(family5Lbl, lastName5ProgressBar, 4, familyLineage);
        }

        private void UpdateFamily(Label familyLabel, ProgressBar progressBar, int familyRanking, FamilyLineage<Team> lineageDetails)
        {
            var familyName = "";
            var percentage = 0.0;

            if (lineageDetails.GetRankingCount() > familyRanking)
            {

                var family = lineageDetails.GetFamilyAtRanking(familyRanking);
                familyName = family.ToString();

                var count = lineageDetails.GetCountOfFamilyAtRanking(familyRanking);
                percentage = (100.0 * count) / (lineageDetails.TotalLineages);
            }

            UIUpdater.SetText(this, familyLabel, familyName);
            UIUpdater.SetProgressBar(this, progressBar, percentage);
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
