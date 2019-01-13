using System;
using Jarrus.GA;
using Jarrus.GA.Models;
using Jarrus.GA.Crossovers;
using Jarrus.GA.Crossovers.Unordered;
using Jarrus.GA.Factory.Enums;
using Jarrus.GA.Mutations;
using Jarrus.GA.ParentSelections;
using Jarrus.GATests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jarrus.GATests
{
    [TestClass]
    public class GAConfigurationTests
    {
        [TestMethod]
        public void ItHasAValidConstructor()
        {
            var task = GATestHelper.GetDummyTravelingSalesmanTask();
            var settings = new GAConfiguration(task);
        }

        [TestMethod]
        public void ItAllowsAValidCrossoverType()
        {
            var task = GATestHelper.GetDummyTravelingSalesmanTask();
            task.CrossoverStrategy = CrossoverStrategy.AlternatingPosition;
            var settings = new GAConfiguration(task);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItDoesNotAllowsAnInvalidCrossoverType()
        {
            var task = GATestHelper.GetDummyTravelingSalesmanTask();
            task.CrossoverStrategy = 0;
            var settings = new GAConfiguration(task);
        }

        [TestMethod]
        public void ItAllowsAValidImmigrationType()
        {
            var task = GATestHelper.GetDummyTravelingSalesmanTask();
            task.ImmigrationStrategy = ImmigrationStrategy.Constant;
            var settings = new GAConfiguration(task);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItDoesNotAllowsAnInvalidImmigrationType()
        {
            var task = GATestHelper.GetDummyTravelingSalesmanTask();
            task.ImmigrationStrategy = 0;
            var settings = new GAConfiguration(task);
        }

        [TestMethod]
        public void ItAllowsAValidMutationType()
        {
            var task = GATestHelper.GetDummyTravelingSalesmanTask();
            task.MutationStrategy = MutationStrategy.Boundary;
            var settings = new GAConfiguration(task);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItDoesNotAllowsAnInvalidMutationType()
        {
            var task = GATestHelper.GetDummyTravelingSalesmanTask();
            task.MutationStrategy = 0;
            var settings = new GAConfiguration(task);
        }

        [TestMethod]
        public void ItAllowsAValidParentSelectionType()
        {
            var task = GATestHelper.GetDummyTravelingSalesmanTask();
            task.ParentSelectionStrategy = ParentSelectionStrategy.RouletteWheel;
            var settings = new GAConfiguration(task);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItDoesNotAllowsAnInvalidParentSelectionType()
        {
            var task = GATestHelper.GetDummyTravelingSalesmanTask();
            task.ParentSelectionStrategy = 0;
            var settings = new GAConfiguration(task);
        }

        [TestMethod]
        public void ItAllowsAValidRetirementType()
        {
            var task = GATestHelper.GetDummyTravelingSalesmanTask();
            task.RetirementStrategy = RetirementStrategy.MaxChildren;
            var settings = new GAConfiguration(task);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItDoesNotAllowsAnInvalidRetirementType()
        {
            var task = GATestHelper.GetDummyTravelingSalesmanTask();
            task.RetirementStrategy = 0;
            var settings = new GAConfiguration(task);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItThrowsAnExceptionIfNoTaskIsPassed()
        {
            var settings = new GAConfiguration(null);
        }

        [TestMethod]
        public void ItAllowsRetirementTypeMaxAgeWhenRetirementMaximumIsAboveOne()
        {
            var task = GATestHelper.GetDummyTravelingSalesmanTask();
            task.RetirementStrategy = RetirementStrategy.MaxAge;
            task.MaxRetirement = 2;
            var settings = new GAConfiguration(task);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItThrowsAnExceptionIfRetirementTypeMaxAgeWhenRetirementMaximumIsAtZero()
        {
            var task = GATestHelper.GetDummyTravelingSalesmanTask();
            task.RetirementStrategy = RetirementStrategy.MaxAge;
            task.MaxRetirement = 0;
            var settings = new GAConfiguration(task);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItThrowsAnExceptionIfRetirementTypeMaxAgeWhenRetirementMaximumIsBelowZero()
        {
            var task = GATestHelper.GetDummyTravelingSalesmanTask();
            task.RetirementStrategy = RetirementStrategy.MaxAge;
            task.MaxRetirement = 0;
            var settings = new GAConfiguration(task);
        }

        [TestMethod]
        public void ItAllowsRetirementTypeMaxChildrenWhenRetirementMaximumIsAboveOne()
        {
            var task = GATestHelper.GetDummyTravelingSalesmanTask();
            task.RetirementStrategy = RetirementStrategy.MaxChildren;
            task.MaxRetirement = 2;
            var settings = new GAConfiguration(task);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItThrowsAnExceptionIfRetirementTypeMaxChildrenWhenRetirementMaximumIsAtZero()
        {
            var task = GATestHelper.GetDummyTravelingSalesmanTask();
            task.RetirementStrategy = RetirementStrategy.MaxChildren;
            task.MaxRetirement = 0;
            var settings = new GAConfiguration(task);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItThrowsAnExceptionIfRetirementTypeMaxChildrenWhenRetirementMaximumIsBelowZero()
        {
            var task = GATestHelper.GetDummyTravelingSalesmanTask();
            task.RetirementStrategy = RetirementStrategy.MaxChildren;
            task.MaxRetirement = 0;
            var settings = new GAConfiguration(task);
        }

        [TestMethod]
        public void ItAllowsRetirementTypeNoneWhenRetirementMaximumIsAboveZero()
        {
            var task = GATestHelper.GetDummyTravelingSalesmanTask();
            task.RetirementStrategy = RetirementStrategy.None;
            task.MaxRetirement = 1;
            var settings = new GAConfiguration(task);
        }

        [TestMethod]
        public void ItAllowsRetirementTypeNoneWhenRetirementMaximumIsAtZero()
        {
            var task = GATestHelper.GetDummyTravelingSalesmanTask();
            task.RetirementStrategy = RetirementStrategy.None;
            task.MaxRetirement = 1;
            var settings = new GAConfiguration(task);
        }

        [TestMethod]
        public void ItAllowsRetirementTypeNoneWhenRetirementMaximumIsBelowZero()
        {
            var task = GATestHelper.GetDummyTravelingSalesmanTask();
            task.RetirementStrategy = RetirementStrategy.None;
            task.MaxRetirement = 1;
            var settings = new GAConfiguration(task);
        }

        [TestMethod]
        public void ItsConstructorsCanSetTheStrategies()
        {
            var task = GATestHelper.GetDummyTravelingSalesmanTask();
            var settings = new GAConfiguration(task);

            Assert.IsTrue(settings.IsValid());
        }

        [TestMethod]
        public void ItsConstructorsCanSetTheStrategiesWhenUnordered()
        {
            var task = GATestHelper.GetDummyTravelingSalesmanTask();
            task.MutationStrategy = MutationStrategy.Random;

            var settings = new GAConfiguration(task);

            Assert.IsTrue(settings.IsValid());
        }

        [TestMethod]
        public void ItCanGetARandomInteger()
        {
            var min = 1;
            var max = 10;

            var config = GATestHelper.GetTravelingSalesmanDefaultConfiguration();

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

            var config = GATestHelper.GetTravelingSalesmanDefaultConfiguration();

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
            
            var config = GATestHelper.GetTravelingSalesmanDefaultConfiguration();

            for (int i = 0; i < 100; i++)
            {
                var num = config.GetNextDouble();
                Assert.IsTrue(min <= num);
                Assert.IsTrue(num <= max);
            }
        }

        [TestMethod]
        public void ItContainsAValidValueForImmigrationType()
        {
            var config = GATestHelper.GetTravelingSalesmanDefaultConfiguration();
            Assert.AreEqual(ImmigrationStrategy.Dynamic, config.ImmigrationStrategy);
        }

        [TestMethod]
        public void ItContainsAValidValueForRetirementType()
        {
            var config = GATestHelper.GetTravelingSalesmanDefaultConfiguration();
            Assert.AreEqual(RetirementStrategy.MaxAge, config.RetirementStrategy);
        }

        [TestMethod]
        public void ItCanGetARandomBoolean()
        {
            var truesSeen = 0;
            var config = GATestHelper.GetTravelingSalesmanDefaultConfiguration();

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
            var config = GATestHelper.GetTravelingSalesmanDefaultConfiguration();

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
            var config = GATestHelper.GetTravelingSalesmanDefaultConfiguration();
            config.MutationRate = 1.01;
            config.ValidateProperties();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItThrowsAnExceptionIfMutationRateIsBelowZero()
        {
            var config = GATestHelper.GetTravelingSalesmanDefaultConfiguration();
            config.MutationRate = -0.01;
            config.ValidateProperties();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItThrowsAnExceptionIfCrossoverRateIsAboveOne()
        {
            var config = GATestHelper.GetTravelingSalesmanDefaultConfiguration();
            config.CrossoverRate = 1.01;
            config.ValidateProperties();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItThrowsAnExceptionIfCrossoverRateIsBelowZero()
        {
            var config = GATestHelper.GetTravelingSalesmanDefaultConfiguration();
            config.CrossoverRate = -0.01;
            config.ValidateProperties();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItThrowsAnExceptionIfElitismRateIsAboveOne()
        {
            var config = GATestHelper.GetTravelingSalesmanDefaultConfiguration();
            config.ElitismRate = 1.01;
            config.ValidateProperties();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItThrowsAnExceptionIfElitismRateIsBelowZero()
        {
            var config = GATestHelper.GetTravelingSalesmanDefaultConfiguration();
            config.ElitismRate = -0.01;
            config.ValidateProperties();
        }
    }
}
