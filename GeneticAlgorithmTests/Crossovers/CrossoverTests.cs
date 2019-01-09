using Jarrus.GA;
using Jarrus.GA.Crossovers;
using Jarrus.GA.Crossovers.Unordered;
using Jarrus.GATests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Jarrus.GATests.Crossovers
{
    [TestClass]
    public class CrossoverTests
    {
        [TestMethod]
        public void ItDoesNotThrowErrorsIfValidationPasses()
        {
            var size = GATestHelper.GetRandomInteger(16, 256);
            var father = new Chromosome(size);
            var mother = new Chromosome(size);

            var singlePointCrossover = new SinglePointCrossover();
            singlePointCrossover.Execute(father, mother, GATestHelper.GetTravelingSalesmanDefaultConfiguration());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItThrowsAnErrorIfTheChromosomesAreNotOfTheSameSize()
        {
            var father = new Chromosome(GATestHelper.GetRandomInteger(16, 32));
            var mother = new Chromosome(GATestHelper.GetRandomInteger(1, 8));

            var singlePointCrossover = new SinglePointCrossover();

            singlePointCrossover.Execute(father, mother, GATestHelper.GetTravelingSalesmanDefaultConfiguration());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItThrowsAnErrorIfTheFatherIsNull()
        {
            var father = new Chromosome(GATestHelper.GetRandomInteger(16, 32));
            var mother = new Chromosome(GATestHelper.GetRandomInteger(1, 8));

            var singlePointCrossover = new SinglePointCrossover();
            singlePointCrossover.Execute(null, mother, GATestHelper.GetTravelingSalesmanDefaultConfiguration());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItThrowsAnErrorIfTheMotherIsNull()
        {
            var father = new Chromosome(GATestHelper.GetRandomInteger(16, 32));
            var mother = new Chromosome(GATestHelper.GetRandomInteger(1, 8));

            var singlePointCrossover = new SinglePointCrossover();
            singlePointCrossover.Execute(father, null, GATestHelper.GetTravelingSalesmanDefaultConfiguration());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItThrowsAnErrorIfTheParentsAreNotLargeEnough()
        {
            var father = new Chromosome(1);
            var mother = new Chromosome(1);

            var singlePointCrossover = new SinglePointCrossover();
            singlePointCrossover.Execute(father, mother, GATestHelper.GetTravelingSalesmanDefaultConfiguration());
        }
    }
}
