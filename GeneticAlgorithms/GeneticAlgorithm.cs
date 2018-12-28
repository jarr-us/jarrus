using GeneticAlgorithms.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GeneticAlgorithms
{
    public class GeneticAlgorithm<T>
    {       
        public GAConfiguration<T> Configuration { get; private set; }
        public int Generation { get; private set; }
        private T[] _possibleValues;
        public GARun<T> GARun;

        public List<Chromosome<T>> Retired = new List<Chromosome<T>>();
        public Chromosome<T> BestChromosomeEver { get; private set; }

        public GeneticAlgorithm(GAConfiguration<T> configuration, params T[] possibleValues)
        {            
            Configuration = configuration;
            _possibleValues = possibleValues;
            ValidateSettings();

            GARun = new GARun<T>();
            GARun.SetValues(Configuration);

            GenerateInitialPopulation();
        }

        private void ValidateSettings()
        {
            if (Configuration == null || _possibleValues == null || _possibleValues.Length <= 1)
            {
                throw new ArgumentException("Invalid parameters passed to the GeneticAlgorithm");
            }
        }

        private void GenerateInitialPopulation()
        {
            var randomPool = PopulationGenerator.Generate<T>(_possibleValues, Configuration);
            GARun.Population = new Population<T>(Configuration, randomPool, _possibleValues);
            GARun.HighestChromosome = GARun.Population.Chromosomes[0];
            GARun.LowestChromosome = GARun.Population.Chromosomes[0];
        }

        public GARun<T> Run()
        {
            for (GARun.CurrentGeneration = 0; GARun.CurrentGeneration < Configuration.Iterations; GARun.CurrentGeneration++)
            {
                var nextGeneration = GARun.Population.Advance();

                Retired = GARun.Population.Retired.OrderBy(o => o.FitnessScore).ToList();

                DetermineBestChromosomeEver(GARun.Population);
                Generation = nextGeneration.GenerationNumber;
                GARun.Population = nextGeneration;
            }

            GARun.End = DateTime.UtcNow;
            return GARun;
        }

        private void DetermineBestChromosomeEver(Population<T> genome)
        {
            var highestScoringChromosome = GARun.Population.Chromosomes.Where(o => o.FitnessScore == GARun.Population.Chromosomes.Max(k => k.FitnessScore)).First();
            var lowestScoringChromosome = GARun.Population.Chromosomes.Where(o => o.FitnessScore == GARun.Population.Chromosomes.Min(k => k.FitnessScore)).First();

            if (highestScoringChromosome.FitnessScore > GARun.HighestChromosome.FitnessScore)
            {
                GARun.HighestChromosome = highestScoringChromosome;
            }

            if (lowestScoringChromosome.FitnessScore < GARun.LowestChromosome.FitnessScore)
            {
                GARun.LowestChromosome = lowestScoringChromosome;
            }
        }
    }
}
