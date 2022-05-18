using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using SPPC.Framework.Helpers;
using SPPC.Tadbir.WinRunner.Utility;

namespace SPPC.Tadbir.WinRunner
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
            _runner = new CliRunner();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            var servers = InstallerUtility.GetDbServers();
            servers.Insert(0, "Docker");
            cmbDbServer.DataSource = servers;
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
            pullWorker.RunWorkerAsync();
        }

        private void StartService_Click(object sender, EventArgs e)
        {
            RunConsoleCommand("sc start \"sppckeysrv\"");
        }

        private void Pull_Click(object sender, EventArgs e)
        {
            txtConsole.Focus();
            txtConsole.Text = String.Empty;
            _runner.OutputReceived += Runner_OutputReceived;
            pullWorker.RunWorkerAsync();
        }

        private void StartContainers_Click(object sender, EventArgs e)
        {
            txtConsole.Focus();
            txtConsole.Text = String.Empty;
            _runner.OutputReceived += Runner_OutputReceived;
            startWorker.RunWorkerAsync();
            LaunchDefaultBrowser("http://localhost:9099");
        }

        private void PullWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            pullWorker.ReportProgress(0, "آماده سازی اولیه نصب برنامه...");
            InstallerUtility.CreateInstallationPath(txtInstallPath.Text);
            pullWorker.ReportProgress(2);

            pullWorker.ReportProgress(0, "کپی فایلهای مورد نیاز برنامه...");
            InstallerUtility.CopyFiles(txtInstallPath.Text, chkCreateShortcut.Checked);
            pullWorker.ReportProgress(8);

            pullWorker.ReportProgress(0, "نصب سرویس...");
            bool succeeded = InstallerUtility.InstallService(txtInstallPath.Text);
            if (!succeeded)
            {
                pullWorker.ReportProgress(0, "بروز خطا هنگام نصب سرویس");
                return;
            }

            pullWorker.ReportProgress(4);

            pullWorker.ReportProgress(0, "راه اندازی سرویس...");
            succeeded = InstallerUtility.RunService();
            if (!succeeded)
            {
                pullWorker.ReportProgress(0, "بروز خطا هنگام راه اندازی سرویس");
                return;
            }

            pullWorker.ReportProgress(6);

            string suffix = InstallerUtility.GetCustomerSuffix();
            if (String.IsNullOrEmpty(suffix))
            {
                pullWorker.ReportProgress(0, "بروز خطا هنگام دانلود سرویس های برنامه");
                return;
            }

            pullWorker.ReportProgress(0, "دانلود سرویس های برنامه...");
            _runner.Run(String.Format(PullLicenseTemplate, suffix));
            pullWorker.ReportProgress(20);
            _runner.Run(String.Format(PullApiTemplate, suffix));
            pullWorker.ReportProgress(20);
            _runner.Run(String.Format(PullAppTemplate, suffix));
            pullWorker.ReportProgress(20);
            _runner.Run("docker pull msn1368/db-server:dev");
            pullWorker.ReportProgress(20);

            pullWorker.ReportProgress(0, "تکمیل مراحل پایانی نصب...");
            if (!InstallerUtility.IsAppRegistered())
            {
                var version = InstallerUtility.GetAppVersion();
                if (version == null)
                {
                    pullWorker.ReportProgress(0, "بروز خطا هنگام تکمیل مراحل پایانی نصب");
                    return;
                }

                InstallerUtility.RegisterApplication(txtInstallPath.Text, _dbServer, version);
            }
        }

        private void PullWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnInstall.Enabled = true;
            btnExit.Enabled = true;
            lblStatus.Text = null;
            timer.Enabled = false;
            MessageBox.Show(this, "نصب برنامه با موفقیت انجام شد.", "تکمیل عملیات",
                MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1,
                MessageBoxOptions.RtlReading);
        }

        private void StartWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            _runner.Run("docker-compose -f ..\\..\\..\\src\\TadbirNG\\docker-compose.override.yml -f ..\\..\\..\\src\\TadbirNG\\docker-compose.yml down");
            _runner.Run("docker-compose -f ..\\..\\..\\src\\TadbirNG\\docker-compose.override.yml -f ..\\..\\..\\src\\TadbirNG\\docker-compose.yml up --no-build");
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState != null)
            {
                lblStatus.Text = e.UserState.ToString();
            }

            //txtConsole.AppendText(Environment.NewLine);
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

        private void SetDbServer()
        {
            _dbServer = cmbDbServer.SelectedItem.ToString();
            if (_dbServer == "Docker")
            {
                _dbServer = "DbServer";
            }
        }

        private static void LaunchDefaultBrowser(string url)
        {
            var runner = new CliRunner();
            string[] lines;
            do
            {
                Thread.Sleep(500);
                var output = runner.Run("docker ps -f \"name=WebApp\"");
                lines = output.Split(Environment.NewLine);
            } while (!lines.Any(line => line.Contains("WebApp")));
            Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
        }

        private void RunConsoleCommand(string command, bool append = false)
        {
            Cursor = Cursors.AppStarting;
            txtConsole.Focus();
            if (!append)
            {
                _builder.Clear();
            }

            _builder.Append(_runner.Run(command).Trim());
            _builder.AppendLine().AppendLine();
            txtConsole.Text = _builder.ToString();
            txtConsole.SelectionStart = txtConsole.Text.Length;
            Cursor = Cursors.Default;
        }

        private const string PullLicenseTemplate = "docker pull msn1368/license-server-{0}:dev";
        private const string PullApiTemplate = "docker pull msn1368/api-server-{0}:latest";
        private const string PullAppTemplate = "docker pull msn1368/web-app-{0}:dev";
        private readonly CliRunner _runner;
        private readonly StringBuilder _builder = new();
        private TimeSpan _elapsed;
        private string _dbServer;
    }
}
