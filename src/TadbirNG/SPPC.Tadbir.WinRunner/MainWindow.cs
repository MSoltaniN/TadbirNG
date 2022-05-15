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

        private void Runner_OutputReceived(object sender, OutputReceivedEventArgs e)
        {
            if (txtConsole.InvokeRequired)
            {
                txtConsole.BeginInvoke((MethodInvoker)delegate ()
                {
                    if (!String.IsNullOrEmpty(e.Output))
                    {
                        txtConsole.AppendText(e.Output.Replace("\n", Environment.NewLine));
                        txtConsole.AppendText(Environment.NewLine);
                        txtConsole.ScrollToCaret();
                    }
                });
            }
        }

        private void Install_Click(object sender, EventArgs e)
        {
            var command = "sc create \"sppckeysrv\" type= own start= auto error= normal displayname= \"SPPC Key Server\" binpath= \"C:\\SPPC\\TadbirNG\\Service\\SPPC.Framework.KeyServer.exe\"";
            RunConsoleCommand(command);
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
            _runner.Run("docker pull msn1368/license-server:dev");
            pullWorker.ReportProgress(25);
            _runner.Run("docker pull msn1368/api-server:latest");
            pullWorker.ReportProgress(25);
            _runner.Run("docker pull msn1368/web-app:dev");
            pullWorker.ReportProgress(25);
            _runner.Run("docker pull msn1368/db-server:dev");
            pullWorker.ReportProgress(25);
        }

        private void StartWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            _runner.Run("docker-compose -f ..\\..\\..\\src\\TadbirNG\\docker-compose.override.yml -f ..\\..\\..\\src\\TadbirNG\\docker-compose.yml down");
            _runner.Run("docker-compose -f ..\\..\\..\\src\\TadbirNG\\docker-compose.override.yml -f ..\\..\\..\\src\\TadbirNG\\docker-compose.yml up --no-build");
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            txtConsole.AppendText(Environment.NewLine);
            progress.Value += e.ProgressPercentage;
            lblProgress.Text = String.Format("{0}%", progress.Value);
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
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

        private void TestAppRegistration()
        {
            var appGuid = new Guid("{E9DAA9A7-BB68-472B-8051-5E2ED9386F3C}");
            var utility = new InstallerUtility();
            if (!utility.IsAppInstalled())
            {
                utility.RegisterApplication(@"C:\SPPC\TadbirNG", "BE-LAPTOP", new Version("1.2.1366"));
            }
            else
            {
                MessageBox.Show("Already installed.");
            }
        }

        private readonly CliRunner _runner;
        private readonly StringBuilder _builder = new();
    }
}
