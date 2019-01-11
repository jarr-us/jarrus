﻿using Jarrus.GA.Models;
using System.Collections.Generic;

namespace Jarrus.GA.Crossovers.Ordered
{
    public class AlternatingPositionCrossover : Crossover
    {
        protected override Chromosome Perform(Chromosome father, Chromosome mother, GAConfiguration settings)
        {
            var geneCount = father.Genes.Length;
            var child = new OrderedChromosome(geneCount);

            var seen = new HashSet<Gene>();

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
