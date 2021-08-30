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
            LoadDefaults();
        }

        public LicenseModel License { get; set; }

        public string CustomerKey { get; set; }

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
            dtpStartDate.DataBindings.Add("Value", License, "StartDate");
            dtpEndDate.DataBindings.Add("Value", License, "EndDate");
        }

        private void LoadDefaults()
        {
            cmbCustomer.SelectedValue = CustomerKey;
            cmbEdition.SelectedIndex = 0;
            chkAccounting.Checked = true;
        }

        private bool SaveLicense()
        {
            if (!ValidateLicense())
            {
                return false;
            }

            License.CustomerKey = CustomerKey;
            License.LicenseKey = Guid.NewGuid().ToString();
            License.Edition = cmbEdition.SelectedItem.ToString();
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

        private ICustomerService _customerService;

        private void CustomerCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCustomer.SelectedIndex != -1)
            {
                CustomerKey = cmbCustomer.SelectedItem.ToString();
            }
        }
    }
}
