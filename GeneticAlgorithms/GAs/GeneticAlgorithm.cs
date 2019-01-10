using Jarrus.GA.BasicTypes.Genes;
using Jarrus.GA.Factory.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Jarrus.GA
{
    public abstract class GeneticAlgorithm
    {
        public GAConfiguration Configuration;
        public int Generation;
        public Gene[] PossibleValues;
        public GARun GARun;

        public Type GeneType;
        public List<Chromosome> Retired = new List<Chromosome>();
        public Chromosome BestChromosomeEver;

        protected void StandardConstructorLogic()
        {
            ValidateConfiguration();
            ValidateGeneSize();
            Configuration.Validate();

            GARun = new GARun();
            GenerateInitialPopulation();
        }

        private void ValidateGeneSize()
        {
            if (Configuration.GeneSize <= 1) { throw new ArgumentException("Gene size must be set or larger that 2"); }
        }
        
        protected abstract void ValidateConfiguration();
        protected abstract void GenerateInitialPopulation();
        
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

            if (Configuration.ScoringType == ScoringType.Highest && highestScoringChromosome.FitnessScore > GARun.BestChromosome.FitnessScore)
            {
                GARun.BestChromosome = highestScoringChromosome;
            }

            if (Configuration.ScoringType == ScoringType.Lowest && lowestScoringChromosome.FitnessScore < GARun.BestChromosome.FitnessScore)
            {
                GARun.BestChromosome = lowestScoringChromosome;
            }
        }
    }
}
