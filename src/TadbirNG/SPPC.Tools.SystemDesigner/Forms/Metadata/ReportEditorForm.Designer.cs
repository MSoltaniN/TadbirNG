namespace SPPC.Tools.SystemDesigner.Designers
{
    partial class ReportEditorForm
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
            this.tabNewReport = new System.Windows.Forms.TabControl();
            this.tabView = new System.Windows.Forms.TabPage();
            this.label10 = new System.Windows.Forms.Label();
            this.tvViewModels = new System.Windows.Forms.TreeView();
            this.tabReport = new System.Windows.Forms.TabPage();
            this.grpReportInfo = new System.Windows.Forms.GroupBox();
            this.btnBrowseFa = new System.Windows.Forms.Button();
            this.txtTemplateFa = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.chkIsGroup = new System.Windows.Forms.CheckBox();
            this.btnBrowseEn = new System.Windows.Forms.Button();
            this.txtTemplateEn = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtPersian = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtEnglish = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.chkIsDynamic = new System.Windows.Forms.CheckBox();
            this.chkIsDefault = new System.Windows.Forms.CheckBox();
            this.chkIsSystem = new System.Windows.Forms.CheckBox();
            this.btnSelect = new System.Windows.Forms.Button();
            this.txtServiceUrl = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbParent = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbSubsystem = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tabParameters = new System.Windows.Forms.TabPage();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.grdParameters = new System.Windows.Forms.DataGridView();
            this.label7 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tabNewReport.SuspendLayout();
            this.tabView.SuspendLayout();
            this.tabReport.SuspendLayout();
            this.grpReportInfo.SuspendLayout();
            this.tabParameters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdParameters)).BeginInit();
            this.SuspendLayout();
            // 
            // tabNewReport
            // 
            this.tabNewReport.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabNewReport.Controls.Add(this.tabView);
            this.tabNewReport.Controls.Add(this.tabReport);
            this.tabNewReport.Controls.Add(this.tabParameters);
            this.tabNewReport.Location = new System.Drawing.Point(0, 0);
            this.tabNewReport.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabNewReport.Name = "tabNewReport";
            this.tabNewReport.SelectedIndex = 0;
            this.tabNewReport.Size = new System.Drawing.Size(568, 466);
            this.tabNewReport.TabIndex = 0;
            // 
            // tabView
            // 
            this.tabView.Controls.Add(this.label10);
            this.tabView.Controls.Add(this.tvViewModels);
            this.tabView.Location = new System.Drawing.Point(4, 24);
            this.tabView.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabView.Name = "tabView";
            this.tabView.Size = new System.Drawing.Size(560, 438);
            this.tabView.TabIndex = 2;
            this.tabView.Text = "View";
            this.tabView.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(7, 10);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(96, 15);
            this.label10.TabIndex = 10;
            this.label10.Text = "All view models :";
            // 
            // tvViewModels
            // 
            this.tvViewModels.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tvViewModels.HideSelection = false;
            this.tvViewModels.Indent = 15;
            this.tvViewModels.Location = new System.Drawing.Point(7, 32);
            this.tvViewModels.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tvViewModels.Name = "tvViewModels";
            this.tvViewModels.Size = new System.Drawing.Size(547, 397);
            this.tvViewModels.TabIndex = 9;
            // 
            // tabReport
            // 
            this.tabReport.Controls.Add(this.grpReportInfo);
            this.tabReport.Location = new System.Drawing.Point(4, 24);
            this.tabReport.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabReport.Name = "tabReport";
            this.tabReport.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabReport.Size = new System.Drawing.Size(560, 438);
            this.tabReport.TabIndex = 0;
            this.tabReport.Text = "Report";
            this.tabReport.UseVisualStyleBackColor = true;
            // 
            // grpReportInfo
            // 
            this.grpReportInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpReportInfo.Controls.Add(this.btnBrowseFa);
            this.grpReportInfo.Controls.Add(this.txtTemplateFa);
            this.grpReportInfo.Controls.Add(this.label9);
            this.grpReportInfo.Controls.Add(this.chkIsGroup);
            this.grpReportInfo.Controls.Add(this.btnBrowseEn);
            this.grpReportInfo.Controls.Add(this.txtTemplateEn);
            this.grpReportInfo.Controls.Add(this.label8);
            this.grpReportInfo.Controls.Add(this.txtPersian);
            this.grpReportInfo.Controls.Add(this.label6);
            this.grpReportInfo.Controls.Add(this.txtEnglish);
            this.grpReportInfo.Controls.Add(this.label5);
            this.grpReportInfo.Controls.Add(this.chkIsDynamic);
            this.grpReportInfo.Controls.Add(this.chkIsDefault);
            this.grpReportInfo.Controls.Add(this.chkIsSystem);
            this.grpReportInfo.Controls.Add(this.btnSelect);
            this.grpReportInfo.Controls.Add(this.txtServiceUrl);
            this.grpReportInfo.Controls.Add(this.label4);
            this.grpReportInfo.Controls.Add(this.cmbParent);
            this.grpReportInfo.Controls.Add(this.label3);
            this.grpReportInfo.Controls.Add(this.cmbSubsystem);
            this.grpReportInfo.Controls.Add(this.label2);
            this.grpReportInfo.Location = new System.Drawing.Point(7, 14);
            this.grpReportInfo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpReportInfo.Name = "grpReportInfo";
            this.grpReportInfo.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpReportInfo.Size = new System.Drawing.Size(549, 423);
            this.grpReportInfo.TabIndex = 2;
            this.grpReportInfo.TabStop = false;
            this.grpReportInfo.Text = "Report Properties";
            // 
            // btnBrowseFa
            // 
            this.btnBrowseFa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseFa.Enabled = false;
            this.btnBrowseFa.Location = new System.Drawing.Point(471, 388);
            this.btnBrowseFa.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnBrowseFa.Name = "btnBrowseFa";
            this.btnBrowseFa.Size = new System.Drawing.Size(66, 26);
            this.btnBrowseFa.TabIndex = 20;
            this.btnBrowseFa.Text = "Browse...";
            this.btnBrowseFa.UseVisualStyleBackColor = true;
            this.btnBrowseFa.Click += new System.EventHandler(this.BrowseFa_Click);
            // 
            // txtTemplateFa
            // 
            this.txtTemplateFa.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTemplateFa.Enabled = false;
            this.txtTemplateFa.Location = new System.Drawing.Point(5, 392);
            this.txtTemplateFa.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtTemplateFa.Name = "txtTemplateFa";
            this.txtTemplateFa.Size = new System.Drawing.Size(462, 23);
            this.txtTemplateFa.TabIndex = 19;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(2, 369);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(128, 15);
            this.label9.TabIndex = 18;
            this.label9.Text = "Persian template path :";
            // 
            // chkIsGroup
            // 
            this.chkIsGroup.AutoSize = true;
            this.chkIsGroup.Location = new System.Drawing.Point(162, 285);
            this.chkIsGroup.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkIsGroup.Name = "chkIsGroup";
            this.chkIsGroup.Size = new System.Drawing.Size(97, 19);
            this.chkIsGroup.TabIndex = 14;
            this.chkIsGroup.Text = "Report Group";
            this.chkIsGroup.UseVisualStyleBackColor = true;
            // 
            // btnBrowseEn
            // 
            this.btnBrowseEn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseEn.Enabled = false;
            this.btnBrowseEn.Location = new System.Drawing.Point(471, 332);
            this.btnBrowseEn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnBrowseEn.Name = "btnBrowseEn";
            this.btnBrowseEn.Size = new System.Drawing.Size(66, 26);
            this.btnBrowseEn.TabIndex = 17;
            this.btnBrowseEn.Text = "Browse...";
            this.btnBrowseEn.UseVisualStyleBackColor = true;
            this.btnBrowseEn.Click += new System.EventHandler(this.Browse_Click);
            // 
            // txtTemplateEn
            // 
            this.txtTemplateEn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTemplateEn.Enabled = false;
            this.txtTemplateEn.Location = new System.Drawing.Point(5, 334);
            this.txtTemplateEn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtTemplateEn.Name = "txtTemplateEn";
            this.txtTemplateEn.Size = new System.Drawing.Size(462, 23);
            this.txtTemplateEn.TabIndex = 16;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(2, 313);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(128, 15);
            this.label8.TabIndex = 15;
            this.label8.Text = "English template path :";
            // 
            // txtPersian
            // 
            this.txtPersian.Location = new System.Drawing.Point(284, 116);
            this.txtPersian.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtPersian.Name = "txtPersian";
            this.txtPersian.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtPersian.Size = new System.Drawing.Size(253, 23);
            this.txtPersian.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(281, 97);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(96, 15);
            this.label6.TabIndex = 2;
            this.label6.Text = "Persian Caption :";
            // 
            // txtEnglish
            // 
            this.txtEnglish.Location = new System.Drawing.Point(5, 116);
            this.txtEnglish.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtEnglish.Name = "txtEnglish";
            this.txtEnglish.Size = new System.Drawing.Size(262, 23);
            this.txtEnglish.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(2, 97);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 15);
            this.label5.TabIndex = 0;
            this.label5.Text = "English Caption :";
            // 
            // chkIsDynamic
            // 
            this.chkIsDynamic.AutoSize = true;
            this.chkIsDynamic.Location = new System.Drawing.Point(5, 285);
            this.chkIsDynamic.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkIsDynamic.Name = "chkIsDynamic";
            this.chkIsDynamic.Size = new System.Drawing.Size(95, 19);
            this.chkIsDynamic.TabIndex = 13;
            this.chkIsDynamic.Text = "Quick Report";
            this.chkIsDynamic.UseVisualStyleBackColor = true;
            this.chkIsDynamic.CheckedChanged += new System.EventHandler(this.QuickReport_CheckedChanged);
            // 
            // chkIsDefault
            // 
            this.chkIsDefault.AutoSize = true;
            this.chkIsDefault.Location = new System.Drawing.Point(162, 260);
            this.chkIsDefault.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkIsDefault.Name = "chkIsDefault";
            this.chkIsDefault.Size = new System.Drawing.Size(99, 19);
            this.chkIsDefault.TabIndex = 12;
            this.chkIsDefault.Text = "Set As Default";
            this.chkIsDefault.UseVisualStyleBackColor = true;
            // 
            // chkIsSystem
            // 
            this.chkIsSystem.AutoSize = true;
            this.chkIsSystem.Location = new System.Drawing.Point(5, 260);
            this.chkIsSystem.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkIsSystem.Name = "chkIsSystem";
            this.chkIsSystem.Size = new System.Drawing.Size(102, 19);
            this.chkIsSystem.TabIndex = 11;
            this.chkIsSystem.Text = "System Report";
            this.chkIsSystem.UseVisualStyleBackColor = true;
            // 
            // btnSelect
            // 
            this.btnSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelect.Location = new System.Drawing.Point(471, 223);
            this.btnSelect.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(66, 26);
            this.btnSelect.TabIndex = 10;
            this.btnSelect.Text = "Select...";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.Select_Click);
            // 
            // txtServiceUrl
            // 
            this.txtServiceUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtServiceUrl.Location = new System.Drawing.Point(5, 225);
            this.txtServiceUrl.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtServiceUrl.Name = "txtServiceUrl";
            this.txtServiceUrl.Size = new System.Drawing.Size(462, 23);
            this.txtServiceUrl.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(2, 206);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 15);
            this.label4.TabIndex = 8;
            this.label4.Text = "Service Url :";
            // 
            // cmbParent
            // 
            this.cmbParent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbParent.FormattingEnabled = true;
            this.cmbParent.Location = new System.Drawing.Point(284, 170);
            this.cmbParent.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmbParent.Name = "cmbParent";
            this.cmbParent.Size = new System.Drawing.Size(253, 23);
            this.cmbParent.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(281, 152);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 15);
            this.label3.TabIndex = 6;
            this.label3.Text = "Parent :";
            // 
            // cmbSubsystem
            // 
            this.cmbSubsystem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSubsystem.FormattingEnabled = true;
            this.cmbSubsystem.Items.AddRange(new object[] {
            "Administration",
            "Accounting",
            "Treasury"});
            this.cmbSubsystem.Location = new System.Drawing.Point(5, 170);
            this.cmbSubsystem.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmbSubsystem.Name = "cmbSubsystem";
            this.cmbSubsystem.Size = new System.Drawing.Size(262, 23);
            this.cmbSubsystem.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 152);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "Subsystem :";
            // 
            // tabParameters
            // 
            this.tabParameters.Controls.Add(this.btnDelete);
            this.tabParameters.Controls.Add(this.btnEdit);
            this.tabParameters.Controls.Add(this.btnAdd);
            this.tabParameters.Controls.Add(this.grdParameters);
            this.tabParameters.Controls.Add(this.label7);
            this.tabParameters.Location = new System.Drawing.Point(4, 24);
            this.tabParameters.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabParameters.Name = "tabParameters";
            this.tabParameters.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabParameters.Size = new System.Drawing.Size(560, 438);
            this.tabParameters.TabIndex = 1;
            this.tabParameters.Text = "Parameters";
            this.tabParameters.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(151, 403);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(66, 28);
            this.btnDelete.TabIndex = 4;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.Delete_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(80, 403);
            this.btnEdit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(66, 28);
            this.btnEdit.TabIndex = 3;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.Edit_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(10, 403);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(66, 28);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.Add_Click);
            // 
            // grdParameters
            // 
            this.grdParameters.AllowUserToAddRows = false;
            this.grdParameters.AllowUserToDeleteRows = false;
            this.grdParameters.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.grdParameters.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.grdParameters.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdParameters.Location = new System.Drawing.Point(10, 38);
            this.grdParameters.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grdParameters.Name = "grdParameters";
            this.grdParameters.ReadOnly = true;
            this.grdParameters.RowHeadersWidth = 51;
            this.grdParameters.RowTemplate.Height = 24;
            this.grdParameters.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdParameters.Size = new System.Drawing.Size(546, 357);
            this.grdParameters.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 17);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(115, 15);
            this.label7.TabIndex = 0;
            this.label7.Text = "Current parameters :";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.Location = new System.Drawing.Point(10, 482);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 26);
            this.btnSave.TabIndex = 18;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.Save_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(480, 482);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 26);
            this.btnCancel.TabIndex = 19;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // ReportEditorForm
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(570, 514);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.tabNewReport);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MinimizeBox = false;
            this.Name = "ReportEditorForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Report Properties";
            this.tabNewReport.ResumeLayout(false);
            this.tabView.ResumeLayout(false);
            this.tabView.PerformLayout();
            this.tabReport.ResumeLayout(false);
            this.grpReportInfo.ResumeLayout(false);
            this.grpReportInfo.PerformLayout();
            this.tabParameters.ResumeLayout(false);
            this.tabParameters.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdParameters)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabNewReport;
        private System.Windows.Forms.TabPage tabReport;
        private System.Windows.Forms.GroupBox grpReportInfo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabPage tabParameters;
        private System.Windows.Forms.Button btnBrowseEn;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.DataGridView grdParameters;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnBrowseFa;
        private System.Windows.Forms.Label label9;
        public System.Windows.Forms.TextBox txtPersian;
        public System.Windows.Forms.TextBox txtEnglish;
        public System.Windows.Forms.CheckBox chkIsDynamic;
        public System.Windows.Forms.CheckBox chkIsDefault;
        public System.Windows.Forms.CheckBox chkIsSystem;
        public System.Windows.Forms.TextBox txtServiceUrl;
        public System.Windows.Forms.ComboBox cmbParent;
        public System.Windows.Forms.ComboBox cmbSubsystem;
        public System.Windows.Forms.TextBox txtTemplateEn;
        public System.Windows.Forms.CheckBox chkIsGroup;
        public System.Windows.Forms.TextBox txtTemplateFa;
        private System.Windows.Forms.TabPage tabView;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TreeView tvViewModels;
    }
}