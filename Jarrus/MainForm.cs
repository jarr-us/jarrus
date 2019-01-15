using Jarrus.Data;
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
        public TaskRunnerDisplay FormDisplay;
        public FormControls FControls;
        public static bool IsRunning = true;
        private Thread _procThread, _updateThread;

        // TODO: Threading now available again
        // TODO: API

        public MainForm()
        {            
            var mainThread = ThreadRunner.Instance.GetMainTaskThread();

            UpdateChecker.Check();
            InitializeComponent();

            StartThreads();
            SetupControls();            
            FormDisplay = new TaskRunnerDisplay(mainThread.TaskRunner, this, FControls);

            UpdateVersionLabel();
            StartProcessing();
        }

        private void StartThreads()
        {
            var computerName = Environment.MachineName;

            if (computerName.Equals("R2-D2") || computerName.Equals("K-2SO"))
            {
                threadNumberUpDown.Value = 2;
            } else
            {
                ThreadRunner.Instance.SetThreadCount((int)threadNumberUpDown.Value);
            }
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
            FControls.TicksPerChromosome = ticksPerChromosomeLbl;

            FControls.ConfigImmigrationRateLbl = configImmigrationRateLbl;
            FControls.ConfigImmigrationLbl = configImmigrationLbl;
            FControls.ConfigScoringLbl = configScoringLbl;
            FControls.ConfigDuplicationLbl = configDuplicationLbl;
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
            FControls.MillisecondsPerGeneration = msPerGenerationLbl;
            FControls.SolutionNameLbl = solutionNameLbl;

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

            FControls.TaskRepoQueuedTasks = tasksInQueueLbl;
            FControls.TaskRepoFinishedRuns = runsToInsertLbl;
        }

        private void StartProcessing()
        {
            _procThread = new Thread(DisplayLoop);
            _procThread.Start();

            _updateThread = new Thread(UpdateLoop);
            _updateThread.Start();
        }

        private void UpdateVersionLabel()
        {
            if (ApplicationDeployment.IsNetworkDeployed)
            {
                versionLbl.Text = ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
            }
        }

        private void DisplayLoop() { while (IsRunning) { UpdateDisplay(); Thread.Sleep(50); } }
        private void UpdateDisplay() { if (!FormDisplay.IsReadyToUpdateForm()) { return; } FormDisplay.Update(); }
        private void UpdateLoop() { while (IsRunning) { Thread.Sleep(6000); UpdateChecker.Check(); } }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e) { Application.Exit(); }

        private void threadNumberUpDown_ValueChanged(object sender, EventArgs e)
        {
            ThreadRunner.Instance.SetThreadCount((int)threadNumberUpDown.Value);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e) {
            IsRunning = false;
            ThreadRunner.Instance.Shutdown();
            try { _procThread.Interrupt(); } catch (Exception) { }
        }
    }
}
