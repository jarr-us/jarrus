using GeneticAlgorithms;
using GeneticAlgorithms.BasicTypes;
using GeneticAlgorithms.Crossovers;
using GeneticAlgorithms.Crossovers.Ordered;
using GeneticAlgorithms.Crossovers.Unordered;
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

        public static double GetNextDouble()
        {
            return _random.NextDouble();
        }

        public static Chromosome GetTravelingSalesmanChromosome()
        {
            return new Chromosome(
                new ExampleGene('A'), 
                new ExampleGene('B'), 
                new ExampleGene('C'), 
                new ExampleGene('D')
            );
        }

        public static Chromosome GetNumericChromosomeOne()
        {
            return new Chromosome(
                new ExampleGene('1'),
                new ExampleGene('2'),
                new ExampleGene('3'),
                new ExampleGene('4'),
                new ExampleGene('5'),
                new ExampleGene('6'),
                new ExampleGene('7')
            );
        }

        public static Chromosome GetNumericChromosomeTwo()
        {
            return new Chromosome(
                new ExampleGene('5'),
                new ExampleGene('4'),
                new ExampleGene('6'),
                new ExampleGene('7'),
                new ExampleGene('2'),
                new ExampleGene('3'),
                new ExampleGene('1')
            );
        }

        public static Chromosome GetNumericChromosomeThree()
        {
            return new Chromosome(
                new ExampleGene('5'),
                new ExampleGene('4'),
                new ExampleGene('6'),
                new ExampleGene('7'),
                new ExampleGene('2'),
                new ExampleGene('1'),
                new ExampleGene('3')
            );
        }

        public static Chromosome[] GetTravelingSalesmanGenome()
        {
            return GetTravelingSalesmanGenome(GetDefaultConfiguration());
        }

        public static Chromosome[] GetTravelingSalesmanGenome(GAConfiguration config)
        {
            var chromosome = GetTravelingSalesmanChromosome();
            return PopulationGenerator.Generate(chromosome.Genes, config);
        }

        public static Chromosome GetAlphabetCharacterChromosome()
        {
            return new Chromosome(
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

        public static GAConfiguration GetDefaultConfiguration() 
        {
            return new GAConfiguration( GetDummyTask());
        }

        public static GATask GetDummyTask() 
        {
            var task = new GATask
            {
                ParentSelection = new RouletteWheelSelection(),
                FitnessFunction = new TravelingSalesmanFitnessCalculator(),
                Mutation = new SwapMutation(),
                Crossover = new OrderCrossover(),
                MaximumLifeSpan = 10,
                MaxPopulationSize = 100,
                RandomPoolGenerationSeed = 22,
                RandomSeed = 13,
                ChildrenPerCouple = 2,
                Session = "Test",
                LowestScoreIsBest = true,
                CrossoverRate = 0.4321,
                MutationRate = 0.1234,
                ElitismRate = 0.123,
                PreventDuplications = false,
                MaxGenerations = 2
            };

            return task;
        }
    }
}
