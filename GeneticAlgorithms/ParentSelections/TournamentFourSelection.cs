namespace Jarrus.GA.ParentSelections
{
    public class TournamentFourSelection : TournamentSelection
    {
        private const int NUMBER_OF_COMPETITORS = 4;
        public override ChromosomeParents GetParents() { return GetParentsForKSelection(NUMBER_OF_COMPETITORS); }
    }
}
