using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;
using SPPC.Framework.Helpers;
using SPPC.Tools.Utility;

namespace SPPC.Tadbir.WinRunner
{
    public partial class InstallerForm : Form
    {
        public InstallerForm()
        {
            InitializeComponent();
            _runner = new CliRunner();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            var servers = GetDbServers();
            servers.Insert(0, "Docker");
            cmbDbServer.DataSource = servers;
            txtInstallPath.Text = @"C:\SPPC\TadbirNG";
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

        private void Install_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtInstallPath.Text))
            {
                MessageBox.Show("لطفاً مسیر نصب برنامه را انتخاب کنید.",
                    "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.RtlReading);
                return;
            }

            txtConsole.Focus();
            _runner.OutputReceived += Runner_OutputReceived;
            SetDbServer();
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

            string suffix = InstallerUtility.GetCustomerSuffix();
            if (String.IsNullOrEmpty(suffix))
            {
                worker.ReportProgress(0, "بروز خطا هنگام دانلود سرویس های برنامه");
                return;
            }

            worker.ReportProgress(0, "دانلود سرویس های برنامه...");
            _runner.Run(String.Format(PullLicenseTemplate, suffix));
            worker.ReportProgress(20);
            _runner.Run(String.Format(PullApiTemplate, suffix));
            worker.ReportProgress(20);
            _runner.Run(String.Format(PullAppTemplate, suffix));
            worker.ReportProgress(20);
            _runner.Run("docker pull msn1368/db-server:dev");
            worker.ReportProgress(20);

            worker.ReportProgress(0, "تکمیل مراحل پایانی نصب...");
            if (chkCreateShortcut.Checked)
            {
                string exePath = Path.Combine(txtInstallPath.Text, "runner", "SPPC.Tadbir.WinRunner.exe");
                InstallerUtility.CreateDesktopShortcut(exePath, "سیستم جدید تدبیر", "سیستم جدید تدبیر");
            }
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnInstall.Enabled = true;
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
                lblStatus.Text = e.UserState.ToString();
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
        public static List<string> GetDbServers()
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

        private void SetDbServer()
        {
            _dbServer = cmbDbServer.SelectedItem.ToString();
            if (_dbServer == "Docker")
            {
                _dbServer = "DbServer";
            }
        }

        private const string PullLicenseTemplate = "docker pull msn1368/license-server-{0}:dev";
        private const string PullApiTemplate = "docker pull msn1368/api-server-{0}:latest";
        private const string PullAppTemplate = "docker pull msn1368/web-app-{0}:dev";
        private readonly CliRunner _runner;
        private TimeSpan _elapsed;
        private string _dbServer;
    }
}
