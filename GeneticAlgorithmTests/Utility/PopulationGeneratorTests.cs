using System;
using GeneticAlgorithms.Utility;
using GeneticAlgorithmTests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GeneticAlgorithmTests.Utility
{
    [TestClass]
    public class PopulationGeneratorTests
    {
        [TestMethod]
        public void ItCanGenerateAGenome()
        {
            var configuration = GATestHelper.GetDefaultConfiguration<ExampleGene>();
            var chromosome = GATestHelper.GetTravelingSalesmanChromosome();
            var pool = PopulationGenerator.Generate<ExampleGene>(chromosome.Genes, configuration);
            Assert.AreEqual(configuration.PoolSize, pool.Length);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItThrowsAnExceptionWhenNullValuesArePassed()
        {
            var configuration = GATestHelper.GetDefaultConfiguration<ExampleGene>();
            var pool = PopulationGenerator.Generate<ExampleGene>(null, configuration);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItThrowsAnExceptionWhenNoValuesArePassed()
        {
            var configuration = GATestHelper.GetDefaultConfiguration<ExampleGene>();
            var pool = PopulationGenerator.Generate<ExampleGene>(new ExampleGene[1], configuration);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItThrowsAnExceptionWhenConfigurationIsNotPassed()
        {
            var configuration = GATestHelper.GetDefaultConfiguration<ExampleGene>();
            var chromosome = GATestHelper.GetTravelingSalesmanChromosome();
            var pool = PopulationGenerator.Generate<ExampleGene>(chromosome.Genes, null);
        }
    }
}
