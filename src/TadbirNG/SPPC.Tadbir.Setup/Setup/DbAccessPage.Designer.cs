
namespace SPPC.Tadbir.Setup
{
    partial class DbAccessPage
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
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
            this.grpDbAccess.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpDbAccess
            // 
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
            this.grpDbAccess.Location = new System.Drawing.Point(3, 11);
            this.grpDbAccess.Name = "grpDbAccess";
            this.grpDbAccess.Size = new System.Drawing.Size(594, 307);
            this.grpDbAccess.TabIndex = 0;
            this.grpDbAccess.TabStop = false;
            this.grpDbAccess.Text = "دسترسی به دیتابیس";
            // 
            // chkShowAdminPass
            // 
            this.chkShowAdminPass.AutoSize = true;
            this.chkShowAdminPass.Location = new System.Drawing.Point(129, 137);
            this.chkShowAdminPass.Name = "chkShowAdminPass";
            this.chkShowAdminPass.Size = new System.Drawing.Size(96, 24);
            this.chkShowAdminPass.TabIndex = 5;
            this.chkShowAdminPass.Text = "نمایش رمز";
            this.chkShowAdminPass.UseVisualStyleBackColor = true;
            this.chkShowAdminPass.CheckedChanged += new System.EventHandler(this.ShowAdminPass_CheckedChanged);
            // 
            // txtAdminPassword
            // 
            this.txtAdminPassword.Location = new System.Drawing.Point(231, 135);
            this.txtAdminPassword.Name = "txtAdminPassword";
            this.txtAdminPassword.PasswordChar = '*';
            this.txtAdminPassword.Size = new System.Drawing.Size(357, 27);
            this.txtAdminPassword.TabIndex = 4;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(461, 108);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(127, 20);
            this.label9.TabIndex = 3;
            this.label9.Text = "رمز ورود راهبر (sa):";
            // 
            // chkShowPass
            // 
            this.chkShowPass.AutoSize = true;
            this.chkShowPass.Location = new System.Drawing.Point(129, 268);
            this.chkShowPass.Name = "chkShowPass";
            this.chkShowPass.Size = new System.Drawing.Size(96, 24);
            this.chkShowPass.TabIndex = 10;
            this.chkShowPass.Text = "نمایش رمز";
            this.chkShowPass.UseVisualStyleBackColor = true;
            this.chkShowPass.CheckedChanged += new System.EventHandler(this.ShowPass_CheckedChanged);
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(231, 266);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(356, 27);
            this.txtPassword.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(518, 239);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 20);
            this.label6.TabIndex = 8;
            this.label6.Text = "رمز ورود :";
            // 
            // cmbLogin
            // 
            this.cmbLogin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLogin.FormattingEnabled = true;
            this.cmbLogin.Location = new System.Drawing.Point(129, 198);
            this.cmbLogin.Name = "cmbLogin";
            this.cmbLogin.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmbLogin.Size = new System.Drawing.Size(459, 28);
            this.cmbLogin.TabIndex = 7;
            this.cmbLogin.DropDown += new System.EventHandler(this.Login_DropDown);
            this.cmbLogin.SelectedIndexChanged += new System.EventHandler(this.Login_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(488, 171);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(99, 20);
            this.label5.TabIndex = 6;
            this.label5.Text = "کاربر دیتابیس :";
            // 
            // cmbDbServer
            // 
            this.cmbDbServer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDbServer.FormattingEnabled = true;
            this.cmbDbServer.Location = new System.Drawing.Point(129, 66);
            this.cmbDbServer.Name = "cmbDbServer";
            this.cmbDbServer.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmbDbServer.Size = new System.Drawing.Size(459, 28);
            this.cmbDbServer.TabIndex = 2;
            this.cmbDbServer.SelectedIndexChanged += new System.EventHandler(this.DbServer_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(488, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 20);
            this.label3.TabIndex = 1;
            this.label3.Text = "سرور دیتابیس :";
            // 
            // DbAccessPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpDbAccess);
            this.Name = "DbAccessPage";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Size = new System.Drawing.Size(600, 320);
            this.grpDbAccess.ResumeLayout(false);
            this.grpDbAccess.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpDbAccess;
        private System.Windows.Forms.CheckBox chkShowAdminPass;
        private System.Windows.Forms.TextBox txtAdminPassword;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox chkShowPass;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbLogin;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbDbServer;
        private System.Windows.Forms.Label label3;
    }
}
