using System.Collections.Generic;

namespace GeneticAlgorithms.Crossovers.Ordered
{
    public class AlternatingPositionCrossover : Crossover
    {
        protected override Chromosome<T> Perform<T>(Chromosome<T> father, Chromosome<T> mother, GAConfiguration<T> settings)
        {
            var geneCount = father.Genes.Length;
            var child = new Chromosome<T>(geneCount);

            var seen = new List<T>();

            for (int i = 0; i < geneCount; i++)
            {
                if (IsOdd(i))
                {
                    for (int k = 0; k < geneCount; k++)
                    {
                        if (!seen.Contains(mother.Genes[k]))
                        {
                            child.Genes[i] = mother.Genes[k];
                            seen.Add(child.Genes[i]);
                            break;
                        }
                    }
                } else
                {
                    for (int k = 0; k < geneCount; k++)
                    {
                        if (!seen.Contains(father.Genes[k]))
                        {
                            child.Genes[i] = father.Genes[k];
                            seen.Add(child.Genes[i]);
                            break;
                        }
                    }                    
                }
            }

            return child;
        }

        private static bool IsOdd(int value)
        {
            return value % 2 != 0;
        }
    }
}
