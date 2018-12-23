﻿namespace GeneticAlgorithms.ParentSelections
{
    public class RouletteWheelSelection<T> : RankingWheel<T>
    {
        public override ChromosomeParents<T> GetParents()
        {
            var father = GetParent(Configuration.GetNextDouble());
            var mother = GetParent(Configuration.GetNextDouble());

            while (mother == father) { mother = GetParent(Configuration.GetNextDouble()); }

            return new ChromosomeParents<T>
            {
                Father = father,
                Mother = mother
            };
        }
    }
}
