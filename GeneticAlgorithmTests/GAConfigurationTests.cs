using System;
using GeneticAlgorithms;
using GeneticAlgorithms.Crossovers;
using GeneticAlgorithms.Mutations;
using GeneticAlgorithms.ParentSelections;
using GeneticAlgorithmTests.Models;
using GeneticAlgorithmTests.Models.FitnessCalculators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GeneticAlgorithmTests
{
    [TestClass]
    public class GAConfigurationTests
    {
        [TestMethod]
        public void ItHasAValidConstructor()
        {
            var settings = new GAConfiguration<char>(new RouletteWheelSelection<char>(), new TravelingSalesmanFitnessCalculator(), new SwapMutation(), new SinglePointCrossover());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItThrowsAnExceptionIfSelectionIsNull()
        {
            new GAConfiguration<char>(null, new TravelingSalesmanFitnessCalculator(), new SwapMutation(), new SinglePointCrossover());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItThrowsAnExceptionIfCalculatorIsNull()
        {
            new GAConfiguration<char>(new RouletteWheelSelection<char>(), null, new SwapMutation(), new SinglePointCrossover());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItThrowsAnExceptionIfCrossoverIsNull()
        {
            new GAConfiguration<char>(new RouletteWheelSelection<char>(), new TravelingSalesmanFitnessCalculator(), new SwapMutation(), null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItThrowsAnExceptionIfMutationIsNull()
        {
            new GAConfiguration<char>(new RouletteWheelSelection<char>(), new TravelingSalesmanFitnessCalculator(), null, new SinglePointCrossover());
        }

        [TestMethod]
        public void ItCanGetARandomInteger()
        {
            var min = 1;
            var max = 10;

            var config = GATestHelper.GetDefaultConfiguration<char>();

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

            var config = GATestHelper.GetDefaultConfiguration<char>();

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
            
            var config = GATestHelper.GetDefaultConfiguration<char>();

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
            var config = GATestHelper.GetDefaultConfiguration<char>();

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
            var config = GATestHelper.GetDefaultConfiguration<char>();

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
            var config = GATestHelper.GetDefaultConfiguration<char>();
            config.MutationRate = 1.01;
            config.ValidateProperties();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItThrowsAnExceptionIfMutationRateIsBelowZero()
        {
            var config = GATestHelper.GetDefaultConfiguration<char>();
            config.MutationRate = -0.01;
            config.ValidateProperties();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItThrowsAnExceptionIfCrossoverRateIsAboveOne()
        {
            var config = GATestHelper.GetDefaultConfiguration<char>();
            config.CrossoverRate = 1.01;
            config.ValidateProperties();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItThrowsAnExceptionIfCrossoverRateIsBelowZero()
        {
            var config = GATestHelper.GetDefaultConfiguration<char>();
            config.CrossoverRate = -0.01;
            config.ValidateProperties();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItThrowsAnExceptionIfElitismRateIsAboveOne()
        {
            var config = GATestHelper.GetDefaultConfiguration<char>();
            config.ElitismRate = 1.01;
            config.ValidateProperties();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItThrowsAnExceptionIfElitismRateIsBelowZero()
        {
            var config = GATestHelper.GetDefaultConfiguration<char>();
            config.ElitismRate = -0.01;
            config.ValidateProperties();
        }
    }
}
