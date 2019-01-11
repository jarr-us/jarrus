using Jarrus.GA.Models;
using Jarrus.GA.Models.Attributes;
using System;

namespace Jarrus.GATests.Models.TestGenes
{    
    public class StringGene : UnorderedGene
    {
        [GeneOption(StringValues = new string[9] { "A1", "B2", "C3", "D4", "E5", "F6", "G7", "H8", "I9"})]
        public string Value;

        public StringGene(Random random) : base(random) { }
        public override int GetHashCode() { return Value.GetHashCode(); }
        public override string ToString() { return Value.ToString(); }
    }
}
