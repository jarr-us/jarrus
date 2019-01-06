using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GeneticAlgorithms.BasicTypes;
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
        //[TestMethod]
        //public void ItCanInsertAnOfficeSession5Task()
        //{
        //    var tasks = new List<GATask>();
            
        //    var random = new Random();
        //    for (int i = 0; i < 15000; i++)
        //    {
        //        var task = new GATask(new OfficeSurveySolution())
        //        {
        //            ParentSelectionType = ParentSelectionType.StochasticUniversalSamplingSelection,
        //            MutationType = MutationType.Inversion,
        //            CrossoverType = CrossoverType.Order,

        //            MaxPopulationSize = 50,
        //            CrossoverRate = 0.96,
        //            MutationRate = 0.14,
        //            ElitismRate = 0.25,
        //            MaximumLifeSpan = 637,
        //            ChildrenPerCouple = 5,

        //            MaxGenerations = 10000,
        //            LowestScoreIsBest = false,
        //            PreventDuplications = true,                   
        //            RandomPoolGenerationSeed = 22,

        //            Session = "0005"
        //        };

        //        var randomNumber = random.Next(1, 2 + 1);
        //        if (randomNumber == 2) task.ParentSelectionType = ParentSelectionType.RouletteWheel;

        //        randomNumber = random.Next(1, 4 + 1);
        //        if (randomNumber == 2) { task.MutationType = MutationType.Insert; }
        //        if (randomNumber == 3) { task.MutationType = MutationType.Scramble; }
        //        if (randomNumber == 4) { task.MutationType = MutationType.Swap; }

        //        randomNumber = random.Next(1, 4 + 1);
        //        if (randomNumber == 2) { task.CrossoverType = CrossoverType.AlternatingPosition; }
        //        if (randomNumber == 3) { task.CrossoverType = CrossoverType.Cycle; }
        //        if (randomNumber == 4) { task.CrossoverType = CrossoverType.PartialMapped; }

        //        randomNumber = random.Next(50, 150 + 1);
        //        task.MaxPopulationSize = randomNumber;

        //        randomNumber = random.Next(60000, 99000 + 1);
        //        task.CrossoverRate = randomNumber * 0.00001;

        //        randomNumber = random.Next(1000, 25000 + 1);
        //        task.MutationRate = randomNumber * 0.00001;

        //        randomNumber = random.Next(1000, 25000 + 1);
        //        task.ElitismRate = randomNumber * 0.00001;

        //        randomNumber = random.Next(500, 1500 + 1);
        //        task.MaximumLifeSpan = randomNumber;

        //        randomNumber = random.Next(1, 8 + 1);
        //        task.ChildrenPerCouple = randomNumber;

        //        task.RandomSeed = random.Next();
        //        tasks.Add(task);
        //    }

        //   // InsertTasksToDatabase(tasks, 5);
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
