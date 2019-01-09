using System;
using Jarrus.GA.BasicTypes;
using Jarrus.GA.BasicTypes.Attributes;

namespace Jarrus.GATests.Models
{
    public class IntegerGene : UnorderedGene
    {
        [GeneMutation(IntValues = new int[9] { 1, 2, 3, 4, 5, 6, 7, 8, 9 })]
        public int Value;

        public IntegerGene(Random random) : base(random) { }        
        public override string ToString() { return Value.ToString(); }
    }
}
