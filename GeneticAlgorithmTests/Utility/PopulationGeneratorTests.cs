﻿using System;
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
            var pool = PopulationGenerator.Generate(new ExampleGene[1], configuration);
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
