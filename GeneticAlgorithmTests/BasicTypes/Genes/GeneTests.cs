using Jarrus.GATests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jarrus.GATests.BasicTypes
{
    [TestClass]
    public class GeneTests
    {
        [TestMethod]
        public void ItCanDetermineIfGenesAreEqualBasedOnTheirValues()
        {
            var one = new TravelingSalesmanGene('t');
            var two = new TravelingSalesmanGene('t');

            Assert.AreEqual(one, two);
        }

        [TestMethod]
        public void ItCanDetermineIfGenesAreNotEqualBasedOnTheirValues()
        {
            var one = new TravelingSalesmanGene('t');
            var two = new TravelingSalesmanGene('T');

            Assert.AreNotEqual(one, two);
        }
    }
}
