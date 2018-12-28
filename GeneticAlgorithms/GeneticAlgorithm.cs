using GeneticAlgorithms.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace GeneticAlgorithms
{
    public class GeneticAlgorithm<T>
    {
        public Population<T> Genome;
        public GAConfiguration<T> Configuration { get; private set; }
        public int Generation { get; private set; }
        private T[] _possibleValues;
        public GARun GARun;

        public List<Chromosome<T>> Retired = new List<Chromosome<T>>();
        public Chromosome<T> BestChromosomeEver { get; private set; }

        public GeneticAlgorithm(GAConfiguration<T> configuration, params T[] possibleValues)
        {            
            Configuration = configuration;
            _possibleValues = possibleValues;
            ValidateSettings();

            GARun = new GARun();
            GARun.SetValues(Configuration);

            GenerateInitialGenome();
        }

        private void ValidateSettings()
        {
            if (Configuration == null || _possibleValues == null || _possibleValues.Length <= 1)
            {
                throw new ArgumentException("Invalid parameters passed to the GeneticAlgorithm");
            }
        }

        private void GenerateInitialGenome()
        {
            var randomPool = PopulationGenerator.Generate<T>(_possibleValues, Configuration);
            Genome = new Population<T>(Configuration, randomPool, _possibleValues);
        }

        public GARun Run()
        {
            for (GARun.CurrentGeneration = 0; GARun.CurrentGeneration < Configuration.Iterations; GARun.CurrentGeneration++)
            {
                var nextGeneration = Genome.Advance();

                Retired = Genome.Retired.OrderBy(o => o.FitnessScore).ToList();

                DetermineBestChromosomeEver(Genome);
                Generation = nextGeneration.GenerationNumber;
                Genome = nextGeneration;
            }

            GARun.End = DateTime.UtcNow;
            return GARun;
        }

        private void DetermineBestChromosomeEver(Population<T> genome)
        {
            var highestScoringChromosome = Genome.Chromosomes.Where(o => o.FitnessScore == Genome.Chromosomes.Max(k => k.FitnessScore)).First();
            var lowestScoringChromosome = Genome.Chromosomes.Where(o => o.FitnessScore == Genome.Chromosomes.Min(k => k.FitnessScore)).First();

            if (highestScoringChromosome.FitnessScore > GARun.HighestScore || GARun.HighestScore == -1)
            {
                GARun.HighestScore = highestScoringChromosome.FitnessScore;
                GARun.HighestScoreGeneration = highestScoringChromosome.GenerationNumber;
            }

            if (lowestScoringChromosome.FitnessScore < GARun.LowestScore || GARun.LowestScore == -1)
            {
                GARun.LowestScore = lowestScoringChromosome.FitnessScore;
                GARun.LowestScoreGeneration = lowestScoringChromosome.GenerationNumber;
            }
        }
    }
}
