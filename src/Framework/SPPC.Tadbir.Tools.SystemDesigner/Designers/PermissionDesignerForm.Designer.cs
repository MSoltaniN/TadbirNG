namespace SPPC.Tadbir.Tools.SystemDesigner.Designers
{
    partial class PermissionDesignerForm
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
            this.pnlPage = new System.Windows.Forms.Panel();
            this.btnDeletePermission = new System.Windows.Forms.Button();
            this.btnAddPermission = new System.Windows.Forms.Button();
            this.btnApplyPermissionGroup = new System.Windows.Forms.Button();
            this.gboxPermission = new System.Windows.Forms.GroupBox();
            this.trbFlag = new System.Windows.Forms.TrackBar();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPermissionDesc = new System.Windows.Forms.TextBox();
            this.txtPermissionName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lblFlag = new System.Windows.Forms.Label();
            this.lboxPermissions = new System.Windows.Forms.ListBox();
            this.gboxPermissionGroup = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPermissionGroupDescription = new System.Windows.Forms.TextBox();
            this.txtPermissionGroupName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPermissionGroupEntityName = new System.Windows.Forms.TextBox();
            this.lblStepInfo = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnGenarateScript = new System.Windows.Forms.Button();
            this.pnlPage.SuspendLayout();
            this.gboxPermission.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trbFlag)).BeginInit();
            this.gboxPermissionGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlPage
            // 
            this.pnlPage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlPage.Controls.Add(this.btnDeletePermission);
            this.pnlPage.Controls.Add(this.btnAddPermission);
            this.pnlPage.Controls.Add(this.btnApplyPermissionGroup);
            this.pnlPage.Controls.Add(this.gboxPermission);
            this.pnlPage.Controls.Add(this.lboxPermissions);
            this.pnlPage.Controls.Add(this.gboxPermissionGroup);
            this.pnlPage.Location = new System.Drawing.Point(11, 49);
            this.pnlPage.Margin = new System.Windows.Forms.Padding(2);
            this.pnlPage.Name = "pnlPage";
            this.pnlPage.Size = new System.Drawing.Size(1015, 432);
            this.pnlPage.TabIndex = 3;
            // 
            // btnDeletePermission
            // 
            this.btnDeletePermission.Location = new System.Drawing.Point(628, 351);
            this.btnDeletePermission.Name = "btnDeletePermission";
            this.btnDeletePermission.Size = new System.Drawing.Size(45, 23);
            this.btnDeletePermission.TabIndex = 54;
            this.btnDeletePermission.Text = "delete";
            this.btnDeletePermission.UseVisualStyleBackColor = true;
            this.btnDeletePermission.Click += new System.EventHandler(this.DeletePermission_Click);
            // 
            // btnAddPermission
            // 
            this.btnAddPermission.Location = new System.Drawing.Point(628, 322);
            this.btnAddPermission.Name = "btnAddPermission";
            this.btnAddPermission.Size = new System.Drawing.Size(45, 23);
            this.btnAddPermission.TabIndex = 53;
            this.btnAddPermission.Text = "Add";
            this.btnAddPermission.UseVisualStyleBackColor = true;
            this.btnAddPermission.Click += new System.EventHandler(this.AddPermission_Click);
            // 
            // btnApplyPermissionGroup
            // 
            this.btnApplyPermissionGroup.Location = new System.Drawing.Point(300, 140);
            this.btnApplyPermissionGroup.Name = "btnApplyPermissionGroup";
            this.btnApplyPermissionGroup.Size = new System.Drawing.Size(45, 23);
            this.btnApplyPermissionGroup.TabIndex = 52;
            this.btnApplyPermissionGroup.Text = "->";
            this.btnApplyPermissionGroup.UseVisualStyleBackColor = true;
            this.btnApplyPermissionGroup.Click += new System.EventHandler(this.ApplyPermissionGroup_Click);
            // 
            // gboxPermission
            // 
            this.gboxPermission.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gboxPermission.Controls.Add(this.trbFlag);
            this.gboxPermission.Controls.Add(this.label5);
            this.gboxPermission.Controls.Add(this.txtPermissionDesc);
            this.gboxPermission.Controls.Add(this.txtPermissionName);
            this.gboxPermission.Controls.Add(this.label6);
            this.gboxPermission.Controls.Add(this.lblFlag);
            this.gboxPermission.Enabled = false;
            this.gboxPermission.Location = new System.Drawing.Point(655, 13);
            this.gboxPermission.Name = "gboxPermission";
            this.gboxPermission.Size = new System.Drawing.Size(320, 230);
            this.gboxPermission.TabIndex = 51;
            this.gboxPermission.TabStop = false;
            this.gboxPermission.Text = "Permission";
            // 
            // trbFlag
            // 
            this.trbFlag.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trbFlag.Location = new System.Drawing.Point(20, 87);
            this.trbFlag.Maximum = 50;
            this.trbFlag.Name = "trbFlag";
            this.trbFlag.Size = new System.Drawing.Size(272, 45);
            this.trbFlag.SmallChange = 2;
            this.trbFlag.TabIndex = 13;
            this.trbFlag.Value = 1;
            this.trbFlag.Scroll += new System.EventHandler(this.Flag_Scroll);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 133);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Description :";
            // 
            // txtPermissionDesc
            // 
            this.txtPermissionDesc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPermissionDesc.Location = new System.Drawing.Point(18, 148);
            this.txtPermissionDesc.Margin = new System.Windows.Forms.Padding(2);
            this.txtPermissionDesc.Multiline = true;
            this.txtPermissionDesc.Name = "txtPermissionDesc";
            this.txtPermissionDesc.Size = new System.Drawing.Size(271, 66);
            this.txtPermissionDesc.TabIndex = 9;
            // 
            // txtPermissionName
            // 
            this.txtPermissionName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPermissionName.Location = new System.Drawing.Point(21, 38);
            this.txtPermissionName.Margin = new System.Windows.Forms.Padding(2);
            this.txtPermissionName.Name = "txtPermissionName";
            this.txtPermissionName.Size = new System.Drawing.Size(271, 20);
            this.txtPermissionName.TabIndex = 5;
            this.txtPermissionName.Leave += new System.EventHandler(this.PermissionName_Leave);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 21);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Name :";
            // 
            // lblFlag
            // 
            this.lblFlag.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFlag.AutoSize = true;
            this.lblFlag.Location = new System.Drawing.Point(18, 71);
            this.lblFlag.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblFlag.Name = "lblFlag";
            this.lblFlag.Size = new System.Drawing.Size(33, 13);
            this.lblFlag.TabIndex = 8;
            this.lblFlag.Text = "Flag :";
            // 
            // lboxPermissions
            // 
            this.lboxPermissions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.lboxPermissions.Enabled = false;
            this.lboxPermissions.FormattingEnabled = true;
            this.lboxPermissions.Location = new System.Drawing.Point(360, 13);
            this.lboxPermissions.Name = "lboxPermissions";
            this.lboxPermissions.Size = new System.Drawing.Size(262, 381);
            this.lboxPermissions.TabIndex = 50;
            this.lboxPermissions.SelectedIndexChanged += new System.EventHandler(this.Permissions_SelectedIndexChanged);
            // 
            // gboxPermissionGroup
            // 
            this.gboxPermissionGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.gboxPermissionGroup.Controls.Add(this.label4);
            this.gboxPermissionGroup.Controls.Add(this.txtPermissionGroupDescription);
            this.gboxPermissionGroup.Controls.Add(this.txtPermissionGroupName);
            this.gboxPermissionGroup.Controls.Add(this.label3);
            this.gboxPermissionGroup.Controls.Add(this.label2);
            this.gboxPermissionGroup.Controls.Add(this.txtPermissionGroupEntityName);
            this.gboxPermissionGroup.Location = new System.Drawing.Point(14, 13);
            this.gboxPermissionGroup.Name = "gboxPermissionGroup";
            this.gboxPermissionGroup.Size = new System.Drawing.Size(271, 247);
            this.gboxPermissionGroup.TabIndex = 49;
            this.gboxPermissionGroup.TabStop = false;
            this.gboxPermissionGroup.Text = "Permission Group";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 111);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Description :";
            // 
            // txtPermissionGroupDescription
            // 
            this.txtPermissionGroupDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPermissionGroupDescription.Location = new System.Drawing.Point(18, 127);
            this.txtPermissionGroupDescription.Margin = new System.Windows.Forms.Padding(2);
            this.txtPermissionGroupDescription.Multiline = true;
            this.txtPermissionGroupDescription.Name = "txtPermissionGroupDescription";
            this.txtPermissionGroupDescription.Size = new System.Drawing.Size(222, 103);
            this.txtPermissionGroupDescription.TabIndex = 9;
            // 
            // txtPermissionGroupName
            // 
            this.txtPermissionGroupName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPermissionGroupName.Location = new System.Drawing.Point(18, 43);
            this.txtPermissionGroupName.Margin = new System.Windows.Forms.Padding(2);
            this.txtPermissionGroupName.Name = "txtPermissionGroupName";
            this.txtPermissionGroupName.Size = new System.Drawing.Size(222, 20);
            this.txtPermissionGroupName.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 26);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Name :";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 69);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Entity Name :";
            // 
            // txtPermissionGroupEntityName
            // 
            this.txtPermissionGroupEntityName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPermissionGroupEntityName.Location = new System.Drawing.Point(18, 85);
            this.txtPermissionGroupEntityName.Margin = new System.Windows.Forms.Padding(2);
            this.txtPermissionGroupEntityName.Name = "txtPermissionGroupEntityName";
            this.txtPermissionGroupEntityName.Size = new System.Drawing.Size(222, 20);
            this.txtPermissionGroupEntityName.TabIndex = 7;
            // 
            // lblStepInfo
            // 
            this.lblStepInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStepInfo.BackColor = System.Drawing.Color.AliceBlue;
            this.lblStepInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblStepInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStepInfo.ForeColor = System.Drawing.Color.MediumBlue;
            this.lblStepInfo.Location = new System.Drawing.Point(11, 10);
            this.lblStepInfo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblStepInfo.Name = "lblStepInfo";
            this.lblStepInfo.Size = new System.Drawing.Size(1015, 28);
            this.lblStepInfo.TabIndex = 4;
            this.lblStepInfo.Text = "Permission Designer";
            this.lblStepInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(951, 486);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 28);
            this.btnCancel.TabIndex = 45;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // btnGenarateScript
            // 
            this.btnGenarateScript.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGenarateScript.Location = new System.Drawing.Point(840, 486);
            this.btnGenarateScript.Name = "btnGenarateScript";
            this.btnGenarateScript.Size = new System.Drawing.Size(106, 28);
            this.btnGenarateScript.TabIndex = 53;
            this.btnGenarateScript.Text = "Generate Script";
            this.btnGenarateScript.UseVisualStyleBackColor = true;
            this.btnGenarateScript.Click += new System.EventHandler(this.GenarateScript_Click);
            // 
            // PermissionDesignerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1037, 525);
            this.Controls.Add(this.btnGenarateScript);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.pnlPage);
            this.Controls.Add(this.lblStepInfo);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "PermissionDesignerForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Permission Designer";
            this.pnlPage.ResumeLayout(false);
            this.gboxPermission.ResumeLayout(false);
            this.gboxPermission.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trbFlag)).EndInit();
            this.gboxPermissionGroup.ResumeLayout(false);
            this.gboxPermissionGroup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlPage;
        private System.Windows.Forms.Label lblStepInfo;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox gboxPermissionGroup;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPermissionGroupDescription;
        private System.Windows.Forms.TextBox txtPermissionGroupName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPermissionGroupEntityName;
        private System.Windows.Forms.ListBox lboxPermissions;
        private System.Windows.Forms.GroupBox gboxPermission;
        private System.Windows.Forms.TrackBar trbFlag;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtPermissionDesc;
        private System.Windows.Forms.TextBox txtPermissionName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblFlag;
        private System.Windows.Forms.Button btnApplyPermissionGroup;
        private System.Windows.Forms.Button btnGenarateScript;
        private System.Windows.Forms.Button btnDeletePermission;
        private System.Windows.Forms.Button btnAddPermission;
    }
}