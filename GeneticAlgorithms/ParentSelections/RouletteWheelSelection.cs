namespace GeneticAlgorithms.ParentSelections
{
    public class RouletteWheelSelection : RankingWheel
    {
        public override ChromosomeParents GetParents()
        {
            var father = GetParent(Configuration.GetNextDouble());
            var mother = GetParent(Configuration.GetNextDouble());

            while (mother == father) { mother = GetParent(Configuration.GetNextDouble()); }

            return new ChromosomeParents
            {
                Father = father,
                Mother = mother
            };
        }
    }
}
