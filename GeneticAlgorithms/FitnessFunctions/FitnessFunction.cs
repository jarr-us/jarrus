using GeneticAlgorithms.BasicTypes;

namespace GeneticAlgorithms.FitnessFunctions
{
    public abstract class FitnessFunction
    {
        public abstract double GetFitnessScoreFor<T>(Chromosome<T> chromosome) where T : Gene;
    }
}
