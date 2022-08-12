using System;
using System.IO;
using System.Linq;
using SPPC.Framework.Common;
using SPPC.Framework.Helpers;
using SPPC.Tadbir.Configuration.Models;
using SPPC.Tools.Model;
using SPPC.Tools.Transforms.Templates;

namespace SPPC.Tools.Utility
{
    public class BuildUtility
    {
        public EventHandler<OutputReceivedEventArgs> OutputRedirector
        {
            get
            {
                return _outputRedirector;
            }
            set
            {
                Verify.ArgumentNotNull(value, nameof(value));
                if (_outputRedirector != null)
                {
                    _runner.OutputReceived -= _outputRedirector;
                }

                _outputRedirector = value;
                _runner.OutputReceived += _outputRedirector;
            }
        }

        public OutputProviderDelegate OutputProvider { get; set; }

        public void RunBuildProcess()
        {
            OutputProvider($"Build process started.{Environment.NewLine}");
            DetectLatestChanges();

            // NOTE: ALL builds must be done with caching disabled (--no-cache switch)
            // Calculate current checksums for all Docker projects...
            if (_licenseChanged)
            {
                RebuildLicenseServer();
            }
            else
            {
                OutputProvider($"{DockerService.LicenseServer} is up-to-date.");
            }

            if (_apiChanged)
            {
                RebuildApiServer();
            }
            else
            {
                OutputProvider($"{DockerService.ApiServer} is up-to-date.");
            }

            if (_appChanged)
            {
                RebuildWebApp();
            }
            else
            {
                OutputProvider($"{DockerService.WebApp} is up-to-date.");
            }

            if (_dbChanged)
            {
                RebuildDbServer();
            }
            else
            {
                OutputProvider($"{DockerService.DbServer} is up-to-date.");
            }

            OutputProvider($"Build process completed successfully.{Environment.NewLine}");
        }

        public void RunPublishProcess()
        {
            // Update cached image files in DockerCache repository
            if (_dbChanged)
            {
                OutputProvider("Updating cache for Db Server...");
                UpdateServiceCache(DockerService.DbServerImage);
            }

            if (_licenseChanged)
            {
                OutputProvider("Updating cache for License Server...");
                UpdateServiceCache(DockerService.LicenseServerImage);
            }

            if (_appChanged)
            {
                OutputProvider("Updating cache for Web App...");
                UpdateServiceCache(DockerService.WebAppImage, "dev");
            }

            if (_apiChanged)
            {
                OutputProvider("Updating cache for Api Server...");
                UpdateServiceCache(DockerService.ApiServerImage, Edition.StandardTag);
                UpdateServiceCache(DockerService.ApiServerImage, Edition.ProfessionalTag);
                UpdateServiceCache(DockerService.ApiServerImage, Edition.EnterpriseTag);
            }

            CreateVersionFiles();

            // Commit changes to DockerCache remote
            OutputProvider("Commiting changes to git remote...");
            var apiVersion = VersionUtility.GetApiVersion();
            var appVersion = VersionUtility.GetAppVersion();
            var currentDir = Environment.CurrentDirectory;
            Environment.CurrentDirectory = FileUtility.GetAbsolutePath(PathConfig.DockerCacheRoot);
            var message = String.Format($"Pushed builds {apiVersion},UI-{appVersion}");
            _runner.Run(String.Format(ToolConstants.GitCommitTemplate, message));
            _runner.Run(ToolConstants.GitPushCommand);
            OutputProvider("Publish process completed successfully.");
            Environment.CurrentDirectory = currentDir;
        }

        private void DetectLatestChanges()
        {
            // NOTE: For this process to be correct, current clone MUST be pristine
            // (i.e. NO change must be sensed by git)
            var licenseChecksum = ChecksumUtility.CalculateChecksum(DockerService.LicenseServer);
            var apiChecksum = ChecksumUtility.CalculateChecksum(DockerService.ApiServer);
            var appChecksum = ChecksumUtility.CalculateChecksum(DockerService.WebApp);
            var dbChecksum = ChecksumUtility.CalculateChecksum(DockerService.DbServer);

            // Pull latest changes from TadbirNG repository...
            var currentDir = Environment.CurrentDirectory;
            Environment.CurrentDirectory = FileUtility.GetAbsolutePath(
                Path.Combine("..", "..", ".."));
            _runner.Run(ToolConstants.GitPullCommand);
            Environment.CurrentDirectory = currentDir;

            _licenseChanged = IsModifiedProject(DockerService.LicenseServer, licenseChecksum);
            _apiChanged = IsModifiedProject(DockerService.ApiServer, apiChecksum);
            _appChanged = IsModifiedProject(DockerService.WebApp, appChecksum);
            _dbChanged = IsModifiedProject(DockerService.DbServer, dbChecksum);
        }

        private static void CreateVersionFiles()
        {
            var cacheRoot = FileUtility.GetAbsolutePath(PathConfig.DockerCacheRoot);
            CreateVersionFile(cacheRoot, Edition.Standard, Edition.StandardTag);
            CreateVersionFile(cacheRoot, Edition.Professional, Edition.ProfessionalTag);
            CreateVersionFile(cacheRoot, Edition.Enterprise, Edition.EnterpriseTag);
        }

        private static void CreateVersionFile(string cacheRoot, string edition, string editionTag)
        {
            var version = new VersionInfo()
            {
                Version = VersionUtility.GetAppVersion(),
                Edition = edition
            };

            var imagePath = Path.Combine(
                cacheRoot, DockerService.LicenseServerImage, $"{DockerService.LicenseServerImage}.tar.gz");
            version.Services.Add(DockerUtility.GetServiceInfo(imagePath));
            imagePath = Path.Combine(
                cacheRoot, DockerService.ApiServerImage, $"{DockerService.ApiServerImage}-{editionTag}.tar.gz");
            version.Services.Add(DockerUtility.GetServiceInfo(imagePath));
            imagePath = Path.Combine(
                cacheRoot, DockerService.DbServerImage, $"{DockerService.DbServerImage}.tar.gz");
            version.Services.Add(DockerUtility.GetServiceInfo(imagePath));
            imagePath = Path.Combine(
                cacheRoot, DockerService.WebAppImage, $"{DockerService.WebAppImage}.tar.gz");
            version.Services.Add(DockerUtility.GetServiceInfo(imagePath));
            File.WriteAllText(Path.Combine(cacheRoot, $"version.{editionTag}"), JsonHelper.From(version));
        }

        private static bool IsModifiedProject(string project, string oldChecksum)
        {
            var newChecksum = ChecksumUtility.CalculateChecksum(project);
            return newChecksum != oldChecksum;
        }

        private static string GetEditionData(EditionsConfig editions, string editionTag)
        {
            EditionConfig edition = null;
            switch (editionTag)
            {
                case "std":
                    edition = editions.Standard;
                    break;
                case "pro":
                    edition = editions.Professional;
                    break;
                case "ent":
                    edition = editions.Enterprise;
                    break;
                default:
                    break;
            }

            return JsonHelper.From(edition);
        }

        private static string GetImageFileName(string serviceName, string tag)
        {
            var editionTags = new string[] { "std", "pro", "ent" };
            return editionTags.Contains(tag)
                ? String.Format($"{serviceName}-{tag}")
                : serviceName;
        }

        private void RebuildLicenseServer()
        {
            // If license server is modified, backup appSettings, generate dummy settings,
            // build base image and restore appSettings...
            OutputProvider("Rebuilding license server...");
            string tempPath = String.Empty;
            string settingsPath = String.Empty;
            string devSettingsPath = String.Empty;
            try
            {
                tempPath = FileUtility.GetTempFolderPath();
                settingsPath = Path.Combine(PathConfig.LocalServerRoot, ToolConstants.AppSettings);
                FileUtility.BackupFile(settingsPath, tempPath);
                devSettingsPath = Path.Combine(PathConfig.LocalServerRoot, ToolConstants.DevAppSettings);
                FileUtility.BackupFile(devSettingsPath, tempPath);

                var settings = BuildSettings.DockerDummy;
                var appSettings = new LocalLicenseApiSettings(settings).TransformText();
                File.WriteAllText(settingsPath, appSettings);
                File.WriteAllText(devSettingsPath, appSettings);

                _runner.Run(String.Format($"docker-compose -f {PathConfig.OverridePath} -f {PathConfig.ComposePath} build --no-cache LicenseServer"));
                OutputProvider($"[License Server] => Rebuild succeeded.{Environment.NewLine}");
            }
            finally
            {
                FileUtility.RestoreFile(settingsPath, tempPath);
                FileUtility.RestoreFile(devSettingsPath, tempPath);
                Directory.Delete(tempPath);
            }
        }

        private void RebuildApiServer()
        {
            // If api server is modified :
            // Backup docker-compose, override, appSettings, license and edition files
            // Generate dummy appSettings and dummy license
            // For each edition (std, pro and ent) :
            // 1. Generate docker-compose, override and edition file
            // 2. Build base image for that edition
            // Restore docker-compose, override, appSettings, license and edition files
            string tempPath = String.Empty;
            var files = new string[]
            {
                PathConfig.ComposePath, PathConfig.OverridePath,
                Path.Combine(PathConfig.WebApiRoot, ToolConstants.AppSettings),
                Path.Combine(PathConfig.WebApiRoot, ToolConstants.DevAppSettings),
                Path.Combine(PathConfig.WebApiRoot, "wwwroot", "edition"),
                Path.Combine(PathConfig.WebApiRoot, "wwwroot", "edition.Development.json"),
                Path.Combine(PathConfig.WebApiRoot, "wwwroot", "license"),
                Path.Combine(PathConfig.WebApiRoot, "wwwroot", "license.Development.json"),
            };
            try
            {
                var editions = new string[] { Edition.StandardTag, Edition.ProfessionalTag, Edition.EnterpriseTag };
                tempPath = FileUtility.GetTempFolderPath();
                FileUtility.BackupFiles(files, tempPath);

                var settings = BuildSettings.DockerDummy;
                var appSettings = new WebApiSettings(settings).TransformText();
                File.WriteAllText(files[(int)ApiServerPathIndex.Settings], appSettings);
                File.WriteAllText(files[(int)ApiServerPathIndex.DevSettings], appSettings);
                File.WriteAllText(files[(int)ApiServerPathIndex.License], "{}");
                File.WriteAllText(files[(int)ApiServerPathIndex.DevLicense], "{}");

                var allConfig = JsonHelper.To<EditionsConfig>(File.ReadAllText(PathConfig.EditionConfig));
                foreach (var edition in editions)
                {
                    // NOTE: There's no need to slow down similar builds; disable Docker cache only once.
                    var noCache = (edition == Edition.StandardTag) ? " --no-cache" : String.Empty;
                    var editionData = GetEditionData(allConfig, edition);
                    File.WriteAllText(PathConfig.ComposePath, new DockerCompose(edition).TransformText());
                    File.WriteAllText(PathConfig.OverridePath, new DockerComposeOverride(edition).TransformText());
                    File.WriteAllText(files[(int)ApiServerPathIndex.Edition], editionData);
                    File.WriteAllText(files[(int)ApiServerPathIndex.DevEdition], editionData);

                    OutputProvider($"Building tag {edition}...");
                    _runner.Run(String.Format($"docker-compose -f {PathConfig.OverridePath} -f {PathConfig.ComposePath} build{noCache} ApiServer"));
                }

                OutputProvider($"[Api Server] => Rebuild succeeded.{Environment.NewLine}");
            }
            finally
            {
                FileUtility.RestoreFiles(files, tempPath);
                Directory.Delete(tempPath);
            }
        }

        private void RebuildWebApp()
        {
            // If web app is modified, backup production and development environment files,
            // generate dummy environments, build base image and restore environment files
            OutputProvider("Rebuilding web app...");
            string tempPath = String.Empty;
            string envPath = String.Empty;
            string devEnvPath = String.Empty;
            try
            {
                tempPath = FileUtility.GetTempFolderPath();
                envPath = Path.Combine(PathConfig.WebEnvRoot, "environment.prod.ts");
                FileUtility.BackupFile(envPath, tempPath);
                devEnvPath = Path.Combine(PathConfig.WebEnvRoot, "environment.ts");
                FileUtility.BackupFile(devEnvPath, tempPath);

                var settings = BuildSettings.DockerDummy;
                settings.Version = VersionUtility.GetAppVersion();
                var environment = new NgEnvironment(settings).TransformText();
                File.WriteAllText(envPath, environment);
                File.WriteAllText(devEnvPath, environment);

                _runner.Run(String.Format($"docker-compose -f {PathConfig.OverridePath} -f {PathConfig.ComposePath} build --no-cache WebApp"));
                OutputProvider($"[Web App] => Rebuild succeeded.{Environment.NewLine}");
            }
            finally
            {
                FileUtility.RestoreFile(envPath, tempPath);
                FileUtility.RestoreFile(devEnvPath, tempPath);
                Directory.Delete(tempPath);
            }
        }

        private void RebuildDbServer()
        {
            OutputProvider("Rebuilding db server...");
            _runner.Run(String.Format($"docker-compose -f {PathConfig.OverridePath} -f {PathConfig.ComposePath} build --no-cache DbServer"));
            OutputProvider($"[Db Server] => Rebuild succeeded.{Environment.NewLine}");
        }

        private void UpdateServiceCache(string serviceName, string tag = "latest")
        {
            var currentDir = Environment.CurrentDirectory;
            var root = FileUtility.GetAbsolutePath(PathConfig.DockerCacheRoot);
            var imageFile = GetImageFileName(serviceName, tag);
            Environment.CurrentDirectory = Path.Combine(root, serviceName);
            _runner.Run(String.Format($"docker save msn1368/{serviceName}:{tag} -o {imageFile}.tar"));
            var tarPath = Path.Combine(Environment.CurrentDirectory, String.Format($"{imageFile}.tar"));
            var gzPath = Path.Combine(Environment.CurrentDirectory, String.Format($"{imageFile}.tar.gz"));
            if (File.Exists(gzPath))
            {
                File.Delete(gzPath);
            }

            _archive.GZip(tarPath);
            Environment.CurrentDirectory = currentDir;
        }

        private readonly CliRunner _runner = new();
        private readonly ArchiveUtility _archive = new();
        private EventHandler<OutputReceivedEventArgs> _outputRedirector;
        private bool _licenseChanged, _apiChanged, _dbChanged, _appChanged;
    }

    public delegate void OutputProviderDelegate(string message);
}
