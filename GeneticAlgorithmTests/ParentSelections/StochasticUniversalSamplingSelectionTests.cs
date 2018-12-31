using System;
using GeneticAlgorithms;
using GeneticAlgorithms.ParentSelections;
using GeneticAlgorithmTests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GeneticAlgorithmTests.ParentSelections
{
    [TestClass]
    public class StochasticUniversalSamplingSelectionTests
    {
        private Chromosome<ExampleGene>[] _pool;

        [TestInitialize]
        public void Setup()
        {
            _pool = GATestHelper.GetTravelingSalesmanGenome();
        }

        [TestMethod]
        public void ItHasAValidConstructor()
        {
            var parentSelection = new StochasticUniversalSamplingSelection<ExampleGene>();
            parentSelection.Setup(_pool, GATestHelper.GetDefaultConfiguration<ExampleGene>());
            parentSelection.GetParents();
        }

        [TestMethod]
        public void ItCanGetAValidParent()
        {
            var parentSelection = new StochasticUniversalSamplingSelection<ExampleGene>();
            parentSelection.Setup(_pool, GATestHelper.GetDefaultConfiguration<ExampleGene>());

            var parent = parentSelection.GetParent(0.22);
            Assert.IsNotNull(parent);
        }

        [TestMethod]
        public void ItCanReturnParents()
        {
            var parentSelection = new StochasticUniversalSamplingSelection<ExampleGene>();
            parentSelection.Setup(_pool, GATestHelper.GetDefaultConfiguration<ExampleGene>());

            var parent = parentSelection.GetParents();
            Assert.IsNotNull(parent);
            Assert.IsNotNull(parent.Father);
            Assert.IsNotNull(parent.Mother);
        }
    }
}
