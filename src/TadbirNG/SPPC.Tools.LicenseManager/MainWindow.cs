using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Windows.Forms;
using SPPC.Framework.Cryptography;
using SPPC.Framework.Helpers;
using SPPC.Framework.Service;
using SPPC.Licensing.Model;
using SPPC.Licensing.Service;
using SPPC.Tools.LicenseManager.Properties;
using SPPC.Tools.Model;
using SPPC.Tools.Transforms.Templates;
using SPPC.Tools.Utility;

namespace SPPC.Tools.LicenseManager
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
            _crypto = new CryptoService(new CertificateManager());
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
            if (grdLicenses.SelectedRows.Count == 0)
            {
                return;
            }

            if (!ValidateSaveInstance())
            {
                return;
            }

            var license = grdLicenses.SelectedRows[0].DataBoundItem as LicenseModel;
            string instance = GetInstanceKey(license);
            if (CreateClientInstance(instance))
            {
                string path = ConfigurationManager.AppSettings["InstanceIdPath"];
                if (File.Exists(path))
                {
                    string folder = Path.GetDirectoryName(path);
                    File.Move(path, Path.Combine(folder, "_instance.id"));
                }

                File.WriteAllText(path, instance);
                var customer = CustomerService.GetCustomer(license.CustomerId);
                CreateApiServiceLicense(license, customer);

                MessageBox.Show(this, "شناسه برنامه با موفقیت ثبت شد.",
                    "عملیات موفق", MessageBoxButtons.OK, MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
            }
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

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private static bool HasModule(Subsystems modules, Subsystems module)
        {
            return (modules & module) != 0;
        }

        private static void CreateApiServiceLicense(LicenseModel license, CustomerModel customer)
        {
            var path = ConfigurationManager.AppSettings["WebApiLicensePath"];
            var devPath = String.Format("{0}.Development.json", path);
            var licenseData = GetLicenseData(license, customer);
            var json = JsonHelper.From(licenseData);
            File.WriteAllText(path, json);
            if (!File.Exists(devPath))
            {
                File.WriteAllText(devPath, json);
            }
        }

        private static LicenseViewModel GetLicenseData(LicenseModel license, CustomerModel customer)
        {
            return new LicenseViewModel()
            {
                CustomerName = customer.CompanyName,
                ContactName = String.Format("{0} {1}", customer.ContactFirstName, customer.ContactLastName),
                Edition = license.Edition,
                UserCount = license.UserCount,
                ActiveModules = license.ActiveModules,
                StartDate = license.StartDate,
                EndDate = license.EndDate
            };
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
                    : (int?)null;
                grdLicenses.DataSource = null;
                grdLicenses.DataSource = LicenseService.GetLicenses(customerId);
                Cursor = Cursors.Default;
            }
        }

        private string GetActiveModules(int activeModules)
        {
            var modules = new List<string>();
            var modulesFlags = (Subsystems)activeModules;
            if (HasModule(modulesFlags, Subsystems.Accounting))
            {
                modules.Add(Resources.Accounting);
            }

            if (HasModule(modulesFlags, Subsystems.Budgeting))
            {
                modules.Add(Resources.Budgeting);
            }

            if (HasModule(modulesFlags, Subsystems.CashFlow))
            {
                modules.Add(Resources.CashFlow);
            }

            if (HasModule(modulesFlags, Subsystems.Cheque))
            {
                modules.Add(Resources.Cheque);
            }

            if (HasModule(modulesFlags, Subsystems.Inventory))
            {
                modules.Add(Resources.Inventory);
            }

            if (HasModule(modulesFlags, Subsystems.Personnel))
            {
                modules.Add(Resources.Personnel);
            }

            if (HasModule(modulesFlags, Subsystems.Purchase))
            {
                modules.Add(Resources.Purchase);
            }

            if (HasModule(modulesFlags, Subsystems.Sales))
            {
                modules.Add(Resources.Sales);
            }

            if (HasModule(modulesFlags, Subsystems.WagePayment))
            {
                modules.Add(Resources.WagePayment);
            }

            if (HasModule(modulesFlags, Subsystems.Warehousing))
            {
                modules.Add(Resources.Warehousing);
            }

            return String.Join("، ", modules.ToArray());
        }

        private string GetInstanceKey(LicenseModel license)
        {
            var instance = new InstanceModel()
            {
                CustomerKey = license.CustomerKey,
                LicenseKey = license.LicenseKey
            };
            return _crypto.Encrypt(JsonHelper.From(instance, false));
        }

        private bool CreateClientInstance(string instance)
        {
            var editor = new InstanceInfoEditor()
            {
                Instance = new ClientInstanceModel()
                {
                    Key = instance,
                    Version = VersionInfo.GetAppVersion()
                }
            };
            var result = editor.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                string path = ConfigurationManager.AppSettings["ClientInstanceIdPath"];
                var template = new TsInstanceFromValues(editor.Instance);
                File.WriteAllText(path, template.TransformText());
            }

            return result == DialogResult.OK;
        }

        private bool ValidateSaveInstance()
        {
            if (grdLicenses.SelectedRows.Count == 0)
            {
                MessageBox.Show(this, "مجوزی انتخاب نشده است.", "ثبت شناسه برنامه", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                return false;
            }

            var response = MessageBox.Show(
                this, "این عملیات پیش از ایجاد نسخه برای کاربر انجام می شود. آیا از ادامه عملیات اطمینان دارید؟",
                "تأیید عملیات", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2,
                MessageBoxOptions.RtlReading);

            return response == DialogResult.Yes;
        }

        private ICustomerService _customerService;
        private ILicenseService _licenseService;
        private readonly ICryptoService _crypto;
    }
}
