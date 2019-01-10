using Jarrus.GA.BasicTypes;
using Jarrus.GA.Crossovers;
using Jarrus.GA.Factory;
using Jarrus.GA.FitnessFunctions;
using Jarrus.GA.Mutations;
using Jarrus.GA.ParentSelections;
using Jarrus.GA.Solution;
using Jarrus.GA.Utility;
using System;
using System.Linq;

namespace Jarrus.GA
{
    public class GAConfiguration : GAProperties
    {
        internal Crossover Crossover;
        internal Mutation Mutation;
        internal ParentSelection ParentSelection;
        internal FitnessFunction FitnessFunction;

        public int GeneSize;
        public Random Random;
        public Random RandomPool;
        public Random RandomFirstNameSeed;
        public Random RandomLastNameSeed;
        public readonly string TaskUUID;

        public GAConfiguration(GATask task)
        {
            if (task == null) { throw new ArgumentException("Task can not be null"); }

            TaskUUID = task.UUID;
            Reflection.CopyProperties(task, this);
            ValidateProperties();
            SetupStrategies(task);
            SetupRandomVariables();
            ValidateFactoryObjects();
        }

        public void Validate()
        {
            ValidateProperties();
            ValidateFactoryObjects();
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
            if (MutationRate < 0 || MutationRate > 1 || ElitismRate < 0 || ElitismRate > 1 || CrossoverRate < 0 || CrossoverRate > 1)
            {
                throw new ArgumentException("GAConfiguration rates must be between 0 and 1");
            }

            if (RetirementType != Factory.Enums.RetirementType.None)
            {
                if (MaxRetirement <= 0) { throw new ArgumentException("Retirement maximum must be above 0 for this type of retirement type"); }
            }

            if (!Enum.IsDefined(RetirementType.GetType(), (int) RetirementType)) { throw new ArgumentException("RetirementType must be defined"); }
            if (!Enum.IsDefined(CrossoverType.GetType(), (int)CrossoverType)) { throw new ArgumentException("CrossoverType must be defined"); }
            if (!Enum.IsDefined(ImmigrationType.GetType(), (int)ImmigrationType)) { throw new ArgumentException("ImmigrationType must be defined"); }
            if (!Enum.IsDefined(MutationType.GetType(), (int)MutationType)) { throw new ArgumentException("MutationType must be defined"); }
            if (!Enum.IsDefined(ParentSelectionType.GetType(), (int)ParentSelectionType)) { throw new ArgumentException("ParentSelectionType must be defined"); }
        }

        public void ValidateFactoryObjects()
        {
            if (!IsValid() || ChildrenPerParents == 0)
            {
                throw new ArgumentException("Invalid settings passed to GAConfiguration");
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
        public bool IsValid()
        {
            var solutionsValid = Solution != null && Mutation != null && Crossover != null && ParentSelection != null;

            return solutionsValid && PopulationSize > 0;
        }

        public bool IsOrderedConfiguration() { return typeof(JarrusOrderedSolution).IsAssignableFrom(Solution.GetType()); }
        public bool IsUnorderedConfiguration() { return typeof(JarrusUnorderedSolution).IsAssignableFrom(Solution.GetType()); }
    }
}
