namespace GeneticAlgorithms.Mutations
{
    public class SwapMutation : Mutation
    {
        protected override void Perform<T>(Chromosome<T> chromosome, GAConfiguration<T> settings)
        {
            var firstMutationPoint = settings.GetRandomInteger(0, chromosome.Genes.Length - 1);
            var secondMutationPoint = settings.GetRandomInteger(0, chromosome.Genes.Length - 1, firstMutationPoint);

            FlipGenes(chromosome, firstMutationPoint, secondMutationPoint);
        }
    }
}

