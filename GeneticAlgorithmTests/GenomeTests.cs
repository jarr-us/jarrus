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
    public class GenomeTests
    {
        private Chromosome<char>[] _pool;

        [TestInitialize]
        public void Setup()
        {
            _pool = GATestHelper.GetTravelingSalesmanGenome();
        }

        [TestMethod]
        public void ItHasAValidConstructor()
        {
            var genome = new Genome<char>(GATestHelper.GetDefaultConfiguration<char>(), _pool);
            Assert.IsNotNull(genome.Configuration);
            Assert.IsNotNull(genome.Chromosomes);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItThrowsAnExceptionIfConfigurationIsNull()
        {
            var genome = new Genome<char>(null, _pool);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItThrowsAnExceptionIfThePoolIsNull()
        {
            var genome = new Genome<char>(GATestHelper.GetDefaultConfiguration<char>(), null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItThrowsAnExceptionIfThePoolIsNotLargeEnough()
        {
            var genome = new Genome<char>(GATestHelper.GetDefaultConfiguration<char>(), _pool.Subset(0, 2));
        }

        [TestMethod]
        public void ItCanAdvanceToTheNextGeneration()
        {
            var genome = new Genome<char>(GATestHelper.GetDefaultConfiguration<char>(), _pool);
            var nextGen = genome.Advance();

            Assert.AreEqual(1, genome.GenerationNumber);
            Assert.AreEqual(2, nextGen.GenerationNumber);
        }

        [TestMethod]
        public void ItCanAdvanceToTheNextGenerationViaImmigration()
        {
            var config = GATestHelper.GetDefaultConfiguration<char>();
            config.CrossoverRate = 0.0;
            config.ElitismRate = 0.0;

            var genome = new Genome<char>(config, _pool);
            var nextGen = genome.Advance();

            Assert.AreEqual(1, genome.GenerationNumber);
            Assert.AreEqual(2, nextGen.GenerationNumber);

            foreach(var chromosome in nextGen.Chromosomes)
            {
                Assert.AreEqual(2, chromosome.GenerationNumber);
            }
        }

        [TestMethod]
        public void ItKeepsElitiesBasedOnPercentage()
        {
            var toKeep = GATestHelper.GetRandomInteger(1, 99);
            var config = GATestHelper.GetDefaultConfiguration<char>();
            config.ElitismRate = 0.01 * toKeep;

            var genome = new Genome<char>(config, _pool);
            var nextGen = genome.Advance();

            Assert.AreEqual(toKeep, nextGen.Chromosomes.Where(o => o.GenerationNumber == 1).Count());
        }

        [TestMethod]
        public void ItDeterminesFitnessScoreOnCreation()
        {
            var config = GATestHelper.GetDefaultConfiguration<char>();
            var genome = new Genome<char>(config, _pool);
            
            foreach(var chromosome in genome.Chromosomes)
            {
                Assert.AreNotEqual(0, chromosome.FitnessScore);
            }
        }

        [TestMethod]
        public void ItCanRetireChromosomes()
        {
            var config = GATestHelper.GetDefaultConfiguration<char>();
            config.MaximumLifeSpan = 1;

            var genome = new Genome<char>(config, _pool);

            var nextGen = genome.Advance();
            Assert.AreEqual(100, nextGen.Retired.Count());
        }

        [TestMethod]
        public void ItDoesNotAllowDuplicates()
        {
            var config = GATestHelper.GetDefaultConfiguration<char>();
            config.PoolSize = 10;

            _pool = GATestHelper.GetTravelingSalesmanGenome(config);

            config.PreventDuplicationInPool = true;
            var genome = new Genome<char>(config, _pool);

            var nextGen = genome.Advance();
            var hashset = new HashSet<string>();

            foreach (var chromosome in nextGen.Chromosomes)
            {
                hashset.Add(chromosome.ToString());
            }

            Assert.AreEqual(config.PoolSize, hashset.Count());
        }

        [TestMethod]
        public void ItAllowsDuplicates()
        {
            var config = GATestHelper.GetDefaultConfiguration<char>();
            var genome = new Genome<char>(config, _pool);

            var nextGen = genome.Advance();
            var hashset = new HashSet<string>();

            foreach (var chromosome in nextGen.Chromosomes)
            {
                hashset.Add(chromosome.ToString());
            }

            Assert.AreNotEqual(config.PoolSize, hashset.Count());
        }
    }
}
