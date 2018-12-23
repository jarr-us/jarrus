using System;
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
            mutation.Scramble(chromosome, 2, 6, GATestHelper.GetDefaultSettings<char>());

            Assert.AreNotEqual("A,B,C,D,E,F,G,H,I,J", chromosome.ToString());

            Assert.AreEqual('A', chromosome.Genes[0]);
            Assert.AreEqual('B', chromosome.Genes[1]);
            Assert.AreEqual('H', chromosome.Genes[7]);
            Assert.AreEqual('I', chromosome.Genes[8]);
            Assert.AreEqual('J', chromosome.Genes[9]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ItErrorsIfTheIndexesAreOutOfRange()
        {
            var chromosome = GATestHelper.GetAlphabetCharacterChromosome();
            var mutation = new ScrambleMutation();

            mutation.Scramble(chromosome, -1, 1, GATestHelper.GetDefaultSettings<char>());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItErrorsIfEndIsBeforeTheStart()
        {
            var chromosome = GATestHelper.GetAlphabetCharacterChromosome();
            var mutation = new ScrambleMutation();

            mutation.Scramble(chromosome, 2, 1, GATestHelper.GetDefaultSettings<char>());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItErrorsIfEndIsSameAsStart()
        {
            var chromosome = GATestHelper.GetAlphabetCharacterChromosome();
            var mutation = new ScrambleMutation();

            mutation.Scramble(chromosome, 2, 2, GATestHelper.GetDefaultSettings<char>());
        }

        [TestMethod]
        public void ItCanRandomlyScrambleGenes()
        {
            var chromosome = GATestHelper.GetAlphabetCharacterChromosome();
            var mutation = new ScrambleMutation();

            for (int i = 0; i < 100; i++)
            {
                mutation.Mutate(chromosome, GATestHelper.GetDefaultSettings<char>());
            }
        }
    }
}
