namespace SPPC.Tadbir.Tools.SystemDesigner.Wizards.ViewWizard
{
    partial class ViewWizardForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.labelModelViewSelector = new System.Windows.Forms.Label();
            this.cboxModelViewSelector = new System.Windows.Forms.ComboBox();
            this.GVMetaDataViewer = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GVMetaDataViewer)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.labelModelViewSelector);
            this.splitContainer1.Panel1.Controls.Add(this.cboxModelViewSelector);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.GVMetaDataViewer);
            this.splitContainer1.Size = new System.Drawing.Size(710, 548);
            this.splitContainer1.SplitterDistance = 68;
            this.splitContainer1.TabIndex = 0;
            // 
            // labelModelViewSelector
            // 
            this.labelModelViewSelector.AutoSize = true;
            this.labelModelViewSelector.Location = new System.Drawing.Point(24, 30);
            this.labelModelViewSelector.Name = "labelModelViewSelector";
            this.labelModelViewSelector.Size = new System.Drawing.Size(62, 13);
            this.labelModelViewSelector.TabIndex = 3;
            this.labelModelViewSelector.Text = "View Model";
            // 
            // cboxModelViewSelector
            // 
            this.cboxModelViewSelector.FormattingEnabled = true;
            this.cboxModelViewSelector.Location = new System.Drawing.Point(92, 27);
            this.cboxModelViewSelector.Name = "cboxModelViewSelector";
            this.cboxModelViewSelector.Size = new System.Drawing.Size(225, 21);
            this.cboxModelViewSelector.TabIndex = 2;
            this.cboxModelViewSelector.SelectedIndexChanged += new System.EventHandler(this.cboxModelViewSelector_SelectedIndexChanged);
            // 
            // GVMetaDataViewer
            // 
            this.GVMetaDataViewer.AllowUserToAddRows = false;
            this.GVMetaDataViewer.AllowUserToDeleteRows = false;
            this.GVMetaDataViewer.AllowUserToResizeRows = false;
            this.GVMetaDataViewer.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.GVMetaDataViewer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.GVMetaDataViewer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GVMetaDataViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GVMetaDataViewer.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.GVMetaDataViewer.Location = new System.Drawing.Point(0, 0);
            this.GVMetaDataViewer.Name = "GVMetaDataViewer";
            this.GVMetaDataViewer.ReadOnly = true;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            this.GVMetaDataViewer.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.GVMetaDataViewer.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GVMetaDataViewer.Size = new System.Drawing.Size(710, 476);
            this.GVMetaDataViewer.TabIndex = 1;
            // 
            // ViewWizardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(710, 548);
            this.Controls.Add(this.splitContainer1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ViewWizardForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ViewWizardForm";
            this.Load += new System.EventHandler(this.ViewWizardForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GVMetaDataViewer)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label labelModelViewSelector;
        private System.Windows.Forms.ComboBox cboxModelViewSelector;
        private System.Windows.Forms.DataGridView GVMetaDataViewer;
    }
}