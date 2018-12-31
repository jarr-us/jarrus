using Baseball.Models;
using Jarrus.Metadata;
using Jarrus.Models;
using Kanan;
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
        private MLBCircleIteration<Team> _mlbIteration;
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
                RunNumber++;
                RunKanan();
                UpdateChecker.Check();
            }
        }

        private void RunKanan()
        {
            _mlbIteration = new MLBCircleIteration<Team>();

            _minScoreSeen = _mlbIteration.GeneticAlgorithm.GARun.Population.Chromosomes[0].FitnessScore;
            _maxScoreSeen = _mlbIteration.GeneticAlgorithm.GARun.Population.Chromosomes[0].FitnessScore;

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
            if (_mlbIteration == null || _mlbIteration.GeneticAlgorithm.GARun == null) { return; }

            DrawIterationDetails();
            DrawMetadataDetails();
            DrawFamilyDetails();
            DrawConfigurationDetails();
            DrawCharts();
        }

        private void DrawCharts()
        {
            var chromosomes = _mlbIteration.GeneticAlgorithm.GARun.Population.Chromosomes;
            var min = _mlbIteration.GeneticAlgorithm.GARun.Population.Chromosomes.Select(o => o.FitnessScore).Min();
            var max = _mlbIteration.GeneticAlgorithm.GARun.Population.Chromosomes.Select(o => o.FitnessScore).Max();

            if (_minScoreSeen > min) { _minScoreSeen = min; }
            if (_maxScoreSeen < max) { _maxScoreSeen = max; }

            var poolScoreGenerator = new PoolScoreGenerator<Team>(_mlbIteration.GeneticAlgorithm.GARun.Population, _minScoreSeen, _maxScoreSeen);

            var maxScore = poolScoreGenerator.Points.Max(o => o.Value);
            if (maxScore > _poolScoreMaxYSeen)
            {
                _poolScoreMaxYSeen = (int)maxScore + 1;
            }

            UIUpdater.SetChart(this, poolScoreChart, poolScoreGenerator.Points, _minScoreSeen, _maxScoreSeen, _poolScoreMaxYSeen);
        }

        private void DrawIterationDetails()
        {
            if (_mlbIteration.GeneticAlgorithm.GARun.Population.Chromosomes.Count() == 0) { return; }

            var allTimeBest = _mlbIteration.GeneticAlgorithm.GARun.LowestChromosome;
            var population = _mlbIteration.GeneticAlgorithm.GARun.Population;
            if (population.Chromosomes.Count() == 0) { return; }

            var currentLowestScore = population.Chromosomes.Min(o => o.FitnessScore);
            var currentLowest = population.Chromosomes.Where(o => o.FitnessScore == currentLowestScore).First();

            UIUpdater.SetText(this, currentBestLowestScoreLbl, currentLowest.FitnessScore + "");
            UIUpdater.SetText(this, currentBestFirstNameLbl, currentLowest.FirstName + "");
            UIUpdater.SetText(this, currentBestLastNameLbl, currentLowest.LastName + "");
            UIUpdater.SetText(this, currentBestDirectDescendentsLbl, currentLowest.Children + "");

            UIUpdater.SetText(this, generationLbl, _mlbIteration.GeneticAlgorithm.GARun.CurrentGeneration + "");
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
            var msPerGeneration = elapsedMs / (1.0 * (_mlbIteration.GeneticAlgorithm.GARun.CurrentGeneration - _lastGenSeen));

            UIUpdater.SetText(this, retiredNumberLbl, _mlbIteration.GeneticAlgorithm.Retired.Count + "");
            UIUpdater.SetText(this, msPerGenLbl, msPerGeneration.ToString("#,##0.00"));

            _sw.Reset();
            _sw.Start();
            _lastGenSeen = _mlbIteration.GeneticAlgorithm.GARun.CurrentGeneration;
        }

        private void DrawConfigurationDetails()
        {
            var config = _mlbIteration.Configuration;

            UIUpdater.SetText(this, configPoolSizeLbl, config.PoolSize + "");
            UIUpdater.SetText(this, configIterationsLbl, config.Iterations + "");
            UIUpdater.SetText(this, configCrossoverRateLbl, config.CrossoverRate + "");
            UIUpdater.SetText(this, configMutationRateLbl, config.MutationRate + "");
            UIUpdater.SetText(this, configElitismRateLbl, config.ElitismRate + "");
            UIUpdater.SetText(this, configMaxLifeLbl, config.MaximumLifeSpan + "");
            UIUpdater.SetText(this, configChildrenPerCoupleLbl, config.ChildrenPerCouple + "");

            UIUpdater.SetText(this, configParentSelectionLbl, config.ParentSelection.GetType().Name.Replace("Selection", "").ToString());
            UIUpdater.SetText(this, configCrossoverLbl, config.Crossover.GetType().Name.Replace("Crossover", "").ToString());
            UIUpdater.SetText(this, configMutationLbl, config.Mutation.GetType().Name.Replace("Mutation", "").ToString());
            UIUpdater.SetText(this, configRetirementLbl, "true");
        }

        private void DrawFamilyDetails()
        {
            var familyLineage = new FamilyLineage<Team>(_mlbIteration.GeneticAlgorithm.GARun.Population);

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
