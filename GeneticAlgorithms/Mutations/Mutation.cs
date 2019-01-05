using GeneticAlgorithms.BasicTypes;

namespace GeneticAlgorithms.Mutations
{
    public abstract class Mutation
    {
        public void Mutate(Chromosome chromosome, GAConfiguration settings)
        {
            foreach (var gene in chromosome.Genes)
            {
                if (settings.GetNextDouble() < settings.MutationRate)
                {
                    Perform(chromosome, settings);
                }
            }
        }

        public void FlipGenes(Chromosome chromosome, int spotOne, int spotTwo)
        {
            var temp = chromosome.Genes[spotOne];
            chromosome.Genes[spotOne] = chromosome.Genes[spotTwo];
            chromosome.Genes[spotTwo] = temp;
        }

        protected abstract void Perform(Chromosome chromosome, GAConfiguration settings);
    }
}
