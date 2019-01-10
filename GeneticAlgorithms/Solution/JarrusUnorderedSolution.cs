using System;

namespace Jarrus.GA.Solution
{
    public abstract class JarrusUnorderedSolution : JarrusSolution
    {
        public abstract int GetGeneSize();
        public abstract Type GetGeneType();

        public override GARun Run(GAConfiguration configuration)
        {
            Configuration = configuration;
            Configuration.GeneSize = GetGeneSize();
            GeneticAlgorithm = new UnorderedGeneticAlgorithm(Configuration, GetGeneType());
            return GeneticAlgorithm.Run();
        }
    }
}
