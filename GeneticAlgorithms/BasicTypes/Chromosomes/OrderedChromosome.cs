using Jarrus.GA.BasicTypes.Genes;

namespace Jarrus.GA.BasicTypes.Chromosomes
{
    public class OrderedChromosome : Chromosome
    {
        public OrderedChromosome(int geneSize)
        {
            Genes = new Gene[geneSize];
        }

        public OrderedChromosome(params Gene[] genes)
        {
            Genes = (Gene[])genes.Clone();
        }
    }
}
