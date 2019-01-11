using System;
using Jarrus.GA.Factory.Enums;
using Jarrus.GA.Models;
using Jarrus.GATests.Models;
using Jarrus.GATests.Models.FitnessCalculators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jarrus.GATests.Factory.Enums
{
    [TestClass]
    public class ImmigrationTypeTests
    {
        private GAConfiguration _config;
        private SimpleTravelingSalesmanSolution _solution;

        [TestInitialize]
        public void BeforeEach()
        {
            _config = GATestHelper.GetTravelingSalesmanDefaultConfiguration();
            _config.DuplicationType = DuplicationType.Allow;
            _solution = new SimpleTravelingSalesmanSolution();
        }

        [TestMethod]
        public void ItOnlyContainsPositiveNumbers()
        {
            foreach (ImmigrationType type in Enum.GetValues(typeof(ImmigrationType)))
            {
                Assert.IsTrue((int)type > 0);
            }
        }

        [TestMethod]
        public void ItDoesNotContainAValueForZero()
        {
            foreach (ImmigrationType type in Enum.GetValues(typeof(ImmigrationType)))
            {
                Assert.IsTrue((int)type != 0);
            }
        }

        [TestMethod]
        public void ItReturnsAValueWhenDynamicIsChosen()
        {
            _config.ImmigrationType = ImmigrationType.Dynamic;
            var gaRun = _solution.Run(_config);
            Assert.AreEqual(true, gaRun.BestChromosome.FitnessScore > 0);
        }

        [TestMethod]
        public void ItReturnsAValueWhenNoneIsChosen()
        {
            _config.ImmigrationType = ImmigrationType.None;
            var gaRun = _solution.Run(_config);
            Assert.AreEqual(true, gaRun.BestChromosome.FitnessScore > 0);
        }

        [TestMethod]
        public void ItReturnsAValueWhenConstantIsChosen()
        {
            _config.ImmigrationType = ImmigrationType.Constant;
            _config.ImmigrationRate = 0.01;
            var gaRun = _solution.Run(_config);
            Assert.AreEqual(true, gaRun.BestChromosome.FitnessScore > 0);
        }
    }
}
