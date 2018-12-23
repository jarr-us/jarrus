using System;
using GeneticAlgorithms;
using GeneticAlgorithmTests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GeneticAlgorithmTests
{
    [TestClass]
    public class ChromosomeTests
    {
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
    }
}
