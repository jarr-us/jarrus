namespace GeneticAlgorithms.BasicTypes.Genes
{
    public abstract class OrderedGene : Gene
    {
        public abstract override string ToString();
        public abstract override bool Equals(object obj);
        public override int GetHashCode() { return base.GetHashCode(); }

        public OrderedGene() : base() { }
    }
}
