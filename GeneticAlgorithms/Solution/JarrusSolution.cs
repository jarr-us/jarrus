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
        public GARun GARun;
        
        public GARun Run(GAConfiguration configuration)
        {
            Configuration = configuration;

            var data = GetOptions();
            GeneticAlgorithm = new GeneticAlgorithm(Configuration, data);
            GARun = GeneticAlgorithm.GARun;           
            return GeneticAlgorithm.Run();
        }
    }
}
