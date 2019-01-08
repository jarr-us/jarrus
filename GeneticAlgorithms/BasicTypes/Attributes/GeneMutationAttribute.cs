using System;

namespace GeneticAlgorithms.BasicTypes.Attributes
{
    public class GeneMutationAttribute : Attribute
    {
        public bool[] BoolValues { get; set; }
        public int[] IntValues { get; set; }
        public char[] CharValues { get; set; }
        public string[] StringValues { get; set; }
    }
}
