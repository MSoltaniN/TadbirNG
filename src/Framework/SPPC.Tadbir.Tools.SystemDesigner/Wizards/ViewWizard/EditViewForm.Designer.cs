namespace SPPC.Tadbir.Tools.SystemDesigner.Wizards.ViewWizard
{
    partial class EditViewForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFetchUrl = new System.Windows.Forms.TextBox();
            this.chkIsHierarchy = new System.Windows.Forms.CheckBox();
            this.chkEnableCartable = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name :";
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtName.Location = new System.Drawing.Point(23, 41);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(295, 22);
            this.txtName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Fetch URL :";
            // 
            // txtFetchUrl
            // 
            this.txtFetchUrl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtFetchUrl.Location = new System.Drawing.Point(23, 106);
            this.txtFetchUrl.Name = "txtFetchUrl";
            this.txtFetchUrl.Size = new System.Drawing.Size(295, 22);
            this.txtFetchUrl.TabIndex = 3;
            // 
            // chkIsHierarchy
            // 
            this.chkIsHierarchy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkIsHierarchy.AutoSize = true;
            this.chkIsHierarchy.Location = new System.Drawing.Point(23, 151);
            this.chkIsHierarchy.Name = "chkIsHierarchy";
            this.chkIsHierarchy.Size = new System.Drawing.Size(105, 21);
            this.chkIsHierarchy.TabIndex = 4;
            this.chkIsHierarchy.Text = "Hierarchical";
            this.chkIsHierarchy.UseVisualStyleBackColor = true;
            // 
            // chkEnableCartable
            // 
            this.chkEnableCartable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkEnableCartable.AutoSize = true;
            this.chkEnableCartable.Location = new System.Drawing.Point(23, 178);
            this.chkEnableCartable.Name = "chkEnableCartable";
            this.chkEnableCartable.Size = new System.Drawing.Size(152, 21);
            this.chkEnableCartable.TabIndex = 5;
            this.chkEnableCartable.Text = "Cartable-integrated";
            this.chkEnableCartable.UseVisualStyleBackColor = true;
            // 
            // EditViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkEnableCartable);
            this.Controls.Add(this.chkIsHierarchy);
            this.Controls.Add(this.txtFetchUrl);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label1);
            this.Name = "EditViewForm";
            this.Size = new System.Drawing.Size(338, 219);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtFetchUrl;
        private System.Windows.Forms.CheckBox chkIsHierarchy;
        private System.Windows.Forms.CheckBox chkEnableCartable;
    }
}
