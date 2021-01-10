namespace SPPC.Tools.LicenseManager
{
    partial class MainWindow
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
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabCustomer = new System.Windows.Forms.TabPage();
            this.grpContact = new System.Windows.Forms.GroupBox();
            this.txtCellPhone = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtWorkFax = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtWorkPhone = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.grpCompany = new System.Windows.Forms.GroupBox();
            this.cmbEmployeeCount = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtHqAddress = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbIndustry = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCompanyName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabLicense = new System.Windows.Forms.TabPage();
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
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.label14 = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.label13 = new System.Windows.Forms.Label();
            this.cmbEdition = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.spnUserCount = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.btnSaveCustomer = new System.Windows.Forms.Button();
            this.btnSaveLicense = new System.Windows.Forms.Button();
            this.btnSaveInstance = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tabMain.SuspendLayout();
            this.tabCustomer.SuspendLayout();
            this.grpContact.SuspendLayout();
            this.grpCompany.SuspendLayout();
            this.tabLicense.SuspendLayout();
            this.grpSubsystems.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spnUserCount)).BeginInit();
            this.SuspendLayout();
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.tabCustomer);
            this.tabMain.Controls.Add(this.tabLicense);
            this.tabMain.Location = new System.Drawing.Point(8, 2);
            this.tabMain.Name = "tabMain";
            this.tabMain.RightToLeftLayout = true;
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(513, 600);
            this.tabMain.TabIndex = 0;
            // 
            // tabCustomer
            // 
            this.tabCustomer.Controls.Add(this.grpContact);
            this.tabCustomer.Controls.Add(this.grpCompany);
            this.tabCustomer.Location = new System.Drawing.Point(4, 27);
            this.tabCustomer.Name = "tabCustomer";
            this.tabCustomer.Padding = new System.Windows.Forms.Padding(3);
            this.tabCustomer.Size = new System.Drawing.Size(505, 569);
            this.tabCustomer.TabIndex = 0;
            this.tabCustomer.Text = "مشتری";
            this.tabCustomer.UseVisualStyleBackColor = true;
            // 
            // grpContact
            // 
            this.grpContact.Controls.Add(this.txtCellPhone);
            this.grpContact.Controls.Add(this.label9);
            this.grpContact.Controls.Add(this.txtWorkFax);
            this.grpContact.Controls.Add(this.label8);
            this.grpContact.Controls.Add(this.txtWorkPhone);
            this.grpContact.Controls.Add(this.label7);
            this.grpContact.Controls.Add(this.txtLastName);
            this.grpContact.Controls.Add(this.label6);
            this.grpContact.Controls.Add(this.txtFirstName);
            this.grpContact.Controls.Add(this.label5);
            this.grpContact.Location = new System.Drawing.Point(6, 299);
            this.grpContact.Name = "grpContact";
            this.grpContact.Size = new System.Drawing.Size(493, 258);
            this.grpContact.TabIndex = 1;
            this.grpContact.TabStop = false;
            this.grpContact.Text = "مسئول خرید";
            // 
            // txtCellPhone
            // 
            this.txtCellPhone.Location = new System.Drawing.Point(13, 223);
            this.txtCellPhone.Name = "txtCellPhone";
            this.txtCellPhone.Size = new System.Drawing.Size(327, 26);
            this.txtCellPhone.TabIndex = 11;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(382, 226);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(99, 18);
            this.label9.TabIndex = 10;
            this.label9.Text = "شماره همراه :";
            // 
            // txtWorkFax
            // 
            this.txtWorkFax.Location = new System.Drawing.Point(13, 181);
            this.txtWorkFax.Name = "txtWorkFax";
            this.txtWorkFax.Size = new System.Drawing.Size(327, 26);
            this.txtWorkFax.TabIndex = 9;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(382, 184);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(99, 18);
            this.label8.TabIndex = 8;
            this.label8.Text = "شماره نمابر :";
            // 
            // txtWorkPhone
            // 
            this.txtWorkPhone.Location = new System.Drawing.Point(13, 137);
            this.txtWorkPhone.Name = "txtWorkPhone";
            this.txtWorkPhone.Size = new System.Drawing.Size(327, 26);
            this.txtWorkPhone.TabIndex = 7;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(346, 141);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(135, 18);
            this.label7.TabIndex = 6;
            this.label7.Text = "شماره مستقیم :";
            // 
            // txtLastName
            // 
            this.txtLastName.Location = new System.Drawing.Point(13, 93);
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(327, 26);
            this.txtLastName.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(382, 97);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(99, 18);
            this.label6.TabIndex = 4;
            this.label6.Text = "نام خانوادگی :";
            // 
            // txtFirstName
            // 
            this.txtFirstName.Location = new System.Drawing.Point(13, 51);
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(327, 26);
            this.txtFirstName.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(382, 54);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(99, 18);
            this.label5.TabIndex = 2;
            this.label5.Text = "نام :";
            // 
            // grpCompany
            // 
            this.grpCompany.Controls.Add(this.cmbEmployeeCount);
            this.grpCompany.Controls.Add(this.label4);
            this.grpCompany.Controls.Add(this.txtHqAddress);
            this.grpCompany.Controls.Add(this.label3);
            this.grpCompany.Controls.Add(this.cmbIndustry);
            this.grpCompany.Controls.Add(this.label2);
            this.grpCompany.Controls.Add(this.txtCompanyName);
            this.grpCompany.Controls.Add(this.label1);
            this.grpCompany.Location = new System.Drawing.Point(6, 7);
            this.grpCompany.Name = "grpCompany";
            this.grpCompany.Size = new System.Drawing.Size(493, 286);
            this.grpCompany.TabIndex = 0;
            this.grpCompany.TabStop = false;
            this.grpCompany.Text = "مشخصات شرکت";
            // 
            // cmbEmployeeCount
            // 
            this.cmbEmployeeCount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEmployeeCount.FormattingEnabled = true;
            this.cmbEmployeeCount.Items.AddRange(new object[] {
            "کمتر از ده نفر",
            "ده تا بیست نفر",
            "بیست تا پنجاه نفر",
            "پنجاه تا صد نفر",
            "بیشتر از صد نفر"});
            this.cmbEmployeeCount.Location = new System.Drawing.Point(13, 132);
            this.cmbEmployeeCount.Name = "cmbEmployeeCount";
            this.cmbEmployeeCount.Size = new System.Drawing.Size(327, 26);
            this.cmbEmployeeCount.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(387, 140);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 18);
            this.label4.TabIndex = 4;
            this.label4.Text = "تعداد پرسنل :";
            // 
            // txtHqAddress
            // 
            this.txtHqAddress.AcceptsReturn = true;
            this.txtHqAddress.Location = new System.Drawing.Point(13, 176);
            this.txtHqAddress.Multiline = true;
            this.txtHqAddress.Name = "txtHqAddress";
            this.txtHqAddress.Size = new System.Drawing.Size(327, 101);
            this.txtHqAddress.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(346, 179);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(135, 18);
            this.label3.TabIndex = 6;
            this.label3.Text = "نشانی دفتر مرکزی :";
            // 
            // cmbIndustry
            // 
            this.cmbIndustry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbIndustry.FormattingEnabled = true;
            this.cmbIndustry.Items.AddRange(new object[] {
            "(--- سایر ---)",
            "آموزشی",
            "بازرگانی",
            "پالایشگاه نفت و گاز",
            "تولید فلز خام",
            "جنگلداری - چوب و کاغذ",
            "خدمات ارتباطی و پستی",
            "خدمات بهداشتی / پزشکی",
            "خدمات عمومی",
            "خدمات فرهنگی و رسانه ای",
            "خدمات مالی",
            "ساختمانی",
            "صنایع شیمیایی",
            "صنایع غذایی",
            "صنایع معدن",
            "کشاورزی",
            "ماهیگیری و ترابری دریایی",
            "مهندسی برق و مکانیک",
            "نساجی - چرم - لباس",
            "هتلداری و گردشگری"});
            this.cmbIndustry.Location = new System.Drawing.Point(13, 87);
            this.cmbIndustry.Name = "cmbIndustry";
            this.cmbIndustry.Size = new System.Drawing.Size(327, 26);
            this.cmbIndustry.Sorted = true;
            this.cmbIndustry.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(387, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "دامنه کاری :";
            // 
            // txtCompanyName
            // 
            this.txtCompanyName.Location = new System.Drawing.Point(13, 45);
            this.txtCompanyName.Name = "txtCompanyName";
            this.txtCompanyName.Size = new System.Drawing.Size(327, 26);
            this.txtCompanyName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(387, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "نام شرکت :";
            // 
            // tabLicense
            // 
            this.tabLicense.Controls.Add(this.grpSubsystems);
            this.tabLicense.Controls.Add(this.dtpEndDate);
            this.tabLicense.Controls.Add(this.label14);
            this.tabLicense.Controls.Add(this.dtpStartDate);
            this.tabLicense.Controls.Add(this.label13);
            this.tabLicense.Controls.Add(this.cmbEdition);
            this.tabLicense.Controls.Add(this.label12);
            this.tabLicense.Controls.Add(this.spnUserCount);
            this.tabLicense.Controls.Add(this.label11);
            this.tabLicense.Location = new System.Drawing.Point(4, 27);
            this.tabLicense.Name = "tabLicense";
            this.tabLicense.Padding = new System.Windows.Forms.Padding(3);
            this.tabLicense.Size = new System.Drawing.Size(505, 569);
            this.tabLicense.TabIndex = 1;
            this.tabLicense.Text = "مجوز تدبیر";
            this.tabLicense.UseVisualStyleBackColor = true;
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
            this.grpSubsystems.Location = new System.Drawing.Point(19, 272);
            this.grpSubsystems.Name = "grpSubsystems";
            this.grpSubsystems.Size = new System.Drawing.Size(465, 279);
            this.grpSubsystems.TabIndex = 12;
            this.grpSubsystems.TabStop = false;
            this.grpSubsystems.Text = "زیرسیستم های فعال";
            // 
            // chkBudgeting
            // 
            this.chkBudgeting.Location = new System.Drawing.Point(12, 194);
            this.chkBudgeting.Name = "chkBudgeting";
            this.chkBudgeting.Size = new System.Drawing.Size(215, 27);
            this.chkBudgeting.TabIndex = 9;
            this.chkBudgeting.Text = "بودجه و اعتبارات";
            this.chkBudgeting.UseVisualStyleBackColor = true;
            // 
            // chkWarehousing
            // 
            this.chkWarehousing.Location = new System.Drawing.Point(12, 160);
            this.chkWarehousing.Name = "chkWarehousing";
            this.chkWarehousing.Size = new System.Drawing.Size(215, 27);
            this.chkWarehousing.TabIndex = 8;
            this.chkWarehousing.Text = "انبارداری";
            this.chkWarehousing.UseVisualStyleBackColor = true;
            // 
            // chkSales
            // 
            this.chkSales.Location = new System.Drawing.Point(12, 126);
            this.chkSales.Name = "chkSales";
            this.chkSales.Size = new System.Drawing.Size(215, 27);
            this.chkSales.TabIndex = 7;
            this.chkSales.Text = "فروش";
            this.chkSales.UseVisualStyleBackColor = true;
            // 
            // chkPurchase
            // 
            this.chkPurchase.Location = new System.Drawing.Point(12, 92);
            this.chkPurchase.Name = "chkPurchase";
            this.chkPurchase.Size = new System.Drawing.Size(215, 27);
            this.chkPurchase.TabIndex = 6;
            this.chkPurchase.Text = "خرید و تدارکات";
            this.chkPurchase.UseVisualStyleBackColor = true;
            // 
            // chkInventory
            // 
            this.chkInventory.Location = new System.Drawing.Point(12, 58);
            this.chkInventory.Name = "chkInventory";
            this.chkInventory.Size = new System.Drawing.Size(215, 27);
            this.chkInventory.TabIndex = 5;
            this.chkInventory.Text = "کنترل موجودی";
            this.chkInventory.UseVisualStyleBackColor = true;
            // 
            // chkPersonnel
            // 
            this.chkPersonnel.Location = new System.Drawing.Point(237, 194);
            this.chkPersonnel.Name = "chkPersonnel";
            this.chkPersonnel.Size = new System.Drawing.Size(215, 27);
            this.chkPersonnel.TabIndex = 4;
            this.chkPersonnel.Text = "پرسنلی";
            this.chkPersonnel.UseVisualStyleBackColor = true;
            // 
            // chkWagePayment
            // 
            this.chkWagePayment.Location = new System.Drawing.Point(237, 160);
            this.chkWagePayment.Name = "chkWagePayment";
            this.chkWagePayment.Size = new System.Drawing.Size(215, 27);
            this.chkWagePayment.TabIndex = 3;
            this.chkWagePayment.Text = "حقوق و دستمزد";
            this.chkWagePayment.UseVisualStyleBackColor = true;
            // 
            // chkCashFlow
            // 
            this.chkCashFlow.Location = new System.Drawing.Point(237, 126);
            this.chkCashFlow.Name = "chkCashFlow";
            this.chkCashFlow.Size = new System.Drawing.Size(215, 27);
            this.chkCashFlow.TabIndex = 2;
            this.chkCashFlow.Text = "خزانه داری - نقدینگی";
            this.chkCashFlow.UseVisualStyleBackColor = true;
            // 
            // chkCheque
            // 
            this.chkCheque.Location = new System.Drawing.Point(237, 92);
            this.chkCheque.Name = "chkCheque";
            this.chkCheque.Size = new System.Drawing.Size(215, 27);
            this.chkCheque.TabIndex = 1;
            this.chkCheque.Text = "خزانه داری - چک";
            this.chkCheque.UseVisualStyleBackColor = true;
            // 
            // chkAccounting
            // 
            this.chkAccounting.Location = new System.Drawing.Point(237, 58);
            this.chkAccounting.Name = "chkAccounting";
            this.chkAccounting.Size = new System.Drawing.Size(215, 27);
            this.chkAccounting.TabIndex = 0;
            this.chkAccounting.Text = "حسابداری";
            this.chkAccounting.UseVisualStyleBackColor = true;
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEndDate.Location = new System.Drawing.Point(114, 216);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(239, 26);
            this.dtpEndDate.TabIndex = 11;
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(363, 222);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(128, 18);
            this.label14.TabIndex = 10;
            this.label14.Text = "پایان قرارداد :";
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStartDate.Location = new System.Drawing.Point(114, 174);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(239, 26);
            this.dtpStartDate.TabIndex = 9;
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(363, 180);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(128, 18);
            this.label13.TabIndex = 8;
            this.label13.Text = "شروع قرارداد :";
            // 
            // cmbEdition
            // 
            this.cmbEdition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEdition.FormattingEnabled = true;
            this.cmbEdition.Items.AddRange(new object[] {
            "Standard",
            "Professional",
            "Enterprise"});
            this.cmbEdition.Location = new System.Drawing.Point(114, 130);
            this.cmbEdition.Name = "cmbEdition";
            this.cmbEdition.Size = new System.Drawing.Size(239, 26);
            this.cmbEdition.TabIndex = 7;
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(363, 134);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(128, 18);
            this.label12.TabIndex = 6;
            this.label12.Text = "ویرایش برنامه :";
            // 
            // spnUserCount
            // 
            this.spnUserCount.Location = new System.Drawing.Point(256, 89);
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
            this.spnUserCount.TabIndex = 5;
            this.spnUserCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(363, 91);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(128, 18);
            this.label11.TabIndex = 4;
            this.label11.Text = "تعداد کاربر همزمان :";
            // 
            // btnSaveCustomer
            // 
            this.btnSaveCustomer.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.btnSaveCustomer.Location = new System.Drawing.Point(8, 613);
            this.btnSaveCustomer.Name = "btnSaveCustomer";
            this.btnSaveCustomer.Size = new System.Drawing.Size(120, 40);
            this.btnSaveCustomer.TabIndex = 1;
            this.btnSaveCustomer.Text = "ذخیره مشتری";
            this.btnSaveCustomer.UseVisualStyleBackColor = true;
            this.btnSaveCustomer.Click += new System.EventHandler(this.SaveCustomer_Click);
            // 
            // btnSaveLicense
            // 
            this.btnSaveLicense.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.btnSaveLicense.Location = new System.Drawing.Point(136, 613);
            this.btnSaveLicense.Name = "btnSaveLicense";
            this.btnSaveLicense.Size = new System.Drawing.Size(127, 40);
            this.btnSaveLicense.TabIndex = 2;
            this.btnSaveLicense.Text = "ذخیره مجوز";
            this.btnSaveLicense.UseVisualStyleBackColor = true;
            this.btnSaveLicense.Click += new System.EventHandler(this.SaveLicense_Click);
            // 
            // btnSaveInstance
            // 
            this.btnSaveInstance.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.btnSaveInstance.Location = new System.Drawing.Point(271, 613);
            this.btnSaveInstance.Name = "btnSaveInstance";
            this.btnSaveInstance.Size = new System.Drawing.Size(155, 40);
            this.btnSaveInstance.TabIndex = 3;
            this.btnSaveInstance.Text = "ثبت شناسه برنامه";
            this.btnSaveInstance.UseVisualStyleBackColor = true;
            this.btnSaveInstance.Click += new System.EventHandler(this.SaveInstance_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.btnCancel.Location = new System.Drawing.Point(438, 613);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(83, 40);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "انصراف";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(529, 664);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSaveInstance);
            this.Controls.Add(this.btnSaveLicense);
            this.Controls.Add(this.btnSaveCustomer);
            this.Controls.Add(this.tabMain);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainWindow";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ابزار مدیریت مجوزهای تدبیر - پردازش موازی سامان";
            this.tabMain.ResumeLayout(false);
            this.tabCustomer.ResumeLayout(false);
            this.grpContact.ResumeLayout(false);
            this.grpContact.PerformLayout();
            this.grpCompany.ResumeLayout(false);
            this.grpCompany.PerformLayout();
            this.tabLicense.ResumeLayout(false);
            this.grpSubsystems.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spnUserCount)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabCustomer;
        private System.Windows.Forms.GroupBox grpCompany;
        private System.Windows.Forms.ComboBox cmbIndustry;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCompanyName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabLicense;
        private System.Windows.Forms.TextBox txtHqAddress;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbEmployeeCount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox grpContact;
        private System.Windows.Forms.TextBox txtCellPhone;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtWorkFax;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtWorkPhone;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtLastName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtFirstName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnSaveCustomer;
        private System.Windows.Forms.NumericUpDown spnUserCount;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cmbEdition;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label label13;
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
        private System.Windows.Forms.Button btnSaveLicense;
        private System.Windows.Forms.Button btnSaveInstance;
        private System.Windows.Forms.Button btnCancel;
    }
}

