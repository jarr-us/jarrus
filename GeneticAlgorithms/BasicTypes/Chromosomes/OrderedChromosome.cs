namespace Jarrus.GA.Models
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
