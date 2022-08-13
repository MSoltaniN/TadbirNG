
namespace SPPC.Tadbir.WinRunner
{
    partial class InstallerForm
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
            this.components = new System.ComponentModel.Container();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnInstall = new System.Windows.Forms.Button();
            this.worker = new System.ComponentModel.BackgroundWorker();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabInstall = new System.Windows.Forms.TabPage();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblProgress = new System.Windows.Forms.Label();
            this.lblElapsed = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.chkCreateShortcut = new System.Windows.Forms.CheckBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtInstallPath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.progress = new System.Windows.Forms.ProgressBar();
            this.txtConsole = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabSettings = new System.Windows.Forms.TabPage();
            this.grpDbAccess = new System.Windows.Forms.GroupBox();
            this.chkShowAdminPass = new System.Windows.Forms.CheckBox();
            this.txtAdminPassword = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.chkShowPass = new System.Windows.Forms.CheckBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbLogin = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbDbServer = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.grpAppAccess = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDomain = new System.Windows.Forms.TextBox();
            this.radGlobal = new System.Windows.Forms.RadioButton();
            this.radLocal = new System.Windows.Forms.RadioButton();
            this.tabMain.SuspendLayout();
            this.tabInstall.SuspendLayout();
            this.tabSettings.SuspendLayout();
            this.grpDbAccess.SuspendLayout();
            this.grpAppAccess.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Location = new System.Drawing.Point(683, 476);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(94, 29);
            this.btnExit.TabIndex = 2;
            this.btnExit.Text = "خروج";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.Exit_Click);
            // 
            // btnInstall
            // 
            this.btnInstall.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInstall.Location = new System.Drawing.Point(546, 476);
            this.btnInstall.Name = "btnInstall";
            this.btnInstall.Size = new System.Drawing.Size(131, 29);
            this.btnInstall.TabIndex = 1;
            this.btnInstall.Text = "نصب برنامه";
            this.btnInstall.UseVisualStyleBackColor = true;
            this.btnInstall.Click += new System.EventHandler(this.Install_Click);
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
            // tabMain
            // 
            this.tabMain.Controls.Add(this.tabInstall);
            this.tabMain.Controls.Add(this.tabSettings);
            this.tabMain.Location = new System.Drawing.Point(3, 3);
            this.tabMain.Name = "tabMain";
            this.tabMain.RightToLeftLayout = true;
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(778, 463);
            this.tabMain.TabIndex = 0;
            // 
            // tabInstall
            // 
            this.tabInstall.Controls.Add(this.lblStatus);
            this.tabInstall.Controls.Add(this.lblProgress);
            this.tabInstall.Controls.Add(this.lblElapsed);
            this.tabInstall.Controls.Add(this.label7);
            this.tabInstall.Controls.Add(this.chkCreateShortcut);
            this.tabInstall.Controls.Add(this.btnBrowse);
            this.tabInstall.Controls.Add(this.txtInstallPath);
            this.tabInstall.Controls.Add(this.label2);
            this.tabInstall.Controls.Add(this.progress);
            this.tabInstall.Controls.Add(this.txtConsole);
            this.tabInstall.Controls.Add(this.label1);
            this.tabInstall.Location = new System.Drawing.Point(4, 29);
            this.tabInstall.Name = "tabInstall";
            this.tabInstall.Padding = new System.Windows.Forms.Padding(3);
            this.tabInstall.Size = new System.Drawing.Size(770, 430);
            this.tabInstall.TabIndex = 0;
            this.tabInstall.Text = "نصب";
            this.tabInstall.UseVisualStyleBackColor = true;
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatus.Location = new System.Drawing.Point(341, 389);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(315, 31);
            this.lblStatus.TabIndex = 10;
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblProgress
            // 
            this.lblProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblProgress.Location = new System.Drawing.Point(670, 359);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(90, 23);
            this.lblProgress.TabIndex = 9;
            // 
            // lblElapsed
            // 
            this.lblElapsed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblElapsed.Location = new System.Drawing.Point(10, 126);
            this.lblElapsed.Name = "lblElapsed";
            this.lblElapsed.Size = new System.Drawing.Size(103, 25);
            this.lblElapsed.TabIndex = 6;
            this.lblElapsed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(142, 128);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(115, 20);
            this.label7.TabIndex = 5;
            this.label7.Text = "زمان صرف شده :";
            // 
            // chkCreateShortcut
            // 
            this.chkCreateShortcut.AutoSize = true;
            this.chkCreateShortcut.Checked = true;
            this.chkCreateShortcut.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCreateShortcut.Location = new System.Drawing.Point(547, 74);
            this.chkCreateShortcut.Name = "chkCreateShortcut";
            this.chkCreateShortcut.Size = new System.Drawing.Size(213, 24);
            this.chkCreateShortcut.TabIndex = 3;
            this.chkCreateShortcut.Text = "ایجاد میانبر برنامه روی میز کار";
            this.chkCreateShortcut.UseVisualStyleBackColor = true;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.Location = new System.Drawing.Point(10, 33);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(94, 29);
            this.btnBrowse.TabIndex = 2;
            this.btnBrowse.Text = "انتخاب...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.Browse_Click);
            // 
            // txtInstallPath
            // 
            this.txtInstallPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtInstallPath.Location = new System.Drawing.Point(110, 34);
            this.txtInstallPath.Name = "txtInstallPath";
            this.txtInstallPath.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtInstallPath.Size = new System.Drawing.Size(650, 27);
            this.txtInstallPath.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(633, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(127, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "مسیر نصب برنامه :";
            // 
            // progress
            // 
            this.progress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progress.Location = new System.Drawing.Point(10, 353);
            this.progress.Name = "progress";
            this.progress.Size = new System.Drawing.Size(646, 29);
            this.progress.TabIndex = 8;
            // 
            // txtConsole
            // 
            this.txtConsole.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtConsole.Location = new System.Drawing.Point(10, 155);
            this.txtConsole.Multiline = true;
            this.txtConsole.Name = "txtConsole";
            this.txtConsole.ReadOnly = true;
            this.txtConsole.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtConsole.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtConsole.Size = new System.Drawing.Size(750, 192);
            this.txtConsole.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(650, 131);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "خروجی عملیات :";
            // 
            // tabSettings
            // 
            this.tabSettings.Controls.Add(this.grpDbAccess);
            this.tabSettings.Controls.Add(this.grpAppAccess);
            this.tabSettings.Location = new System.Drawing.Point(4, 29);
            this.tabSettings.Name = "tabSettings";
            this.tabSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tabSettings.Size = new System.Drawing.Size(770, 430);
            this.tabSettings.TabIndex = 1;
            this.tabSettings.Text = "تنظیمات";
            this.tabSettings.UseVisualStyleBackColor = true;
            // 
            // grpDbAccess
            // 
            this.grpDbAccess.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpDbAccess.Controls.Add(this.chkShowAdminPass);
            this.grpDbAccess.Controls.Add(this.txtAdminPassword);
            this.grpDbAccess.Controls.Add(this.label9);
            this.grpDbAccess.Controls.Add(this.chkShowPass);
            this.grpDbAccess.Controls.Add(this.txtPassword);
            this.grpDbAccess.Controls.Add(this.label6);
            this.grpDbAccess.Controls.Add(this.cmbLogin);
            this.grpDbAccess.Controls.Add(this.label5);
            this.grpDbAccess.Controls.Add(this.cmbDbServer);
            this.grpDbAccess.Controls.Add(this.label3);
            this.grpDbAccess.Location = new System.Drawing.Point(6, 157);
            this.grpDbAccess.Name = "grpDbAccess";
            this.grpDbAccess.Size = new System.Drawing.Size(758, 267);
            this.grpDbAccess.TabIndex = 1;
            this.grpDbAccess.TabStop = false;
            this.grpDbAccess.Text = "دسترسی به دیتابیس";
            // 
            // chkShowAdminPass
            // 
            this.chkShowAdminPass.AutoSize = true;
            this.chkShowAdminPass.Location = new System.Drawing.Point(179, 100);
            this.chkShowAdminPass.Name = "chkShowAdminPass";
            this.chkShowAdminPass.Size = new System.Drawing.Size(96, 24);
            this.chkShowAdminPass.TabIndex = 9;
            this.chkShowAdminPass.Text = "نمایش رمز";
            this.chkShowAdminPass.UseVisualStyleBackColor = true;
            this.chkShowAdminPass.CheckedChanged += new System.EventHandler(this.ShowAdminPass_CheckedChanged);
            // 
            // txtAdminPassword
            // 
            this.txtAdminPassword.Location = new System.Drawing.Point(6, 67);
            this.txtAdminPassword.Name = "txtAdminPassword";
            this.txtAdminPassword.PasswordChar = '*';
            this.txtAdminPassword.Size = new System.Drawing.Size(269, 27);
            this.txtAdminPassword.TabIndex = 8;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(148, 40);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(127, 20);
            this.label9.TabIndex = 7;
            this.label9.Text = "رمز ورود راهبر (sa):";
            // 
            // chkShowPass
            // 
            this.chkShowPass.AutoSize = true;
            this.chkShowPass.Location = new System.Drawing.Point(285, 212);
            this.chkShowPass.Name = "chkShowPass";
            this.chkShowPass.Size = new System.Drawing.Size(96, 24);
            this.chkShowPass.TabIndex = 6;
            this.chkShowPass.Text = "نمایش رمز";
            this.chkShowPass.UseVisualStyleBackColor = true;
            this.chkShowPass.CheckedChanged += new System.EventHandler(this.ShowPass_CheckedChanged);
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(387, 210);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(356, 27);
            this.txtPassword.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(674, 183);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 20);
            this.label6.TabIndex = 4;
            this.label6.Text = "رمز ورود :";
            // 
            // cmbLogin
            // 
            this.cmbLogin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLogin.FormattingEnabled = true;
            this.cmbLogin.Location = new System.Drawing.Point(285, 139);
            this.cmbLogin.Name = "cmbLogin";
            this.cmbLogin.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmbLogin.Size = new System.Drawing.Size(459, 28);
            this.cmbLogin.TabIndex = 3;
            this.cmbLogin.DropDown += new System.EventHandler(this.Login_DropDown);
            this.cmbLogin.SelectedIndexChanged += new System.EventHandler(this.Login_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(644, 112);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(99, 20);
            this.label5.TabIndex = 2;
            this.label5.Text = "کاربر دیتابیس :";
            // 
            // cmbDbServer
            // 
            this.cmbDbServer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDbServer.FormattingEnabled = true;
            this.cmbDbServer.Location = new System.Drawing.Point(285, 67);
            this.cmbDbServer.Name = "cmbDbServer";
            this.cmbDbServer.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmbDbServer.Size = new System.Drawing.Size(459, 28);
            this.cmbDbServer.TabIndex = 1;
            this.cmbDbServer.SelectedIndexChanged += new System.EventHandler(this.DbServer_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(644, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 20);
            this.label3.TabIndex = 0;
            this.label3.Text = "سرور دیتابیس :";
            // 
            // grpAppAccess
            // 
            this.grpAppAccess.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpAppAccess.Controls.Add(this.label8);
            this.grpAppAccess.Controls.Add(this.label4);
            this.grpAppAccess.Controls.Add(this.txtDomain);
            this.grpAppAccess.Controls.Add(this.radGlobal);
            this.grpAppAccess.Controls.Add(this.radLocal);
            this.grpAppAccess.Location = new System.Drawing.Point(6, 17);
            this.grpAppAccess.Name = "grpAppAccess";
            this.grpAppAccess.Size = new System.Drawing.Size(758, 125);
            this.grpAppAccess.TabIndex = 0;
            this.grpAppAccess.TabStop = false;
            this.grpAppAccess.Text = "دسترسی به برنامه";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(229, 78);
            this.label8.Name = "label8";
            this.label8.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label8.Size = new System.Drawing.Size(56, 25);
            this.label8.TabIndex = 4;
            this.label8.Text = "http://";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(285, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(219, 20);
            this.label4.TabIndex = 2;
            this.label4.Text = "آدرس سرور (دامنه یا آی پی ثابت) :";
            // 
            // txtDomain
            // 
            this.txtDomain.Location = new System.Drawing.Point(285, 77);
            this.txtDomain.Name = "txtDomain";
            this.txtDomain.Size = new System.Drawing.Size(219, 27);
            this.txtDomain.TabIndex = 3;
            // 
            // radGlobal
            // 
            this.radGlobal.AutoSize = true;
            this.radGlobal.Location = new System.Drawing.Point(544, 78);
            this.radGlobal.Name = "radGlobal";
            this.radGlobal.Size = new System.Drawing.Size(200, 24);
            this.radGlobal.TabIndex = 1;
            this.radGlobal.TabStop = true;
            this.radGlobal.Text = "سراسری (روی شبکه اینترنت)";
            this.radGlobal.UseVisualStyleBackColor = true;
            this.radGlobal.CheckedChanged += new System.EventHandler(this.Global_CheckedChanged);
            // 
            // radLocal
            // 
            this.radLocal.AutoSize = true;
            this.radLocal.Location = new System.Drawing.Point(523, 37);
            this.radLocal.Name = "radLocal";
            this.radLocal.Size = new System.Drawing.Size(221, 24);
            this.radLocal.TabIndex = 0;
            this.radLocal.TabStop = true;
            this.radLocal.Text = "محلی (فقط روی دستگاه سرور)";
            this.radLocal.UseVisualStyleBackColor = true;
            // 
            // InstallerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(784, 513);
            this.Controls.Add(this.tabMain);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnInstall);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "InstallerForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "سیستم جدید تدبیر";
            this.tabMain.ResumeLayout(false);
            this.tabInstall.ResumeLayout(false);
            this.tabInstall.PerformLayout();
            this.tabSettings.ResumeLayout(false);
            this.grpDbAccess.ResumeLayout(false);
            this.grpDbAccess.PerformLayout();
            this.grpAppAccess.ResumeLayout(false);
            this.grpAppAccess.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnInstall;
        private System.ComponentModel.BackgroundWorker worker;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabInstall;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.Label lblElapsed;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox chkCreateShortcut;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox txtInstallPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ProgressBar progress;
        private System.Windows.Forms.TextBox txtConsole;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabSettings;
        private System.Windows.Forms.GroupBox grpDbAccess;
        private System.Windows.Forms.GroupBox grpAppAccess;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDomain;
        private System.Windows.Forms.RadioButton radGlobal;
        private System.Windows.Forms.RadioButton radLocal;
        private System.Windows.Forms.ComboBox cmbDbServer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbLogin;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkShowPass;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox chkShowAdminPass;
        private System.Windows.Forms.TextBox txtAdminPassword;
        private System.Windows.Forms.Label label9;
    }
}

