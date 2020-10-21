namespace SPPC.Tadbir.Tools.SystemDesigner.Designers
{
    partial class ReportDesignerForm
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabGeneral = new System.Windows.Forms.TabPage();
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
            this.chkQuickReport = new System.Windows.Forms.CheckBox();
            this.chkSetAsDefault = new System.Windows.Forms.CheckBox();
            this.chkSystemReport = new System.Windows.Forms.CheckBox();
            this.btnSelect = new System.Windows.Forms.Button();
            this.txtServiceUrl = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbParent = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbSubsystem = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbListViews = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabParameters = new System.Windows.Forms.TabPage();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.grdParameters = new System.Windows.Forms.DataGridView();
            this.label7 = new System.Windows.Forms.Label();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabGeneral.SuspendLayout();
            this.grpReportInfo.SuspendLayout();
            this.tabParameters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdParameters)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabGeneral);
            this.tabControl1.Controls.Add(this.tabParameters);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(487, 404);
            this.tabControl1.TabIndex = 0;
            // 
            // tabGeneral
            // 
            this.tabGeneral.Controls.Add(this.grpReportInfo);
            this.tabGeneral.Controls.Add(this.cmbListViews);
            this.tabGeneral.Controls.Add(this.label1);
            this.tabGeneral.Location = new System.Drawing.Point(4, 22);
            this.tabGeneral.Margin = new System.Windows.Forms.Padding(2);
            this.tabGeneral.Name = "tabGeneral";
            this.tabGeneral.Padding = new System.Windows.Forms.Padding(2);
            this.tabGeneral.Size = new System.Drawing.Size(479, 378);
            this.tabGeneral.TabIndex = 0;
            this.tabGeneral.Text = "General";
            this.tabGeneral.UseVisualStyleBackColor = true;
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
            this.grpReportInfo.Controls.Add(this.chkQuickReport);
            this.grpReportInfo.Controls.Add(this.chkSetAsDefault);
            this.grpReportInfo.Controls.Add(this.chkSystemReport);
            this.grpReportInfo.Controls.Add(this.btnSelect);
            this.grpReportInfo.Controls.Add(this.txtServiceUrl);
            this.grpReportInfo.Controls.Add(this.label4);
            this.grpReportInfo.Controls.Add(this.cmbParent);
            this.grpReportInfo.Controls.Add(this.label3);
            this.grpReportInfo.Controls.Add(this.cmbSubsystem);
            this.grpReportInfo.Controls.Add(this.label2);
            this.grpReportInfo.Location = new System.Drawing.Point(6, 66);
            this.grpReportInfo.Margin = new System.Windows.Forms.Padding(2);
            this.grpReportInfo.Name = "grpReportInfo";
            this.grpReportInfo.Padding = new System.Windows.Forms.Padding(2);
            this.grpReportInfo.Size = new System.Drawing.Size(470, 311);
            this.grpReportInfo.TabIndex = 2;
            this.grpReportInfo.TabStop = false;
            this.grpReportInfo.Text = "Report Properties";
            // 
            // btnBrowseFa
            // 
            this.btnBrowseFa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseFa.Enabled = false;
            this.btnBrowseFa.Location = new System.Drawing.Point(406, 284);
            this.btnBrowseFa.Margin = new System.Windows.Forms.Padding(2);
            this.btnBrowseFa.Name = "btnBrowseFa";
            this.btnBrowseFa.Size = new System.Drawing.Size(56, 22);
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
            this.txtTemplateFa.Location = new System.Drawing.Point(7, 287);
            this.txtTemplateFa.Margin = new System.Windows.Forms.Padding(2);
            this.txtTemplateFa.Name = "txtTemplateFa";
            this.txtTemplateFa.Size = new System.Drawing.Size(396, 20);
            this.txtTemplateFa.TabIndex = 19;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(4, 268);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(109, 13);
            this.label9.TabIndex = 18;
            this.label9.Text = "Persian template path";
            // 
            // chkIsGroup
            // 
            this.chkIsGroup.AutoSize = true;
            this.chkIsGroup.Location = new System.Drawing.Point(141, 195);
            this.chkIsGroup.Margin = new System.Windows.Forms.Padding(2);
            this.chkIsGroup.Name = "chkIsGroup";
            this.chkIsGroup.Size = new System.Drawing.Size(66, 17);
            this.chkIsGroup.TabIndex = 14;
            this.chkIsGroup.Text = "Is Group";
            this.chkIsGroup.UseVisualStyleBackColor = true;
            // 
            // btnBrowseEn
            // 
            this.btnBrowseEn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseEn.Enabled = false;
            this.btnBrowseEn.Location = new System.Drawing.Point(406, 236);
            this.btnBrowseEn.Margin = new System.Windows.Forms.Padding(2);
            this.btnBrowseEn.Name = "btnBrowseEn";
            this.btnBrowseEn.Size = new System.Drawing.Size(56, 22);
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
            this.txtTemplateEn.Location = new System.Drawing.Point(7, 238);
            this.txtTemplateEn.Margin = new System.Windows.Forms.Padding(2);
            this.txtTemplateEn.Name = "txtTemplateEn";
            this.txtTemplateEn.Size = new System.Drawing.Size(396, 20);
            this.txtTemplateEn.TabIndex = 16;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(4, 219);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(108, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "English template path";
            // 
            // txtPersian
            // 
            this.txtPersian.Location = new System.Drawing.Point(245, 49);
            this.txtPersian.Margin = new System.Windows.Forms.Padding(2);
            this.txtPersian.Name = "txtPersian";
            this.txtPersian.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtPersian.Size = new System.Drawing.Size(218, 20);
            this.txtPersian.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(243, 32);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Persian Caption";
            // 
            // txtEnglish
            // 
            this.txtEnglish.Location = new System.Drawing.Point(7, 49);
            this.txtEnglish.Margin = new System.Windows.Forms.Padding(2);
            this.txtEnglish.Name = "txtEnglish";
            this.txtEnglish.Size = new System.Drawing.Size(225, 20);
            this.txtEnglish.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 32);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "English Caption";
            // 
            // chkQuickReport
            // 
            this.chkQuickReport.AutoSize = true;
            this.chkQuickReport.Checked = true;
            this.chkQuickReport.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkQuickReport.Location = new System.Drawing.Point(7, 195);
            this.chkQuickReport.Margin = new System.Windows.Forms.Padding(2);
            this.chkQuickReport.Name = "chkQuickReport";
            this.chkQuickReport.Size = new System.Drawing.Size(89, 17);
            this.chkQuickReport.TabIndex = 13;
            this.chkQuickReport.Text = "Quick Report";
            this.chkQuickReport.UseVisualStyleBackColor = true;
            this.chkQuickReport.CheckedChanged += new System.EventHandler(this.QuickReport_CheckedChanged);
            // 
            // chkSetAsDefault
            // 
            this.chkSetAsDefault.AutoSize = true;
            this.chkSetAsDefault.Checked = true;
            this.chkSetAsDefault.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSetAsDefault.Location = new System.Drawing.Point(141, 173);
            this.chkSetAsDefault.Margin = new System.Windows.Forms.Padding(2);
            this.chkSetAsDefault.Name = "chkSetAsDefault";
            this.chkSetAsDefault.Size = new System.Drawing.Size(94, 17);
            this.chkSetAsDefault.TabIndex = 12;
            this.chkSetAsDefault.Text = "Set As Default";
            this.chkSetAsDefault.UseVisualStyleBackColor = true;
            // 
            // chkSystemReport
            // 
            this.chkSystemReport.AutoSize = true;
            this.chkSystemReport.Checked = true;
            this.chkSystemReport.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSystemReport.Location = new System.Drawing.Point(7, 173);
            this.chkSystemReport.Margin = new System.Windows.Forms.Padding(2);
            this.chkSystemReport.Name = "chkSystemReport";
            this.chkSystemReport.Size = new System.Drawing.Size(95, 17);
            this.chkSystemReport.TabIndex = 11;
            this.chkSystemReport.Text = "System Report";
            this.chkSystemReport.UseVisualStyleBackColor = true;
            // 
            // btnSelect
            // 
            this.btnSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelect.Location = new System.Drawing.Point(406, 141);
            this.btnSelect.Margin = new System.Windows.Forms.Padding(2);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(56, 22);
            this.btnSelect.TabIndex = 10;
            this.btnSelect.Text = "Select...";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.Select_Click);
            // 
            // txtServiceUrl
            // 
            this.txtServiceUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtServiceUrl.Location = new System.Drawing.Point(7, 143);
            this.txtServiceUrl.Margin = new System.Windows.Forms.Padding(2);
            this.txtServiceUrl.Name = "txtServiceUrl";
            this.txtServiceUrl.Size = new System.Drawing.Size(396, 20);
            this.txtServiceUrl.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 127);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Service Url";
            // 
            // cmbParent
            // 
            this.cmbParent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbParent.FormattingEnabled = true;
            this.cmbParent.Location = new System.Drawing.Point(245, 95);
            this.cmbParent.Margin = new System.Windows.Forms.Padding(2);
            this.cmbParent.Name = "cmbParent";
            this.cmbParent.Size = new System.Drawing.Size(218, 21);
            this.cmbParent.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(243, 79);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Parent";
            // 
            // cmbSubsystem
            // 
            this.cmbSubsystem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSubsystem.FormattingEnabled = true;
            this.cmbSubsystem.Items.AddRange(new object[] {
            "Administration",
            "Account"});
            this.cmbSubsystem.Location = new System.Drawing.Point(7, 95);
            this.cmbSubsystem.Margin = new System.Windows.Forms.Padding(2);
            this.cmbSubsystem.Name = "cmbSubsystem";
            this.cmbSubsystem.Size = new System.Drawing.Size(225, 21);
            this.cmbSubsystem.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 79);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Subsystem";
            // 
            // cmbListViews
            // 
            this.cmbListViews.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbListViews.FormattingEnabled = true;
            this.cmbListViews.Location = new System.Drawing.Point(6, 28);
            this.cmbListViews.Margin = new System.Windows.Forms.Padding(2);
            this.cmbListViews.Name = "cmbListViews";
            this.cmbListViews.Size = new System.Drawing.Size(178, 21);
            this.cmbListViews.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 12);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select list view";
            // 
            // tabParameters
            // 
            this.tabParameters.Controls.Add(this.btnDelete);
            this.tabParameters.Controls.Add(this.btnEdit);
            this.tabParameters.Controls.Add(this.btnAdd);
            this.tabParameters.Controls.Add(this.grdParameters);
            this.tabParameters.Controls.Add(this.label7);
            this.tabParameters.Location = new System.Drawing.Point(4, 22);
            this.tabParameters.Margin = new System.Windows.Forms.Padding(2);
            this.tabParameters.Name = "tabParameters";
            this.tabParameters.Padding = new System.Windows.Forms.Padding(2);
            this.tabParameters.Size = new System.Drawing.Size(479, 378);
            this.tabParameters.TabIndex = 1;
            this.tabParameters.Text = "Parameters";
            this.tabParameters.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(130, 349);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(2);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(56, 24);
            this.btnDelete.TabIndex = 4;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.Delete_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(69, 349);
            this.btnEdit.Margin = new System.Windows.Forms.Padding(2);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(56, 24);
            this.btnEdit.TabIndex = 3;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.Edit_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(8, 349);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(2);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(56, 24);
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
            this.grdParameters.Location = new System.Drawing.Point(8, 41);
            this.grdParameters.Margin = new System.Windows.Forms.Padding(2);
            this.grdParameters.Name = "grdParameters";
            this.grdParameters.ReadOnly = true;
            this.grdParameters.RowTemplate.Height = 24;
            this.grdParameters.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdParameters.Size = new System.Drawing.Size(468, 298);
            this.grdParameters.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 15);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(96, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Current parameters";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSubmit.Location = new System.Drawing.Point(9, 416);
            this.btnSubmit.Margin = new System.Windows.Forms.Padding(2);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(99, 24);
            this.btnSubmit.TabIndex = 18;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.Submit_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(412, 416);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(68, 24);
            this.btnCancel.TabIndex = 19;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // ReportDesignerForm
            // 
            this.AcceptButton = this.btnSubmit;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(488, 445);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.tabControl1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimizeBox = false;
            this.Name = "ReportDesignerForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Report Designer";
            this.tabControl1.ResumeLayout(false);
            this.tabGeneral.ResumeLayout(false);
            this.tabGeneral.PerformLayout();
            this.grpReportInfo.ResumeLayout(false);
            this.grpReportInfo.PerformLayout();
            this.tabParameters.ResumeLayout(false);
            this.tabParameters.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdParameters)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabGeneral;
        private System.Windows.Forms.GroupBox grpReportInfo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabParameters;
        private System.Windows.Forms.Button btnBrowseEn;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.DataGridView grdParameters;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnBrowseFa;
        private System.Windows.Forms.Label label9;
        public System.Windows.Forms.TextBox txtPersian;
        public System.Windows.Forms.TextBox txtEnglish;
        public System.Windows.Forms.CheckBox chkQuickReport;
        public System.Windows.Forms.CheckBox chkSetAsDefault;
        public System.Windows.Forms.CheckBox chkSystemReport;
        public System.Windows.Forms.TextBox txtServiceUrl;
        public System.Windows.Forms.ComboBox cmbParent;
        public System.Windows.Forms.ComboBox cmbSubsystem;
        public System.Windows.Forms.ComboBox cmbListViews;
        public System.Windows.Forms.TextBox txtTemplateEn;
        public System.Windows.Forms.CheckBox chkIsGroup;
        public System.Windows.Forms.TextBox txtTemplateFa;
    }
}