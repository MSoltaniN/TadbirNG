using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using SPPC.Framework.Helpers;
using SPPC.Tadbir.Configuration.Models;
using SPPC.Tools.Model;
using SPPC.Tools.Transforms.Templates;
using SPPC.Tools.Utility;

namespace SPPC.Tools.BuildServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var stopWatch = new Stopwatch();
            DisplayBanner();
            if (!DockerUtility.IsDockerEngineRunning())
            {
                Console.WriteLine("ERROR: Build process needs Docker engine to be up and running.");
                Console.WriteLine(
                    "Please make sure Docker Desktop is running and you are logged into Docker Hub repository.");
                return;
            }

            _runner.OutputReceived += Runner_OutputReceived;
            stopWatch.Start();

            RunBuildProcess();
            RunPublishProcess();

            stopWatch.Stop();
            Console.WriteLine($"Elapsed time : {stopWatch.Elapsed}");
        }

        private static void RunBuildProcess()
        {
            // NOTE: For this process to be correct, current clone MUST be pristine
            // (i.e. NO change must be sensed by git)
            // NOTE: ALL builds must be done with caching disabled (--no-cache switch)
            // Calculate current checksums for all Docker projects...
            _licenseChecksum = ChecksumUtility.CalculateChecksum(DockerService.LicenseServer);
            _apiChecksum = ChecksumUtility.CalculateChecksum(DockerService.ApiServer);
            _appChecksum = ChecksumUtility.CalculateChecksum(DockerService.WebApp);
            _dbChecksum = ChecksumUtility.CalculateChecksum(DockerService.DbServer);

            // Pull latest changes from TadbirNG repository...
            var currentDir = Environment.CurrentDirectory;
            Environment.CurrentDirectory = GetRepositoryRoot();
            ////_runner.Run(String.Format(GitUserNameCommand, GitUserName));
            ////_runner.Run(String.Format(GitEmailCommand, GitEmail));
            _runner.Run("git pull --progress");
            Environment.CurrentDirectory = currentDir;

            if (IsModifiedProject(DockerService.LicenseServer, _licenseChecksum))
            {
                RebuildLicenseServer();
            }
            else
            {
                Console.WriteLine($"{DockerService.LicenseServer} is up-to-date.");
            }

            if (IsModifiedProject(DockerService.ApiServer, _apiChecksum))
            {
                RebuildApiServer();
            }
            else
            {
                Console.WriteLine($"{DockerService.ApiServer} is up-to-date.");
            }

            if (IsModifiedProject(DockerService.WebApp, _appChecksum))
            {
                RebuildWebApp();
            }
            else
            {
                Console.WriteLine($"{DockerService.WebApp} is up-to-date.");
            }

            if (IsModifiedProject(DockerService.DbServer, _dbChecksum))
            {
                RebuildDbServer();
            }
            else
            {
                Console.WriteLine($"{DockerService.DbServer} is up-to-date.");
            }

            Console.WriteLine();
            Console.WriteLine("Build process completed successfully.");
        }

        public static void DisplayBanner()
        {
            Console.WriteLine();
            Console.WriteLine("============================================================");
            Console.WriteLine("TadbirNG Build Server (v1.2)");
            Console.WriteLine($"(c) Copyright {DateTime.Now.Year}, SPPC, All Rights Reserved");
            Console.WriteLine("============================================================");
            Console.WriteLine();
        }

        private static string GetRepositoryRoot()
        {
            var currentDir = Environment.CurrentDirectory;
            return Path.GetDirectoryName(
                Path.GetDirectoryName(
                    Path.GetDirectoryName(currentDir)));
        }

        private static bool IsModifiedProject(string project, string oldChecksum)
        {
            var newChecksum = ChecksumUtility.CalculateChecksum(project);
            return newChecksum != oldChecksum;
        }

        private static void RebuildLicenseServer()
        {
            // If license server is modified, backup appSettings, generate dummy settings,
            // build base image and restore appSettings...
            Console.WriteLine();
            Console.WriteLine("Rebuilding license server...");
            Console.WriteLine();
            string tempPath = String.Empty;
            string settingsPath = String.Empty;
            string devSettingsPath = String.Empty;
            try
            {
                tempPath = FileUtility.GetTempFolderPath();
                settingsPath = Path.Combine(PathConfig.LocalServerRoot, "appSettings.json");
                FileUtility.BackupFile(settingsPath, tempPath);
                devSettingsPath = Path.Combine(PathConfig.LocalServerRoot, "appSettings.Development.json");
                FileUtility.BackupFile(devSettingsPath, tempPath);

                var settings = BuildSettings.DockerDummy;
                var appSettings = new LocalLicenseApiSettings(settings).TransformText();
                File.WriteAllText(settingsPath, appSettings);
                File.WriteAllText(devSettingsPath, appSettings);

                _runner.Run(String.Format($"docker-compose -f {PathConfig.OverridePath} -f {PathConfig.ComposePath} build --no-cache LicenseServer"));
                Console.WriteLine();
                Console.WriteLine("[License Server] => Rebuild succeeded.");
                Console.WriteLine();
            }
            finally
            {
                FileUtility.RestoreFile(settingsPath, tempPath);
                FileUtility.RestoreFile(devSettingsPath, tempPath);
                Directory.Delete(tempPath);
            }
        }

        private static void RebuildApiServer()
        {
            // If api server is modified :
            // Backup docker-compose, override, appSettings, license and edition files
            // Generate dummy appSettings and dummy license
            // For each edition (std, pro and ent) :
            // 1. Generate docker-compose, override and edition file
            // 2. Build base image for that edition
            // Restore docker-compose, override, appSettings, license and edition files
            Console.WriteLine();
            string tempPath = String.Empty;
            var files = new string[]
            {
                PathConfig.ComposePath, PathConfig.OverridePath,
                Path.Combine(PathConfig.WebApiRoot, "appSettings.json"),
                Path.Combine(PathConfig.WebApiRoot, "appSettings.Development.json"),
                Path.Combine(PathConfig.WebApiRoot, "wwwroot", "edition"),
                Path.Combine(PathConfig.WebApiRoot, "wwwroot", "edition.Development.json"),
                Path.Combine(PathConfig.WebApiRoot, "wwwroot", "license"),
                Path.Combine(PathConfig.WebApiRoot, "wwwroot", "license.Development.json"),
            };
            try
            {
                var editions = new string[] { "std", "pro", "ent" };
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
                    var noCache = (edition == "std") ? " --no-cache" : String.Empty;
                    var editionData = GetEditionData(allConfig, edition);
                    File.WriteAllText(PathConfig.ComposePath, new DockerCompose(edition).TransformText());
                    File.WriteAllText(PathConfig.OverridePath, new DockerComposeOverride(edition).TransformText());
                    File.WriteAllText(files[(int)ApiServerPathIndex.Edition], editionData);
                    File.WriteAllText(files[(int)ApiServerPathIndex.DevEdition], editionData);

                    Console.WriteLine($"Building tag {edition}...");
                    _runner.Run(String.Format($"docker-compose -f {PathConfig.OverridePath} -f {PathConfig.ComposePath} build{noCache} ApiServer"));
                }

                Console.WriteLine();
                Console.WriteLine("[Api Server] => Rebuild succeeded.");
                Console.WriteLine();
            }
            finally
            {
                FileUtility.RestoreFiles(files, tempPath);
                Directory.Delete(tempPath);
            }
        }

        private static void RebuildWebApp()
        {
            // If web app is modified, backup production and development environment files,
            // generate dummy environments, build base image and restore environment files
            Console.WriteLine();
            Console.WriteLine("Rebuilding web app...");
            Console.WriteLine();
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
                settings.Version = VersionInfo.GetAppVersion();
                var environment = new NgEnvironment(settings).TransformText();
                File.WriteAllText(envPath, environment);
                File.WriteAllText(devEnvPath, environment);

                _runner.Run(String.Format($"docker-compose -f {PathConfig.OverridePath} -f {PathConfig.ComposePath} build --no-cache WebApp"));
                Console.WriteLine();
                Console.WriteLine("[Web App] => Rebuild succeeded.");
                Console.WriteLine();
            }
            finally
            {
                FileUtility.RestoreFile(envPath, tempPath);
                FileUtility.RestoreFile(devEnvPath, tempPath);
                Directory.Delete(tempPath);
            }
        }

        private static void RebuildDbServer()
        {
            Console.WriteLine();
            Console.WriteLine("Rebuilding db server...");
            Console.WriteLine();

            _runner.Run(String.Format($"docker-compose -f {PathConfig.OverridePath} -f {PathConfig.ComposePath} build --no-cache DbServer"));
            Console.WriteLine();
            Console.WriteLine("[Db Server] => Rebuild succeeded.");
            Console.WriteLine();
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

        private static void RunPublishProcess()
        {
            // Update cached image files in DockerCache repository
            ArchiveUtility.RedirectOutput();
            Console.WriteLine("Updating cache for Db Server...");
            UpdateServiceCache("db-server");
            Console.WriteLine("Updating cache for License Server...");
            UpdateServiceCache("license-server");
            Console.WriteLine("Updating cache for Web App...");
            UpdateServiceCache("web-app", "dev");
            Console.WriteLine("Updating cache for Api Server...");
            UpdateServiceCache("api-server", "std");
            UpdateServiceCache("api-server", "pro");
            UpdateServiceCache("api-server", "ent");

            // Commit changes to DockerCache remote
            Console.WriteLine("Commiting changes to git remote...");
            var message = String.Format($"Pushed builds {VersionInfo.GetApiVersion()},UI-{VersionInfo.GetAppVersion()}");
            _runner.Run(String.Format(GitCommitCommand, message));
            _runner.Run("git push --progress");
            Console.WriteLine();
            Console.WriteLine("Publish process completed successfully.");
        }

        private static void UpdateServiceCache(string serviceName, string tag = "latest")
        {
            var currentDir = Environment.CurrentDirectory;
            var root = GetDockerCacheRoot();
            var imageFile = GetImageFileName(serviceName, tag);
            Environment.CurrentDirectory = Path.Combine(root, serviceName);
            _runner.Run(String.Format($"docker save msn1368/{serviceName}:{tag} -o {imageFile}.tar"));
            var tarPath = Path.Combine(Environment.CurrentDirectory, String.Format($"{imageFile}.tar"));
            var gzPath = Path.Combine(Environment.CurrentDirectory, String.Format($"{imageFile}.tar.gz"));
            if (File.Exists(gzPath))
            {
                File.Delete(gzPath);
            }

            ArchiveUtility.GZip(tarPath);
            Environment.CurrentDirectory = currentDir;
        }

        private static string GetDockerCacheRoot()
        {
            // NOTE: This ugly syntax is required, because current directory must be an absolute path.
            var root = Path.GetDirectoryName(
                Path.GetDirectoryName(
                    Path.GetDirectoryName(
                        Path.GetDirectoryName(Environment.CurrentDirectory))));
            return Path.Combine(root, "dockercache");
        }

        private static string GetImageFileName(string serviceName, string tag)
        {
            var editionTags = new string[] { "std", "pro", "ent" };
            return editionTags.Contains(tag)
                ? String.Format($"{serviceName}-{tag}")
                : serviceName;
        }

        private static void Runner_OutputReceived(object sender, OutputReceivedEventArgs e)
        {
            if (!String.IsNullOrEmpty(e.Output))
            {
                Console.WriteLine(e.Output.Replace("\n", Environment.NewLine));
                _logBuilder.AppendLine(e.Output.Replace("\n", Environment.NewLine));
                if (_logBuilder.Length >= FlushLimit)
                {
                    var chunk = _logBuilder.ToString().Substring(0, FlushLimit);
                    File.AppendAllText("build.log", chunk);
                    _logBuilder.Remove(0, FlushLimit);
                }
            }
        }

        ////private const string GitUserName = "build-daemon";
        ////private const string GitEmail = "babakesl@hotmail.com";
        ////private const string GitUserNameCommand = "git config --global user.name \"{0}\"";
        ////private const string GitEmailCommand = "git config --global user.email \"{0}\"";
        private const int FlushLimit = 65536; // i.e. 64KB
        private const string GitCommitCommand = "git commit -a -m \"{0}\"";
        private static readonly CliRunner _runner = new();
        private static readonly StringBuilder _logBuilder = new();
        private static string _licenseChecksum;
        private static string _apiChecksum;
        private static string _appChecksum;
        private static string _dbChecksum;
    }
}
