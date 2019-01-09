﻿using System;
using Jarrus.GA.Enums;
using Jarrus.GA.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jarrus.GATests.Utility
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
        public void ItWillReturnAadhyaIfRandomIsNullForFirstName()
        {
            var firstName = NameGenerator.GetFirstName(null);
            Assert.AreEqual(firstName, FirstName.Aadhya);
        }

        [TestMethod]
        public void ItWillReturnAbbottIfRandomIsNullForLastName()
        {
            var lastName = NameGenerator.GetLastName(null);
            Assert.AreEqual(lastName, LastName.Abbott);
        }
    }
}
