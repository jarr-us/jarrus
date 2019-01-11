using Jarrus.GA.Models;
using Jarrus.GA.Utility;
using System;

namespace Jarrus.GA.Crossovers
{
    public abstract class Crossover
    {
        private const string INVALID_CHROMOSOME_SIZE = "The father and mother must be initialized, have at least two genes, and must be of the same size";

        public Chromosome Execute(Chromosome father, Chromosome mother, GAConfiguration configuration)
        {
            if (father == null || mother == null) { throw new ArgumentException(INVALID_CHROMOSOME_SIZE); }
            if (father.Genes == null || mother.Genes == null) { throw new ArgumentException(INVALID_CHROMOSOME_SIZE); }
            if (father.Genes.Length != mother.Genes.Length) { throw new ArgumentException(INVALID_CHROMOSOME_SIZE); }
            if (father.Genes.Length <= 1) { throw new ArgumentException(INVALID_CHROMOSOME_SIZE); }

            father.Children++;
            mother.Children++;

            var child = Perform(father, mother, configuration);

            child.LastName = father.LastName;
            child.FirstName = NameGenerator.GetFirstName(configuration.RandomFirstNameSeed);

            child.SetParents(father, mother);

            return child;
        }

        protected abstract Chromosome Perform(Chromosome father, Chromosome mother, GAConfiguration settings);
    }
}
