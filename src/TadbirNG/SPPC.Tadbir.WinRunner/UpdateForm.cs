using System;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SPPC.Framework.Helpers;
using SPPC.Tadbir.Configuration;
using SPPC.Tadbir.Utility;
using SPPC.Tadbir.Utility.Docker;
using SPPC.Tadbir.Utility.Model;

namespace SPPC.Tadbir.WinRunner
{
    public partial class UpdateForm : Form
    {
        public UpdateForm()
        {
            InitializeComponent();
        }

        public UpdateUtility Updater { get; set; }

        public string InstanceKey { get; set; }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            timer.Enabled = true;
            worker.RunWorkerAsync();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            UpdateUtility.CleanUp(_updateFolder, _backupFolder);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            var seconds = _elapsed.TotalSeconds;
            _elapsed = TimeSpan.FromSeconds(++seconds);
            lblElapsed.Text = _elapsed.ToString(@"hh\:mm\:ss");
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            LogOperation("Update process started.");
            LogOperation();
            LogOperation("Estimating download size...");
            _downloadSize = Updater.GetDownloadSize();
            LogOperation($"Approximate download size : {_downloadSize}");
            LogOperation();
            LogOperation("Initializing update...");
            _updateFolder = UpdateUtility.PrepareUpdateFolder();
            Updater.PrepareLatestServices();
            LogOperation("Downloading services...");
            DownloadServices();
            LogOperation();
            LogOperation("Preparing backup for current service images...");
            BackupServices();
            LogOperation();
            DisableCancelButton();
            if (!UpdateServices())
            {
                timer.Enabled = false;
                MessageBox.Show("بروز خطا هنگام به روزرسانی سرویس برنامه. لطفاً دوباره تلاش کنید.",
                    "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.RtlReading);
                worker.CancelAsync();
                CloseForm();
            }
            else
            {
                LogOperation("Finalizing update...");
                Updater.FinalizeUpdate();
            }
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value += e.ProgressPercentage;
            lblProgress.Text = String.Format("{0}%", progressBar.Value);
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBar.Value = 100;
            lblProgress.Text = String.Format("{0}%", progressBar.Value);
            timer.Enabled = false;
            MessageBox.Show("به روزرسانی برنامه با موفقیت انجام شد. لطفاً برای اعمال شدن تغییرات، برنامه را دوباره راه اندازی کنید.",
                "عملیات موفق", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1,
                MessageBoxOptions.RtlReading);
            Environment.CurrentDirectory = Path.GetDirectoryName(
                Environment.GetCommandLineArgs()[0]);
            File.AppendAllText("update.log", _logBuilder.ToString());
            Close();
        }

        private bool UsesDockerDbServer()
        {
            return Updater.DbServerName == SysParameterUtility.DbServer.Name;
        }

        private void DownloadServices()
        {
            if (!DownloadService(SysParameterUtility.LicenseServer.ImageName))
            {
                worker.CancelAsync();
                CloseForm();
            }

            if (!DownloadService(SysParameterUtility.ApiServer.ImageName, InstanceKey))
            {
                worker.CancelAsync();
                CloseForm();
            }

            if (UsesDockerDbServer() && !DownloadService(SysParameterUtility.DbServer.ImageName))
            {
                worker.CancelAsync();
                CloseForm();
            }

            if (!DownloadService(SysParameterUtility.WebApp.ImageName))
            {
                worker.CancelAsync();
                CloseForm();
            }
        }

        private bool DownloadService(string serviceName, string instance = null)
        {
            bool downloaded = true;
            var progress = TryDownloadService(serviceName, instance);
            if (progress == null)
            {
                downloaded = false;
            }
            else
            {
                worker.ReportProgress(progress.DownloadProgress);
            }

            return downloaded;
        }

        private UpdateProgress TryDownloadService(string serviceName, string instance = null)
        {
            var progress = GetUpdateProgress(serviceName, _downloadSize);
            if (progress.DownloadProgress > 0)
            {
                var serviceInfo = Updater.Latest.Services
                    .Where(svc => svc.Name == serviceName)
                    .FirstOrDefault();
                LogOperation($"Downloading service {serviceName}...");
                bool downloaded = Updater.DownloadService(_updateFolder, serviceInfo, instance);
                if (!downloaded)
                {
                    LogOperation($"Could not download service {serviceName}. Encountered failed or corrupted download.", true);
                    timer.Enabled = false;
                    MessageBox.Show("بروز خطا هنگام دانلود نسخه جدید سرویس برنامه. لطفاً دوباره تلاش کنید.",
                        "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.RtlReading);
                    return null;
                }
                else
                {
                    LogOperation("Service downloaded.");
                }
            }
            else
            {
                LogOperation($"Service {serviceName} is up-to-date.");
            }

            return progress;
        }

        private void BackupServices()
        {
            _backupFolder = FileUtility.GetTempFolderPath();
            var editionTag = DockerUtility.GetEditionTag(Updater.Current.Edition);
            BackupService(SysParameterUtility.LicenseServer.ImageName);
            BackupService(SysParameterUtility.ApiServer.ImageName, editionTag);
            BackupService(SysParameterUtility.WebApp.ImageName, SysParameterUtility.WebApp.Tag);
            if (UsesDockerDbServer())
            {
                BackupService(SysParameterUtility.DbServer.ImageName);
            }
        }

        private void BackupService(string serviceName, string tag = null)
        {
            var imageTag = tag ?? SysParameterUtility.DbServer.Tag;
            var progress = GetUpdateProgress(serviceName, _downloadSize);
            if (progress.BackupProgress > 0)
            {
                Updater.BackupService(_backupFolder, serviceName, imageTag);
                worker.ReportProgress(progress.BackupProgress);
                LogOperation($"Backup completed for service {serviceName}.");
            }
        }

        private bool UpdateServices()
        {
            bool updated = true;
            if (!UpdateService(SysParameterUtility.LicenseServer.ImageName))
            {
                return false;
            }

            if (!UpdateService(SysParameterUtility.ApiServer.ImageName))
            {
                return false;
            }

            if (UsesDockerDbServer() && !UpdateService(SysParameterUtility.DbServer.ImageName))
            {
                return false;
            }

            if (!UpdateService(SysParameterUtility.WebApp.ImageName))
            {
                updated = false;
            }

            return updated;
        }

        private bool UpdateService(string serviceName)
        {
            bool updated = true;
            if (Updater.UpdateService(serviceName))
            {
                var progress = GetUpdateProgress(serviceName, _downloadSize);
                worker.ReportProgress(progress.SetupProgress);
                if (progress.SetupProgress > 0)
                {
                    LogOperation($"Service {serviceName} updated.");
                }
            }
            else
            {
                LogOperation($"Error updating service {serviceName}. Performing rollback...");
                Updater.RollbackUpdate(_backupFolder);
                updated = false;
            }

            return updated;
        }

        private UpdateProgress GetUpdateProgress(string serviceName, int downloadSize)
        {
            var progress = new UpdateProgress();
            if (Updater.NeedsUpdate(serviceName))
            {
                var latestInfo = Updater.Latest.Services
                    .Where(svc => svc.Name == serviceName)
                    .FirstOrDefault();
                var percent = (int)(FileSize.ToMegaBytes(latestInfo.Size) / downloadSize * 100 / 3);
                progress.DownloadProgress =
                    progress.BackupProgress =
                    progress.SetupProgress = percent;
            }

            return progress;
        }

        private void DisableCancelButton()
        {
            if (btnCancel.InvokeRequired)
            {
                btnCancel.BeginInvoke((MethodInvoker)delegate ()
                {
                    btnCancel.Enabled = false;
                });
            }
        }

        private void CloseForm()
        {
            if (InvokeRequired)
            {
                BeginInvoke((MethodInvoker)delegate ()
                {
                    Close();
                });
            }
        }

        private void LogOperation(string message = null, bool isError = false)
        {
            string log = message ?? Environment.NewLine;
            if (log == Environment.NewLine)
            {
                _logBuilder.AppendLine();
            }
            else
            {
                var logType = isError ? "ERROR" : "INFO";
                _logBuilder.AppendLine($"[{DateTime.Now.ToShortDateString()} {DateTime.Now.TimeOfDay}] [{logType}] {message}");
            }

            int chunkSize = (int)FileSize.FromKiloBytes(1);
            if (_logBuilder.Length >= chunkSize)
            {
                var chunk = _logBuilder
                    .ToString()
                    .Substring(0, chunkSize);
                File.AppendAllText("update.log", chunk);
                _logBuilder.Remove(0, chunkSize);
            }
        }

        private TimeSpan _elapsed;
        private int _downloadSize;
        private string _updateFolder;
        private string _backupFolder;
        private readonly StringBuilder _logBuilder = new();
    }
}
