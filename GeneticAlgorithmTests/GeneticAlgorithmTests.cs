using System;
using GeneticAlgorithms;
using GeneticAlgorithms.Enums;
using GeneticAlgorithms.Utility;
using GeneticAlgorithmTests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GeneticAlgorithmTests
{
    [TestClass]
    public class GeneticAlgorithmTests
    {
        private ExampleGene[] _exampleGenes;
        private GAConfiguration<ExampleGene> _configuration;

        [TestInitialize]
        public void Setup()
        {
            SetConfiguration();
        }

        private void SetConfiguration()
        {
            _configuration = GATestHelper.GetDefaultConfiguration<ExampleGene>();
            _configuration.PreventDuplicationInPool = true;
            _configuration.PoolSize = 10;
            _configuration.MaxGenerations = 5;

            _exampleGenes = GATestHelper.GetTravelingSalesmanChromosome().Genes;
        }

        [TestMethod]
        public void ItHasAValidConstructor()
        {
            var ga = new GeneticAlgorithm<ExampleGene>(_configuration, _exampleGenes);
        }

        [TestMethod]
        public void ItWillRunMultipleGenerations()
        {
            var ga = new GeneticAlgorithm<ExampleGene>(_configuration, _exampleGenes);
            var runDetails = ga.Run();
            Assert.AreEqual(_configuration.MaxGenerations + 1, ga.Generation);
        }

        [TestMethod]
        public void ItIsRepeatable()
        {
            var ga = new GeneticAlgorithm<ExampleGene>(_configuration, _exampleGenes);
            var runDetails = ga.Run();
            Assert.AreEqual(_configuration.MaxGenerations + 1, ga.Generation);

            Assert.AreEqual(LastName.Miranda, runDetails.BestChromosome.LastName);
            Assert.AreEqual(80, runDetails.BestChromosome.FitnessScore);
        }

        [TestMethod]
        public void ItReturnsAnObjectWithDetailsOfTheRun()
        {
            var ga = new GeneticAlgorithm<ExampleGene>(_configuration, _exampleGenes);
            var run = ga.Run();

            Assert.AreEqual(_configuration.MaxGenerations + 1, ga.Generation);
            Assert.AreNotEqual(-1, run.BestChromosome.FitnessScore);
        }

        [TestMethod]
        public void ItSetsTheStartAndEndTimeOfARun()
        {
            var ga = new GeneticAlgorithm<ExampleGene>(_configuration, _exampleGenes);
            var run = ga.Run();

            Assert.AreEqual(_configuration.MaxGenerations + 1, ga.Generation);
            Assert.IsTrue(run.GetTotalMSToRun() >= 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItThrowsAnExceptionIfTheConfigurationIsNotSet()
        {
            var ga = new GeneticAlgorithm<ExampleGene>(null, _exampleGenes);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItThrowsAnExceptionIfTheGenesAreNotSet()
        {
            var ga = new GeneticAlgorithm<ExampleGene>(_configuration, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItThrowsAnExceptionIfTheGeneLengthIsInvalid()
        {
            var ga = new GeneticAlgorithm<ExampleGene>(_configuration, _exampleGenes.Subset(0, 1));
        }
    }
}
