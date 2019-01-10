using Jarrus.GA.BasicTypes;
using Jarrus.GA.BasicTypes.Attributes;
using System;

namespace Jarrus.GATests.Models.TestGenes
{
    public class BooleanGene : UnorderedGene
    {
        [GeneOption(BoolValues = new bool[2] { true, false })]
        public bool Value;

        public BooleanGene(Random random) : base(random) { }
        public override string ToString() { return Value.ToString(); }
    }
}
