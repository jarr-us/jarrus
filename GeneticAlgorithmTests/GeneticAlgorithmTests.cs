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
        public void ItWillRunMultipleGenerations()
        {
            var ga = new GeneticAlgorithm<char>(_configuration, _exampleGenes);
            ga.Run();
            Assert.AreEqual(_configuration.Iterations + 1, ga.Generation);
        }

        [TestMethod]
        public void ItReturnsAnObjectWithDetailsOfTheRun()
        {
            var ga = new GeneticAlgorithm<char>(_configuration, _exampleGenes);
            var run = ga.Run();

            Assert.AreEqual(_configuration.Iterations + 1, ga.Generation);
            Assert.AreNotEqual(-1, run.HighestScore);
            Assert.AreNotEqual(-1, run.LowestScore);
            Assert.AreNotEqual(-1, run.HighestScoreGeneration);
            Assert.AreNotEqual(-1, run.LowestScoreGeneration);
        }

        [TestMethod]
        public void ItSetsTheStartAndEndTimeOfARun()
        {
            var ga = new GeneticAlgorithm<char>(_configuration, _exampleGenes);
            var run = ga.Run();

            Assert.AreEqual(_configuration.Iterations + 1, ga.Generation);
            Assert.IsTrue(run.GetTotalMSToRun() > 0);
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
