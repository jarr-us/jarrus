using System;
using System.Collections.Generic;
using System.Linq;
using Jarrus.GA;
using Jarrus.GA.BasicTypes.Chromosomes;
using Jarrus.GA.Enums;
using Jarrus.GA.Utility;
using Jarrus.GATests.Models;
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
            var doubleChromo = new OrderedChromosome(random);
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
            settings.MaxRetirement = 0;

            Assert.IsFalse(chromosome.ShouldRetire(settings));
        }

        [TestMethod]
        public void ItCanMakeAnUnorderedChromosome()
        {
            var random = new Random();
            var phraseType = typeof(PhraseGene);
            var c = new UnorderedChromosome(8, phraseType, random);

            Assert.AreEqual(8, c.Genes.Length);
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
            var ExampleGeneChromo = new OrderedChromosome();
            Assert.AreEqual("", ExampleGeneChromo.ToString());
        }

        [TestMethod]
        public void ItHasADefaultName()
        {
            var chromo = new OrderedChromosome();
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
            var chromo = new OrderedChromosome();

            var lastName = NameGenerator.GetLastName(_random);
            while(_lastNamesUsed.Contains(lastName)) { lastName = NameGenerator.GetLastName(_random); }

            chromo.FirstName = NameGenerator.GetFirstName(_random);
            chromo.LastName = lastName;

            return chromo;
        }
    }
}
