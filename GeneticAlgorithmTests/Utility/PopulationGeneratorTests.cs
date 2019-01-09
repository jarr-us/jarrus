using System;
using Jarrus.GA.Utility;
using Jarrus.GATests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jarrus.GATests.Utility
{
    [TestClass]
    public class PopulationGeneratorTests
    {
        [TestMethod]
        public void ItCanGenerateAGenome()
        {
            var configuration = GATestHelper.GetTravelingSalesmanDefaultConfiguration();
            var chromosome = GATestHelper.GetTravelingSalesmanChromosome();
            var pool = PopulationGenerator.Generate(chromosome.Genes, configuration);
            Assert.AreEqual(configuration.MaxPopulationSize, pool.Length);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItThrowsAnExceptionWhenNullValuesArePassed()
        {
            var configuration = GATestHelper.GetTravelingSalesmanDefaultConfiguration();
            var pool = PopulationGenerator.Generate(null, configuration);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItThrowsAnExceptionWhenNoValuesArePassed()
        {
            var configuration = GATestHelper.GetTravelingSalesmanDefaultConfiguration();
            var pool = PopulationGenerator.Generate(new TravelingSalesmanGene[1], configuration);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItThrowsAnExceptionWhenConfigurationIsNotPassed()
        {
            var configuration = GATestHelper.GetTravelingSalesmanDefaultConfiguration();
            var chromosome = GATestHelper.GetTravelingSalesmanChromosome();
            var pool = PopulationGenerator.Generate(chromosome.Genes, null);
        }
    }
}
