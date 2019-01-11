using System;
using Jarrus.GA.Models;
using Jarrus.GATests.Models.FitnessCalculators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jarrus.GATests.BasicTypes
{
    [TestClass]
    public class GATaskTests
    {
        [TestMethod]
        public void ItHasAValidConstructor()
        {
            var gaTask = new GATask(new SimpleTravelingSalesmanSolution());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItRequiresAValidSolution()
        {
            var gaTask = new GATask(null);
        }
    }
}
