using GeneticAlgorithms.BasicTypes;

namespace GeneticAlgorithms.Mutations
{
    public class InsertMutation : Mutation
    {
        protected override void Perform(Chromosome chromosome, GAConfiguration settings)
        {
            var firstMutationPoint = settings.GetRandomInteger(0, chromosome.Genes.Length - 1);
            var secondMutationPoint = settings.GetRandomInteger(0, chromosome.Genes.Length - 1, firstMutationPoint);

            Shift(chromosome, firstMutationPoint, secondMutationPoint);
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
