using System.Collections.Generic;
using System.Threading.Tasks;
using Jarrus.GA;
using Jarrus.GA.Models;
using Jarrus.GA.Factory.Enums;
using Jarrus.Data;
using Kanan.MLBDriveTime.Jarrus;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Kanan.OfficeSurveys;
using Kanan.Shakespeare;

namespace JarrusTests.Data
{
    [TestClass]
    public class JarrusDAOTests
    {
        [TestMethod]
        public void ItIsRepeatable()
        {
            var firstRunScore = GetFitnessScoreForExample();
            var secondRunScore = GetFitnessScoreForExample();

            Assert.AreEqual(firstRunScore, secondRunScore);
        }

        private double GetFitnessScoreForExample()
        {
            var solution = new ShakespeareSolution();
            var task = new GATask(solution);
            task.ParentSelectionType = ParentSelectionType.StochasticUniversalSamplingSelection;
            task.MutationType = MutationType.Random;
            task.CrossoverType = CrossoverType.TwoPoint;
            task.ImmigrationType = ImmigrationType.None;
            task.RetirementType = RetirementType.MaxChildren;
            task.ScoringType = ScoringType.Lowest;
            task.DuplicationType = DuplicationType.Prevent;
            
            task.PopulationSize = 1000;
            task.MaxGenerations = 10;
            task.CrossoverRate = 0.936670;
            task.MutationRate = 0.127500;
            task.ElitismRate = 0.092570;
            task.ImmigrationRate = 0.083820;
            task.MaxRetirement = 110;
            task.ChildrenPerParents = 3;
            task.RandomSeed = 1132120617;
            task.RandomPoolGenerationSeed = 22;

            var config = new GAConfiguration(task);

            var gaRun = solution.Run(config);

            return gaRun.BestChromosome.FitnessScore;
        }

        //[TestMethod]
        //public void ItCanInsertUnorderedFullRandoms()
        //{
        //    var tasks = new List<GATask>();

        //    var random = new Random();
        //    for (int i = 0; i < 788; i++)
        //    {
        //        var task = new GATask(new ShakespeareSolution())
        //        {
        //            ParentSelectionType = ParentSelectionType.StochasticUniversalSamplingSelection,
        //            MutationType = MutationType.Random,
        //            CrossoverType = CrossoverType.SinglePoint,
        //            RetirementType = RetirementType.MaxAge,
        //            ImmigrationType = ImmigrationType.Dynamic,
        //            ScoringType = ScoringType.Lowest,
        //            DuplicationType = DuplicationType.Prevent,

        //            MaxGenerations = 100,
        //            RandomPoolGenerationSeed = 22,

        //            Session = "0018"
        //        };

        //        var randomNumber = random.Next(1, 2 + 1);
        //        if (randomNumber == 2) task.ParentSelectionType = ParentSelectionType.RouletteWheel;

        //        randomNumber = random.Next(1, 3 + 1);
        //        if (randomNumber == 2) { task.CrossoverType = CrossoverType.TwoPoint; }
        //        if (randomNumber == 3) { task.CrossoverType = CrossoverType.Uniform; }

        //        randomNumber = random.Next(1, 3 + 1);
        //        if (randomNumber == 2) { task.ImmigrationType = ImmigrationType.Constant; }
        //        if (randomNumber == 3) { task.ImmigrationType = ImmigrationType.None; }

        //        randomNumber = random.Next(1, 3 + 1);
        //        if (randomNumber == 2) { task.RetirementType = RetirementType.None; }
        //        if (randomNumber == 3) { task.RetirementType = RetirementType.MaxChildren; }

        //        randomNumber = random.Next(1, 2 + 1);
        //        if (randomNumber == 2) { task.DuplicationType = DuplicationType.Allow; }

        //        task.PopulationSize = 1000;

        //        randomNumber = random.Next(88000, 98000 + 1);
        //        task.CrossoverRate = randomNumber * 0.00001;

        //        randomNumber = random.Next(1000, 15000 + 1);
        //        task.MutationRate = randomNumber * 0.00001;

        //        randomNumber = random.Next(5000, 25000 + 1);
        //        task.ElitismRate = randomNumber * 0.00001;

        //        randomNumber = random.Next(2000, 10000 + 1);
        //        task.ImmigrationRate = randomNumber * 0.00001;

        //        randomNumber = random.Next((task.MaxGenerations / 8), (task.MaxGenerations / 4) + 1);
        //        task.MaxRetirement = randomNumber;

        //        randomNumber = random.Next(1, 8 + 1);
        //        task.ChildrenPerParents = randomNumber;

        //        task.RandomSeed = random.Next();
        //        tasks.Add(task);
        //    }

        //    InsertTasksToDatabase(tasks, 18);
        //}

        [TestMethod]
        public void ItCanInsertOrderedFullRandoms()
        {
            var tasks = new List<GATask>();

            var random = new Random();
            for (int i = 0; i < 50000; i++)
            {
                var task = new GATask(new MLBDriveTimeSolution())
                {
                    ParentSelectionType = ParentSelectionType.StochasticUniversalSamplingSelection,
                    MutationType = MutationType.Swap,
                    CrossoverType = CrossoverType.Cycle,
                    RetirementType = RetirementType.MaxAge,
                    ImmigrationType = ImmigrationType.Dynamic,
                    ScoringType = ScoringType.Lowest,
                    DuplicationType = DuplicationType.Prevent,

                    MaxGenerations = 1000,
                    RandomPoolGenerationSeed = 22,

                    Session = "0017"
                };

                var randomNumber = random.Next(1, 2 + 1);
                if (randomNumber == 2) task.ParentSelectionType = ParentSelectionType.RouletteWheel;

                randomNumber = random.Next(1, 4 + 1);
                if (randomNumber == 2) { task.MutationType = MutationType.Insert; }
                if (randomNumber == 3) { task.MutationType = MutationType.Scramble; }
                if (randomNumber == 4) { task.MutationType = MutationType.Inversion; }

                randomNumber = random.Next(1, 4 + 1);
                if (randomNumber == 2) { task.CrossoverType = CrossoverType.AlternatingPosition; }
                if (randomNumber == 3) { task.CrossoverType = CrossoverType.Order; }
                if (randomNumber == 4) { task.CrossoverType = CrossoverType.PartialMapped; }

                randomNumber = random.Next(1, 3 + 1);
                if (randomNumber == 2) { task.ImmigrationType = ImmigrationType.Constant; }
                if (randomNumber == 3) { task.ImmigrationType = ImmigrationType.None; }

                randomNumber = random.Next(1, 3 + 1);
                if (randomNumber == 2) { task.RetirementType = RetirementType.None; }
                if (randomNumber == 3) { task.RetirementType = RetirementType.MaxChildren; }

                randomNumber = random.Next(1, 2 + 1);
                if (randomNumber == 2) { task.DuplicationType = DuplicationType.Allow; }

                randomNumber = random.Next(50, 150 + 1);
                task.PopulationSize = randomNumber;

                randomNumber = random.Next(88000, 98000 + 1);
                task.CrossoverRate = randomNumber * 0.00001;

                randomNumber = random.Next(1000, 15000 + 1);
                task.MutationRate = randomNumber * 0.00001;

                randomNumber = random.Next(5000, 25000 + 1);
                task.ElitismRate = randomNumber * 0.00001;

                randomNumber = random.Next(2000, 10000 + 1);
                task.ImmigrationRate = randomNumber * 0.00001;

                randomNumber = random.Next((task.MaxGenerations / 8), (task.MaxGenerations / 2) + 1);
                task.MaxRetirement = randomNumber;

                randomNumber = random.Next(1, 8 + 1);
                task.ChildrenPerParents = randomNumber;

                task.RandomSeed = random.Next();
                tasks.Add(task);
            }

            InsertTasksToDatabase(tasks, 18.1);
        }

        //[TestMethod]
        //public void ItCanInsertSession12s()
        //{
        //    var tasks = new List<GATask>();


        //    var mutationRate = 0.010000;
        //    while(mutationRate < 0.03)
        //    {
        //        var task = new GATask(new OfficeSurveySolution())
        //        {
        //            ParentSelectionType = ParentSelectionType.StochasticUniversalSamplingSelection,
        //            MutationType = MutationType.Swap,
        //            CrossoverType = CrossoverType.PartialMapped,

        //            MaxPopulationSize = 141,
        //            CrossoverRate = 0.956050,
        //            MutationRate = mutationRate,
        //            ElitismRate = 0.155190,
        //            MaximumLifeSpan = 1059,
        //            ChildrenPerCouple = 7,

        //            MaxGenerations = 2000,
        //            LowestScoreIsBest = false,
        //            PreventDuplications = true,
        //            RandomPoolGenerationSeed = 22,
        //            RandomSeed = 233754038,

        //            //sessions 6-11: EA88A653-85C9-497E-BB2E-E5787150CC02
        //            //session 12: 1BED407A-B54B-42E5-AA52-9E28779F4FB0

        //            Session = "0014"
        //        };

        //        tasks.Add(task);
        //        mutationRate += 0.00001;
        //    }

        //    InsertTasksToDatabase(tasks, 14);
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
