using Jarrus.GA.Models;
using System;
using System.Linq;

namespace Jarrus.GA.ParentSelections
{
    public abstract class ParentSelection
    {
        protected GAConfiguration Configuration;
        protected Chromosome[] Population;

        public void Setup(Chromosome[] pop, GAConfiguration settings)
        {
            Configuration = settings;
            Population = pop;
            SetupSelection();
            Validate();
        }

        protected abstract void SetupSelection();

        private void Validate()
        {
            if (Population.Min(o => o.FitnessScore) < 0)
            {
                throw new ArgumentException("Unable to work with Genomes with negative fitness scores.");
            }
        }


        public abstract ChromosomeParents GetParents();
    }
}
