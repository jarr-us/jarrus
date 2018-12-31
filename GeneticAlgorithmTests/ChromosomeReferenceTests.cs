using System;
using GeneticAlgorithms;
using GeneticAlgorithms.Utility;
using GeneticAlgorithmTests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GeneticAlgorithmTests
{
    [TestClass]
    public class ChromosomeReferenceTests
    {
        [TestMethod]
        public void ItCanDetermineObjectsAreEqualByReference()
        {
            var data = GetSampleDataset();

            var chromosomeOne = new Chromosome<ExampleGene>(data);

            var tempdata = data[0];
            data[0] = data[1];
            data[1] = tempdata;

            var chromosomeTwo = new Chromosome<ExampleGene>(data);

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
            two.Genes.Shuffle();

            var hashsetOne = one.GetHashCode();
            var hashsetTwo = two.GetHashCode();

            Assert.AreNotEqual(hashsetOne, hashsetTwo);
        }

        private ExampleGene[] GetSampleDataset()
        {
            var array = new ExampleGene[2];

            array[0] = new ExampleGene('A');
            array[1] = new ExampleGene('B');

            return array;
        }
    }
}
