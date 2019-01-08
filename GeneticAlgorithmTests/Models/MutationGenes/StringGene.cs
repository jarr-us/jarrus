using GeneticAlgorithms.BasicTypes;
using GeneticAlgorithms.BasicTypes.Attributes;
using System;

namespace GeneticAlgorithmTests.Models.MutationGenes
{    
    public class StringGene : UnorderedGene
    {
        [GeneMutation(StringValues = new string[9] { "A1", "B2", "C3", "D4", "E5", "F6", "G7", "H8", "I9"})]
        public string Value;

        public StringGene(Random random) : base(random) { }
        public override string ToString() { return Value.ToString(); }
    }
}
