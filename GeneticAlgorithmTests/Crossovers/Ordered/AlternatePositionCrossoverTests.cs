﻿using System.Collections.Generic;
using Jarrus.GA.Models;
using Jarrus.GA.Crossovers.Ordered;
using Jarrus.GATests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jarrus.GATests.Crossovers.Ordered
{
    [TestClass]
    public class AlternatePositionCrossoverTests
    {
        [TestMethod]
        public void ItCanPerformCrossover()
        {
            var one = GATestHelper.GetNumericChromosomeOne();
            var two = GATestHelper.GetNumericChromosomeTwo();

            var cc = new AlternatingPositionCrossover();
            var child = cc.Execute(one, two, GATestHelper.GetTravelingSalesmanDefaultConfiguration());
            
            Assert.AreEqual("1,5,2,4,3,6,7", string.Join(",", (IEnumerable<Gene>)child.Genes));
        }
    }
}
