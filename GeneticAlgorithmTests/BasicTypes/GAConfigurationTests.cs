using System;
using GeneticAlgorithms;
using GeneticAlgorithms.BasicTypes;
using GeneticAlgorithms.Crossovers;
using GeneticAlgorithms.Crossovers.Unordered;
using GeneticAlgorithms.Mutations;
using GeneticAlgorithms.ParentSelections;
using GeneticAlgorithmTests.Models;
using GeneticAlgorithmTests.Models.FitnessFunctions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GeneticAlgorithmTests
{
    [TestClass]
    public class GAConfigurationTests
    {
        [TestMethod]
        public void ItHasAValidConstructor()
        {
            var task = GetDummyTask();
            var settings = new GAConfiguration(task);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItThrowsAnExceptionIfSelectionIsNull()
        {
            var task = GetDummyTask();
            task.ParentSelection = null;
            new GAConfiguration(task);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItThrowsAnExceptionIfCalculatorIsNull()
        {
            var task = GetDummyTask();
            task.FitnessFunction = null;
            new GAConfiguration(task);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItThrowsAnExceptionIfCrossoverIsNull()
        {
            var task = GetDummyTask();
            task.Crossover = null;
            new GAConfiguration(task);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItThrowsAnExceptionIfMutationIsNull()
        {
            var task = GetDummyTask();
            task.Mutation = null;
            new GAConfiguration(task);
        }

        private GATask GetDummyTask()
        {
            var task = new GATask();

            task.ParentSelection = new RouletteWheelSelection();
            task.FitnessFunction = new TravelingSalesmanFitnessCalculator();
            task.Mutation = new SwapMutation();
            task.Crossover = new SinglePointCrossover();
            task.ChildrenPerCouple = 4;

            return task;
        }

        [TestMethod]
        public void ItCanGetARandomInteger()
        {
            var min = 1;
            var max = 10;

            var config = GATestHelper.GetDefaultConfiguration();

            for(int i = 0; i < 100; i++)
            {
                var num = config.GetRandomInteger(min, max);
                Assert.IsTrue(min <= num);
                Assert.IsTrue(num <= max);
            }
        }

        [TestMethod]
        public void ItCanGetARandomIntegerExcludingSomeNumbers()
        {
            var min = 1;
            var max = 10;

            var forbiddenNumberFour = 4;

            var config = GATestHelper.GetDefaultConfiguration();

            for (int i = 0; i < 100; i++)
            {
                var num = config.GetRandomInteger(min, max, forbiddenNumberFour);
                Assert.IsTrue(min <= num);
                Assert.IsTrue(num <= max);
                Assert.AreNotEqual(num, forbiddenNumberFour);
            }
        }

        [TestMethod]
        public void ItCanGetARandomNumberBetweenZeroAndOne()
        {
            var min = 0;
            var max = 1;
            
            var config = GATestHelper.GetDefaultConfiguration();

            for (int i = 0; i < 100; i++)
            {
                var num = config.GetNextDouble();
                Assert.IsTrue(min <= num);
                Assert.IsTrue(num <= max);
            }
        }

        [TestMethod]
        public void ItCanGetARandomBoolean()
        {
            var truesSeen = 0;
            var config = GATestHelper.GetDefaultConfiguration();

            for (int i = 0; i < 100; i++)
            {
                var tORf = config.GetRandomBoolean();
                if (tORf)
                {
                    truesSeen++;
                }
            }

            Assert.IsTrue(truesSeen >= 10);
        }

        [TestMethod]
        public void ItCanGetAWeightedRandomBoolean()
        {
            var chanceOfTrue = 1;
            var truesSeen = 0;
            var config = GATestHelper.GetDefaultConfiguration();

            for (int i = 0; i < 100; i++)
            {
                var tORf = config.GetRandomBoolean(chanceOfTrue);
                if (tORf)
                {
                    truesSeen++;
                }
            }

            Assert.IsTrue(truesSeen <= 10);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItThrowsAnExceptionIfMutationRateIsAboveOne()
        {
            var config = GATestHelper.GetDefaultConfiguration();
            config.MutationRate = 1.01;
            config.ValidateProperties();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItThrowsAnExceptionIfMutationRateIsBelowZero()
        {
            var config = GATestHelper.GetDefaultConfiguration();
            config.MutationRate = -0.01;
            config.ValidateProperties();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItThrowsAnExceptionIfCrossoverRateIsAboveOne()
        {
            var config = GATestHelper.GetDefaultConfiguration();
            config.CrossoverRate = 1.01;
            config.ValidateProperties();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItThrowsAnExceptionIfCrossoverRateIsBelowZero()
        {
            var config = GATestHelper.GetDefaultConfiguration();
            config.CrossoverRate = -0.01;
            config.ValidateProperties();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItThrowsAnExceptionIfElitismRateIsAboveOne()
        {
            var config = GATestHelper.GetDefaultConfiguration();
            config.ElitismRate = 1.01;
            config.ValidateProperties();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItThrowsAnExceptionIfElitismRateIsBelowZero()
        {
            var config = GATestHelper.GetDefaultConfiguration();
            config.ElitismRate = -0.01;
            config.ValidateProperties();
        }
    }
}
