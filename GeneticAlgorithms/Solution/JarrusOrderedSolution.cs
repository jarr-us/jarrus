using Jarrus.GA.Models;
namespace Jarrus.GA.Solution
{
    public abstract class JarrusOrderedSolution : JarrusSolution
    {
        public abstract Gene[] GetOptions();
                
        public override GARun Run(GAConfiguration configuration)
        {
            Configuration = configuration;

            var options = GetOptions();
            GeneticAlgorithm = new OrderedGeneticAlgorithm(Configuration, options);
            return GeneticAlgorithm.Run();
        }
    }
}
