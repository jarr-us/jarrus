using Jarrus.GA;
using Jarrus.Data;
using Jarrus.Metadata;
using Jarrus.Models;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Jarrus.GA.Factory.Enums;
using Jarrus.GA.Models;
using Jarrus.GA.Solution;
using GeneralHux.ErrorHandling;

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
        private JarrusDAO _jarrusDAO = new JarrusDAO();

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
            if (Config.ScoringStrategy == ScoringStrategy.Highest) { currentBestScore = population.Chromosomes.Max(o => o.FitnessScore); }

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

            if (double.IsInfinity(msPerGeneration)) { Stopwatch.Restart(); return; }

            UIUpdater.SetText(Form, Controls.RetiredNumberLbl, GARun.Population.Retired.Count + "");
            UIUpdater.SetText(Form, Controls.TicksPerChromosome, ticksPerChromosome.ToString("#,##0"));
            UIUpdater.SetText(Form, Controls.MillisecondsPerGeneration, msPerGeneration.ToString("#,##0.00"));

            Stopwatch.Restart();
            LastGenerationSeen = GARun.CurrentGeneration;
        }

        private void DrawConfigurationDetails()
        {
            if (Config == null) { return; }

            UIUpdater.SetText(Form, Controls.ConfigPoolSizeLbl, Config.PopulationSize + "");
            UIUpdater.SetText(Form, Controls.ConfigIterationsLbl, Config.MaxGenerations + "");
            UIUpdater.SetText(Form, Controls.ConfigCrossoverRateLbl, Config.CrossoverRate + "");
            UIUpdater.SetText(Form, Controls.ConfigMutationRateLbl, Config.MutationRate + "");
            UIUpdater.SetText(Form, Controls.ConfigElitismRateLbl, Config.ElitismRate + "");
            UIUpdater.SetText(Form, Controls.ConfigImmigrationRateLbl, Config.ImmigrationRate + "");
            UIUpdater.SetText(Form, Controls.ConfigMaxLifeLbl, Config.MaxRetirement + "");
            UIUpdater.SetText(Form, Controls.ConfigChildrenPerCoupleLbl, Config.ChildrenPerParents + "");

            UIUpdater.SetText(Form, Controls.ConfigParentSelectionLbl, Config.ParentSelectionStrategy.ToString());
            UIUpdater.SetText(Form, Controls.ConfigCrossoverLbl, Config.CrossoverStrategy.ToString());
            UIUpdater.SetText(Form, Controls.ConfigMutationLbl, Config.MutationStrategy.ToString());
            UIUpdater.SetText(Form, Controls.ConfigRetirementLbl, Config.RetirementStrategy.ToString());
            UIUpdater.SetText(Form, Controls.ConfigImmigrationLbl, Config.ImmigrationStrategy.ToString());
            UIUpdater.SetText(Form, Controls.ConfigScoringLbl, Config.ScoringStrategy.ToString());
            UIUpdater.SetText(Form, Controls.ConfigDuplicationLbl, Config.DuplicationStrategy.ToString());
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
            var task = _jarrusDAO.CheckoutATaskToRun();
            if (task == null || !task.IsValid()) { Thread.Sleep(1000); return; }
            var config = new GAConfiguration(task);

            RunOrderedConfiguration(config);
            RunUnorderedConfiguration(config);
        }

        private void RunUnorderedConfiguration(GAConfiguration config)
        {
            if (!config.IsUnorderedConfiguration()) { return; }

            var solution = (JarrusUnorderedSolution)config.Solution;
            var ga = new UnorderedGeneticAlgorithm(config, solution.GetGeneType());

            try {
                Config = config;
                GARun = ga.GARun;
                RunConfiguration(ga);
            } catch (Exception ex)
            {
                try { ErrorHandlingSystem.HandleError(ex, "Something failed in the process."); } catch (Exception) {  }
            }            
        }

        private void RunConfiguration(GeneticAlgorithm ga)
        {
            UIUpdater.SetText(Form, Controls.SessionNameLbl, Config.Session);
            UIUpdater.SetText(Form, Controls.SolutionNameLbl, Config.Solution.GetType().Name.Replace("Solution", "").ToString());
            MinScoreSeen = GARun.Population.Chromosomes.Select(o => o.FitnessScore).Min();
            MaxScoreSeen = GARun.Population.Chromosomes.Select(o => o.FitnessScore).Max();

            ga.Run();
            _jarrusDAO.InsertCompletedRun(Config, GARun);
            _jarrusDAO.DeleteTask(Config.TaskUUID);

            RunNumber++;
        }

        private void RunOrderedConfiguration(GAConfiguration config)
        {
            if (!config.IsOrderedConfiguration()) { return; }

            var solution = (JarrusOrderedSolution)config.Solution;

            var data = solution.GetOptions();
            var ga = new OrderedGeneticAlgorithm(config, data);

            Config = config;
            GARun = ga.GARun;
            RunConfiguration(ga);
        }
    }
}
