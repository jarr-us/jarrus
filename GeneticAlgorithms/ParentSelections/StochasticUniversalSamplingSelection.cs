using Jarrus.GA.Models;
using System;

namespace Jarrus.GA.ParentSelections
{
    public class StochasticUniversalSamplingSelection : FitnessProportionateSelection
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

