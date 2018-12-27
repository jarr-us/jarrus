using GeneticAlgorithms;
using GeneticAlgorithms.ParentSelections;
using GeneticAlgorithmTests.Models;
using GeneticAlgorithmTests.Models.FitnessFunctions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GeneticAlgorithmTests.ParentSelections
{
    [TestClass]
    public class RouletteWheelSelectionTests
    {
        private Chromosome<char>[] _pool;

        [TestInitialize]
        public void Setup()
        {
            _pool = GATestHelper.GetTravelingSalesmanGenome();
            var calc = new TravelingSalesmanFitnessCalculator();
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
            var parentSelection = new RouletteWheelSelection<char>();
            parentSelection.Setup(_pool, GATestHelper.GetDefaultConfiguration<char>());
            parentSelection.GetParents();
        }

        [TestMethod]
        public void ItCanGetAValidParent()
        {
            var parentSelection = new RouletteWheelSelection<char>();
            parentSelection.Setup(_pool, GATestHelper.GetDefaultConfiguration<char>());

            var parent = parentSelection.GetParent(0.22);
            Assert.IsNotNull(parent);
        }

        [TestMethod]
        public void ItCanReturnParents()
        {
            var parentSelection = new RouletteWheelSelection<char>();
            parentSelection.Setup(_pool, GATestHelper.GetDefaultConfiguration<char>());

            var parent = parentSelection.GetParents();
            Assert.IsNotNull(parent);
            Assert.IsNotNull(parent.Father);
            Assert.IsNotNull(parent.Mother);
        }

        [TestMethod]
        public void ItCantReturnTheSameParentTwice()
        {
            var parentSelection = new RouletteWheelSelection<char>();
            parentSelection.Setup(_pool, GATestHelper.GetDefaultConfiguration<char>());

            for (int i = 0; i < 1000; i++)
            {
                var parent = parentSelection.GetParents();
                Assert.AreNotEqual(parent.Father, parent.Mother);
            }
        }
    }
}
