using System;
using GeneticAlgorithms;
using GeneticAlgorithmTests.Models;
using GeneticAlgorithmTests.Models.FitnessFunctions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GeneticAlgorithmTests.FitnessCalculators
{
    [TestClass]
    public class FitnessFunctionTests
    {
        [TestMethod]
        public void ItHasAValidConstructor()
        {
            var fitnessGenerator = new TravelingSalesmanFitnessCalculator();
        }

        [TestMethod]
        public void ItCanDetermineAFitnessScore()
        {
            var fitnessGenerator = new TravelingSalesmanFitnessCalculator();
            var chromosome = GATestHelper.GetTravelingSalesmanChromosome();

            var value = fitnessGenerator.GetFitnessScoreFor(chromosome);
            Assert.AreEqual(10 + 35 + 30 + 20, value);
        }
    }
}
