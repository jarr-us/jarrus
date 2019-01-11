using System;
using Jarrus.GA.Factory.Enums;
using Jarrus.GA.Models;
using Jarrus.GATests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jarrus.GATests.BasicTypes.Populations
{
    [TestClass]
    public class OrderedPopulationTests
    {
        [TestMethod]
        public void ItCanAddToTheNextGeneration()
        {
            var population = GetExamplePopulationForOrdered();
            var config = GetConfigurationForOrdered();
            config.DuplicationType = DuplicationType.Prevent;

            var orderedPopulation = new OrderedPopulation(config, population, population[0].Genes);
            Assert.IsNotNull(orderedPopulation);

            orderedPopulation.AddToNextGeneration(population[0]);
            Assert.AreEqual(1, orderedPopulation.NextGeneration.Count);
            Assert.AreEqual(1, orderedPopulation.OptionsInPool.Count);
        }

        [TestMethod]
        public void ItCanPreventDuplicates()
        {
            var population = GetExamplePopulationForOrdered();
            var config = GetConfigurationForOrdered();
            config.DuplicationType = DuplicationType.Prevent;

            var orderedPopulation = new OrderedPopulation(config, population, population[0].Genes);
            Assert.IsNotNull(orderedPopulation);

            orderedPopulation.AddToNextGeneration(population[0]);
            Assert.AreEqual(1, orderedPopulation.NextGeneration.Count);
            Assert.AreEqual(1, orderedPopulation.OptionsInPool.Count);

            orderedPopulation.AddToNextGeneration(population[0]);
            Assert.AreEqual(1, orderedPopulation.NextGeneration.Count);
            Assert.AreEqual(1, orderedPopulation.OptionsInPool.Count);
        }

        private GAConfiguration GetConfigurationForOrdered() { return GATestHelper.GetTravelingSalesmanDefaultConfiguration(); }
        private Chromosome[] GetExamplePopulationForOrdered() { return GATestHelper.GetTravelingSalesmanPopulation(GetConfigurationForOrdered()); }
    }
}
