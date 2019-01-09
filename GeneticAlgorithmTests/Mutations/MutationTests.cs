using System;
using Jarrus.GA.Mutations;
using Jarrus.GATests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jarrus.GATests.Mutations
{
    [TestClass]
    public class MutationTests
    {
        [TestMethod]
        public void ItCanSwapGenes()
        {
            var chromosome = GATestHelper.GetAlphabetCharacterChromosome();

            var mutation = new SwapMutation();
            mutation.FlipGenes(chromosome, 0, 2);
            Assert.AreEqual("C,B,A,D,E,F,G,H,I,J", chromosome.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void ItErrorsIfSwapIsOutOfRange()
        {
            var mutation = new SwapMutation();
            mutation.FlipGenes(GATestHelper.GetAlphabetCharacterChromosome(), -1, 3);
        }
    }
}
