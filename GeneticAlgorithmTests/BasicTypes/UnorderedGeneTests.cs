using System;
using GeneticAlgorithms.Factory.Enums;
using GeneticAlgorithmTests.Models;
using GeneticAlgorithmTests.Models.MutationGenes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GeneticAlgorithmTests.BasicTypes
{
    [TestClass]
    public class UnorderedGeneTests
    {
        private readonly Random _r = new Random(24);

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

            numberGene.Mutate(MutationType.Random);
            Assert.AreEqual(8, numberGene.Value);
        }

        [TestMethod]
        public void ItCanMutateBasedOnAListOfCharacters()
        {
            var gene = new CharacterGene(_r);
            Assert.AreEqual('C', gene.Value);

            gene.Mutate(MutationType.Random);
            Assert.AreEqual('H', gene.Value);
        }

        [TestMethod]
        public void ItCanMutateMultipleTimes()
        {
            var gene = new CharacterGene(_r);
            Assert.AreEqual('C', gene.Value);

            gene.Mutate(MutationType.Random);
            Assert.AreEqual('H', gene.Value);

            gene.Mutate(MutationType.Random);
            Assert.AreEqual('E', gene.Value);

            gene.Mutate(MutationType.Random);
            Assert.AreEqual('G', gene.Value);

            gene.Mutate(MutationType.Random);
            Assert.AreEqual('A', gene.Value);
        }

        [TestMethod]
        public void ItCanMutateBasedOnAListOfStrings()
        {
            var gene = new StringGene(_r);
            Assert.AreEqual("C3", gene.Value);

            gene.Mutate(MutationType.Random);
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

            numberGene.Mutate(MutationType.Random);
            Assert.AreEqual(8, numberGene.Value);
        }

        [TestMethod]
        public void ItCanBoundaryMutate()
        {
            var numberGene = new IntegerGene(_r);
            Assert.AreEqual(1, numberGene.GetNumberOfGeneAttributes());

            numberGene.Mutate(MutationType.Boundary);
            Assert.AreEqual(9, numberGene.Value);
        }

        [TestMethod]
        public void ItCanFlipMutate()
        {
            var gene = new BooleanGene(_r);
            Assert.AreEqual(1, gene.GetNumberOfGeneAttributes());
            Assert.AreEqual(false, gene.Value);

            gene.Mutate(MutationType.Flip);
            Assert.AreEqual(true, gene.Value);
        }
    }
}
