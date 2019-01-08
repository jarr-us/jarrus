using GeneticAlgorithms;
using GeneticAlgorithms.BasicTypes;
using GeneticAlgorithms.Crossovers;
using GeneticAlgorithms.Crossovers.Ordered;
using GeneticAlgorithms.Crossovers.Unordered;
using GeneticAlgorithms.Factory.Enums;
using GeneticAlgorithms.Mutations;
using GeneticAlgorithms.ParentSelections;
using GeneticAlgorithms.Solution;
using GeneticAlgorithms.Utility;
using GeneticAlgorithmTests.Models.FitnessCalculators;
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

        public static Chromosome GetNumericChromosomeOne()
        {
            return new Chromosome(
                new TravelingSalesmanGene('1'),
                new TravelingSalesmanGene('2'),
                new TravelingSalesmanGene('3'),
                new TravelingSalesmanGene('4'),
                new TravelingSalesmanGene('5'),
                new TravelingSalesmanGene('6'),
                new TravelingSalesmanGene('7')
            );
        }

        public static Chromosome GetNumericChromosomeTwo()
        {
            return new Chromosome(
                new TravelingSalesmanGene('5'),
                new TravelingSalesmanGene('4'),
                new TravelingSalesmanGene('6'),
                new TravelingSalesmanGene('7'),
                new TravelingSalesmanGene('2'),
                new TravelingSalesmanGene('3'),
                new TravelingSalesmanGene('1')
            );
        }

        public static Chromosome GetNumericChromosomeThree()
        {
            return new Chromosome(
                new TravelingSalesmanGene('5'),
                new TravelingSalesmanGene('4'),
                new TravelingSalesmanGene('6'),
                new TravelingSalesmanGene('7'),
                new TravelingSalesmanGene('2'),
                new TravelingSalesmanGene('1'),
                new TravelingSalesmanGene('3')
            );
        }

        public static Chromosome GetTravelingSalesmanChromosome()
        {
            var _solution = new SimpleTravelingSalesmanSolution();
            return new Chromosome(_solution.GetOptions());
    }

        public static Chromosome[] GetTravelingSalesmanGenome()
        {
            return GetTravelingSalesmanGenome(GetTravelingSalesmanDefaultConfiguration());
        }

        public static Chromosome[] GetTravelingSalesmanGenome(GAConfiguration config)
        {
            var chromosome = GetTravelingSalesmanChromosome();
            return PopulationGenerator.Generate(chromosome.Genes, config);
        }

        public static Chromosome GetAlphabetCharacterChromosome()
        {
            return new Chromosome(
                new TravelingSalesmanGene('A'),
                new TravelingSalesmanGene('B'),
                new TravelingSalesmanGene('C'),
                new TravelingSalesmanGene('D'),
                new TravelingSalesmanGene('E'),
                new TravelingSalesmanGene('F'),
                new TravelingSalesmanGene('G'),
                new TravelingSalesmanGene('H'),
                new TravelingSalesmanGene('I'),
                new TravelingSalesmanGene('J')
            );
        }

        public static GAConfiguration GetDefaultConfiguration(JarrusSolution solution) 
        {
            return new GAConfiguration( GetDummyTask(solution));
        }

        public static GAConfiguration GetTravelingSalesmanDefaultConfiguration()
        {
            return new GAConfiguration(GetDummyTask(new SimpleTravelingSalesmanSolution()));
        }

        public static GATask GetDummyTravelingSalesmanTask()
        {
            return GetDummyTask(new SimpleTravelingSalesmanSolution());
        }

        public static GATask GetDummyTask(JarrusSolution solution) 
        {
            var task = new GATask(solution)
            {
                ParentSelectionType = ParentSelectionType.RouletteWheel,
                MutationType = MutationType.Swap,
                CrossoverType = CrossoverType.Order,
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
