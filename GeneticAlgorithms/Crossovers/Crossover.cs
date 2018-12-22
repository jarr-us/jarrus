using System;

namespace GeneticAlgorithms.Crossovers
{
    public abstract class Crossover
    {
        private const string INVALID_CHROMOSOME_SIZE = "The father and mother must be initialized, have at least two genes, and must be of the same size";

        public Chromosome<T> Execute<T>(Chromosome<T> father, Chromosome<T> mother, GAConfiguration<T> settings)
        {
            if (father == null || mother == null) { throw new ArgumentException(INVALID_CHROMOSOME_SIZE); }
            if (father.Genes == null || mother.Genes == null) { throw new ArgumentException(INVALID_CHROMOSOME_SIZE); }
            if (father.Genes.Length != mother.Genes.Length) { throw new ArgumentException(INVALID_CHROMOSOME_SIZE); }
            if (father.Genes.Length <= 1) { throw new ArgumentException(INVALID_CHROMOSOME_SIZE); }

            father.Children++;
            mother.Children++;

            return Perform<T>(father, mother, settings);
        }

        protected abstract Chromosome<T> Perform<T>(Chromosome<T> father, Chromosome<T> mother, GAConfiguration<T> settings);
    }
}
