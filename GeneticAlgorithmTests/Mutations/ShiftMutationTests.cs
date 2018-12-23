using System;
using GeneticAlgorithms.Mutations;
using GeneticAlgorithmTests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GeneticAlgorithmTests.Mutations
{
    [TestClass]
    public class ShiftMutationTests
    {
        [TestMethod]
        public void ItCanSwapGenesAndStaysWithinRange()
        {
            var chromosome = GATestHelper.GetAlphabetCharacterChromosome();
            var mutation = new ShiftMutation();

            for (int i = 0; i < 100; i++)
            {
                mutation.Mutate(chromosome, GATestHelper.GetDefaultSettings<char>());
            }
        }

        [TestMethod]
        public void ItCanShift()
        {
            var chromosome = GATestHelper.GetAlphabetCharacterChromosome();
            var mutation = new ShiftMutation();
            mutation.Shift(chromosome, 1, 4);

            Assert.AreEqual("A,E,B,C,D,F,G,H,I,J", chromosome.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void ItThrowsAnErrorIfOutOfRange()
        {
            var chromosome = GATestHelper.GetAlphabetCharacterChromosome();
            var mutation = new ShiftMutation();
            mutation.Shift(chromosome, -1, 4);
        }
    }
}
