using Jarrus.GA.Models;
using Jarrus.GA.Models.Attributes;
using System;

namespace Jarrus.GATests.Models.TestGenes
{
    public class BooleanGene : UnorderedGene
    {
        [GeneOption(BoolValues = new bool[2] { true, false })]
        public bool Value;

        public BooleanGene(Random random) : base(random) { }
        public override int GetHashCode() { return Value.GetHashCode(); }
        public override string ToString() { return Value.ToString(); }
    }
}
