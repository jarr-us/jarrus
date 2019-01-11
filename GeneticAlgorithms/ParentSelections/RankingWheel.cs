using Jarrus.GA.Models;
using Jarrus.GA.Factory.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Jarrus.GA.ParentSelections
{
    public abstract class RankingWheel : ParentSelection
    {
        public List<double> Rankings = new List<double>();

        protected override void SetupSelection() { GenerateRouletteWheel(); }

        protected void GenerateRouletteWheel()
        {
            Rankings = new List<double>();            
            DetermineAdjustedFitnessScores();
            GenerateWheel();
        }

        private void DetermineAdjustedFitnessScores()
        {
            foreach(var ch in Population) { ch.AdjustedFitnessScore = ch.FitnessScore; }
            AdjustFitnessScoresToAllBeAboveOne();
            AdjustFitnessScoresToBeInversed();
        }

        private void AdjustFitnessScoresToAllBeAboveOne()
        {
            var minValue = Population.Min(o => o.AdjustedFitnessScore);
            if (minValue == 0) { minValue = -1; }
            if (minValue > 0) { return; }
            
            foreach(var ch in Population)
            {
                ch.AdjustedFitnessScore += Math.Abs(minValue);
            }
        }

        private void AdjustFitnessScoresToBeInversed()
        {
            if (Configuration.ScoringType != ScoringType.Lowest) { return; }

            var maxValue = Population.Max(o => o.AdjustedFitnessScore) + 1;

            foreach (var ch in Population)
            {
                ch.AdjustedFitnessScore = Math.Abs(maxValue - ch.AdjustedFitnessScore);
            }
        }

        private void GenerateWheel()
        {
            var sum = Population.Sum(o => o.AdjustedFitnessScore);
            var totalWheel = 0.0;
            var nextValue = 0.0;

            foreach (var chromosome in Population)
            {
                Rankings.Add(totalWheel);
                nextValue = chromosome.AdjustedFitnessScore / sum;
                totalWheel += nextValue;
            }
        }

        public Chromosome GetParent(double value)
        {
            for (int i = 1; i < Population.Length; i++)
            {
                var rankingValue = Rankings[i];

                if (value <= rankingValue)
                {
                    return Population[i - 1];
                }
            }

            return Population.Last();
        }
    }
}
