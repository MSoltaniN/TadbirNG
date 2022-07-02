using System;
using System.Diagnostics;
using System.IO;
using SPPC.Framework.Extensions;
using SPPC.Framework.Helpers;
using SPPC.Licensing.Model;
using SPPC.Tools.Model;
using SPPC.Tools.Utility;

namespace SPPC.Tools.DeliveryCli
{
    class Program
    {
        static void Main(string[] args)
        {
            ////DoPublishProcess();
            ////CreateChecksumFiles();
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
            setup.ConfigureService();

            // Configure api server...
            Console.WriteLine("Configuring api server...");
            setup = new ApiServiceSetup(settings);
            setup.ConfigureService();

            stopwatch.Stop();
            Console.WriteLine("Successfully configured two services.");
            Console.WriteLine($"Elapsed time : {stopwatch.Elapsed}");
            Console.ReadLine();
        }

        static bool IsDefaultLicense(LicenseModel license)
        {
            return license.Id == LocalLicenseId || license.Id == PublishLicenseId;
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
    }
}
