using System;

namespace Jarrus.GA.Models.Attributes
{
    public class GeneRangeAttribute : Attribute
    {
        public bool[] BoolValues { get; set; }
        public int[] IntValues { get; set; }
        public char[] CharValues { get; set; }
        public string[] StringValues { get; set; }
    }
}

