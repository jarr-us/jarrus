using Jarrus.GA.BasicTypes;
using Jarrus.GA.BasicTypes.Attributes;
using System;

namespace Jarrus.GATests.Models.MutationGenes
{
    public class CharacterGene : UnorderedGene
    {
        [GeneMutation(CharValues = new char[9] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I' })]
        public char Value;

        public CharacterGene(Random random) : base(random) { }
        public override string ToString() { return Value.ToString(); }
    }
}
