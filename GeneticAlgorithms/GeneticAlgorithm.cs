using GeneticAlgorithms.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GeneticAlgorithms
{
    public class GeneticAlgorithm<T>
    {
        public Genome<T> Genome;
        public GAConfiguration<T> Configuration { get; private set; }
        public int Generation { get; private set; }
        private T[] _possibleValues;
        private GARun _run;

        public List<Chromosome<T>> Retired = new List<Chromosome<T>>();
        public Chromosome<T> BestChromosomeEver { get; private set; }

        public GeneticAlgorithm(GAConfiguration<T> configuration, params T[] possibleValues)
        {            
            Configuration = configuration;
            _possibleValues = possibleValues;
            ValidateSettings();

            _run = new GARun();
            _run.SetValues(Configuration);

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
            var randomPool = GenomeGenerator.Generate<T>(_possibleValues, Configuration);
            Genome = new Genome<T>(Configuration, randomPool, _possibleValues);
        }

        public GARun Run()
        {
            for (int i = 0; i < Configuration.Iterations; i++)
            {
                var nextGeneration = Genome.Advance();
                Retired = Genome.Retired.OrderBy(o => o.FitnessScore).ToList();

                DetermineBestChromosomeEver(Genome);
                Generation = nextGeneration.GenerationNumber;
                Genome = nextGeneration;
            }

            _run.End = DateTime.UtcNow;
            return _run;
        }

        private void DetermineBestChromosomeEver(Genome<T> genome)
        {
            var highestScoringChromosome = Genome.Chromosomes.Where(o => o.FitnessScore == Genome.Chromosomes.Max(k => k.FitnessScore)).First();
            var lowestScoringChromosome = Genome.Chromosomes.Where(o => o.FitnessScore == Genome.Chromosomes.Min(k => k.FitnessScore)).First();

            if (highestScoringChromosome.FitnessScore > _run.HighestScore || _run.HighestScore == -1)
            {
                _run.HighestScore = highestScoringChromosome.FitnessScore;
                _run.HighestScoreGeneration = highestScoringChromosome.GenerationNumber;
            }

            if (lowestScoringChromosome.FitnessScore < _run.LowestScore || _run.LowestScore == -1)
            {
                _run.LowestScore = lowestScoringChromosome.FitnessScore;
                _run.LowestScoreGeneration = lowestScoringChromosome.GenerationNumber;
            }
        }
    }
}
