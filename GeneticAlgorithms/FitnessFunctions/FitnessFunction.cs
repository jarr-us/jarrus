using Jarrus.GA.BasicTypes;

namespace Jarrus.GA.FitnessFunctions
{
    public abstract class FitnessFunction
    {
        public abstract double GetFitnessScoreFor(Chromosome chromosome);
    }
}
