using System;
using System.Windows.Forms;
using SPPC.Tools.Model;

namespace SPPC.Tools.LicenseManager
{
    public partial class InstanceInfoEditor : Form
    {
        public InstanceInfoEditor()
        {
            InitializeComponent();
        }

        public ClientInstanceModel Instance { get; set; }

        public IBuildSettings BuildSettings { get; set; }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            SetupBindings();
            chkShowPassword.Checked = false;
        }

        private void SaveInstanceButton_Click(object sender, EventArgs e)
        {
            if (!ValidateInstance())
            {
                return;
            }

            Close();
        }

        private void SetupBindings()
        {
            txtBaseUrl.DataBindings.Add("Text", BuildSettings, "WebApiUrl");
            txtServerUrl.DataBindings.Add("Text", BuildSettings, "LocalServerRoot");
            txtOnlineServerUrl.DataBindings.Add("Text", BuildSettings, "OnlineServerRoot");
            txtUserName.DataBindings.Add("Text", BuildSettings, "Ssh.User");
            txtPassword.DataBindings.Add("Text", BuildSettings, "Ssh.Password");
            txtInstanceKey.DataBindings.Add("Text", BuildSettings, "Key");
            txtVersion.DataBindings.Add("Text", BuildSettings, "Version");
        }

        private bool ValidateInstance()
        {
            if (!EnsureNotEmpty(txtBaseUrl, "آدرس سرویس وب"))
            {
                return false;
            }

            if (!EnsureNotEmpty(txtServerUrl, "آدرس سرور آفلاین مجوزها"))
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

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = chkShowPassword.Checked ? '\0' : '*';
        }
    }
}
