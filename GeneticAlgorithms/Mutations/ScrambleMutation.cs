using GeneticAlgorithms.BasicTypes;
using GeneticAlgorithms.Utility;
using System;

namespace GeneticAlgorithms.Mutations
{
    public class ScrambleMutation : Mutation
    {
        protected override void Perform<T>(Chromosome<T> chromosome, GAConfiguration<T> settings)
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

        public void Scramble<T>(Chromosome<T> chromosome, int startingAtIndex, int endingAtIndex, GAConfiguration<T> settings) where T : Gene
        {
            if (endingAtIndex <= startingAtIndex) { throw new ArgumentException("Scramble Mutation must have a swap length of at least 1."); }
            chromosome.Genes = chromosome.Genes.ShuffleSubset(startingAtIndex, endingAtIndex, settings.Random);
        }
    }
}