using System;
using GeneticAlgorithms;
using GeneticAlgorithmTests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GeneticAlgorithmTests.BasicTypes
{
    [TestClass]
    public class GARunTests
    {
        [TestMethod]
        public void ItHasAValidConstructor()
        {
            var run = new GARun<ExampleGene>();
        }
    }
}
