using GeneticAlgorithms.BasicTypes;

namespace GeneticAlgorithms.Mutations
{
    public class InsertMutation : Mutation
    {
        protected override void Perform<T>(Chromosome<T> chromosome, GAConfiguration<T> settings)
        {
            var firstMutationPoint = settings.GetRandomInteger(0, chromosome.Genes.Length - 1);
            var secondMutationPoint = settings.GetRandomInteger(0, chromosome.Genes.Length - 1, firstMutationPoint);

            Shift(chromosome, firstMutationPoint, secondMutationPoint);
        }

        public void Shift<T>(Chromosome<T> chromosome, int start, int end) where T : Gene
        {
            for (int i = 0; i < end - start; i++)
            {
                FlipGenes(chromosome, start + i, end);
            }
        }
    }
}
