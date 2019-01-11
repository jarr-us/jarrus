using System;
using Jarrus.GA;
using Jarrus.GA.Models;
using Jarrus.GA.ParentSelections;
using Jarrus.GATests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jarrus.GATests.ParentSelections
{
    [TestClass]
    public class StochasticUniversalSamplingSelectionTests
    {
        private Chromosome[] _pool;

        [TestInitialize]
        public void Setup()
        {
            _pool = GATestHelper.GetTravelingSalesmanPopulation();
        }

        [TestMethod]
        public void ItHasAValidConstructor()
        {
            var parentSelection = new StochasticUniversalSamplingSelection();
            parentSelection.Setup(_pool, GATestHelper.GetTravelingSalesmanDefaultConfiguration());
            parentSelection.GetParents();
        }

        [TestMethod]
        public void ItCanGetAValidParent()
        {
            var parentSelection = new StochasticUniversalSamplingSelection();
            parentSelection.Setup(_pool, GATestHelper.GetTravelingSalesmanDefaultConfiguration());

            var parent = parentSelection.GetParent(0.22);
            Assert.IsNotNull(parent);
        }

        [TestMethod]
        public void ItCanReturnParents()
        {
            var parentSelection = new StochasticUniversalSamplingSelection();
            parentSelection.Setup(_pool, GATestHelper.GetTravelingSalesmanDefaultConfiguration());

            var parent = parentSelection.GetParents();
            Assert.IsNotNull(parent);
            Assert.IsNotNull(parent.Father);
            Assert.IsNotNull(parent.Mother);
        }
    }
}
