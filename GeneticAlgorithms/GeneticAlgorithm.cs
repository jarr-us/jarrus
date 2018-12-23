﻿using GeneticAlgorithms.Utility;
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

        public List<Chromosome<T>> Retired = new List<Chromosome<T>>();
        public Chromosome<T> BestChromosomeEver { get; private set; }

        public GeneticAlgorithm(GAConfiguration<T> configuration, params T[] possibleValues)
        {            
            Configuration = configuration;
            _possibleValues = possibleValues;
            ValidateSettings();

            GenerateInitialGenome();
            Run();
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
            Genome = new Genome<T>(Configuration, randomPool);
        }

        private void Run()
        {
            for (int i = 0; i < Configuration.Iterations; i++)
            {
                var nextGeneration = Genome.Advance();
                Retired = Genome.Retired.OrderBy(o => o.FitnessScore).ToList();

                DetermineBestChromosomeEver(Genome);
                Generation = nextGeneration.GenerationNumber;
                Genome = nextGeneration;
            }
        }

        private void DetermineBestChromosomeEver(Genome<T> genome)
        {
            var chromosome = Genome.Chromosomes.Where(o => o.FitnessScore == Genome.Chromosomes.Max(k => k.FitnessScore)).First();
            if (Configuration.LowestScoreIsBest) { chromosome = Genome.Chromosomes.Where(o => o.FitnessScore == Genome.Chromosomes.Min(k => k.FitnessScore)).First(); }

            if (Configuration.LowestScoreIsBest && chromosome.FitnessScore != 0 && (BestChromosomeEver == null || chromosome.FitnessScore < BestChromosomeEver.FitnessScore)) { BestChromosomeEver = chromosome; }
            if (!Configuration.LowestScoreIsBest && (BestChromosomeEver == null || chromosome.FitnessScore > BestChromosomeEver.FitnessScore)) { BestChromosomeEver = chromosome; }
        }
    }
}
