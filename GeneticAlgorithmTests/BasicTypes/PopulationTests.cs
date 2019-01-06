using System;
using System.Collections.Generic;
using System.Linq;
using GeneticAlgorithms;
using GeneticAlgorithms.Utility;
using GeneticAlgorithmTests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GeneticAlgorithmTests
{
    [TestClass]
    public class PopulationTests
    {
        private Chromosome[] _pool;
        private ExampleGene[] _possibleValues;

        [TestInitialize]
        public void Setup()
        {
            _possibleValues = GATestHelper.GetTravelingSalesmanChromosome().Genes.Cast<ExampleGene>().ToArray();
            _pool = GATestHelper.GetTravelingSalesmanGenome();
        }

        [TestMethod]
        public void ItHasAValidConstructor()
        {
            var genome = new Population(GATestHelper.GetTravelingSalesmanDefaultConfiguration(), _pool, _possibleValues);
            Assert.IsNotNull(genome.Configuration);
            Assert.IsNotNull(genome.Chromosomes);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItThrowsAnExceptionIfConfigurationIsNull()
        {
            new Population(null, _pool, _possibleValues);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItThrowsAnExceptionIfThePoolIsNull()
        {
            new Population(GATestHelper.GetTravelingSalesmanDefaultConfiguration(), null, _possibleValues);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItThrowsAnExceptionIfThePoolIsNotLargeEnough()
        {
            new Population(GATestHelper.GetTravelingSalesmanDefaultConfiguration(), _pool.Subset(0, 2), _possibleValues);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItThrowsAnExceptionIfThePossibleValuesAreNotSet()
        {
            new Population(GATestHelper.GetTravelingSalesmanDefaultConfiguration(), _pool, null);
        }

        [TestMethod]
        public void ItCanAdvanceToTheNextGeneration()
        {
            var genome = new Population(GATestHelper.GetTravelingSalesmanDefaultConfiguration(), _pool, _possibleValues);
            var nextGen = genome.Advance();

            Assert.AreEqual(1, genome.GenerationNumber);
            Assert.AreEqual(2, nextGen.GenerationNumber);
        }

        [TestMethod]
        public void ItCanAdvanceToTheNextGenerationViaImmigration()
        {
            var config = GATestHelper.GetTravelingSalesmanDefaultConfiguration();
            config.CrossoverRate = 0.0;
            config.ElitismRate = 0.0;

            var genome = new Population(config, _pool, _possibleValues);
            var nextGen = genome.Advance();

            Assert.AreEqual(1, genome.GenerationNumber);
            Assert.AreEqual(2, nextGen.GenerationNumber);

            foreach(var chromosome in nextGen.Chromosomes)
            {
                Assert.AreEqual(2, chromosome.GenerationNumber);
            }
        }

        [TestMethod]
        public void ItKeepsElities()
        {
            var toKeep = GATestHelper.GetRandomInteger(1, 99);
            var config = GATestHelper.GetTravelingSalesmanDefaultConfiguration();
            config.ElitismRate = 0.01 * toKeep;

            var genome = new Population(config, _pool, _possibleValues);
            var nextGen = genome.Advance();

            Assert.IsTrue(nextGen.Chromosomes.Where(o => o.GenerationNumber == 1).Count() > 0);
        }

        [TestMethod]
        public void ItDeterminesFitnessScoreOnCreation()
        {
            var config = GATestHelper.GetTravelingSalesmanDefaultConfiguration();
            var genome = new Population(config, _pool, _possibleValues);
            
            foreach(var chromosome in genome.Chromosomes)
            {
                Assert.AreNotEqual(0, chromosome.FitnessScore);
            }
        }

        [TestMethod]
        public void ItCanRetireChromosomes()
        {
            var config = GATestHelper.GetTravelingSalesmanDefaultConfiguration();
            config.MaximumLifeSpan = 1;

            var genome = new Population(config, _pool, _possibleValues);

            var nextGen = genome.Advance();
            Assert.AreEqual(100, nextGen.Retired.Count());
        }

        [TestMethod]
        public void ItDoesNotAllowDuplicates()
        {
            var config = GATestHelper.GetTravelingSalesmanDefaultConfiguration();
            config.MaxPopulationSize = 10;

            _pool = GATestHelper.GetTravelingSalesmanGenome(config);

            config.PreventDuplications = true;
            var genome = new Population(config, _pool, _possibleValues);

            var nextGen = genome.Advance();
            var hashset = new HashSet<string>();

            foreach (var chromosome in nextGen.Chromosomes)
            {
                hashset.Add(chromosome.ToString());
            }

            Assert.AreEqual(config.MaxPopulationSize, hashset.Count());
        }

        [TestMethod]
        public void ItAllowsDuplicates()
        {
            var config = GATestHelper.GetTravelingSalesmanDefaultConfiguration();
            var genome = new Population(config, _pool, _possibleValues);

            var nextGen = genome.Advance();
            var hashset = new HashSet<string>();

            foreach (var chromosome in nextGen.Chromosomes)
            {
                hashset.Add(chromosome.ToString());
            }

            Assert.AreNotEqual(config.MaxPopulationSize, hashset.Count());
        }
    }
}
