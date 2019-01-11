using Jarrus.GA.Models;
using Jarrus.GA.Models.Attributes;
using System;

namespace Jarrus.GATests.Models
{
    public class PhraseGene : UnorderedGene
    {
        [GeneOption(CharValues = new char[28] {
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's',
            't', 'u', 'v', 'w', 'x', 'y', 'z', ',', ' '
        })]
        public char Value;

        public PhraseGene(Random random) : base(random) { }

        public override int GetHashCode() { return Value.GetHashCode(); }
        public override string ToString() { return Value.ToString(); }
    }
}
