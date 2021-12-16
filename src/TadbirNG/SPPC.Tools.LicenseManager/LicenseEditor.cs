using System;
using System.Configuration;
using System.Windows.Forms;
using SPPC.Framework.Service;
using SPPC.Licensing.Model;
using SPPC.Licensing.Service;

namespace SPPC.Tools.LicenseManager
{
    public partial class LicenseEditor : Form
    {
        public LicenseEditor()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            LoadCustomers();
            SetupBindings();
            SetSubsystems();
        }

        public LicenseModel License { get; set; }

        private ICustomerService CustomerService
        {
            get
            {
                if (_customerService == null)
                {
                    string root = ConfigurationManager.AppSettings["OnlineServerRoot"];
                    _customerService = new CustomerService(new ServiceClient(root));
                }

                return _customerService;
            }
        }

        private void SaveLicenseButton_Click(object sender, EventArgs e)
        {
            if (!SaveLicense())
            {
                return;
            }

            Close();
        }

        private void LoadCustomers()
        {
            Cursor = Cursors.WaitCursor;
            cmbCustomer.DataSource = CustomerService.GetCustomerLookup();
            cmbCustomer.DisplayMember = "Value";
            cmbCustomer.ValueMember = "Key";
            Cursor = Cursors.Default;
        }

        private void SetupBindings()
        {
            spnUserCount.DataBindings.Add("Value", License, "UserCount");
            spnOfflineLimit.DataBindings.Add("Value", License, "OfflineLimit");
            dtpStartDate.DataBindings.Add("Value", License, "StartDate");
            dtpEndDate.DataBindings.Add("Value", License, "EndDate");
            cmbEdition.DataBindings.Add("SelectedItem", License, "Edition");
            chkIsActivated.DataBindings.Add("Checked", License, "IsActivated");
        }

        private bool SaveLicense()
        {
            License.ActiveModules = GetSubsystems();
            if (!ValidateLicense())
            {
                return false;
            }

            License.CustomerId = Int32.Parse(cmbCustomer.SelectedValue.ToString());
            if (String.IsNullOrEmpty(License.LicenseKey))
            {
                License.LicenseKey = Guid.NewGuid().ToString();
            }

            return true;
        }

        private bool ValidateLicense()
        {
            // Validate License...
            if (cmbCustomer.SelectedIndex == -1)
            {
                string message = String.Format("مشتری انتخاب نشده است.");
                MessageBox.Show(this, message, "پیغام خطا", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.RtlReading);
            }
            if (License.ActiveModules == (int)Subsystems.None)
            {
                string message = String.Format("زیرسیستمی انتخاب نشده است.");
                MessageBox.Show(this, message, "پیغام خطا", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.RtlReading);
            }

            return true;
        }

        private void SetSubsystems()
        {
            cmbCustomer.SelectedValue = License.CustomerId.ToString();
            var modules = (Subsystems)License.ActiveModules;
            chkAccounting.Checked = HasModule(modules, Subsystems.Accounting);
            chkCheque.Checked = HasModule(modules, Subsystems.Cheque);
            chkCashFlow.Checked = HasModule(modules, Subsystems.CashFlow);
            chkWagePayment.Checked = HasModule(modules, Subsystems.WagePayment);
            chkPersonnel.Checked = HasModule(modules, Subsystems.Personnel);
            chkInventory.Checked = HasModule(modules, Subsystems.Inventory);
            chkPurchase.Checked = HasModule(modules, Subsystems.Purchase);
            chkSales.Checked = HasModule(modules, Subsystems.Sales);
            chkWarehousing.Checked = HasModule(modules, Subsystems.Warehousing);
            chkBudgeting.Checked = HasModule(modules, Subsystems.Budgeting);
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

        private bool HasModule(Subsystems modules, Subsystems module)
        {
            return (modules & module) != 0;
        }

        private ICustomerService _customerService;
    }
}
