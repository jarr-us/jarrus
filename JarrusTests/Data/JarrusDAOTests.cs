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
            var frun = GetRunForExample();
            var srun = GetRunForExample();

            Assert.AreEqual(frun.BestChromosome.FitnessScore, srun.BestChromosome.FitnessScore);
        }

        [TestMethod]
        public void ItsGenerationReflectsTheCorrectValueUponTermination()
        {
            var run = GetRunForExample();
            Assert.AreEqual(run.CurrentGeneration, run.BestChromosome.GenerationNumber);
        }

        private GARun GetRunForExample()
        {
            var solution = new ShakespeareSolution();
            var task = new GATask(solution);
            task.ParentSelectionStrategy = ParentSelectionStrategy.TournamentFive;
            task.MutationStrategy = MutationStrategy.Random;
            task.CrossoverStrategy = CrossoverStrategy.TwoPoint;
            task.ImmigrationStrategy = ImmigrationStrategy.Dynamic;
            task.RetirementStrategy = RetirementStrategy.None;
            task.ScoringStrategy = ScoringStrategy.Lowest;
            task.DuplicationStrategy = DuplicationStrategy.Prevent;
            
            task.PopulationSize = 1000;
            task.MaxGenerations = 100;
            task.CrossoverRate = 0.659410;
            task.MutationRate = 0.074880;
            task.ElitismRate = 0.021880;
            task.ImmigrationRate = 0.150480;
            task.MaxRetirement = 0;
            task.ChildrenPerParents = 1;
            task.RandomSeed = 567545391;
            task.RandomPoolGenerationSeed = 22;

            var config = new GAConfiguration(task);

            var gaRun = solution.Run(config);

            return gaRun;
        }

        //[TestMethod]
        //public void ItCanInsertUnorderedRandoms()
        //{
        //    var tasks = new List<GATask>();

        //    var random = new Random();
        //    for (int i = 0; i < 150000; i++)
        //    {
        //        var task = new GATask(new ShakespeareSolution())
        //        {
        //            ParentSelectionStrategy = ParentSelectionStrategy.StochasticUniversalSamplingSelection,
        //            MutationStrategy = MutationStrategy.Random,
        //            CrossoverStrategy = CrossoverStrategy.SinglePoint,
        //            RetirementStrategy = RetirementStrategy.MaxAge,
        //            ImmigrationStrategy = ImmigrationStrategy.Dynamic,
        //            ScoringStrategy = ScoringStrategy.Lowest,
        //            DuplicationStrategy = DuplicationStrategy.Prevent,

        //            MaxGenerations = 100,
        //            RandomPoolGenerationSeed = 22,

        //            Session = "0019"
        //        };

        //        var randomNumber = random.Next(1, 7 + 1);
        //        if (randomNumber == 2) task.ParentSelectionStrategy = ParentSelectionStrategy.RouletteWheel;
        //        if (randomNumber == 3) task.ParentSelectionStrategy = ParentSelectionStrategy.Rank;
        //        if (randomNumber == 4) task.ParentSelectionStrategy = ParentSelectionStrategy.TournamentTwo;
        //        if (randomNumber == 5) task.ParentSelectionStrategy = ParentSelectionStrategy.TournamentThree;
        //        if (randomNumber == 6) task.ParentSelectionStrategy = ParentSelectionStrategy.TournamentFour;
        //        if (randomNumber == 7) task.ParentSelectionStrategy = ParentSelectionStrategy.TournamentFive;

        //        randomNumber = random.Next(1, 3 + 1);
        //        if (randomNumber == 2) { task.CrossoverStrategy = CrossoverStrategy.TwoPoint; }
        //        if (randomNumber == 3) { task.CrossoverStrategy = CrossoverStrategy.Uniform; }

        //        randomNumber = random.Next(1, 3 + 1);
        //        if (randomNumber == 2) { task.ImmigrationStrategy = ImmigrationStrategy.Constant; }
        //        if (randomNumber == 3) { task.ImmigrationStrategy = ImmigrationStrategy.None; }

        //        randomNumber = random.Next(1, 3 + 1);
        //        if (randomNumber == 2) { task.RetirementStrategy = RetirementStrategy.None; }
        //        if (randomNumber == 3) { task.RetirementStrategy = RetirementStrategy.MaxChildren; }

        //        randomNumber = random.Next(1, 2 + 1);
        //        if (randomNumber == 2) { task.DuplicationStrategy = DuplicationStrategy.Allow; }

        //        task.PopulationSize = 1000;

        //        randomNumber = random.Next(60000, 99000 + 1);
        //        task.CrossoverRate = randomNumber * 0.00001;

        //        randomNumber = random.Next(1000, 20000 + 1);
        //        task.MutationRate = randomNumber * 0.00001;

        //        randomNumber = random.Next(1000, 20000 + 1);
        //        task.ElitismRate = randomNumber * 0.00001;

        //        randomNumber = random.Next(1000, 20000 + 1);
        //        task.ImmigrationRate = randomNumber * 0.00001;

        //        if (task.RetirementStrategy == RetirementStrategy.MaxAge)
        //        {
        //            randomNumber = random.Next(40, 60 + 1);
        //            task.MaxRetirement = randomNumber;
        //        }

        //        if (task.RetirementStrategy == RetirementStrategy.MaxChildren)
        //        {
        //            randomNumber = random.Next(200, 400 + 1);
        //            task.MaxRetirement = randomNumber;
        //        }

        //        randomNumber = random.Next(1, 8 + 1);
        //        task.ChildrenPerParents = randomNumber;

        //        task.RandomSeed = random.Next();
        //        tasks.Add(task);
        //    }

        //    InsertTasksToDatabase(tasks, 19);
        //}

        //[TestMethod]
        //public void ItCanInsertOrderedFullRandoms()
        //{
        //    var tasks = new List<GATask>();

        //    var random = new Random();
        //    for (int i = 0; i < 605601; i++)
        //    {
        //        var task = new GATask(new MLBDriveTimeSolution())
        //        {
        //            ParentSelectionStrategy = ParentSelectionStrategy.StochasticUniversalSamplingSelection,
        //            MutationStrategy = MutationStrategy.Swap,
        //            CrossoverStrategy = CrossoverStrategy.Order,
        //            RetirementStrategy = RetirementStrategy.MaxAge,
        //            ImmigrationStrategy = ImmigrationStrategy.Dynamic,
        //            ScoringStrategy = ScoringStrategy.Lowest,
        //            DuplicationStrategy = DuplicationStrategy.Prevent,

        //            MaxGenerations = 1000,
        //            RandomPoolGenerationSeed = 22,

        //            Session = "0020"
        //        };

        //        var randomNumber = random.Next(1, 7 + 1);
        //        if (randomNumber == 2) task.ParentSelectionStrategy = ParentSelectionStrategy.RouletteWheel;
        //        if (randomNumber == 3) task.ParentSelectionStrategy = ParentSelectionStrategy.Rank;
        //        if (randomNumber == 4) task.ParentSelectionStrategy = ParentSelectionStrategy.TournamentTwo;
        //        if (randomNumber == 5) task.ParentSelectionStrategy = ParentSelectionStrategy.TournamentThree;
        //        if (randomNumber == 6) task.ParentSelectionStrategy = ParentSelectionStrategy.TournamentFour;
        //        if (randomNumber == 7) task.ParentSelectionStrategy = ParentSelectionStrategy.TournamentFive;

        //        randomNumber = random.Next(1, 4 + 1);
        //        if (randomNumber == 2) { task.MutationStrategy = MutationStrategy.Scramble; }
        //        if (randomNumber == 3) { task.MutationStrategy = MutationStrategy.Insert; }
        //        if (randomNumber == 4) { task.MutationStrategy = MutationStrategy.Inversion; }

        //        randomNumber = random.Next(1, 4 + 1);
        //        if (randomNumber == 2) { task.CrossoverStrategy = CrossoverStrategy.Cycle; }
        //        if (randomNumber == 3) { task.CrossoverStrategy = CrossoverStrategy.AlternatingPosition; }
        //        if (randomNumber == 4) { task.CrossoverStrategy = CrossoverStrategy.PartialMapped; }

        //        randomNumber = random.Next(1, 3 + 1);
        //        if (randomNumber == 2) { task.ImmigrationStrategy = ImmigrationStrategy.Constant; }
        //        if (randomNumber == 3) { task.ImmigrationStrategy = ImmigrationStrategy.None; }

        //        randomNumber = random.Next(1, 3 + 1);
        //        if (randomNumber == 2) { task.RetirementStrategy = RetirementStrategy.None; }
        //        if (randomNumber == 3) { task.RetirementStrategy = RetirementStrategy.MaxChildren; }

        //        randomNumber = random.Next(1, 2 + 1);
        //        if (randomNumber == 2) { task.DuplicationStrategy = DuplicationStrategy.Allow; }

        //        task.PopulationSize = 50;

        //        randomNumber = random.Next(60000, 99000 + 1);
        //        task.CrossoverRate = randomNumber * 0.00001;

        //        randomNumber = random.Next(1000, 20000 + 1);
        //        task.MutationRate = randomNumber * 0.00001;

        //        randomNumber = random.Next(1000, 20000 + 1);
        //        task.ElitismRate = randomNumber * 0.00001;

        //        randomNumber = random.Next(1000, 10000 + 1);
        //        task.ImmigrationRate = randomNumber * 0.00001;

        //        randomNumber = random.Next((task.MaxGenerations / 8), (task.MaxGenerations / 2) + 1);
        //        task.MaxRetirement = randomNumber;

        //        randomNumber = random.Next(1, 8 + 1);
        //        task.ChildrenPerParents = randomNumber;

        //        task.RandomSeed = random.Next();
        //        tasks.Add(task);
        //    }

        //    InsertTasksToDatabase(tasks, 20);
        //}

        //[TestMethod]
        //public void ItCanInsertMLBRanges()
        //{
        //    var tasks = new List<GATask>();
        //    var random = new Random();

        //    //781F1A52-A44B-4C12-9848-60F3647BA8D4
        //    for (var k = 1; k <= 2500; k++)
        //    {
        //        for (int i = 0; i < 3; i++)
        //        {
        //            var task = new GATask(new MLBDriveTimeSolution())
        //            {
        //                ParentSelectionStrategy = ParentSelectionStrategy.TournamentFive,
        //                MutationStrategy = MutationStrategy.Insert,
        //                CrossoverStrategy = CrossoverStrategy.Order,
        //                ImmigrationStrategy = ImmigrationStrategy.None,
        //                RetirementStrategy = RetirementStrategy.MaxChildren,
        //                ScoringStrategy = ScoringStrategy.Lowest,
        //                DuplicationStrategy = DuplicationStrategy.Prevent,

        //                PopulationSize = k,
        //                MaxGenerations = 1000,
        //                CrossoverRate = 0.887420,
        //                MutationRate = 0.021370,
        //                ElitismRate = 0.061640,
        //                ImmigrationRate = 0.086450,
        //                MaxRetirement = 170,
        //                ChildrenPerParents = 3,
        //                RandomSeed = 19554395,
        //                RandomPoolGenerationSeed = 22,

        //                Session = "0025"
        //            };

        //            task.RandomSeed = random.Next();
        //            tasks.Add(task);
        //        }
        //    }

        //    InsertTasksToDatabase(tasks, 25);
        //}

        //[TestMethod]
        //public void ItCanInsertMLBTests()
        //{
        //    var tasks = new List<GATask>();
        //    var random = new Random();

        //    //781F1A52-A44B-4C12-9848-60F3647BA8D4
        //    var iterations = 3;
        //    double startingRate = 3;
        //    double rateAdjustment = 1;
        //    double rateChange = 1;

        //    double rateStart = startingRate - rateAdjustment;
        //    double rateEnd = startingRate + rateAdjustment;
        //    for (var i = rateStart; i < rateEnd; i += rateChange)
        //    {
        //        for (var k = 0; k < iterations; k++)
        //        {
        //            var task = new GATask(new MLBDriveTimeSolution())
        //            {
        //                ParentSelectionStrategy = ParentSelectionStrategy.TournamentFive,
        //                MutationStrategy = MutationStrategy.Insert,
        //                CrossoverStrategy = CrossoverStrategy.Order,
        //                ImmigrationStrategy = ImmigrationStrategy.None,
        //                RetirementStrategy = RetirementStrategy.MaxChildren,
        //                ScoringStrategy = ScoringStrategy.Lowest,
        //                DuplicationStrategy = DuplicationStrategy.Prevent,

        //                PopulationSize = 50,
        //                MaxGenerations = 1000,
        //                CrossoverRate = 0.887420,
        //                MutationRate = 0.021370,
        //                ElitismRate = 0.061640,
        //                ImmigrationRate = 0.086450,
        //                MaxRetirement = 170,
        //                ChildrenPerParents = 3,
        //                RandomSeed = 19554395,
        //                RandomPoolGenerationSeed = 22,

        //                Session = "0024"
        //            };

        //            task.RandomSeed = random.Next();
        //            tasks.Add(task);
        //        }                
        //    }

        //    InsertTasksToDatabase(tasks, 24);
        //}

        //private void InsertTasksToDatabase(List<GATask> tasks, double priority)
        //{
        //    var dao = new JarrusDAO();

        //    Parallel.ForEach(tasks, new ParallelOptions { MaxDegreeOfParallelism = 8 },task =>
        //    {
        //        dao.InsertTask(task, priority);
        //    });
        //}
    }
}
