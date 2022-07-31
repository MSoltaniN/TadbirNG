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
            _utility = new UpdateUtility() { DockerPath = WinUtility.GetDockerExePath() };
        }

        public VersionInfo CurrentVersion { get; set; }

        public VersionInfo LatestVersion { get; set; }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            timer.Enabled = true;
            worker.RunWorkerAsync();
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            _summary = UpdateUtility.GetUpdateSummary(CurrentVersion, LatestVersion);
            _updateFolder = UpdateUtility.PrepareUpdateFolder();
            PrepareLatestServices();
            DownloadServices();
            BackupServices();
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value += e.ProgressPercentage;
            lblProgress.Text = String.Format("{0}%", progressBar.Value);
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

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
                Close();
            }

            if (!DownloadService(DockerService.ApiServerImage))
            {
                worker.CancelAsync();
                Close();
            }

            if (!DownloadService(DockerService.DbServerImage))
            {
                worker.CancelAsync();
                Close();
            }

            if (!DownloadService(DockerService.WebAppImage))
            {
                worker.CancelAsync();
                Close();
            }
        }

        private bool DownloadService(string serviceName)
        {
            bool downloaded = true;
            var progress = TryDownloadService(serviceName);
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
            var progress = GetUpdateProgress(serviceName, _summary.Item2);
            if (progress != null)
            {
                var serviceInfo = LatestVersion.Services
                    .Where(svc => svc.Name == serviceName)
                    .FirstOrDefault();
                bool downloaded = _utility.DownloadService(_updateFolder, serviceInfo, instance);
                if (!downloaded)
                {
                    timer.Enabled = false;
                    MessageBox.Show("بروز خطا هنگام دانلود نسخه جدید سرویس برنامه. لطفاً دوباره تلاش کنید.",
                        "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.RtlReading);
                    return null;
                }
            }

            return progress ?? new UpdateProgress();
        }

        private void BackupServices()
        {
            var tempFolder = FileUtility.GetTempFolderPath();
            var editionTag = DockerUtility.GetEditionTag(CurrentVersion.Edition);
            BackupService(tempFolder, DockerService.LicenseServerImage);
            BackupService(tempFolder, DockerService.ApiServerImage, editionTag);
            BackupService(tempFolder, DockerService.DbServerImage);
            BackupService(tempFolder, DockerService.WebAppImage, "dev");
        }

        private void BackupService(string tempFolder, string serviceName, string tag = "latest")
        {
            var progress = GetUpdateProgress(serviceName, _summary.Item2);
            if (progress != null)
            {
                _utility.BackupService(tempFolder, serviceName, tag);
                worker.ReportProgress(progress.BackupProgress);
            }
        }

        private UpdateProgress GetUpdateProgress(string serviceName, int downloadSize)
        {
            var progress = default(UpdateProgress);
            var currentInfo = CurrentVersion.Services
                .Where(svc => svc.Name == serviceName)
                .FirstOrDefault();
            var latestInfo = LatestVersion.Services
                .Where(svc => svc.Name == serviceName)
                .FirstOrDefault();
            if (currentInfo.Sha256 != latestInfo.Sha256)
            {
                var percent = (int)(FileSize.ToMegaBytes(latestInfo.Size) / downloadSize * 100 / 3);
                progress = new UpdateProgress()
                {
                    DownloadProgress = percent,
                    BackupProgress = percent,
                    SetupProgress = percent
                };
            }

            return progress;
        }

        private readonly UpdateUtility _utility;
        private (int, int) _summary;
        private string _updateFolder;
    }
}
