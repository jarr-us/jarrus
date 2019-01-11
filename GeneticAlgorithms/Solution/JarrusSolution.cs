using Jarrus.GA.Models;

namespace Jarrus.GA.Solution
{
    public abstract class JarrusSolution
    {
        public GAConfiguration Configuration;
        public GeneticAlgorithm GeneticAlgorithm;

        public abstract bool ShouldTerminate(Population population);
        public abstract double GetFitnessScoreFor(Chromosome chromosome);
        public abstract GARun Run(GAConfiguration configuration);
    }
}
