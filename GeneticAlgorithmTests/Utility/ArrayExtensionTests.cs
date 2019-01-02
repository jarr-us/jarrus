using System;
using GeneticAlgorithms.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GeneticAlgorithmTests.Utility
{
    [TestClass]
    public class ArrayExtensionTests
    {
        [TestMethod]
        public void ItCanTakeASubArray()
        {
            var allInts = new int[100];

            for (var i = 0; i < 100; i++)
            {
                allInts[i] = i;
            }

            var subarray = allInts.Subset(5, 5);
            Assert.AreEqual(5, subarray[0]);
            Assert.AreEqual(6, subarray[1]);
            Assert.AreEqual(7, subarray[2]);
            Assert.AreEqual(8, subarray[3]);
            Assert.AreEqual(9, subarray[4]);

            Assert.AreEqual(5, subarray.Length);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ItThrowsAnErrorErrorIfTheSubArrayIsNotInRange()
        {
            var allInts = new int[100];

            for (var i = 0; i < 100; i++)
            {
                allInts[i] = i;
            }

            var subarray = allInts.Subset(-1, 5);
        }

        [TestMethod]
        public void ItCanShuffleASubset()
        {
            var allInts = new int[10];

            for (var i = 0; i < 10; i++)
            {
                allInts[i] = i;
            }

            allInts = allInts.ShuffleSubset(1, 8, new Random());
            Assert.AreEqual(0, allInts[0]);
            Assert.AreEqual(8, allInts[8]);
            Assert.AreEqual(9, allInts[9]);

            Assert.AreEqual(10, allInts.Length);
            Console.Out.WriteLine(GetString(allInts));
            Assert.AreNotEqual("0123456789", GetString(allInts));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ItThrowsAnErrorIfTheShuffleSubsetIsNotInRange()
        {
            var allInts = new int[10];

            for (var i = 0; i < 10; i++)
            {
                allInts[i] = i;
            }

            allInts = allInts.ShuffleSubset(-1, 8, new Random());
        }

        [TestMethod]
        public void ItCanShuffle()
        {
            var allInts = new int[100];

            for (var i = 0; i < 100; i++)
            {
                allInts[i] = i;
            }

            allInts.Shuffle(new Random());

            var isTheSame = 0;
            for (var i = 0; i < 100; i++)
            {
                if (allInts[i] == i)
                {
                    isTheSame++;
                }
            }

            Assert.IsFalse(isTheSame == allInts.Length);
        }

        [TestMethod]
        public void ItCanShuffleAnEmptyArray()
        {
            var allInts = new int[0];
            allInts.Shuffle(new Random());
        }

        private string GetString(int[] ints)
        {
            var str = "";

            foreach (var i in ints)
            {
                str += i;
            }

            return str;
        }
    }
}
