using System;
using GeneticAlgorithms.BasicTypes;
using GeneticAlgorithms.Data;
using GeneticAlgorithms.Factory.Enums;
using Kanan.MLBDriveTime.Jarrus;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GeneticAlgorithmTests.Data
{
    [TestClass]
    public class JarrusDAOTests
    {
        //[TestMethod]
        //public void ItCanInsertASession4Task()
        //{
        //    var dao = new JarrusDAO();
        //    var task = new GATask(new MLBDriveTimeSolution())
        //    {
        //        ParentSelectionType = ParentSelectionType.StochasticUniversalSamplingSelection,
        //        MutationType = MutationType.Inversion,
        //        CrossoverType = CrossoverType.Order,

        //        MaxPopulationSize = 50,
        //        LowestScoreIsBest = true,
        //        MaxGenerations = 10000,
        //        CrossoverRate = 0.92,
        //        MutationRate = 0.01,
        //        ElitismRate = 0.25,
        //        PreventDuplications = true,
        //        MaximumLifeSpan = 767,
        //        ChildrenPerCouple = 8,
        //        RandomPoolGenerationSeed = 22,
        //        RandomSeed = 556411793,

        //        Session = "0004"
        //    };

        //    var random = new Random();
        //    for (int i = 0; i < 111; i++)
        //    {
        //        dao.InsertTask(task, 160.0);
        //        task.RandomSeed = random.Next();
        //    }
        //}

        //[TestMethod]
        //public void ItCanRetrieveATask()
        //{
        //    var dao = new JarrusDAO();
        //    var task = dao.CheckoutATaskToRun();

        //    Assert.IsNotNull(task);
        //}
    }
}
