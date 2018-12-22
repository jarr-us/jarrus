namespace GeneticAlgorithms.FitnessCalculators
{
    public abstract class FitnessCalculator
    {
        public abstract double GetFitnessScoreFor<T>(Chromosome<T> chromosome);
    }
}
