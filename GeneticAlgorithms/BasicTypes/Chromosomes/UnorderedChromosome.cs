using Jarrus.GA.Solution;
using System;
using System.Diagnostics;

namespace Jarrus.GA.Models
{
    public class UnorderedChromosome : Chromosome
    {
        public UnorderedChromosome(int geneSize)
        {
            Genes = new Gene[geneSize];
        }

        public UnorderedChromosome(Gene[] genes)
        {
            Genes = genes;
        }

        public UnorderedChromosome(int geneSize, Type unorderedGeneType, Random random)
        {
            Genes = new Gene[geneSize];

            for (int i = 0; i < geneSize; i++)
            {
                Genes[i] = (Gene)Activator.CreateInstance(unorderedGeneType, random);
            }
        }

        public UnorderedChromosome(JarrusSolution solution, Random random)
        {
            var unorderedSolution = (JarrusUnorderedSolution)solution;
            var geneSize = unorderedSolution.GetGeneSize();

            Genes = new Gene[geneSize];
            for (int i = 0; i < geneSize; i++)
            {
                Genes[i] = unorderedSolution.GetNewGene(random);
            }
        }
    }
}
