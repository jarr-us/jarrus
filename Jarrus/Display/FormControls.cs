using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Jarrus.Display
{
    public class FormControls
    {
        public Label SessionNameLbl;
        public Label CurrentBestLowestScoreLbl;
        public Label CurrentBestFirstNameLbl;
        public Label CurrentBestLastNameLbl;
        public Label CurrentBestDirectDescendentsLbl;
        public Label GenerationLbl;
        public Label GoatBestFirstNameLbl;
        public Label GoatBestLastNameLbl;
        public Label GoatBestLowestScoreLbl;
        public Label GoatDirectDescendentsLbl;
        public Label RunsCompletedLbl;
        public Label RetiredNumberLbl;
        public Label TicksPerChromosome;
        public Label ConfigPoolSizeLbl;
        public Label ConfigIterationsLbl;
        public Label ConfigCrossoverRateLbl;
        public Label ConfigMutationRateLbl;
        public Label ConfigElitismRateLbl;
        public Label ConfigMaxLifeLbl;
        public Label ConfigChildrenPerCoupleLbl;
        public Label ConfigParentSelectionLbl;
        public Label ConfigCrossoverLbl;
        public Label ConfigMutationLbl;
        public Label ConfigRetirementLbl;
        public Label MillisecondsPerGeneration;
        public Label SolutionNameLbl;

        public Label Family1Lbl, Family2Lbl, Family3Lbl, Family4Lbl, Family5Lbl;
        public ProgressBar Family1ProgressBar, Family2ProgressBar, Family3ProgressBar, Family4ProgressBar, Family5ProgressBar;
        public Chart PoolScoreChart;
    }
}
