using GeneticAlgorithms.BasicTypes;
using GeneticAlgorithms.Utility;
using System.Collections.Generic;

namespace GeneticAlgorithms.Crossovers.Ordered
{
    public class CycleCrossover : Crossover
    {
        public List<int> Cycle = new List<int>();

        protected override Chromosome<T> Perform<T>(Chromosome<T> father, Chromosome<T> mother, GAConfiguration<T> settings)
        {
            var geneCount = father.Genes.Length;
            
            DetermineCycle(father, mother);
            var child = new Chromosome<T>(mother.Genes);
            PlaceCycleInsideOfChild(father, child);

            return child;
        }

        private void DetermineCycle<T>(Chromosome<T> father, Chromosome<T> mother) where T : Gene
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

        private void PlaceCycleInsideOfChild<T>(Chromosome<T> father, Chromosome<T> child) where T : Gene
        {
            foreach(var index in Cycle)
            {
                child.Genes[index] = father.Genes[index];
            }
        }
    }
}
