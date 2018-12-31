using GeneticAlgorithms;
using GeneticAlgorithms.BasicTypes;
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

        public static Chromosome<ExampleGene> GetTravelingSalesmanChromosome()
        {
            return new Chromosome<ExampleGene>(
                new ExampleGene('A'), 
                new ExampleGene('B'), 
                new ExampleGene('C'), 
                new ExampleGene('D')
            );
        }

        public static Chromosome<ExampleGene>[] GetTravelingSalesmanGenome()
        {
            return GetTravelingSalesmanGenome(GetDefaultConfiguration<ExampleGene>());
        }

        public static Chromosome<ExampleGene>[] GetTravelingSalesmanGenome(GAConfiguration<ExampleGene> config)
        {
            var chromosome = GetTravelingSalesmanChromosome();
            return PopulationGenerator.Generate(chromosome.Genes, config);
        }

        public static Chromosome<ExampleGene> GetAlphabetCharacterChromosome()
        {
            return new Chromosome<ExampleGene>(
                new ExampleGene('A'),
                new ExampleGene('B'),
                new ExampleGene('C'),
                new ExampleGene('D'),
                new ExampleGene('E'),
                new ExampleGene('F'),
                new ExampleGene('G'),
                new ExampleGene('H'),
                new ExampleGene('I'),
                new ExampleGene('J')
            );
        }

        public static GAConfiguration<T> GetDefaultConfiguration<T>() where T : Gene
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
