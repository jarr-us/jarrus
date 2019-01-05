namespace GeneticAlgorithms.Mutations
{
    public class SwapMutation : Mutation
    {
        protected override void Perform(Chromosome chromosome, GAConfiguration settings)
        {
            var firstMutationPoint = settings.GetRandomInteger(0, chromosome.Genes.Length - 1);
            var secondMutationPoint = settings.GetRandomInteger(0, chromosome.Genes.Length - 1, firstMutationPoint);

            FlipGenes(chromosome, firstMutationPoint, secondMutationPoint);
        }
    }
}

