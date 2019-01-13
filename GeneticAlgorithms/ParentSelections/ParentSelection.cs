using Jarrus.GA.Factory.Enums;
using Jarrus.GA.Models;
using System;
using System.Linq;

namespace Jarrus.GA.ParentSelections
{
    public abstract class ParentSelection
    {
        protected GAConfiguration Configuration;
        protected Chromosome[] Population;

        public void Setup(Chromosome[] pop, GAConfiguration configuration)
        {
            Configuration = configuration;
            Population = pop;
            NormalizeAllFitnessScores();
            SetupSelection();
            Validate();
        }

        public void NormalizeAllFitnessScores()
        {
            foreach (var ch in Population) { ch.AdjustedFitnessScore = ch.FitnessScore; }
            AdjustFitnessScoresToBeInversed();
        }

        private void AdjustFitnessScoresToBeInversed()
        {
            if (Configuration.ScoringStrategy != ScoringStrategy.Lowest) { return; }

            var maxValue = Population.Max(o => o.AdjustedFitnessScore) + 1;

            foreach (var ch in Population)
            {
                ch.AdjustedFitnessScore = Math.Abs(maxValue - ch.AdjustedFitnessScore);
            }
        }

        protected abstract void SetupSelection();

        private void Validate()
        {
            if (Population.Min(o => o.FitnessScore) < 0)
            {
                throw new ArgumentException("Unable to work with Genomes with negative fitness scores.");
            }
        }


        public abstract ChromosomeParents GetParents();
    }
}
