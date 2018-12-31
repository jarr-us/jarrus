using GeneticAlgorithms.BasicTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithms.Mutations
{
    public class InversionMutation : Mutation
    {
        protected override void Perform<T>(Chromosome<T> chromosome, GAConfiguration<T> settings) 
        {
            var firstMutationPoint = settings.GetRandomInteger(0, chromosome.Genes.Length - 1);
            var secondMutationPoint = settings.GetRandomInteger(0, chromosome.Genes.Length - 1, firstMutationPoint);

            Inverse(chromosome, firstMutationPoint, secondMutationPoint);
        }
          
        public void Inverse<T>(Chromosome<T> chromosome, int startingAtIndex, int endingAtIndex) where T : Gene
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
