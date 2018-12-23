﻿using GeneticAlgorithms.ParentSelections;
using GeneticAlgorithmTests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace GeneticAlgorithmTests.ParentSelections
{
    [TestClass]
    public class ParentSelectionTests
    {
        [TestMethod]
        public void ItConstructsProperlyIfAllFitnessScoresAreAboveOrEqualToZero()
        {
            var genome = GATestHelper.GetTravelingSalesmanGenome();
            var parentSelection = new RouletteWheelSelection<char>();
            parentSelection.Setup(genome, GATestHelper.GetDefaultSettings<char>());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItFailsIfANegativeFitnessScoreIsPassed()
        {
            var genome = GATestHelper.GetTravelingSalesmanGenome();
            genome[0].FitnessScore = -1;
            var parentSelection = new RouletteWheelSelection<char>();

            parentSelection.Setup(genome, GATestHelper.GetDefaultSettings<char>());
        }
    }
}
