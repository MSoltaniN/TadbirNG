
namespace SPPC.Tools.SystemDesigner.Forms
{
    partial class LoginUtilityForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbUserName = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.chkShowPass = new System.Windows.Forms.CheckBox();
            this.cmbCompany = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbFiscalPeriod = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbBranch = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.grpOptions = new System.Windows.Forms.GroupBox();
            this.chkConnectToServer = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtTicket = new System.Windows.Forms.TextBox();
            this.btnAppLogin = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnCompanyLogin = new System.Windows.Forms.Button();
            this.grpOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(776, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Use this form to login with any user to any company and get a two-week ticket.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "User Name :";
            // 
            // cmbUserName
            // 
            this.cmbUserName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUserName.FormattingEnabled = true;
            this.cmbUserName.Location = new System.Drawing.Point(12, 69);
            this.cmbUserName.Name = "cmbUserName";
            this.cmbUserName.Size = new System.Drawing.Size(229, 28);
            this.cmbUserName.TabIndex = 2;
            this.cmbUserName.SelectedIndexChanged += new System.EventHandler(this.UserName_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(265, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 20);
            this.label3.TabIndex = 3;
            this.label3.Text = "Password :";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(265, 70);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(229, 27);
            this.txtPassword.TabIndex = 4;
            // 
            // chkShowPass
            // 
            this.chkShowPass.AutoSize = true;
            this.chkShowPass.Location = new System.Drawing.Point(516, 72);
            this.chkShowPass.Name = "chkShowPass";
            this.chkShowPass.Size = new System.Drawing.Size(134, 24);
            this.chkShowPass.TabIndex = 5;
            this.chkShowPass.Text = "Show password";
            this.chkShowPass.UseVisualStyleBackColor = true;
            this.chkShowPass.CheckedChanged += new System.EventHandler(this.ShowPass_CheckedChanged);
            // 
            // cmbCompany
            // 
            this.cmbCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCompany.FormattingEnabled = true;
            this.cmbCompany.Location = new System.Drawing.Point(12, 142);
            this.cmbCompany.Name = "cmbCompany";
            this.cmbCompany.Size = new System.Drawing.Size(229, 28);
            this.cmbCompany.TabIndex = 7;
            this.cmbCompany.SelectedIndexChanged += new System.EventHandler(this.Company_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 119);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "Company :";
            // 
            // cmbFiscalPeriod
            // 
            this.cmbFiscalPeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFiscalPeriod.FormattingEnabled = true;
            this.cmbFiscalPeriod.Location = new System.Drawing.Point(265, 142);
            this.cmbFiscalPeriod.Name = "cmbFiscalPeriod";
            this.cmbFiscalPeriod.Size = new System.Drawing.Size(229, 28);
            this.cmbFiscalPeriod.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(265, 119);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 20);
            this.label5.TabIndex = 8;
            this.label5.Text = "Fiscal Period :";
            // 
            // cmbBranch
            // 
            this.cmbBranch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBranch.FormattingEnabled = true;
            this.cmbBranch.Location = new System.Drawing.Point(516, 142);
            this.cmbBranch.Name = "cmbBranch";
            this.cmbBranch.Size = new System.Drawing.Size(229, 28);
            this.cmbBranch.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(516, 119);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 20);
            this.label6.TabIndex = 10;
            this.label6.Text = "Branch :";
            // 
            // grpOptions
            // 
            this.grpOptions.Controls.Add(this.chkConnectToServer);
            this.grpOptions.Location = new System.Drawing.Point(12, 183);
            this.grpOptions.Name = "grpOptions";
            this.grpOptions.Size = new System.Drawing.Size(733, 73);
            this.grpOptions.TabIndex = 12;
            this.grpOptions.TabStop = false;
            this.grpOptions.Text = "Options";
            // 
            // chkConnectToServer
            // 
            this.chkConnectToServer.AutoSize = true;
            this.chkConnectToServer.Enabled = false;
            this.chkConnectToServer.Location = new System.Drawing.Point(10, 33);
            this.chkConnectToServer.Name = "chkConnectToServer";
            this.chkConnectToServer.Size = new System.Drawing.Size(146, 24);
            this.chkConnectToServer.TabIndex = 0;
            this.chkConnectToServer.Text = "Connect to server";
            this.chkConnectToServer.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 266);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(148, 20);
            this.label7.TabIndex = 13;
            this.label7.Text = "X-Tadbir-AuthTicket :";
            // 
            // txtTicket
            // 
            this.txtTicket.Location = new System.Drawing.Point(12, 289);
            this.txtTicket.Multiline = true;
            this.txtTicket.Name = "txtTicket";
            this.txtTicket.ReadOnly = true;
            this.txtTicket.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtTicket.Size = new System.Drawing.Size(733, 224);
            this.txtTicket.TabIndex = 14;
            // 
            // btnAppLogin
            // 
            this.btnAppLogin.Location = new System.Drawing.Point(12, 531);
            this.btnAppLogin.Name = "btnAppLogin";
            this.btnAppLogin.Size = new System.Drawing.Size(94, 37);
            this.btnAppLogin.TabIndex = 15;
            this.btnAppLogin.Text = "App Login";
            this.btnAppLogin.UseVisualStyleBackColor = true;
            this.btnAppLogin.Click += new System.EventHandler(this.AppLogin_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(651, 531);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(94, 37);
            this.btnCancel.TabIndex = 16;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // btnCompanyLogin
            // 
            this.btnCompanyLogin.Location = new System.Drawing.Point(112, 531);
            this.btnCompanyLogin.Name = "btnCompanyLogin";
            this.btnCompanyLogin.Size = new System.Drawing.Size(129, 37);
            this.btnCompanyLogin.TabIndex = 17;
            this.btnCompanyLogin.Text = "Company Login";
            this.btnCompanyLogin.UseVisualStyleBackColor = true;
            this.btnCompanyLogin.Click += new System.EventHandler(this.CompanyLogin_Click);
            // 
            // LoginUtilityForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(762, 576);
            this.Controls.Add(this.btnCompanyLogin);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAppLogin);
            this.Controls.Add(this.txtTicket);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.grpOptions);
            this.Controls.Add(this.cmbBranch);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cmbFiscalPeriod);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmbCompany);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.chkShowPass);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbUserName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginUtilityForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Login Utility";
            this.grpOptions.ResumeLayout(false);
            this.grpOptions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbUserName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.CheckBox chkShowPass;
        private System.Windows.Forms.ComboBox cmbCompany;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbFiscalPeriod;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbBranch;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox grpOptions;
        private System.Windows.Forms.CheckBox chkConnectToServer;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtTicket;
        private System.Windows.Forms.Button btnAppLogin;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnCompanyLogin;
    }
}