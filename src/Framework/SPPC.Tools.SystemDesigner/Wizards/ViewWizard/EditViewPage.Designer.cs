namespace SPPC.Tools.SystemDesigner.Wizards.ViewWizard
{
    partial class EditViewPage
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
            this.tvViewModels = new System.Windows.Forms.TreeView();
            this.label1 = new System.Windows.Forms.Label();
            this.chkEnableCartable = new System.Windows.Forms.CheckBox();
            this.chkIsHierarchy = new System.Windows.Forms.CheckBox();
            this.txtFetchUrl = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.gboxViewModel = new System.Windows.Forms.GroupBox();
            this.cmbEntityType = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSearchUrl = new System.Windows.Forms.TextBox();
            this.gboxViewModel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tvViewModels
            // 
            this.tvViewModels.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tvViewModels.Indent = 5;
            this.tvViewModels.Location = new System.Drawing.Point(12, 39);
            this.tvViewModels.Margin = new System.Windows.Forms.Padding(2);
            this.tvViewModels.Name = "tvViewModels";
            this.tvViewModels.Size = new System.Drawing.Size(291, 418);
            this.tvViewModels.TabIndex = 0;
            this.tvViewModels.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.ViewModels_AfterSelect);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 20);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "All view models :";
            // 
            // chkEnableCartable
            // 
            this.chkEnableCartable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkEnableCartable.AutoSize = true;
            this.chkEnableCartable.Location = new System.Drawing.Point(15, 396);
            this.chkEnableCartable.Margin = new System.Windows.Forms.Padding(2);
            this.chkEnableCartable.Name = "chkEnableCartable";
            this.chkEnableCartable.Size = new System.Drawing.Size(115, 17);
            this.chkEnableCartable.TabIndex = 13;
            this.chkEnableCartable.Text = "Cartable-integrated";
            this.chkEnableCartable.UseVisualStyleBackColor = true;
            // 
            // chkIsHierarchy
            // 
            this.chkIsHierarchy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkIsHierarchy.AutoSize = true;
            this.chkIsHierarchy.Location = new System.Drawing.Point(15, 374);
            this.chkIsHierarchy.Margin = new System.Windows.Forms.Padding(2);
            this.chkIsHierarchy.Name = "chkIsHierarchy";
            this.chkIsHierarchy.Size = new System.Drawing.Size(82, 17);
            this.chkIsHierarchy.TabIndex = 12;
            this.chkIsHierarchy.Text = "Hierarchical";
            this.chkIsHierarchy.UseVisualStyleBackColor = true;
            // 
            // txtFetchUrl
            // 
            this.txtFetchUrl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtFetchUrl.Location = new System.Drawing.Point(16, 244);
            this.txtFetchUrl.Margin = new System.Windows.Forms.Padding(2);
            this.txtFetchUrl.Name = "txtFetchUrl";
            this.txtFetchUrl.Size = new System.Drawing.Size(222, 20);
            this.txtFetchUrl.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 228);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Fetch URL :";
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtName.Location = new System.Drawing.Point(16, 202);
            this.txtName.Margin = new System.Windows.Forms.Padding(2);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(222, 20);
            this.txtName.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 187);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Name :";
            // 
            // gboxViewModel
            // 
            this.gboxViewModel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gboxViewModel.Controls.Add(this.cmbEntityType);
            this.gboxViewModel.Controls.Add(this.label5);
            this.gboxViewModel.Controls.Add(this.label4);
            this.gboxViewModel.Controls.Add(this.txtSearchUrl);
            this.gboxViewModel.Controls.Add(this.txtName);
            this.gboxViewModel.Controls.Add(this.chkEnableCartable);
            this.gboxViewModel.Controls.Add(this.label3);
            this.gboxViewModel.Controls.Add(this.chkIsHierarchy);
            this.gboxViewModel.Controls.Add(this.label2);
            this.gboxViewModel.Controls.Add(this.txtFetchUrl);
            this.gboxViewModel.Location = new System.Drawing.Point(327, 34);
            this.gboxViewModel.Name = "gboxViewModel";
            this.gboxViewModel.Size = new System.Drawing.Size(271, 422);
            this.gboxViewModel.TabIndex = 3;
            this.gboxViewModel.TabStop = false;
            this.gboxViewModel.Text = "View Model";
            // 
            // cmbEntityType
            // 
            this.cmbEntityType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmbEntityType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEntityType.FormattingEnabled = true;
            this.cmbEntityType.Items.AddRange(new object[] {
            "(not set)",
            "Core",
            "Fiscal",
            "Base",
            "Operational"});
            this.cmbEntityType.Location = new System.Drawing.Point(15, 334);
            this.cmbEntityType.Margin = new System.Windows.Forms.Padding(2);
            this.cmbEntityType.Name = "cmbEntityType";
            this.cmbEntityType.Size = new System.Drawing.Size(120, 21);
            this.cmbEntityType.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 318);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Entity type :";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 270);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Search URL :";
            // 
            // txtSearchUrl
            // 
            this.txtSearchUrl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtSearchUrl.Location = new System.Drawing.Point(16, 286);
            this.txtSearchUrl.Margin = new System.Windows.Forms.Padding(2);
            this.txtSearchUrl.Name = "txtSearchUrl";
            this.txtSearchUrl.Size = new System.Drawing.Size(222, 20);
            this.txtSearchUrl.TabIndex = 9;
            // 
            // EditViewPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gboxViewModel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tvViewModels);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "EditViewPage";
            this.Size = new System.Drawing.Size(609, 469);
            this.gboxViewModel.ResumeLayout(false);
            this.gboxViewModel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView tvViewModels;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkEnableCartable;
        private System.Windows.Forms.CheckBox chkIsHierarchy;
        private System.Windows.Forms.TextBox txtFetchUrl;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox gboxViewModel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSearchUrl;
        private System.Windows.Forms.ComboBox cmbEntityType;
        private System.Windows.Forms.Label label5;
    }
}
