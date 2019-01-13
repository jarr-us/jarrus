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
            foreach (RetirementStrategy type in Enum.GetValues(typeof(RetirementStrategy)))
            {
                Assert.IsTrue((int)type > 0);
            }
        }

        [TestMethod]
        public void ItDoesNotContainAValueForZero()
        {
            foreach (RetirementStrategy type in Enum.GetValues(typeof(RetirementStrategy)))
            {
                Assert.IsTrue((int)type != 0);
            }
        }

        [TestMethod]
        public void ItRetiresPeopleByAge()
        {
            _config.RetirementStrategy = RetirementStrategy.MaxAge;
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
        public void ItCanDetermineIfAChromosomeItShouldRetireByAge()
        {
            _config.RetirementStrategy = RetirementStrategy.MaxAge;
            var chromosome = new OrderedChromosome(8);
            chromosome.Age = 10;

            Assert.AreEqual(true, chromosome.ShouldRetire(_config));
        }

        [TestMethod]
        public void ItCanDetermineIfAChromosomeItShouldRetireByChildrenSired()
        {
            _config.RetirementStrategy = RetirementStrategy.MaxChildren;
            _config.MaxRetirement = 9;
            var chromosome = new OrderedChromosome(8);
            chromosome.Age = 0;
            chromosome.Children = 100;

            Assert.AreEqual(true, chromosome.ShouldRetire(_config));
        }

        [TestMethod]
        public void ItCanNotRetirePeople()
        {
            _config.RetirementStrategy = RetirementStrategy.None;

            var gaRun = _solution.Run(_config);
            Assert.AreEqual(true, gaRun.BestChromosome.FitnessScore > 0);

            Assert.AreEqual(0, gaRun.Population.Retired.Count);
        }

        [TestMethod]
        public void ItRetiresPeopleByChildrenSired()
        {
            _config.RetirementStrategy = RetirementStrategy.MaxChildren;
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
