using GeneticAlgorithms.Utility;
using System;

namespace GeneticAlgorithms.Mutations
{
    public class ScrambleMutation : Mutation
    {
        protected override void Perform(GAConfiguration settings, Chromosome chromosome, int geneIndex)
        {
            var secondMutationPoint = settings.GetRandomInteger(0, chromosome.Genes.Length - 1, geneIndex);

            if (geneIndex > secondMutationPoint)
            {
                var temp = secondMutationPoint;
                secondMutationPoint = geneIndex;
                geneIndex = temp;
            }

            Scramble(chromosome, geneIndex, secondMutationPoint, settings);
        }

        public void Scramble(Chromosome chromosome, int startingAtIndex, int endingAtIndex, GAConfiguration settings)
        {
            if (endingAtIndex <= startingAtIndex) { throw new ArgumentException("Scramble Mutation must have a swap length of at least 1."); }
            chromosome.Genes = chromosome.Genes.ShuffleSubset(startingAtIndex, endingAtIndex, settings.Random);
        }
    }
}