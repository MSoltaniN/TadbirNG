namespace SPPC.Tadbir.Tools.SystemDesigner.Wizards.ViewWizard
{
    partial class BrowseViewsPage
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.labelModelViewSelector = new System.Windows.Forms.Label();
            this.cmbViewSelector = new System.Windows.Forms.ComboBox();
            this.gvColumns = new System.Windows.Forms.DataGridView();
            this.EditCell = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gvColumns)).BeginInit();
            this.SuspendLayout();
            // 
            // labelModelViewSelector
            // 
            this.labelModelViewSelector.AutoSize = true;
            this.labelModelViewSelector.Location = new System.Drawing.Point(14, 29);
            this.labelModelViewSelector.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelModelViewSelector.Name = "labelModelViewSelector";
            this.labelModelViewSelector.Size = new System.Drawing.Size(37, 17);
            this.labelModelViewSelector.TabIndex = 3;
            this.labelModelViewSelector.Text = "View";
            // 
            // cmbViewSelector
            // 
            this.cmbViewSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbViewSelector.FormattingEnabled = true;
            this.cmbViewSelector.Location = new System.Drawing.Point(71, 26);
            this.cmbViewSelector.Margin = new System.Windows.Forms.Padding(4);
            this.cmbViewSelector.Name = "cmbViewSelector";
            this.cmbViewSelector.Size = new System.Drawing.Size(299, 24);
            this.cmbViewSelector.TabIndex = 2;
            this.cmbViewSelector.SelectedIndexChanged += new System.EventHandler(this.ViewSelector_SelectedIndexChanged);
            // 
            // gvColumns
            // 
            this.gvColumns.AllowUserToAddRows = false;
            this.gvColumns.AllowUserToDeleteRows = false;
            this.gvColumns.AllowUserToResizeRows = false;
            this.gvColumns.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gvColumns.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.gvColumns.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.gvColumns.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvColumns.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.EditCell,
            this.Column1});
            this.gvColumns.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.gvColumns.Location = new System.Drawing.Point(17, 68);
            this.gvColumns.Margin = new System.Windows.Forms.Padding(4);
            this.gvColumns.Name = "gvColumns";
            this.gvColumns.ReadOnly = true;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            this.gvColumns.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.gvColumns.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvColumns.Size = new System.Drawing.Size(749, 475);
            this.gvColumns.TabIndex = 2;
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
            // BrowseViewsPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelModelViewSelector);
            this.Controls.Add(this.gvColumns);
            this.Controls.Add(this.cmbViewSelector);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "BrowseViewsPage";
            this.Size = new System.Drawing.Size(781, 558);
            ((System.ComponentModel.ISupportInitialize)(this.gvColumns)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelModelViewSelector;
        private System.Windows.Forms.ComboBox cmbViewSelector;
        private System.Windows.Forms.DataGridView gvColumns;
        private System.Windows.Forms.DataGridViewButtonColumn EditCell;
        private System.Windows.Forms.DataGridViewButtonColumn Column1;
    }
}
