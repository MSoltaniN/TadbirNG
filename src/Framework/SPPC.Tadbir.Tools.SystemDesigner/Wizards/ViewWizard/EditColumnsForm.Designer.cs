namespace SPPC.Tadbir.Tools.SystemDesigner.Wizards.ViewWizard
{
    partial class EditColumnsForm
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
            this.lbxColumns = new System.Windows.Forms.CheckedListBox();
            this.grpColumn = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.cmbDotNetType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbScriptType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbStorageType = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.spnLength = new System.Windows.Forms.NumericUpDown();
            this.spnMinLength = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.chkIsFixedLength = new System.Windows.Forms.CheckBox();
            this.chkAllowSorting = new System.Windows.Forms.CheckBox();
            this.chkAllowFiltering = new System.Windows.Forms.CheckBox();
            this.chkIsNullable = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtExpression = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnMoveUp = new System.Windows.Forms.Button();
            this.btnMoveDown = new System.Windows.Forms.Button();
            this.grpColumn.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spnLength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnMinLength)).BeginInit();
            this.SuspendLayout();
            // 
            // lbxColumns
            // 
            this.lbxColumns.FormattingEnabled = true;
            this.lbxColumns.Location = new System.Drawing.Point(14, 32);
            this.lbxColumns.Name = "lbxColumns";
            this.lbxColumns.Size = new System.Drawing.Size(264, 395);
            this.lbxColumns.TabIndex = 0;
            this.lbxColumns.SelectedIndexChanged += new System.EventHandler(this.lbxColumns_SelectedIndexChanged);
            // 
            // grpColumn
            // 
            this.grpColumn.Controls.Add(this.txtExpression);
            this.grpColumn.Controls.Add(this.label8);
            this.grpColumn.Controls.Add(this.chkAllowFiltering);
            this.grpColumn.Controls.Add(this.chkIsNullable);
            this.grpColumn.Controls.Add(this.chkAllowSorting);
            this.grpColumn.Controls.Add(this.chkIsFixedLength);
            this.grpColumn.Controls.Add(this.spnMinLength);
            this.grpColumn.Controls.Add(this.label7);
            this.grpColumn.Controls.Add(this.spnLength);
            this.grpColumn.Controls.Add(this.label6);
            this.grpColumn.Controls.Add(this.cmbScriptType);
            this.grpColumn.Controls.Add(this.label4);
            this.grpColumn.Controls.Add(this.cmbStorageType);
            this.grpColumn.Controls.Add(this.label5);
            this.grpColumn.Controls.Add(this.cmbDotNetType);
            this.grpColumn.Controls.Add(this.label3);
            this.grpColumn.Controls.Add(this.cmbType);
            this.grpColumn.Controls.Add(this.label2);
            this.grpColumn.Controls.Add(this.txtName);
            this.grpColumn.Controls.Add(this.label1);
            this.grpColumn.Location = new System.Drawing.Point(296, 26);
            this.grpColumn.Name = "grpColumn";
            this.grpColumn.Size = new System.Drawing.Size(328, 401);
            this.grpColumn.TabIndex = 1;
            this.grpColumn.TabStop = false;
            this.grpColumn.Text = "Column properties";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Name :";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(6, 79);
            this.txtName.Name = "txtName";
            this.txtName.ReadOnly = true;
            this.txtName.Size = new System.Drawing.Size(305, 22);
            this.txtName.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 114);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Type :";
            // 
            // cmbType
            // 
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Items.AddRange(new object[] {
            "(not set)",
            "Money",
            "Amount"});
            this.cmbType.Location = new System.Drawing.Point(6, 134);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(140, 24);
            this.cmbType.TabIndex = 5;
            // 
            // cmbDotNetType
            // 
            this.cmbDotNetType.FormattingEnabled = true;
            this.cmbDotNetType.Items.AddRange(new object[] {
            "System.Int16",
            "System.Int32",
            "System.Int64",
            "System.Single",
            "System.Double",
            "System.Decimal",
            "System.String",
            "System.DateTime",
            "System.Boolean",
            "System.Object"});
            this.cmbDotNetType.Location = new System.Drawing.Point(152, 134);
            this.cmbDotNetType.Name = "cmbDotNetType";
            this.cmbDotNetType.Size = new System.Drawing.Size(159, 24);
            this.cmbDotNetType.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(149, 114);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = ".NET type :";
            // 
            // cmbScriptType
            // 
            this.cmbScriptType.FormattingEnabled = true;
            this.cmbScriptType.Items.AddRange(new object[] {
            "number",
            "string",
            "boolean",
            "Date",
            "Object"});
            this.cmbScriptType.Location = new System.Drawing.Point(152, 189);
            this.cmbScriptType.Name = "cmbScriptType";
            this.cmbScriptType.Size = new System.Drawing.Size(159, 24);
            this.cmbScriptType.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(149, 169);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 17);
            this.label4.TabIndex = 10;
            this.label4.Text = "Script type :";
            // 
            // cmbStorageType
            // 
            this.cmbStorageType.FormattingEnabled = true;
            this.cmbStorageType.Items.AddRange(new object[] {
            "nvarchar",
            "datetime",
            "date",
            "time",
            "bit",
            "tinyint",
            "smallint",
            "int",
            "bigint",
            "money",
            "float"});
            this.cmbStorageType.Location = new System.Drawing.Point(6, 189);
            this.cmbStorageType.Name = "cmbStorageType";
            this.cmbStorageType.Size = new System.Drawing.Size(140, 24);
            this.cmbStorageType.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 169);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 17);
            this.label5.TabIndex = 8;
            this.label5.Text = "SQL type :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 224);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 17);
            this.label6.TabIndex = 12;
            this.label6.Text = "Length :";
            // 
            // spnLength
            // 
            this.spnLength.Location = new System.Drawing.Point(6, 244);
            this.spnLength.Name = "spnLength";
            this.spnLength.Size = new System.Drawing.Size(140, 22);
            this.spnLength.TabIndex = 13;
            // 
            // spnMinLength
            // 
            this.spnMinLength.Location = new System.Drawing.Point(152, 244);
            this.spnMinLength.Name = "spnMinLength";
            this.spnMinLength.Size = new System.Drawing.Size(97, 22);
            this.spnMinLength.TabIndex = 15;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(149, 224);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(114, 17);
            this.label7.TabIndex = 14;
            this.label7.Text = "Minimum length :";
            // 
            // chkIsFixedLength
            // 
            this.chkIsFixedLength.AutoSize = true;
            this.chkIsFixedLength.Location = new System.Drawing.Point(6, 282);
            this.chkIsFixedLength.Name = "chkIsFixedLength";
            this.chkIsFixedLength.Size = new System.Drawing.Size(106, 21);
            this.chkIsFixedLength.TabIndex = 16;
            this.chkIsFixedLength.Text = "Fixed length";
            this.chkIsFixedLength.UseVisualStyleBackColor = true;
            // 
            // chkAllowSorting
            // 
            this.chkAllowSorting.AutoSize = true;
            this.chkAllowSorting.Location = new System.Drawing.Point(6, 309);
            this.chkAllowSorting.Name = "chkAllowSorting";
            this.chkAllowSorting.Size = new System.Drawing.Size(109, 21);
            this.chkAllowSorting.TabIndex = 17;
            this.chkAllowSorting.Text = "Allow sorting";
            this.chkAllowSorting.UseVisualStyleBackColor = true;
            // 
            // chkAllowFiltering
            // 
            this.chkAllowFiltering.AutoSize = true;
            this.chkAllowFiltering.Location = new System.Drawing.Point(152, 309);
            this.chkAllowFiltering.Name = "chkAllowFiltering";
            this.chkAllowFiltering.Size = new System.Drawing.Size(112, 21);
            this.chkAllowFiltering.TabIndex = 19;
            this.chkAllowFiltering.Text = "Allow filtering";
            this.chkAllowFiltering.UseVisualStyleBackColor = true;
            // 
            // chkIsNullable
            // 
            this.chkIsNullable.AutoSize = true;
            this.chkIsNullable.Location = new System.Drawing.Point(152, 282);
            this.chkIsNullable.Name = "chkIsNullable";
            this.chkIsNullable.Size = new System.Drawing.Size(81, 21);
            this.chkIsNullable.TabIndex = 18;
            this.chkIsNullable.Text = "Nullable";
            this.chkIsNullable.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 348);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(85, 17);
            this.label8.TabIndex = 20;
            this.label8.Text = "Expression :";
            // 
            // txtExpression
            // 
            this.txtExpression.Location = new System.Drawing.Point(6, 368);
            this.txtExpression.Name = "txtExpression";
            this.txtExpression.Size = new System.Drawing.Size(305, 22);
            this.txtExpression.TabIndex = 21;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(11, 12);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(129, 17);
            this.label9.TabIndex = 22;
            this.label9.Text = "Available columns :";
            // 
            // btnMoveUp
            // 
            this.btnMoveUp.Location = new System.Drawing.Point(14, 434);
            this.btnMoveUp.Name = "btnMoveUp";
            this.btnMoveUp.Size = new System.Drawing.Size(126, 33);
            this.btnMoveUp.TabIndex = 23;
            this.btnMoveUp.Text = "Move Up";
            this.btnMoveUp.UseVisualStyleBackColor = true;
            // 
            // btnMoveDown
            // 
            this.btnMoveDown.Location = new System.Drawing.Point(152, 433);
            this.btnMoveDown.Name = "btnMoveDown";
            this.btnMoveDown.Size = new System.Drawing.Size(126, 33);
            this.btnMoveDown.TabIndex = 24;
            this.btnMoveDown.Text = "Move Down";
            this.btnMoveDown.UseVisualStyleBackColor = true;
            // 
            // EditColumnsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnMoveDown);
            this.Controls.Add(this.btnMoveUp);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.grpColumn);
            this.Controls.Add(this.lbxColumns);
            this.Name = "EditColumnsForm";
            this.Size = new System.Drawing.Size(636, 478);
            this.grpColumn.ResumeLayout(false);
            this.grpColumn.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spnLength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnMinLength)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox lbxColumns;
        private System.Windows.Forms.GroupBox grpColumn;
        private System.Windows.Forms.ComboBox cmbScriptType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbStorageType;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbDotNetType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtExpression;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox chkAllowFiltering;
        private System.Windows.Forms.CheckBox chkIsNullable;
        private System.Windows.Forms.CheckBox chkAllowSorting;
        private System.Windows.Forms.CheckBox chkIsFixedLength;
        private System.Windows.Forms.NumericUpDown spnMinLength;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown spnLength;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnMoveUp;
        private System.Windows.Forms.Button btnMoveDown;
    }
}
