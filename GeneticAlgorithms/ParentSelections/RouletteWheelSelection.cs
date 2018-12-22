namespace GeneticAlgorithms.ParentSelections
{
    public class RouletteWheelSelection<T> : RankingWheel<T>
    {
        public override ChromosomeParents<T> GetParents()
        {
            var father = GetParent(Settings.GetNextDouble());
            var mother = GetParent(Settings.GetNextDouble());

            while (mother == father) { mother = GetParent(Settings.GetNextDouble()); }

            return new ChromosomeParents<T>
            {
                Father = father,
                Mother = mother
            };
        }
    }
}
