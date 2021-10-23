namespace SPPC.Tools.MetaDesigner.Forms
{
    partial class NewRelation
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
            this.cmbEntities = new System.Windows.Forms.ComboBox();
            this.cmbRelationType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnAddRelation = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblJoinTable = new System.Windows.Forms.Label();
            this.txtJoinTable = new System.Windows.Forms.TextBox();
            this.chkHasKey = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Related entity :";
            // 
            // cmbEntities
            // 
            this.cmbEntities.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEntities.FormattingEnabled = true;
            this.cmbEntities.Location = new System.Drawing.Point(12, 40);
            this.cmbEntities.Name = "cmbEntities";
            this.cmbEntities.Size = new System.Drawing.Size(218, 24);
            this.cmbEntities.TabIndex = 1;
            // 
            // cmbRelationType
            // 
            this.cmbRelationType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRelationType.FormattingEnabled = true;
            this.cmbRelationType.Items.AddRange(new object[] {
            "One to Many",
            "Many to One",
            "One to One",
            "Many to Many"});
            this.cmbRelationType.Location = new System.Drawing.Point(247, 40);
            this.cmbRelationType.Name = "cmbRelationType";
            this.cmbRelationType.Size = new System.Drawing.Size(218, 24);
            this.cmbRelationType.TabIndex = 3;
            this.cmbRelationType.SelectedIndexChanged += new System.EventHandler(this.RelationType_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(247, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Relation type :";
            // 
            // btnAddRelation
            // 
            this.btnAddRelation.Location = new System.Drawing.Point(12, 163);
            this.btnAddRelation.Name = "btnAddRelation";
            this.btnAddRelation.Size = new System.Drawing.Size(118, 34);
            this.btnAddRelation.TabIndex = 4;
            this.btnAddRelation.Text = "Add Relation";
            this.btnAddRelation.UseVisualStyleBackColor = true;
            this.btnAddRelation.Click += new System.EventHandler(this.AddRelation_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(136, 163);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(118, 34);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // lblJoinTable
            // 
            this.lblJoinTable.AutoSize = true;
            this.lblJoinTable.Location = new System.Drawing.Point(12, 83);
            this.lblJoinTable.Name = "lblJoinTable";
            this.lblJoinTable.Size = new System.Drawing.Size(116, 17);
            this.lblJoinTable.TabIndex = 6;
            this.lblJoinTable.Text = "Join table name :";
            // 
            // txtJoinTable
            // 
            this.txtJoinTable.Location = new System.Drawing.Point(12, 103);
            this.txtJoinTable.Name = "txtJoinTable";
            this.txtJoinTable.Size = new System.Drawing.Size(218, 22);
            this.txtJoinTable.TabIndex = 7;
            // 
            // chkHasKey
            // 
            this.chkHasKey.AutoSize = true;
            this.chkHasKey.Location = new System.Drawing.Point(247, 105);
            this.chkHasKey.Name = "chkHasKey";
            this.chkHasKey.Size = new System.Drawing.Size(130, 21);
            this.chkHasKey.TabIndex = 8;
            this.chkHasKey.Text = "Has key column";
            this.chkHasKey.UseVisualStyleBackColor = true;
            // 
            // NewRelation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(522, 209);
            this.Controls.Add(this.chkHasKey);
            this.Controls.Add(this.txtJoinTable);
            this.Controls.Add(this.lblJoinTable);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAddRelation);
            this.Controls.Add(this.cmbRelationType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbEntities);
            this.Controls.Add(this.label1);
            this.Name = "NewRelation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "New Relation";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbEntities;
        private System.Windows.Forms.ComboBox cmbRelationType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnAddRelation;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblJoinTable;
        private System.Windows.Forms.TextBox txtJoinTable;
        private System.Windows.Forms.CheckBox chkHasKey;
    }
}