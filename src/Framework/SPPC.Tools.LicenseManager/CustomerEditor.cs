using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SPPC.Licensing.Model;

namespace SPPC.Tools.LicenseManager
{
    public partial class CustomerEditor : Form
    {
        public CustomerEditor()
        {
            InitializeComponent();
        }

        public CustomerModel Customer { get; set; }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            SetupBindings();
            //LoadDefaults();
        }

        private void SaveCustomerButton_Click(object sender, EventArgs e)
        {
            if (!SaveCustomer())
            {
                return;
            }

            Close();
        }

        private void SetupBindings()
        {
            txtCompanyName.DataBindings.Add("Text", Customer, "CompanyName");
            txtHqAddress.DataBindings.Add("Text", Customer, "HeadquartersAddress");
            txtFirstName.DataBindings.Add("Text", Customer, "ContactFirstName");
            txtLastName.DataBindings.Add("Text", Customer, "ContactLastName");
            txtWorkPhone.DataBindings.Add("Text", Customer, "WorkPhone");
            txtWorkFax.DataBindings.Add("Text", Customer, "WorkFax");
            txtCellPhone.DataBindings.Add("Text", Customer, "CellPhone");
            cmbIndustry.DataBindings.Add("SelectedItem", Customer, "Industry");
            cmbEmployeeCount.DataBindings.Add("SelectedItem", Customer, "EmployeeCount");
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

        private bool ValidateCustomer()
        {
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
    }
}
