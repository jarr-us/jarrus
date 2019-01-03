using System;
using Baseball.FitnessCalculators;
using Baseball.Models;
using GeneticAlgorithms.BasicTypes;
using GeneticAlgorithms.Crossovers.Ordered;
using GeneticAlgorithms.Data;
using GeneticAlgorithms.FitnessFunctions;
using GeneticAlgorithms.Mutations;
using GeneticAlgorithms.ParentSelections;
using GeneticAlgorithmTests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GeneticAlgorithmTests.Data
{
    [TestClass]
    public class JarrusDAOTests
    {
        //[TestMethod]
        //public void ItCanInsertASession2Task()
        //{
        //    var dao = new JarrusDAO();
        //    var task = GetExampleTask();

        //    task.ParentSelection = new StochasticUniversalSamplingSelection<Team>();
        //    task.Mutation = new InversionMutation();
        //    task.Crossover = new OrderCrossover();

        //    task.LowestScoreIsBest = true;
        //    task.MaxGenerations = 10000;
        //    task.CrossoverRate = 0.93;
        //    task.MutationRate = 0.08;
        //    task.ElitismRate = 0.30;
        //    task.PreventDuplications = true;
        //    task.MaximumLifeSpan = 416;
        //    task.ChildrenPerCouple = 4;
        //    task.RandomPoolGenerationSeed = 22;
        //    task.RandomSeed = 808816214;

        //    task.Session = "0002";
        //    var random = new Random();
        //    for (int i = 0; i < 10000; i++)
        //    {
        //        dao.InsertTask(task, 150.0);
        //        task.RandomSeed = random.Next();
        //    }
        //}

        [TestMethod]
        public void ItCanInsertASession4Task()
        {
            //TODO: end when a viable fitness score is reached
            //TODO: show the session on the app
            //TODO: get priority working
            //TODO: tons of tests. 
            //TODO: blog data dumps
            //TODO: more tests. seriously. what the fuck.
            //TODO: remove all logic from the App, test the shit out of it.
            //TODO: can <T> be made generically?
            //TODO: Threading now available again
            //TODO: API


            var dao = new JarrusDAO();
            var task = GetExampleTask();

            task.ParentSelection = new StochasticUniversalSamplingSelection<Team>();
            task.Mutation = new InversionMutation();
            task.Crossover = new OrderCrossover();

            task.LowestScoreIsBest = true;
            task.MaxGenerations = 10000;
            task.CrossoverRate = 0.92;
            task.MutationRate = 0.01;
            task.ElitismRate = 0.25;
            task.PreventDuplications = true;
            task.MaximumLifeSpan = 767;
            task.ChildrenPerCouple = 8;
            task.RandomPoolGenerationSeed = 22;
            task.RandomSeed = 556411793;

            task.Session = "0004";
            var random = new Random();
            for (int i = 0; i < 10000; i++)
            {
                dao.InsertTask(task, 160.0);
                task.RandomSeed = random.Next();
            }
        }

        //[TestMethod]
        //public void ItCanInsertASession3Task()
        //{
        //    var dao = new JarrusDAO();
        //    var task = GetExampleTask();

        //    task.ParentSelection = new StochasticUniversalSamplingSelection<Team>();
        //    task.Mutation = new InversionMutation();
        //    task.Crossover = new OrderCrossover();

        //    task.LowestScoreIsBest = true;
        //    task.MaxGenerations = 10000;
        //    task.CrossoverRate = 0.96;
        //    task.MutationRate = 0.14;
        //    task.ElitismRate = 0.25;
        //    task.PreventDuplications = true;
        //    task.MaximumLifeSpan = 637;
        //    task.ChildrenPerCouple = 5;
        //    task.RandomPoolGenerationSeed = 22;
        //    task.RandomSeed = 604037347;

        //    task.Session = "0003";
        //    var random = new Random();
        //    for (int i = 0; i < 10000; i++)
        //    {
        //        dao.InsertTask(task, 150.0);
        //        task.RandomSeed = random.Next();
        //    }
        //}

        [TestMethod]
        public void ItCanRetrieveATask()
        {
            var dao = new JarrusDAO();
            var task = dao.FetchMyFirstTask<ExampleGene>();

            Assert.IsNotNull(task);
        }

        //[TestMethod]
        //public void ItCanDoTheWholeCycle()
        //{
        //    var dao = new JarrusDAO();
        //    var task = dao.CheckoutATaskToRun<Team>();
        //    if (task.ParentSelection == null) { return; }

        //    var config = new GAConfiguration<Team>(task);

        //    var teamdao = new TeamDAO();
        //    var data = teamdao.FetchAllGenes().ToArray();

        //    var ga = new GeneticAlgorithm<Team>(config, data);
        //    var runDetails = ga.Run();

        //    dao.InsertCompletedRun(config, runDetails);
        //    dao.DeleteTask(task.UUID);
        //}

        [TestMethod]
        public void ItCanGetAnObjectByItsName()
        {
            var className = "GeneticAlgorithmTests.Models.FitnessFunctions.TravelingSalesmanFitnessCalculator";
            var elementType = Type.GetType(className);
            var fitnessFunction = (FitnessFunction)(Activator.CreateInstance(elementType));

            Assert.IsNotNull(fitnessFunction);
        }

        private GATask<Team> GetExampleTask()
        {
            var task = GATestHelper.GetDummyTask<Team>();
            task.FitnessFunction = new TeamTimeDrivingFitnessFunction();
            return task;
        }
    }
}
