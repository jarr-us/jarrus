using GeneticAlgorithms;
using GeneticAlgorithms.BasicTypes.Genes;
using GeneticAlgorithms.FitnessFunctions;
using GeneticAlgorithms.Solution;
using GeneticAlgorithmTests.Models.FitnessFunctions;

namespace GeneticAlgorithmTests.Models.FitnessCalculators
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
