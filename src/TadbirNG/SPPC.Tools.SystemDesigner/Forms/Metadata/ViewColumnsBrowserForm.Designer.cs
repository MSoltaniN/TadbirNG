
namespace SPPC.Tools.SystemDesigner.Forms
{
    partial class ViewColumnsBrowserForm
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
            this.tabViews = new System.Windows.Forms.TabPage();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.btnDeleteView = new System.Windows.Forms.Button();
            this.btnEditView = new System.Windows.Forms.Button();
            this.btnAddView = new System.Windows.Forms.Button();
            this.grdViews = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.tabColumns = new System.Windows.Forms.TabPage();
            this.btnDeleteColumn = new System.Windows.Forms.Button();
            this.btnEditColumn = new System.Windows.Forms.Button();
            this.btnAddColumn = new System.Windows.Forms.Button();
            this.grdColumns = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabViews.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdViews)).BeginInit();
            this.tabColumns.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdColumns)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabViews);
            this.tabControl1.Controls.Add(this.tabColumns);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(934, 639);
            this.tabControl1.TabIndex = 0;
            // 
            // tabViews
            // 
            this.tabViews.Controls.Add(this.btnExit);
            this.tabViews.Controls.Add(this.btnGenerate);
            this.tabViews.Controls.Add(this.btnDeleteView);
            this.tabViews.Controls.Add(this.btnEditView);
            this.tabViews.Controls.Add(this.btnAddView);
            this.tabViews.Controls.Add(this.grdViews);
            this.tabViews.Controls.Add(this.label1);
            this.tabViews.Location = new System.Drawing.Point(4, 29);
            this.tabViews.Name = "tabViews";
            this.tabViews.Padding = new System.Windows.Forms.Padding(3);
            this.tabViews.Size = new System.Drawing.Size(926, 606);
            this.tabViews.TabIndex = 0;
            this.tabViews.Text = "Views";
            this.tabViews.UseVisualStyleBackColor = true;
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Location = new System.Drawing.Point(824, 563);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(94, 34);
            this.btnExit.TabIndex = 6;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.Exit_Click);
            // 
            // btnGenerate
            // 
            this.btnGenerate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnGenerate.Location = new System.Drawing.Point(311, 563);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(135, 34);
            this.btnGenerate.TabIndex = 5;
            this.btnGenerate.Text = "Generate Scripts";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.Generate_Click);
            // 
            // btnDeleteView
            // 
            this.btnDeleteView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDeleteView.Location = new System.Drawing.Point(211, 563);
            this.btnDeleteView.Name = "btnDeleteView";
            this.btnDeleteView.Size = new System.Drawing.Size(94, 34);
            this.btnDeleteView.TabIndex = 4;
            this.btnDeleteView.Text = "Delete";
            this.btnDeleteView.UseVisualStyleBackColor = true;
            this.btnDeleteView.Click += new System.EventHandler(this.DeleteView_Click);
            // 
            // btnEditView
            // 
            this.btnEditView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEditView.Location = new System.Drawing.Point(111, 563);
            this.btnEditView.Name = "btnEditView";
            this.btnEditView.Size = new System.Drawing.Size(94, 34);
            this.btnEditView.TabIndex = 3;
            this.btnEditView.Text = "Edit";
            this.btnEditView.UseVisualStyleBackColor = true;
            this.btnEditView.Click += new System.EventHandler(this.EditView_Click);
            // 
            // btnAddView
            // 
            this.btnAddView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddView.Location = new System.Drawing.Point(11, 563);
            this.btnAddView.Name = "btnAddView";
            this.btnAddView.Size = new System.Drawing.Size(94, 34);
            this.btnAddView.TabIndex = 2;
            this.btnAddView.Text = "Add";
            this.btnAddView.UseVisualStyleBackColor = true;
            this.btnAddView.Click += new System.EventHandler(this.AddView_Click);
            // 
            // grdViews
            // 
            this.grdViews.AllowUserToAddRows = false;
            this.grdViews.AllowUserToDeleteRows = false;
            this.grdViews.AllowUserToResizeRows = false;
            this.grdViews.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdViews.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grdViews.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdViews.Location = new System.Drawing.Point(11, 44);
            this.grdViews.MultiSelect = false;
            this.grdViews.Name = "grdViews";
            this.grdViews.ReadOnly = true;
            this.grdViews.RowHeadersVisible = false;
            this.grdViews.RowHeadersWidth = 51;
            this.grdViews.RowTemplate.Height = 29;
            this.grdViews.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdViews.Size = new System.Drawing.Size(907, 506);
            this.grdViews.TabIndex = 1;
            this.grdViews.SelectionChanged += new System.EventHandler(this.Views_SelectionChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "All Views :";
            // 
            // tabColumns
            // 
            this.tabColumns.Controls.Add(this.btnDeleteColumn);
            this.tabColumns.Controls.Add(this.btnEditColumn);
            this.tabColumns.Controls.Add(this.btnAddColumn);
            this.tabColumns.Controls.Add(this.grdColumns);
            this.tabColumns.Controls.Add(this.label2);
            this.tabColumns.Location = new System.Drawing.Point(4, 29);
            this.tabColumns.Name = "tabColumns";
            this.tabColumns.Padding = new System.Windows.Forms.Padding(3);
            this.tabColumns.Size = new System.Drawing.Size(926, 606);
            this.tabColumns.TabIndex = 1;
            this.tabColumns.Text = "Columns";
            this.tabColumns.UseVisualStyleBackColor = true;
            // 
            // btnDeleteColumn
            // 
            this.btnDeleteColumn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDeleteColumn.Location = new System.Drawing.Point(210, 563);
            this.btnDeleteColumn.Name = "btnDeleteColumn";
            this.btnDeleteColumn.Size = new System.Drawing.Size(94, 34);
            this.btnDeleteColumn.TabIndex = 9;
            this.btnDeleteColumn.Text = "Delete";
            this.btnDeleteColumn.UseVisualStyleBackColor = true;
            this.btnDeleteColumn.Click += new System.EventHandler(this.DeleteColumn_Click);
            // 
            // btnEditColumn
            // 
            this.btnEditColumn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEditColumn.Location = new System.Drawing.Point(110, 563);
            this.btnEditColumn.Name = "btnEditColumn";
            this.btnEditColumn.Size = new System.Drawing.Size(94, 34);
            this.btnEditColumn.TabIndex = 8;
            this.btnEditColumn.Text = "Edit";
            this.btnEditColumn.UseVisualStyleBackColor = true;
            this.btnEditColumn.Click += new System.EventHandler(this.EditColumn_Click);
            // 
            // btnAddColumn
            // 
            this.btnAddColumn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddColumn.Location = new System.Drawing.Point(10, 563);
            this.btnAddColumn.Name = "btnAddColumn";
            this.btnAddColumn.Size = new System.Drawing.Size(94, 34);
            this.btnAddColumn.TabIndex = 7;
            this.btnAddColumn.Text = "Add";
            this.btnAddColumn.UseVisualStyleBackColor = true;
            this.btnAddColumn.Click += new System.EventHandler(this.AddColumn_Click);
            // 
            // grdColumns
            // 
            this.grdColumns.AllowUserToAddRows = false;
            this.grdColumns.AllowUserToDeleteRows = false;
            this.grdColumns.AllowUserToResizeRows = false;
            this.grdColumns.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdColumns.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grdColumns.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdColumns.Location = new System.Drawing.Point(10, 43);
            this.grdColumns.MultiSelect = false;
            this.grdColumns.Name = "grdColumns";
            this.grdColumns.ReadOnly = true;
            this.grdColumns.RowHeadersVisible = false;
            this.grdColumns.RowHeadersWidth = 51;
            this.grdColumns.RowTemplate.Height = 29;
            this.grdColumns.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdColumns.Size = new System.Drawing.Size(907, 506);
            this.grdColumns.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(170, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Selected View Columns :";
            // 
            // ViewColumnsBrowserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(934, 639);
            this.Controls.Add(this.tabControl1);
            this.Name = "ViewColumnsBrowserForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Manage List Views";
            this.tabControl1.ResumeLayout(false);
            this.tabViews.ResumeLayout(false);
            this.tabViews.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdViews)).EndInit();
            this.tabColumns.ResumeLayout(false);
            this.tabColumns.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdColumns)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabViews;
        private System.Windows.Forms.TabPage tabColumns;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Button btnDeleteView;
        private System.Windows.Forms.Button btnEditView;
        private System.Windows.Forms.Button btnAddView;
        private System.Windows.Forms.DataGridView grdViews;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnDeleteColumn;
        private System.Windows.Forms.Button btnEditColumn;
        private System.Windows.Forms.Button btnAddColumn;
        private System.Windows.Forms.DataGridView grdColumns;
        private System.Windows.Forms.Label label2;
    }
}