using System;

namespace GeneticAlgorithms.BasicTypes
{
    public class GeneAttribute : Attribute
    {
        public char[] MutatableValues { get; set; }
    }
}
