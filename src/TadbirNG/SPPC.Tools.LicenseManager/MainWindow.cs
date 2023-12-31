﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Windows.Forms;
using SPPC.Framework.Common;
using SPPC.Framework.Helpers;
using SPPC.Framework.Service;
using SPPC.Framework.Utility;
using SPPC.Licensing.Model;
using SPPC.Licensing.Service;
using SPPC.Tadbir.Configuration.Models;
using SPPC.Tadbir.Utility;
using SPPC.Tadbir.Utility.Model;
using SPPC.Tadbir.Utility.Templates;
using SPPC.Tools.LicenseManager.Properties;
using SPPC.Tools.Model;
using SPPC.Tools.Transforms.Templates;

namespace SPPC.Tools.LicenseManager
{
    public partial class MainWindow : Form
    {
        public MainWindow()
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

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            RefreshCustomerList();
        }

        private void DisplayCustomersButton_Click(object sender, EventArgs e)
        {
            RefreshCustomerList();
        }

        private void NewCustomerButton_Click(object sender, EventArgs e)
        {
            var editor = new CustomerEditor
            {
                Customer = new CustomerModel()
            };
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
            if (grdCustomers.SelectedRows.Count == 0)
            {
                return;
            }

            var editor = new CustomerEditor
            {
                Customer = grdCustomers.SelectedRows[0].DataBoundItem as CustomerModel
            };
            if (editor.ShowDialog(this) == DialogResult.OK)
            {
                Cursor = Cursors.WaitCursor;
                string error = CustomerService.UpdateCustomer(editor.Customer);
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
                RefreshCustomerList();
            }
        }

        private void CustomerLicensesButton_Click(object sender, EventArgs e)
        {
            if (grdCustomers.SelectedRows.Count == 0)
            {
                return;
            }

            var customer = grdCustomers.SelectedRows[0].DataBoundItem as CustomerModel;
            tabMain.SelectedIndex = 1;
            cmbCustomer.SelectedValue = customer.Id.ToString();
        }

        private void NewLicenseButton_Click(object sender, EventArgs e)
        {
            int customerId = 0;
            if (cmbCustomer.SelectedIndex != -1)
            {
                customerId = Int32.Parse(cmbCustomer.SelectedValue.ToString());
            }

            var editor = new LicenseEditor()
            {
                License = new LicenseModel() { CustomerId = customerId }
            };
            if (editor.ShowDialog(this) == DialogResult.OK)
            {
                Cursor = Cursors.WaitCursor;
                var customer = CustomerService.GetCustomer(editor.License.CustomerId);
                editor.License.CustomerKey = customer.CustomerKey;
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
                RefreshLicenseList();
            }
        }

        private void EditLicenseButton_Click(object sender, EventArgs e)
        {
            if (grdLicenses.SelectedRows.Count == 0)
            {
                return;
            }

            var editor = new LicenseEditor
            {
                License = grdLicenses.SelectedRows[0].DataBoundItem as LicenseModel
            };
            if (editor.ShowDialog(this) == DialogResult.OK)
            {
                Cursor = Cursors.WaitCursor;
                string error = LicenseService.UpdateLicense(editor.License);
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
                RefreshLicenseList();
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

        private void SaveDockerInstance_Click(object sender, EventArgs e)
        {
        }

        private void CustomerCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshLicenseList();
        }

        private void CustomersGrid_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            var columnMap = new Dictionary<string, string>
            {
                { "CompanyName", Resources.CompanyName },
                { "Industry", Resources.Industry },
                { "EmployeeCount", Resources.EmployeeCount },
                { "HeadquartersAddress", Resources.HeadquartersAddress },
                { "ContactFirstName", Resources.ContactFirstName },
                { "ContactLastName", Resources.ContactLastName },
                { "WorkPhone", Resources.WorkPhone },
                { "WorkFax", Resources.WorkFax },
                { "CellPhone", Resources.CellPhone }
            };

            if (columnMap.ContainsKey(e.Column.Name))
            {
                e.Column.HeaderText = columnMap[e.Column.Name];
            }
            else
            {
                e.Column.Visible = false;
            }
        }

        private void LicensesGrid_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            var columnMap = new Dictionary<string, string>
            {
                { "UserCount", Resources.ActiveUserCount },
                { "Edition", Resources.AppEdition },
                { "StartDate", Resources.ContractStart },
                { "EndDate", Resources.ContractEnd },
                { "ActiveModules", Resources.ActiveSubsystems },
                { "IsActivated", Resources.IsActivated }
            };

            if (columnMap.ContainsKey(e.Column.Name))
            {
                e.Column.HeaderText = columnMap[e.Column.Name];
            }
            else
            {
                e.Column.Visible = false;
            }
        }

        private void LicensesGrid_DataBindingComplete(
            object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ////foreach (DataGridViewRow row in grdLicenses.Rows)
            ////{
            ////    int activeModules = Int32.Parse(row.Cells["ActiveModules"].Value.ToString());
            ////    row.Cells["ActiveModuleNames"].Value = GetActiveModules(activeModules);
            ////}
        }

        private void MainTab_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabMain.SelectedIndex == 1)
            {
                Cursor = Cursors.WaitCursor;
                var customers = CustomerService.GetCustomerLookup();
                customers.Insert(0, new KeyValue(null, Resources.AllCustomers));
                cmbCustomer.DisplayMember = "Value";
                cmbCustomer.ValueMember = "Key";
                cmbCustomer.DataSource = customers;
                Cursor = Cursors.Default;
            }
        }

        private void NewRelease_Click(object sender, EventArgs e)
        {
            if (grdLicenses.SelectedRows.Count == 0)
            {
                return;
            }

            var license = grdLicenses.SelectedRows[0].DataBoundItem as LicenseModel;
            license.Customer = grdCustomers.SelectedRows[0].DataBoundItem as CustomerModel;
            var form = new CreateRelease()
            {
                License = license
            };
            form.ShowDialog(this);
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void RefreshCustomerList()
        {
            Cursor = Cursors.WaitCursor;
            grdCustomers.DataSource = null;
            grdCustomers.DataSource = CustomerService.GetCustomers();
            Cursor = Cursors.Default;
        }

        private void RefreshLicenseList()
        {
            if (cmbCustomer.SelectedIndex != -1)
            {
                Cursor = Cursors.WaitCursor;
                var lookupItem = cmbCustomer.SelectedItem as KeyValue;
                int? customerId = lookupItem.Key != null
                    ? Int32.Parse(lookupItem.Key)
                    : null;
                grdLicenses.DataSource = null;
                grdLicenses.DataSource = LicenseService.GetLicenses(customerId);
                Cursor = Cursors.Default;
            }
        }

        private ICustomerService _customerService;
        private ILicenseService _licenseService;
    }
}
