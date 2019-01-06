using GeneticAlgorithms.BasicTypes;
using GeneticAlgorithms.Crossovers;
using GeneticAlgorithms.Factory;
using GeneticAlgorithms.FitnessFunctions;
using GeneticAlgorithms.Mutations;
using GeneticAlgorithms.ParentSelections;
using GeneticAlgorithms.Utility;
using System;
using System.Linq;

namespace GeneticAlgorithms
{
    public class GAConfiguration : GAProperties
    {
        internal Crossover Crossover;
        internal Mutation Mutation;
        internal ParentSelection ParentSelection;
        internal FitnessFunction FitnessFunction;

        public Random Random;
        public Random RandomPool;
        public Random RandomFirstNameSeed;
        public Random RandomLastNameSeed;

        public GAConfiguration(GATask task)
        {
            if (task == null) { throw new ArgumentException("Task can not be null"); }

            Reflection.CopyProperties(task, this);
            SetupStrategies(task);
            SetupRandomVariables();
            ValidateProperties();
        }

        private void SetupStrategies(GATask task)
        {
            Crossover = JarrusObjectFactory.Instance.GetCrossover(CrossoverType);
            Mutation = JarrusObjectFactory.Instance.GetMutation(MutationType);
            ParentSelection = JarrusObjectFactory.Instance.GetParentSelection(ParentSelectionType);
            FitnessFunction = task.Solution.GetFitnessFunction();
        }

        private void SetupRandomVariables()
        {
            var tempRandom = new Random();

            if (RandomSeed == 0)
            {
                RandomSeed = tempRandom.Next(0, int.MaxValue - 1);
                Random = new Random(RandomSeed);
            }

            if (RandomPoolGenerationSeed == 0)
            {
                RandomPoolGenerationSeed = tempRandom.Next(0, int.MaxValue - 1);
                RandomPool = new Random(RandomPoolGenerationSeed);
            }

            Random = new Random(RandomSeed);
            RandomPool = new Random(RandomPoolGenerationSeed);

            RandomFirstNameSeed = new Random(RandomSeed);
            RandomLastNameSeed = new Random(RandomPoolGenerationSeed);
    }

        public void ValidateProperties()
        {
            if (!IsValid() || ChildrenPerCouple == 0)
            {
                throw new ArgumentException("Invalid settings passed to GAConfiguration");
            }

            if (MutationRate < 0 || MutationRate > 1 || ElitismRate < 0 || ElitismRate > 1 || CrossoverRate < 0 || CrossoverRate > 1)
            {
                throw new ArgumentException("GAConfiguration rates must be between 0 and 1");
            }
        }

        public GARun Run() { return Solution.Run(this); }

        public int GetRandomInteger(int min, int max)
        {
            max++;
            return Random.Next(min, max);
        }

        public int GetRandomInteger(int min, int max, params int[] excluding)
        {
            var randomValue = GetRandomInteger(min, max);

            while (excluding.Contains(randomValue))
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
        public bool IsValid() {
            var solutionsValid = Solution != null && Mutation != null && Crossover != null && ParentSelection != null;

            return solutionsValid && MaxPopulationSize > 0;
        }
    }
}
