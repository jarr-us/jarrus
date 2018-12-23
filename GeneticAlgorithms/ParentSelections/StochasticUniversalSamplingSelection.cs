using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithms.ParentSelections
{
    public class StochasticUniversalSamplingSelection<T> : RankingWheel<T>
    {
        public StochasticUniversalSamplingSelection() { }

        public override ChromosomeParents<T> GetParents()
        {
            var spinValue = Configuration.GetNextDouble();
            var oppositeSideValue = Math.Abs(spinValue - 0.5);

            return new ChromosomeParents<T>
            {
                Father = GetParent(spinValue),
                Mother = GetParent(oppositeSideValue)
            };
        }
    }
}

