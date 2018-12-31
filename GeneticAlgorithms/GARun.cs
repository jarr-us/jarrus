using GeneticAlgorithms.BasicTypes;
using System;

namespace GeneticAlgorithms
{
    public class GARun<T> where T : Gene
    {
        public string UUID { get; set; }
        public string Session { get; set; }
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
        public int MaxGenerations { get; set; }
        public double CrossoverRate { get; set; }
        public double MutationRate { get; set; }
        public double ElitismRate { get; set; }
        public bool PreventDuplications { get; set; }
        public int MaximumLifeSpan { get; set; }
        public int ChildrenPerCouple { get; set; }
        public int RandomSeed { get; set; }
        public int RandomPoolGenerationSeed { get; set; }
        public Chromosome<T> LowestChromosome { get; set; }
        public Chromosome<T> HighestChromosome { get; set; }
        public Population<T> Population;
        
        public void SetValues(GAConfiguration<T> config)
        {
            Start = DateTime.UtcNow;
            SolutionType = config.FitnessCalculator.GetType().Name;
            ComputerName = Environment.MachineName;
            ParentSelectionType = config.ParentSelection.GetType().Name;
            MutationType = config.Mutation.GetType().Name;
            CrossoverType = config.Crossover.GetType().Name;
            LowestScoreIsBest = config.LowestScoreIsBest;
            PoolSize = config.PoolSize;
            MaxGenerations = config.Iterations;
            CrossoverRate = config.CrossoverRate;
            MutationRate = config.MutationRate;
            ElitismRate = config.ElitismRate;
            PreventDuplications = config.PreventDuplicationInPool;
            MaximumLifeSpan = config.MaximumLifeSpan;
            ChildrenPerCouple = config.ChildrenPerCouple;
            RandomSeed = config.RandomSeed;
            RandomPoolGenerationSeed = config.RandomPoolGenerationSeed;
        }

        public double GetTotalMSToRun() { return (End - Start).TotalMilliseconds; }
    }
}
