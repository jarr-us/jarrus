using Jarrus.GA.Factory.Enums;
using Jarrus.GA.Solution;

namespace Jarrus.GA.BasicTypes
{
    public abstract class GAProperties
    {
        public ParentSelectionType ParentSelectionType;
        public MutationType MutationType;
        public CrossoverType CrossoverType;

        public JarrusSolution Solution;

        public string UUID { get; set; }
        public string Session { get; set; }
        public string ComputerName { get; set; }
        public bool LowestScoreIsBest { get; set; }
        public int MaxPopulationSize { get; set; }
        public int MaxGenerations { get; set; }
        public double CrossoverRate { get; set; }
        public double MutationRate { get; set; }
        public double ElitismRate { get; set; }
        public bool PreventDuplications { get; set; }
        public int MaximumLifeSpan { get; set; }
        public int ChildrenPerCouple { get; set; }
        public int RandomSeed { get; set; }
        public int RandomPoolGenerationSeed { get; set; }
    }
}
