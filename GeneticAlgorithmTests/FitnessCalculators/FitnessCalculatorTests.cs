using System;
using GeneticAlgorithms;
using GeneticAlgorithmTests.Models.FitnessCalculators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GeneticAlgorithmTests.FitnessCalculators
{
    [TestClass]
    public class FitnessCalculatorTests
    {
        private char[] _validInputs = { 'A', 'B', 'C', 'D' };

        [TestMethod]
        public void ItHasAValidConstructor()
        {
            var fitnessGenerator = new TravelingSalesmanFitnessCalculator();
        }

        [TestMethod]
        public void ItCanDetermineAFitnessScore()
        {
            var fitnessGenerator = new TravelingSalesmanFitnessCalculator();
            var chromosome = new Chromosome<char>('A', 'B', 'C', 'D');

            var value = fitnessGenerator.GetFitnessScoreFor(chromosome);
            Assert.AreEqual(10 + 35 + 30 + 20, value);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        public void ItFailsIfCharactersAreNotPassed()
        {
            var fitnessGenerator = new TravelingSalesmanFitnessCalculator();
            var chromosome = new Chromosome<string>("A", "B", "C", "D");

            fitnessGenerator.GetFitnessScoreFor(chromosome);
        }
    }
}
