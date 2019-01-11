using Jarrus.GA.Models;

namespace Jarrus.GA.Mutations
{
    public class InversionMutation : Mutation
    {
        protected override void Perform(GAConfiguration settings, Chromosome chromosome, int geneIndex)
        {
            var secondMutationPoint = settings.GetRandomInteger(0, chromosome.Genes.Length - 1, geneIndex);
            Inverse(chromosome, geneIndex, secondMutationPoint);
        }
          
        public void Inverse(Chromosome chromosome, int startingAtIndex, int endingAtIndex)
        {
            var length = endingAtIndex - startingAtIndex;
            var count = 0;

            for (int i = startingAtIndex; i < length; i++)
            {
                FlipGenes(chromosome, i, endingAtIndex - count);
                count++;
            }
        }
    }
}
