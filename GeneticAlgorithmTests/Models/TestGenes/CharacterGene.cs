using Jarrus.GA.Models;
using Jarrus.GA.Models.Attributes;
using System;

namespace Jarrus.GATests.Models.TestGenes
{
    public class CharacterGene : UnorderedGene
    {
        [GeneOption(CharValues = new char[9] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I' })]
        public char Value;

        public CharacterGene(Random random) : base(random) { }
        public override int GetHashCode() { return Value.GetHashCode(); }
        public override string ToString() { return Value.ToString(); }
    }
}
