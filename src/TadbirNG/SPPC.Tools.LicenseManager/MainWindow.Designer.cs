
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
            this.tabCustomers = new System.Windows.Forms.TabPage();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnDisplayCustomers = new System.Windows.Forms.Button();
            this.btnCustomerLicenses = new System.Windows.Forms.Button();
            this.btnEditCustomer = new System.Windows.Forms.Button();
            this.btnNewCustomer = new System.Windows.Forms.Button();
            this.grdCustomers = new System.Windows.Forms.DataGridView();
            this.tabLicenses = new System.Windows.Forms.TabPage();
            this.btnSaveDockerInstance = new System.Windows.Forms.Button();
            this.btnSaveInstance = new System.Windows.Forms.Button();
            this.btnDeleteLicense = new System.Windows.Forms.Button();
            this.btnEditLicense = new System.Windows.Forms.Button();
            this.btnNewLicense = new System.Windows.Forms.Button();
            this.cmbCustomer = new System.Windows.Forms.ComboBox();
            this.lblCustomer = new System.Windows.Forms.Label();
            this.grdLicenses = new System.Windows.Forms.DataGridView();
            this.tabMain.SuspendLayout();
            this.tabCustomers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdCustomers)).BeginInit();
            this.tabLicenses.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdLicenses)).BeginInit();
            this.SuspendLayout();
            // 
            // tabMain
            // 
            this.tabMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabMain.Controls.Add(this.tabCustomers);
            this.tabMain.Controls.Add(this.tabLicenses);
            this.tabMain.Location = new System.Drawing.Point(12, 12);
            this.tabMain.Name = "tabMain";
            this.tabMain.RightToLeftLayout = true;
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(758, 529);
            this.tabMain.TabIndex = 0;
            this.tabMain.SelectedIndexChanged += new System.EventHandler(this.MainTab_SelectedIndexChanged);
            // 
            // tabCustomers
            // 
            this.tabCustomers.Controls.Add(this.btnExit);
            this.tabCustomers.Controls.Add(this.btnDisplayCustomers);
            this.tabCustomers.Controls.Add(this.btnCustomerLicenses);
            this.tabCustomers.Controls.Add(this.btnEditCustomer);
            this.tabCustomers.Controls.Add(this.btnNewCustomer);
            this.tabCustomers.Controls.Add(this.grdCustomers);
            this.tabCustomers.Location = new System.Drawing.Point(4, 27);
            this.tabCustomers.Name = "tabCustomers";
            this.tabCustomers.Padding = new System.Windows.Forms.Padding(3);
            this.tabCustomers.Size = new System.Drawing.Size(750, 498);
            this.tabCustomers.TabIndex = 0;
            this.tabCustomers.Text = "مشتریان";
            this.tabCustomers.UseVisualStyleBackColor = true;
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.Location = new System.Drawing.Point(6, 455);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(111, 34);
            this.btnExit.TabIndex = 5;
            this.btnExit.Text = "خروج";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // btnDisplayCustomers
            // 
            this.btnDisplayCustomers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDisplayCustomers.Location = new System.Drawing.Point(633, 455);
            this.btnDisplayCustomers.Name = "btnDisplayCustomers";
            this.btnDisplayCustomers.Size = new System.Drawing.Size(111, 34);
            this.btnDisplayCustomers.TabIndex = 1;
            this.btnDisplayCustomers.Text = "نمایش";
            this.btnDisplayCustomers.UseVisualStyleBackColor = true;
            this.btnDisplayCustomers.Click += new System.EventHandler(this.DisplayCustomersButton_Click);
            // 
            // btnCustomerLicenses
            // 
            this.btnCustomerLicenses.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCustomerLicenses.Location = new System.Drawing.Point(224, 455);
            this.btnCustomerLicenses.Name = "btnCustomerLicenses";
            this.btnCustomerLicenses.Size = new System.Drawing.Size(147, 34);
            this.btnCustomerLicenses.TabIndex = 4;
            this.btnCustomerLicenses.Text = "مجوزهای مشتری";
            this.btnCustomerLicenses.UseVisualStyleBackColor = true;
            this.btnCustomerLicenses.Click += new System.EventHandler(this.CustomerLicensesButton_Click);
            // 
            // btnEditCustomer
            // 
            this.btnEditCustomer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditCustomer.Location = new System.Drawing.Point(377, 455);
            this.btnEditCustomer.Name = "btnEditCustomer";
            this.btnEditCustomer.Size = new System.Drawing.Size(111, 34);
            this.btnEditCustomer.TabIndex = 3;
            this.btnEditCustomer.Text = "ویرایش";
            this.btnEditCustomer.UseVisualStyleBackColor = true;
            this.btnEditCustomer.Click += new System.EventHandler(this.EditCustomerButton_Click);
            // 
            // btnNewCustomer
            // 
            this.btnNewCustomer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNewCustomer.Location = new System.Drawing.Point(494, 455);
            this.btnNewCustomer.Name = "btnNewCustomer";
            this.btnNewCustomer.Size = new System.Drawing.Size(133, 34);
            this.btnNewCustomer.TabIndex = 2;
            this.btnNewCustomer.Text = "مشتری جدید";
            this.btnNewCustomer.UseVisualStyleBackColor = true;
            this.btnNewCustomer.Click += new System.EventHandler(this.NewCustomerButton_Click);
            // 
            // grdCustomers
            // 
            this.grdCustomers.AllowUserToAddRows = false;
            this.grdCustomers.AllowUserToDeleteRows = false;
            this.grdCustomers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdCustomers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdCustomers.Location = new System.Drawing.Point(6, 6);
            this.grdCustomers.MultiSelect = false;
            this.grdCustomers.Name = "grdCustomers";
            this.grdCustomers.ReadOnly = true;
            this.grdCustomers.RowHeadersVisible = false;
            this.grdCustomers.RowHeadersWidth = 51;
            this.grdCustomers.RowTemplate.Height = 24;
            this.grdCustomers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdCustomers.Size = new System.Drawing.Size(738, 434);
            this.grdCustomers.TabIndex = 0;
            this.grdCustomers.ColumnAdded += new System.Windows.Forms.DataGridViewColumnEventHandler(this.CustomersGrid_ColumnAdded);
            // 
            // tabLicenses
            // 
            this.tabLicenses.Controls.Add(this.btnSaveDockerInstance);
            this.tabLicenses.Controls.Add(this.btnSaveInstance);
            this.tabLicenses.Controls.Add(this.btnDeleteLicense);
            this.tabLicenses.Controls.Add(this.btnEditLicense);
            this.tabLicenses.Controls.Add(this.btnNewLicense);
            this.tabLicenses.Controls.Add(this.cmbCustomer);
            this.tabLicenses.Controls.Add(this.lblCustomer);
            this.tabLicenses.Controls.Add(this.grdLicenses);
            this.tabLicenses.Location = new System.Drawing.Point(4, 27);
            this.tabLicenses.Name = "tabLicenses";
            this.tabLicenses.Padding = new System.Windows.Forms.Padding(3);
            this.tabLicenses.Size = new System.Drawing.Size(750, 498);
            this.tabLicenses.TabIndex = 1;
            this.tabLicenses.Text = "مجوزها";
            this.tabLicenses.UseVisualStyleBackColor = true;
            // 
            // btnSaveDockerInstance
            // 
            this.btnSaveDockerInstance.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveDockerInstance.Location = new System.Drawing.Point(95, 454);
            this.btnSaveDockerInstance.Name = "btnSaveDockerInstance";
            this.btnSaveDockerInstance.Size = new System.Drawing.Size(150, 34);
            this.btnSaveDockerInstance.TabIndex = 7;
            this.btnSaveDockerInstance.Text = "ثبت شناسه داکر";
            this.btnSaveDockerInstance.UseVisualStyleBackColor = true;
            this.btnSaveDockerInstance.Click += new System.EventHandler(this.SaveDockerInstance_Click);
            // 
            // btnSaveInstance
            // 
            this.btnSaveInstance.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveInstance.Location = new System.Drawing.Point(251, 454);
            this.btnSaveInstance.Name = "btnSaveInstance";
            this.btnSaveInstance.Size = new System.Drawing.Size(150, 34);
            this.btnSaveInstance.TabIndex = 6;
            this.btnSaveInstance.Text = "ثبت شناسه برنامه";
            this.btnSaveInstance.UseVisualStyleBackColor = true;
            this.btnSaveInstance.Click += new System.EventHandler(this.SaveInstanceButton_Click);
            // 
            // btnDeleteLicense
            // 
            this.btnDeleteLicense.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteLicense.Location = new System.Drawing.Point(407, 454);
            this.btnDeleteLicense.Name = "btnDeleteLicense";
            this.btnDeleteLicense.Size = new System.Drawing.Size(102, 34);
            this.btnDeleteLicense.TabIndex = 5;
            this.btnDeleteLicense.Text = "حذف";
            this.btnDeleteLicense.UseVisualStyleBackColor = true;
            this.btnDeleteLicense.Click += new System.EventHandler(this.DeleteLicenseButton_Click);
            // 
            // btnEditLicense
            // 
            this.btnEditLicense.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditLicense.Location = new System.Drawing.Point(515, 454);
            this.btnEditLicense.Name = "btnEditLicense";
            this.btnEditLicense.Size = new System.Drawing.Size(108, 34);
            this.btnEditLicense.TabIndex = 4;
            this.btnEditLicense.Text = "ویرایش";
            this.btnEditLicense.UseVisualStyleBackColor = true;
            this.btnEditLicense.Click += new System.EventHandler(this.EditLicenseButton_Click);
            // 
            // btnNewLicense
            // 
            this.btnNewLicense.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNewLicense.Location = new System.Drawing.Point(629, 454);
            this.btnNewLicense.Name = "btnNewLicense";
            this.btnNewLicense.Size = new System.Drawing.Size(115, 34);
            this.btnNewLicense.TabIndex = 3;
            this.btnNewLicense.Text = "مجوز جدید";
            this.btnNewLicense.UseVisualStyleBackColor = true;
            this.btnNewLicense.Click += new System.EventHandler(this.NewLicenseButton_Click);
            // 
            // cmbCustomer
            // 
            this.cmbCustomer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbCustomer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCustomer.FormattingEnabled = true;
            this.cmbCustomer.Location = new System.Drawing.Point(251, 8);
            this.cmbCustomer.Name = "cmbCustomer";
            this.cmbCustomer.Size = new System.Drawing.Size(372, 26);
            this.cmbCustomer.TabIndex = 1;
            this.cmbCustomer.SelectedIndexChanged += new System.EventHandler(this.CustomerCombo_SelectedIndexChanged);
            // 
            // lblCustomer
            // 
            this.lblCustomer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCustomer.AutoSize = true;
            this.lblCustomer.Location = new System.Drawing.Point(637, 11);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new System.Drawing.Size(110, 18);
            this.lblCustomer.TabIndex = 0;
            this.lblCustomer.Text = "انتخاب مشتری :";
            // 
            // grdLicenses
            // 
            this.grdLicenses.AllowUserToAddRows = false;
            this.grdLicenses.AllowUserToDeleteRows = false;
            this.grdLicenses.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdLicenses.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdLicenses.Location = new System.Drawing.Point(6, 45);
            this.grdLicenses.MultiSelect = false;
            this.grdLicenses.Name = "grdLicenses";
            this.grdLicenses.ReadOnly = true;
            this.grdLicenses.RowHeadersVisible = false;
            this.grdLicenses.RowHeadersWidth = 51;
            this.grdLicenses.RowTemplate.Height = 24;
            this.grdLicenses.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdLicenses.Size = new System.Drawing.Size(738, 394);
            this.grdLicenses.TabIndex = 2;
            this.grdLicenses.ColumnAdded += new System.Windows.Forms.DataGridViewColumnEventHandler(this.LicensesGrid_ColumnAdded);
            this.grdLicenses.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.LicensesGrid_DataBindingComplete);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(782, 553);
            this.Controls.Add(this.tabMain);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Name = "MainWindow";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ابزار مدیریت مجوزهای تدبیر - پردازش موازی سامان";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.tabMain.ResumeLayout(false);
            this.tabCustomers.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdCustomers)).EndInit();
            this.tabLicenses.ResumeLayout(false);
            this.tabLicenses.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdLicenses)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabCustomers;
        private System.Windows.Forms.TabPage tabLicenses;
        private System.Windows.Forms.DataGridView grdCustomers;
        private System.Windows.Forms.Button btnCustomerLicenses;
        private System.Windows.Forms.Button btnEditCustomer;
        private System.Windows.Forms.Button btnNewCustomer;
        private System.Windows.Forms.Button btnDeleteLicense;
        private System.Windows.Forms.Button btnEditLicense;
        private System.Windows.Forms.Button btnNewLicense;
        private System.Windows.Forms.ComboBox cmbCustomer;
        private System.Windows.Forms.Label lblCustomer;
        private System.Windows.Forms.DataGridView grdLicenses;
        private System.Windows.Forms.Button btnSaveInstance;
        private System.Windows.Forms.Button btnDisplayCustomers;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnSaveDockerInstance;
    }
}