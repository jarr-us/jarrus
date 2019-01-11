using System;
using Jarrus.GA.Factory.Enums;
using Jarrus.GA.Models;
using Jarrus.GATests.Models;
using Jarrus.GATests.Models.FitnessCalculators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jarrus.GATests.Factory.Enums
{
    [TestClass]
    public class RetirementTypeTests
    {
        private GAConfiguration _config;
        private SimpleTravelingSalesmanSolution _solution;

        [TestInitialize]
        public void BeforeEach()
        {
            _config = GATestHelper.GetTravelingSalesmanDefaultConfiguration();            
            _solution = new SimpleTravelingSalesmanSolution();
        }

        [TestMethod]
        public void ItOnlyContainsPositiveNumbers()
        {
            foreach (RetirementType type in Enum.GetValues(typeof(RetirementType)))
            {
                Assert.IsTrue((int)type > 0);
            }
        }

        [TestMethod]
        public void ItDoesNotContainAValueForZero()
        {
            foreach (RetirementType type in Enum.GetValues(typeof(RetirementType)))
            {
                Assert.IsTrue((int)type != 0);
            }
        }

        [TestMethod]
        public void ItRetiresPeopleByAge()
        {
            _config.RetirementType = RetirementType.MaxAge;
            _config.MaxRetirement = 2;

            var gaRun = _solution.Run(_config);
            Assert.AreEqual(true, gaRun.BestChromosome.FitnessScore > 0);

            Assert.AreNotEqual(0, gaRun.Population.Retired.Count);

            foreach(var retiredPerson in gaRun.Population.Retired)
            {
                Assert.AreEqual(_config.MaxRetirement, retiredPerson.Age);
            }
        }

        [TestMethod]
        public void ItCanNotRetirePeople()
        {
            _config.RetirementType = RetirementType.None;

            var gaRun = _solution.Run(_config);
            Assert.AreEqual(true, gaRun.BestChromosome.FitnessScore > 0);

            Assert.AreEqual(0, gaRun.Population.Retired.Count);
        }

        [TestMethod]
        public void ItRetiresPeopleByChildrenSired()
        {
            _config.RetirementType = RetirementType.MaxChildren;
            _config.MaxRetirement = 2;

            var gaRun = _solution.Run(_config);
            Assert.AreEqual(true, gaRun.BestChromosome.FitnessScore > 0);

            Assert.AreNotEqual(0, gaRun.Population.Retired.Count);

            foreach (var retiredPerson in gaRun.Population.Retired)
            {
                Assert.AreEqual(_config.MaxRetirement, retiredPerson.Age);
            }
        }
    }
}
