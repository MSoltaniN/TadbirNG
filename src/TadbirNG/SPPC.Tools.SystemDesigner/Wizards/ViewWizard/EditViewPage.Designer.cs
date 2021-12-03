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
            this.tvViewModels.Indent = 15;
            this.tvViewModels.Location = new System.Drawing.Point(16, 60);
            this.tvViewModels.Name = "tvViewModels";
            this.tvViewModels.Size = new System.Drawing.Size(387, 641);
            this.tvViewModels.TabIndex = 0;
            this.tvViewModels.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.ViewModels_AfterSelect);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "All view models :";
            // 
            // chkEnableCartable
            // 
            this.chkEnableCartable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkEnableCartable.AutoSize = true;
            this.chkEnableCartable.Location = new System.Drawing.Point(20, 611);
            this.chkEnableCartable.Name = "chkEnableCartable";
            this.chkEnableCartable.Size = new System.Drawing.Size(162, 24);
            this.chkEnableCartable.TabIndex = 13;
            this.chkEnableCartable.Text = "Cartable-integrated";
            this.chkEnableCartable.UseVisualStyleBackColor = true;
            // 
            // chkIsHierarchy
            // 
            this.chkIsHierarchy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkIsHierarchy.AutoSize = true;
            this.chkIsHierarchy.Location = new System.Drawing.Point(20, 578);
            this.chkIsHierarchy.Name = "chkIsHierarchy";
            this.chkIsHierarchy.Size = new System.Drawing.Size(110, 24);
            this.chkIsHierarchy.TabIndex = 12;
            this.chkIsHierarchy.Text = "Hierarchical";
            this.chkIsHierarchy.UseVisualStyleBackColor = true;
            // 
            // txtFetchUrl
            // 
            this.txtFetchUrl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtFetchUrl.Location = new System.Drawing.Point(21, 375);
            this.txtFetchUrl.Name = "txtFetchUrl";
            this.txtFetchUrl.Size = new System.Drawing.Size(295, 27);
            this.txtFetchUrl.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 351);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 20);
            this.label2.TabIndex = 8;
            this.label2.Text = "Fetch URL :";
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtName.Location = new System.Drawing.Point(21, 311);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(295, 27);
            this.txtName.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 288);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 20);
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
            this.gboxViewModel.Location = new System.Drawing.Point(436, 52);
            this.gboxViewModel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gboxViewModel.Name = "gboxViewModel";
            this.gboxViewModel.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gboxViewModel.Size = new System.Drawing.Size(361, 649);
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
            this.cmbEntityType.Location = new System.Drawing.Point(20, 514);
            this.cmbEntityType.Name = "cmbEntityType";
            this.cmbEntityType.Size = new System.Drawing.Size(159, 28);
            this.cmbEntityType.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 489);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 20);
            this.label5.TabIndex = 14;
            this.label5.Text = "Entity type :";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 415);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 20);
            this.label4.TabIndex = 12;
            this.label4.Text = "Search URL :";
            // 
            // txtSearchUrl
            // 
            this.txtSearchUrl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtSearchUrl.Location = new System.Drawing.Point(21, 440);
            this.txtSearchUrl.Name = "txtSearchUrl";
            this.txtSearchUrl.Size = new System.Drawing.Size(295, 27);
            this.txtSearchUrl.TabIndex = 9;
            // 
            // EditViewPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gboxViewModel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tvViewModels);
            this.Name = "EditViewPage";
            this.Size = new System.Drawing.Size(812, 722);
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
