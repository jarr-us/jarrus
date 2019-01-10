using System;
using Jarrus.GA.Factory.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jarrus.GATests.Factory.Enums
{
    [TestClass]
    public class DuplicationTypeTests
    {
        [TestMethod]
        public void ItOnlyContainsPositiveNumbers()
        {
            foreach (DuplicationType type in Enum.GetValues(typeof(DuplicationType)))
            {
                Assert.IsTrue((int)type > 0);
            }
        }

        [TestMethod]
        public void ItDoesNotContainAValueForZero()
        {
            foreach (DuplicationType type in Enum.GetValues(typeof(DuplicationType)))
            {
                Assert.IsTrue((int)type != 0);
            }
        }
    }
}
