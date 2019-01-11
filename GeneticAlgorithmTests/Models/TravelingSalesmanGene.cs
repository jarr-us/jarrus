using Jarrus.GA.Models;

namespace Jarrus.GATests.Models
{
    public class TravelingSalesmanGene : OrderedGene
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
