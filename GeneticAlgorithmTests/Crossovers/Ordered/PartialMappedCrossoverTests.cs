using System.Collections.Generic;
using Jarrus.GA.Models;
using Jarrus.GA.Crossovers.Ordered;
using Jarrus.GATests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jarrus.GATests.Crossovers.Ordered
{
    [TestClass]
    public class PartialMappedCrossoverTests
    {
        [TestMethod]
        public void ItCanCreateAValidChild()
        {
            var one = GATestHelper.GetNumericChromosomeOne();
            var three = GATestHelper.GetNumericChromosomeThree();

            var pmx = new PartialMappedCrossover();
            var children = pmx.DetermineChildren(one, three, 2, 5);
            
            Assert.AreEqual("3,5,6,7,2,1,4", string.Join(",", (IEnumerable<Gene>)children[0].Genes));
            Assert.AreEqual("2,7,3,4,5,6,1", string.Join(",", (IEnumerable<Gene>)children[1].Genes));
        }
    }
}
