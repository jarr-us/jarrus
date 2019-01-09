using Jarrus.GA.BasicTypes.Genes;
using Jarrus.GA.FitnessFunctions;
namespace Jarrus.GA.Solution
{
    public abstract class JarrusSolution
    {
        public abstract Gene[] GetOptions();
        public abstract FitnessFunction GetFitnessFunction();

        public GAConfiguration Configuration;
        public GeneticAlgorithm GeneticAlgorithm;
        
        public GARun Run(GAConfiguration configuration)
        {
            Configuration = configuration;
            GeneticAlgorithm = new GeneticAlgorithm(Configuration, GetOptions());     
            return GeneticAlgorithm.Run();
        }
    }
}
