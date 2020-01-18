namespace SPPC.Tadbir.Tools.SystemDesigner.Forms
{
    partial class LogCatalogueForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.lbxSourceEntity = new System.Windows.Forms.ListBox();
            this.grpProperties = new System.Windows.Forms.GroupBox();
            this.lbxOperations = new System.Windows.Forms.CheckedListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbSourceType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbSubsystem = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.btnUpdateDb = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.grpProperties.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Sources/Entities :";
            // 
            // lbxSourceEntity
            // 
            this.lbxSourceEntity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lbxSourceEntity.FormattingEnabled = true;
            this.lbxSourceEntity.Location = new System.Drawing.Point(12, 37);
            this.lbxSourceEntity.Name = "lbxSourceEntity";
            this.lbxSourceEntity.Size = new System.Drawing.Size(159, 264);
            this.lbxSourceEntity.TabIndex = 1;
            this.lbxSourceEntity.SelectedIndexChanged += new System.EventHandler(this.SourceEntity_SelectedIndexChanged);
            // 
            // grpProperties
            // 
            this.grpProperties.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpProperties.Controls.Add(this.lbxOperations);
            this.grpProperties.Controls.Add(this.label4);
            this.grpProperties.Controls.Add(this.cmbSourceType);
            this.grpProperties.Controls.Add(this.label3);
            this.grpProperties.Controls.Add(this.cmbSubsystem);
            this.grpProperties.Controls.Add(this.label2);
            this.grpProperties.Location = new System.Drawing.Point(183, 31);
            this.grpProperties.Name = "grpProperties";
            this.grpProperties.Size = new System.Drawing.Size(316, 270);
            this.grpProperties.TabIndex = 2;
            this.grpProperties.TabStop = false;
            this.grpProperties.Text = "Properties";
            // 
            // lbxOperations
            // 
            this.lbxOperations.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbxOperations.CheckOnClick = true;
            this.lbxOperations.FormattingEnabled = true;
            this.lbxOperations.Location = new System.Drawing.Point(146, 48);
            this.lbxOperations.Name = "lbxOperations";
            this.lbxOperations.Size = new System.Drawing.Size(159, 199);
            this.lbxOperations.TabIndex = 5;
            this.lbxOperations.SelectedIndexChanged += new System.EventHandler(this.Operations_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(143, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Enabled operations :";
            // 
            // cmbSourceType
            // 
            this.cmbSourceType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSourceType.FormattingEnabled = true;
            this.cmbSourceType.Location = new System.Drawing.Point(6, 99);
            this.cmbSourceType.Name = "cmbSourceType";
            this.cmbSourceType.Size = new System.Drawing.Size(121, 21);
            this.cmbSourceType.TabIndex = 3;
            this.cmbSourceType.SelectedIndexChanged += new System.EventHandler(this.SourceType_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Source type :";
            // 
            // cmbSubsystem
            // 
            this.cmbSubsystem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSubsystem.FormattingEnabled = true;
            this.cmbSubsystem.Location = new System.Drawing.Point(6, 48);
            this.cmbSubsystem.Name = "cmbSubsystem";
            this.cmbSubsystem.Size = new System.Drawing.Size(121, 21);
            this.cmbSubsystem.TabIndex = 1;
            this.cmbSubsystem.SelectedIndexChanged += new System.EventHandler(this.Subsystem_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Subsystem :";
            // 
            // btnGenerate
            // 
            this.btnGenerate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnGenerate.Location = new System.Drawing.Point(12, 320);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(97, 27);
            this.btnGenerate.TabIndex = 3;
            this.btnGenerate.Text = "Generate Script";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.Generate_Click);
            // 
            // btnUpdateDb
            // 
            this.btnUpdateDb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnUpdateDb.Location = new System.Drawing.Point(115, 320);
            this.btnUpdateDb.Name = "btnUpdateDb";
            this.btnUpdateDb.Size = new System.Drawing.Size(106, 27);
            this.btnUpdateDb.TabIndex = 4;
            this.btnUpdateDb.Text = "Update Database";
            this.btnUpdateDb.UseVisualStyleBackColor = true;
            this.btnUpdateDb.Click += new System.EventHandler(this.UpdateDb_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(424, 320);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 27);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // LogCatalogueForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(508, 354);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnUpdateDb);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.grpProperties);
            this.Controls.Add(this.lbxSourceEntity);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LogCatalogueForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Log Catalog";
            this.grpProperties.ResumeLayout(false);
            this.grpProperties.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lbxSourceEntity;
        private System.Windows.Forms.GroupBox grpProperties;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Button btnUpdateDb;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckedListBox lbxOperations;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbSourceType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbSubsystem;
        private System.Windows.Forms.Label label2;
    }
}