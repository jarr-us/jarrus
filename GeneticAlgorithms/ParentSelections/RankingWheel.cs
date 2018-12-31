using GeneticAlgorithms.BasicTypes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GeneticAlgorithms.ParentSelections
{
    public abstract class RankingWheel<T> : ParentSelection<T> where T : Gene
    {
        private List<double> rankings = new List<double>();

        protected override void SetupSelection() { GenerateRouletteWheel(); }

        protected void GenerateRouletteWheel()
        {
            rankings = new List<double>();
            if (Configuration.LowestScoreIsBest)
            {
                GenerateLowestScoreIsBestWheel();
            }
            else
            {
                GenerateHighestScoreIsBestWheel();
            }
        }

        private void GenerateHighestScoreIsBestWheel()
        {
            var sum = Genome.Sum(o => o.FitnessScore);
            var totalWheel = 0.0;

            foreach (var chromosome in Genome)
            {
                var pieceOfWheel = chromosome.FitnessScore / sum;
                totalWheel += pieceOfWheel;

                rankings.Add(totalWheel);
            }

            rankings[Genome.Length - 1] = 1;
        }

        private void GenerateLowestScoreIsBestWheel()
        {
            var maxFitness = Genome.Max(o => o.FitnessScore);
            var totalWheel = 0.0;

            var inversedTotalWheelSum = Genome.Sum(o => Math.Abs(o.FitnessScore - maxFitness));

            foreach (var chromosome in Genome)
            {
                var pieceOfWheel = Math.Abs(chromosome.FitnessScore - maxFitness) / inversedTotalWheelSum;
                totalWheel += pieceOfWheel;

                rankings.Add(totalWheel);
            }

            rankings[Genome.Length - 1] = 1;
        }

        public Chromosome<T> GetParent(double value)
        {
            for (int i = 1; i < Genome.Length; i++)
            {
                var rankingValue = rankings[i];

                if (value <= rankingValue)
                {
                    return Genome[i - 1];
                }
            }

            return Genome.Last();
        }
    }
}
