using System;
using System.Diagnostics;
using Jarrus.GA.Factory.Enums;
using Jarrus.GATests.Models;
using Jarrus.GATests.Models.TestGenes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jarrus.GATests.BasicTypes
{
    [TestClass]
    public class UnorderedGeneTests
    {
        private readonly Random _r = new Random(24);

        [TestMethod]
        public void ItCanCreateAGeneInLessThan100Ticks()
        {
            var gene = new PhraseGene(new Random());

            var sw = new Stopwatch();
            sw.Start();
            gene = new PhraseGene(new Random());
            sw.Stop();

            Console.Out.WriteLine("Ticks to create unordered gene: " + sw.ElapsedTicks);
            Assert.IsTrue(sw.ElapsedTicks < 100);
        }

        [TestMethod]
        public void ItCanDetermineTheNumberOfGeneAttributesOnAGene()
        {
            var numberGene = new IntegerGene(_r);
            Assert.AreEqual(1, numberGene.GetNumberOfGeneAttributes());
        }

        [TestMethod]
        public void ItCanMutateBasedOnAListOfIntegers()
        {
            var numberGene = new IntegerGene(_r);
            Assert.AreEqual(1, numberGene.GetNumberOfGeneAttributes());

            numberGene.Mutate(MutationStrategy.Random);
            Assert.AreEqual(8, numberGene.Value);
        }

        [TestMethod]
        public void ItCanMutateBasedOnAListOfCharacters()
        {
            var gene = new CharacterGene(_r);
            Assert.AreEqual('C', gene.Value);

            gene.Mutate(MutationStrategy.Random);
            Assert.AreEqual('H', gene.Value);
        }

        [TestMethod]
        public void ItCanMutateMultipleTimes()
        {
            var gene = new CharacterGene(_r);
            Assert.AreEqual('C', gene.Value);

            gene.Mutate(MutationStrategy.Random);
            Assert.AreEqual('H', gene.Value);

            gene.Mutate(MutationStrategy.Random);
            Assert.AreEqual('E', gene.Value);

            gene.Mutate(MutationStrategy.Random);
            Assert.AreEqual('G', gene.Value);

            gene.Mutate(MutationStrategy.Random);
            Assert.AreEqual('A', gene.Value);
        }

        [TestMethod]
        public void ItCanMutateBasedOnAListOfStrings()
        {
            var gene = new StringGene(_r);
            Assert.AreEqual("C3", gene.Value);

            gene.Mutate(MutationStrategy.Random);
            Assert.AreEqual("H8", gene.Value);
        }

        [TestMethod]
        public void ItCanCreateWithRandomValues()
        {
            var gene = new IntegerGene(_r);
            Assert.AreEqual(3, gene.Value);
        }

        [TestMethod]
        public void ItCanMutateRandomly()
        {
            var numberGene = new IntegerGene(_r);
            Assert.AreEqual(1, numberGene.GetNumberOfGeneAttributes());

            numberGene.Mutate(MutationStrategy.Random);
            Assert.AreEqual(8, numberGene.Value);
        }

        [TestMethod]
        public void ItCanBoundaryMutate()
        {
            var numberGene = new IntegerGene(_r);
            Assert.AreEqual(1, numberGene.GetNumberOfGeneAttributes());

            numberGene.Mutate(MutationStrategy.Boundary);
            Assert.AreEqual(9, numberGene.Value);
        }

        [TestMethod]
        public void ItCanFlipMutate()
        {
            var gene = new BooleanGene(_r);
            Assert.AreEqual(1, gene.GetNumberOfGeneAttributes());
            Assert.AreEqual(false, gene.Value);

            gene.Mutate(MutationStrategy.Flip);
            Assert.AreEqual(true, gene.Value);
        }
    }
}
