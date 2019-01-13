using System;
using Jarrus.GA.Factory.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jarrus.GATests.Factory.Enums
{
    [TestClass]
    public class MutationTypeTests
    {
        [TestMethod]
        public void ItOnlyContainsPositiveNumbers()
        {
            foreach (MutationStrategy type in Enum.GetValues(typeof(MutationStrategy)))
            {
                Assert.IsTrue((int)type > 0);
            }
        }

        [TestMethod]
        public void ItDoesNotContainAValueForZero()
        {
            foreach (MutationStrategy type in Enum.GetValues(typeof(MutationStrategy)))
            {
                Assert.IsTrue((int)type != 0);
            }
        }
    }
}
