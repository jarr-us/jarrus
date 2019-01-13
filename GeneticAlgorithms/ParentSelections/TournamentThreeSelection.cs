namespace Jarrus.GA.ParentSelections
{
    public class TournamentThreeSelection : TournamentSelection
    {
        private const int NUMBER_OF_COMPETITORS = 3;
        public override ChromosomeParents GetParents() { return GetParentsForKSelection(NUMBER_OF_COMPETITORS); }
    }
}
