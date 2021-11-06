namespace SPPC.Tools.SystemDesigner.Designers
{
    partial class PermissionEditorForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnDeletePermission = new System.Windows.Forms.Button();
            this.btnAddPermission = new System.Windows.Forms.Button();
            this.gboxPermission = new System.Windows.Forms.GroupBox();
            this.trbFlag = new System.Windows.Forms.TrackBar();
            this.label5 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lblFlag = new System.Windows.Forms.Label();
            this.lboxPermissions = new System.Windows.Forms.ListBox();
            this.gboxPermissionGroup = new System.Windows.Forms.GroupBox();
            this.txtGroupName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtGroupDescription = new System.Windows.Forms.TextBox();
            this.btnApplyPermissionGroup = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtEntityName = new System.Windows.Forms.TextBox();
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
            this.pnlPage.Controls.Add(this.label1);
            this.pnlPage.Controls.Add(this.btnDeletePermission);
            this.pnlPage.Controls.Add(this.btnAddPermission);
            this.pnlPage.Controls.Add(this.gboxPermission);
            this.pnlPage.Controls.Add(this.lboxPermissions);
            this.pnlPage.Controls.Add(this.gboxPermissionGroup);
            this.pnlPage.Location = new System.Drawing.Point(13, 23);
            this.pnlPage.Name = "pnlPage";
            this.pnlPage.Size = new System.Drawing.Size(984, 466);
            this.pnlPage.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(421, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Permissions :";
            // 
            // btnDeletePermission
            // 
            this.btnDeletePermission.Location = new System.Drawing.Point(539, 414);
            this.btnDeletePermission.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnDeletePermission.Name = "btnDeletePermission";
            this.btnDeletePermission.Size = new System.Drawing.Size(101, 35);
            this.btnDeletePermission.TabIndex = 4;
            this.btnDeletePermission.Text = "Delete";
            this.btnDeletePermission.UseVisualStyleBackColor = true;
            this.btnDeletePermission.Click += new System.EventHandler(this.DeletePermission_Click);
            // 
            // btnAddPermission
            // 
            this.btnAddPermission.Location = new System.Drawing.Point(424, 414);
            this.btnAddPermission.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnAddPermission.Name = "btnAddPermission";
            this.btnAddPermission.Size = new System.Drawing.Size(107, 35);
            this.btnAddPermission.TabIndex = 3;
            this.btnAddPermission.Text = "Add";
            this.btnAddPermission.UseVisualStyleBackColor = true;
            this.btnAddPermission.Click += new System.EventHandler(this.AddPermission_Click);
            // 
            // gboxPermission
            // 
            this.gboxPermission.Controls.Add(this.trbFlag);
            this.gboxPermission.Controls.Add(this.label5);
            this.gboxPermission.Controls.Add(this.txtDescription);
            this.gboxPermission.Controls.Add(this.txtName);
            this.gboxPermission.Controls.Add(this.label6);
            this.gboxPermission.Controls.Add(this.lblFlag);
            this.gboxPermission.Location = new System.Drawing.Point(649, 20);
            this.gboxPermission.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gboxPermission.Name = "gboxPermission";
            this.gboxPermission.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gboxPermission.Size = new System.Drawing.Size(324, 429);
            this.gboxPermission.TabIndex = 5;
            this.gboxPermission.TabStop = false;
            this.gboxPermission.Text = "Permission Details";
            // 
            // trbFlag
            // 
            this.trbFlag.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trbFlag.Location = new System.Drawing.Point(27, 134);
            this.trbFlag.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.trbFlag.Maximum = 50;
            this.trbFlag.Name = "trbFlag";
            this.trbFlag.Size = new System.Drawing.Size(285, 56);
            this.trbFlag.SmallChange = 2;
            this.trbFlag.TabIndex = 3;
            this.trbFlag.Value = 1;
            this.trbFlag.Scroll += new System.EventHandler(this.Flag_Scroll);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(21, 205);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(92, 20);
            this.label5.TabIndex = 4;
            this.label5.Text = "Description :";
            // 
            // txtDescription
            // 
            this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescription.Location = new System.Drawing.Point(24, 228);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(288, 181);
            this.txtDescription.TabIndex = 5;
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.Location = new System.Drawing.Point(28, 58);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(284, 27);
            this.txtName.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(24, 32);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 20);
            this.label6.TabIndex = 0;
            this.label6.Text = "Name :";
            // 
            // lblFlag
            // 
            this.lblFlag.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFlag.AutoSize = true;
            this.lblFlag.Location = new System.Drawing.Point(24, 109);
            this.lblFlag.Name = "lblFlag";
            this.lblFlag.Size = new System.Drawing.Size(44, 20);
            this.lblFlag.TabIndex = 2;
            this.lblFlag.Text = "Flag :";
            // 
            // lboxPermissions
            // 
            this.lboxPermissions.FormattingEnabled = true;
            this.lboxPermissions.ItemHeight = 20;
            this.lboxPermissions.Location = new System.Drawing.Point(424, 46);
            this.lboxPermissions.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lboxPermissions.Name = "lboxPermissions";
            this.lboxPermissions.Size = new System.Drawing.Size(217, 344);
            this.lboxPermissions.TabIndex = 2;
            this.lboxPermissions.SelectedIndexChanged += new System.EventHandler(this.Permissions_SelectedIndexChanged);
            // 
            // gboxPermissionGroup
            // 
            this.gboxPermissionGroup.Controls.Add(this.txtGroupName);
            this.gboxPermissionGroup.Controls.Add(this.label3);
            this.gboxPermissionGroup.Controls.Add(this.label4);
            this.gboxPermissionGroup.Controls.Add(this.txtGroupDescription);
            this.gboxPermissionGroup.Controls.Add(this.btnApplyPermissionGroup);
            this.gboxPermissionGroup.Controls.Add(this.label2);
            this.gboxPermissionGroup.Controls.Add(this.txtEntityName);
            this.gboxPermissionGroup.Location = new System.Drawing.Point(19, 20);
            this.gboxPermissionGroup.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gboxPermissionGroup.Name = "gboxPermissionGroup";
            this.gboxPermissionGroup.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gboxPermissionGroup.Size = new System.Drawing.Size(361, 429);
            this.gboxPermissionGroup.TabIndex = 0;
            this.gboxPermissionGroup.TabStop = false;
            this.gboxPermissionGroup.Text = "Permission Group";
            // 
            // txtGroupName
            // 
            this.txtGroupName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGroupName.Location = new System.Drawing.Point(23, 134);
            this.txtGroupName.Name = "txtGroupName";
            this.txtGroupName.Size = new System.Drawing.Size(295, 27);
            this.txtGroupName.TabIndex = 3;
            this.txtGroupName.Enter += new System.EventHandler(this.GroupName_Enter);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Name :";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 171);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 20);
            this.label4.TabIndex = 4;
            this.label4.Text = "Description :";
            // 
            // txtGroupDescription
            // 
            this.txtGroupDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGroupDescription.Location = new System.Drawing.Point(24, 195);
            this.txtGroupDescription.Multiline = true;
            this.txtGroupDescription.Name = "txtGroupDescription";
            this.txtGroupDescription.Size = new System.Drawing.Size(295, 175);
            this.txtGroupDescription.TabIndex = 5;
            // 
            // btnApplyPermissionGroup
            // 
            this.btnApplyPermissionGroup.Location = new System.Drawing.Point(23, 383);
            this.btnApplyPermissionGroup.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnApplyPermissionGroup.Name = "btnApplyPermissionGroup";
            this.btnApplyPermissionGroup.Size = new System.Drawing.Size(199, 35);
            this.btnApplyPermissionGroup.TabIndex = 6;
            this.btnApplyPermissionGroup.Text = "Add Default Permissions";
            this.btnApplyPermissionGroup.UseVisualStyleBackColor = true;
            this.btnApplyPermissionGroup.Click += new System.EventHandler(this.AddDefaultPermissions_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "Entity Name :";
            // 
            // txtEntityName
            // 
            this.txtEntityName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEntityName.Location = new System.Drawing.Point(23, 71);
            this.txtEntityName.Name = "txtEntityName";
            this.txtEntityName.Size = new System.Drawing.Size(295, 27);
            this.txtEntityName.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(898, 513);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 35);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // btnGenarateScript
            // 
            this.btnGenarateScript.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGenarateScript.Location = new System.Drawing.Point(750, 513);
            this.btnGenarateScript.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnGenarateScript.Name = "btnGenarateScript";
            this.btnGenarateScript.Size = new System.Drawing.Size(141, 35);
            this.btnGenarateScript.TabIndex = 2;
            this.btnGenarateScript.Text = "Generate Script";
            this.btnGenarateScript.UseVisualStyleBackColor = true;
            this.btnGenarateScript.Click += new System.EventHandler(this.GenarateScript_Click);
            // 
            // PermissionEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(1009, 560);
            this.Controls.Add(this.btnGenarateScript);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.pnlPage);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PermissionEditorForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Permission Properties";
            this.pnlPage.ResumeLayout(false);
            this.pnlPage.PerformLayout();
            this.gboxPermission.ResumeLayout(false);
            this.gboxPermission.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trbFlag)).EndInit();
            this.gboxPermissionGroup.ResumeLayout(false);
            this.gboxPermissionGroup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlPage;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox gboxPermissionGroup;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtGroupDescription;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtEntityName;
        private System.Windows.Forms.ListBox lboxPermissions;
        private System.Windows.Forms.GroupBox gboxPermission;
        private System.Windows.Forms.TrackBar trbFlag;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblFlag;
        private System.Windows.Forms.Button btnApplyPermissionGroup;
        private System.Windows.Forms.Button btnGenarateScript;
        private System.Windows.Forms.Button btnDeletePermission;
        private System.Windows.Forms.Button btnAddPermission;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtGroupName;
        private System.Windows.Forms.Label label3;
    }
}