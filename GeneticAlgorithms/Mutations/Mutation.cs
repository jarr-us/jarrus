
using GeneticAlgorithms.BasicTypes;

namespace GeneticAlgorithms.Mutations
{
    public abstract class Mutation
    {
        public void Mutate<T>(Chromosome<T> chromosome, GAConfiguration<T> settings) where T : Gene
        {
            foreach (var gene in chromosome.Genes)
            {
                if (settings.GetNextDouble() < settings.MutationRate)
                {
                    Perform<T>(chromosome, settings);
                }
            }
        }

        public void FlipGenes<T>(Chromosome<T> chromosome, int spotOne, int spotTwo) where T : Gene
        {
            T temp = chromosome.Genes[spotOne];
            chromosome.Genes[spotOne] = chromosome.Genes[spotTwo];
            chromosome.Genes[spotTwo] = temp;
        }

        protected abstract void Perform<T>(Chromosome<T> chromosome, GAConfiguration<T> settings) where T : Gene;
    }
}
