﻿using Baseball.Models;
using GeneticAlgorithms;
using Jarrus.Models;
using Kanan;
using Kanan.Models;
using System;
using System.Deployment.Application;
using System.Threading;
using System.Windows.Forms;

namespace Jarrus
{
    public partial class MainForm : Form
    {
        private MLBCircleIteration _mlbIteration;
        public static int RunNumber;

        public MainForm()
        {
            UpdateChecker.Check();

            InitializeComponent();
            UpdateVersionLabel();
            StartProcessing();
        }

        private void StartProcessing()
        {
            var procThread = new Thread(ProcessLoop);
            procThread.Start();

            var kananThread = new Thread(KananLoop);
            kananThread.Start();
        }

        private void KananLoop()
        {
            while(true)
            {
                RunNumber++;
                RunKanan();
                UpdateChecker.Check();
            }
        }

        private void RunKanan()
        {
            _mlbIteration = new MLBCircleIteration();
            _mlbIteration.Run();
        }

        private void ProcessLoop()
        {
            while(true)
            {
                Process();
                Thread.Sleep(50);
            }            
        }

        private void Process()
        {
            if (_mlbIteration == null || _mlbIteration.GARun == null) { return; }

            TextUpdater.SetText(this, generationLbl, _mlbIteration.GARun.CurrentGeneration + "");
            TextUpdater.SetText(this, highestScoreLbl, _mlbIteration.GARun.HighestScore + "");
            TextUpdater.SetText(this, lowestScoreLbl, _mlbIteration.GARun.LowestScore + "");
            TextUpdater.SetText(this, runsCompletedLbl, RunNumber + "");
        }
        
        private void UpdateVersionLabel()
        {
            if (ApplicationDeployment.IsNetworkDeployed)
            {
                versionLbl.Text = ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
