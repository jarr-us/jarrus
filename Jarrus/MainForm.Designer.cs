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
            this.runsCompletedLbl = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.highestScoreLbl = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lowestScoreLbl = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.generationLbl = new System.Windows.Forms.Label();
            this.msPerGenLbl = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.menuStrip.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(219, 24);
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
            this.versionLbl.Location = new System.Drawing.Point(98, 255);
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
            this.groupBox1.Controls.Add(this.msPerGenLbl);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.runsCompletedLbl);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.highestScoreLbl);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.lowestScoreLbl);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.generationLbl);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(195, 225);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Run Details";
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
            // highestScoreLbl
            // 
            this.highestScoreLbl.Location = new System.Drawing.Point(97, 55);
            this.highestScoreLbl.Name = "highestScoreLbl";
            this.highestScoreLbl.Size = new System.Drawing.Size(89, 13);
            this.highestScoreLbl.TabIndex = 7;
            this.highestScoreLbl.Text = "0";
            this.highestScoreLbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 55);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Highest Score";
            // 
            // lowestScoreLbl
            // 
            this.lowestScoreLbl.Location = new System.Drawing.Point(97, 42);
            this.lowestScoreLbl.Name = "lowestScoreLbl";
            this.lowestScoreLbl.Size = new System.Drawing.Size(89, 13);
            this.lowestScoreLbl.TabIndex = 5;
            this.lowestScoreLbl.Text = "0";
            this.lowestScoreLbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 42);
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
            // msPerGenLbl
            // 
            this.msPerGenLbl.Location = new System.Drawing.Point(97, 80);
            this.msPerGenLbl.Name = "msPerGenLbl";
            this.msPerGenLbl.Size = new System.Drawing.Size(89, 13);
            this.msPerGenLbl.TabIndex = 11;
            this.msPerGenLbl.Text = "0";
            this.msPerGenLbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 80);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(86, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "MS / Generation";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(219, 281);
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
        private System.Windows.Forms.Label highestScoreLbl;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lowestScoreLbl;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label runsCompletedLbl;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label msPerGenLbl;
        private System.Windows.Forms.Label label6;
    }
}

