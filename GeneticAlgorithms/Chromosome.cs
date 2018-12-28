using GeneticAlgorithms.Enums;
using System.Collections.Generic;
using System.Diagnostics;

namespace GeneticAlgorithms
{
    [DebuggerDisplay("{FitnessScore}")]
    public class Chromosome<T>
    {
        public int Age, GenerationNumber;
        public T[] Genes;
        public double FitnessScore;
        public int Children;
        public FirstName FirstName;
        public LastName LastName;
        public List<LastName> ParentsLastNames = new List<LastName>();

        public Chromosome(int geneSize)
        {
            Genes = new T[geneSize];
        }

        public Chromosome(params T[] genes)
        {
            Genes = (T[])genes.Clone();
        }

        public bool ShouldRetire(GAConfiguration<T> settings)
        {
            return Age >= settings.MaximumLifeSpan && settings.MaximumLifeSpan != 0;
        }

        public override string ToString() { return string.Join(",", Genes); }
    }
}
