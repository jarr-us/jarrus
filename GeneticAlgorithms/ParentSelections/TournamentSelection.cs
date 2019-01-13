using Jarrus.GA.Models;
using System.Collections.Generic;
using System.Linq;

namespace Jarrus.GA.ParentSelections
{
    public abstract class TournamentSelection : ParentSelection
    {
        public ChromosomeParents GetParentsForKSelection(int k)
        {
            var father = GetParent(k);
            var mother = GetParent(k);

            while (mother == null || ReferenceEquals(mother, father)) { mother = GetParent(k); }

            return new ChromosomeParents
            {
                Father = father,
                Mother = mother
            };
        }

        private Chromosome GetParent(int kSelection)
        {
            var possibleParents = SelectParents(kSelection);
            return possibleParents.OrderByDescending(o => o.AdjustedFitnessScore).FirstOrDefault();
        }

        private List<Chromosome> SelectParents(int kSelection)
        {
            var options = new List<Chromosome>();

            for (int i = 0; i < kSelection; i++)
            {
                options.Add(Population[Configuration.GetRandomInteger(0, Population.Length - 1)]);
            }            

            return options;
        }

        protected override void SetupSelection() { }
    }
}
