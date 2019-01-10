using Jarrus.GA.BasicTypes.Genes;
using System;

namespace Jarrus.GA.BasicTypes.Chromosomes
{
    public class UnorderedChromosome : Chromosome
    {
        public UnorderedChromosome(int geneSize)
        {
            Genes = new Gene[geneSize];
        }

        public UnorderedChromosome(int geneSize, Type unorderedGeneType, Random random)
        {
            Genes = new Gene[geneSize];

            for (int i = 0; i < geneSize; i++)
            {
                Genes[i] = (Gene)Activator.CreateInstance(unorderedGeneType, random);
            }
        }
    }
}
