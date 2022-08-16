using System;
using System.IO;
using System.Linq;
using SPPC.Framework.Helpers;
using SPPC.Tools.Model;
using SPPC.Framework.Common;
using SPPC.Framework.Service;
using SPPC.Licensing.Model;
using SPPC.Framework.Cryptography;
using SPPC.Tadbir.Configuration;

namespace SPPC.Tools.Utility
{
    public class UpdateUtility
    {
        public UpdateUtility()
        {
            _runner = new CliRunner();
            _runner.OutputReceived += Runner_OutputReceived;
            _archive = new ArchiveUtility(Path.Combine("..", "tools"), false);
        }

        public UpdateUtility(string imageRoot)
            : this()
        {
            _imageRoot = FileUtility.GetAbsolutePath(imageRoot);
        }

        public VersionInfo Current { get; set; }

        public VersionInfo Latest { get; set; }

        public string UpdateServerUrl
        {
            get
            {
                return _updateServerUrl;
            }
            set
            {
                Verify.ArgumentNotNullOrEmptyString(value, nameof(value));
                _updateServerUrl = value;
                _apiClient = new ServiceClient(_updateServerUrl);
            }
        }

        public static int GetDownloadSize(VersionInfo current, VersionInfo latest)
        {
            double downloadSize = 0;
            if (current.Services[0].Sha256 != latest.Services[0].Sha256)
            {
                downloadSize += FileSize.ToMegaBytes(latest.Services[0].Size, 1);
            }

            if (current.Services[1].Sha256 != latest.Services[1].Sha256)
            {
                downloadSize += FileSize.ToMegaBytes(latest.Services[1].Size, 1);
            }

            if (current.Services[2].Sha256 != latest.Services[2].Sha256)
            {
                downloadSize += FileSize.ToMegaBytes(latest.Services[2].Size, 1);
            }

            if (current.Services[3].Sha256 != latest.Services[3].Sha256)
            {
                downloadSize += FileSize.ToMegaBytes(latest.Services[3].Size, 1);
            }

            return (int)Math.Round(downloadSize);
        }

        public static string PrepareUpdateFolder()
        {
            var root = Path.GetDirectoryName(Environment.CurrentDirectory);
            var updateFolder = Path.Combine(root, "docker");
            if (!Directory.Exists(updateFolder))
            {
                var dirInfo = Directory.CreateDirectory(updateFolder);
                dirInfo.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
            }

            return updateFolder;
        }

        public bool DownloadService(string updateFolder, ServiceInfo serviceInfo, string instance = null)
        {
            if (!String.IsNullOrEmpty(instance))
            {
                _apiClient.AddHeader(LicenseConstants.InstanceHeaderName, instance);
            }

            var file = _apiClient.GetFile(serviceInfo.SourceUrl);
            var validated = CryptoService.Default.ValidateHash(file.RawData, serviceInfo.Sha256)
                && file.RawData.Length == serviceInfo.Size;
            if (validated)
            {
                File.WriteAllBytes(Path.Combine(updateFolder, file.Name), file.RawData);
            }

            _apiClient.RemoveHeader(LicenseConstants.InstanceHeaderName);
            return validated;
        }

        public void BackupService(string tempFolder, string serviceName, string tag = null)
        {
            var imageTag = tag ?? SysParameterUtility.DbServer.Tag;
            var currentDir = Environment.CurrentDirectory;
            Environment.CurrentDirectory = tempFolder;
            _runner.Run($"docker save {SysParameterUtility.DockerHubHandle}/{serviceName}:{imageTag} -o {serviceName}.tar");
            var tarPath = Path.Combine(Environment.CurrentDirectory, $"{serviceName}.tar");
            _archive.GZip(tarPath);
            Environment.CurrentDirectory = currentDir;
        }

        public bool UpdateService(string serviceName)
        {
            bool updated = false;
            if (NeedsUpdate(serviceName))
            {
                var setupRoot = Path.GetDirectoryName(Environment.CurrentDirectory);
                var configPath = Path.Combine(setupRoot, "config");
                if (File.Exists(configPath))
                {
                    var config = JsonHelper.To<RawBuildSettings>(
                        CryptoService.Default.Decrypt(
                            File.ReadAllText(configPath)));
                    var root = Path.Combine(setupRoot, "docker");
                    InstallerUtility.ConfigureDockerService(root, serviceName, config);
                    updated = true;
                }
            }
            else
            {
                updated = true;
            }

            return updated;
        }

        public static string GetInstalledEdition()
        {
            // NOTE: This method assumes current directory is set to runner folder.
            string edition = String.Empty;
            var overridePath = "docker-compose.override.yml";
            if (File.Exists(overridePath))
            {
                var editionTag = File
                    .ReadAllLines(overridePath)
                    .Where(line => line.Contains(SysParameterUtility.ApiServer.ImageName))
                    .Select(line => line[line.IndexOf(SysParameterUtility.ApiServer.ImageName)..])
                    .Select(line => line
                        .Replace(SysParameterUtility.ApiServer.ImageName, String.Empty)
                        .Replace(":", String.Empty))
                    .FirstOrDefault();
                edition = DockerUtility.GetEdition(editionTag);
            }

            return edition;
        }

        public void RollbackUpdate(string backupFolder)
        {
            var currentDir = Environment.CurrentDirectory;
            Environment.CurrentDirectory = backupFolder;
            Array.ForEach(new DirectoryInfo(backupFolder)
                .GetFiles(), fi =>
                {
                    _runner.Run($"docker load -i {fi.Name}");
                });
            Environment.CurrentDirectory = currentDir;
        }

        public static void CleanUp(string updateFolder, string backupFolder)
        {
            if (Directory.Exists(updateFolder))
            {
                FileUtility.DeleteFolder(updateFolder);
            }

            if (Directory.Exists(backupFolder))
            {
                FileUtility.DeleteFolder(backupFolder);
            }
        }

        public void FinalizeUpdate()
        {
            foreach (var info in Latest.Services)
            {
                info.SourceUrl = null;
            }

            var versionPath = Path.Combine("..", "version");
            File.WriteAllText(versionPath, JsonHelper.From(Latest));
        }

        public VersionInfo GetCurrentVersionInfo(string edition)
        {
            Verify.ArgumentNotNullOrEmptyString(edition, nameof(edition));
            var editionTag = DockerUtility.GetEditionTag(edition);
            var versionFilePath = Path.Combine(_imageRoot, $"version.{editionTag}");
            return JsonHelper.To<VersionInfo>(File.ReadAllText(versionFilePath));
        }

        public byte[] GetImageData(string serviceName, string edition = null)
        {
            string suffix = edition != null
                ? $"-{DockerUtility.GetEditionTag(edition)}"
                : String.Empty;
            var imagePath = Path.Combine(_imageRoot, serviceName, $"{serviceName}{suffix}.tar.gz");
            return File.ReadAllBytes(imagePath);
        }

        public bool NeedsUpdate()
        {
            return Current.Version != Latest.Version
                || NeedsUpdate(SysParameterUtility.LicenseServer.ImageName)
                || NeedsUpdate(SysParameterUtility.ApiServer.ImageName)
                || NeedsUpdate(SysParameterUtility.DbServer.ImageName)
                || NeedsUpdate(SysParameterUtility.WebApp.ImageName);
        }

        public bool NeedsUpdate(string serviceName)
        {
            var currentInfo = Current.Services
                .Where(svc => svc.Name == serviceName)
                .FirstOrDefault();
            var latestInfo = Latest.Services
                .Where(svc => svc.Name == serviceName)
                .FirstOrDefault();
            return currentInfo.Sha256 != latestInfo.Sha256;
        }

        private void Runner_OutputReceived(object sender, OutputReceivedEventArgs e)
        {
            if (!String.IsNullOrEmpty(e.Output))
            {
                var log = $"{e.Output.Replace("\n", Environment.NewLine)}{Environment.NewLine}";
                File.AppendAllText(_logPath, log);
            }
        }

        private const string _logPath = "update.log";
        private readonly string _imageRoot;
        private readonly CliRunner _runner;
        private readonly ArchiveUtility _archive;
        private IApiClient _apiClient;
        private string _updateServerUrl;
    }
}
