using System;
using Jarrus.GA;
using Jarrus.GA.BasicTypes;
using Jarrus.GA.Crossovers;
using Jarrus.GA.Crossovers.Unordered;
using Jarrus.GA.Factory.Enums;
using Jarrus.GA.Mutations;
using Jarrus.GA.ParentSelections;
using Jarrus.GATests.Models;
using Jarrus.GATests.Models.FitnessFunctions;
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
            task.CrossoverType = CrossoverType.AlternatingPosition;
            var settings = new GAConfiguration(task);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItDoesNotAllowsAnInvalidCrossoverType()
        {
            var task = GATestHelper.GetDummyTravelingSalesmanTask();
            task.CrossoverType = 0;
            var settings = new GAConfiguration(task);
        }

        [TestMethod]
        public void ItAllowsAValidImmigrationType()
        {
            var task = GATestHelper.GetDummyTravelingSalesmanTask();
            task.ImmigrationType = ImmigrationType.Constant;
            var settings = new GAConfiguration(task);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItDoesNotAllowsAnInvalidImmigrationType()
        {
            var task = GATestHelper.GetDummyTravelingSalesmanTask();
            task.ImmigrationType = 0;
            var settings = new GAConfiguration(task);
        }

        [TestMethod]
        public void ItAllowsAValidMutationType()
        {
            var task = GATestHelper.GetDummyTravelingSalesmanTask();
            task.MutationType = MutationType.Boundary;
            var settings = new GAConfiguration(task);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItDoesNotAllowsAnInvalidMutationType()
        {
            var task = GATestHelper.GetDummyTravelingSalesmanTask();
            task.MutationType = 0;
            var settings = new GAConfiguration(task);
        }

        [TestMethod]
        public void ItAllowsAValidParentSelectionType()
        {
            var task = GATestHelper.GetDummyTravelingSalesmanTask();
            task.ParentSelectionType = ParentSelectionType.RouletteWheel;
            var settings = new GAConfiguration(task);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItDoesNotAllowsAnInvalidParentSelectionType()
        {
            var task = GATestHelper.GetDummyTravelingSalesmanTask();
            task.ParentSelectionType = 0;
            var settings = new GAConfiguration(task);
        }

        [TestMethod]
        public void ItAllowsAValidRetirementType()
        {
            var task = GATestHelper.GetDummyTravelingSalesmanTask();
            task.RetirementType = RetirementType.MaxChildren;
            var settings = new GAConfiguration(task);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItDoesNotAllowsAnInvalidRetirementType()
        {
            var task = GATestHelper.GetDummyTravelingSalesmanTask();
            task.RetirementType = 0;
            var settings = new GAConfiguration(task);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItThrowsAnExceptionIfNoTaskIsPassed()
        {
            var settings = new GAConfiguration(null);
        }

        [TestMethod]
        public void ItAllowsRetirementTypeMaxAgeWhenRetirementMaximumIsAboveZero()
        {
            var task = GATestHelper.GetDummyTravelingSalesmanTask();
            task.RetirementType = RetirementType.MaxAge;
            task.MaxRetirement = 1;
            var settings = new GAConfiguration(task);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItThrowsAnExceptionIfRetirementTypeMaxAgeWhenRetirementMaximumIsAtZero()
        {
            var task = GATestHelper.GetDummyTravelingSalesmanTask();
            task.RetirementType = RetirementType.MaxAge;
            task.MaxRetirement = 0;
            var settings = new GAConfiguration(task);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItThrowsAnExceptionIfRetirementTypeMaxAgeWhenRetirementMaximumIsBelowZero()
        {
            var task = GATestHelper.GetDummyTravelingSalesmanTask();
            task.RetirementType = RetirementType.MaxAge;
            task.MaxRetirement = 0;
            var settings = new GAConfiguration(task);
        }

        [TestMethod]
        public void ItAllowsRetirementTypeMaxChildrenWhenRetirementMaximumIsAboveZero()
        {
            var task = GATestHelper.GetDummyTravelingSalesmanTask();
            task.RetirementType = RetirementType.MaxChildren;
            task.MaxRetirement = 1;
            var settings = new GAConfiguration(task);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItThrowsAnExceptionIfRetirementTypeMaxChildrenWhenRetirementMaximumIsAtZero()
        {
            var task = GATestHelper.GetDummyTravelingSalesmanTask();
            task.RetirementType = RetirementType.MaxChildren;
            task.MaxRetirement = 0;
            var settings = new GAConfiguration(task);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItThrowsAnExceptionIfRetirementTypeMaxChildrenWhenRetirementMaximumIsBelowZero()
        {
            var task = GATestHelper.GetDummyTravelingSalesmanTask();
            task.RetirementType = RetirementType.MaxChildren;
            task.MaxRetirement = 0;
            var settings = new GAConfiguration(task);
        }

        [TestMethod]
        public void ItAllowsRetirementTypeNoneWhenRetirementMaximumIsAboveZero()
        {
            var task = GATestHelper.GetDummyTravelingSalesmanTask();
            task.RetirementType = RetirementType.None;
            task.MaxRetirement = 1;
            var settings = new GAConfiguration(task);
        }

        [TestMethod]
        public void ItAllowsRetirementTypeNoneWhenRetirementMaximumIsAtZero()
        {
            var task = GATestHelper.GetDummyTravelingSalesmanTask();
            task.RetirementType = RetirementType.None;
            task.MaxRetirement = 1;
            var settings = new GAConfiguration(task);
        }

        [TestMethod]
        public void ItAllowsRetirementTypeNoneWhenRetirementMaximumIsBelowZero()
        {
            var task = GATestHelper.GetDummyTravelingSalesmanTask();
            task.RetirementType = RetirementType.None;
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
            task.MutationType = MutationType.Random;

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
            Assert.AreEqual(ImmigrationType.Dynamic, config.ImmigrationType);
        }

        [TestMethod]
        public void ItContainsAValidValueForRetirementType()
        {
            var config = GATestHelper.GetTravelingSalesmanDefaultConfiguration();
            Assert.AreEqual(RetirementType.MaxAge, config.RetirementType);
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
