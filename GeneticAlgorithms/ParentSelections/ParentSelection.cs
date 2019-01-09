using Jarrus.GA.BasicTypes;
using System;
using System.Linq;

namespace Jarrus.GA.ParentSelections
{
    public abstract class ParentSelection
    {
        protected GAConfiguration Configuration;
        protected Chromosome[] Genome;

        public void Setup(Chromosome[] genome, GAConfiguration settings)
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


        public abstract ChromosomeParents GetParents();
    }
}
