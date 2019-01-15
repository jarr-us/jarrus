using GeneralHux.ErrorHandling;
using Jarrus.Data;
using Jarrus.GA;
using Jarrus.GA.Models;
using Jarrus.GA.Solution;
using System;
using System.Threading;

namespace Jarrus.Models
{
    public class TaskRunner
    {   
        public GARun GARun;
        public GAConfiguration Config;

        public void RunIteration()
        {
            var task = JarrusTaskRepository.Instance.GetTask();
            if (task == null || !task.IsValid()) { Thread.Sleep(1000); return; }
            var config = new GAConfiguration(task);

            RunOrderedConfiguration(config);
            RunUnorderedConfiguration(config);
        }

        private void RunUnorderedConfiguration(GAConfiguration config)
        {
            if (!config.IsUnorderedConfiguration()) { return; }

            var solution = (JarrusUnorderedSolution)config.Solution;
            var ga = new UnorderedGeneticAlgorithm(config, solution.GetNewGene(new Random()).GetType());

            try
            {
                Config = config;
                GARun = ga.GARun;
                RunConfiguration(ga);
            }
            catch (Exception ex)
            {
                try { ErrorHandlingSystem.HandleError(ex, "Something failed in the process."); } catch (Exception) { }
            }
        }

        private void RunConfiguration(GeneticAlgorithm ga)
        {
            ga.Run();
            JarrusTaskRepository.Instance.InsertCompletedRun(new TaskCompleted(GARun, Config));
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
