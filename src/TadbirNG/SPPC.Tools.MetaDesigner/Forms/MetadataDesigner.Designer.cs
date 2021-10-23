namespace SPPC.Tools.MetaDesigner
{
    partial class MetadataDesigner
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
            this.prgProperties = new System.Windows.Forms.PropertyGrid();
            this.tvMetadata = new System.Windows.Forms.TreeView();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.fileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.fileNewMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.fileNewFileRepositoryMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.fileOpenMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.fileOpenFileRepositoryMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.fileSaveRepositoryMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.fileSaveRepositoryAsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.txtCurrentElement = new System.Windows.Forms.TextBox();
            this.mainSplitView = new System.Windows.Forms.SplitContainer();
            this.mainMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitView)).BeginInit();
            this.mainSplitView.Panel1.SuspendLayout();
            this.mainSplitView.Panel2.SuspendLayout();
            this.mainSplitView.SuspendLayout();
            this.SuspendLayout();
            // 
            // prgProperties
            // 
            this.prgProperties.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.prgProperties.Location = new System.Drawing.Point(3, 33);
            this.prgProperties.Name = "prgProperties";
            this.prgProperties.Size = new System.Drawing.Size(549, 639);
            this.prgProperties.TabIndex = 0;
            this.prgProperties.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.Properties_PropertyValueChanged);
            // 
            // tvMetadata
            // 
            this.tvMetadata.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvMetadata.HideSelection = false;
            this.tvMetadata.Location = new System.Drawing.Point(0, 0);
            this.tvMetadata.Name = "tvMetadata";
            this.tvMetadata.Size = new System.Drawing.Size(423, 672);
            this.tvMetadata.TabIndex = 1;
            this.tvMetadata.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.Metadata_AfterSelect);
            // 
            // mainMenu
            // 
            this.mainMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenu});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.mainMenu.Size = new System.Drawing.Size(1006, 28);
            this.mainMenu.TabIndex = 2;
            this.mainMenu.Text = "menuStrip1";
            // 
            // fileMenu
            // 
            this.fileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileNewMenu,
            this.fileOpenMenu,
            this.fileSaveRepositoryMenu,
            this.fileSaveRepositoryAsMenu});
            this.fileMenu.Name = "fileMenu";
            this.fileMenu.Size = new System.Drawing.Size(92, 24);
            this.fileMenu.Text = "&Repository";
            // 
            // fileNewMenu
            // 
            this.fileNewMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileNewFileRepositoryMenu});
            this.fileNewMenu.Name = "fileNewMenu";
            this.fileNewMenu.Size = new System.Drawing.Size(144, 26);
            this.fileNewMenu.Text = "&New";
            // 
            // fileNewFileRepositoryMenu
            // 
            this.fileNewFileRepositoryMenu.Name = "fileNewFileRepositoryMenu";
            this.fileNewFileRepositoryMenu.Size = new System.Drawing.Size(152, 26);
            this.fileNewFileRepositoryMenu.Text = "From &file...";
            this.fileNewFileRepositoryMenu.Click += new System.EventHandler(this.NewRepositoryFromFileMenu_Click);
            // 
            // fileOpenMenu
            // 
            this.fileOpenMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileOpenFileRepositoryMenu});
            this.fileOpenMenu.Name = "fileOpenMenu";
            this.fileOpenMenu.Size = new System.Drawing.Size(144, 26);
            this.fileOpenMenu.Text = "&Open";
            // 
            // fileOpenFileRepositoryMenu
            // 
            this.fileOpenFileRepositoryMenu.Name = "fileOpenFileRepositoryMenu";
            this.fileOpenFileRepositoryMenu.Size = new System.Drawing.Size(152, 26);
            this.fileOpenFileRepositoryMenu.Text = "From &file...";
            this.fileOpenFileRepositoryMenu.Click += new System.EventHandler(this.OpenRepositoryFromFileMenu_Click);
            // 
            // fileSaveRepositoryMenu
            // 
            this.fileSaveRepositoryMenu.Name = "fileSaveRepositoryMenu";
            this.fileSaveRepositoryMenu.Size = new System.Drawing.Size(144, 26);
            this.fileSaveRepositoryMenu.Text = "&Save";
            this.fileSaveRepositoryMenu.Click += new System.EventHandler(this.SaveRepositoryMenu_Click);
            // 
            // fileSaveRepositoryAsMenu
            // 
            this.fileSaveRepositoryAsMenu.Name = "fileSaveRepositoryAsMenu";
            this.fileSaveRepositoryAsMenu.Size = new System.Drawing.Size(144, 26);
            this.fileSaveRepositoryAsMenu.Text = "Save &As...";
            this.fileSaveRepositoryAsMenu.Click += new System.EventHandler(this.SaveRepositoryAsMenu_Click);
            // 
            // txtCurrentElement
            // 
            this.txtCurrentElement.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCurrentElement.Location = new System.Drawing.Point(3, 0);
            this.txtCurrentElement.Name = "txtCurrentElement";
            this.txtCurrentElement.ReadOnly = true;
            this.txtCurrentElement.Size = new System.Drawing.Size(549, 24);
            this.txtCurrentElement.TabIndex = 3;
            // 
            // mainSplitView
            // 
            this.mainSplitView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainSplitView.Location = new System.Drawing.Point(12, 44);
            this.mainSplitView.Name = "mainSplitView";
            // 
            // mainSplitView.Panel1
            // 
            this.mainSplitView.Panel1.Controls.Add(this.tvMetadata);
            // 
            // mainSplitView.Panel2
            // 
            this.mainSplitView.Panel2.Controls.Add(this.prgProperties);
            this.mainSplitView.Panel2.Controls.Add(this.txtCurrentElement);
            this.mainSplitView.Size = new System.Drawing.Size(982, 672);
            this.mainSplitView.SplitterDistance = 423;
            this.mainSplitView.TabIndex = 4;
            // 
            // MetadataDesigner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1006, 721);
            this.Controls.Add(this.mainMenu);
            this.Controls.Add(this.mainSplitView);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainMenuStrip = this.mainMenu;
            this.MinimizeBox = false;
            this.Name = "MetadataDesigner";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Metadata Designer";
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.mainSplitView.Panel1.ResumeLayout(false);
            this.mainSplitView.Panel2.ResumeLayout(false);
            this.mainSplitView.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitView)).EndInit();
            this.mainSplitView.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PropertyGrid prgProperties;
        private System.Windows.Forms.TreeView tvMetadata;
        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.TextBox txtCurrentElement;
        private System.Windows.Forms.ToolStripMenuItem fileMenu;
        private System.Windows.Forms.ToolStripMenuItem fileNewMenu;
        private System.Windows.Forms.ToolStripMenuItem fileNewFileRepositoryMenu;
        private System.Windows.Forms.ToolStripMenuItem fileSaveRepositoryMenu;
        private System.Windows.Forms.ToolStripMenuItem fileOpenMenu;
        private System.Windows.Forms.ToolStripMenuItem fileOpenFileRepositoryMenu;
        private System.Windows.Forms.ToolStripMenuItem fileSaveRepositoryAsMenu;
        private System.Windows.Forms.SplitContainer mainSplitView;
    }
}

