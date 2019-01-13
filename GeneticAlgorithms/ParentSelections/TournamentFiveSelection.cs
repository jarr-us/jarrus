namespace Jarrus.GA.ParentSelections
{
    public class TournamentFiveSelection : TournamentSelection
    {
        private const int NUMBER_OF_COMPETITORS = 5;
        public override ChromosomeParents GetParents() { return GetParentsForKSelection(NUMBER_OF_COMPETITORS); }
    }
}
