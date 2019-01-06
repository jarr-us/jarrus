using GeneticAlgorithms;
using Jarrus.Data;
using Jarrus.Metadata;
using Jarrus.Models;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace Jarrus.Display
{
    public class TaskRunnerDisplay 
    {
        public TaskRunnerDisplay(Form form, FormControls controls) { Form = form;  Controls = controls; }

        protected Form Form;
        protected FormControls Controls;
        public GARun GARun;
        public GAConfiguration Config;
        public int RunNumber;
        public Stopwatch Stopwatch = new Stopwatch();
        public int LastGenerationSeen;

        public int PoolScoreMaxYSeen;
        public double MinScoreSeen, MaxScoreSeen;

        public bool IsReadyToUpdateForm() { return GARun != null; }
        public bool HasAPopulation() { return !GARun.Population.Chromosomes.Any(); }

        internal void Update()
        {
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

            if (MinScoreSeen > min) { MinScoreSeen = min; }
            if (MaxScoreSeen < max) { MaxScoreSeen = max; }

            var poolScoreGenerator = new PoolScoreGenerator(GARun.Population, MinScoreSeen, MaxScoreSeen);

            var maxScore = poolScoreGenerator.Points.Max(o => o.Value);
            if (maxScore > PoolScoreMaxYSeen)
            {
                PoolScoreMaxYSeen = (int)maxScore + 1;
            }

            UIUpdater.SetChart(Form, Controls.PoolScoreChart, poolScoreGenerator.Points, MinScoreSeen, MaxScoreSeen, PoolScoreMaxYSeen);
        }

        private void DrawIterationDetails()
        {
            if (HasAPopulation()) { return; }

            var allTimeBest = GARun.BestChromosome;
            var population = GARun.Population;
            if (population.Chromosomes.Count() == 0) { return; }

            var currentBestScore = population.Chromosomes.Min(o => o.FitnessScore);
            if (!Config.LowestScoreIsBest) { currentBestScore = population.Chromosomes.Max(o => o.FitnessScore); }

            var currentBest = population.Chromosomes.Where(o => o.FitnessScore == currentBestScore).First();

            UIUpdater.SetText(Form, Controls.CurrentBestLowestScoreLbl, currentBest.FitnessScore + "");
            UIUpdater.SetText(Form, Controls.CurrentBestFirstNameLbl, currentBest.FirstName + "");
            UIUpdater.SetText(Form, Controls.CurrentBestLastNameLbl, currentBest.LastName + "");
            UIUpdater.SetText(Form, Controls.CurrentBestDirectDescendentsLbl, currentBest.Children + "");

            UIUpdater.SetText(Form, Controls.GenerationLbl, GARun.CurrentGeneration + "");
            UIUpdater.SetText(Form, Controls.GoatBestFirstNameLbl, allTimeBest.FirstName + "");
            UIUpdater.SetText(Form, Controls.GoatBestLastNameLbl, allTimeBest.LastName + "");
            UIUpdater.SetText(Form, Controls.GoatBestLowestScoreLbl, allTimeBest.FitnessScore + "");
            UIUpdater.SetText(Form, Controls.GoatDirectDescendentsLbl, allTimeBest.Children + "");

            UIUpdater.SetText(Form, Controls.RunsCompletedLbl, RunNumber + "");
        }

        private void DrawMetadataDetails()
        {
            Stopwatch.Stop();
            var ticksPerChromosome = Stopwatch.ElapsedTicks / (GARun.Population.Chromosomes.Length * 1.0 * (GARun.CurrentGeneration - LastGenerationSeen));
            var msPerGeneration = Stopwatch.ElapsedMilliseconds / (1.0 * (GARun.CurrentGeneration - LastGenerationSeen));

            UIUpdater.SetText(Form, Controls.RetiredNumberLbl, GARun.Population.Retired.Count + "");
            UIUpdater.SetText(Form, Controls.TicksPerChromosome, ticksPerChromosome.ToString("#,##0"));
            UIUpdater.SetText(Form, Controls.MillisecondsPerGeneration, msPerGeneration.ToString("#,##0.00"));

            Stopwatch.Restart();
            LastGenerationSeen = GARun.CurrentGeneration;
        }

        private void DrawConfigurationDetails()
        {
            if (Config == null) { return; }

            UIUpdater.SetText(Form, Controls.ConfigPoolSizeLbl, Config.MaxPopulationSize + "");
            UIUpdater.SetText(Form, Controls.ConfigIterationsLbl, Config.MaxGenerations + "");
            UIUpdater.SetText(Form, Controls.ConfigCrossoverRateLbl, Config.CrossoverRate + "");
            UIUpdater.SetText(Form, Controls.ConfigMutationRateLbl, Config.MutationRate + "");
            UIUpdater.SetText(Form, Controls.ConfigElitismRateLbl, Config.ElitismRate + "");
            UIUpdater.SetText(Form, Controls.ConfigMaxLifeLbl, Config.MaximumLifeSpan + "");
            UIUpdater.SetText(Form, Controls.ConfigChildrenPerCoupleLbl, Config.ChildrenPerCouple + "");

            UIUpdater.SetText(Form, Controls.ConfigParentSelectionLbl, Config.ParentSelectionType.ToString());
            UIUpdater.SetText(Form, Controls.ConfigCrossoverLbl, Config.CrossoverType.ToString());
            UIUpdater.SetText(Form, Controls.ConfigMutationLbl, Config.MutationType.ToString());
            UIUpdater.SetText(Form, Controls.ConfigRetirementLbl, "true");
        }

        private void DrawFamilyDetails()
        {
            var familyLineage = new FamilyLineage(GARun.Population);

            UpdateFamily(Controls.Family1Lbl, Controls.Family1ProgressBar, 0, familyLineage);
            UpdateFamily(Controls.Family2Lbl, Controls.Family2ProgressBar, 1, familyLineage);
            UpdateFamily(Controls.Family3Lbl, Controls.Family3ProgressBar, 2, familyLineage);
            UpdateFamily(Controls.Family4Lbl, Controls.Family4ProgressBar, 3, familyLineage);
            UpdateFamily(Controls.Family5Lbl, Controls.Family5ProgressBar, 4, familyLineage);
        }

        private void UpdateFamily(Label familyLabel, ProgressBar progressBar, int familyRanking, FamilyLineage lineageDetails)
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

            UIUpdater.SetText(Form, familyLabel, familyName);
            UIUpdater.SetProgressBar(Form, progressBar, percentage);
        }

        public void RunIteration()
        {
            var jarrusDAO = new JarrusDAO();
            var task = jarrusDAO.CheckoutATaskToRun();
            if (task == null || !task.IsValid()) { Thread.Sleep(1000); return; }

            var config = new GAConfiguration(task);            
            var data = config.Solution.GetOptions();
            var ga = new GeneticAlgorithm(config, data);

            Config = config;
            GARun = ga.GARun;

            UIUpdater.SetText(Form, Controls.SessionNameLbl, config.Session);
            UIUpdater.SetText(Form, Controls.SolutionNameLbl, config.Solution.GetType().Name.Replace("Solution", "").ToString());
            MinScoreSeen = GARun.Population.Chromosomes.Select(o => o.FitnessScore).Min();
            MaxScoreSeen = GARun.Population.Chromosomes.Select(o => o.FitnessScore).Max();

            ga.Run();
            jarrusDAO.InsertCompletedRun(Config, GARun);
            jarrusDAO.DeleteTask(task.UUID);

            RunNumber++;
        }
    }
}
