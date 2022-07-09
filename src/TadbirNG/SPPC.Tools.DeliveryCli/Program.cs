using System;
using System.Diagnostics;
using System.IO;
using SPPC.Framework.Cryptography;
using SPPC.Framework.Extensions;
using SPPC.Framework.Helpers;
using SPPC.Licensing.Model;
using SPPC.Tools.Model;
using SPPC.Tools.Transforms.Templates;
using SPPC.Tools.Utility;

namespace SPPC.Tools.DeliveryCli
{
    class Program
    {
        static void Main(string[] args)
        {
            ////DoPublishProcess();
            ////CreateChecksumFiles();
            ////GeneratePasswords();
            ////CalculateOldNewHashes();
            RunBuildProcess();
        }

        private static void RunBuildProcess()
        {
            var stopWatch = new Stopwatch();
            InputUtility.DisplayBanner();
            if (!CommonUtility.IsDockerEngineRunning())
            {
                Console.WriteLine("ERROR: Delivery process needs Docker engine to be up and running.");
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

            // Calculate new checksums for all Docker projects...

            // If license server is modified, backup appSettings, generate dummy settings,
            // build base image and restore appSettings...
            if (IsModifiedProject(DockerService.LicenseServer, _licenseChecksum))
            {
                RebuildLicenseServer();
            }
            else
            {
                Console.WriteLine($"{DockerService.LicenseServer} is up-to-date.");
            }

            // If api server is modified :
            // Backup docker-compose, override, appSettings, license and edition files
            // Generate dummy appSettings and dummy license
            // For each edition (std, pro and ent) :
            // 1. Generate docker-compose, override and edition file
            // 2. Build base image for that edition
            // Restore docker-compose, override, appSettings, license and edition files
            if (IsModifiedProject(DockerService.ApiServer, _apiChecksum))
            {
                RebuildApiServer();
            }
            else
            {
                Console.WriteLine($"{DockerService.ApiServer} is up-to-date.");
            }

            // If web app is modified, backup production and development environment files,
            // generate dummy environments, build base image and restore environment files
            if (IsModifiedProject(DockerService.WebApp, _appChecksum))
            {
                RebuildWebApp();
            }
            else
            {
                Console.WriteLine($"{DockerService.WebApp} is up-to-date.");
            }

            // If db server is modified, build main image
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
                tempPath = GetTempFolderPath();
                settingsPath = Path.Combine(PathConfig.LocalServerRoot, "appSettings.json");
                BackupFile(settingsPath, tempPath);
                devSettingsPath = Path.Combine(PathConfig.LocalServerRoot, "appSettings.Development.json");
                BackupFile(devSettingsPath, tempPath);

                var settings = BuildSettings.DockerDummy;
                var appSettings = new LocalLicenseApiSettings(settings).TransformText();
                File.WriteAllText(settingsPath, appSettings);
                File.WriteAllText(devSettingsPath, appSettings);

                var composePath = Path.Combine(PathConfig.SolutionRoot, "docker-compose.yml");
                var overridePath = Path.Combine(PathConfig.SolutionRoot, "docker-compose.override.yml");
                _runner.Run(String.Format($"docker-compose -f {overridePath} -f {composePath} build --no-cache LicenseServer"));
                Console.WriteLine();
                Console.WriteLine("License server rebuild succeeded.");
                Console.WriteLine();
            }
            finally
            {
                RestoreFile(settingsPath, tempPath);
                RestoreFile(devSettingsPath, tempPath);
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
            Console.WriteLine("Rebuilding api server...");
            Console.WriteLine();
            string tempPath = String.Empty;
            var files = new string[]
            {
                Path.Combine(PathConfig.SolutionRoot, "docker-compose.yml"),
                Path.Combine(PathConfig.SolutionRoot, "docker-compose.override.yml"),
                Path.Combine(PathConfig.WebApiRoot, "appSettings.json"),
                Path.Combine(PathConfig.WebApiRoot, "appSettings.Development.json"),
                Path.Combine(PathConfig.WebApiRoot, "wwwroot", "edition"),
                Path.Combine(PathConfig.WebApiRoot, "wwwroot", "edition.Development.json"),
                Path.Combine(PathConfig.WebApiRoot, "wwwroot", "license"),
                Path.Combine(PathConfig.WebApiRoot, "wwwroot", "license.Development.json"),
            };
            try
            {
                tempPath = GetTempFolderPath();
                BackupFiles(files, tempPath);

                var settings = BuildSettings.DockerDummy;
                var appSettings = new WebApiSettings(settings).TransformText();
                File.WriteAllText(files[(int)ApiServerPathIndex.Settings], appSettings);
                File.WriteAllText(files[(int)ApiServerPathIndex.DevSettings], appSettings);

                var composePath = files[(int)ApiServerPathIndex.Compose];
                var overridePath = files[(int)ApiServerPathIndex.Override];
                _runner.Run(String.Format($"docker-compose -f {overridePath} -f {composePath} build --no-cache ApiServer"));
                Console.WriteLine();
                Console.WriteLine("Api server rebuild succeeded.");
                Console.WriteLine();
            }
            finally
            {
                RestoreFiles(files, tempPath);
                Directory.Delete(tempPath);
            }
        }

        private static void RebuildWebApp()
        {
            Console.WriteLine("Rebuilding web app...");
        }

        private static void RebuildDbServer()
        {
            Console.WriteLine("Rebuilding db server...");
        }

        private static string GetTempFolderPath()
        {
            var tempFile = Path.GetTempFileName();
            var tempFolder = String.Format($"bla-{Path.GetFileName(tempFile)}");
            File.Delete(tempFile);
            var tempPath = Path.Combine(Path.GetDirectoryName(tempFile), tempFolder);
            if (!Directory.Exists(tempPath))
            {
                Directory.CreateDirectory(tempPath);
            }

            return tempPath;
        }

        private static void BackupFile(string filePath, string tempPath)
        {
            if (File.Exists(filePath))
            {
                File.Copy(filePath, Path.Combine(tempPath, Path.GetFileName(filePath)));
                File.Delete(filePath);
            }
        }

        private static void RestoreFile(string filePath, string tempPath)
        {
            string backupPath = Path.Combine(tempPath, Path.GetFileName(filePath));
            if (File.Exists(backupPath))
            {
                File.Copy(backupPath, filePath, true);
                File.Delete(backupPath);
            }
        }

        private static void BackupFiles(string[] files, string tempPath)
        {
            Array.ForEach(files, file => BackupFile(file, tempPath));
        }

        private static void RestoreFiles(string[] files, string tempPath)
        {
            Array.ForEach(files, file => RestoreFile(file, tempPath));
        }

        private static void RunPublishProcess()
        {
        }

        private static void CalculateOldNewHashes()
        {
            var crypto = new CryptoService(new CertificateManager());
            var oldHash = crypto.CreateHash(File.ReadAllBytes(@"D:\GitHub\babaksoft\Projects\SPPC\dockercache\bla\9f23bd2e7b973bace914883cd39c12bb9e51659440806b67ca23036d108e4a8b\layer.tar")).ToLower();
            var newHash = crypto.CreateHash(File.ReadAllBytes(@"D:\GitHub\babaksoft\Projects\SPPC\dockercache\web-app\9f23bd2e7b973bace914883cd39c12bb9e51659440806b67ca23036d108e4a8b\layer.tar")).ToLower();
            Console.WriteLine($"Old Hash : {oldHash}");
            Console.WriteLine($"New Hash : {newHash}");
            Console.ReadLine();
        }

        private static void Runner_OutputReceived(object sender, OutputReceivedEventArgs e)
        {
            if (!String.IsNullOrEmpty(e.Output))
            {
                Console.WriteLine(e.Output.Replace("\n", Environment.NewLine));
            }
        }

        private static void DoPublishProcess()
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            InputUtility.DisplayBanner();
            if (!CommonUtility.IsDockerEngineRunning())
            {
                Console.WriteLine("ERROR: Delivery process needs Docker engine to be up and running.");
                Console.WriteLine(
                    "Please make sure Docker Desktop is running and you are logged into Docker Hub repository.");
                return;
            }

            _runner.OutputReceived += Runner_OutputReceived;
            if (!InputUtility.ConfirmLicenseId())
            {
                stopWatch.Stop();
                return;
            }

            var license = InputUtility.QueryLicense();
            if (license == null)
            {
                stopWatch.Stop();
                return;
            }

            try
            {
                Console.WriteLine("Generating settings for the new release...");
                ReleaseUtility.GenerateSettings(license);
                Console.WriteLine("(5% completed)");
                Console.WriteLine();

                Console.WriteLine("Building Docker images...");
                var composePath = Path.Combine(PathConfig.SolutionRoot, "docker-compose.yml");
                var overridePath = Path.Combine(PathConfig.SolutionRoot, "docker-compose.override.yml");
                _runner.Run(String.Format($"docker-compose -f {overridePath} -f {composePath} build"));
                Console.WriteLine("(20% completed)");
                Console.WriteLine();

                Console.WriteLine("Publishing images to Docker Hub...");
                var separator = IsDefaultLicense(license) ? String.Empty : "-";
                var guid = IsDefaultLicense(license) ? String.Empty : license.LicenseKey.Substring(0, 8);
                _runner.Run(String.Format(PushLicenseTemplate, separator, guid));
                Console.WriteLine("(40% completed)");
                Console.WriteLine();

                _runner.Run(String.Format(PushApiTemplate, separator, guid));
                Console.WriteLine("(60% completed)");
                Console.WriteLine();

                _runner.Run(String.Format(PushAppTemplate, separator, guid));
                Console.WriteLine("(80% completed)");
                Console.WriteLine();

                _runner.Run("docker push msn1368/db-server");
                Console.WriteLine("(100% completed)");
                Console.WriteLine();
                Console.WriteLine("Customer delivery completed successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occured while delivering customer release.");
                Console.WriteLine(ex.GetErrorInfo());
            }
            finally
            {
                ReleaseUtility.RestoreSettings(license.Id);
                stopWatch.Stop();
            }

            Console.WriteLine(String.Format($"Elapsed Time : {stopWatch.Elapsed}"));
        }

        private static void DoConfigureProcess()
        {
            var settings = BuildSettings.DockerLocal;
            settings.DbServerName = "host.docker.internal";
            settings.DbUserName = "Teymour";
            settings.DbPassword = "MyBlaBla123456";
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            // Configure license server...
            Console.WriteLine("Configuring license server...");
            DockerServiceSetup setup = new LicenseServiceSetup(settings);
            setup.ConfigureService(null);

            // Configure api server...
            Console.WriteLine("Configuring api server...");
            setup = new ApiServiceSetup(settings);
            setup.ConfigureService(null);

            stopwatch.Stop();
            Console.WriteLine("Successfully configured two services.");
            Console.WriteLine($"Elapsed time : {stopwatch.Elapsed}");
            Console.ReadLine();
        }

        static bool IsDefaultLicense(LicenseModel license)
        {
            return license.Id == LocalLicenseId || license.Id == PublishLicenseId;
        }

        static void GeneratePasswords()
        {
            int length = 1;
            while (length > 0)
            {
                Console.Write("Length (0 to quit): ");
                var input = Console.ReadLine();
                if (Int32.TryParse(input, out length))
                {
                    if (length > 0)
                    {
                        Console.WriteLine($"Password : {PasswordGenerator.Generate(length)}");
                    }
                }
                else
                {
                    length = 10;
                    Console.WriteLine($"Password : {PasswordGenerator.Generate()}");
                }
            }
        }

        static void CreateChecksumFiles()
        {
            var path = Path.Combine(PathConfig.WebApiRoot, "checksum");
            File.WriteAllText(path, ChecksumUtility.CalculateChecksum(DockerService.ApiServer));
            path = Path.Combine(PathConfig.LocalServerRoot, "checksum");
            File.WriteAllText(path, ChecksumUtility.CalculateChecksum(DockerService.LicenseServer));
            path = Path.Combine(PathConfig.ResourceRoot, "checksum");
            File.WriteAllText(path, ChecksumUtility.CalculateChecksum(DockerService.DbServer));
            path = Path.Combine(PathConfig.WebAppRoot, "checksum");
            File.WriteAllText(path, ChecksumUtility.CalculateChecksum(DockerService.WebApp));
        }

        const string PushLicenseTemplate = "docker push msn1368/license-server{0}{1}";
        const string PushApiTemplate = "docker push msn1368/api-server{0}{1}";
        const string PushAppTemplate = "docker push msn1368/web-app{0}{1}:dev";
        const int LocalLicenseId = 5;     // Used for building base (no-suffix) images on my local system
        const int PublishLicenseId = 18;  // Used for building base (no-suffix) images on Linux test bed
        static readonly CliRunner _runner = new();

        private static string _licenseChecksum;
        private static string _apiChecksum;
        private static string _appChecksum;
        private static string _dbChecksum;
    }

    internal enum ApiServerPathIndex
    {
        Compose = 0,
        Override = 1,
        Settings = 2,
        DevSettings = 3,
        Edition = 4,
        DevEdition = 5,
        License = 6,
        DevLicense = 7
    }
}
