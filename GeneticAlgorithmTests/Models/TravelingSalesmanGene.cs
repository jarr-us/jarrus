using GeneticAlgorithms.BasicTypes;

namespace GeneticAlgorithmTests.Models
{
    public class TravelingSalesmanGene : Gene
    {
        public char Value;
        public TravelingSalesmanGene(char c) { Value = c; }

        public override bool Equals(object obj)
        {
            var castedObject = (TravelingSalesmanGene)obj;
            return castedObject.Value == Value;
        }

        public override string ToString() { return Value.ToString(); }
        public override int GetHashCode() { return Value.GetHashCode(); }
    }
}
