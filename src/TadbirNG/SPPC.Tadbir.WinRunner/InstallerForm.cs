using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using SPPC.Framework.Helpers;
using SPPC.Tadbir.Domain;
using SPPC.Tools.Model;
using SPPC.Tools.Utility;

namespace SPPC.Tadbir.WinRunner
{
    public partial class InstallerForm : Form
    {
        public InstallerForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            var servers = GetDbServers();
            servers.Insert(0, "Docker");
            cmbDbServer.DataSource = servers;
            txtInstallPath.Text = @"C:\SPPC\TadbirNG";
            radGlobal.Checked = true;
            chkShowPass.Checked = false;
            chkShowAdminPass.Checked = false;
            txtAdminPassword.Enabled = cmbDbServer.SelectedItem?.ToString() != "Docker";
            chkShowAdminPass.Enabled = cmbDbServer.SelectedItem?.ToString() != "Docker";
        }

        private void Runner_OutputReceived(object sender, OutputReceivedEventArgs e)
        {
            if (txtConsole.InvokeRequired)
            {
                txtConsole.BeginInvoke((MethodInvoker)delegate ()
                {
                    if (!String.IsNullOrEmpty(e.Output))
                    {
                        txtConsole.AppendText(e.Output
                            .Replace("\n", Environment.NewLine)
                            .Trim());
                        txtConsole.AppendText(Environment.NewLine);
                        txtConsole.ScrollToCaret();
                    }
                });
            }
        }

        private void Global_CheckedChanged(object sender, EventArgs e)
        {
            txtDomain.Enabled = radGlobal.Checked;
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
                var logins = InstallerUtility.GetDbLoginNames(server, txtAdminPassword.Text);
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
            else if (selected == "Default")
            {
                txtPassword.Text = PasswordGenerator.Generate();
            }
            else
            {
                txtPassword.Text = null;
            }
        }

        private void Install_Click(object sender, EventArgs e)
        {
            if (!ValidateSettings())
            {
                return;
            }

            txtConsole.Focus();
            SetBuildSettings();
            btnInstall.Enabled = false;
            btnExit.Enabled = false;
            timer.Enabled = true;
            worker.RunWorkerAsync();
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            worker.ReportProgress(0, "آماده سازی اولیه نصب برنامه...");
            InstallerUtility.CreateInstallationPath(txtInstallPath.Text);
            worker.ReportProgress(2);

            worker.ReportProgress(0, "کپی فایلهای مورد نیاز برنامه...");
            InstallerUtility.CopyFiles(txtInstallPath.Text, chkCreateShortcut.Checked);
            worker.ReportProgress(8);

            worker.ReportProgress(0, "نصب سرویس...");
            bool succeeded = InstallerUtility.InstallService(txtInstallPath.Text);
            if (!succeeded)
            {
                worker.ReportProgress(0, "بروز خطا هنگام نصب سرویس");
                return;
            }

            worker.ReportProgress(4);

            worker.ReportProgress(0, "راه اندازی سرویس...");
            succeeded = InstallerUtility.RunService();
            if (!succeeded)
            {
                worker.ReportProgress(0, "بروز خطا هنگام راه اندازی سرویس");
                return;
            }

            worker.ReportProgress(6);

            var root = GetDockerImageRoot();
            worker.ReportProgress(0, "آماده سازی سرویس های برنامه...");
            InstallerUtility.ConfigureDockerService(root, DockerService.LicenseServer, _settings);
            worker.ReportProgress(20);
            InstallerUtility.ConfigureDockerService(root, DockerService.ApiServer, _settings);
            worker.ReportProgress(20);
            InstallerUtility.ConfigureDockerService(root, DockerService.WebApp, _settings);
            worker.ReportProgress(20);
            InstallerUtility.ConfigureDockerService(root, DockerService.DbServer, _settings);
            worker.ReportProgress(20);
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnExit.Enabled = true;
            lblStatus.Text = null;
            timer.Enabled = false;
            MessageBox.Show(this, "نصب برنامه با موفقیت انجام شد.", "تکمیل عملیات",
                MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1,
                MessageBoxOptions.RtlReading);
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState != null)
            {
                txtConsole.AppendText(e.UserState.ToString());
                txtConsole.AppendText(Environment.NewLine);
                txtConsole.AppendText(Environment.NewLine);
            }

            progress.Value += e.ProgressPercentage;
            lblProgress.Text = String.Format("{0}%", progress.Value);
        }

        private void Browse_Click(object sender, EventArgs e)
        {
            var browser = new FolderBrowserDialog()
            {
                Description = "پوشه نصب برنامه را انتخاب کنید",
                RootFolder = Environment.SpecialFolder.Desktop,
                ShowNewFolderButton = true,
                UseDescriptionForTitle = true
            };
            if (browser.ShowDialog(this) == DialogResult.OK)
            {
                txtInstallPath.Text = browser.SelectedPath;
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            var seconds = _elapsed.TotalSeconds;
            _elapsed = TimeSpan.FromSeconds(++seconds);
            lblElapsed.Text = _elapsed.ToString(@"hh\:mm\:ss");
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

#pragma warning disable CA1416 // Validate platform compatibility
        private static List<string> GetDbServers()
        {
            var servers = new List<string>();
            var key = Registry.LocalMachine.OpenSubKey(
                @"SOFTWARE\Microsoft\Microsoft SQL Server\Instance Names\SQL");
            if (key != null)
            {
                foreach (var value in key.GetValueNames())
                {
                    if (value == "MSSQLSERVER")
                    {
                        servers.Add(Environment.MachineName);
                    }
                    else
                    {
                        servers.Add(String.Format($"{Environment.MachineName}\\{value}"));
                    }
                }
            }

            return servers;
        }
#pragma warning restore CA1416 // Validate platform compatibility

        private bool ValidateSettings()
        {
            if (String.IsNullOrWhiteSpace(txtInstallPath.Text))
            {
                MessageBox.Show("لطفاً مسیر نصب برنامه را انتخاب کنید.",
                    "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.RtlReading);
                return false;
            }

            if (radGlobal.Checked && String.IsNullOrWhiteSpace(txtDomain.Text))
            {
                MessageBox.Show("لطفاً آدرس سرور را به صورت دامنه یا آی پی ثابت وارد کنید.",
                    "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.RtlReading);
                return false;
            }

            if (cmbDbServer.SelectedIndex == -1)
            {
                MessageBox.Show("لطفاً سرور دیتابیس را انتخاب کنید.",
                    "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.RtlReading);
                return false;
            }

            if (cmbLogin.SelectedIndex == -1)
            {
                MessageBox.Show("لطفاً کاربر دیتابیس را انتخاب کنید.",
                    "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.RtlReading);
                return false;
            }

            if (String.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("لطفاً رمز ورود کاربر دیتابیس را وارد کنید.",
                    "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.RtlReading);
                return false;
            }

            return true;
        }

        private void SetBuildSettings()
        {
            _settings = radGlobal.Checked ? BuildSettings.DockerNetwork : BuildSettings.DockerLocal;
            var dbServer = cmbDbServer.SelectedItem.ToString();
            if (dbServer == "Docker")
            {
                dbServer = DockerService.DbServer;
            }
            else
            {
                dbServer = dbServer.Replace(Environment.MachineName, BuildSettingValues.DockerHostInternalUrl);
            }

            _settings.DbServerName = dbServer;
            var dbLogin = cmbLogin.SelectedItem.ToString();
            if (dbLogin == "Default")
            {
                dbLogin = AppConstants.SystemLoginName;
            }

            _settings.DbUserName = dbLogin;
            _settings.DbPassword = txtPassword.Text;
            string host = txtDomain.Text;
            if (String.IsNullOrEmpty(txtDomain.Text))
            {
                host = "http://localhost";
            }

            if (!host.StartsWith("http://"))
            {
                host = String.Format($"http://{host}");
            }

            _settings.WebApiUrl = String.Format($"{host}:{BuildSettingValues.DefaultApiPort}");
            _settings.LocalServerUrl = String.Format($"{host}:{BuildSettingValues.DefaultLicenseApiPort}");
            _settings.Tcp.Domain = BuildSettingValues.DockerHostInternalUrl;
            _settings.Key = InstallerUtility.GetInstanceKey();
        }

        private static string GetDockerImageRoot()
        {
            string root = Path.GetDirectoryName(Environment.CurrentDirectory);
            return Path.Combine(root, "docker");
        }

        private TimeSpan _elapsed;
        private IBuildSettings _settings;
    }
}
