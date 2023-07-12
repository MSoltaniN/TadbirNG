using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using SPPC.Framework.Extensions;
using SPPC.Framework.Helpers;
using SPPC.Framework.Persistence;
using SPPC.Framework.Service;
using SPPC.Licensing.Model;
using SPPC.Tadbir.Configuration;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.ViewModel.Auth;
using SPPC.Tools.Model;

namespace SPPC.Tools.SystemDesigner.Forms
{
    public partial class LoginUtilityForm : Form
    {
        public LoginUtilityForm()
        {
            InitializeComponent();
            _apiClient = new ServiceClient(_apiServiceUrl);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Cursor = Cursors.WaitCursor;
            TryGetAdminLicense();
            LoadUsers();
            Cursor = Cursors.Default;
        }

        private void ShowPass_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = chkShowPass.Checked
                ? '\0'
                : '*';
        }

        private void UserName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbUserName.SelectedIndex != -1)
            {
                var userId = Convert.ToInt32(cmbUserName.SelectedValue);
                var adminPassword = SysParameterUtility.AllParameters.Db.AdminPassword;
                txtPassword.Text = userId == AppConstants.AdminUserId
                    ? adminPassword
                    : String.Empty;
                txtPassword.ReadOnly = txtPassword.Text == adminPassword;
            }
        }

        private void Company_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            LoadFiscalPeriods();
            LoadBranches();
            Cursor = Cursors.Default;
        }

        private void AppLogin_Click(object sender, EventArgs e)
        {
            if (!VerifyLoginParameters())
            {
                return;
            }

            try
            {
                Cursor = Cursors.WaitCursor;
                var selected = cmbUserName.SelectedItem as UserInfo;
                var login = new LoginViewModel()
                {
                    UserName = selected.UserName,
                    Password = txtPassword.Text
                };
                var response = _apiClient.Update(login, "users/login");
                var ticket = response.Headers[AppConstants.ContextHeaderName];
                _apiClient.AddHeader(AppConstants.ContextHeaderName, ticket);
                txtTicket.Text = ticket;

                var companies = _apiClient.Get<IList<KeyValue>>($"lookup/companies/user/{selected.UserID}");
                cmbCompany.DisplayMember = "Value";
                cmbCompany.ValueMember = "Key";
                cmbCompany.DataSource = companies;
                btnAppLogin.Enabled = false;
                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                var message = $@"
Invalid user name or password.

More info :
{ex.Message}";
                Cursor = Cursors.Default;
                MessageBox.Show(this, message, "Login Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void CompanyLogin_Click(object sender, EventArgs e)
        {
            if (!VerifyCompanyLoginParameters())
            {
                return;
            }

            try
            {
                var login = new CompanyLoginViewModel()
                {
                    CompanyId = Convert.ToInt32(cmbCompany.SelectedValue),
                    FiscalPeriodId = Convert.ToInt32(cmbFiscalPeriod.SelectedValue),
                    BranchId = Convert.ToInt32(cmbBranch.SelectedValue)
                };
                var response = _apiClient.Update(login, "users/login/company");
                txtTicket.Text = response.Headers[AppConstants.ContextHeaderName];
            }
            catch (Exception ex)
            {
                var message = $@"
Could not login to selected environment.

More info :
{ex.Message}";
                Cursor = Cursors.Default;
                MessageBox.Show(this, message, "Login Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void TryGetAdminLicense()
        {
            var license = _serverLicense;
            if (!chkConnectToServer.Checked)
            {
                using var apiClient = new ServiceClient(_licenseServiceUrl);
                try
                {
                    var instanceKey = GetCurrentInstance();
                    apiClient.AddHeader(LicenseConstants.InstanceHeaderName, instanceKey);
                    license = apiClient.Get<string>("license/users/1");
                    apiClient.Delete("sessions/current");
                    _apiClient.AddHeader(LicenseConstants.LicenseHeaderName, license);
                }
                catch
                {
                    Cursor = Cursors.Default;
                    MessageBox.Show(this, "Please make sure API and License servers are running.", "Login Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private static string GetCurrentInstance()
        {
            var path = Path.Combine(PathConfig.WebEnvRoot, "environment.ts");
            var keyLine = File.ReadAllLines(path)
                .Where(line => line.Trim().StartsWith("InstanceKey"))
                .First();
            return keyLine
                .Trim()
                .Replace("InstanceKey: ", String.Empty)
                .Replace("\"", String.Empty)
                .Replace("'", String.Empty)
                .Replace(",", String.Empty);
        }

        private void LoadUsers()
        {
            var dal = new SqlDataLayer(DbConnections.SystemConnection);
            var result = dal.Query(ToolsQuery.UsersLookup);
            var users = result.Rows
                .Cast<DataRow>()
                .Select(row => new UserInfo()
                {
                    UserID = row.ValueOrDefault<int>("UserID"),
                    UserName = row.ValueOrDefault("UserName"),
                    FullName = row.ValueOrDefault("FullName")
                })
                .ToList();
            cmbUserName.DisplayMember = "FullName";
            cmbUserName.ValueMember = "UserID";
            cmbUserName.DataSource = users;
        }

        private void LoadFiscalPeriods()
        {
            int userId = Convert.ToInt32(cmbUserName.SelectedValue);
            int companyId = Convert.ToInt32(cmbCompany.SelectedValue);
            var fiscalPeriods = _apiClient.Get<IList<KeyValue>>($"lookup/fps/company/{companyId}/user/{userId}");
            cmbFiscalPeriod.DisplayMember = "Value";
            cmbFiscalPeriod.ValueMember = "Key";
            cmbFiscalPeriod.DataSource = fiscalPeriods;
        }

        private void LoadBranches()
        {
            int userId = Convert.ToInt32(cmbUserName.SelectedValue);
            int companyId = Convert.ToInt32(cmbCompany.SelectedValue);
            var branches = _apiClient.Get<IList<KeyValue>>($"lookup/branches/company/{companyId}/user/{userId}");
            cmbBranch.DisplayMember = "Value";
            cmbBranch.ValueMember = "Key";
            cmbBranch.DataSource = branches;
        }

        private bool VerifyLoginParameters()
        {
            if (cmbUserName.SelectedIndex == -1)
            {
                MessageBox.Show(this, "Please select a user.", "Login Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cmbUserName.Focus();
                return false;
            }

            if (String.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show(this, "Please enter correct password for selected user.", "Login Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPassword.Focus();
                return false;
            }

            return true;
        }

        private bool VerifyCompanyLoginParameters()
        {
            if (cmbCompany.SelectedIndex == -1)
            {
                MessageBox.Show(this, "Please select a company.", "Login Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cmbCompany.Focus();
                return false;
            }

            if (cmbFiscalPeriod.SelectedIndex == -1)
            {
                MessageBox.Show(this, "Please select a fiscal period.", "Login Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cmbFiscalPeriod.Focus();
                return false;
            }

            if (cmbBranch.SelectedIndex == -1)
            {
                MessageBox.Show(this, "Please select a branch.", "Login Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cmbBranch.Focus();
                return false;
            }

            return true;
        }

        private const string _apiServiceUrl = "http://localhost:8801";
        private const string _licenseServiceUrl = "http://localhost:7473";
        private const string _serverLicense = @"a5xEk1TYbysQyUu85fZWFTMySyCNE8CT3G/isBD9Xu1EPUQs4loI0OnhLJoD03YlX2z8W+KJRi4PP9xIvteCrRKpIN5B3i/rl4gF9u6jycKwFhUK7d+FtQcho6Wh6KY5V4jzhJsj1MuNpyI5rb7Fh8XeyLOIuzekBa/Zd3rvYwJjt2/K8QmlJ6OlUtfJCpLocSmE8fDwGjKSbvPYnFNkbUdLaS6eB8gtCOzLiNScc/9CbtJYxUpsCFXmybjB+gCM/YaF6jzY78ErNXSkZGa4wSJTGK9hs7rFyLpslpjNTsp2wDfoNQmySX7cIkfuqjqhOBi7QpTjiwxNRqca7FKJrg==";
        private readonly IApiClient _apiClient;
    }
}
