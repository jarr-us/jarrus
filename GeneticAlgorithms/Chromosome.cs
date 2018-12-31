using GeneticAlgorithms.Enums;
using System.Collections.Generic;
using System.Diagnostics;

namespace GeneticAlgorithms
{
    [DebuggerDisplay("{FirstName} {LastName} {FitnessScore}")]
    public class Chromosome<T>
    {
        public int Age, GenerationNumber;
        public T[] Genes;
        public double FitnessScore;
        public int Children;
        public FirstName FirstName;
        public LastName LastName;

        public List<Chromosome<T>> Parents = new List<Chromosome<T>>();
        public List<LastName> Lineage = new List<LastName>();

        public Chromosome(int geneSize)
        {
            Genes = new T[geneSize];
        }

        public Chromosome(params T[] genes)
        {
            Genes = (T[])genes.Clone();
        }

        public void SetParents(params Chromosome<T>[] parents)
        {
            foreach(var parent in parents)
            {
                Parents.Add(parent);
            }

            SetLineage();
        }

        private void SetLineage()
        {
            foreach(var parent in Parents)
            {
                Lineage.Add(parent.LastName);

                foreach(var grandparent in parent.Parents)
                {
                    Lineage.Add(grandparent.LastName);

                    foreach(var gg in grandparent.Parents)
                    {
                        Lineage.Add(gg.LastName);

                        foreach(var ggg in gg.Parents)
                        {
                            Lineage.Add(ggg.LastName);

                            foreach(var gggg in ggg.Parents)
                            {
                                Lineage.Add(gggg.LastName);
                            }
                        }
                    }
                }
            }
        }

        public bool ShouldRetire(GAConfiguration<T> settings)
        {
            return Age >= settings.MaximumLifeSpan && settings.MaximumLifeSpan != 0;
        }

        public override string ToString() { return string.Join(",", Genes); }
    }
}
