using Jarrus.GA.ParentSelections;
using Jarrus.GATests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Jarrus.GATests.ParentSelections
{
    [TestClass]
    public class ParentSelectionTests
    {
        [TestMethod]
        public void ItConstructsProperlyIfAllFitnessScoresAreAboveOrEqualToZero()
        {
            var genome = GATestHelper.GetTravelingSalesmanGenome();
            var parentSelection = new RouletteWheelSelection();
            parentSelection.Setup(genome, GATestHelper.GetTravelingSalesmanDefaultConfiguration());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItFailsIfANegativeFitnessScoreIsPassed()
        {
            var genome = GATestHelper.GetTravelingSalesmanGenome();
            genome[0].FitnessScore = -1;
            var parentSelection = new RouletteWheelSelection();

            parentSelection.Setup(genome, GATestHelper.GetTravelingSalesmanDefaultConfiguration());
        }
    }
}
