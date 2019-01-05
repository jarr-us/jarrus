using Baseball.Models;
using GeneticAlgorithms.BasicTypes;
using Jarrus.Display;
using Jarrus.Models;
using System;
using System.Deployment.Application;
using System.Threading;
using System.Windows.Forms;

namespace Jarrus
{
    public partial class MainForm : Form
    {
        public FormDisplay FormDisplay;
        public FormControls FControls;
        private bool _running = true;
        private Thread _procThread, _gaThread;

        public MainForm()
        {
            UpdateChecker.Check();
            InitializeComponent();

            SetupControls();

            //factory
            //updated task system
            //

            FormDisplay = new TaskRunnerDisplay(this, FControls);

            UpdateVersionLabel();
            StartProcessing();
        }

        private void SetupControls()
        {
            FControls = new FormControls();

            FControls.SessionNameLbl = sessionNameLbl;
            FControls.PoolScoreChart = poolScoreChart;
            FControls.CurrentBestLowestScoreLbl = currentBestLowestScoreLbl;
            FControls.CurrentBestFirstNameLbl = currentBestFirstNameLbl;
            FControls.CurrentBestLastNameLbl = currentBestLastNameLbl;
            FControls.CurrentBestDirectDescendentsLbl = currentBestDirectDescendentsLbl;

            FControls.GenerationLbl = generationLbl;
            FControls.GoatBestFirstNameLbl = goatBestFirstNameLbl;
            FControls.GoatBestLastNameLbl = goatBestLastNameLbl;
            FControls.GoatBestLowestScoreLbl = goatBestLowestScoreLbl;
            FControls.GoatDirectDescendentsLbl = goatDirectDescendentsLbl;
            FControls.RunsCompletedLbl = runsCompletedLbl;
            FControls.RetiredNumberLbl = retiredNumberLbl;
            FControls.MsPerGenLbl = msPerGenLbl;

            FControls.ConfigPoolSizeLbl = configPoolSizeLbl;
            FControls.ConfigIterationsLbl = configIterationsLbl;
            FControls.ConfigCrossoverRateLbl = configCrossoverRateLbl;
            FControls.ConfigMutationRateLbl = configMutationRateLbl;
            FControls.ConfigElitismRateLbl = configElitismRateLbl;
            FControls.ConfigMaxLifeLbl = configMaxLifeLbl;
            FControls.ConfigChildrenPerCoupleLbl = configChildrenPerCoupleLbl;
            FControls.ConfigParentSelectionLbl = configParentSelectionLbl;
            FControls.ConfigCrossoverLbl = configCrossoverLbl;
            FControls.ConfigMutationLbl = configMutationLbl;
            FControls.ConfigRetirementLbl = configRetirementLbl;

            FControls.Family1Lbl = family1Lbl;
            FControls.Family2Lbl = family2Lbl;
            FControls.Family3Lbl = family3Lbl;
            FControls.Family4Lbl = family4Lbl;
            FControls.Family5Lbl = family5Lbl;
            FControls.Family1ProgressBar = lastName1ProgressBar;
            FControls.Family2ProgressBar = lastName2ProgressBar;
            FControls.Family3ProgressBar = lastName3ProgressBar;
            FControls.Family4ProgressBar = lastName4ProgressBar;
            FControls.Family5ProgressBar = lastName5ProgressBar;
        }

        private void StartProcessing()
        {
            _procThread = new Thread(DisplayLoop);
            _procThread.Start();

            _gaThread = new Thread(GALoop);
            _gaThread.Start();
        }

        private void UpdateVersionLabel()
        {
            if (ApplicationDeployment.IsNetworkDeployed)
            {
                versionLbl.Text = ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
            }
        }

        private void GALoop() { while (_running) { RunGAIteration(); UpdateChecker.Check(); } }
        private void RunGAIteration() { FormDisplay.RunIteration(); }
        private void DisplayLoop() { while (_running) { UpdateDisplay(); Thread.Sleep(50); } }
        private void UpdateDisplay() { if (!FormDisplay.IsReadyToUpdateForm()) { return; } FormDisplay.Update(); }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e) { Application.Exit(); }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e) {
            try { _procThread.Interrupt(); } catch (Exception) { }
            try { _gaThread.Interrupt(); } catch (Exception) { }
        }
    }
}
