using System;
using System.Linq;

namespace GeneticAlgorithms.ParentSelections
{
    public abstract class ParentSelection<T>
    {
        protected GAConfiguration<T> Configuration;
        protected Chromosome<T>[] Genome;

        public void Setup(Chromosome<T>[] genome, GAConfiguration<T> settings)
        {
            Configuration = settings;
            Genome = genome;
            SetupSelection();
            Validate();
        }

        protected abstract void SetupSelection();

        private void Validate()
        {
            if (Genome.Min(o => o.FitnessScore) < 0)
            {
                throw new ArgumentException("Unable to work with Genomes with negative fitness scores.");
            }
        }


        public abstract ChromosomeParents<T> GetParents();
    }
}
