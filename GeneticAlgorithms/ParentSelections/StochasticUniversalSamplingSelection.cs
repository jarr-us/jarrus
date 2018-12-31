using GeneticAlgorithms.BasicTypes;
using System;

namespace GeneticAlgorithms.ParentSelections
{
    public class StochasticUniversalSamplingSelection<T> : RankingWheel<T> where T : Gene
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

