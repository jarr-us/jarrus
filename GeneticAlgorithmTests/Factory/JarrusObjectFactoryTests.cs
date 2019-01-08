using System;
using System.Collections.Generic;
using GeneticAlgorithms.Factory;
using GeneticAlgorithms.Factory.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GeneticAlgorithmTests.Factory
{
    [TestClass]
    public class JarrusObjectFactoryTests
    {
        [TestMethod]
        public void ItCanGetACrossover()
        {
            var order = JarrusObjectFactory.Instance.GetCrossover(CrossoverType.Order);
            Assert.IsNotNull(order);
        }

        [TestMethod]
        public void ItCanGetAllCrossovers()
        {
            foreach (CrossoverType type in Enum.GetValues(typeof(CrossoverType)))
            {
                var crossover = JarrusObjectFactory.Instance.GetCrossover(type);
                Assert.IsNotNull(crossover);
            }
        }

        [TestMethod]
        public void ItDoesNotRepeatObjectsForEachCrossoverType()
        {
            var hashset = new HashSet<string>();

            foreach (CrossoverType type in Enum.GetValues(typeof(CrossoverType)))
            {
                var crossover = JarrusObjectFactory.Instance.GetCrossover(type);
                Assert.IsNotNull(crossover);
                hashset.Add(crossover.GetType().AssemblyQualifiedName);
            }

            Assert.AreEqual(hashset.Count, Enum.GetValues(typeof(CrossoverType)).Length);
        }

        [TestMethod]
        public void ItCanGetAMutation()
        {
            var obj = JarrusObjectFactory.Instance.GetMutation(MutationType.Swap);
            Assert.IsNotNull(obj);
        }

        [TestMethod]
        public void ItSetsTheMutationTypeWhenGettingAMutation()
        {
            var obj = JarrusObjectFactory.Instance.GetMutation(MutationType.Swap);
            Assert.IsNotNull(obj);

            Assert.AreEqual(MutationType.Swap, obj.MutationType);
        }

        [TestMethod]
        public void ItCanGetAllMutationsForOrderedTypes()
        {
            foreach (MutationType type in Enum.GetValues(typeof(MutationType)))
            {
                var obj = JarrusObjectFactory.Instance.GetMutation(type);
                Assert.IsNotNull(obj);
            }
        }

        [TestMethod]
        public void ItDoesNotRepeatObjectsForEachMutationType()
        {
            var hashset = new HashSet<string>();

            foreach (MutationType type in Enum.GetValues(typeof(MutationType)))
            {
                var obj = JarrusObjectFactory.Instance.GetMutation(type);
                Assert.IsNotNull(obj);
                hashset.Add(obj.GetType().AssemblyQualifiedName);
            }
        }

        [TestMethod]
        public void ItCanGetAParentSelection()
        {
            var obj = JarrusObjectFactory.Instance.GetParentSelection(ParentSelectionType.RouletteWheel);
            Assert.IsNotNull(obj);
        }

        [TestMethod]
        public void ItCanGetAllParentSelections()
        {
            foreach (ParentSelectionType type in Enum.GetValues(typeof(ParentSelectionType)))
            {
                var obj = JarrusObjectFactory.Instance.GetParentSelection(type);
                Assert.IsNotNull(obj);
            }
        }

        [TestMethod]
        public void ItDoesNotRepeatObjectsForEachParentSelectionType()
        {
            var hashset = new HashSet<string>();

            foreach (ParentSelectionType type in Enum.GetValues(typeof(ParentSelectionType)))
            {
                var obj = JarrusObjectFactory.Instance.GetParentSelection(type);
                Assert.IsNotNull(obj);
                hashset.Add(obj.GetType().AssemblyQualifiedName);
            }

            Assert.AreEqual(hashset.Count, Enum.GetValues(typeof(ParentSelectionType)).Length);
        }
    }
}
