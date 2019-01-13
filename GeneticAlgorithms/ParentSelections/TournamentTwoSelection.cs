namespace Jarrus.GA.ParentSelections
{
    public class TournamentTwoSelection : TournamentSelection
    {
        private const int NUMBER_OF_COMPETITORS = 2;
        public override ChromosomeParents GetParents() { return GetParentsForKSelection(NUMBER_OF_COMPETITORS); }
    }
}
