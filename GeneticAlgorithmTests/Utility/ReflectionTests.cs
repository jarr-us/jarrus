using GeneticAlgorithms;
using GeneticAlgorithms.BasicTypes;
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
            var task = GATestHelper.GetDefaultConfiguration<ExampleGene>();
            var config = new GAConfiguration<ExampleGene>(task);

            task.MutationRate = GATestHelper.GetRandomInteger(1, int.MaxValue - 1);
            Assert.AreEqual(0, config.MutationRate);
        }

        [TestMethod]
        public void ItCanCopyProperties()
        {
            var randomValue = GATestHelper.GetNextDouble();

            var task = GATestHelper.GetDefaultConfiguration<ExampleGene>();
            task.MutationRate = randomValue;
            var config = new GAConfiguration<ExampleGene>(task);
                        
            Assert.AreEqual(randomValue, config.MutationRate);
        }

        [TestMethod]
        public void ItCanCopyObjects()
        {
            var task = GATestHelper.GetDefaultConfiguration<ExampleGene>();
            var config = new GAConfiguration<ExampleGene>(task);

            Assert.AreEqual(task.Mutation, config.Mutation);
            Assert.AreEqual(task.Crossover, config.Crossover);
            Assert.AreEqual(task.FitnessFunction, config.FitnessFunction);
            Assert.AreEqual(task.ParentSelection, config.ParentSelection);
        }
    }
}
