using System;
using GeneticAlgorithms;
using GeneticAlgorithms.Crossovers;
using GeneticAlgorithmTests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GeneticAlgorithmTests.Crossovers
{
    [TestClass]
    public class OrderedCrossoverTests
    {
        private Chromosome<char> _father, _mother;

        public OrderedCrossoverTests()
        {
            _father = new Chromosome<char>('A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M');
            _mother = new Chromosome<char>('M', 'L', 'K', 'J', 'I', 'H', 'G', 'F', 'E', 'D', 'C', 'B', 'A');
        }

        [TestMethod]
        public void ItCanPerformACrossover()
        {
            var crossover = new OrderedCrossover();
            var settings = GATestHelper.GetDefaultSettings<char>();

            for (int i = 0; i < GATestHelper.GetRandomInteger(16, 32); i++)
            {
                var child = crossover.Execute(_father, _mother, settings);
                Console.Out.WriteLine("Child: " + child.ToString());

                Assert.AreNotEqual(_father.ToString(), child.ToString());
                Assert.AreNotEqual(_mother.ToString(), child.ToString());
            }
        }
    }
}
