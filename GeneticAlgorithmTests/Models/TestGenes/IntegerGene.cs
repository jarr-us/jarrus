using System;
using Jarrus.GA.Models;
using Jarrus.GA.Models.Attributes;

namespace Jarrus.GATests.Models.TestGenes
{
    public class IntegerGene : UnorderedGene
    {
        [GeneOptionAttribute(IntValues = new int[9] { 1, 2, 3, 4, 5, 6, 7, 8, 9 })]
        public int Value;

        public IntegerGene(Random random) : base(random) { }
        public override int GetHashCode() { return Value.GetHashCode(); }
        public override string ToString() { return Value.ToString(); }
    }
}
