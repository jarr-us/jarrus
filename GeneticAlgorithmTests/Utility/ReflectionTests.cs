﻿using Jarrus.GA.Models;
using Jarrus.GATests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jarrus.GATests.Utility
{
    [TestClass]
    public class ReflectionTests
    {
        [TestMethod]
        public void ItCopiesByValue()
        {
            var task = GATestHelper.GetDummyTravelingSalesmanTask();
            var config = new GAConfiguration(task);

            var value = GATestHelper.GetRandomInteger(1, int.MaxValue - 1);
            task.MutationRate = value;
            Assert.AreNotEqual(value, config.MutationRate);
        }

        [TestMethod]
        public void ItCanCopyProperties()
        {
            var randomValue = GATestHelper.GetNextDouble();

            var task = GATestHelper.GetDummyTravelingSalesmanTask();
            task.MutationRate = randomValue;
            var config = new GAConfiguration(task);
                        
            Assert.AreEqual(randomValue, config.MutationRate);
        }

        [TestMethod]
        public void ItCanCopyObjects()
        {
            var task = GATestHelper.GetDummyTravelingSalesmanTask();
            var config = new GAConfiguration(task);

            Assert.AreEqual(task.MutationStrategy, config.MutationStrategy);
            Assert.AreEqual(task.CrossoverStrategy, config.CrossoverStrategy);
            Assert.AreEqual(task.ParentSelectionStrategy, config.ParentSelectionStrategy);
        }
    }
}
