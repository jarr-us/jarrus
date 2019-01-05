using GeneticAlgorithms.BasicTypes;
using GeneticAlgorithms.Utility;
using System;
using System.Linq;

namespace GeneticAlgorithms
{
    public class GAConfiguration : GATask
    {
        private const int FIRST_NAME_SEED = 22;
        private const int LAST_NAME_SEED = 35;
                
        public Random Random;
        public Random RandomPool;
        public Random RandomFirstNameSeed = new Random(FIRST_NAME_SEED);
        public Random RandomLastNameSeed = new Random(LAST_NAME_SEED);

        public GAConfiguration(GATask task)
        {
            Reflection.CopyProperties(task, this);
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

            ValidateProperties();
        }

        public void ValidateProperties()
        {
            if (ParentSelection == null || Crossover == null || FitnessFunction == null || Mutation == null || ChildrenPerCouple == 0)
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
