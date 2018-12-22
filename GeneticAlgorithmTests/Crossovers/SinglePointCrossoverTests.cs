using System;
using GeneticAlgorithms;
using GeneticAlgorithms.Crossovers;
using GeneticAlgorithmTests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GeneticAlgorithmTests.Crossovers
{
    [TestClass]
    public class SinglePointCrossoverTests
    {
        [TestMethod]
        public void ItCanPerformACrossover()
        {
            var father = new Chromosome<char>('A', 'B', 'C', 'D');
            var mother = new Chromosome<char>('Z', 'Y', 'X', 'W');

            var singlePoint = new SinglePointCrossover();

            for (int i = 0; i < GATestHelper.GetRandomInteger(16, 32); i++)
            {
                var child = singlePoint.Execute(father, mother, GATestHelper.GetDefaultSettings<char>());
                Console.Out.WriteLine("Child: " + child.ToString());

                Assert.AreNotEqual(father.ToString(), child.ToString());
                Assert.AreNotEqual(mother.ToString(), child.ToString());
            }
        }
    }
}
