using System;
using Jarrus.GA.Factory.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jarrus.GATests.Factory.Enums
{
    [TestClass]
    public class ParentSelectionTypeTests
    {
        [TestMethod]
        public void ItOnlyContainsPositiveNumbers()
        {
            foreach (ParentSelectionStrategy type in Enum.GetValues(typeof(ParentSelectionStrategy)))
            {
                Assert.IsTrue((int)type > 0);
            }
        }

        [TestMethod]
        public void ItDoesNotContainAValueForZero()
        {
            foreach (ParentSelectionStrategy type in Enum.GetValues(typeof(ParentSelectionStrategy)))
            {
                Assert.IsTrue((int)type != 0);
            }
        }
    }
}
