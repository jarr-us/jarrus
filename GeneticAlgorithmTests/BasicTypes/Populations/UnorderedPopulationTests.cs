using System;
using Jarrus.GA.Factory.Enums;
using Jarrus.GA.Models;
using Jarrus.GATests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jarrus.GATests.BasicTypes.Populations
{
    [TestClass]
    public class UnorderedPopulationTests
    {
        private Random _random = new Random(35);

        [TestMethod]
        public void ItCanAddToTheNextGeneration()
        {
            var config = GetUnorderedConfiguration();
            var population = GetUnorderedChromosomes(config);

            var up = new UnorderedPopulation(config, population, typeof(PhraseGene));
            Assert.IsNotNull(up);

            up.AddToNextGeneration(population[0]);
            Assert.AreEqual(1, up.NextGeneration.Count);
            Assert.AreEqual(0, up.OptionsInPool.Count);
        }

        [TestMethod]
        public void ItCanPreventDuplication()
        {
            var config = GetUnorderedConfiguration();
            config.DuplicationType = DuplicationType.Prevent;
            var population = GetUnorderedChromosomes(config);

            var up = new UnorderedPopulation(config, population, typeof(PhraseGene));
            Assert.IsNotNull(up);

            up.AddToNextGeneration(population[0]);
            Assert.AreEqual(1, up.NextGeneration.Count);
            Assert.AreEqual(1, up.OptionsInPool.Count);

            up.AddToNextGeneration(population[0]);
            Assert.AreEqual(1, up.NextGeneration.Count);
            Assert.AreEqual(1, up.OptionsInPool.Count);
        }

        private Chromosome[] GetUnorderedChromosomes(GAConfiguration config)
        {
            var chromosomes = new Chromosome[config.PopulationSize];

            for (int i = 0; i < config.PopulationSize; i++)
            {
                chromosomes[i] = new UnorderedChromosome(PhraseSolution.Shakespeare.Length, typeof(PhraseGene), _random);
            }

            return chromosomes;
        }

        private GAConfiguration GetUnorderedConfiguration() { return GATestHelper.GetPhraseConfiguration(); }
    }
}
