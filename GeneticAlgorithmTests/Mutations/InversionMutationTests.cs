using System;
using GeneticAlgorithms.Mutations;
using GeneticAlgorithmTests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GeneticAlgorithmTests.Mutations
{
    [TestClass]
    public class InversionMutationTests
    {
        [TestMethod]
        public void ItCanInverseGenes()
        {
            var chromosome = GATestHelper.GetAlphabetCharacterChromosome();

            var mutation = new InversionMutation();
            mutation.Inverse(chromosome, 2, 6);
            Assert.AreEqual("A,B,G,F,E,D,C,H,I,J", chromosome.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void ItErrorsIfTheIndexesAreOutOfRange()
        {
            var chromosome = GATestHelper.GetAlphabetCharacterChromosome();
            var mutation = new InversionMutation();

            mutation.Inverse(chromosome, -1, 1);
        }

        [TestMethod]
        public void ItCanRandomlyInverseGenes()
        {
            var chromosome = GATestHelper.GetAlphabetCharacterChromosome();
            var mutation = new InversionMutation();

            for (int i = 0; i < 100; i++)
            {
                mutation.Mutate(chromosome, GATestHelper.GetTravelingSalesmanDefaultConfiguration());
            }
        }
    }
}
