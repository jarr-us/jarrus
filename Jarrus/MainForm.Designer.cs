namespace Jarrus
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.versionLbl = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lastNameLbl = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.msPerGenLbl = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.runsCompletedLbl = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.firstNameLbl = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lowestScoreLbl = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.generationLbl = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.family10Lbl = new System.Windows.Forms.Label();
            this.family9Lbl = new System.Windows.Forms.Label();
            this.family8Lbl = new System.Windows.Forms.Label();
            this.family7Lbl = new System.Windows.Forms.Label();
            this.family6Lbl = new System.Windows.Forms.Label();
            this.family5Lbl = new System.Windows.Forms.Label();
            this.family4Lbl = new System.Windows.Forms.Label();
            this.family1Lbl = new System.Windows.Forms.Label();
            this.family3Lbl = new System.Windows.Forms.Label();
            this.family2Lbl = new System.Windows.Forms.Label();
            this.lastName1ProgressBar = new System.Windows.Forms.ProgressBar();
            this.lastName2ProgressBar = new System.Windows.Forms.ProgressBar();
            this.lastName3ProgressBar = new System.Windows.Forms.ProgressBar();
            this.lastName4ProgressBar = new System.Windows.Forms.ProgressBar();
            this.lastName5ProgressBar = new System.Windows.Forms.ProgressBar();
            this.lastName6ProgressBar = new System.Windows.Forms.ProgressBar();
            this.lastName7ProgressBar = new System.Windows.Forms.ProgressBar();
            this.lastName8ProgressBar = new System.Windows.Forms.ProgressBar();
            this.lastName9ProgressBar = new System.Windows.Forms.ProgressBar();
            this.lastName10ProgressBar = new System.Windows.Forms.ProgressBar();
            this.menuStrip.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(429, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // versionLbl
            // 
            this.versionLbl.BackColor = System.Drawing.SystemColors.Control;
            this.versionLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.versionLbl.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.versionLbl.Location = new System.Drawing.Point(305, 194);
            this.versionLbl.Name = "versionLbl";
            this.versionLbl.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.versionLbl.Size = new System.Drawing.Size(112, 24);
            this.versionLbl.TabIndex = 1;
            this.versionLbl.Text = "Debug Mode";
            this.versionLbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Generation";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lastNameLbl);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.msPerGenLbl);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.runsCompletedLbl);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.firstNameLbl);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.lowestScoreLbl);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.generationLbl);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(195, 164);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Run Details";
            // 
            // lastNameLbl
            // 
            this.lastNameLbl.Location = new System.Drawing.Point(97, 79);
            this.lastNameLbl.Name = "lastNameLbl";
            this.lastNameLbl.Size = new System.Drawing.Size(89, 13);
            this.lastNameLbl.TabIndex = 13;
            this.lastNameLbl.Text = "0";
            this.lastNameLbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 79);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Last Name";
            // 
            // msPerGenLbl
            // 
            this.msPerGenLbl.Location = new System.Drawing.Point(97, 119);
            this.msPerGenLbl.Name = "msPerGenLbl";
            this.msPerGenLbl.Size = new System.Drawing.Size(89, 13);
            this.msPerGenLbl.TabIndex = 11;
            this.msPerGenLbl.Text = "0";
            this.msPerGenLbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 119);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(86, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "MS / Generation";
            // 
            // runsCompletedLbl
            // 
            this.runsCompletedLbl.Location = new System.Drawing.Point(97, 16);
            this.runsCompletedLbl.Name = "runsCompletedLbl";
            this.runsCompletedLbl.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.runsCompletedLbl.Size = new System.Drawing.Size(89, 13);
            this.runsCompletedLbl.TabIndex = 9;
            this.runsCompletedLbl.Text = "0";
            this.runsCompletedLbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Run Number";
            // 
            // firstNameLbl
            // 
            this.firstNameLbl.Location = new System.Drawing.Point(97, 66);
            this.firstNameLbl.Name = "firstNameLbl";
            this.firstNameLbl.Size = new System.Drawing.Size(89, 13);
            this.firstNameLbl.TabIndex = 7;
            this.firstNameLbl.Text = "0";
            this.firstNameLbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 66);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "First Name";
            // 
            // lowestScoreLbl
            // 
            this.lowestScoreLbl.Location = new System.Drawing.Point(97, 53);
            this.lowestScoreLbl.Name = "lowestScoreLbl";
            this.lowestScoreLbl.Size = new System.Drawing.Size(89, 13);
            this.lowestScoreLbl.TabIndex = 5;
            this.lowestScoreLbl.Text = "0";
            this.lowestScoreLbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Lowest Score";
            // 
            // generationLbl
            // 
            this.generationLbl.Location = new System.Drawing.Point(97, 29);
            this.generationLbl.Name = "generationLbl";
            this.generationLbl.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.generationLbl.Size = new System.Drawing.Size(89, 13);
            this.generationLbl.TabIndex = 3;
            this.generationLbl.Text = "0";
            this.generationLbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lastName10ProgressBar);
            this.groupBox2.Controls.Add(this.lastName9ProgressBar);
            this.groupBox2.Controls.Add(this.lastName8ProgressBar);
            this.groupBox2.Controls.Add(this.lastName7ProgressBar);
            this.groupBox2.Controls.Add(this.lastName6ProgressBar);
            this.groupBox2.Controls.Add(this.lastName5ProgressBar);
            this.groupBox2.Controls.Add(this.lastName4ProgressBar);
            this.groupBox2.Controls.Add(this.lastName3ProgressBar);
            this.groupBox2.Controls.Add(this.lastName2ProgressBar);
            this.groupBox2.Controls.Add(this.lastName1ProgressBar);
            this.groupBox2.Controls.Add(this.family10Lbl);
            this.groupBox2.Controls.Add(this.family9Lbl);
            this.groupBox2.Controls.Add(this.family8Lbl);
            this.groupBox2.Controls.Add(this.family7Lbl);
            this.groupBox2.Controls.Add(this.family6Lbl);
            this.groupBox2.Controls.Add(this.family5Lbl);
            this.groupBox2.Controls.Add(this.family4Lbl);
            this.groupBox2.Controls.Add(this.family1Lbl);
            this.groupBox2.Controls.Add(this.family3Lbl);
            this.groupBox2.Controls.Add(this.family2Lbl);
            this.groupBox2.Location = new System.Drawing.Point(213, 27);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 164);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Family Strength";
            // 
            // family10Lbl
            // 
            this.family10Lbl.AutoSize = true;
            this.family10Lbl.Location = new System.Drawing.Point(6, 132);
            this.family10Lbl.Name = "family10Lbl";
            this.family10Lbl.Size = new System.Drawing.Size(80, 13);
            this.family10Lbl.TabIndex = 32;
            this.family10Lbl.Text = "#10 Last Name";
            // 
            // family9Lbl
            // 
            this.family9Lbl.AutoSize = true;
            this.family9Lbl.Location = new System.Drawing.Point(6, 119);
            this.family9Lbl.Name = "family9Lbl";
            this.family9Lbl.Size = new System.Drawing.Size(74, 13);
            this.family9Lbl.TabIndex = 30;
            this.family9Lbl.Text = "#9 Last Name";
            // 
            // family8Lbl
            // 
            this.family8Lbl.AutoSize = true;
            this.family8Lbl.Location = new System.Drawing.Point(6, 106);
            this.family8Lbl.Name = "family8Lbl";
            this.family8Lbl.Size = new System.Drawing.Size(74, 13);
            this.family8Lbl.TabIndex = 28;
            this.family8Lbl.Text = "#8 Last Name";
            // 
            // family7Lbl
            // 
            this.family7Lbl.AutoSize = true;
            this.family7Lbl.Location = new System.Drawing.Point(6, 93);
            this.family7Lbl.Name = "family7Lbl";
            this.family7Lbl.Size = new System.Drawing.Size(74, 13);
            this.family7Lbl.TabIndex = 26;
            this.family7Lbl.Text = "#7 Last Name";
            // 
            // family6Lbl
            // 
            this.family6Lbl.AutoSize = true;
            this.family6Lbl.Location = new System.Drawing.Point(6, 81);
            this.family6Lbl.Name = "family6Lbl";
            this.family6Lbl.Size = new System.Drawing.Size(74, 13);
            this.family6Lbl.TabIndex = 24;
            this.family6Lbl.Text = "#6 Last Name";
            // 
            // family5Lbl
            // 
            this.family5Lbl.AutoSize = true;
            this.family5Lbl.Location = new System.Drawing.Point(6, 68);
            this.family5Lbl.Name = "family5Lbl";
            this.family5Lbl.Size = new System.Drawing.Size(74, 13);
            this.family5Lbl.TabIndex = 22;
            this.family5Lbl.Text = "#5 Last Name";
            // 
            // family4Lbl
            // 
            this.family4Lbl.AutoSize = true;
            this.family4Lbl.Location = new System.Drawing.Point(6, 55);
            this.family4Lbl.Name = "family4Lbl";
            this.family4Lbl.Size = new System.Drawing.Size(74, 13);
            this.family4Lbl.TabIndex = 20;
            this.family4Lbl.Text = "#4 Last Name";
            // 
            // family1Lbl
            // 
            this.family1Lbl.AutoSize = true;
            this.family1Lbl.Location = new System.Drawing.Point(6, 16);
            this.family1Lbl.Name = "family1Lbl";
            this.family1Lbl.Size = new System.Drawing.Size(74, 13);
            this.family1Lbl.TabIndex = 14;
            this.family1Lbl.Text = "#1 Last Name";
            // 
            // family3Lbl
            // 
            this.family3Lbl.AutoSize = true;
            this.family3Lbl.Location = new System.Drawing.Point(6, 42);
            this.family3Lbl.Name = "family3Lbl";
            this.family3Lbl.Size = new System.Drawing.Size(74, 13);
            this.family3Lbl.TabIndex = 18;
            this.family3Lbl.Text = "#3 Last Name";
            // 
            // family2Lbl
            // 
            this.family2Lbl.AutoSize = true;
            this.family2Lbl.Location = new System.Drawing.Point(6, 29);
            this.family2Lbl.Name = "family2Lbl";
            this.family2Lbl.Size = new System.Drawing.Size(74, 13);
            this.family2Lbl.TabIndex = 16;
            this.family2Lbl.Text = "#2 Last Name";
            // 
            // lastName1ProgressBar
            // 
            this.lastName1ProgressBar.Location = new System.Drawing.Point(100, 19);
            this.lastName1ProgressBar.Name = "lastName1ProgressBar";
            this.lastName1ProgressBar.Size = new System.Drawing.Size(86, 10);
            this.lastName1ProgressBar.TabIndex = 5;
            // 
            // lastName2ProgressBar
            // 
            this.lastName2ProgressBar.Location = new System.Drawing.Point(100, 32);
            this.lastName2ProgressBar.Name = "lastName2ProgressBar";
            this.lastName2ProgressBar.Size = new System.Drawing.Size(86, 10);
            this.lastName2ProgressBar.TabIndex = 33;
            // 
            // lastName3ProgressBar
            // 
            this.lastName3ProgressBar.Location = new System.Drawing.Point(100, 45);
            this.lastName3ProgressBar.Name = "lastName3ProgressBar";
            this.lastName3ProgressBar.Size = new System.Drawing.Size(86, 10);
            this.lastName3ProgressBar.TabIndex = 34;
            // 
            // lastName4ProgressBar
            // 
            this.lastName4ProgressBar.Location = new System.Drawing.Point(100, 58);
            this.lastName4ProgressBar.Name = "lastName4ProgressBar";
            this.lastName4ProgressBar.Size = new System.Drawing.Size(86, 10);
            this.lastName4ProgressBar.TabIndex = 35;
            // 
            // lastName5ProgressBar
            // 
            this.lastName5ProgressBar.Location = new System.Drawing.Point(100, 71);
            this.lastName5ProgressBar.Name = "lastName5ProgressBar";
            this.lastName5ProgressBar.Size = new System.Drawing.Size(86, 10);
            this.lastName5ProgressBar.TabIndex = 36;
            // 
            // lastName6ProgressBar
            // 
            this.lastName6ProgressBar.Location = new System.Drawing.Point(100, 84);
            this.lastName6ProgressBar.Name = "lastName6ProgressBar";
            this.lastName6ProgressBar.Size = new System.Drawing.Size(86, 10);
            this.lastName6ProgressBar.TabIndex = 37;
            // 
            // lastName7ProgressBar
            // 
            this.lastName7ProgressBar.Location = new System.Drawing.Point(100, 96);
            this.lastName7ProgressBar.Name = "lastName7ProgressBar";
            this.lastName7ProgressBar.Size = new System.Drawing.Size(86, 10);
            this.lastName7ProgressBar.TabIndex = 38;
            // 
            // lastName8ProgressBar
            // 
            this.lastName8ProgressBar.Location = new System.Drawing.Point(100, 109);
            this.lastName8ProgressBar.Name = "lastName8ProgressBar";
            this.lastName8ProgressBar.Size = new System.Drawing.Size(86, 10);
            this.lastName8ProgressBar.TabIndex = 39;
            // 
            // lastName9ProgressBar
            // 
            this.lastName9ProgressBar.Location = new System.Drawing.Point(100, 122);
            this.lastName9ProgressBar.Name = "lastName9ProgressBar";
            this.lastName9ProgressBar.Size = new System.Drawing.Size(86, 10);
            this.lastName9ProgressBar.TabIndex = 40;
            // 
            // lastName10ProgressBar
            // 
            this.lastName10ProgressBar.Location = new System.Drawing.Point(100, 135);
            this.lastName10ProgressBar.Name = "lastName10ProgressBar";
            this.lastName10ProgressBar.Size = new System.Drawing.Size(86, 10);
            this.lastName10ProgressBar.TabIndex = 41;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(429, 225);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.versionLbl);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainForm";
            this.Text = "Jarrus";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Label versionLbl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label generationLbl;
        private System.Windows.Forms.Label firstNameLbl;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lowestScoreLbl;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label runsCompletedLbl;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label msPerGenLbl;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lastNameLbl;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label family10Lbl;
        private System.Windows.Forms.Label family9Lbl;
        private System.Windows.Forms.Label family8Lbl;
        private System.Windows.Forms.Label family7Lbl;
        private System.Windows.Forms.Label family6Lbl;
        private System.Windows.Forms.Label family5Lbl;
        private System.Windows.Forms.Label family4Lbl;
        private System.Windows.Forms.Label family1Lbl;
        private System.Windows.Forms.Label family3Lbl;
        private System.Windows.Forms.Label family2Lbl;
        private System.Windows.Forms.ProgressBar lastName1ProgressBar;
        private System.Windows.Forms.ProgressBar lastName10ProgressBar;
        private System.Windows.Forms.ProgressBar lastName9ProgressBar;
        private System.Windows.Forms.ProgressBar lastName8ProgressBar;
        private System.Windows.Forms.ProgressBar lastName7ProgressBar;
        private System.Windows.Forms.ProgressBar lastName6ProgressBar;
        private System.Windows.Forms.ProgressBar lastName5ProgressBar;
        private System.Windows.Forms.ProgressBar lastName4ProgressBar;
        private System.Windows.Forms.ProgressBar lastName3ProgressBar;
        private System.Windows.Forms.ProgressBar lastName2ProgressBar;
    }
}

