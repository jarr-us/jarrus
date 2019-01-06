using GeneticAlgorithms.BasicTypes;
using GeneticAlgorithms.FitnessFunctions;
namespace GeneticAlgorithms.Solution
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
