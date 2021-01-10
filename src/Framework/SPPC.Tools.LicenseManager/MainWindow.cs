using System;
using System.Configuration;
using System.IO;
using System.Windows.Forms;
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
            _service = new LicenseService(new ServiceClient(Constants.OnlineServerRoot));
        }

        public LicenseModel License { get; set; }

        public CustomerModel Customer { get; set; }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            SetupBindings();
            LoadDefaults();
        }

        private void SaveCustomer_Click(object sender, EventArgs e)
        {
            if (!SaveCustomer())
            {
                return;
            }

            this.Cursor = Cursors.WaitCursor;
            string error = _service.InsertCustomer(Customer);
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
            string error = _service.InsertLicense(License);
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
            string json = JsonHelper.From(instance);
            File.WriteAllText(path, json);
            CreateApiServiceLicense();
            MessageBox.Show(this, "شناسه برنامه با موفقیت ثبت شد.",
                "عملیات موفق", MessageBoxButtons.OK, MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
        }

        private void Cancel_Click(object sender, EventArgs e)
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
                selected = selected | Subsystems.Accounting;
            }

            if (chkCheque.Checked)
            {
                selected = selected | Subsystems.Cheque;
            }

            if (chkCashFlow.Checked)
            {
                selected = selected | Subsystems.CashFlow;
            }

            if (chkWagePayment.Checked)
            {
                selected = selected | Subsystems.WagePayment;
            }

            if (chkPersonnel.Checked)
            {
                selected = selected | Subsystems.Personnel;
            }

            if (chkInventory.Checked)
            {
                selected = selected | Subsystems.Inventory;
            }

            if (chkPurchase.Checked)
            {
                selected = selected | Subsystems.Purchase;
            }

            if (chkSales.Checked)
            {
                selected = selected | Subsystems.Sales;
            }

            if (chkWarehousing.Checked)
            {
                selected = selected | Subsystems.Warehousing;
            }

            if (chkBudgeting.Checked)
            {
                selected = selected | Subsystems.Budgeting;
            }

            return (int)selected;
        }

        private void CreateApiServiceLicense()
        {
            var path = ConfigurationManager.AppSettings["WebApiLicensePath"];
            var copy = License.GetCopy();
            var ignored = new string[]
            {
                "Id", "CustomerId", "CustomerKey", "LicenseKey", "HardwareKey",
                "ClientKey", "Secret", "Customer", "RowGuid", "ModifiedDate", "IsActivated"
            };
            var json = JsonHelper.From(copy, true, ignored);
            File.WriteAllText(path, json);
            return;
        }

        private readonly ILicenseService _service;
    }
}
