using System;
using GeneticAlgorithms;
using GeneticAlgorithms.ParentSelections;
using GeneticAlgorithms.Utility;
using GeneticAlgorithmTests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GeneticAlgorithmTests
{
    [TestClass]
    public class GeneticAlgorithmTests
    {
        private char[] _exampleGenes;
        private GAConfiguration<char> _configuration;

        [TestInitialize]
        public void Setup()
        {
            _configuration = GATestHelper.GetDefaultConfiguration<char>();
            _configuration.PreventDuplicationInPool = true;
            _configuration.PoolSize = 10;
            _configuration.Iterations = 5;

            _exampleGenes = GATestHelper.GetTravelingSalesmanChromosome().Genes;
        }

        [TestMethod]
        public void ItHasAValidConstructor()
        {
            var ga = new GeneticAlgorithm<char>(_configuration, _exampleGenes);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItThrowsAnExceptionIfTheConfigurationIsNotSet()
        {
            var ga = new GeneticAlgorithm<char>(null, _exampleGenes);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItThrowsAnExceptionIfTheGenesAreNotSet()
        {
            var ga = new GeneticAlgorithm<char>(_configuration, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItThrowsAnExceptionIfTheGeneLengthIsInvalid()
        {
            var ga = new GeneticAlgorithm<char>(_configuration, _exampleGenes.Subset(0, 1));
        }
    }
}
