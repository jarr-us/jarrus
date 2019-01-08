using GeneticAlgorithms.BasicTypes;
using GeneticAlgorithms.BasicTypes.Attributes;
using System;

namespace GeneticAlgorithmTests.Models.MutationGenes
{
    public class BooleanGene : UnorderedGene
    {
        [GeneMutation(BoolValues = new bool[2] { true, false })]
        public bool Value;

        public BooleanGene(Random random) : base(random) { }
        public override string ToString() { return Value.ToString(); }
    }
}
