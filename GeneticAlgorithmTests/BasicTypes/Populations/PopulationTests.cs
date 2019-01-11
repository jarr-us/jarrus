using System;
using System.Collections.Generic;
using System.Linq;
using Jarrus.GA.Models;
using Jarrus.GA.Factory.Enums;
using Jarrus.GA.Utility;
using Jarrus.GATests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jarrus.GATests
{
    [TestClass]
    public class PopulationTests
    {
        private Chromosome[] _pool;
        private TravelingSalesmanGene[] _possibleValues;

        [TestInitialize]
        public void Setup()
        {
            _possibleValues = GATestHelper.GetTravelingSalesmanChromosome().Genes.Cast<TravelingSalesmanGene>().ToArray();
            _pool = GATestHelper.GetTravelingSalesmanPopulation();
        }

        [TestMethod]
        public void ItHasAValidConstructor()
        {
            var population = new OrderedPopulation(GATestHelper.GetTravelingSalesmanDefaultConfiguration(), _pool, _possibleValues);
            Assert.IsNotNull(population.Configuration);
            Assert.IsNotNull(population.Chromosomes);
        }

        [TestMethod]
        public void ItCanUseUnorderedGenes()
        {
            var configuration = GATestHelper.GetTravelingSalesmanDefaultConfiguration();
            configuration.GeneSize = 11;
            var pool = PopulationGenerator.GenerateUnorderedPopulation(configuration, typeof(PhraseGene));

            var population = new UnorderedPopulation(GATestHelper.GetTravelingSalesmanDefaultConfiguration(), _pool, typeof(PhraseGene));
            Assert.IsNotNull(population.Configuration);
            Assert.IsNotNull(population.Chromosomes);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItThrowsAnExceptionIfConfigurationIsNull()
        {
            new OrderedPopulation(null, _pool, _possibleValues);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItThrowsAnExceptionIfThePoolIsNull()
        {
            new OrderedPopulation(GATestHelper.GetTravelingSalesmanDefaultConfiguration(), null, _possibleValues);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItThrowsAnExceptionIfThePoolIsNotLargeEnough()
        {
            new OrderedPopulation(GATestHelper.GetTravelingSalesmanDefaultConfiguration(), _pool.Subset(0, 2), _possibleValues);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItThrowsAnExceptionIfThePossibleValuesAreNotSet()
        {
            new OrderedPopulation(GATestHelper.GetTravelingSalesmanDefaultConfiguration(), _pool);
        }

        [TestMethod]
        public void ItCanAdvanceToTheNextGeneration()
        {
            var population = new OrderedPopulation(GATestHelper.GetTravelingSalesmanDefaultConfiguration(), _pool, _possibleValues);
            var nextGen = population.Advance();

            Assert.AreEqual(1, population.GenerationNumber);
            Assert.AreEqual(2, nextGen.GenerationNumber);
        }

        [TestMethod]
        public void ItCanAdvanceToTheNextGenerationViaImmigration()
        {
            var config = GATestHelper.GetTravelingSalesmanDefaultConfiguration();
            config.CrossoverRate = 0.0;
            config.ElitismRate = 0.0;

            var population = new OrderedPopulation(config, _pool, _possibleValues);
            var nextGen = population.Advance();

            Assert.AreEqual(1, population.GenerationNumber);
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

            var population = new OrderedPopulation(config, _pool, _possibleValues);
            var nextGen = population.Advance();

            Assert.IsTrue(nextGen.Chromosomes.Where(o => o.GenerationNumber == 1).Count() > 0);
        }

        [TestMethod]
        public void ItDeterminesFitnessScoreOnCreation()
        {
            var config = GATestHelper.GetTravelingSalesmanDefaultConfiguration();
            var population = new OrderedPopulation(config, _pool, _possibleValues);
            
            foreach(var chromosome in population.Chromosomes)
            {
                Assert.AreNotEqual(0, chromosome.FitnessScore);
            }
        }

        [TestMethod]
        public void ItCanRetireChromosomes()
        {
            var config = GATestHelper.GetTravelingSalesmanDefaultConfiguration();
            config.MaxRetirement = 1;
            config.MaxGenerations = 1;
            config.PopulationSize = 10;

            var population = new OrderedPopulation(config, _pool, _possibleValues);

            var nextGen = population.Advance();
            Assert.IsTrue(nextGen.Retired.Count() > 0);
        }

        [TestMethod]
        public void ItDoesNotAllowDuplicates()
        {
            var config = GATestHelper.GetTravelingSalesmanDefaultConfiguration();
            config.PopulationSize = 10;

            _pool = GATestHelper.GetTravelingSalesmanPopulation(config);

            config.DuplicationType = DuplicationType.Prevent;
            var population = new OrderedPopulation(config, _pool, _possibleValues);

            var nextGen = population.Advance();
            var hashset = new HashSet<string>();

            foreach (var chromosome in nextGen.Chromosomes)
            {
                hashset.Add(chromosome.ToString());
            }

            Assert.AreEqual(config.PopulationSize, hashset.Count());
        }

        [TestMethod]
        public void ItAllowsDuplicates()
        {
            var config = GATestHelper.GetTravelingSalesmanDefaultConfiguration();
            var population = new OrderedPopulation(config, _pool, _possibleValues);

            var nextGen = population.Advance();
            var hashset = new HashSet<string>();

            foreach (var chromosome in nextGen.Chromosomes)
            {
                hashset.Add(chromosome.ToString());
            }

            Assert.AreNotEqual(config.PopulationSize, hashset.Count());
        }
    }
}
