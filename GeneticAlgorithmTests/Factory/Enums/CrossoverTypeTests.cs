using System;
using Jarrus.GA.Factory.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jarrus.GATests.Factory.Enums
{
    [TestClass]
    public class CrossoverTypeTests
    {
        [TestMethod]
        public void ItOnlyContainsPositiveNumbers()
        {
            foreach (CrossoverStrategy type in Enum.GetValues(typeof(CrossoverStrategy)))
            {
                Assert.IsTrue((int) type > 0);
            }
        }

        [TestMethod]
        public void ItDoesNotContainAValueForZero()
        {
            foreach (CrossoverStrategy type in Enum.GetValues(typeof(CrossoverStrategy)))
            {
                Assert.IsTrue((int)type != 0);
            }
        }
    }
}
