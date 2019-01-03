using GeneticAlgorithms.BasicTypes;

namespace GeneticAlgorithmTests.Models
{
    public class ExampleGene : Gene
    {
        public char Value;
        public ExampleGene(char c) { Value = c; }

        public override bool Equals(object obj)
        {
            var castedObject = (ExampleGene)obj;
            return castedObject.Value == Value;
        }

        public override string ToString() { return Value.ToString(); }
        public override int GetHashCode() { return Value.GetHashCode(); }
    }
}
