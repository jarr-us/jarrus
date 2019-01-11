using Jarrus.GA.Models;

namespace Jarrus.GA.Crossovers.Unordered
{
    public class TwoPointCrossover : Crossover
    {
        protected override Chromosome Perform(Chromosome father, Chromosome mother, GAConfiguration configuration)
        {
            var geneCount = father.Genes.Length;

            var childOne = new UnorderedChromosome(geneCount);
            var childTwo = new UnorderedChromosome(geneCount);

            var firstCrossoverPoint = configuration.GetRandomInteger(1, father.Genes.Length - 1);
            var secondCrossoverPoint = configuration.GetRandomInteger(1, father.Genes.Length - 1, firstCrossoverPoint);

            if (firstCrossoverPoint > secondCrossoverPoint)
            {
                var temp = firstCrossoverPoint;
                firstCrossoverPoint = secondCrossoverPoint;
                secondCrossoverPoint = temp;
            }

            for (int i = 0; i < geneCount; i++)
            {
                if (i < firstCrossoverPoint)
                {
                    childOne.Genes[i] = father.Genes[i];
                    childTwo.Genes[i] = mother.Genes[i];
                }
                else if (i < secondCrossoverPoint)
                {
                    childOne.Genes[i] = mother.Genes[i];
                    childTwo.Genes[i] = father.Genes[i];
                }
                else
                {
                    childOne.Genes[i] = father.Genes[i];
                    childTwo.Genes[i] = mother.Genes[i];
                }
            }

            if (configuration.GetNextDouble() < 0.5)
            {
                return childOne;
            }
            else
            {
                return childTwo;
            }
        }
    }
}
