using System;
using System.Linq;
using GeneticAlgorithms;
using GeneticAlgorithms.BasicTypes;
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
        private GAConfiguration _configuration;

        [TestInitialize]
        public void Setup()
        {
            SetConfiguration();
        }

        private void SetConfiguration()
        {
            _configuration = GATestHelper.GetTravelingSalesmanDefaultConfiguration();
            _configuration.PreventDuplications = true;
            _configuration.MaxPopulationSize = 10;
            _configuration.MaximumLifeSpan = 2;
            _configuration.MaxGenerations = 5;

            _exampleGenes = Array.ConvertAll(GATestHelper.GetTravelingSalesmanChromosome().Genes, option => (ExampleGene)option);
        }

        [TestMethod]
        public void ItHasAValidConstructor()
        {
            var ga = new GeneticAlgorithm(_configuration, _exampleGenes);
        }

        [TestMethod]
        public void ItWillRunMultipleGenerations()
        {
            var ga = new GeneticAlgorithm(_configuration, _exampleGenes);
            var runDetails = ga.Run();
            Assert.AreEqual(_configuration.MaxGenerations + 1, ga.Generation);
        }

        [TestMethod]
        public void ItIsRepeatable()
        {
            var ga = new GeneticAlgorithm(_configuration, _exampleGenes);
            var runDetails = ga.Run();
            Assert.AreEqual(_configuration.MaxGenerations + 1, ga.Generation);

            Assert.AreEqual(LastName.Campos, runDetails.BestChromosome.LastName);
            Assert.AreEqual(80, runDetails.BestChromosome.FitnessScore);
        }
        
        [TestMethod]
        public void ItKeepsTrackOfTheBestScore()
        {
            var ga = new GeneticAlgorithm(_configuration, _exampleGenes);
            var runDetails = ga.Run();
            Assert.AreEqual(_configuration.MaxGenerations + 1, ga.Generation);

            Assert.AreEqual(LastName.Campos, runDetails.BestChromosome.LastName);
            Assert.AreEqual(FirstName.Anakin, runDetails.BestChromosome.FirstName);
            Assert.AreEqual(80, runDetails.BestChromosome.FitnessScore);

            var lowest = runDetails.Population.Chromosomes.Select(o => o.FitnessScore).Min();
            var lastBestSeen = runDetails.Population.Chromosomes.Where(o => o.FitnessScore == lowest).First();
            Assert.AreEqual(LastName.Phelps, lastBestSeen.LastName);
            Assert.AreEqual(FirstName.Clyde, lastBestSeen.FirstName);
        }

        [TestMethod]
        public void ItReturnsAnObjectWithDetailsOfTheRun()
        {
            var ga = new GeneticAlgorithm(_configuration, _exampleGenes);
            var run = ga.Run();

            Assert.AreEqual(_configuration.MaxGenerations + 1, ga.Generation);
            Assert.AreNotEqual(-1, run.BestChromosome.FitnessScore);
        }

        [TestMethod]
        public void ItSetsTheStartAndEndTimeOfARun()
        {
            var ga = new GeneticAlgorithm(_configuration, _exampleGenes);
            var run = ga.Run();

            Assert.AreEqual(_configuration.MaxGenerations + 1, ga.Generation);
            Assert.IsTrue(run.GetTotalMSToRun() >= 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItThrowsAnExceptionIfTheConfigurationIsNotSet()
        {
            var ga = new GeneticAlgorithm(null, _exampleGenes);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItThrowsAnExceptionIfTheGenesAreNotSet()
        {
            var ga = new GeneticAlgorithm(_configuration, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItThrowsAnExceptionIfTheGeneLengthIsInvalid()
        {
            var ga = new GeneticAlgorithm(_configuration, _exampleGenes.Subset(0, 1));
        }
    }
}
