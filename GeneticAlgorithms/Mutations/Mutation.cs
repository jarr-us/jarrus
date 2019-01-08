using GeneticAlgorithms.Factory.Enums;

namespace GeneticAlgorithms.Mutations
{
    public abstract class Mutation
    {
        public MutationType MutationType;

        public void Mutate(Chromosome chromosome, GAConfiguration settings)
        {
            for(int i = 0; i < chromosome.Genes.Length; i++)
            {
                if (settings.GetNextDouble() < settings.MutationRate)
                {
                    Perform(settings, chromosome, i);
                }
            }
        }

        public void FlipGenes(Chromosome chromosome, int spotOne, int spotTwo)
        {
            var temp = chromosome.Genes[spotOne];
            chromosome.Genes[spotOne] = chromosome.Genes[spotTwo];
            chromosome.Genes[spotTwo] = temp;
        }

        protected abstract void Perform(GAConfiguration settings, Chromosome chromosome, int geneIndex);
    }
}
