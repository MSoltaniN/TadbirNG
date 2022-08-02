using System;
using System.IO;
using System.Linq;
using SPPC.Framework.Helpers;
using SPPC.Tools.Model;
using SPPC.Framework.Common;
using SPPC.Framework.Service;
using SPPC.Licensing.Model;
using SPPC.Framework.Cryptography;

namespace SPPC.Tools.Utility
{
    public class UpdateUtility
    {
        public UpdateUtility()
        {
            _runner = new CliRunner();
            _runner.OutputReceived += Runner_OutputReceived;
            _apiClient = new ServiceClient(UpdateServerUrl);
            _archive = new ArchiveUtility(Path.Combine("..", "tools"), false);
        }

        public UpdateUtility(string imageRoot)
            : this()
        {
            _imageRoot = FileUtility.GetAbsolutePath(imageRoot);
        }

        public string DockerPath
        {
            get
            {
                return _dockerPath;
            }
            set
            {
                Verify.ArgumentNotNullOrEmptyString("value", nameof(value));
                _dockerPath = value;
                AddToProcessPath(_dockerPath);
            }
        }

        public VersionInfo Current { get; set; }

        public VersionInfo Latest { get; set; }

        public static int GetDownloadSize(VersionInfo current, VersionInfo latest)
        {
            int count = 0;
            double downloadSize = 0;
            if (current.Services[0].Sha256 != latest.Services[0].Sha256)
            {
                count++;
                downloadSize += FileSize.ToMegaBytes(latest.Services[0].Size, 1);
            }

            if (current.Services[1].Sha256 != latest.Services[1].Sha256)
            {
                count++;
                downloadSize += FileSize.ToMegaBytes(latest.Services[1].Size, 1);
            }

            if (current.Services[2].Sha256 != latest.Services[2].Sha256)
            {
                count++;
                downloadSize += FileSize.ToMegaBytes(latest.Services[2].Size, 1);
            }

            if (current.Services[3].Sha256 != latest.Services[3].Sha256)
            {
                count++;
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

        public void BackupService(string tempFolder, string serviceName, string tag = "latest")
        {
            var currentDir = Environment.CurrentDirectory;
            Environment.CurrentDirectory = tempFolder;
            _runner.Run($"docker save msn1368/{serviceName}:{tag} -o {serviceName}.tar");
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
                    .Where(line => line.Contains(DockerService.ApiServerImage))
                    .Select(line => line[line.IndexOf(DockerService.ApiServerImage)..])
                    .Select(line => line
                        .Replace(DockerService.ApiServerImage, String.Empty)
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
            var currentDir = Environment.CurrentDirectory;
            Environment.CurrentDirectory = _imageRoot;
            var version = new VersionInfo()
            {
                Version = QueryAppVersion()
            };
            version.Services.Add(GetServiceInfo(DockerService.LicenseServerImage));
            version.Services.Add(GetServiceInfo(DockerService.ApiServerImage, editionTag));
            version.Services.Add(GetServiceInfo(DockerService.DbServerImage));
            version.Services.Add(GetServiceInfo(DockerService.WebAppImage));
            Environment.CurrentDirectory = currentDir;
            return version;
        }

        public byte[] GetImageData(string serviceName, string edition = null)
        {
            string suffix = edition != null
                ? $"-{DockerUtility.GetEditionTag(edition)}"
                : String.Empty;
            var imagePath = Path.Combine(_imageRoot, serviceName, $"{serviceName}{suffix}.tar.gz");
            return File.ReadAllBytes(imagePath);
        }

        public void UpdateImageCache()
        {
            var currentDir = Environment.CurrentDirectory;
            Environment.CurrentDirectory = _imageRoot;
            _runner.Run("git pull --progress");
            Environment.CurrentDirectory = currentDir;
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

        private static ServiceInfo GetServiceInfo(string serviceName, string editionTag = null)
        {
            // NOTE: This method assumes current directory to be root folder of local dockercache clone
            var suffix = editionTag != null ? $"-{editionTag}" : String.Empty;
            var imagePath = Path.Combine(
                Environment.CurrentDirectory, serviceName, $"{serviceName}{suffix}.tar.gz");
            return DockerUtility.GetServiceInfo(imagePath);
        }

        private static void AddToProcessPath(string path)
        {
            var currentPath = Environment.GetEnvironmentVariable("Path", EnvironmentVariableTarget.Process);
            if (!String.IsNullOrEmpty(currentPath))
            {
                currentPath = String.Join(';', currentPath, path);
            }

            Environment.SetEnvironmentVariable("Path", currentPath, EnvironmentVariableTarget.Process);
        }

        private string QueryAppVersion()
        {
            // NOTE: This method assumes current directory to be root folder of local dockercache clone
            var output = _runner.Run("git show --oneline");
            return output
                .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                .Where(line => line.Contains("UI-"))
                .Select(line => line[(line.IndexOf("UI-") + 3)..])
                .FirstOrDefault();
        }

        private void Runner_OutputReceived(object sender, OutputReceivedEventArgs e)
        {
            if (!String.IsNullOrEmpty(e.Output))
            {
                File.AppendAllText(_logPath, e.Output.Replace("\n", Environment.NewLine));
            }
        }

        private const string _logPath = "update.log";
        private const string UpdateServerUrl = "http://localhost:9092";
        private string _dockerPath;
        private readonly string _imageRoot;
        private readonly CliRunner _runner;
        private readonly IApiClient _apiClient;
        private readonly ArchiveUtility _archive;
    }
}
