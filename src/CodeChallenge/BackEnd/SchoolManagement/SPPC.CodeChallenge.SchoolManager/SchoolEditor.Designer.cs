
namespace SPPC.CodeChallenge.SchoolManager
{
    partial class SchoolEditor
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.cmbAdminType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtManagerName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.spnCapacity = new System.Windows.Forms.NumericUpDown();
            this.spnTuition = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dtpEstablished = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbProvince = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbCity = new System.Windows.Forms.ComboBox();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.chkListed = new System.Windows.Forms.CheckBox();
            this.tooltip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.spnCapacity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnTuition)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name :";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(12, 48);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(577, 27);
            this.txtName.TabIndex = 1;
            // 
            // cmbAdminType
            // 
            this.cmbAdminType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAdminType.FormattingEnabled = true;
            this.cmbAdminType.Items.AddRange(new object[] {
            "دولتی",
            "نیمه دولتی",
            "هیئت امنایی",
            "غیرانتفاعی"});
            this.cmbAdminType.Location = new System.Drawing.Point(12, 125);
            this.cmbAdminType.Name = "cmbAdminType";
            this.cmbAdminType.Size = new System.Drawing.Size(263, 28);
            this.cmbAdminType.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(149, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Administration Type :";
            // 
            // txtManagerName
            // 
            this.txtManagerName.Location = new System.Drawing.Point(12, 203);
            this.txtManagerName.Name = "txtManagerName";
            this.txtManagerName.Size = new System.Drawing.Size(577, 27);
            this.txtManagerName.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 176);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(119, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "Manager Name :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 255);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 20);
            this.label4.TabIndex = 8;
            this.label4.Text = "Capacity :";
            // 
            // spnCapacity
            // 
            this.spnCapacity.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.spnCapacity.Location = new System.Drawing.Point(12, 282);
            this.spnCapacity.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.spnCapacity.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.spnCapacity.Name = "spnCapacity";
            this.spnCapacity.Size = new System.Drawing.Size(263, 27);
            this.spnCapacity.TabIndex = 9;
            this.spnCapacity.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // spnTuition
            // 
            this.spnTuition.Increment = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.spnTuition.Location = new System.Drawing.Point(298, 126);
            this.spnTuition.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.spnTuition.Minimum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.spnTuition.Name = "spnTuition";
            this.spnTuition.Size = new System.Drawing.Size(291, 27);
            this.spnTuition.TabIndex = 5;
            this.spnTuition.ThousandsSeparator = true;
            this.spnTuition.Value = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(298, 98);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 20);
            this.label5.TabIndex = 4;
            this.label5.Text = "Tuition :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(298, 255);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(114, 20);
            this.label6.TabIndex = 10;
            this.label6.Text = "Established On :";
            // 
            // dtpEstablished
            // 
            this.dtpEstablished.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEstablished.Location = new System.Drawing.Point(298, 282);
            this.dtpEstablished.Name = "dtpEstablished";
            this.dtpEstablished.Size = new System.Drawing.Size(291, 27);
            this.dtpEstablished.TabIndex = 11;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 331);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 20);
            this.label7.TabIndex = 12;
            this.label7.Text = "Province :";
            // 
            // cmbProvince
            // 
            this.cmbProvince.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProvince.FormattingEnabled = true;
            this.cmbProvince.Items.AddRange(new object[] {
            "دولتی",
            "نیمه دولتی",
            "هیئت امنایی",
            "غیرانتفاعی"});
            this.cmbProvince.Location = new System.Drawing.Point(12, 358);
            this.cmbProvince.Name = "cmbProvince";
            this.cmbProvince.Size = new System.Drawing.Size(263, 28);
            this.cmbProvince.TabIndex = 13;
            this.cmbProvince.SelectedIndexChanged += new System.EventHandler(this.Province_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(298, 331);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 20);
            this.label8.TabIndex = 14;
            this.label8.Text = "City :";
            // 
            // cmbCity
            // 
            this.cmbCity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCity.FormattingEnabled = true;
            this.cmbCity.Items.AddRange(new object[] {
            "دولتی",
            "نیمه دولتی",
            "هیئت امنایی",
            "غیرانتفاعی"});
            this.cmbCity.Location = new System.Drawing.Point(298, 358);
            this.cmbCity.Name = "cmbCity";
            this.cmbCity.Size = new System.Drawing.Size(291, 28);
            this.cmbCity.TabIndex = 15;
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(12, 438);
            this.txtAddress.Multiline = true;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtAddress.Size = new System.Drawing.Size(577, 115);
            this.txtAddress.TabIndex = 17;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 411);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(69, 20);
            this.label9.TabIndex = 16;
            this.label9.Text = "Address :";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(113, 603);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(95, 35);
            this.btnCancel.TabIndex = 20;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(12, 603);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(95, 35);
            this.btnSave.TabIndex = 19;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.Save_Click);
            // 
            // chkListed
            // 
            this.chkListed.AutoSize = true;
            this.chkListed.Location = new System.Drawing.Point(13, 565);
            this.chkListed.Name = "chkListed";
            this.chkListed.Size = new System.Drawing.Size(200, 24);
            this.chkListed.TabIndex = 18;
            this.chkListed.Text = "Listed in School Directory";
            this.chkListed.UseVisualStyleBackColor = true;
            // 
            // SchoolEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(601, 647);
            this.Controls.Add(this.chkListed);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cmbCity);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cmbProvince);
            this.Controls.Add(this.dtpEstablished);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.spnTuition);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.spnCapacity);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtManagerName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbAdminType);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label1);
            this.Name = "SchoolEditor";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SchoolEditor";
            ((System.ComponentModel.ISupportInitialize)(this.spnCapacity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnTuition)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.ComboBox cmbAdminType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtManagerName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown spnCapacity;
        private System.Windows.Forms.NumericUpDown spnTuition;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtpEstablished;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbProvince;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbCity;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.CheckBox chkListed;
        private System.Windows.Forms.ToolTip tooltip;
    }
}