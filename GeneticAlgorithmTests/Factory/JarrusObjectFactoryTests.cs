using System;
using System.Collections.Generic;
using Jarrus.GA.Factory;
using Jarrus.GA.Factory.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jarrus.GATests.Factory
{
    [TestClass]
    public class JarrusObjectFactoryTests
    {
        [TestMethod]
        public void ItCanGetACrossover()
        {
            var order = JarrusObjectFactory.Instance.GetCrossover(CrossoverStrategy.Order);
            Assert.IsNotNull(order);
        }

        [TestMethod]
        public void ItCanGetAllCrossovers()
        {
            foreach (CrossoverStrategy type in Enum.GetValues(typeof(CrossoverStrategy)))
            {
                var crossover = JarrusObjectFactory.Instance.GetCrossover(type);
                Assert.IsNotNull(crossover);
            }
        }

        [TestMethod]
        public void ItDoesNotRepeatObjectsForEachCrossoverType()
        {
            var hashset = new HashSet<string>();

            foreach (CrossoverStrategy type in Enum.GetValues(typeof(CrossoverStrategy)))
            {
                var crossover = JarrusObjectFactory.Instance.GetCrossover(type);
                Assert.IsNotNull(crossover);
                hashset.Add(crossover.GetType().AssemblyQualifiedName);
            }

            Assert.AreEqual(hashset.Count, Enum.GetValues(typeof(CrossoverStrategy)).Length);
        }

        [TestMethod]
        public void ItCanGetAMutation()
        {
            var obj = JarrusObjectFactory.Instance.GetMutation(MutationStrategy.Swap);
            Assert.IsNotNull(obj);
        }

        [TestMethod]
        public void ItSetsTheMutationTypeWhenGettingAMutation()
        {
            var obj = JarrusObjectFactory.Instance.GetMutation(MutationStrategy.Swap);
            Assert.IsNotNull(obj);

            Assert.AreEqual(MutationStrategy.Swap, obj.MutationType);
        }

        [TestMethod]
        public void ItCanGetAllMutationsForOrderedTypes()
        {
            foreach (MutationStrategy type in Enum.GetValues(typeof(MutationStrategy)))
            {
                var obj = JarrusObjectFactory.Instance.GetMutation(type);
                Assert.IsNotNull(obj);
            }
        }

        [TestMethod]
        public void ItDoesNotRepeatObjectsForEachMutationType()
        {
            var hashset = new HashSet<string>();

            foreach (MutationStrategy type in Enum.GetValues(typeof(MutationStrategy)))
            {
                var obj = JarrusObjectFactory.Instance.GetMutation(type);
                Assert.IsNotNull(obj);
                hashset.Add(obj.GetType().AssemblyQualifiedName);
            }
        }

        [TestMethod]
        public void ItCanGetAParentSelection()
        {
            var obj = JarrusObjectFactory.Instance.GetParentSelection(ParentSelectionStrategy.RouletteWheel);
            Assert.IsNotNull(obj);
        }

        [TestMethod]
        public void ItCanGetAllParentSelections()
        {
            foreach (ParentSelectionStrategy type in Enum.GetValues(typeof(ParentSelectionStrategy)))
            {
                var obj = JarrusObjectFactory.Instance.GetParentSelection(type);
                Assert.IsNotNull(obj);
            }
        }

        [TestMethod]
        public void ItDoesNotRepeatObjectsForEachParentSelectionType()
        {
            var hashset = new HashSet<string>();

            foreach (ParentSelectionStrategy type in Enum.GetValues(typeof(ParentSelectionStrategy)))
            {
                var obj = JarrusObjectFactory.Instance.GetParentSelection(type);
                Assert.IsNotNull(obj);
                hashset.Add(obj.GetType().AssemblyQualifiedName);
            }

            Assert.AreEqual(hashset.Count, Enum.GetValues(typeof(ParentSelectionStrategy)).Length);
        }
    }
}
