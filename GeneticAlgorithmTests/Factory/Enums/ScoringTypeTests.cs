using System;
using Jarrus.GA.Factory.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jarrus.GATests.Factory.Enums
{
    [TestClass]
    public class ScoringTypeTests
    {
        [TestMethod]
        public void ItOnlyContainsPositiveNumbers()
        {
            foreach (ScoringStrategy type in Enum.GetValues(typeof(ScoringStrategy)))
            {
                Assert.IsTrue((int)type > 0);
            }
        }

        [TestMethod]
        public void ItDoesNotContainAValueForZero()
        {
            foreach (ScoringStrategy type in Enum.GetValues(typeof(ScoringStrategy)))
            {
                Assert.IsTrue((int)type != 0);
            }
        }
    }
}
