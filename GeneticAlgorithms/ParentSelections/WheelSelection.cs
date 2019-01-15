using Jarrus.GA.Models;
using Jarrus.GA.Factory.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Jarrus.GA.ParentSelections
{
    public abstract class WheelSelection : ParentSelection
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
            AdjustFitnessScoresToAllBeAboveOne();
            AdjustFitnessScoreToBeRanked();
        }

        private void AdjustFitnessScoresToAllBeAboveOne()
        {
            if (Configuration.ParentSelectionStrategy == ParentSelectionStrategy.Rank) { return; }
            var minValue = Population.Min(o => o.AdjustedFitnessScore);
            if (minValue == 0) { minValue = -1; }
            if (minValue > 0) { return; }

            foreach (var ch in Population)
            {
                ch.AdjustedFitnessScore += Math.Abs(minValue) + 1;
            }
        }

        private void AdjustFitnessScoreToBeRanked()
        {
            if (Configuration.ParentSelectionStrategy != ParentSelectionStrategy.Rank) { return; }

            var length = Population.Length;
            Population = Population.OrderByDescending(o => o.AdjustedFitnessScore).ToArray();

            for (int i = 0; i < Population.Length; i++)
            {
                Population[i].AdjustedFitnessScore = length - i;
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
            int index = -1;
            int middle;
            int first = 0;
            int last = Population.Length - 1;
            middle = (last - first) / 2;

            while (index == -1 && first <= last)
            {
                if (value < Rankings[middle]) { last = middle; }
                else if (value > Rankings[middle]) { first = middle; }
                middle = (first + last) / 2;

                if ((last - first) == 1) { index = last; }
            }

            if (Rankings[index] >= value)
            {
                return Population[index - 1];
            }

            return Population[index];
        }
    }
}
