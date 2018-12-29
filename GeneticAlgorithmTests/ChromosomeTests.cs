using System;
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
        public void ItsConstructorDeterminesItsType()
        {
            var doubleChromo = new Chromosome<double>(GATestHelper.GetRandomInteger(1, 255));
            Assert.IsTrue(new double[1].GetType().ToString() == doubleChromo.Genes.GetType().ToString());
        }

        [TestMethod]
        public void ItsConstructorDeterminesTheGeneSize()
        {
            var random = GATestHelper.GetRandomInteger(1, 255);
            var doubleChromo = new Chromosome<double>(random);
            Assert.AreEqual(random, doubleChromo.Genes.Length);
        }

        [TestMethod]
        public void ItCanAcceptTheGenesAsAParameter()
        {
            var charChromo = new Chromosome<char>('A', 'B', 'C', 'D');
            Assert.AreEqual('A', charChromo.Genes[0]);
            Assert.AreEqual('B', charChromo.Genes[1]);
            Assert.AreEqual('C', charChromo.Genes[2]);
            Assert.AreEqual('D', charChromo.Genes[3]);
        }

        [TestMethod]
        public void ItsToStringMethodShowsAsCommaDelimited()
        {
            var charChromo = new Chromosome<char>('A', 'B', 'C', 'D');
            Assert.AreEqual("A,B,C,D", charChromo.ToString());
        }

        [TestMethod]
        public void ItCanDetermineIfTheChromosomeShouldRetireBasedOnAge_False()
        {
            var charChromo = new Chromosome<char>('A', 'B', 'C', 'D');
            charChromo.Age = 1;

            Assert.IsFalse(charChromo.ShouldRetire(GATestHelper.GetDefaultConfiguration<char>()));
        }

        [TestMethod]
        public void ItCanDetermineIfTheChromosomeShouldRetireBasedOnAge()
        {
            var charChromo = new Chromosome<char>('A', 'B', 'C', 'D');
            charChromo.Age = 100;

            Assert.IsTrue(charChromo.ShouldRetire(GATestHelper.GetDefaultConfiguration<char>()));
        }

        [TestMethod]
        public void ItWillNotRetireIfSettingDisabled()
        {
            var charChromo = new Chromosome<char>('A', 'B', 'C', 'D');
            charChromo.Age = 5000;

            var settings = GATestHelper.GetDefaultConfiguration<char>();
            settings.MaximumLifeSpan = 0;

            Assert.IsFalse(charChromo.ShouldRetire(settings));
        }


        [TestMethod]
        public void ItCanOutputGenesToCommaDelimitedString()
        {
            var charChromo = new Chromosome<char>('A', 'B', 'C', 'D');
            Assert.AreEqual("A,B,C,D", charChromo.ToString());
        }

        [TestMethod]
        public void ItCanOutputGenesToCommaDelimitedStringWhenEmpty()
        {
            var charChromo = new Chromosome<char>();
            Assert.AreEqual("", charChromo.ToString());
        }

        [TestMethod]
        public void ItHasADefaultName()
        {
            var chromo = new Chromosome<char>();
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

        private Chromosome<char> GetPersonWithParents()
        {
            var father = GetRandomNamedChromosome();
            var mother = GetRandomNamedChromosome();
            var child = GetRandomNamedChromosome();

            child.SetParents(father, mother);
            return child;
        }

        private Chromosome<char> GetRandomNamedChromosome()
        {
            var chromo = new Chromosome<char>();

            var lastName = NameGenerator.GetLastName(_random);
            while(_lastNamesUsed.Contains(lastName)) { lastName = NameGenerator.GetLastName(_random); }

            chromo.FirstName = NameGenerator.GetFirstName(_random);
            chromo.LastName = lastName;

            return chromo;
        }
    }
}
