using System;
using Jarrus.GA;
using Jarrus.GA.Utility;
using Jarrus.GATests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jarrus.GATests
{
    [TestClass]
    public class ChromosomeReferenceTests
    {
        [TestMethod]
        public void ItCanDetermineObjectsAreEqualByReference()
        {
            var data = GetSampleDataset();

            var chromosomeOne = new Chromosome(data);

            var tempdata = data[0];
            data[0] = data[1];
            data[1] = tempdata;

            var chromosomeTwo = new Chromosome(data);

            var areEqual = chromosomeOne.Genes[0] == chromosomeTwo.Genes[1];

            Assert.AreEqual(chromosomeOne.Genes[0], chromosomeTwo.Genes[1]);
            Console.Out.WriteLine("");
        }

        [TestMethod]
        public void ItCanDetermineIfObjectsAreEqualByContentOfGenes()
        {
            var one = GATestHelper.GetAlphabetCharacterChromosome();
            var two = GATestHelper.GetAlphabetCharacterChromosome();

            Assert.AreEqual(one, two);
        }

        [TestMethod]
        public void ItCanDetermineIfObjectsAreEqualByHashCode()
        {
            var one = GATestHelper.GetAlphabetCharacterChromosome();
            var two = GATestHelper.GetAlphabetCharacterChromosome();

            var hashsetOne = one.GetHashCode();
            var hashsetTwo = two.GetHashCode();

            Assert.AreEqual(hashsetOne, hashsetTwo);
        }

        [TestMethod]
        public void ItCanDetermineIfObjectsAreNotEqualByHashCode()
        {
            var one = GATestHelper.GetAlphabetCharacterChromosome();
            var two = GATestHelper.GetAlphabetCharacterChromosome();
            two.Genes.Shuffle(new Random());

            var hashsetOne = one.GetHashCode();
            var hashsetTwo = two.GetHashCode();

            Assert.AreNotEqual(hashsetOne, hashsetTwo);
        }

        private TravelingSalesmanGene[] GetSampleDataset()
        {
            var array = new TravelingSalesmanGene[2];

            array[0] = new TravelingSalesmanGene('A');
            array[1] = new TravelingSalesmanGene('B');

            return array;
        }
    }
}
