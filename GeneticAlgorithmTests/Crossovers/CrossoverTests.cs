using Jarrus.GA.Models;
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
            var father = new OrderedChromosome(size);
            var mother = new OrderedChromosome(size);

            var singlePointCrossover = new SinglePointCrossover();
            singlePointCrossover.Execute(father, mother, GATestHelper.GetTravelingSalesmanDefaultConfiguration());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItThrowsAnErrorIfTheChromosomesAreNotOfTheSameSize()
        {
            var father = new OrderedChromosome(GATestHelper.GetRandomInteger(16, 32));
            var mother = new OrderedChromosome(GATestHelper.GetRandomInteger(1, 8));

            var singlePointCrossover = new SinglePointCrossover();

            singlePointCrossover.Execute(father, mother, GATestHelper.GetTravelingSalesmanDefaultConfiguration());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItThrowsAnErrorIfTheFatherIsNull()
        {
            var father = new OrderedChromosome(GATestHelper.GetRandomInteger(16, 32));
            var mother = new OrderedChromosome(GATestHelper.GetRandomInteger(1, 8));

            var singlePointCrossover = new SinglePointCrossover();
            singlePointCrossover.Execute(null, mother, GATestHelper.GetTravelingSalesmanDefaultConfiguration());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItThrowsAnErrorIfTheMotherIsNull()
        {
            var father = new OrderedChromosome(GATestHelper.GetRandomInteger(16, 32));
            var mother = new OrderedChromosome(GATestHelper.GetRandomInteger(1, 8));

            var singlePointCrossover = new SinglePointCrossover();
            singlePointCrossover.Execute(father, null, GATestHelper.GetTravelingSalesmanDefaultConfiguration());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItThrowsAnErrorIfTheParentsAreNotLargeEnough()
        {
            var father = new OrderedChromosome(1);
            var mother = new OrderedChromosome(1);

            var singlePointCrossover = new SinglePointCrossover();
            singlePointCrossover.Execute(father, mother, GATestHelper.GetTravelingSalesmanDefaultConfiguration());
        }
    }
}
