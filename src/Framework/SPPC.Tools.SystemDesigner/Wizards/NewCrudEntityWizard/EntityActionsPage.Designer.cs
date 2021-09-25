namespace SPPC.Tools.SystemDesigner.Wizards.NewCrudEntityWizard
{
    partial class EntityActionsPage
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
            this.grpCrudActions = new System.Windows.Forms.GroupBox();
            this.chkDelete = new System.Windows.Forms.CheckBox();
            this.chkModify = new System.Windows.Forms.CheckBox();
            this.chkCreate = new System.Windows.Forms.CheckBox();
            this.chkView = new System.Windows.Forms.CheckBox();
            this.lstActions = new System.Windows.Forms.ListBox();
            this.btnRemove = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.chkFilter = new System.Windows.Forms.CheckBox();
            this.chkPrint = new System.Windows.Forms.CheckBox();
            this.chkExport = new System.Windows.Forms.CheckBox();
            this.grpCrudActions.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpCrudActions
            // 
            this.grpCrudActions.Controls.Add(this.chkExport);
            this.grpCrudActions.Controls.Add(this.chkPrint);
            this.grpCrudActions.Controls.Add(this.chkFilter);
            this.grpCrudActions.Controls.Add(this.chkDelete);
            this.grpCrudActions.Controls.Add(this.chkModify);
            this.grpCrudActions.Controls.Add(this.chkCreate);
            this.grpCrudActions.Controls.Add(this.chkView);
            this.grpCrudActions.Location = new System.Drawing.Point(17, 17);
            this.grpCrudActions.Name = "grpCrudActions";
            this.grpCrudActions.Size = new System.Drawing.Size(618, 72);
            this.grpCrudActions.TabIndex = 0;
            this.grpCrudActions.TabStop = false;
            this.grpCrudActions.Text = "Standard Actions";
            // 
            // chkDelete
            // 
            this.chkDelete.AutoSize = true;
            this.chkDelete.Location = new System.Drawing.Point(268, 36);
            this.chkDelete.Name = "chkDelete";
            this.chkDelete.Size = new System.Drawing.Size(71, 21);
            this.chkDelete.TabIndex = 3;
            this.chkDelete.Text = "Delete";
            this.chkDelete.UseVisualStyleBackColor = true;
            // 
            // chkModify
            // 
            this.chkModify.AutoSize = true;
            this.chkModify.Location = new System.Drawing.Point(179, 36);
            this.chkModify.Name = "chkModify";
            this.chkModify.Size = new System.Drawing.Size(71, 21);
            this.chkModify.TabIndex = 2;
            this.chkModify.Text = "Modify";
            this.chkModify.UseVisualStyleBackColor = true;
            // 
            // chkCreate
            // 
            this.chkCreate.AutoSize = true;
            this.chkCreate.Location = new System.Drawing.Point(89, 36);
            this.chkCreate.Name = "chkCreate";
            this.chkCreate.Size = new System.Drawing.Size(72, 21);
            this.chkCreate.TabIndex = 1;
            this.chkCreate.Text = "Create";
            this.chkCreate.UseVisualStyleBackColor = true;
            // 
            // chkView
            // 
            this.chkView.AutoSize = true;
            this.chkView.Location = new System.Drawing.Point(12, 36);
            this.chkView.Name = "chkView";
            this.chkView.Size = new System.Drawing.Size(59, 21);
            this.chkView.TabIndex = 0;
            this.chkView.Text = "View";
            this.chkView.UseVisualStyleBackColor = true;
            // 
            // lstActions
            // 
            this.lstActions.FormattingEnabled = true;
            this.lstActions.ItemHeight = 16;
            this.lstActions.Location = new System.Drawing.Point(17, 136);
            this.lstActions.Name = "lstActions";
            this.lstActions.Size = new System.Drawing.Size(540, 228);
            this.lstActions.TabIndex = 2;
            this.lstActions.SelectedIndexChanged += new System.EventHandler(this.Actions_SelectedIndexChanged);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(452, 370);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(105, 34);
            this.btnRemove.TabIndex = 6;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.Remove_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 113);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Custom Actions :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 379);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Action Name :";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(118, 376);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(247, 22);
            this.txtName.TabIndex = 4;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(371, 370);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 34);
            this.btnAdd.TabIndex = 5;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.Add_Click);
            // 
            // chkFilter
            // 
            this.chkFilter.AutoSize = true;
            this.chkFilter.Location = new System.Drawing.Point(357, 36);
            this.chkFilter.Name = "chkFilter";
            this.chkFilter.Size = new System.Drawing.Size(61, 21);
            this.chkFilter.TabIndex = 4;
            this.chkFilter.Text = "Filter";
            this.chkFilter.UseVisualStyleBackColor = true;
            // 
            // chkPrint
            // 
            this.chkPrint.AutoSize = true;
            this.chkPrint.Location = new System.Drawing.Point(446, 36);
            this.chkPrint.Name = "chkPrint";
            this.chkPrint.Size = new System.Drawing.Size(59, 21);
            this.chkPrint.TabIndex = 5;
            this.chkPrint.Text = "Print";
            this.chkPrint.UseVisualStyleBackColor = true;
            // 
            // chkExport
            // 
            this.chkExport.AutoSize = true;
            this.chkExport.Location = new System.Drawing.Point(535, 36);
            this.chkExport.Name = "chkExport";
            this.chkExport.Size = new System.Drawing.Size(70, 21);
            this.chkExport.TabIndex = 6;
            this.chkExport.Text = "Export";
            this.chkExport.UseVisualStyleBackColor = true;
            // 
            // EntityActionsPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.lstActions);
            this.Controls.Add(this.grpCrudActions);
            this.Name = "EntityActionsPage";
            this.Size = new System.Drawing.Size(650, 420);
            this.grpCrudActions.ResumeLayout(false);
            this.grpCrudActions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpCrudActions;
        private System.Windows.Forms.CheckBox chkDelete;
        private System.Windows.Forms.CheckBox chkModify;
        private System.Windows.Forms.CheckBox chkCreate;
        private System.Windows.Forms.CheckBox chkView;
        private System.Windows.Forms.ListBox lstActions;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.CheckBox chkExport;
        private System.Windows.Forms.CheckBox chkPrint;
        private System.Windows.Forms.CheckBox chkFilter;
    }
}
