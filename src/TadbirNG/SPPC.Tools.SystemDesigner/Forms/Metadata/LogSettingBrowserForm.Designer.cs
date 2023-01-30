namespace SPPC.Tools.SystemDesigner.Forms
{
    partial class LogSettingBrowserForm
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
            this.components = new System.ComponentModel.Container();
            this.chkSystemSettings = new System.Windows.Forms.CheckBox();
            this.tvLogMetadata = new System.Windows.Forms.TreeView();
            this.btnNew = new System.Windows.Forms.Button();
            this.menuNewOptions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuNewSubsystem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuNewEntityType = new System.Windows.Forms.ToolStripMenuItem();
            this.menuNewOperationSource = new System.Windows.Forms.ToolStripMenuItem();
            this.menuNewOperation = new System.Windows.Forms.ToolStripMenuItem();
            this.menuNewLogSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.grpProperties = new System.Windows.Forms.GroupBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.menuNewOptions.SuspendLayout();
            this.grpProperties.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkSystemSettings
            // 
            this.chkSystemSettings.AutoSize = true;
            this.chkSystemSettings.Location = new System.Drawing.Point(21, 26);
            this.chkSystemSettings.Name = "chkSystemSettings";
            this.chkSystemSettings.Size = new System.Drawing.Size(164, 24);
            this.chkSystemSettings.TabIndex = 0;
            this.chkSystemSettings.Text = "System Log Settings";
            this.chkSystemSettings.UseVisualStyleBackColor = true;
            this.chkSystemSettings.CheckedChanged += new System.EventHandler(this.SystemSettings_CheckedChanged);
            // 
            // tvLogMetadata
            // 
            this.tvLogMetadata.CheckBoxes = true;
            this.tvLogMetadata.HideSelection = false;
            this.tvLogMetadata.Location = new System.Drawing.Point(21, 56);
            this.tvLogMetadata.Name = "tvLogMetadata";
            this.tvLogMetadata.Size = new System.Drawing.Size(452, 529);
            this.tvLogMetadata.TabIndex = 1;
            this.tvLogMetadata.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.LogMetadata_BeforeSelect);
            this.tvLogMetadata.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.LogMetadata_AfterSelect);
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(21, 604);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(218, 36);
            this.btnNew.TabIndex = 2;
            this.btnNew.Text = "New";
            this.btnNew.UseVisualStyleBackColor = true;
            // 
            // menuNewOptions
            // 
            this.menuNewOptions.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuNewOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuNewSubsystem,
            this.menuNewEntityType,
            this.menuNewOperationSource,
            this.menuNewOperation,
            this.menuNewLogSettings});
            this.menuNewOptions.Name = "menuNewOptions";
            this.menuNewOptions.Size = new System.Drawing.Size(195, 124);
            // 
            // menuNewSubsystem
            // 
            this.menuNewSubsystem.Name = "menuNewSubsystem";
            this.menuNewSubsystem.Size = new System.Drawing.Size(194, 24);
            this.menuNewSubsystem.Text = "Subsystem";
            // 
            // menuNewEntityType
            // 
            this.menuNewEntityType.Name = "menuNewEntityType";
            this.menuNewEntityType.Size = new System.Drawing.Size(194, 24);
            this.menuNewEntityType.Text = "Entity Type";
            // 
            // menuNewOperationSource
            // 
            this.menuNewOperationSource.Name = "menuNewOperationSource";
            this.menuNewOperationSource.Size = new System.Drawing.Size(194, 24);
            this.menuNewOperationSource.Text = "Operation Source";
            // 
            // menuNewOperation
            // 
            this.menuNewOperation.Name = "menuNewOperation";
            this.menuNewOperation.Size = new System.Drawing.Size(194, 24);
            this.menuNewOperation.Text = "Operation";
            // 
            // menuNewLogSettings
            // 
            this.menuNewLogSettings.Name = "menuNewLogSettings";
            this.menuNewLogSettings.Size = new System.Drawing.Size(194, 24);
            this.menuNewLogSettings.Text = "Log Settings";
            // 
            // grpProperties
            // 
            this.grpProperties.Controls.Add(this.txtDescription);
            this.grpProperties.Controls.Add(this.label2);
            this.grpProperties.Controls.Add(this.txtName);
            this.grpProperties.Controls.Add(this.label1);
            this.grpProperties.Location = new System.Drawing.Point(489, 47);
            this.grpProperties.Name = "grpProperties";
            this.grpProperties.Size = new System.Drawing.Size(386, 538);
            this.grpProperties.TabIndex = 4;
            this.grpProperties.TabStop = false;
            this.grpProperties.Text = "Properties";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(16, 216);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(364, 304);
            this.txtDescription.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 187);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Description :";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(16, 140);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(364, 27);
            this.txtName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 111);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name :";
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(327, 605);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(146, 36);
            this.btnGenerate.TabIndex = 5;
            this.btnGenerate.Text = "Generate Scripts";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.Generate_Click);
            // 
            // LogSettingBrowserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(887, 653);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.grpProperties);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.tvLogMetadata);
            this.Controls.Add(this.chkSystemSettings);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LogSettingBrowserForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Log Catalog";
            this.menuNewOptions.ResumeLayout(false);
            this.grpProperties.ResumeLayout(false);
            this.grpProperties.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkSystemSettings;
        private System.Windows.Forms.TreeView tvLogMetadata;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.ContextMenuStrip menuNewOptions;
        private System.Windows.Forms.ToolStripMenuItem menuNewSubsystem;
        private System.Windows.Forms.ToolStripMenuItem menuNewEntityType;
        private System.Windows.Forms.ToolStripMenuItem menuNewOperationSource;
        private System.Windows.Forms.ToolStripMenuItem menuNewOperation;
        private System.Windows.Forms.ToolStripMenuItem menuNewLogSettings;
        private System.Windows.Forms.GroupBox grpProperties;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGenerate;
    }
}