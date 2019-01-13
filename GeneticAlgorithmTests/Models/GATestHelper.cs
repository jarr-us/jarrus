using Jarrus.GA.Models;
using Jarrus.GA.Factory.Enums;
using Jarrus.GA.Solution;
using Jarrus.GA.Utility;
using Jarrus.GATests.Models.FitnessCalculators;
using System;

namespace Jarrus.GATests.Models
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
            return new OrderedChromosome(
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
            return new OrderedChromosome(
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
            return new OrderedChromosome(
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
            return new OrderedChromosome(_solution.GetOptions());
    }

        public static Chromosome[] GetTravelingSalesmanPopulation()
        {
            return GetTravelingSalesmanPopulation(GetTravelingSalesmanDefaultConfiguration());
        }

        public static Chromosome[] GetTravelingSalesmanPopulation(GAConfiguration config)
        {
            var chromosome = GetTravelingSalesmanChromosome();
            return PopulationGenerator.GenerateOrderedPopulation(config, chromosome.Genes);
        }

        public static Chromosome GetAlphabetCharacterChromosome()
        {
            return new OrderedChromosome(
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

        public static GAConfiguration GetDefaultConfiguration(JarrusOrderedSolution solution) 
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

        public static GATask GetDummyTask(JarrusOrderedSolution solution) 
        {
            var task = new GATask(solution)
            {
                ParentSelectionStrategy = ParentSelectionStrategy.RouletteWheel,
                MutationStrategy = MutationStrategy.Swap,
                CrossoverStrategy = CrossoverStrategy.Order,
                RetirementStrategy = RetirementStrategy.MaxAge,
                ImmigrationStrategy = ImmigrationStrategy.Dynamic,
                DuplicationStrategy = DuplicationStrategy.Allow,

                MaxRetirement = 10,
                PopulationSize = 100,
                RandomPoolGenerationSeed = 22,
                RandomSeed = 13,
                ChildrenPerParents = 2,
                Session = "Test",
                ScoringStrategy = ScoringStrategy.Lowest,
                CrossoverRate = 0.4321,
                MutationRate = 0.1234,
                ElitismRate = 0.123,
                MaxGenerations = 2
            };

            return task;
        }

        public static GAConfiguration GetPhraseConfiguration()
        {
            return new GAConfiguration(GetDummyUnorderedTask(new PhraseSolution()));
        }

        public static GATask GetDummyUnorderedTask(JarrusUnorderedSolution solution)
        {
            var task = new GATask(solution)
            {
                ParentSelectionStrategy = ParentSelectionStrategy.RouletteWheel,
                MutationStrategy = MutationStrategy.Random,
                CrossoverStrategy = CrossoverStrategy.TwoPoint,
                RetirementStrategy = RetirementStrategy.MaxAge,
                ImmigrationStrategy = ImmigrationStrategy.Dynamic,
                DuplicationStrategy = DuplicationStrategy.Allow,
                ScoringStrategy = ScoringStrategy.Lowest,

                MaxRetirement = 10,
                PopulationSize = 100,
                RandomPoolGenerationSeed = 22,
                RandomSeed = 13,
                ChildrenPerParents = 2,
                Session = "Test",
                CrossoverRate = 0.95,
                MutationRate = 0.12,
                ElitismRate = 0.22,
                MaxGenerations = 20
            };

            return task;
        }
    }
}
