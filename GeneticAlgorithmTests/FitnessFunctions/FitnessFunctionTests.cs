using System;
using Jarrus.GA;
using Jarrus.GATests.Models;
using Jarrus.GATests.Models.FitnessFunctions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jarrus.GATests.FitnessCalculators
{
    [TestClass]
    public class FitnessFunctionTests
    {
        [TestMethod]
        public void ItHasAValidConstructor()
        {
            var fitnessGenerator = new TravelingSalesmanFitnessFunction();
        }

        [TestMethod]
        public void ItCanDetermineAFitnessScore()
        {
            var fitnessGenerator = new TravelingSalesmanFitnessFunction();
            var chromosome = GATestHelper.GetTravelingSalesmanChromosome();

            var value = fitnessGenerator.GetFitnessScoreFor(chromosome);
            Assert.AreEqual(10 + 35 + 30 + 20, value);
        }
    }
}
