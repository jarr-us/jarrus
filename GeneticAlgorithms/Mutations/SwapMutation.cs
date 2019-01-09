using Jarrus.GA.BasicTypes.Genes;

namespace Jarrus.GA.Mutations
{
    public class SwapMutation : Mutation
    {
        protected override void Perform(GAConfiguration settings, Chromosome chromosome, int geneIndex)
        {
            var secondMutationPoint = settings.GetRandomInteger(0, chromosome.Genes.Length - 1, geneIndex);
            FlipGenes(chromosome, geneIndex, secondMutationPoint);
        }
    }
}

