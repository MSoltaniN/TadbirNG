
namespace SPPC.Tadbir.Setup
{
    partial class ModifyWizard
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
            this.components = new System.ComponentModel.Container();
            this.btnSetup = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.pnlPage = new System.Windows.Forms.Panel();
            this.worker = new System.ComponentModel.BackgroundWorker();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // btnSetup
            // 
            this.btnSetup.Location = new System.Drawing.Point(212, 348);
            this.btnSetup.Name = "btnSetup";
            this.btnSetup.Size = new System.Drawing.Size(120, 36);
            this.btnSetup.TabIndex = 9;
            this.btnSetup.Text = "نصب برنامه";
            this.btnSetup.UseVisualStyleBackColor = true;
            this.btnSetup.Click += new System.EventHandler(this.Setup_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(518, 348);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(94, 36);
            this.btnExit.TabIndex = 8;
            this.btnExit.Text = "خروج";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(112, 348);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(94, 36);
            this.btnNext.TabIndex = 7;
            this.btnNext.Text = "بعدی";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.Next_Click);
            // 
            // btnPrevious
            // 
            this.btnPrevious.Location = new System.Drawing.Point(12, 348);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(94, 36);
            this.btnPrevious.TabIndex = 6;
            this.btnPrevious.Text = "قبلی";
            this.btnPrevious.UseVisualStyleBackColor = true;
            // 
            // pnlPage
            // 
            this.pnlPage.Location = new System.Drawing.Point(12, 11);
            this.pnlPage.Name = "pnlPage";
            this.pnlPage.Size = new System.Drawing.Size(600, 320);
            this.pnlPage.TabIndex = 5;
            // 
            // worker
            // 
            this.worker.WorkerReportsProgress = true;
            this.worker.WorkerSupportsCancellation = true;
            this.worker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.Worker_DoWork);
            this.worker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.Worker_ProgressChanged);
            this.worker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.Worker_RunWorkerCompleted);
            // 
            // timer
            // 
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.Timer_Tick);
            // 
            // ModifyWizard
            // 
            this.AcceptButton = this.btnNext;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(624, 392);
            this.Controls.Add(this.btnSetup);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnPrevious);
            this.Controls.Add(this.pnlPage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ModifyWizard";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "تغییر وضعیت نصب برنامه";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnSetup;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.Panel pnlPage;
        private System.ComponentModel.BackgroundWorker worker;
        private System.Windows.Forms.Timer timer;
    }
}