namespace SPPC.Tools.MetaDesigner.Forms
{
    partial class NewProperty
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
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.chkAutoGenerate = new System.Windows.Forms.CheckBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblType = new System.Windows.Forms.Label();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.grpValidation = new System.Windows.Forms.GroupBox();
            this.txtFormat = new System.Windows.Forms.TextBox();
            this.txtMaximum = new System.Windows.Forms.TextBox();
            this.txtMinimum = new System.Windows.Forms.TextBox();
            this.lblFormat = new System.Windows.Forms.Label();
            this.lblMaximum = new System.Windows.Forms.Label();
            this.lblMinimum = new System.Windows.Forms.Label();
            this.chkRequired = new System.Windows.Forms.CheckBox();
            this.grpValidation.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(11, 37);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(116, 18);
            this.lblName.TabIndex = 2;
            this.lblName.Text = "Property Name :";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(14, 58);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(239, 24);
            this.txtName.TabIndex = 3;
            // 
            // chkAutoGenerate
            // 
            this.chkAutoGenerate.AutoSize = true;
            this.chkAutoGenerate.Location = new System.Drawing.Point(16, 156);
            this.chkAutoGenerate.Name = "chkAutoGenerate";
            this.chkAutoGenerate.Size = new System.Drawing.Size(246, 22);
            this.chkAutoGenerate.TabIndex = 6;
            this.chkAutoGenerate.Text = "Auto-populate property members";
            this.chkAutoGenerate.UseVisualStyleBackColor = true;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(14, 195);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(116, 26);
            this.btnAdd.TabIndex = 8;
            this.btnAdd.Text = "Add Property";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.Add_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(136, 195);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(84, 26);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(11, 103);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(108, 18);
            this.lblType.TabIndex = 4;
            this.lblType.Text = "Property Type :";
            // 
            // cmbType
            // 
            this.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Location = new System.Drawing.Point(14, 124);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(239, 26);
            this.cmbType.TabIndex = 5;
            this.cmbType.SelectedIndexChanged += new System.EventHandler(this.Type_SelectedIndexChanged);
            // 
            // grpValidation
            // 
            this.grpValidation.Controls.Add(this.txtFormat);
            this.grpValidation.Controls.Add(this.txtMaximum);
            this.grpValidation.Controls.Add(this.txtMinimum);
            this.grpValidation.Controls.Add(this.lblFormat);
            this.grpValidation.Controls.Add(this.lblMaximum);
            this.grpValidation.Controls.Add(this.lblMinimum);
            this.grpValidation.Controls.Add(this.chkRequired);
            this.grpValidation.Location = new System.Drawing.Point(267, 10);
            this.grpValidation.Name = "grpValidation";
            this.grpValidation.Size = new System.Drawing.Size(335, 168);
            this.grpValidation.TabIndex = 7;
            this.grpValidation.TabStop = false;
            this.grpValidation.Text = "Validation";
            // 
            // txtFormat
            // 
            this.txtFormat.Location = new System.Drawing.Point(6, 114);
            this.txtFormat.Name = "txtFormat";
            this.txtFormat.Size = new System.Drawing.Size(150, 24);
            this.txtFormat.TabIndex = 5;
            // 
            // txtMaximum
            // 
            this.txtMaximum.Location = new System.Drawing.Point(179, 48);
            this.txtMaximum.Name = "txtMaximum";
            this.txtMaximum.Size = new System.Drawing.Size(150, 24);
            this.txtMaximum.TabIndex = 3;
            // 
            // txtMinimum
            // 
            this.txtMinimum.Location = new System.Drawing.Point(9, 48);
            this.txtMinimum.Name = "txtMinimum";
            this.txtMinimum.Size = new System.Drawing.Size(150, 24);
            this.txtMinimum.TabIndex = 1;
            // 
            // lblFormat
            // 
            this.lblFormat.AutoSize = true;
            this.lblFormat.Location = new System.Drawing.Point(6, 93);
            this.lblFormat.Name = "lblFormat";
            this.lblFormat.Size = new System.Drawing.Size(64, 18);
            this.lblFormat.TabIndex = 4;
            this.lblFormat.Text = "Format :";
            // 
            // lblMaximum
            // 
            this.lblMaximum.AutoSize = true;
            this.lblMaximum.Location = new System.Drawing.Point(179, 27);
            this.lblMaximum.Name = "lblMaximum";
            this.lblMaximum.Size = new System.Drawing.Size(81, 18);
            this.lblMaximum.TabIndex = 2;
            this.lblMaximum.Text = "Maximum :";
            // 
            // lblMinimum
            // 
            this.lblMinimum.AutoSize = true;
            this.lblMinimum.Location = new System.Drawing.Point(6, 27);
            this.lblMinimum.Name = "lblMinimum";
            this.lblMinimum.Size = new System.Drawing.Size(77, 18);
            this.lblMinimum.TabIndex = 0;
            this.lblMinimum.Text = "Minimum :";
            // 
            // chkRequired
            // 
            this.chkRequired.AutoSize = true;
            this.chkRequired.Location = new System.Drawing.Point(179, 116);
            this.chkRequired.Name = "chkRequired";
            this.chkRequired.Size = new System.Drawing.Size(89, 22);
            this.chkRequired.TabIndex = 6;
            this.chkRequired.Text = "Required";
            this.chkRequired.UseVisualStyleBackColor = true;
            // 
            // NewProperty
            // 
            this.AcceptButton = this.btnAdd;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(612, 229);
            this.Controls.Add(this.grpValidation);
            this.Controls.Add(this.cmbType);
            this.Controls.Add(this.lblType);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.chkAutoGenerate);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblName);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewProperty";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add new property";
            this.grpValidation.ResumeLayout(false);
            this.grpValidation.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.CheckBox chkAutoGenerate;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.GroupBox grpValidation;
        private System.Windows.Forms.TextBox txtFormat;
        private System.Windows.Forms.TextBox txtMaximum;
        private System.Windows.Forms.TextBox txtMinimum;
        private System.Windows.Forms.Label lblFormat;
        private System.Windows.Forms.Label lblMaximum;
        private System.Windows.Forms.Label lblMinimum;
        private System.Windows.Forms.CheckBox chkRequired;
    }
}