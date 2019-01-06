using System;
using GeneticAlgorithms;
using GeneticAlgorithms.Crossovers.Ordered;
using GeneticAlgorithms.Utility;
using GeneticAlgorithmTests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GeneticAlgorithmTests.Crossovers
{
    [TestClass]
    public class OrderedCrossoverTests
    {
        private Chromosome _father, _mother;

        public OrderedCrossoverTests()
        {
            _father = GATestHelper.GetAlphabetCharacterChromosome();
            _mother = GATestHelper.GetAlphabetCharacterChromosome();
            _mother.Genes.Shuffle(new Random());
        }

        [TestMethod]
        public void ItCanPerformACrossover()
        {
            var crossover = new OrderCrossover();
            var settings = GATestHelper.GetTravelingSalesmanDefaultConfiguration();

            var child = crossover.Execute(_father, _mother, settings);
            Console.Out.WriteLine("Child: " + child.ToString());

            Assert.AreNotEqual(_father.ToString(), child.ToString());
            Assert.AreNotEqual(_mother.ToString(), child.ToString());
        }
    }
}
