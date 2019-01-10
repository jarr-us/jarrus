using Jarrus.GA.BasicTypes.Chromosomes;
using Jarrus.GA.BasicTypes.Genes;
using System.Collections.Generic;

namespace Jarrus.GA.Crossovers.Ordered
{
    /// <summary>
    /// OX1
    /// </summary>
    public class OrderCrossover : Crossover
    {
        protected override Chromosome Perform(Chromosome father, Chromosome mother, GAConfiguration settings)
        {
            var geneCount = father.Genes.Length;
            var child = new OrderedChromosome(geneCount);
            var crossoverPoint = settings.GetRandomInteger(1, father.Genes.Length - 2);

            var seen = new HashSet<Gene>();

            for (int i = 0; i < crossoverPoint; i++)
            {
                child.Genes[i] = father.Genes[i];
                seen.Add(father.Genes[i]);                
            }

            var count = 0;
            for (int i = 0; i < geneCount; i++)
            {
                if (!seen.Contains(mother.Genes[i]))
                {
                    child.Genes[crossoverPoint + count] = mother.Genes[i];
                    seen.Add(mother.Genes[i]);
                    count++;
                }
            }

            return child;
        }
    }
}
