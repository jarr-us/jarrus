using GeneticAlgorithms.BasicTypes.Genes;
using System;

namespace GeneticAlgorithms.BasicTypes
{
    public abstract class UnorderedGene : Gene
    {
        public UnorderedGene(Random random) : base(random) { }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj);
        }

        public abstract override string ToString();

        public override int GetHashCode() { return base.GetHashCode(); }
    }
}
