using GeneticAlgorithms.Utility;
using System;

namespace GeneticAlgorithms.Crossovers
{
    public abstract class Crossover
    {
        private const string INVALID_CHROMOSOME_SIZE = "The father and mother must be initialized, have at least two genes, and must be of the same size";

        public Chromosome<T> Execute<T>(Chromosome<T> father, Chromosome<T> mother, GAConfiguration<T> configuration)
        {
            if (father == null || mother == null) { throw new ArgumentException(INVALID_CHROMOSOME_SIZE); }
            if (father.Genes == null || mother.Genes == null) { throw new ArgumentException(INVALID_CHROMOSOME_SIZE); }
            if (father.Genes.Length != mother.Genes.Length) { throw new ArgumentException(INVALID_CHROMOSOME_SIZE); }
            if (father.Genes.Length <= 1) { throw new ArgumentException(INVALID_CHROMOSOME_SIZE); }

            father.Children++;
            mother.Children++;

            var child = Perform<T>(father, mother, configuration);

            child.LastName = father.LastName;
            child.FirstName = NameGenerator.GetFirstName(configuration.RandomPool);

            child.ParentsLastNames.Add(father.LastName);
            child.ParentsLastNames.Add(mother.LastName);

            return child;
        }

        public abstract int GetId();
        protected abstract Chromosome<T> Perform<T>(Chromosome<T> father, Chromosome<T> mother, GAConfiguration<T> settings);
    }
}
