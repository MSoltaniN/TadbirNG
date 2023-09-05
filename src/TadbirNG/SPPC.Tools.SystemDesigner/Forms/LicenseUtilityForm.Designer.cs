
namespace SPPC.Tools.SystemDesigner.Forms
{
    partial class LicenseUtilityForm
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
            this.spnOfflineLimit = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.lblCustomer = new System.Windows.Forms.Label();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.cmbEdition = new System.Windows.Forms.ComboBox();
            this.spnUserCount = new System.Windows.Forms.NumericUpDown();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnRenew = new System.Windows.Forms.Button();
            this.txtCustomer = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.spnOfflineLimit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnUserCount)).BeginInit();
            this.SuspendLayout();
            // 
            // spnOfflineLimit
            // 
            this.spnOfflineLimit.Location = new System.Drawing.Point(186, 234);
            this.spnOfflineLimit.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.spnOfflineLimit.Name = "spnOfflineLimit";
            this.spnOfflineLimit.Size = new System.Drawing.Size(97, 27);
            this.spnOfflineLimit.TabIndex = 27;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(17, 236);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 25);
            this.label1.TabIndex = 26;
            this.label1.Text = "Offline Limit :";
            // 
            // lblCustomer
            // 
            this.lblCustomer.AutoSize = true;
            this.lblCustomer.Location = new System.Drawing.Point(17, 20);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new System.Drawing.Size(79, 20);
            this.lblCustomer.TabIndex = 16;
            this.lblCustomer.Text = "Customer :";
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEndDate.Location = new System.Drawing.Point(186, 192);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(239, 27);
            this.dtpEndDate.TabIndex = 25;
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStartDate.Location = new System.Drawing.Point(186, 150);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(239, 27);
            this.dtpStartDate.TabIndex = 23;
            // 
            // cmbEdition
            // 
            this.cmbEdition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEdition.FormattingEnabled = true;
            this.cmbEdition.Items.AddRange(new object[] {
            "Standard",
            "Professional",
            "Enterprise"});
            this.cmbEdition.Location = new System.Drawing.Point(186, 106);
            this.cmbEdition.Name = "cmbEdition";
            this.cmbEdition.Size = new System.Drawing.Size(239, 28);
            this.cmbEdition.TabIndex = 21;
            // 
            // spnUserCount
            // 
            this.spnUserCount.Location = new System.Drawing.Point(186, 61);
            this.spnUserCount.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.spnUserCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spnUserCount.Name = "spnUserCount";
            this.spnUserCount.Size = new System.Drawing.Size(97, 27);
            this.spnUserCount.TabIndex = 19;
            this.spnUserCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(17, 194);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(119, 25);
            this.label14.TabIndex = 24;
            this.label14.Text = "End Date :";
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(17, 152);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(119, 25);
            this.label13.TabIndex = 22;
            this.label13.Text = "Start Date :";
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(17, 106);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(119, 28);
            this.label12.TabIndex = 20;
            this.label12.Text = "Edition :";
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(17, 61);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(119, 27);
            this.label11.TabIndex = 18;
            this.label11.Text = "User Count :";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(377, 312);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(115, 33);
            this.btnCancel.TabIndex = 28;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // btnRenew
            // 
            this.btnRenew.Location = new System.Drawing.Point(186, 312);
            this.btnRenew.Name = "btnRenew";
            this.btnRenew.Size = new System.Drawing.Size(185, 33);
            this.btnRenew.TabIndex = 29;
            this.btnRenew.Text = "Renew License";
            this.btnRenew.UseVisualStyleBackColor = true;
            this.btnRenew.Click += new System.EventHandler(this.Renew_Click);
            // 
            // txtCustomer
            // 
            this.txtCustomer.Location = new System.Drawing.Point(186, 17);
            this.txtCustomer.Name = "txtCustomer";
            this.txtCustomer.ReadOnly = true;
            this.txtCustomer.Size = new System.Drawing.Size(306, 27);
            this.txtCustomer.TabIndex = 30;
            // 
            // LicenseUtilityForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(504, 353);
            this.Controls.Add(this.txtCustomer);
            this.Controls.Add(this.btnRenew);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.spnOfflineLimit);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblCustomer);
            this.Controls.Add(this.dtpEndDate);
            this.Controls.Add(this.dtpStartDate);
            this.Controls.Add(this.cmbEdition);
            this.Controls.Add(this.spnUserCount);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LicenseUtilityForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Renew Developer License";
            ((System.ComponentModel.ISupportInitialize)(this.spnOfflineLimit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnUserCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown spnOfflineLimit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCustomer;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.ComboBox cmbEdition;
        private System.Windows.Forms.NumericUpDown spnUserCount;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnRenew;
        private System.Windows.Forms.TextBox txtCustomer;
    }
}