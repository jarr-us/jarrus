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
            _config.DuplicationStrategy = DuplicationStrategy.Allow;
            _solution = new SimpleTravelingSalesmanSolution();
        }

        [TestMethod]
        public void ItOnlyContainsPositiveNumbers()
        {
            foreach (ImmigrationStrategy type in Enum.GetValues(typeof(ImmigrationStrategy)))
            {
                Assert.IsTrue((int)type > 0);
            }
        }

        [TestMethod]
        public void ItDoesNotContainAValueForZero()
        {
            foreach (ImmigrationStrategy type in Enum.GetValues(typeof(ImmigrationStrategy)))
            {
                Assert.IsTrue((int)type != 0);
            }
        }

        [TestMethod]
        public void ItReturnsAValueWhenDynamicIsChosen()
        {
            _config.ImmigrationStrategy = ImmigrationStrategy.Dynamic;
            var gaRun = _solution.Run(_config);
            Assert.AreEqual(true, gaRun.BestChromosome.FitnessScore > 0);
        }

        [TestMethod]
        public void ItReturnsAValueWhenNoneIsChosen()
        {
            _config.ImmigrationStrategy = ImmigrationStrategy.None;
            var gaRun = _solution.Run(_config);
            Assert.AreEqual(true, gaRun.BestChromosome.FitnessScore > 0);
        }

        [TestMethod]
        public void ItReturnsAValueWhenConstantIsChosen()
        {
            _config.ImmigrationStrategy = ImmigrationStrategy.Constant;
            _config.ImmigrationRate = 0.01;
            var gaRun = _solution.Run(_config);
            Assert.AreEqual(true, gaRun.BestChromosome.FitnessScore > 0);
        }
    }
}
