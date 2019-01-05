using GeneticAlgorithms;
using GeneticAlgorithmTests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GeneticAlgorithmTests.Utility
{
    [TestClass]
    public class ReflectionTests
    {
        [TestMethod]
        public void ItCopiesByValue()
        {
            var task = GATestHelper.GetDefaultConfiguration();
            var config = new GAConfiguration(task);

            var value = GATestHelper.GetRandomInteger(1, int.MaxValue - 1);
            task.MutationRate = value;
            Assert.AreNotEqual(value, config.MutationRate);
        }

        [TestMethod]
        public void ItCanCopyProperties()
        {
            var randomValue = GATestHelper.GetNextDouble();

            var task = GATestHelper.GetDefaultConfiguration();
            task.MutationRate = randomValue;
            var config = new GAConfiguration(task);
                        
            Assert.AreEqual(randomValue, config.MutationRate);
        }

        [TestMethod]
        public void ItCanCopyObjects()
        {
            var task = GATestHelper.GetDefaultConfiguration();
            var config = new GAConfiguration(task);

            Assert.AreEqual(task.Mutation, config.Mutation);
            Assert.AreEqual(task.Crossover, config.Crossover);
            Assert.AreEqual(task.FitnessFunction, config.FitnessFunction);
            Assert.AreEqual(task.ParentSelection, config.ParentSelection);
        }
    }
}
