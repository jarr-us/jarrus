using Jarrus.GA.Enums;
using Jarrus.GATests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jarrus.GATests
{
    [TestClass]
    public class GeneticAlgorithmUnorderedTests
    {
        [TestMethod]
        public void ItCanRunAnUnorderedGA()
        {
            var phraseConfig = GATestHelper.GetPhraseConfiguration();
            var phraseSolution = new PhraseSolution();

            var run = phraseSolution.Run(phraseConfig);
            Assert.AreNotEqual(0, run.CurrentGeneration);
            Assert.AreNotEqual(FirstName.Kanan, run.BestChromosome.FirstName);
            Assert.AreNotEqual(LastName.Jarrus, run.BestChromosome.LastName);
        }
    }
}
