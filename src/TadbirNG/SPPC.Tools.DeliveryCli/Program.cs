using System;
using System.Diagnostics;
using System.IO;
using SPPC.Framework.Helpers;
using SPPC.Tadbir.Configuration.Models;
using SPPC.Tools.Model;
using SPPC.Tools.Transforms.Templates;
using SPPC.Tools.Utility;

namespace SPPC.Tools.DeliveryCli
{
    class Program
    {
        static void Main(string[] args)
        {
            RunBuildProcess();
        }

        private static void RunBuildProcess()
        {
            var stopWatch = new Stopwatch();
            InputUtility.DisplayBanner();
            if (!DockerUtility.IsDockerEngineRunning())
            {
                Console.WriteLine("ERROR: Build process needs Docker engine to be up and running.");
                Console.WriteLine(
                    "Please make sure Docker Desktop is running and you are logged into Docker Hub repository.");
                return;
            }

            _runner.OutputReceived += Runner_OutputReceived;
            stopWatch.Start();

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

            stopWatch.Stop();
            Console.WriteLine($"Elapsed time : {stopWatch.Elapsed}");
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
        }

        private static void UpdateServiceCache(string serviceName, string tag = "latest")
        {
            var currentDir = Environment.CurrentDirectory;
            Environment.CurrentDirectory = GetDockerCacheRoot();
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

        private static void Runner_OutputReceived(object sender, OutputReceivedEventArgs e)
        {
            if (!String.IsNullOrEmpty(e.Output))
            {
                Console.WriteLine(e.Output.Replace("\n", Environment.NewLine));
            }
        }

        private static readonly CliRunner _runner = new();
        private static string _licenseChecksum;
        private static string _apiChecksum;
        private static string _appChecksum;
        private static string _dbChecksum;
    }
}
