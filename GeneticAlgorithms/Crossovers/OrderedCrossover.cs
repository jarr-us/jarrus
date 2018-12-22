using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithms.Crossovers
{
    public class OrderedCrossover : Crossover
    {
        protected override Chromosome<T> Perform<T>(Chromosome<T> father, Chromosome<T> mother, GAConfiguration<T> settings)
        {
            var geneCount = father.Genes.Length;
            var child = new Chromosome<T>(geneCount);
            var crossoverPoint = settings.GetRandomInteger(1, father.Genes.Length - 2);

            var seen = new List<T>();

            for (int i = 0; i < geneCount; i++)
            {
                if (i < crossoverPoint)
                {
                    child.Genes[i] = father.Genes[i];
                    seen.Add(father.Genes[i]);
                }
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
