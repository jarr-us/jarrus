using Jarrus.GA;
using Jarrus.GA.BasicTypes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Jarrus.Metadata
{
    public class PoolScoreGenerator
    {
        private Population _population;
        private List<double> _scores;
        public double MinScore, MaxScore;
        private double _bucketSize;
        public List<KeyValuePair<double, double>> Points = new List<KeyValuePair<double, double>>();
        public const int NUMBER_OF_BUCKETS = 35;

        public PoolScoreGenerator(Population pop, double minScore, double maxScore)
        {
            _population = pop;
            MinScore = minScore;
            MaxScore = maxScore;

            DetermineScores();
            DeterminePoints();
        }

        private void DetermineScores()
        {
            _scores = _population.Chromosomes.Select(o => o.FitnessScore).ToList();

            var min = _scores.Min();
            var max = _scores.Max();

            if (min < MinScore) { MinScore = min; }
            if (max > MaxScore) { MaxScore = max; }

            _bucketSize = (MaxScore - MinScore) / NUMBER_OF_BUCKETS;
        }

        private void DeterminePoints()
        {
            for(int i = 0; i < NUMBER_OF_BUCKETS; i++)
            {
                var minScoreToBeInBucket = (MinScore) + (i * _bucketSize);
                var maxScoreToBeInBucket = (MinScore) + ((i + 1) * _bucketSize);

                var kvp = new KeyValuePair<double, double>
                (
                    (int) minScoreToBeInBucket,
                    _scores.Where(o => o >= minScoreToBeInBucket && o < maxScoreToBeInBucket ).Count()                    
                );

                Points.Add(kvp);
            }
        }
    }
}
