using System;
using GeneticAlgorithms.Mutations;
using GeneticAlgorithmTests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GeneticAlgorithmTests.Mutations
{
    [TestClass]
    public class SwapMutationTests
    {
        [TestMethod]
        public void ItCanSwapGenesAndStaysWithinRange()
        {
            var chromosome = GATestHelper.GetAlphabetCharacterChromosome();
            var mutation = new SwapMutation();

            for (int i = 0; i < 100; i++)
            {
                mutation.Mutate(chromosome, GATestHelper.GetDefaultConfiguration<char>());
            }
        }

        [TestMethod]
        public void ItCanFlipGenes()
        {
            var chromosome = GATestHelper.GetAlphabetCharacterChromosome();
            var mutation = new SwapMutation();

            mutation.FlipGenes(chromosome, 1, 2);
            Assert.AreEqual("A,C,B,D,E,F,G,H,I,J", chromosome.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void ItThrowsAnExceptionIfOutOfRange()
        {
            var chromosome = GATestHelper.GetAlphabetCharacterChromosome();
            var mutation = new SwapMutation();

            mutation.FlipGenes(chromosome, -1, 2);
        }
    }
}
