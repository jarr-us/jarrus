using System;
using System.Collections.Generic;
using System.Linq;
using Jarrus.GA;
using Jarrus.GA.Enums;
using Jarrus.GA.Utility;
using Jarrus.GATests.Models;
using Jarrus.GATests.Models.FitnessCalculators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jarrus.GATests
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
        public void ItCanDetermineIfTheChromosomeShouldRetireBasedOnAge_False()
        {
            var chromosome = GATestHelper.GetTravelingSalesmanChromosome();
            chromosome.Age = 1;

            Assert.IsFalse(chromosome.ShouldRetire(GATestHelper.GetTravelingSalesmanDefaultConfiguration()));
        }

        [TestMethod]
        public void ItCanDetermineIfTheChromosomeShouldRetireBasedOnAge()
        {
            var chromosome = GATestHelper.GetTravelingSalesmanChromosome();
            chromosome.Age = 100;

            Assert.IsTrue(chromosome.ShouldRetire(GATestHelper.GetTravelingSalesmanDefaultConfiguration()));
        }

        [TestMethod]
        public void ItWillNotRetireIfSettingDisabled()
        {
            var chromosome = GATestHelper.GetTravelingSalesmanChromosome();
            chromosome.Age = 5000;

            var settings = GATestHelper.GetTravelingSalesmanDefaultConfiguration();
            settings.MaximumLifeSpan = 0;

            Assert.IsFalse(chromosome.ShouldRetire(settings));
        }


        [TestMethod]
        public void ItCanOutputGenesToCommaDelimitedString()
        {
            var chromosome = GATestHelper.GetTravelingSalesmanChromosome();
            Assert.AreEqual("A,B,C,D", chromosome.ToString());
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
