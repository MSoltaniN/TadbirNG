using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using SPPC.Framework.Helpers;
using SPPC.Tools.Api;
using SPPC.Tools.Model;
using SPPC.Tools.Utility;

namespace SPPC.Tadbir.WinRunner
{
    public partial class UpdateForm : Form
    {
        public UpdateForm()
        {
            InitializeComponent();
        }

        public VersionInfo CurrentVersion { get; set; }

        public VersionInfo LatestVersion { get; set; }

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
            _utility = new UpdateUtility()
            {
                DockerPath = WinUtility.GetDockerExePath(),
                Current = CurrentVersion,
                Latest = LatestVersion
            };
            _downloadSize = UpdateUtility.GetDownloadSize(CurrentVersion, LatestVersion);
            _updateFolder = UpdateUtility.PrepareUpdateFolder();
            PrepareLatestServices();
            DownloadServices();
            BackupServices();
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
                _utility.FinalizeUpdate();
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
            Close();
        }

        private void PrepareLatestServices()
        {
            PrepareLatestService(DockerService.LicenseServerImage, UpdateApi.LicenseServerImageUrl);
            PrepareLatestService(DockerService.ApiServerImage, UpdateApi.ApiServerImageUrl);
            PrepareLatestService(DockerService.DbServerImage, UpdateApi.DbServerImageUrl);
            PrepareLatestService(DockerService.WebAppImage, UpdateApi.WebAppImageUrl);
        }

        private void PrepareLatestService(string name, string sourceUrl)
        {
            var serviceInfo = LatestVersion.Services
                .Where(svc => svc.Name == name)
                .FirstOrDefault();
            if (serviceInfo != null)
            {
                serviceInfo.SourceUrl = sourceUrl;
            }
        }

        private void DownloadServices()
        {
            if (!DownloadService(DockerService.LicenseServerImage))
            {
                worker.CancelAsync();
                CloseForm();
            }

            if (!DownloadService(DockerService.ApiServerImage, InstanceKey))
            {
                worker.CancelAsync();
                CloseForm();
            }

            if (!DownloadService(DockerService.DbServerImage))
            {
                worker.CancelAsync();
                CloseForm();
            }

            if (!DownloadService(DockerService.WebAppImage))
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
                var serviceInfo = LatestVersion.Services
                    .Where(svc => svc.Name == serviceName)
                    .FirstOrDefault();
                bool downloaded = _utility.DownloadService(_updateFolder, serviceInfo, instance);
                if (!downloaded)
                {
                    timer.Enabled = false;
                    MessageBox.Show("بروز خطا هنگام دانلود نسخه جدید سرویس برنامه. لطفاً دوباره تلاش کنید.",
                        "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.RtlReading);
                    return null;
                }
            }

            return progress;
        }

        private void BackupServices()
        {
            _backupFolder = FileUtility.GetTempFolderPath();
            var editionTag = DockerUtility.GetEditionTag(CurrentVersion.Edition);
            BackupService(DockerService.LicenseServerImage);
            BackupService(DockerService.ApiServerImage, editionTag);
            BackupService(DockerService.DbServerImage);
            BackupService(DockerService.WebAppImage, "dev");
        }

        private void BackupService(string serviceName, string tag = "latest")
        {
            var progress = GetUpdateProgress(serviceName, _downloadSize);
            if (progress.BackupProgress > 0)
            {
                _utility.BackupService(_backupFolder, serviceName, tag);
                worker.ReportProgress(progress.BackupProgress);
            }
        }

        private bool UpdateServices()
        {
            bool updated = true;
            if (!UpdateService(DockerService.LicenseServerImage))
            {
                return false;
            }

            if (!UpdateService(DockerService.ApiServerImage))
            {
                return false;
            }

            if (!UpdateService(DockerService.DbServerImage))
            {
                return false;
            }

            if (!UpdateService(DockerService.WebAppImage))
            {
                updated = false;
            }

            return updated;
        }

        private bool UpdateService(string serviceName)
        {
            bool updated = true;
            if (_utility.UpdateService(serviceName))
            {
                var progress = GetUpdateProgress(serviceName, _downloadSize);
                worker.ReportProgress(progress.SetupProgress);
            }
            else
            {
                _utility.RollbackUpdate(_backupFolder);
                updated = false;
            }

            return updated;
        }

        private UpdateProgress GetUpdateProgress(string serviceName, int downloadSize)
        {
            var progress = new UpdateProgress();
            if (_utility.NeedsUpdate(serviceName))
            {
                var latestInfo = LatestVersion.Services
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

        private TimeSpan _elapsed;
        private UpdateUtility _utility;
        private int _downloadSize;
        private string _updateFolder;
        private string _backupFolder;
    }
}
