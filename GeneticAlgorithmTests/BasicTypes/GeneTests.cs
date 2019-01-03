using GeneticAlgorithmTests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GeneticAlgorithmTests.BasicTypes
{
    [TestClass]
    public class GeneTests
    {
        [TestMethod]
        public void ItCanDetermineIfGenesAreEqualBasedOnTheirValues()
        {
            var one = new ExampleGene('t');
            var two = new ExampleGene('t');

            Assert.AreEqual(one, two);
        }

        [TestMethod]
        public void ItCanDetermineIfGenesAreNotEqualBasedOnTheirValues()
        {
            var one = new ExampleGene('t');
            var two = new ExampleGene('T');

            Assert.AreNotEqual(one, two);
        }
    }
}
