﻿using GeneticAlgorithms.BasicTypes;
using GeneticAlgorithms.Utility;
using System.Collections.Generic;

namespace GeneticAlgorithms.Crossovers.Ordered
{
    public class CycleCrossover : Crossover
    {
        public List<int> Cycle = new List<int>();

        protected override Chromosome Perform(Chromosome father, Chromosome mother, GAConfiguration settings)
        {
            var geneCount = father.Genes.Length;
            
            DetermineCycle(father, mother);
            var child = new Chromosome(mother.Genes);
            PlaceCycleInsideOfChild(father, child);

            return child;
        }

        private void DetermineCycle(Chromosome father, Chromosome mother)
        {
            Cycle = new List<int>();
            var index = 0;
            var fatherPoint = father.Genes[index];

            while (index != -1 && !Cycle.Contains(index))
            {               
                Cycle.Add(index);

                index = father.Genes.IndexOf(o => o.GetHashCode() == mother.Genes[index].GetHashCode());
                fatherPoint = father.Genes[index];
            }
        }

        private void PlaceCycleInsideOfChild(Chromosome father, Chromosome child)
        {
            foreach(var index in Cycle)
            {
                child.Genes[index] = father.Genes[index];
            }
        }
    }
}
