using System;

namespace GeneticAlgorithms
{
    public class GARun
    {
        public int Id { get; set; }
        public int CurrentGeneration { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string SolutionType { get; set; }
        public string ComputerName { get; set; }
        public string ParentSelectionType { get; set; }
        public string MutationType { get; set; }
        public string CrossoverType { get; set; }
        public bool LowestScoreIsBest { get; set; }
        public int PoolSize { get; set; }
        public int Iterations { get; set; }
        public double CrossoverRate { get; set; }
        public double MutationRate { get; set; }
        public double ElitismRate { get; set; }
        public bool PreventDuplications { get; set; }
        public int MaximumLifeSpan { get; set; }
        public int ChildrenPerCouple { get; set; }
        public int RandomSeed { get; set; }
        public int RandomPoolGenerationSeed { get; set; }
        public double HighestScore { get; set; }
        public int HighestScoreGeneration { get; set; }
        public double LowestScore { get; set; }
        public int LowestScoreGeneration { get; set; }

        public void SetValues<T>(GAConfiguration<T> config)
        {
            Start = DateTime.UtcNow;
            SolutionType = config.FitnessCalculator.GetType().Name;
            ComputerName = Environment.MachineName;
            ParentSelectionType = config.ParentSelection.GetType().Name;
            MutationType = config.Mutation.GetType().Name;
            CrossoverType = config.Crossover.GetType().Name;
            LowestScoreIsBest = config.LowestScoreIsBest;
            PoolSize = config.PoolSize;
            Iterations = config.Iterations;
            CrossoverRate = config.CrossoverRate;
            MutationRate = config.MutationRate;
            ElitismRate = config.ElitismRate;
            PreventDuplications = config.PreventDuplicationInPool;
            MaximumLifeSpan = config.MaximumLifeSpan;
            ChildrenPerCouple = config.ChildrenPerCouple;
            RandomSeed = config.RandomSeed;
            RandomPoolGenerationSeed = config.RandomPoolGenerationSeed;

            HighestScore = -1;
            LowestScore = -1;
        }

        public double GetTotalMSToRun() { return (End - Start).TotalMilliseconds; }
    }
}
