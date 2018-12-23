using System;
using GeneticAlgorithms.Utility;
using GeneticAlgorithmTests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GeneticAlgorithmTests.Utility
{
    [TestClass]
    public class GenomeGeneratorTests
    {
        [TestMethod]
        public void ItCanGenerateAGenome()
        {
            var configuration = GATestHelper.GetDefaultConfiguration<char>();
            var chromosome = GATestHelper.GetTravelingSalesmanChromosome();
            var pool = GenomeGenerator.Generate<char>(chromosome.Genes, configuration);
            Assert.AreEqual(configuration.PoolSize, pool.Length);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItThrowsAnExceptionWhenNullValuesArePassed()
        {
            var configuration = GATestHelper.GetDefaultConfiguration<char>();
            var pool = GenomeGenerator.Generate<char>(null, configuration);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItThrowsAnExceptionWhenNoValuesArePassed()
        {
            var configuration = GATestHelper.GetDefaultConfiguration<char>();
            var pool = GenomeGenerator.Generate<char>(new char[1], configuration);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItThrowsAnExceptionWhenConfigurationIsNotPassed()
        {
            var configuration = GATestHelper.GetDefaultConfiguration<char>();
            var chromosome = GATestHelper.GetTravelingSalesmanChromosome();
            var pool = GenomeGenerator.Generate<char>(chromosome.Genes, null);
        }
    }
}
