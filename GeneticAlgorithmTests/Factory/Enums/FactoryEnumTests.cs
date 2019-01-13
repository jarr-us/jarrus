using System;
using Jarrus.GA.Factory.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jarrus.GATests.Factory.Enums
{
    [TestClass]
    public class FactoryEnumTests
    {
        [TestMethod]
        public void ItDoesNotContainAValueForZeroForCrossoverTypes()
        {
            foreach (CrossoverStrategy type in Enum.GetValues(typeof(CrossoverStrategy)))
            {
                var value = (int)type;
                Assert.AreNotEqual(0, value);
            }
        }

        [TestMethod]
        public void ItDoesNotContainANegativeNumberValueForZeroForCrossoverTypes()
        {
            foreach (CrossoverStrategy type in Enum.GetValues(typeof(CrossoverStrategy)))
            {
                var value = (int)type;
                Assert.IsTrue(value >= 1);
            }
        }

        [TestMethod]
        public void ItDoesNotContainAValueForZeroForMutationTypes()
        {
            foreach (MutationStrategy type in Enum.GetValues(typeof(MutationStrategy)))
            {
                var value = (int)type;
                Assert.AreNotEqual(0, value);
            }
        }

        [TestMethod]
        public void ItDoesNotContainANegativeNumberValueForZeroForMutationTypes()
        {
            foreach (MutationStrategy type in Enum.GetValues(typeof(MutationStrategy)))
            {
                var value = (int)type;
                Assert.IsTrue(value >= 1);
            }
        }

        [TestMethod]
        public void ItDoesNotContainAValueForZeroForParentSelectionTypes()
        {
            foreach (ParentSelectionStrategy type in Enum.GetValues(typeof(ParentSelectionStrategy)))
            {
                var value = (int)type;
                Assert.AreNotEqual(0, value);
            }
        }

        [TestMethod]
        public void ItDoesNotContainANegativeNumberValueForZeroForParentSelectionTypes()
        {
            foreach (ParentSelectionStrategy type in Enum.GetValues(typeof(ParentSelectionStrategy)))
            {
                var value = (int)type;
                Assert.IsTrue(value >= 1);
            }
        }
    }
}
