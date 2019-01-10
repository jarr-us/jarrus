using Jarrus.GA.BasicTypes.Genes;
namespace Jarrus.GA.Solution
{
    public abstract class JarrusOrderedSolution : JarrusSolution
    {
        public abstract Gene[] GetOptions();
                
        public override GARun Run(GAConfiguration configuration)
        {
            Configuration = configuration;

            var options = GetOptions();
            Configuration.GeneSize = options.Length;
            GeneticAlgorithm = new OrderedGeneticAlgorithm(Configuration, options);
            return GeneticAlgorithm.Run();
        }
    }
}
