using GeneticAlgorithms.BasicTypes.Genes;

namespace GeneticAlgorithms.Mutations
{
    public class InsertMutation : Mutation
    {
        protected override void Perform(GAConfiguration settings, Chromosome chromosome, int geneIndex)
        {
            var secondMutationPoint = settings.GetRandomInteger(0, chromosome.Genes.Length - 1, geneIndex);
            Shift(chromosome, geneIndex, secondMutationPoint);
        }

        public void Shift(Chromosome chromosome, int start, int end)
        {
            for (int i = 0; i < end - start; i++)
            {
                FlipGenes(chromosome, start + i, end);
            }
        }
    }
}
