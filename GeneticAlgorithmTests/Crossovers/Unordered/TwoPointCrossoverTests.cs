using GeneticAlgorithms;
using GeneticAlgorithms.Crossovers;
using GeneticAlgorithms.Crossovers.Unordered;
using GeneticAlgorithms.Utility;
using GeneticAlgorithmTests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GeneticAlgorithmTests.Crossovers
{
    [TestClass]
    public class TwoPointCrossoverTests
    {
        [TestMethod]
        public void ItCanPerformACrossover()
        {
            var father = GATestHelper.GetAlphabetCharacterChromosome();
            var mother = GATestHelper.GetAlphabetCharacterChromosome();
            mother.Genes.Shuffle();

            var settings = GATestHelper.GetDefaultConfiguration<ExampleGene>();
            var twoPoint = new TwoPointCrossover();

            for (int i = 0; i < GATestHelper.GetRandomInteger(2, 8); i++)
            {
                var child = twoPoint.Execute(father, mother, settings);

                Assert.AreNotEqual(father.ToString(), child.ToString());
                Assert.AreNotEqual(mother.ToString(), child.ToString());
            }
        }
    }
}
