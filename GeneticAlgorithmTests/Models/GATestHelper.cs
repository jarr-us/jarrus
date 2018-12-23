using GeneticAlgorithms;
using GeneticAlgorithms.Crossovers;
using GeneticAlgorithms.Mutations;
using GeneticAlgorithms.ParentSelections;
using GeneticAlgorithms.Utility;
using GeneticAlgorithmTests.Models.FitnessCalculators;
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
            var chromosome = GetTravelingSalesmanChromosome();
            return GenomeGenerator.Generate<char>(chromosome.Genes, GetDefaultSettings<char>());
        }

        public static Chromosome<char> GetAlphabetCharacterChromosome()
        {
            return new Chromosome<char>('A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J');
        }

        public static GAConfiguration<T> GetDefaultSettings<T>()
        {
            return new GAConfiguration<T>(
                new RouletteWheelSelection<T>(),
                new TravelingSalesmanFitnessCalculator(),
                new SwapMutation(),
                new OrderedCrossover(),
                maximumLifeSpan: 10
            );
        }
    }
}
