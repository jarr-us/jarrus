﻿using System;
using GeneticAlgorithms.Crossovers.Unordered;
using GeneticAlgorithms.Utility;
using GeneticAlgorithmTests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GeneticAlgorithmTests.Crossovers
{
    [TestClass]
    public class SinglePointCrossoverTests
    {
        [TestMethod]
        public void ItCanPerformACrossover()
        {
            var father = GATestHelper.GetAlphabetCharacterChromosome();
            var mother = GATestHelper.GetAlphabetCharacterChromosome();
            mother.Genes.Shuffle();

            var singlePoint = new SinglePointCrossover();

            var child = singlePoint.Execute(father, mother, GATestHelper.GetDefaultConfiguration<ExampleGene>());
            Console.Out.WriteLine("Child: " + child.ToString());

            Assert.AreNotEqual(father.ToString(), child.ToString());
            Assert.AreNotEqual(mother.ToString(), child.ToString());
        }

        [TestMethod]
        public void ItCanEnsureTheMotherAndFatherPassAtLeastOneGene()
        {
            var father = GATestHelper.GetAlphabetCharacterChromosome();
            var mother = GATestHelper.GetAlphabetCharacterChromosome();
            mother.Genes.Shuffle();

            var singlePoint = new SinglePointCrossover();
            var child = singlePoint.Execute(father, mother, GATestHelper.GetDefaultConfiguration<ExampleGene>());
            Assert.AreNotEqual(child, father);
        }
    }
}
