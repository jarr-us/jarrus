using GeneticAlgorithms.Crossovers;
using GeneticAlgorithms.FitnessFunctions;
using GeneticAlgorithms.Mutations;
using GeneticAlgorithms.ParentSelections;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GeneticAlgorithms
{
    public class GAConfiguration<T>
    {
        private const int FIRST_NAME_SEED = 22;
        private const int LAST_NAME_SEED = 35;

        public ParentSelection<T> ParentSelection;
        public FitnessFunction FitnessCalculator;
        public Crossover Crossover;
        public Mutation Mutation;
        public bool LowestScoreIsBest;
        public int PoolSize;
        public int Iterations;
        public double CrossoverRate;
        public double ElitismRate;
        public double MutationRate;
        public bool PreventDuplicationInPool;
        public int MaximumLifeSpan;
        public int ChildrenPerCouple;
        public List<T> Chromosome = new List<T>();
        public int RandomSeed;
        public Random Random;
        public int RandomPoolGenerationSeed;
        public Random RandomPool;
        public Random RandomFirstNameSeed = new Random(FIRST_NAME_SEED);
        public Random RandomLastNameSeed = new Random(LAST_NAME_SEED);

        public GAConfiguration(ParentSelection<T> parentSelection, FitnessFunction calculator, Mutation mutation, Crossover crossover,
            bool lowestScoreIsBest = false, int poolSize = 100, int iterations = 50, double crossoverRate = 0.83,
            double mutationRate = 0.03, double elitismRate = 0.02, bool preventDuplicationInPool = false, int maximumLifeSpan = 0,
            int childrenPerCouple = 4, int randomSeed = 0, int randomPoolGenerationSeed = 0)
        {
            ParentSelection = parentSelection;
            Crossover = crossover;
            FitnessCalculator = calculator;
            Mutation = mutation;

            LowestScoreIsBest = lowestScoreIsBest;
            PoolSize = poolSize;
            Iterations = iterations;
            CrossoverRate = crossoverRate;
            MutationRate = mutationRate;
            ElitismRate = elitismRate;
            PreventDuplicationInPool = preventDuplicationInPool;
            MaximumLifeSpan = maximumLifeSpan;
            ChildrenPerCouple = childrenPerCouple;

            if (randomSeed == 0)
            {
                Random = new Random();
                RandomSeed = Random.Next(0, int.MaxValue - 1);
                Random = new Random(RandomSeed);
            }
            else
            {
                RandomSeed = randomSeed;
                Random = new Random(RandomSeed);
            }

            if (randomPoolGenerationSeed == 0)
            {
                RandomPool = new Random();
                RandomPoolGenerationSeed = RandomPool.Next(0, int.MaxValue - 1);
                RandomPool = new Random(RandomPoolGenerationSeed);
            }
            else
            {
                RandomPoolGenerationSeed = randomPoolGenerationSeed;
                RandomPool = new Random(RandomPoolGenerationSeed);
            }

            ValidateProperties();
        }

        public void ValidateProperties()
        {
            if (ParentSelection == null || Crossover == null || FitnessCalculator == null || Mutation == null || ChildrenPerCouple == 0)
            {
                throw new ArgumentException("Invalid settings passed to GAConfiguration");
            }

            if (MutationRate < 0 || MutationRate > 1 || ElitismRate < 0 || ElitismRate > 1 || CrossoverRate < 0 || CrossoverRate > 1)
            {
                throw new ArgumentException("GAConfiguration rates must be between 0 and 1");
            }
        }

        public int GetRandomInteger(int min, int max)
        {
            max++;
            return Random.Next(min, max);
        }

        public int GetRandomInteger(int min, int max, params int[] excluding)
        {
            var randomValue = GetRandomInteger(min, max);
            var excludingList = excluding.ToList();

            while (excludingList.Contains(randomValue))
            {
                randomValue = GetRandomInteger(min, max);
            }

            return randomValue;
        }

        public bool GetRandomBoolean(double probabilityOfTrue = 50)
        {
            return Random.NextDouble() < probabilityOfTrue / 100.0;
        }

        public double GetNextDouble() { return Random.NextDouble(); }
    }
}
