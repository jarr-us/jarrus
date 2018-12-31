namespace GeneticAlgorithms.BasicTypes
{
    public abstract class Gene
    {
        public abstract override string ToString();
        public abstract override bool Equals(object obj);

        public static bool operator ==(Gene obj1, Gene obj2)
        {
            if (ReferenceEquals(obj1, obj2)) { return true; }
            if (ReferenceEquals(obj1, null)) { return false; }
            if (ReferenceEquals(obj2, null)) { return false; }

            return obj1.Equals(obj2);
        }

        public static bool operator !=(Gene obj1, Gene obj2) { return !(obj1 == obj2); }
        public override int GetHashCode() { return base.GetHashCode(); }
    }
}