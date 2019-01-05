﻿using System;
using System.Collections.Generic;
using System.Linq;
using GeneticAlgorithms;
using GeneticAlgorithms.Enums;
using GeneticAlgorithms.Utility;
using GeneticAlgorithmTests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GeneticAlgorithmTests
{
    [TestClass]
    public class ChromosomeTests
    {
        private Random _random = new Random();
        private List<LastName> _lastNamesUsed = new List<LastName>();
        
        [TestMethod]
        public void ItsConstructorDeterminesTheGeneSize()
        {
            var random = GATestHelper.GetRandomInteger(1, 255);
            var doubleChromo = new Chromosome(random);
            Assert.AreEqual(random, doubleChromo.Genes.Length);
        }

        [TestMethod]
        public void ItsToStringMethodShowsAsCommaDelimited()
        {
            var ExampleGeneChromo = GATestHelper.GetTravelingSalesmanChromosome();
            Assert.AreEqual("A,B,C,D", ExampleGeneChromo.ToString());
        }

        [TestMethod]
        public void ItCanDetermineIfTheChromosomeShouldRetireBasedOnAge_False()
        {
            var ExampleGeneChromo = GATestHelper.GetTravelingSalesmanChromosome();
            ExampleGeneChromo.Age = 1;

            Assert.IsFalse(ExampleGeneChromo.ShouldRetire(GATestHelper.GetDefaultConfiguration()));
        }

        [TestMethod]
        public void ItCanDetermineIfTheChromosomeShouldRetireBasedOnAge()
        {
            var ExampleGeneChromo = GATestHelper.GetTravelingSalesmanChromosome();
            ExampleGeneChromo.Age = 100;

            Assert.IsTrue(ExampleGeneChromo.ShouldRetire(GATestHelper.GetDefaultConfiguration()));
        }

        [TestMethod]
        public void ItWillNotRetireIfSettingDisabled()
        {
            var ExampleGeneChromo = GATestHelper.GetTravelingSalesmanChromosome();
            ExampleGeneChromo.Age = 5000;

            var settings = GATestHelper.GetDefaultConfiguration();
            settings.MaximumLifeSpan = 0;

            Assert.IsFalse(ExampleGeneChromo.ShouldRetire(settings));
        }


        [TestMethod]
        public void ItCanOutputGenesToCommaDelimitedString()
        {
            var ExampleGeneChromo = GATestHelper.GetTravelingSalesmanChromosome();
            Assert.AreEqual("A,B,C,D", ExampleGeneChromo.ToString());
        }

        [TestMethod]
        public void ItCanOutputGenesToCommaDelimitedStringWhenEmpty()
        {
            var ExampleGeneChromo = new Chromosome();
            Assert.AreEqual("", ExampleGeneChromo.ToString());
        }

        [TestMethod]
        public void ItHasADefaultName()
        {
            var chromo = new Chromosome();
            Assert.AreEqual(FirstName.Kanan, chromo.FirstName);
            Assert.AreEqual(LastName.Jarrus, chromo.LastName);
        }

        [TestMethod]
        public void ItCanSetParents()
        {
            var father = GetRandomNamedChromosome();
            var mother = GetRandomNamedChromosome();
            var child = GetRandomNamedChromosome();

            child.SetParents(father, mother);
            Assert.AreEqual(2, child.Parents.Count);
        }

        [TestMethod]
        public void ItCanSetLineage()
        {
            var father = GetPersonWithParents();
            var mother = GetPersonWithParents();
            var child = GetRandomNamedChromosome();

            child.SetParents(father, mother);
            Assert.AreEqual(6, child.Lineage.Count);
        }

        private Chromosome GetPersonWithParents()
        {
            var father = GetRandomNamedChromosome();
            var mother = GetRandomNamedChromosome();
            var child = GetRandomNamedChromosome();

            child.SetParents(father, mother);
            return child;
        }

        private Chromosome GetRandomNamedChromosome()
        {
            var chromo = new Chromosome();

            var lastName = NameGenerator.GetLastName(_random);
            while(_lastNamesUsed.Contains(lastName)) { lastName = NameGenerator.GetLastName(_random); }

            chromo.FirstName = NameGenerator.GetFirstName(_random);
            chromo.LastName = lastName;

            return chromo;
        }
    }
}
