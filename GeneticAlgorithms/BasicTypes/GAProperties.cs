using Jarrus.GA.Factory.Enums;
using Jarrus.GA.Solution;

namespace Jarrus.GA.Models
{
    public abstract class GAProperties
    {
        public ParentSelectionType ParentSelectionType;
        public MutationType MutationType;
        public CrossoverType CrossoverType;
        public ImmigrationType ImmigrationType = ImmigrationType.None;
        public RetirementType RetirementType = RetirementType.None;
        public ScoringType ScoringType = ScoringType.Highest;
        public DuplicationType DuplicationType = DuplicationType.Prevent;

        public JarrusSolution Solution;

        public string UUID { get; set; }
        public string Session { get; set; }
        public string ComputerName { get; set; }        
        public int PopulationSize { get; set; }
        public int MaxGenerations { get; set; }
        public double CrossoverRate { get; set; }
        public double MutationRate { get; set; }
        public double ElitismRate { get; set; }
        public double ImmigrationRate { get; set; }
        public int MaxRetirement { get; set; }
        public int ChildrenPerParents { get; set; }
        public int RandomSeed { get; set; }
        public int RandomPoolGenerationSeed { get; set; }
    }
}
