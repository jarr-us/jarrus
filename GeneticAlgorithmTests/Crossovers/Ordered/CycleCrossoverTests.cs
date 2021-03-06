﻿using System.Collections.Generic;
using Jarrus.GA.Models;
using Jarrus.GA.Crossovers.Ordered;
using Jarrus.GATests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jarrus.GATests.Crossovers.Ordered
{
    [TestClass]
    public class CycleCrossoverTests
    {
        [TestMethod]
        public void ItCanDetermineTheCycle()
        {
            var one = GATestHelper.GetNumericChromosomeOne();
            var two = GATestHelper.GetNumericChromosomeTwo();

            var cc = new CycleCrossover();
            cc.Execute(one, two, GATestHelper.GetTravelingSalesmanDefaultConfiguration());

            Assert.AreEqual("0,4,1,3,6", string.Join(",", cc.Cycle));
        }

        [TestMethod]
        public void ItCanPerformCrossover()
        {
            var one = GATestHelper.GetNumericChromosomeOne();
            var two = GATestHelper.GetNumericChromosomeTwo();

            var cc = new CycleCrossover();
            var child = cc.Execute(one, two, GATestHelper.GetTravelingSalesmanDefaultConfiguration());

            Assert.AreEqual("1,2,6,4,5,3,7", string.Join(",", (IEnumerable<Gene>)child.Genes));
        }
    }
}
