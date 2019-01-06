using GeneticAlgorithms;
using GeneticAlgorithms.BasicTypes;
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
                new ExampleGene('A'),
                new ExampleGene('B'),
                new ExampleGene('C'),
                new ExampleGene('D')
            ).Genes;
        }
    }
}
