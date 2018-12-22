using GeneticAlgorithms;
using GeneticAlgorithms.Crossovers;
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
            var father = new Chromosome<char>('A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M');
            var mother = new Chromosome<char>('Z', 'Y', 'X', 'W', 'V', 'U', 'T', 'S', 'R', 'Q', 'P', 'O', 'N');

            var settings = GATestHelper.GetDefaultSettings<char>();
            var twoPoint = new TwoPointCrossover();

            for (int i = 0; i < GATestHelper.GetRandomInteger(16, 32); i++)
            {
                var child = twoPoint.Execute(father, mother, settings);

                Assert.AreNotEqual(father.ToString(), child.ToString());
                Assert.AreNotEqual(mother.ToString(), child.ToString());
            }
        }
    }
}
