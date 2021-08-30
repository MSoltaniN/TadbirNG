using System;
using System.Configuration;
using System.Windows.Forms;
using SPPC.Framework.Service;
using SPPC.Licensing.Service;

namespace SPPC.Tools.LicenseManager
{
    public partial class CustomerList : Form
    {
        public CustomerList()
        {
            InitializeComponent();
        }

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

        private ILicenseService LicenseService
        {
            get
            {
                if (_licenseService == null)
                {
                    string root = ConfigurationManager.AppSettings["OnlineServerRoot"];
                    _licenseService = new LicenseService(new ServiceClient(root));
                }

                return _licenseService;
            }
        }

        private void NewCustomerButton_Click(object sender, EventArgs e)
        {
            var editor = new CustomerEditor();
            if (editor.ShowDialog(this) == DialogResult.OK)
            {
                Cursor = Cursors.WaitCursor;
                string error = CustomerService.InsertCustomer(editor.Customer);
                if (!String.IsNullOrEmpty(error))
                {
                    Cursor = Cursors.Default;
                    MessageBox.Show(this, error, "بروز خطا", MessageBoxButtons.OK,
                        MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                    return;
                }

                Cursor = Cursors.Default;
                MessageBox.Show(this, "مشتری با موفقیت ذخیره شد.", "عملیات موفق", MessageBoxButtons.OK,
                    MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
            }
        }

        private void EditCustomerButton_Click(object sender, EventArgs e)
        {
            var editor = new CustomerEditor();
            if (editor.ShowDialog(this) == DialogResult.OK)
            {
                MessageBox.Show(this, "Customer was edited.", "Success", MessageBoxButtons.OK,
                    MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
            }
        }

        private void CustomerLicensesButton_Click(object sender, EventArgs e)
        {

        }

        private void NewLicenseButton_Click(object sender, EventArgs e)
        {
            var editor = new LicenseEditor();
            if (editor.ShowDialog(this) == DialogResult.OK)
            {
                Cursor = Cursors.WaitCursor;
                string error = LicenseService.InsertLicense(editor.License);
                if (!String.IsNullOrEmpty(error))
                {
                    Cursor = Cursors.Default;
                    MessageBox.Show(this, error, "بروز خطا", MessageBoxButtons.OK,
                        MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                    return;
                }

                Cursor = Cursors.Default;
                MessageBox.Show(this, "مجوز تدبیر با موفقیت ذخیره شد.", "عملیات موفق", MessageBoxButtons.OK,
                    MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
            }
        }

        private void EditLicenseButton_Click(object sender, EventArgs e)
        {
            var editor = new LicenseEditor();
            if (editor.ShowDialog(this) == DialogResult.OK)
            {
                MessageBox.Show(this, "License was edited.", "Success", MessageBoxButtons.OK,
                    MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
            }
        }

        private void DeleteLicenseButton_Click(object sender, EventArgs e)
        {
            var response = MessageBox.Show(this, "Are you sure?", "Confirmation", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading);
            if (response == DialogResult.Yes)
            {
                MessageBox.Show(this, "License was deleted.", "Success", MessageBoxButtons.OK,
                    MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
            }
        }

        private void SaveInstanceButton_Click(object sender, EventArgs e)
        {

        }

        private void CustomerCombo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private ICustomerService _customerService;
        private ILicenseService _licenseService;
    }
}
