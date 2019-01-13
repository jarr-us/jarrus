using Jarrus.GA.Models;

namespace Jarrus.GA.Crossovers.Unordered
{
    public class SinglePointCrossover : Crossover
    {
        protected override Chromosome Perform(Chromosome father, Chromosome mother, GAConfiguration configuration)
        {
            var geneCount = father.Genes.Length;

            var childOne = new UnorderedChromosome(geneCount);
            var childTwo = new UnorderedChromosome(geneCount);

            var crossoverPoint = configuration.GetRandomInteger(1, father.Genes.Length - 1);

            for (int i = 0; i < geneCount; i++)
            {
                if (i < crossoverPoint)
                {
                    childOne.Genes[i] = father.Genes[i];
                    childTwo.Genes[i] = mother.Genes[i];
                }
                else
                {
                    childOne.Genes[i] = mother.Genes[i];
                    childTwo.Genes[i] = father.Genes[i];
                }
            }

            if (configuration.GetNextDouble() < 0.5)
            {
                return childOne;
            } else
            {
                return childTwo;
            }            
        }
    }
}
