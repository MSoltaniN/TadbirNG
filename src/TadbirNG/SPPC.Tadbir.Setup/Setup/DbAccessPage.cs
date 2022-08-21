using System;
using System.Windows.Forms;
using SPPC.Tadbir.Utility;

namespace SPPC.Tadbir.Setup
{
    public partial class DbAccessPage : UserControl, ISetupWizardPage
    {
        public DbAccessPage()
        {
            InitializeComponent();
        }

        public SetupWizardModel WizardModel { get; set; }

        public Func<bool> PageValidator
        {
            get { return ValidateSettings; }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            var servers = WinUtility.GetDbServers();
            servers.Insert(0, "Docker");
            cmbDbServer.DataSource = servers;
            chkShowPass.Checked = false;
            chkShowAdminPass.Checked = false;
            //txtAdminPassword.Enabled = cmbDbServer.SelectedItem?.ToString() != "Docker";
            //chkShowAdminPass.Enabled = cmbDbServer.SelectedItem?.ToString() != "Docker";
        }

        private void ShowPass_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = chkShowPass.Checked ? '\0' : '*';
        }

        private void ShowAdminPass_CheckedChanged(object sender, EventArgs e)
        {
            txtAdminPassword.PasswordChar = chkShowAdminPass.Checked ? '\0' : '*';
        }

        private void DbServer_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selected = cmbDbServer.SelectedItem?.ToString();
            if (selected == "Docker")
            {
                txtAdminPassword.Text = null;
                cmbLogin.Items.Clear();
                cmbLogin.Items.Add("Default");
                cmbLogin.SelectedIndex = 0;
            }
            else
            {
                cmbLogin.Items.Clear();
            }

            txtAdminPassword.Enabled = selected != "Docker";
            chkShowAdminPass.Enabled = selected != "Docker";
        }

        private void Login_DropDown(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(txtAdminPassword.Text))
            {
                Cursor = Cursors.WaitCursor;
                var server = cmbDbServer.SelectedItem?.ToString();
                cmbLogin.Items.Clear();
                var logins = SetupUtility.GetDbLoginNames(server, txtAdminPassword.Text);
                Cursor = Cursors.Default;
                if (logins.Length == 0)
                {
                    MessageBox.Show("رمز ورود راهبر نادرست است.",
                        "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.RtlReading);
                    return;
                }
                else
                {
                    cmbLogin.Items.AddRange(logins);
                }
            }
        }

        private void Login_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selected = cmbLogin.SelectedItem?.ToString();
            if (selected == "sa")
            {
                txtPassword.Text = txtAdminPassword.Text;
            }
            else
            {
                txtPassword.Text = null;
            }
        }

        private bool ValidateSettings()
        {
            BindValues();
            if (WizardModel.DbServer == null)
            {
                MessageBox.Show("لطفاً سرور دیتابیس را انتخاب کنید.",
                    "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.RtlReading);
                return false;
            }

            if (WizardModel.DbLogin == null)
            {
                MessageBox.Show("لطفاً کاربر دیتابیس را انتخاب کنید.",
                    "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.RtlReading);
                return false;
            }

            if (WizardModel.DbLogin != "Default" && String.IsNullOrWhiteSpace(WizardModel.DbPassword))
            {
                MessageBox.Show("لطفاً رمز ورود کاربر دیتابیس را وارد کنید.",
                    "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.RtlReading);
                return false;
            }

            return true;
        }

        private void BindValues()
        {
            WizardModel.DbServer = cmbDbServer.SelectedItem?.ToString();
            WizardModel.DbLogin = cmbLogin.SelectedItem?.ToString();
            WizardModel.DbPassword = txtPassword.Text;
        }
    }
}
