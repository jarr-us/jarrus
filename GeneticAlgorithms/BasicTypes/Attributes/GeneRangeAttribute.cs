﻿using System;

namespace Jarrus.GA.BasicTypes.Attributes
{
    public class GeneRangeAttribute : Attribute
    {
        public bool[] BoolValues { get; set; }
        public int[] IntValues { get; set; }
        public char[] CharValues { get; set; }
        public string[] StringValues { get; set; }
    }
}
