using System;
using GeneticAlgorithms.Factory.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GeneticAlgorithmTests.Factory.Enums
{
    [TestClass]
    public class FactoryEnumTests
    {
        [TestMethod]
        public void ItDoesNotContainAValueForZeroForCrossoverTypes()
        {
            foreach (CrossoverType type in Enum.GetValues(typeof(CrossoverType)))
            {
                var value = (int)type;
                Assert.AreNotEqual(0, value);
            }
        }

        [TestMethod]
        public void ItDoesNotContainANegativeNumberValueForZeroForCrossoverTypes()
        {
            foreach (CrossoverType type in Enum.GetValues(typeof(CrossoverType)))
            {
                var value = (int)type;
                Assert.IsTrue(value >= 1);
            }
        }

        [TestMethod]
        public void ItDoesNotContainAValueForZeroForMutationTypes()
        {
            foreach (MutationType type in Enum.GetValues(typeof(MutationType)))
            {
                var value = (int)type;
                Assert.AreNotEqual(0, value);
            }
        }

        [TestMethod]
        public void ItDoesNotContainANegativeNumberValueForZeroForMutationTypes()
        {
            foreach (MutationType type in Enum.GetValues(typeof(MutationType)))
            {
                var value = (int)type;
                Assert.IsTrue(value >= 1);
            }
        }

        [TestMethod]
        public void ItDoesNotContainAValueForZeroForParentSelectionTypes()
        {
            foreach (ParentSelectionType type in Enum.GetValues(typeof(ParentSelectionType)))
            {
                var value = (int)type;
                Assert.AreNotEqual(0, value);
            }
        }

        [TestMethod]
        public void ItDoesNotContainANegativeNumberValueForZeroForParentSelectionTypes()
        {
            foreach (ParentSelectionType type in Enum.GetValues(typeof(ParentSelectionType)))
            {
                var value = (int)type;
                Assert.IsTrue(value >= 1);
            }
        }
    }
}
