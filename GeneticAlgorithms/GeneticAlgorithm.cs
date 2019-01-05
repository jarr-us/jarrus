using GeneticAlgorithms.BasicTypes;
using GeneticAlgorithms.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GeneticAlgorithms
{
    public class GeneticAlgorithm
    {        
        public GAConfiguration Configuration { get; private set; }
        public int Generation { get; private set; }
        private Gene[] _possibleValues;
        public GARun GARun;

        public List<Chromosome> Retired = new List<Chromosome>();
        public Chromosome BestChromosomeEver { get; private set; }

        public GeneticAlgorithm(GAConfiguration configuration, params Gene[] possibleValues)
        {            
            Configuration = configuration;
            _possibleValues = possibleValues;
            ValidateSettings();

            GARun = new GARun();
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
            var randomPool = PopulationGenerator.Generate(_possibleValues, Configuration);
            GARun.Population = new Population(Configuration, randomPool, _possibleValues);
            GARun.BestChromosome = GARun.Population.Chromosomes[0];
        }

        public GARun Run()
        {
            GARun.Start = DateTime.UtcNow;

            for (GARun.CurrentGeneration = 0; GARun.CurrentGeneration < Configuration.MaxGenerations; GARun.CurrentGeneration++)
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

        private void DetermineBestChromosomeEver(Population genome) 
        {
            var highestScoringChromosome = GARun.Population.Chromosomes.Where(o => o.FitnessScore == GARun.Population.Chromosomes.Max(k => k.FitnessScore)).First();
            var lowestScoringChromosome = GARun.Population.Chromosomes.Where(o => o.FitnessScore == GARun.Population.Chromosomes.Min(k => k.FitnessScore)).First();

            if (!Configuration.LowestScoreIsBest && highestScoringChromosome.FitnessScore > GARun.BestChromosome.FitnessScore)
            {
                GARun.BestChromosome = highestScoringChromosome;
            }

            if (Configuration.LowestScoreIsBest && lowestScoringChromosome.FitnessScore < GARun.BestChromosome.FitnessScore)
            {
                GARun.BestChromosome = lowestScoringChromosome;
            }
        }
    }
}
