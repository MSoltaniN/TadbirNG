﻿using System;
using System.Configuration;
using System.IO;
using System.Windows.Forms;
using SPPC.Framework.Cryptography;
using SPPC.Framework.Helpers;
using SPPC.Framework.Service;
using SPPC.Licensing.Model;
using SPPC.Licensing.Service;

namespace SPPC.Tools.LicenseManager
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
            License = new LicenseModel();
            Customer = new CustomerModel();
            _crypto = new CryptoService();
        }

        public LicenseModel License { get; set; }

        public CustomerModel Customer { get; set; }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            SetupBindings();
            LoadDefaults();
        }

        private ILicenseService Service
        {
            get
            {
                if (_service == null)
                {
                    string root = ConfigurationManager.AppSettings["OnlineServerRoot"];
                    _service = new LicenseService(new ServiceClient(root));
                }

                return _service;
            }
        }

        private void SaveCustomer_Click(object sender, EventArgs e)
        {
            if (!SaveCustomer())
            {
                return;
            }

            this.Cursor = Cursors.WaitCursor;
            string error = Service.InsertCustomer(Customer);
            if (!String.IsNullOrEmpty(error))
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(this, error, "بروز خطا", MessageBoxButtons.OK,
                    MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                return;
            }

            this.Cursor = Cursors.Default;
            MessageBox.Show(this, "مشتری با موفقیت ذخیره شد.", "عملیات موفق", MessageBoxButtons.OK,
                MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
        }

        private void SaveLicense_Click(object sender, EventArgs e)
        {
            if (!SaveLicense())
            {
                return;
            }

            this.Cursor = Cursors.WaitCursor;
            string error = Service.InsertLicense(License);
            if (!String.IsNullOrEmpty(error))
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(this, error, "بروز خطا", MessageBoxButtons.OK,
                    MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                return;
            }

            this.Cursor = Cursors.Default;
            MessageBox.Show(this, "مجوز تدبیر با موفقیت ذخیره شد.", "عملیات موفق", MessageBoxButtons.OK,
                MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
        }

        private void SaveInstance_Click(object sender, EventArgs e)
        {
            string path = ConfigurationManager.AppSettings["InstanceIdPath"];
            var instance = new InstanceModel()
            {
                CustomerKey = License.CustomerKey,
                LicenseKey = License.LicenseKey
            };
            string instanceData = _crypto.Encrypt(JsonHelper.From(instance));
            File.WriteAllText(path, instanceData);
            CreateApiServiceLicense();
            MessageBox.Show(this, "شناسه برنامه با موفقیت ثبت شد.",
                "عملیات موفق", MessageBoxButtons.OK, MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SetupBindings()
        {
            // Customer bindings
            txtCompanyName.DataBindings.Add("Text", Customer, "CompanyName");
            txtHqAddress.DataBindings.Add("Text", Customer, "HeadquartersAddress");
            txtFirstName.DataBindings.Add("Text", Customer, "ContactFirstName");
            txtLastName.DataBindings.Add("Text", Customer, "ContactLastName");
            txtWorkPhone.DataBindings.Add("Text", Customer, "WorkPhone");
            txtWorkFax.DataBindings.Add("Text", Customer, "WorkFax");
            txtCellPhone.DataBindings.Add("Text", Customer, "CellPhone");

            // License bindings
            spnUserCount.DataBindings.Add("Value", License, "UserCount");
            dtpStartDate.DataBindings.Add("Value", License, "StartDate");
            dtpEndDate.DataBindings.Add("Value", License, "EndDate");
        }

        private void LoadDefaults()
        {

            cmbEdition.SelectedIndex = 0;
            cmbEmployeeCount.SelectedIndex = 3;
            cmbIndustry.SelectedIndex = 1;
            chkAccounting.Checked = true;
        }

        private bool SaveCustomer()
        {
            if (!ValidateCustomer())
            {
                return false;
            }

            Customer.CustomerKey = Guid.NewGuid().ToString();
            Customer.Industry = cmbIndustry.SelectedItem.ToString();
            Customer.EmployeeCount = cmbEmployeeCount.SelectedItem.ToString();

            return true;
        }

        private bool SaveLicense()
        {
            if (!ValidateLicense())
            {
                return false;
            }

            License.CustomerKey = Customer.CustomerKey;
            License.LicenseKey = Guid.NewGuid().ToString();
            License.Edition = cmbEdition.SelectedItem.ToString();
            return true;
        }

        private bool ValidateCustomer()
        {
            // Validate Customer...
            if (!EnsureNotEmpty(txtCompanyName, "نام شرکت"))
            {
                return false;
            }

            if (!EnsureNotEmpty(txtHqAddress, "نشانی"))
            {
                return false;
            }

            if (!EnsureNotEmpty(txtFirstName, "نام"))
            {
                return false;
            }

            if (!EnsureNotEmpty(txtLastName, "نام خانوادگی"))
            {
                return false;
            }

            if (!EnsureNotEmpty(txtWorkFax, "شماره نمابر"))
            {
                return false;
            }

            if (!EnsureNotEmpty(txtCellPhone, "شماره همراه"))
            {
                return false;
            }

            return true;
        }

        private bool ValidateLicense()
        {
            // Validate License...
            License.ActiveModules = GetSubsystems();
            if (License.ActiveModules == (int)Subsystems.None)
            {
                string message = String.Format("زیرسیستمی انتخاب نشده است.");
                MessageBox.Show(this, message, "پیغام خطا", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.RtlReading);
            }

            return true;
        }

        private bool EnsureNotEmpty(TextBox textBox, string field)
        {
            bool isValid = true;
            string template = "وارد کردن {0} اجباری است.";
            if (String.IsNullOrWhiteSpace(textBox.Text))
            {
                string message = String.Format(template, field);
                MessageBox.Show(this, message, "پیغام خطا", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.RtlReading);
                isValid = false;
            }

            return isValid;
        }

        private int GetSubsystems()
        {
            var selected = Subsystems.None;
            if (chkAccounting.Checked)
            {
                selected |= Subsystems.Accounting;
            }

            if (chkCheque.Checked)
            {
                selected |= Subsystems.Cheque;
            }

            if (chkCashFlow.Checked)
            {
                selected |= Subsystems.CashFlow;
            }

            if (chkWagePayment.Checked)
            {
                selected |= Subsystems.WagePayment;
            }

            if (chkPersonnel.Checked)
            {
                selected |= Subsystems.Personnel;
            }

            if (chkInventory.Checked)
            {
                selected |= Subsystems.Inventory;
            }

            if (chkPurchase.Checked)
            {
                selected |= Subsystems.Purchase;
            }

            if (chkSales.Checked)
            {
                selected |= Subsystems.Sales;
            }

            if (chkWarehousing.Checked)
            {
                selected |= Subsystems.Warehousing;
            }

            if (chkBudgeting.Checked)
            {
                selected |= Subsystems.Budgeting;
            }

            return (int)selected;
        }

        private void CreateApiServiceLicense()
        {
            var path = ConfigurationManager.AppSettings["WebApiLicensePath"];
            var license = GetLicenseData();
            var json = JsonHelper.From(license);
            File.WriteAllText(path, json);
            return;
        }

        private LicenseViewModel GetLicenseData()
        {
            return new LicenseViewModel()
            {
                CustomerName = Customer.CompanyName,
                Edition = License.Edition,
                UserCount = License.UserCount,
                ActiveModules = License.ActiveModules,
                StartDate = License.StartDate,
                EndDate = License.EndDate
            };
        }

        private readonly ICryptoService _crypto;
        private ILicenseService _service;
    }
}
