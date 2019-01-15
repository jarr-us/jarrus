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
using System.Collections.Generic;

namespace Jarrus.Display
{
    public class TaskRunnerDisplay 
    {
        public Form Form;
        public TaskRunner TaskRunner;
        protected FormControls Controls;
        public Stopwatch Stopwatch = new Stopwatch();
        public int LastGenerationSeen;

        public int PoolScoreMaxYSeen;
        public double MinScoreSeen, MaxScoreSeen;

        public bool HasAPopulation() { return !TaskRunner.GARun.Population.Chromosomes.Any(); }
        private JarrusDAO _jarrusDAO = new JarrusDAO();
        private string _currentSolution;

        public TaskRunnerDisplay(TaskRunner taskRunner, Form form, FormControls controls)
        {
            Form = form;
            Controls = controls;
            TaskRunner = taskRunner;
        }

        public bool IsReadyToUpdateForm() {
            return TaskRunner.GARun != null;
        }

        internal void Update()
        {
            DrawIterationDetails();
            DrawMetadataDetails();
            DrawFamilyDetails();
            DrawConfigurationDetails();
            DrawCharts();
            DrawTaskRepoDetails();
        }

        private void DrawCharts()
        {
            if (_currentSolution != TaskRunner.Config.Solution.GetType().AssemblyQualifiedName)
            {
                _currentSolution = TaskRunner.Config.Solution.GetType().AssemblyQualifiedName;
                MinScoreSeen = TaskRunner.GARun.Population.Chromosomes.Select(o => o.FitnessScore).Min();
                MaxScoreSeen = TaskRunner.GARun.Population.Chromosomes.Select(o => o.FitnessScore).Max();
            }

            var chromosomes = TaskRunner.GARun.Population.Chromosomes;
            var min = TaskRunner.GARun.Population.Chromosomes.Select(o => o.FitnessScore).Min();
            var max = TaskRunner.GARun.Population.Chromosomes.Select(o => o.FitnessScore).Max();

            if (MinScoreSeen > min) { MinScoreSeen = min; }
            if (MaxScoreSeen < max) { MaxScoreSeen = max; }

            var poolScoreGenerator = new PoolScoreGenerator(TaskRunner.GARun.Population, MinScoreSeen, MaxScoreSeen);

            var maxScore = poolScoreGenerator.Points.Max(o => o.Value);
            if (maxScore > PoolScoreMaxYSeen)
            {
                PoolScoreMaxYSeen = (int)maxScore + 1;
            }

            UIUpdater.SetChart(Form, Controls.PoolScoreChart, poolScoreGenerator.Points, MinScoreSeen, MaxScoreSeen, PoolScoreMaxYSeen);
        }

        private void DrawTaskRepoDetails()
        {
            UIUpdater.SetText(Form, Controls.TaskRepoQueuedTasks, JarrusTaskRepository.Instance.GetNumberOfTasksToRun() + "");
            UIUpdater.SetText(Form, Controls.TaskRepoFinishedRuns, JarrusTaskRepository.Instance.GetNumberOfCompletedRunsToInsert() + "");
        }

        private void DrawIterationDetails()
        {
            if (HasAPopulation()) { return; }

            var allTimeBest = TaskRunner.GARun.BestChromosome;
            var population = TaskRunner.GARun.Population;
            if (population.Chromosomes.Count() == 0) { return; }

            var currentBestScore = population.Chromosomes.Min(o => o.FitnessScore);
            if (TaskRunner.Config.ScoringStrategy == ScoringStrategy.Highest) { currentBestScore = population.Chromosomes.Max(o => o.FitnessScore); }

            var currentBest = population.Chromosomes.Where(o => o.FitnessScore == currentBestScore).First();

            UIUpdater.SetText(Form, Controls.CurrentBestLowestScoreLbl, currentBest.FitnessScore + "");
            UIUpdater.SetText(Form, Controls.CurrentBestFirstNameLbl, currentBest.FirstName + "");
            UIUpdater.SetText(Form, Controls.CurrentBestLastNameLbl, currentBest.LastName + "");
            UIUpdater.SetText(Form, Controls.CurrentBestDirectDescendentsLbl, currentBest.Children + "");

            UIUpdater.SetText(Form, Controls.GenerationLbl, TaskRunner.GARun.CurrentGeneration + "");
            UIUpdater.SetText(Form, Controls.GoatBestFirstNameLbl, allTimeBest.FirstName + "");
            UIUpdater.SetText(Form, Controls.GoatBestLastNameLbl, allTimeBest.LastName + "");
            UIUpdater.SetText(Form, Controls.GoatBestLowestScoreLbl, allTimeBest.FitnessScore + "");
            UIUpdater.SetText(Form, Controls.GoatDirectDescendentsLbl, allTimeBest.Children + "");

            UIUpdater.SetText(Form, Controls.RunsCompletedLbl, JarrusTaskRepository.Instance.NumberOfRunsCompleted + "");
        }

        private void DrawMetadataDetails()
        {
            Stopwatch.Stop();
            var ticksPerChromosome = Stopwatch.ElapsedTicks / (TaskRunner.GARun.Population.Chromosomes.Length * 1.0 * (TaskRunner.GARun.CurrentGeneration - LastGenerationSeen));
            var msPerGeneration = Stopwatch.ElapsedMilliseconds / (1.0 * (TaskRunner.GARun.CurrentGeneration - LastGenerationSeen));

            if (double.IsInfinity(msPerGeneration)) { Stopwatch.Restart(); return; }

            UIUpdater.SetText(Form, Controls.RetiredNumberLbl, TaskRunner.GARun.Population.Retired.Count + "");
            UIUpdater.SetText(Form, Controls.TicksPerChromosome, ticksPerChromosome.ToString("#,##0"));
            UIUpdater.SetText(Form, Controls.MillisecondsPerGeneration, msPerGeneration.ToString("#,##0.00"));

            Stopwatch.Restart();
            LastGenerationSeen = TaskRunner.GARun.CurrentGeneration;
        }

        private void DrawConfigurationDetails()
        {
            if (TaskRunner.Config == null) { return; }

            UIUpdater.SetText(Form, Controls.SessionNameLbl, TaskRunner.Config.Session);
            UIUpdater.SetText(Form, Controls.SolutionNameLbl, TaskRunner.Config.Solution.GetType().Name.Replace("Solution", "").ToString());
            
            UIUpdater.SetText(Form, Controls.ConfigPoolSizeLbl, TaskRunner.Config.PopulationSize + "");
            UIUpdater.SetText(Form, Controls.ConfigIterationsLbl, TaskRunner.Config.MaxGenerations + "");
            UIUpdater.SetText(Form, Controls.ConfigCrossoverRateLbl, TaskRunner.Config.CrossoverRate + "");
            UIUpdater.SetText(Form, Controls.ConfigMutationRateLbl, TaskRunner.Config.MutationRate + "");
            UIUpdater.SetText(Form, Controls.ConfigElitismRateLbl, TaskRunner.Config.ElitismRate + "");
            UIUpdater.SetText(Form, Controls.ConfigImmigrationRateLbl, TaskRunner.Config.ImmigrationRate + "");
            UIUpdater.SetText(Form, Controls.ConfigMaxLifeLbl, TaskRunner.Config.MaxRetirement + "");
            UIUpdater.SetText(Form, Controls.ConfigChildrenPerCoupleLbl, TaskRunner.Config.ChildrenPerParents + "");

            UIUpdater.SetText(Form, Controls.ConfigParentSelectionLbl, TaskRunner.Config.ParentSelectionStrategy.ToString());
            UIUpdater.SetText(Form, Controls.ConfigCrossoverLbl, TaskRunner.Config.CrossoverStrategy.ToString());
            UIUpdater.SetText(Form, Controls.ConfigMutationLbl, TaskRunner.Config.MutationStrategy.ToString());
            UIUpdater.SetText(Form, Controls.ConfigRetirementLbl, TaskRunner.Config.RetirementStrategy.ToString());
            UIUpdater.SetText(Form, Controls.ConfigImmigrationLbl, TaskRunner.Config.ImmigrationStrategy.ToString());
            UIUpdater.SetText(Form, Controls.ConfigScoringLbl, TaskRunner.Config.ScoringStrategy.ToString());
            UIUpdater.SetText(Form, Controls.ConfigDuplicationLbl, TaskRunner.Config.DuplicationStrategy.ToString());
        }

        private void DrawFamilyDetails()
        {
            var familyLineage = new FamilyLineage(TaskRunner.GARun.Population);

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
    }
}
