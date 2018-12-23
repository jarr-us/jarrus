using GeneticAlgorithms;
using GeneticAlgorithms.Crossovers;
using GeneticAlgorithmTests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace GeneticAlgorithmTests.Crossovers
{
    [TestClass]
    public class CrossoverTests
    {
        [TestMethod]
        public void ItDoesNotThrowErrorsIfValidationPasses()
        {
            var size = GATestHelper.GetRandomInteger(16, 256);
            var father = new Chromosome<double>(size);
            var mother = new Chromosome<double>(size);

            var singlePointCrossover = new SinglePointCrossover();
            singlePointCrossover.Execute(father, mother, GATestHelper.GetDefaultConfiguration<double>());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItThrowsAnErrorIfTheChromosomesAreNotOfTheSameSize()
        {
            var father = new Chromosome<double>(GATestHelper.GetRandomInteger(16, 32));
            var mother = new Chromosome<double>(GATestHelper.GetRandomInteger(1, 8));

            var singlePointCrossover = new SinglePointCrossover();

            singlePointCrossover.Execute(father, mother, GATestHelper.GetDefaultConfiguration<double>());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItThrowsAnErrorIfTheFatherIsNull()
        {
            var father = new Chromosome<double>(GATestHelper.GetRandomInteger(16, 32));
            var mother = new Chromosome<double>(GATestHelper.GetRandomInteger(1, 8));

            var singlePointCrossover = new SinglePointCrossover();
            singlePointCrossover.Execute(null, mother, GATestHelper.GetDefaultConfiguration<double>());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItThrowsAnErrorIfTheMotherIsNull()
        {
            var father = new Chromosome<double>(GATestHelper.GetRandomInteger(16, 32));
            var mother = new Chromosome<double>(GATestHelper.GetRandomInteger(1, 8));

            var singlePointCrossover = new SinglePointCrossover();
            singlePointCrossover.Execute(father, null, GATestHelper.GetDefaultConfiguration<double>());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItThrowsAnErrorIfTheParentsAreNotLargeEnough()
        {
            var father = new Chromosome<double>(1);
            var mother = new Chromosome<double>(1);

            var singlePointCrossover = new SinglePointCrossover();
            singlePointCrossover.Execute(father, mother, GATestHelper.GetDefaultConfiguration<double>());
        }
    }
}
