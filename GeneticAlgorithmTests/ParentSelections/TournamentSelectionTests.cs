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
    public class TournamentSelectionTests
    {
        private TournamentSelection tournament;
        private GAConfiguration _config;
        private Random _random = new Random(22);

        [TestInitialize]
        public void BeforeEach()
        {
            _config = GetConfiguration();            
        }

        [TestMethod]
        public void ItCanChooseParentsForTournamentTwo()
        {
            tournament = new TournamentTwoSelection();
            tournament.Setup(GetStepChromosomes(), _config);

            var parents = tournament.GetParents();
            Assert.IsNotNull(parents);
        }

        [TestMethod]
        public void ItCanChooseParentsForTournamentThree()
        {
            tournament = new TournamentThreeSelection();
            tournament.Setup(GetStepChromosomes(), _config);

            var parents = tournament.GetParents();
            Assert.IsNotNull(parents);
        }

        [TestMethod]
        public void ItCanChooseParentsForTournamentFour()
        {
            tournament = new TournamentFourSelection();
            tournament.Setup(GetStepChromosomes(), _config);

            var parents = tournament.GetParents();
            Assert.IsNotNull(parents);
        }

        [TestMethod]
        public void ItCanChooseParentsForTournamentFive()
        {
            tournament = new TournamentFiveSelection();
            tournament.Setup(GetStepChromosomes(), _config);

            var parents = tournament.GetParents();
            Assert.IsNotNull(parents);
        }

        private GAConfiguration GetConfiguration() { return GATestHelper.GetPhraseConfiguration(); }

        private Chromosome[] GetStepChromosomes(int offset = 0)
        {
            var chromosomes = new Chromosome[100];

            for (int i = 0; i < 100; i++)
            {
                chromosomes[i] = new OrderedChromosome(8);
                chromosomes[i].FitnessScore = i + offset;
            }

            return chromosomes;
        }
    }
}
