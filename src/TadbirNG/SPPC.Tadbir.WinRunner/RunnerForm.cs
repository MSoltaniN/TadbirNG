using System;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using SPPC.Framework.Helpers;
using SPPC.Tools.Model;
using SPPC.Tools.Transforms;
using SPPC.Tools.Transforms.Templates;
using SPPC.Tools.Utility;

namespace SPPC.Tadbir.WinRunner
{
    public partial class RunnerForm : Form
    {
        public RunnerForm()
        {
            InitializeComponent();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            if (MessageBox.Show("می خواهید سرویس های برنامه را متوقف کنید؟",
                "خطا", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2,
                MessageBoxOptions.RtlReading) == DialogResult.Yes)
            {
                CleanStop();
            }
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

        private void RunApp_Click(object sender, EventArgs e)
        {
            txtConsole.Focus();
            _runner.OutputReceived += Runner_OutputReceived;
            btnRunApp.Enabled = false;
            worker.RunWorkerAsync();
            //runWorker.RunWorkerAsync();
        }

        private void RunWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(15000);
            var settings = BuildSettings.Docker;
            settings.DbServerName = ConfigurationManager.AppSettings["DbServerName"];
            settings.DbUserName = ConfigurationManager.AppSettings["LoginName"];
            settings.DbPassword = ConfigurationManager.AppSettings["Password"];

            var utility = new DockerUtility();
            utility.WaitForContainer(DockerService.LicenseServer);
            ITextTemplate generator = new LocalLicenseApiSettings(settings);
            File.WriteAllText("appSettings.json", generator.TransformText());
            utility.ReplaceContainerFile(DockerService.LicenseServer, "appSettings.json", "appSettings.json");

            utility.WaitForContainer(DockerService.ApiServer);
            generator = new WebApiSettings(settings);
            File.WriteAllText("appSettings.json", generator.TransformText());
            utility.ReplaceContainerFile(DockerService.ApiServer, "appSettings.json", "appSettings.json");
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            _runner.Run(String.Format($"{ComposeCommand} up --no-build"));
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("می خواهید سرویس های برنامه را متوقف کنید؟",
                "خطا", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2,
                MessageBoxOptions.RtlReading) == DialogResult.Yes)
            {
                CleanStop();
                Application.Exit();
            }
        }

        private void CleanStop()
        {
            _runner.Stop();
            _runner.Run(String.Format($"{ComposeCommand} down"));
            if (worker.IsBusy)
            {
                worker.CancelAsync();
            }
        }

        private const string ComposeCommand = "docker-compose -f docker-compose.override.yml -f docker-compose.yml";
        private readonly CliRunner _runner = new();
    }
}
