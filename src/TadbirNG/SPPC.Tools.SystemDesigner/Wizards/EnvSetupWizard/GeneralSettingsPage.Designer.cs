
namespace SPPC.Tools.SystemDesigner.Wizards.EnvSetupWizard
{
    partial class GeneralSettingsPage
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
            this.grpProject = new System.Windows.Forms.GroupBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtRootFolder = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.grpLicensee = new System.Windows.Forms.GroupBox();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.grpWinLogin = new System.Windows.Forms.GroupBox();
            this.txtWinPassword = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtWinUser = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.grpProject.SuspendLayout();
            this.grpLicensee.SuspendLayout();
            this.grpWinLogin.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpProject
            // 
            this.grpProject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpProject.Controls.Add(this.btnBrowse);
            this.grpProject.Controls.Add(this.txtRootFolder);
            this.grpProject.Controls.Add(this.label1);
            this.grpProject.Location = new System.Drawing.Point(6, 3);
            this.grpProject.Name = "grpProject";
            this.grpProject.Size = new System.Drawing.Size(452, 111);
            this.grpProject.TabIndex = 0;
            this.grpProject.TabStop = false;
            this.grpProject.Text = "Project Settings";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.Location = new System.Drawing.Point(352, 65);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(94, 34);
            this.btnBrowse.TabIndex = 2;
            this.btnBrowse.Text = "Browse...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.Browse_Click);
            // 
            // txtRootFolder
            // 
            this.txtRootFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRootFolder.Location = new System.Drawing.Point(11, 69);
            this.txtRootFolder.Name = "txtRootFolder";
            this.txtRootFolder.Size = new System.Drawing.Size(335, 27);
            this.txtRootFolder.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(163, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Repository root folder :";
            // 
            // grpLicensee
            // 
            this.grpLicensee.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpLicensee.Controls.Add(this.txtLastName);
            this.grpLicensee.Controls.Add(this.label3);
            this.grpLicensee.Controls.Add(this.txtFirstName);
            this.grpLicensee.Controls.Add(this.label2);
            this.grpLicensee.Location = new System.Drawing.Point(6, 125);
            this.grpLicensee.Name = "grpLicensee";
            this.grpLicensee.Size = new System.Drawing.Size(452, 174);
            this.grpLicensee.TabIndex = 1;
            this.grpLicensee.TabStop = false;
            this.grpLicensee.Text = "Licensee";
            // 
            // txtLastName
            // 
            this.txtLastName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLastName.Location = new System.Drawing.Point(11, 133);
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(335, 27);
            this.txtLastName.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 105);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "Last Name :";
            // 
            // txtFirstName
            // 
            this.txtFirstName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFirstName.Location = new System.Drawing.Point(11, 63);
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(335, 27);
            this.txtFirstName.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "First Name :";
            // 
            // grpWinLogin
            // 
            this.grpWinLogin.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpWinLogin.Controls.Add(this.label6);
            this.grpWinLogin.Controls.Add(this.txtWinPassword);
            this.grpWinLogin.Controls.Add(this.label4);
            this.grpWinLogin.Controls.Add(this.txtWinUser);
            this.grpWinLogin.Controls.Add(this.label5);
            this.grpWinLogin.Location = new System.Drawing.Point(6, 319);
            this.grpWinLogin.Name = "grpWinLogin";
            this.grpWinLogin.Size = new System.Drawing.Size(452, 201);
            this.grpWinLogin.TabIndex = 2;
            this.grpWinLogin.TabStop = false;
            this.grpWinLogin.Text = "Windows Login (developer system)";
            // 
            // txtWinPassword
            // 
            this.txtWinPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtWinPassword.Location = new System.Drawing.Point(11, 133);
            this.txtWinPassword.Name = "txtWinPassword";
            this.txtWinPassword.Size = new System.Drawing.Size(335, 27);
            this.txtWinPassword.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 105);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 20);
            this.label4.TabIndex = 4;
            this.label4.Text = "Password :";
            // 
            // txtWinUser
            // 
            this.txtWinUser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtWinUser.Location = new System.Drawing.Point(11, 63);
            this.txtWinUser.Name = "txtWinUser";
            this.txtWinUser.Size = new System.Drawing.Size(335, 27);
            this.txtWinUser.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 35);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 20);
            this.label5.TabIndex = 2;
            this.label5.Text = "User Name :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 169);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(303, 20);
            this.label6.TabIndex = 6;
            this.label6.Text = "* Note: This is required for license activation.";
            // 
            // GeneralSettingsPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpWinLogin);
            this.Controls.Add(this.grpLicensee);
            this.Controls.Add(this.grpProject);
            this.Name = "GeneralSettingsPage";
            this.Size = new System.Drawing.Size(464, 526);
            this.grpProject.ResumeLayout(false);
            this.grpProject.PerformLayout();
            this.grpLicensee.ResumeLayout(false);
            this.grpLicensee.PerformLayout();
            this.grpWinLogin.ResumeLayout(false);
            this.grpWinLogin.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpProject;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox txtRootFolder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox grpLicensee;
        private System.Windows.Forms.TextBox txtLastName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtFirstName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox grpWinLogin;
        private System.Windows.Forms.TextBox txtWinPassword;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtWinUser;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}
