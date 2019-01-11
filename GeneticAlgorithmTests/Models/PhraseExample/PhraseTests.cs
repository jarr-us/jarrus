using System;
using Jarrus.GA.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jarrus.GATests.Models
{
    [TestClass]
    public class PhraseTests
    {
        [TestMethod]
        public void ItCanDetermineAFitnessScore()
        {
            var random = new Random(22);
            var length = PhraseSolution.Shakespeare.Length;
            var chromosome = new UnorderedChromosome(length, typeof(PhraseGene), random);

            var solution = new PhraseSolution();

            var distance = solution.GetFitnessScoreFor(chromosome);
            Assert.AreNotEqual(0, distance);
        }

        [TestMethod]
        public void ItCanRun()
        {
            var config = GATestHelper.GetPhraseConfiguration();
            var solution = new PhraseSolution();

            var run = solution.Run(config);
            Assert.AreEqual(24, run.BestChromosome.FitnessScore);
        }
    }
}
