
namespace SPPC.Tadbir.WinRunner
{
    partial class MainWindow
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnExit = new System.Windows.Forms.Button();
            this.btnInstall = new System.Windows.Forms.Button();
            this.btnStartService = new System.Windows.Forms.Button();
            this.btnPull = new System.Windows.Forms.Button();
            this.btnStartContainers = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtConsole = new System.Windows.Forms.TextBox();
            this.progress = new System.Windows.Forms.ProgressBar();
            this.lblProgress = new System.Windows.Forms.Label();
            this.pullWorker = new System.ComponentModel.BackgroundWorker();
            this.startWorker = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Location = new System.Drawing.Point(678, 457);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(94, 29);
            this.btnExit.TabIndex = 4;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.Exit_Click);
            // 
            // btnInstall
            // 
            this.btnInstall.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnInstall.Location = new System.Drawing.Point(12, 457);
            this.btnInstall.Name = "btnInstall";
            this.btnInstall.Size = new System.Drawing.Size(131, 29);
            this.btnInstall.TabIndex = 0;
            this.btnInstall.Text = "Install Service";
            this.btnInstall.UseVisualStyleBackColor = true;
            this.btnInstall.Click += new System.EventHandler(this.Install_Click);
            // 
            // btnStartService
            // 
            this.btnStartService.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnStartService.Location = new System.Drawing.Point(149, 457);
            this.btnStartService.Name = "btnStartService";
            this.btnStartService.Size = new System.Drawing.Size(114, 29);
            this.btnStartService.TabIndex = 1;
            this.btnStartService.Text = "Start Service";
            this.btnStartService.UseVisualStyleBackColor = true;
            this.btnStartService.Click += new System.EventHandler(this.StartService_Click);
            // 
            // btnPull
            // 
            this.btnPull.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPull.Location = new System.Drawing.Point(269, 457);
            this.btnPull.Name = "btnPull";
            this.btnPull.Size = new System.Drawing.Size(117, 29);
            this.btnPull.TabIndex = 2;
            this.btnPull.Text = "Pull Images";
            this.btnPull.UseVisualStyleBackColor = true;
            this.btnPull.Click += new System.EventHandler(this.Pull_Click);
            // 
            // btnStartContainers
            // 
            this.btnStartContainers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnStartContainers.Location = new System.Drawing.Point(392, 457);
            this.btnStartContainers.Name = "btnStartContainers";
            this.btnStartContainers.Size = new System.Drawing.Size(141, 29);
            this.btnStartContainers.TabIndex = 3;
            this.btnStartContainers.Text = "Start Containers";
            this.btnStartContainers.UseVisualStyleBackColor = true;
            this.btnStartContainers.Click += new System.EventHandler(this.StartContainers_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(157, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "Console Output/Error :";
            // 
            // txtConsole
            // 
            this.txtConsole.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtConsole.Location = new System.Drawing.Point(12, 46);
            this.txtConsole.Multiline = true;
            this.txtConsole.Name = "txtConsole";
            this.txtConsole.ReadOnly = true;
            this.txtConsole.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtConsole.Size = new System.Drawing.Size(760, 355);
            this.txtConsole.TabIndex = 6;
            // 
            // progress
            // 
            this.progress.Location = new System.Drawing.Point(12, 407);
            this.progress.Name = "progress";
            this.progress.Size = new System.Drawing.Size(659, 29);
            this.progress.TabIndex = 7;
            // 
            // lblProgress
            // 
            this.lblProgress.Location = new System.Drawing.Point(682, 412);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(90, 23);
            this.lblProgress.TabIndex = 8;
            // 
            // pullWorker
            // 
            this.pullWorker.WorkerReportsProgress = true;
            this.pullWorker.WorkerSupportsCancellation = true;
            this.pullWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.PullWorker_DoWork);
            this.pullWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.Worker_ProgressChanged);
            // 
            // startWorker
            // 
            this.startWorker.WorkerReportsProgress = true;
            this.startWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.StartWorker_DoWork);
            this.startWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.Worker_ProgressChanged);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(784, 494);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.progress);
            this.Controls.Add(this.txtConsole);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnStartContainers);
            this.Controls.Add(this.btnPull);
            this.Controls.Add(this.btnStartService);
            this.Controls.Add(this.btnInstall);
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TadbirNG";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnInstall;
        private System.Windows.Forms.Button btnStartService;
        private System.Windows.Forms.Button btnPull;
        private System.Windows.Forms.Button btnStartContainers;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtConsole;
        private System.Windows.Forms.ProgressBar progress;
        private System.Windows.Forms.Label lblProgress;
        private System.ComponentModel.BackgroundWorker pullWorker;
        private System.ComponentModel.BackgroundWorker startWorker;
    }
}

