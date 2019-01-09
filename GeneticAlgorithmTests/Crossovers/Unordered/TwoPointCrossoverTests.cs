using Jarrus.GA.Crossovers.Unordered;
using Jarrus.GA.Utility;
using Jarrus.GATests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jarrus.GATests.Crossovers
{
    [TestClass]
    public class TwoPointCrossoverTests
    {
        [TestMethod]
        public void ItCanPerformACrossover()
        {
            var config = GATestHelper.GetTravelingSalesmanDefaultConfiguration();
            var father = GATestHelper.GetAlphabetCharacterChromosome();
            var mother = GATestHelper.GetAlphabetCharacterChromosome();
            mother.Genes.Shuffle(config.Random);

            var twoPoint = new TwoPointCrossover();

            var child = twoPoint.Execute(father, mother, config);

            Assert.AreNotEqual(father.ToString(), child.ToString());
            Assert.AreNotEqual(mother.ToString(), child.ToString());
        }
    }
}
