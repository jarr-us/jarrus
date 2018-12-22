﻿using GeneticAlgorithms.Crossovers;
using GeneticAlgorithms.FitnessCalculators;
using GeneticAlgorithms.Mutations;
using GeneticAlgorithms.ParentSelections;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GeneticAlgorithms
{
    public class GAConfiguration<T>
    {
        public ParentSelection<T> ParentSelection;
        public FitnessCalculator FitnessCalculator;
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

        public GAConfiguration(ParentSelection<T> parentSelection, FitnessCalculator calculator, Mutation mutation, Crossover crossover,
            bool lowestScoreIsBest = false, int poolSize = 100, int iterations = 50, double crossoverRate = 0.83,
            double mutationRate = 0.03, double elitismRate = 0.02, bool preventDuplicationInPool = false, int maximumLifeSpan = 0, int childrenPerCouple = 4, int randomSeed = 0)
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
                Random = new Random(randomSeed);
            }

            ValidateInputs();
        }

        private void ValidateInputs()
        {
            if (ParentSelection == null || Crossover == null || FitnessCalculator == null || Mutation == null || ChildrenPerCouple == 0)
            {
                throw new ArgumentException("Invalid settings passed to GASettings");
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

        public bool GetBool(double probabilityOfTrue = 50)
        {
            return Random.NextDouble() < probabilityOfTrue / 100.0;
        }

        public double GetNextDouble() { return Random.NextDouble(); }
    }
}
