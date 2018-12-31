using System;
using GeneticAlgorithms.BasicTypes;
using GeneticAlgorithms.Utility;
using GeneticAlgorithmTests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GeneticAlgorithmTests.Utility
{
    [TestClass]
    public class GeneUtilitiesTests
    {
        private TestHelper _helper = new TestHelper();

        [TestMethod]
        public void ItCanTakeASubArray()
        {
            var genes = _helper.GetChromosome().Genes;

            var subarray = (ExampleGene[]) genes.Subset(5, 5);
            Assert.AreEqual('F', subarray[0].Value);
            Assert.AreEqual('G', subarray[1].Value);
            Assert.AreEqual('H', subarray[2].Value);
            Assert.AreEqual('I', subarray[3].Value);
            Assert.AreEqual('J', subarray[4].Value);

            Assert.AreEqual(5, subarray.Length);
        }

        //[TestMethod]
        //[ExpectedException(typeof(ArgumentOutOfRangeException))]
        //public void ItThrowsAnErrorErrorIfTheSubArrayIsNotInRange()
        //{
        //    var allInts = new int[100];

        //    for (var i = 0; i < 100; i++)
        //    {
        //        allInts[i] = i;
        //    }

        //    var subarray = allInts.Subset(-1, 5);
        //}

        //[TestMethod]
        //public void ItCanShuffleASubset()
        //{
        //    var allInts = new int[10];

        //    for (var i = 0; i < 10; i++)
        //    {
        //        allInts[i] = i;
        //    }

        //    allInts = allInts.ShuffleSubset(1, 8);
        //    Assert.AreEqual(0, allInts[0]);
        //    Assert.AreEqual(8, allInts[8]);
        //    Assert.AreEqual(9, allInts[9]);

        //    Assert.AreEqual(10, allInts.Length);
        //    Console.Out.WriteLine(GetString(allInts));
        //    Assert.AreNotEqual("0123456789", GetString(allInts));
        //}

        //[TestMethod]
        //[ExpectedException(typeof(ArgumentOutOfRangeException))]
        //public void ItThrowsAnErrorIfTheShuffleSubsetIsNotInRange()
        //{
        //    var allInts = new int[10];

        //    for (var i = 0; i < 10; i++)
        //    {
        //        allInts[i] = i;
        //    }

        //    allInts = allInts.ShuffleSubset(-1, 8);
        //}

        //[TestMethod]
        //public void ItCanShuffle()
        //{
        //    var allInts = new int[100];

        //    for (var i = 0; i < 100; i++)
        //    {
        //        allInts[i] = i;
        //    }

        //    allInts.Shuffle();

        //    var isTheSame = 0;
        //    for (var i = 0; i < 100; i++)
        //    {
        //        if (allInts[i] == i)
        //        {
        //            isTheSame++;
        //        }
        //    }

        //    Assert.IsFalse(isTheSame == allInts.Length);
        //}

        //[TestMethod]
        //public void ItCanShuffleAnEmptyArray()
        //{
        //    var allInts = new int[0];
        //    allInts.Shuffle();
        //}

        //private string GetString(int[] ints)
        //{
        //    var str = "";

        //    foreach (var i in ints)
        //    {
        //        str += i;
        //    }

        //    return str;
        //}
    }
}
