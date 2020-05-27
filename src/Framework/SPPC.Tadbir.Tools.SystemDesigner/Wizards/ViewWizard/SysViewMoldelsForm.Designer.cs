namespace SPPC.Tadbir.Tools.SystemDesigner.Wizards.ViewWizard
{
    partial class SysViewMoldelsForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.labelModelViewSelector = new System.Windows.Forms.Label();
            this.cboxModelViewSelector = new System.Windows.Forms.ComboBox();
            this.GVMetaDataViewer = new System.Windows.Forms.DataGridView();
            this.EditCell = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GVMetaDataViewer)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.labelModelViewSelector);
            this.splitContainer.Panel1.Controls.Add(this.cboxModelViewSelector);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.GVMetaDataViewer);
            this.splitContainer.Size = new System.Drawing.Size(666, 512);
            this.splitContainer.SplitterDistance = 62;
            this.splitContainer.TabIndex = 1;
            // 
            // labelModelViewSelector
            // 
            this.labelModelViewSelector.AutoSize = true;
            this.labelModelViewSelector.Location = new System.Drawing.Point(12, 25);
            this.labelModelViewSelector.Name = "labelModelViewSelector";
            this.labelModelViewSelector.Size = new System.Drawing.Size(62, 13);
            this.labelModelViewSelector.TabIndex = 3;
            this.labelModelViewSelector.Text = "View Model";
            // 
            // cboxModelViewSelector
            // 
            this.cboxModelViewSelector.FormattingEnabled = true;
            this.cboxModelViewSelector.Location = new System.Drawing.Point(79, 22);
            this.cboxModelViewSelector.Name = "cboxModelViewSelector";
            this.cboxModelViewSelector.Size = new System.Drawing.Size(225, 21);
            this.cboxModelViewSelector.TabIndex = 2;
            this.cboxModelViewSelector.SelectedIndexChanged += new System.EventHandler(this.cboxModelViewSelector_SelectedIndexChanged_1);
            // 
            // GVMetaDataViewer
            // 
            this.GVMetaDataViewer.AllowUserToAddRows = false;
            this.GVMetaDataViewer.AllowUserToDeleteRows = false;
            this.GVMetaDataViewer.AllowUserToResizeRows = false;
            this.GVMetaDataViewer.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.GVMetaDataViewer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.GVMetaDataViewer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GVMetaDataViewer.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.EditCell,
            this.Column1});
            this.GVMetaDataViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GVMetaDataViewer.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.GVMetaDataViewer.Location = new System.Drawing.Point(0, 0);
            this.GVMetaDataViewer.Name = "GVMetaDataViewer";
            this.GVMetaDataViewer.ReadOnly = true;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            this.GVMetaDataViewer.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.GVMetaDataViewer.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GVMetaDataViewer.Size = new System.Drawing.Size(666, 446);
            this.GVMetaDataViewer.TabIndex = 1;
            // 
            // EditCell
            // 
            this.EditCell.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.EditCell.HeaderText = "";
            this.EditCell.Name = "EditCell";
            this.EditCell.ReadOnly = true;
            this.EditCell.Text = "Edit";
            this.EditCell.UseColumnTextForButtonValue = true;
            this.EditCell.Width = 40;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Text = "Apply";
            this.Column1.UseColumnTextForButtonValue = true;
            this.Column1.Width = 40;
            // 
            // SysViewMoldelsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer);
            this.Name = "SysViewMoldelsForm";
            this.Size = new System.Drawing.Size(666, 512);
            this.Leave += new System.EventHandler(this.SysViewMoldelsForm_Leave);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel1.PerformLayout();
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GVMetaDataViewer)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Label labelModelViewSelector;
        private System.Windows.Forms.ComboBox cboxModelViewSelector;
        private System.Windows.Forms.DataGridView GVMetaDataViewer;
        private System.Windows.Forms.DataGridViewButtonColumn EditCell;
        private System.Windows.Forms.DataGridViewButtonColumn Column1;
    }
}
