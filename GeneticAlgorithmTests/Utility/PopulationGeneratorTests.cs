using System;
using Jarrus.GA.Utility;
using Jarrus.GATests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jarrus.GATests.Utility
{
    [TestClass]
    public class PopulationGeneratorTests
    {
        [TestMethod]
        public void ItCanGenerateAGenome()
        {
            var configuration = GATestHelper.GetTravelingSalesmanDefaultConfiguration();
            var chromosome = GATestHelper.GetTravelingSalesmanChromosome();
            var pool = PopulationGenerator.GenerateOrderedPopulation(configuration, chromosome.Genes);
            Assert.AreEqual(configuration.PopulationSize, pool.Length);
        }

        [TestMethod]
        public void ItCanGenerateAnUnorderedPopulation()
        {
            var configuration = GATestHelper.GetTravelingSalesmanDefaultConfiguration();
            configuration.GeneSize = 11;
            var pool = PopulationGenerator.GenerateUnorderedPopulation(configuration, typeof(PhraseGene));
            Assert.AreEqual(configuration.PopulationSize, pool.Length);
        }

        [TestMethod]
        public void ItCanGenerateAnUnorderedPopulationAndRandomizesTheirValues()
        {
            var configuration = GATestHelper.GetTravelingSalesmanDefaultConfiguration();
            configuration.GeneSize = 24;

            var pool = PopulationGenerator.GenerateUnorderedPopulation(configuration, typeof(PhraseGene));
            Assert.AreEqual(configuration.PopulationSize, pool.Length);

            foreach(var chromosome in pool)
            {
                Assert.IsNotNull(chromosome);

                foreach(var gene in chromosome.Genes)
                {
                    var phraseGene = (PhraseGene)gene;
                    Assert.IsNotNull(phraseGene);
                }
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItThrowsAnExceptionIfAnOrderedGeneIsUsedToPopulateAnUnorderedPopulation()
        {
            var configuration = GATestHelper.GetTravelingSalesmanDefaultConfiguration();
            var pool = PopulationGenerator.GenerateUnorderedPopulation(configuration, typeof(TravelingSalesmanGene));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItThrowsAnExceptionWhenNullValuesArePassed()
        {
            var configuration = GATestHelper.GetTravelingSalesmanDefaultConfiguration();
            var pool = PopulationGenerator.GenerateOrderedPopulation(configuration, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItThrowsAnExceptionWhenNoValuesArePassed()
        {
            var configuration = GATestHelper.GetTravelingSalesmanDefaultConfiguration();
            var pool = PopulationGenerator.GenerateOrderedPopulation(configuration, new TravelingSalesmanGene[1]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItThrowsAnExceptionWhenConfigurationIsNotPassed()
        {
            var configuration = GATestHelper.GetTravelingSalesmanDefaultConfiguration();
            var chromosome = GATestHelper.GetTravelingSalesmanChromosome();
            var pool = PopulationGenerator.GenerateOrderedPopulation(null, chromosome.Genes);
        }
    }
}
