using System;
using System.Linq;
using Jarrus.GA;
using Jarrus.GA.Enums;
using Jarrus.GA.Factory.Enums;
using Jarrus.GA.Models;
using Jarrus.GA.Utility;
using Jarrus.GATests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jarrus.GATests
{
    [TestClass]
    public class GAlgorithmTests
    {
        private TravelingSalesmanGene[] _exampleGenes;
        private GAConfiguration _configuration;

        [TestInitialize]
        public void Setup()
        {
            SetConfiguration();
        }

        private void SetConfiguration()
        {
            _configuration = GATestHelper.GetTravelingSalesmanDefaultConfiguration();
            _configuration.DuplicationType = DuplicationType.Prevent;
            _configuration.PopulationSize = 10;
            _configuration.MaxRetirement = 2;
            _configuration.MaxGenerations = 5;

            _exampleGenes = Array.ConvertAll(GATestHelper.GetTravelingSalesmanChromosome().Genes, option => (TravelingSalesmanGene)option);
        }

        [TestMethod]
        public void ItHasAValidConstructor()
        {
            var ga = new OrderedGeneticAlgorithm(_configuration, _exampleGenes);
        }

        [TestMethod]
        public void ItWillRunMultipleGenerations()
        {
            var ga = new OrderedGeneticAlgorithm(_configuration, _exampleGenes);
            var runDetails = ga.Run();
            Assert.AreEqual(_configuration.MaxGenerations + 1, ga.Generation);
        }

        [TestMethod]
        public void ItIsRepeatable()
        {
            var ga = new OrderedGeneticAlgorithm(_configuration, _exampleGenes);
            var runDetails = ga.Run();
            Assert.AreEqual(_configuration.MaxGenerations + 1, ga.Generation);

            Assert.AreEqual(LastName.Bernard, runDetails.BestChromosome.LastName);
            Assert.AreEqual(80, runDetails.BestChromosome.FitnessScore);
        }
        
        [TestMethod]
        public void ItKeepsTrackOfTheBestScore()
        {
            var ga = new OrderedGeneticAlgorithm(_configuration, _exampleGenes);
            var runDetails = ga.Run();
            Assert.AreEqual(_configuration.MaxGenerations + 1, ga.Generation);

            Assert.AreEqual(LastName.Bernard, runDetails.BestChromosome.LastName);
            Assert.AreEqual(FirstName.Anakin, runDetails.BestChromosome.FirstName);
            Assert.AreEqual(80, runDetails.BestChromosome.FitnessScore);

            var lowest = runDetails.Population.Chromosomes.Select(o => o.FitnessScore).Min();
            var lastBestSeen = runDetails.Population.Chromosomes.Where(o => o.FitnessScore == lowest).First();
            Assert.AreEqual(LastName.Fry, lastBestSeen.LastName);
            Assert.AreEqual(FirstName.Ira, lastBestSeen.FirstName);
        }

        [TestMethod]
        public void ItReturnsAnObjectWithDetailsOfTheRun()
        {
            var ga = new OrderedGeneticAlgorithm(_configuration, _exampleGenes);
            var run = ga.Run();

            Assert.AreEqual(_configuration.MaxGenerations + 1, ga.Generation);
            Assert.AreNotEqual(-1, run.BestChromosome.FitnessScore);
        }

        [TestMethod]
        public void ItSetsTheStartAndEndTimeOfARun()
        {
            var ga = new OrderedGeneticAlgorithm(_configuration, _exampleGenes);
            var run = ga.Run();

            Assert.AreEqual(_configuration.MaxGenerations + 1, ga.Generation);
            Assert.IsTrue(run.GetTotalMSToRun() >= 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItThrowsAnExceptionIfTheConfigurationIsNotSet()
        {
            var ga = new OrderedGeneticAlgorithm(null, _exampleGenes);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItThrowsAnExceptionIfTheGenesAreNotSet()
        {
            var ga = new OrderedGeneticAlgorithm(_configuration);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItThrowsAnExceptionIfTheGeneLengthIsInvalid()
        {
            var ga = new OrderedGeneticAlgorithm(_configuration, _exampleGenes.Subset(0, 1));
        }
    }
}
