using GeneticAlgorithms.BasicTypes;
using GeneticAlgorithms.Utility;
using System;

namespace GeneticAlgorithms.Mutations
{
    public class ScrambleMutation : Mutation
    {
        protected override void Perform(Chromosome chromosome, GAConfiguration settings)
        {
            var firstMutationPoint = settings.GetRandomInteger(0, chromosome.Genes.Length - 1);
            var secondMutationPoint = settings.GetRandomInteger(0, chromosome.Genes.Length - 1, firstMutationPoint);

            if (firstMutationPoint > secondMutationPoint)
            {
                var temp = secondMutationPoint;
                secondMutationPoint = firstMutationPoint;
                firstMutationPoint = temp;
            }

            Scramble(chromosome, firstMutationPoint, secondMutationPoint, settings);
        }

        public void Scramble(Chromosome chromosome, int startingAtIndex, int endingAtIndex, GAConfiguration settings)
        {
            if (endingAtIndex <= startingAtIndex) { throw new ArgumentException("Scramble Mutation must have a swap length of at least 1."); }
            chromosome.Genes = chromosome.Genes.ShuffleSubset(startingAtIndex, endingAtIndex, settings.Random);
        }
    }
}