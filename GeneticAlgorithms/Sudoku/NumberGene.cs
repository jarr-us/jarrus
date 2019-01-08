using System;
using GeneticAlgorithms.BasicTypes;
using GeneticAlgorithms.BasicTypes.Attributes;

namespace GeneticAlgorithms.Sudoku
{
    public class NumberGene : UnorderedGene
    {
        [GeneMutation(IntValues = new int[10] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9 })]
        public int Value;
        
        public NumberGene(Random random) : base(random) { }
        public override string ToString() { return Value.ToString(); }
    }
}
