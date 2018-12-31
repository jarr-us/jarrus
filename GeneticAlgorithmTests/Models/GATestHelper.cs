using GeneticAlgorithms;
using GeneticAlgorithms.Crossovers;
using GeneticAlgorithms.Crossovers.Ordered;
using GeneticAlgorithms.Mutations;
using GeneticAlgorithms.ParentSelections;
using GeneticAlgorithms.Utility;
using GeneticAlgorithmTests.Models.FitnessFunctions;
using System;

namespace GeneticAlgorithmTests.Models
{
    public class GATestHelper
    {
        private static Random _random = new Random();

        public static int GetRandomInteger(int min, int max)
        {
            max++;
            return _random.Next(min, max);
        }

        public static Chromosome<char> GetTravelingSalesmanChromosome()
        {
            return new Chromosome<char>('A', 'B', 'C', 'D');
        }

        public static Chromosome<char>[] GetTravelingSalesmanGenome()
        {
            return GetTravelingSalesmanGenome(GetDefaultConfiguration<char>());
        }

        public static Chromosome<char>[] GetTravelingSalesmanGenome(GAConfiguration<char> config)
        {
            var chromosome = GetTravelingSalesmanChromosome();
            return PopulationGenerator.Generate<char>(chromosome.Genes, config);
        }

        public static Chromosome<char> GetAlphabetCharacterChromosome()
        {
            return new Chromosome<char>('A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J');
        }

        public static GAConfiguration<T> GetDefaultConfiguration<T>()
        {
            return new GAConfiguration<T>(
                new RouletteWheelSelection<T>(),
                new TravelingSalesmanFitnessCalculator(),
                new SwapMutation(),
                new OrderCrossover(),
                maximumLifeSpan: 10,
                poolSize: 100
            );
        }
    }
}
