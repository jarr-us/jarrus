using System;
using System.Collections.Generic;
using Jarrus.GA.Factory.Enums;
using Jarrus.GA.Models;
using Jarrus.GA.ParentSelections;
using Jarrus.GATests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jarrus.GATests.ParentSelections
{
    [TestClass]
    public class WheelSelectionTests
    {
        private RouletteWheelSelection _rankSelection;
        private GAConfiguration _config;
        private Random _random = new Random(22);

        [TestInitialize]
        public void BeforeEach()
        {
            _rankSelection = new RouletteWheelSelection();
            _config = GetConfiguration();
        }

        [TestMethod]
        public void ItCanSetupRankSelection()
        {
            _config.ParentSelectionStrategy = ParentSelectionStrategy.Rank;
            var rankings = GetRankingsForStep(1);
            ItSetsUpTheRankingsInOrder(rankings);

            _rankSelection.Setup(GetZeroesAndOnesChromosomes(), _config);
            ItSetsUpTheRankingsInOrder(_rankSelection.Rankings);
        }

        [TestMethod]
        public void ItCanSetupTheRankings()
        {
            _rankSelection.Setup(GetStepChromosomes(1), _config);
            Assert.AreEqual(100, _rankSelection.Rankings.Count);
        }

        [TestMethod]
        public void ItSetsUpTheRankingsInOrder_Step()
        {
            var rankings = GetRankingsForStep(1);
            ItSetsUpTheRankingsInOrder(rankings);
        }

        [TestMethod]
        public void ItSupportsZeroFitnessScores_Step()
        {
            _rankSelection.Setup(GetZeroesAndOnesChromosomes(), _config);
            ItCanSupportZeroFitnessScores(_rankSelection.Rankings);
        }

        [TestMethod]
        public void ItSetsUpTheRankingsInOrder_Equal()
        {
            var rankings = GetRankingsForEqual(1);
            ItSetsUpTheRankingsInOrder(rankings);
        }

        [TestMethod]
        public void ItSupportsZeroFitnessScores_Equal()
        {
            _rankSelection.Setup(GetZeroesAndOnesChromosomes(), _config);
            ItCanSupportZeroFitnessScores(_rankSelection.Rankings);
        }

        [TestMethod]
        public void ItSupportsLowestFitnessScoresBeingBest()
        {
            var rankings = GetRankingsForStep(1, ScoringStrategy.Lowest);
            ItSetsUpTheRankingsInOrder(rankings);
        }
        
        private void ItSetsUpTheRankingsInOrder(List<double> rankings)
        {
            Assert.AreEqual(100, rankings.Count);

            for (int i = 0; i < rankings.Count - 1; i++)
            {
                Assert.IsTrue(rankings[i] < rankings[i + 1]);
            }

            Assert.IsTrue(rankings[0] == 0);
            Assert.IsTrue(rankings[99] < 1);
        }
        
        private void ItCanSupportZeroFitnessScores(List<double> rankings)
        {
            Assert.AreEqual(100, rankings.Count);

            for (int i = 0; i < rankings.Count - 1; i++)
            {
                Assert.IsTrue(rankings[i] < rankings[i + 1]);
            }

            Assert.IsTrue(rankings[0] == 0);
            Assert.IsTrue(rankings[99] < 1);
        }

        [TestMethod]
        public void ItOffsetsZeroesAndTheRankingsRemainTheSame()
        {
            var noOffset = GetRankingsForStep(0);
            var offset = GetRankingsForStep(1);
            var offsetTwo = GetRankingsForStep(100);

            for(int i = 0; i < noOffset.Count; i++)
            {
                Assert.AreEqual(noOffset[0], offset[0]);
                Assert.AreEqual(offsetTwo[0], noOffset[0]);
                Assert.AreEqual(offsetTwo[0], offset[0]);
            }
        }

        private List<double> GetRankingsForStep(int offset = 0, ScoringStrategy scoringType = ScoringStrategy.Highest)
        {
            _config.ScoringStrategy = scoringType;
            _rankSelection.Setup(GetStepChromosomes(offset), _config);
            return _rankSelection.Rankings;
        }

        private List<double> GetRankingsForEqual(int offset = 0)
        {
            _config.ScoringStrategy = ScoringStrategy.Highest;
            _rankSelection.Setup(GetStepChromosomes(offset), _config);
            return _rankSelection.Rankings;
        }

        private Chromosome[] GetStepChromosomes(int offset = 0)
        {
            var chromosomes = new Chromosome[100];

            for(int i = 0; i < 100; i++)
            {
                chromosomes[i] = new OrderedChromosome(8);
                chromosomes[i].FitnessScore = i + offset;
            }

            return chromosomes;
        }

        private Chromosome[] GetEqualValueChromosomes(int offset = 0)
        {
            var chromosomes = new Chromosome[100];

            for (int i = 0; i < 100; i++)
            {
                chromosomes[i] = new OrderedChromosome(8);
                chromosomes[i].FitnessScore = 10 + offset;
            }

            return chromosomes;
        }

        private Chromosome[] GetZeroesAndOnesChromosomes()
        {
            var chromosomes = new Chromosome[100];

            for (int i = 0; i < 100; i++)
            {
                chromosomes[i] = new OrderedChromosome(8);
                chromosomes[i].FitnessScore = _random.Next(0, 2);
            }

            return chromosomes;
        }

        private GAConfiguration GetConfiguration() { return GATestHelper.GetPhraseConfiguration(); }
    }
}
