using System;

namespace Jarrus.GA.Models
{
    public abstract class UnorderedGene : Gene
    {
        public UnorderedGene(Random random) : base(random) { }
        public abstract override string ToString();
    }
}
