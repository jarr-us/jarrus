using GeneticAlgorithms.BasicTypes;

namespace GeneticAlgorithmTests.Models.Shakespeare
{
    public class CharacterGene : Gene
    {

        //https://stackoverflow.com/questions/16710533/passing-static-array-in-attribute
        [Gene(MutatableValues = new char[4] {'A', 'B', 'C', 'D' })]
        public char Value;
        public CharacterGene(char c) { Value = c; }
        
        public override bool Equals(object obj)
        {
            var castedObject = (TravelingSalesmanGene)obj;
            return castedObject.Value == Value;
        }

        public override string ToString() { return Value.ToString(); }
        public override int GetHashCode() { return Value.GetHashCode(); }
    }
}
