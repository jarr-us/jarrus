using System;
using System.Linq;
using GeneticAlgorithms.Mutations;
using GeneticAlgorithmTests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GeneticAlgorithmTests.Mutations
{
    [TestClass]
    public class ScrambleMutationTests
    {
        [TestMethod]
        public void ItCanScrambleGenes()
        {
            var chromosome = GATestHelper.GetAlphabetCharacterChromosome();
            var mutation = new ScrambleMutation();
            mutation.Scramble(chromosome, 2, 6, GATestHelper.GetDefaultConfiguration());

            Assert.AreNotEqual("A,B,C,D,E,F,G,H,I,J", chromosome.ToString());

            var genes = chromosome.Genes.Cast<ExampleGene>().ToArray();

            Assert.AreEqual('A', genes[0].Value);
            Assert.AreEqual('B', genes[1].Value);
            Assert.AreEqual('H', genes[7].Value);
            Assert.AreEqual('I', genes[8].Value);
            Assert.AreEqual('J', genes[9].Value);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ItErrorsIfTheIndexesAreOutOfRange()
        {
            var chromosome = GATestHelper.GetAlphabetCharacterChromosome();
            var mutation = new ScrambleMutation();

            mutation.Scramble(chromosome, -1, 1, GATestHelper.GetDefaultConfiguration());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItErrorsIfEndIsBeforeTheStart()
        {
            var chromosome = GATestHelper.GetAlphabetCharacterChromosome();
            var mutation = new ScrambleMutation();

            mutation.Scramble(chromosome, 2, 1, GATestHelper.GetDefaultConfiguration());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItErrorsIfEndIsSameAsStart()
        {
            var chromosome = GATestHelper.GetAlphabetCharacterChromosome();
            var mutation = new ScrambleMutation();

            mutation.Scramble(chromosome, 2, 2, GATestHelper.GetDefaultConfiguration());
        }

        [TestMethod]
        public void ItCanRandomlyScrambleGenes()
        {
            var chromosome = GATestHelper.GetAlphabetCharacterChromosome();
            var mutation = new ScrambleMutation();

            for (int i = 0; i < 100; i++)
            {
                mutation.Mutate(chromosome, GATestHelper.GetDefaultConfiguration());
            }
        }
    }
}
