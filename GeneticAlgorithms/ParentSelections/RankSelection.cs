namespace Jarrus.GA.ParentSelections
{
    public class RankSelection : WheelSelection
    {
        public override ChromosomeParents GetParents()
        {
            var father = GetParent(Configuration.GetNextDouble());
            var mother = GetParent(Configuration.GetNextDouble());

            while (ReferenceEquals(mother, father)) { mother = GetParent(Configuration.GetNextDouble()); }

            return new ChromosomeParents
            {
                Father = father,
                Mother = mother
            };
        }
    }
}
