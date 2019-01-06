using System;
using GeneticAlgorithms.BasicTypes;
using GeneticAlgorithmTests.Models.FitnessCalculators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GeneticAlgorithmTests.BasicTypes
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
