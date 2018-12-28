using System;
using GeneticAlgorithms.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GeneticAlgorithmTests.Utility
{
    [TestClass]
    public class NameGeneratorTests
    {
        [TestMethod]
        public void ItCanGenerateAFirstName()
        {
            var firstName = NameGenerator.GetFirstName(new Random());
            Assert.IsNotNull(firstName);
        }

        [TestMethod]
        public void ItCanGenerateALastName()
        {
            var lastName = NameGenerator.GetLastName(new Random());
            Assert.IsNotNull(lastName);
        }

        [TestMethod]
        public void ItWillReturnNullIfRandomIsNullForFirstName()
        {
            var firstName = NameGenerator.GetFirstName(null);
            Assert.IsNull(firstName);
        }

        [TestMethod]
        public void ItWillReturnNullIfRandomIsNullForLastName()
        {
            var lastName = NameGenerator.GetLastName(null);
            Assert.IsNull(lastName);
        }
    }
}
