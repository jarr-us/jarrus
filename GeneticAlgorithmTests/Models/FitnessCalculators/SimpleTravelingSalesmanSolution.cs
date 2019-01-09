using Jarrus.GA;
using Jarrus.GA.BasicTypes.Genes;
using Jarrus.GA.FitnessFunctions;
using Jarrus.GA.Solution;
using Jarrus.GATests.Models.FitnessFunctions;

namespace Jarrus.GATests.Models.FitnessCalculators
{
    public class SimpleTravelingSalesmanSolution : JarrusSolution
    {
        public override FitnessFunction GetFitnessFunction() { return new TravelingSalesmanFitnessFunction(); }

        public override Gene[] GetOptions()
        {
            return new Chromosome(
                new TravelingSalesmanGene('A'),
                new TravelingSalesmanGene('B'),
                new TravelingSalesmanGene('C'),
                new TravelingSalesmanGene('D')
            ).Genes;
        }
    }
}
