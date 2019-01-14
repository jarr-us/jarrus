using Jarrus.GA.Models;
using System;

namespace Jarrus.GA.Solution
{
    public abstract class JarrusUnorderedSolution : JarrusSolution
    {
        public abstract int GetGeneSize();

        public override GARun Run(GAConfiguration configuration)
        {
            Configuration = configuration;
            Configuration.GeneSize = GetGeneSize();
            GeneticAlgorithm = new UnorderedGeneticAlgorithm(Configuration, GetNewGene(new Random()).GetType());
            return GeneticAlgorithm.Run();
        }

        public abstract UnorderedGene GetNewGene(Random random);
    }
}
