﻿using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using SPPC.Framework.Cryptography;
using SPPC.Framework.Helpers;
using SPPC.Framework.Service;
using SPPC.Licensing.Model;
using SPPC.Tadbir.Configuration;
using SPPC.Tadbir.Utility;
using SPPC.Tadbir.Utility.Model;

namespace SPPC.Tadbir.WinRunner
{
    public partial class RunnerForm : Form
    {
        public RunnerForm()
        {
            InitializeComponent();
            _apiClient = new ServiceClient(SysParameterUtility.Servers.Update);
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
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            _runner.Run(String.Format($"{ComposeCommand} down"));
            _runner.Run(String.Format($"{ComposeCommand} up --no-build -d"));
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
        }

        private void CheckForUpdate_Click(object sender, EventArgs e)
        {
            var versionPath = Path.Combine("..", "version");
            var configPath = Path.Combine("..", "config");
            if (!File.Exists(versionPath))
            {
                MessageBox.Show("اطلاعات نسخه جاری در دسترس نیست. لطفاً دوباره برنامه را نصب کنید.",
                    "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.RtlReading);
                return;
            }

            Cursor = Cursors.WaitCursor;
            var current = JsonHelper.To<VersionInfo>(File.ReadAllText(versionPath));
            var config = JsonHelper.To<RawBuildSettings>(
                CryptoService.Default.Decrypt(
                    File.ReadAllText(configPath)));
            _apiClient.AddHeader(LicenseConstants.InstanceHeaderName, config.Key);
            var latest = _apiClient.Get<VersionInfo>(UpdateApi.LatestVersionInfoUrl);
            _apiClient.RemoveHeader(LicenseConstants.InstanceHeaderName);
            var utility = new UpdateUtility()
            {
                UpdateServerUrl = SysParameterUtility.Servers.Update,
                Current = current,
                Latest = latest,
                DbServerName = config.DbServerName
            };
            if (!utility.NeedsUpdate())
            {
                MessageBox.Show("شما از آخرین نسخه برنامه استفاده می کنید.",
                    "اطلاع به کاربر", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.RtlReading);
            }
            else if (ConfirmApplicationUpdate(utility))
            {
                Cursor = Cursors.Default;
                current.Edition = UpdateUtility.GetInstalledEdition();
                var updater = new UpdateForm()
                {
                    InstanceKey = config.Key,
                    Updater = utility
                };
                updater.Show(this);
            }

            Cursor = Cursors.Default;
        }

        private bool ConfirmApplicationUpdate(UpdateUtility utility)
        {
            int downloadSize = utility.GetDownloadSize();
            var message = String.Format(
                "با ادامه عملیات، حدود {0} مگابایت دانلود می شود.{1}آیا با به روزرسانی برنامه موافق هستید؟",
                downloadSize, Environment.NewLine);
            var result = MessageBox.Show(
                this, message, "دریافت تایید از کاربر", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading);
            return result == DialogResult.Yes;
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private const string ComposeCommand = "docker compose -f \"docker-compose.yml\" -p \"tadbirng\"";
        private readonly CliRunner _runner = new();
        private readonly IApiClient _apiClient;
    }
}
