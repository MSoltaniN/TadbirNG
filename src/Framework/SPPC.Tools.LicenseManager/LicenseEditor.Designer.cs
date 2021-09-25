
namespace SPPC.Tools.LicenseManager
{
    partial class LicenseEditor
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
            this.grpSubsystems = new System.Windows.Forms.GroupBox();
            this.chkBudgeting = new System.Windows.Forms.CheckBox();
            this.chkWarehousing = new System.Windows.Forms.CheckBox();
            this.chkSales = new System.Windows.Forms.CheckBox();
            this.chkPurchase = new System.Windows.Forms.CheckBox();
            this.chkInventory = new System.Windows.Forms.CheckBox();
            this.chkPersonnel = new System.Windows.Forms.CheckBox();
            this.chkWagePayment = new System.Windows.Forms.CheckBox();
            this.chkCashFlow = new System.Windows.Forms.CheckBox();
            this.chkCheque = new System.Windows.Forms.CheckBox();
            this.chkAccounting = new System.Windows.Forms.CheckBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.cmbEdition = new System.Windows.Forms.ComboBox();
            this.spnUserCount = new System.Windows.Forms.NumericUpDown();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSaveLicense = new System.Windows.Forms.Button();
            this.cmbCustomer = new System.Windows.Forms.ComboBox();
            this.lblCustomer = new System.Windows.Forms.Label();
            this.chkIsActivated = new System.Windows.Forms.CheckBox();
            this.grpSubsystems.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spnUserCount)).BeginInit();
            this.SuspendLayout();
            // 
            // grpSubsystems
            // 
            this.grpSubsystems.Controls.Add(this.chkBudgeting);
            this.grpSubsystems.Controls.Add(this.chkWarehousing);
            this.grpSubsystems.Controls.Add(this.chkSales);
            this.grpSubsystems.Controls.Add(this.chkPurchase);
            this.grpSubsystems.Controls.Add(this.chkInventory);
            this.grpSubsystems.Controls.Add(this.chkPersonnel);
            this.grpSubsystems.Controls.Add(this.chkWagePayment);
            this.grpSubsystems.Controls.Add(this.chkCashFlow);
            this.grpSubsystems.Controls.Add(this.chkCheque);
            this.grpSubsystems.Controls.Add(this.chkAccounting);
            this.grpSubsystems.Location = new System.Drawing.Point(15, 248);
            this.grpSubsystems.Name = "grpSubsystems";
            this.grpSubsystems.Size = new System.Drawing.Size(465, 218);
            this.grpSubsystems.TabIndex = 10;
            this.grpSubsystems.TabStop = false;
            this.grpSubsystems.Text = "زیرسیستم های فعال";
            // 
            // chkBudgeting
            // 
            this.chkBudgeting.Location = new System.Drawing.Point(12, 174);
            this.chkBudgeting.Name = "chkBudgeting";
            this.chkBudgeting.Size = new System.Drawing.Size(215, 27);
            this.chkBudgeting.TabIndex = 9;
            this.chkBudgeting.Text = "بودجه و اعتبارات";
            this.chkBudgeting.UseVisualStyleBackColor = true;
            // 
            // chkWarehousing
            // 
            this.chkWarehousing.Location = new System.Drawing.Point(12, 140);
            this.chkWarehousing.Name = "chkWarehousing";
            this.chkWarehousing.Size = new System.Drawing.Size(215, 27);
            this.chkWarehousing.TabIndex = 8;
            this.chkWarehousing.Text = "انبارداری";
            this.chkWarehousing.UseVisualStyleBackColor = true;
            // 
            // chkSales
            // 
            this.chkSales.Location = new System.Drawing.Point(12, 106);
            this.chkSales.Name = "chkSales";
            this.chkSales.Size = new System.Drawing.Size(215, 27);
            this.chkSales.TabIndex = 7;
            this.chkSales.Text = "فروش";
            this.chkSales.UseVisualStyleBackColor = true;
            // 
            // chkPurchase
            // 
            this.chkPurchase.Location = new System.Drawing.Point(12, 72);
            this.chkPurchase.Name = "chkPurchase";
            this.chkPurchase.Size = new System.Drawing.Size(215, 27);
            this.chkPurchase.TabIndex = 6;
            this.chkPurchase.Text = "خرید و تدارکات";
            this.chkPurchase.UseVisualStyleBackColor = true;
            // 
            // chkInventory
            // 
            this.chkInventory.Location = new System.Drawing.Point(12, 38);
            this.chkInventory.Name = "chkInventory";
            this.chkInventory.Size = new System.Drawing.Size(215, 27);
            this.chkInventory.TabIndex = 5;
            this.chkInventory.Text = "کنترل موجودی";
            this.chkInventory.UseVisualStyleBackColor = true;
            // 
            // chkPersonnel
            // 
            this.chkPersonnel.Location = new System.Drawing.Point(237, 174);
            this.chkPersonnel.Name = "chkPersonnel";
            this.chkPersonnel.Size = new System.Drawing.Size(215, 27);
            this.chkPersonnel.TabIndex = 4;
            this.chkPersonnel.Text = "پرسنلی";
            this.chkPersonnel.UseVisualStyleBackColor = true;
            // 
            // chkWagePayment
            // 
            this.chkWagePayment.Location = new System.Drawing.Point(237, 140);
            this.chkWagePayment.Name = "chkWagePayment";
            this.chkWagePayment.Size = new System.Drawing.Size(215, 27);
            this.chkWagePayment.TabIndex = 3;
            this.chkWagePayment.Text = "حقوق و دستمزد";
            this.chkWagePayment.UseVisualStyleBackColor = true;
            // 
            // chkCashFlow
            // 
            this.chkCashFlow.Location = new System.Drawing.Point(237, 106);
            this.chkCashFlow.Name = "chkCashFlow";
            this.chkCashFlow.Size = new System.Drawing.Size(215, 27);
            this.chkCashFlow.TabIndex = 2;
            this.chkCashFlow.Text = "خزانه داری - نقدینگی";
            this.chkCashFlow.UseVisualStyleBackColor = true;
            // 
            // chkCheque
            // 
            this.chkCheque.Location = new System.Drawing.Point(237, 72);
            this.chkCheque.Name = "chkCheque";
            this.chkCheque.Size = new System.Drawing.Size(215, 27);
            this.chkCheque.TabIndex = 1;
            this.chkCheque.Text = "خزانه داری - چک";
            this.chkCheque.UseVisualStyleBackColor = true;
            // 
            // chkAccounting
            // 
            this.chkAccounting.Location = new System.Drawing.Point(237, 38);
            this.chkAccounting.Name = "chkAccounting";
            this.chkAccounting.Size = new System.Drawing.Size(215, 27);
            this.chkAccounting.TabIndex = 0;
            this.chkAccounting.Text = "حسابداری";
            this.chkAccounting.UseVisualStyleBackColor = true;
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(25, 201);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(142, 18);
            this.label14.TabIndex = 8;
            this.label14.Text = "پایان قرارداد :";
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(25, 159);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(142, 18);
            this.label13.TabIndex = 6;
            this.label13.Text = "شروع قرارداد :";
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(25, 113);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(142, 18);
            this.label12.TabIndex = 4;
            this.label12.Text = "ویرایش برنامه :";
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(25, 70);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(142, 18);
            this.label11.TabIndex = 2;
            this.label11.Text = "تعداد کاربر همزمان :";
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEndDate.Location = new System.Drawing.Point(173, 199);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(239, 26);
            this.dtpEndDate.TabIndex = 9;
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStartDate.Location = new System.Drawing.Point(173, 157);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(239, 26);
            this.dtpStartDate.TabIndex = 7;
            // 
            // cmbEdition
            // 
            this.cmbEdition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEdition.FormattingEnabled = true;
            this.cmbEdition.Items.AddRange(new object[] {
            "Standard",
            "Professional",
            "Enterprise"});
            this.cmbEdition.Location = new System.Drawing.Point(173, 113);
            this.cmbEdition.Name = "cmbEdition";
            this.cmbEdition.Size = new System.Drawing.Size(239, 26);
            this.cmbEdition.TabIndex = 5;
            // 
            // spnUserCount
            // 
            this.spnUserCount.Location = new System.Drawing.Point(173, 70);
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
            this.spnUserCount.Size = new System.Drawing.Size(97, 26);
            this.spnUserCount.TabIndex = 3;
            this.spnUserCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.btnCancel.Location = new System.Drawing.Point(138, 533);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(110, 36);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "انصراف";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnSaveLicense
            // 
            this.btnSaveLicense.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSaveLicense.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSaveLicense.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.btnSaveLicense.Location = new System.Drawing.Point(12, 533);
            this.btnSaveLicense.Name = "btnSaveLicense";
            this.btnSaveLicense.Size = new System.Drawing.Size(120, 36);
            this.btnSaveLicense.TabIndex = 11;
            this.btnSaveLicense.Text = "ذخیره مجوز";
            this.btnSaveLicense.UseVisualStyleBackColor = true;
            this.btnSaveLicense.Click += new System.EventHandler(this.SaveLicenseButton_Click);
            // 
            // cmbCustomer
            // 
            this.cmbCustomer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCustomer.FormattingEnabled = true;
            this.cmbCustomer.Location = new System.Drawing.Point(173, 19);
            this.cmbCustomer.Name = "cmbCustomer";
            this.cmbCustomer.Size = new System.Drawing.Size(307, 26);
            this.cmbCustomer.TabIndex = 1;
            // 
            // lblCustomer
            // 
            this.lblCustomer.AutoSize = true;
            this.lblCustomer.Location = new System.Drawing.Point(25, 22);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new System.Drawing.Size(110, 18);
            this.lblCustomer.TabIndex = 0;
            this.lblCustomer.Text = "انتخاب مشتری :";
            // 
            // chkIsActivated
            // 
            this.chkIsActivated.Enabled = false;
            this.chkIsActivated.Location = new System.Drawing.Point(28, 480);
            this.chkIsActivated.Name = "chkIsActivated";
            this.chkIsActivated.Size = new System.Drawing.Size(215, 27);
            this.chkIsActivated.TabIndex = 13;
            this.chkIsActivated.Text = "فعالسازی شده";
            this.chkIsActivated.UseVisualStyleBackColor = true;
            // 
            // LicenseEditor
            // 
            this.AcceptButton = this.btnSaveLicense;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(494, 577);
            this.Controls.Add(this.chkIsActivated);
            this.Controls.Add(this.cmbCustomer);
            this.Controls.Add(this.lblCustomer);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSaveLicense);
            this.Controls.Add(this.dtpEndDate);
            this.Controls.Add(this.dtpStartDate);
            this.Controls.Add(this.cmbEdition);
            this.Controls.Add(this.spnUserCount);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.grpSubsystems);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LicenseEditor";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ایجاد یا ویرایش مجوز برنامه";
            this.grpSubsystems.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spnUserCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpSubsystems;
        private System.Windows.Forms.CheckBox chkBudgeting;
        private System.Windows.Forms.CheckBox chkWarehousing;
        private System.Windows.Forms.CheckBox chkSales;
        private System.Windows.Forms.CheckBox chkPurchase;
        private System.Windows.Forms.CheckBox chkInventory;
        private System.Windows.Forms.CheckBox chkPersonnel;
        private System.Windows.Forms.CheckBox chkWagePayment;
        private System.Windows.Forms.CheckBox chkCashFlow;
        private System.Windows.Forms.CheckBox chkCheque;
        private System.Windows.Forms.CheckBox chkAccounting;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.ComboBox cmbEdition;
        private System.Windows.Forms.NumericUpDown spnUserCount;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSaveLicense;
        private System.Windows.Forms.ComboBox cmbCustomer;
        private System.Windows.Forms.Label lblCustomer;
        private System.Windows.Forms.CheckBox chkIsActivated;
    }
}