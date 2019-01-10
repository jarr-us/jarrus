using System.Collections.Generic;
using System.Threading.Tasks;
using Jarrus.GA;
using Jarrus.GA.BasicTypes;
using Jarrus.GA.Factory.Enums;
using Jarrus.Data;
using Kanan.MLBDriveTime.Jarrus;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Kanan.OfficeSurveys;

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
            var solution = new MLBDriveTimeSolution();
            var task = new GATask(solution);
            task.ParentSelectionType = ParentSelectionType.StochasticUniversalSamplingSelection;
            task.MutationType = MutationType.Inversion;
            task.CrossoverType = CrossoverType.Order;
            task.ScoringType = ScoringType.Lowest;
            task.DuplicationType = DuplicationType.Prevent;

            task.PopulationSize = 10;
            task.MaxGenerations = 25;
            task.CrossoverRate = 0.93;
            task.MutationRate = 0.08;
            task.ElitismRate = 0.30;            
            task.MaxRetirement = 416;
            task.ChildrenPerParents = 4;
            task.RandomSeed = 808816214;
            task.RandomPoolGenerationSeed = 22;

            var config = new GAConfiguration(task);

            var ga = new OrderedGeneticAlgorithm(config, solution.GetOptions());
            var runDetails = ga.Run();

            return runDetails.BestChromosome.FitnessScore;
        }

        //[TestMethod]
        //public void ItCanInsertFullRandoms()
        //{
        //    var tasks = new List<GATask>();

        //    var random = new Random();
        //    for (int i = 0; i < 35000; i++)
        //    {
        //        var task = new GATask(new MLBDriveTimeSolution())
        //        {
        //            ParentSelectionType = ParentSelectionType.StochasticUniversalSamplingSelection,
        //            MutationType = MutationType.Swap,
        //            CrossoverType = CrossoverType.Cycle,
        //            RetirementType = RetirementType.MaxAge,
        //            ImmigrationType = ImmigrationType.Dynamic,
        //            ScoringType = ScoringType.Lowest,
        //            DuplicationType = DuplicationType.Prevent,
                    
        //            CrossoverRate = 0.89,
        //            MutationRate = 0.14,
        //            ElitismRate = 0.25,
        //            ImmigrationRate = 0.10,

        //            PopulationSize = 112,
        //            MaxRetirement = 1000,
        //            ChildrenPerParents = 5,                    

        //            MaxGenerations = 1000,
        //            RandomPoolGenerationSeed = 22,

        //            Session = "0017"
        //        };

        //        var randomNumber = random.Next(1, 2 + 1);
        //        if (randomNumber == 2) task.ParentSelectionType = ParentSelectionType.RouletteWheel;

        //        randomNumber = random.Next(1, 4 + 1);
        //        if (randomNumber == 2) { task.MutationType = MutationType.Insert; }
        //        if (randomNumber == 3) { task.MutationType = MutationType.Scramble; }
        //        if (randomNumber == 4) { task.MutationType = MutationType.Inversion; }

        //        randomNumber = random.Next(1, 4 + 1);
        //        if (randomNumber == 2) { task.CrossoverType = CrossoverType.AlternatingPosition; }
        //        if (randomNumber == 3) { task.CrossoverType = CrossoverType.Order; }
        //        if (randomNumber == 4) { task.CrossoverType = CrossoverType.PartialMapped; }

        //        randomNumber = random.Next(1, 3 + 1);
        //        if (randomNumber == 2) { task.ImmigrationType = ImmigrationType.Constant; }
        //        if (randomNumber == 3) { task.ImmigrationType = ImmigrationType.None; }

        //        randomNumber = random.Next(1, 3 + 1);
        //        if (randomNumber == 2) { task.RetirementType = RetirementType.None; }
        //        if (randomNumber == 3) { task.RetirementType = RetirementType.MaxChildren; }

        //        randomNumber = random.Next(1, 2 + 1);
        //        if (randomNumber == 2) { task.DuplicationType = DuplicationType.Allow; }

        //        randomNumber = random.Next(50, 150 + 1);
        //        task.PopulationSize = randomNumber;

        //        randomNumber = random.Next(88000, 98000 + 1);
        //        task.CrossoverRate = randomNumber * 0.00001;

        //        randomNumber = random.Next(1000, 15000 + 1);
        //        task.MutationRate = randomNumber * 0.00001;

        //        randomNumber = random.Next(5000, 25000 + 1);
        //        task.ElitismRate = randomNumber * 0.00001;

        //        randomNumber = random.Next(2000, 10000 + 1);
        //        task.ImmigrationRate = randomNumber * 0.00001;

        //        randomNumber = random.Next(50, (task.MaxGenerations / 4) + 1);
        //        task.MaxRetirement = randomNumber;

        //        randomNumber = random.Next(1, 8 + 1);
        //        task.ChildrenPerParents = randomNumber;

        //        task.RandomSeed = random.Next();
        //        tasks.Add(task);
        //    }

        //    InsertTasksToDatabase(tasks, 17);
        //}

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
