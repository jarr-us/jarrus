using System;
using Jarrus.GA.Models;
using Jarrus.GATests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jarrus.GATests.BasicTypes
{
    [TestClass]
    public class UnorderedChromosomeTests
    {
        [TestMethod]
        public void ItCanDetermineInequality()
        {
            var randomSeed = 23;
            var c1 = new UnorderedChromosome(10, typeof(PhraseGene), new Random(randomSeed + 1));
            var c2 = new UnorderedChromosome(10, typeof(PhraseGene), new Random(randomSeed));

            Assert.AreEqual(true, c1 != c2);
        }

        [TestMethod]
        public void ItCanDetermineEquality()
        {
            var randomSeed = 22;
            var c1 = new UnorderedChromosome(10, typeof(PhraseGene), new Random(randomSeed));
            var c2 = new UnorderedChromosome(10, typeof(PhraseGene), new Random(randomSeed));

            Assert.AreEqual(true, c1 == c2);
        }
    }
}
