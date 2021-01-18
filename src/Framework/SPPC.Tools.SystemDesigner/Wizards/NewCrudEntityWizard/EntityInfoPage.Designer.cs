namespace SPPC.Tools.SystemDesigner.Wizards.NewCrudEntityWizard
{
    partial class EntityInfoPage
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
            this.grpEntity = new System.Windows.Forms.GroupBox();
            this.chkIsFiscal = new System.Windows.Forms.CheckBox();
            this.cmbEntity = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPluralName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSingularName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.grpEntity.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpEntity
            // 
            this.grpEntity.Controls.Add(this.chkIsFiscal);
            this.grpEntity.Controls.Add(this.cmbEntity);
            this.grpEntity.Controls.Add(this.label5);
            this.grpEntity.Controls.Add(this.txtPluralName);
            this.grpEntity.Controls.Add(this.label6);
            this.grpEntity.Controls.Add(this.label4);
            this.grpEntity.Controls.Add(this.txtSingularName);
            this.grpEntity.Controls.Add(this.label2);
            this.grpEntity.Controls.Add(this.label1);
            this.grpEntity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpEntity.Location = new System.Drawing.Point(0, 0);
            this.grpEntity.Name = "grpEntity";
            this.grpEntity.Size = new System.Drawing.Size(650, 420);
            this.grpEntity.TabIndex = 1;
            this.grpEntity.TabStop = false;
            this.grpEntity.Text = "Entity Information";
            // 
            // chkIsFiscal
            // 
            this.chkIsFiscal.AutoSize = true;
            this.chkIsFiscal.Location = new System.Drawing.Point(16, 142);
            this.chkIsFiscal.Name = "chkIsFiscal";
            this.chkIsFiscal.Size = new System.Drawing.Size(263, 21);
            this.chkIsFiscal.TabIndex = 8;
            this.chkIsFiscal.Text = "Depends on fiscal period and branch";
            this.chkIsFiscal.UseVisualStyleBackColor = true;
            // 
            // cmbEntity
            // 
            this.cmbEntity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEntity.FormattingEnabled = true;
            this.cmbEntity.Location = new System.Drawing.Point(183, 38);
            this.cmbEntity.Name = "cmbEntity";
            this.cmbEntity.Size = new System.Drawing.Size(214, 24);
            this.cmbEntity.TabIndex = 1;
            this.cmbEntity.SelectedIndexChanged += new System.EventHandler(this.Entity_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(413, 109);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 17);
            this.label5.TabIndex = 7;
            this.label5.Text = "(plural)";
            // 
            // txtPluralName
            // 
            this.txtPluralName.Location = new System.Drawing.Point(183, 106);
            this.txtPluralName.Name = "txtPluralName";
            this.txtPluralName.Size = new System.Drawing.Size(214, 22);
            this.txtPluralName.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 109);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(152, 17);
            this.label6.TabIndex = 5;
            this.label6.Text = "Entity name (Persian) :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(413, 75);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 17);
            this.label4.TabIndex = 4;
            this.label4.Text = "(singular)";
            // 
            // txtSingularName
            // 
            this.txtSingularName.Location = new System.Drawing.Point(183, 72);
            this.txtSingularName.Name = "txtSingularName";
            this.txtSingularName.Size = new System.Drawing.Size(214, 22);
            this.txtSingularName.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(152, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Entity name (Persian) :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Entity name :";
            // 
            // EntityInfoPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpEntity);
            this.Name = "EntityInfoPage";
            this.Size = new System.Drawing.Size(650, 420);
            this.grpEntity.ResumeLayout(false);
            this.grpEntity.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpEntity;
        private System.Windows.Forms.CheckBox chkIsFiscal;
        private System.Windows.Forms.ComboBox cmbEntity;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtPluralName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSingularName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}
