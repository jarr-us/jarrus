using Jarrus.GA.FitnessFunctions;

namespace Jarrus.GA.Solution
{
    public abstract class JarrusSolution
    {
        public GAConfiguration Configuration;
        public GeneticAlgorithm GeneticAlgorithm;

        public abstract FitnessFunction GetFitnessFunction();
        public abstract GARun Run(GAConfiguration configuration);
    }
}
