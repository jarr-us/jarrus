using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneticAlgorithms;
using GeneticAlgorithms.BasicTypes;
using GeneticAlgorithms.Enums;
using GeneticAlgorithms.Factory.Enums;
using Jarrus.Data;
using Kanan.MLBDriveTime.Jarrus;
using Kanan.OfficeSurveys;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JarrusTests.Data
{
    [TestClass]
    public class JarrusDAOTests
    {
        [TestMethod]
        public void ItIsRepeatable()
        {
            var solution = new MLBDriveTimeSolution();
            var task = new GATask(solution);
            task.ParentSelectionType = ParentSelectionType.StochasticUniversalSamplingSelection;
            task.MutationType = MutationType.Inversion;
            task.CrossoverType = CrossoverType.Order;
            task.LowestScoreIsBest = true;
            task.MaxPopulationSize = 50;
            task.MaxGenerations = 509 + 417;
            task.CrossoverRate = 0.93;
            task.MutationRate = 0.08;
            task.ElitismRate = 0.30;
            task.PreventDuplications = true;
            task.MaximumLifeSpan = 416;
            task.ChildrenPerCouple = 4;
            task.RandomSeed = 808816214;
            task.RandomPoolGenerationSeed = 22;

            var config = new GAConfiguration(task);
            
            var ga = new GeneticAlgorithm(config, solution.GetOptions());
            var runDetails = ga.Run();
            Assert.AreEqual(config.MaxGenerations + 1, ga.Generation);

            var currentBest = runDetails.Population.Chromosomes.OrderBy(o => o.FitnessScore).First();
            Assert.AreEqual(579289.000000, runDetails.BestChromosome.FitnessScore);
            Assert.AreNotEqual(runDetails.BestChromosome.FitnessScore, currentBest.FitnessScore);
            Assert.AreEqual(584390, currentBest.FitnessScore);
            Assert.AreEqual(LastName.Espinosa, currentBest.LastName);
            Assert.AreEqual(LastName.Espinosa, runDetails.BestChromosome.LastName);
        }
        
        [TestMethod]
        public void ItCanInsertFullRandoms()
        {
            var tasks = new List<GATask>();

            var random = new Random();
            for (int i = 0; i < 5000; i++)
            {
                var task = new GATask(new OfficeSurveySolution())
                {
                    ParentSelectionType = ParentSelectionType.StochasticUniversalSamplingSelection,
                    MutationType = MutationType.Swap,
                    CrossoverType = CrossoverType.Cycle,

                    MaxPopulationSize = 112,
                    CrossoverRate = 0.96,
                    MutationRate = 0.14,
                    ElitismRate = 0.25,
                    MaximumLifeSpan = 637,
                    ChildrenPerCouple = 5,

                    MaxGenerations = 20000,
                    LowestScoreIsBest = false,
                    PreventDuplications = true,
                    RandomPoolGenerationSeed = 22,

                    Session = "0005"
                };

                var randomNumber = random.Next(1, 2 + 1);
                if (randomNumber == 2) task.ParentSelectionType = ParentSelectionType.RouletteWheel;

                randomNumber = random.Next(1, 4 + 1);
                if (randomNumber == 2) { task.MutationType = MutationType.Insert; }
                if (randomNumber == 3) { task.MutationType = MutationType.Scramble; }
                if (randomNumber == 4) { task.MutationType = MutationType.Swap; }

                randomNumber = random.Next(1, 4 + 1);
                if (randomNumber == 2) { task.CrossoverType = CrossoverType.AlternatingPosition; }
                if (randomNumber == 3) { task.CrossoverType = CrossoverType.Cycle; }
                if (randomNumber == 4) { task.CrossoverType = CrossoverType.PartialMapped; }

                randomNumber = random.Next(50, 150 + 1);
                task.MaxPopulationSize = randomNumber;

                randomNumber = random.Next(60000, 99000 + 1);
                task.CrossoverRate = randomNumber * 0.00001;

                randomNumber = random.Next(1000, 15000 + 1);
                task.MutationRate = randomNumber * 0.00001;

                randomNumber = random.Next(7000, 25000 + 1);
                task.ElitismRate = randomNumber * 0.00001;

                randomNumber = random.Next(500, 1300 + 1);
                task.MaximumLifeSpan = randomNumber;

                randomNumber = random.Next(1, 8 + 1);
                task.ChildrenPerCouple = randomNumber;

                task.RandomSeed = random.Next();
                tasks.Add(task);
            }

            InsertTasksToDatabase(tasks, 5);
        }

        //[TestMethod]
        //public void ItCanInsertSessionSixes()
        //{
        //    var tasks = new List<GATask>();

        //    var random = new Random();
        //    for (int i = 0; i < 5000; i++)
        //    {
        //        var task = new GATask(new MLBDriveTimeSolution())
        //        {
        //            ParentSelectionType = ParentSelectionType.StochasticUniversalSamplingSelection,
        //            MutationType = MutationType.Swap,
        //            CrossoverType = CrossoverType.AlternatingPosition,

        //            MaxPopulationSize = 50,
        //            CrossoverRate = 0.68,
        //            MutationRate = 0.13,
        //            ElitismRate = 0.35,
        //            MaximumLifeSpan = 961,
        //            ChildrenPerCouple = 2,

        //            MaxGenerations = 10000,
        //            LowestScoreIsBest = true,
        //            PreventDuplications = true,
        //            RandomPoolGenerationSeed = 22,

        //            Session = "0006"
        //        };

        //        task.RandomSeed = random.Next();
        //        tasks.Add(task);
        //    }

        //    InsertTasksToDatabase(tasks, 4.5);
        //}

        private void InsertTasksToDatabase(List<GATask> tasks, double priority)
        {
            var dao = new JarrusDAO();

            Parallel.ForEach(tasks, task =>
            {
                dao.InsertTask(task, priority);
            });
        }
    }
}
