using Jarrus.GA.Models;
using Jarrus.GA.ParentSelections;
using Jarrus.GATests.Models;
using Jarrus.GATests.Models.FitnessCalculators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jarrus.GATests.ParentSelections
{
    [TestClass]
    public class RouletteWheelSelectionTests
    {
        private Chromosome[] _pool;
        
        [TestInitialize]
        public void Setup()
        {
            _pool = GATestHelper.GetTravelingSalesmanPopulation();
            var calc = new SimpleTravelingSalesmanSolution();
            foreach (var chromosome in _pool)
            {
                chromosome.FitnessScore = calc.GetFitnessScoreFor(chromosome);
            }
        }

        [TestMethod]
        public void ItHasValidTestData()
        {           
            Assert.AreNotEqual(0, _pool.Length);

            foreach (var chromosome in _pool)
            {
                Assert.AreNotEqual(0, chromosome.FitnessScore);
            }
        }

        [TestMethod]
        public void ItHasAValidConstructor()
        {
            var parentSelection = new RouletteWheelSelection();
            parentSelection.Setup(_pool, GATestHelper.GetTravelingSalesmanDefaultConfiguration());
            parentSelection.GetParents();
        }

        [TestMethod]
        public void ItCanGetAValidParent()
        {
            var parentSelection = new RouletteWheelSelection();
            parentSelection.Setup(_pool, GATestHelper.GetTravelingSalesmanDefaultConfiguration());

            var parent = parentSelection.GetParent(0.22);
            Assert.IsNotNull(parent);
        }

        [TestMethod]
        public void ItCanReturnParents()
        {
            var parentSelection = new RouletteWheelSelection();
            parentSelection.Setup(_pool, GATestHelper.GetTravelingSalesmanDefaultConfiguration());

            var parent = parentSelection.GetParents();
            Assert.IsNotNull(parent);
            Assert.IsNotNull(parent.Father);
            Assert.IsNotNull(parent.Mother);
        }

        [TestMethod]
        public void ItCantReturnTheSameParentTwice()
        {
            var parentSelection = new RouletteWheelSelection();
            parentSelection.Setup(_pool, GATestHelper.GetTravelingSalesmanDefaultConfiguration());

            for (int i = 0; i < 1000; i++)
            {
                var parent = parentSelection.GetParents();
                Assert.IsFalse(ReferenceEquals(parent.Father, parent.Mother));
            }
        }
    }
}
