namespace SPPC.Tools.MetaDesigner.Forms
{
    partial class NewEntity
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
            this.lblEntityName = new System.Windows.Forms.Label();
            this.txtEntityName = new System.Windows.Forms.TextBox();
            this.chkGenerateId = new System.Windows.Forms.CheckBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.chkDerive = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // lblEntityName
            // 
            this.lblEntityName.AutoSize = true;
            this.lblEntityName.Location = new System.Drawing.Point(12, 9);
            this.lblEntityName.Name = "lblEntityName";
            this.lblEntityName.Size = new System.Drawing.Size(90, 17);
            this.lblEntityName.TabIndex = 0;
            this.lblEntityName.Text = "Entity name :";
            // 
            // txtEntityName
            // 
            this.txtEntityName.Location = new System.Drawing.Point(12, 29);
            this.txtEntityName.Name = "txtEntityName";
            this.txtEntityName.Size = new System.Drawing.Size(429, 22);
            this.txtEntityName.TabIndex = 1;
            // 
            // chkGenerateId
            // 
            this.chkGenerateId.AutoSize = true;
            this.chkGenerateId.Enabled = false;
            this.chkGenerateId.Location = new System.Drawing.Point(12, 61);
            this.chkGenerateId.Name = "chkGenerateId";
            this.chkGenerateId.Size = new System.Drawing.Size(195, 21);
            this.chkGenerateId.TabIndex = 2;
            this.chkGenerateId.Text = "Auto-generate ID property";
            this.chkGenerateId.UseVisualStyleBackColor = true;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(12, 141);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 27);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.Add_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(93, 141);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 27);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // chkDerive
            // 
            this.chkDerive.AutoSize = true;
            this.chkDerive.Checked = true;
            this.chkDerive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDerive.Location = new System.Drawing.Point(12, 88);
            this.chkDerive.Name = "chkDerive";
            this.chkDerive.Size = new System.Drawing.Size(145, 21);
            this.chkDerive.TabIndex = 5;
            this.chkDerive.Text = "Derive from IEntity";
            this.chkDerive.UseVisualStyleBackColor = true;
            // 
            // NewEntity
            // 
            this.AcceptButton = this.btnAdd;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(456, 176);
            this.Controls.Add(this.chkDerive);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.chkGenerateId);
            this.Controls.Add(this.txtEntityName);
            this.Controls.Add(this.lblEntityName);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewEntity";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add new entity";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblEntityName;
        private System.Windows.Forms.TextBox txtEntityName;
        private System.Windows.Forms.CheckBox chkGenerateId;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox chkDerive;
    }
}