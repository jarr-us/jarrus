using System;
using GeneticAlgorithms;
using GeneticAlgorithmTests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GeneticAlgorithmTests
{
    [TestClass]
    public class ChromosomeReferenceTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            var data = GetSampleDataset();

            var chromosomeOne = new Chromosome<ExampleClass>(data);

            var tempdata = data[0];
            data[0] = data[1];
            data[1] = tempdata;

            var chromosomeTwo = new Chromosome<ExampleClass>(data);

            var areEqual = chromosomeOne.Genes[0] == chromosomeTwo.Genes[1];

            Assert.AreEqual(chromosomeOne.Genes[0], chromosomeTwo.Genes[1]);
            Console.Out.WriteLine("");

        }

        private ExampleClass[] GetSampleDataset()
        {
            var array = new ExampleClass[2];

            array[0] = new ExampleClass("Test");
            array[1] = new ExampleClass("Test2");

            return array;
        }
    }
}
