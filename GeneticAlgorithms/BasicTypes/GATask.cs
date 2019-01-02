using GeneticAlgorithms.Crossovers;
using GeneticAlgorithms.FitnessFunctions;
using GeneticAlgorithms.Mutations;
using GeneticAlgorithms.ParentSelections;

namespace GeneticAlgorithms.BasicTypes
{
    public class GATask<T> where T :Gene
    {
        public string UUID { get; set; }
        public string Session { get; set; }
        public string ComputerName { get; set; }        

        public ParentSelection<T> ParentSelection;
        public FitnessFunction FitnessFunction;
        public Crossover Crossover;
        public Mutation Mutation;

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
        public bool PreventDuplicationInPool;
    }
}
