using GeneticAlgorithms.BasicTypes;
using System;

namespace GeneticAlgorithms.ParentSelections
{
    public class StochasticUniversalSamplingSelection : RankingWheel
    {
        public StochasticUniversalSamplingSelection() { }

        public override ChromosomeParents GetParents()
        {
            var spinValue = Configuration.GetNextDouble();
            var oppositeSideValue = Math.Abs(spinValue - 0.5);

            return new ChromosomeParents
            {
                Father = GetParent(spinValue),
                Mother = GetParent(oppositeSideValue)
            };
        }
    }
}

