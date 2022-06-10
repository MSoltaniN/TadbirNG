﻿using System;
using System.IO;
using SPPC.Framework.Extensions;
using SPPC.Framework.Helpers;
using SPPC.Tools.Model;
using SPPC.Tools.Utility;

namespace SPPC.Tools.DeliveryCli
{
    class Program
    {
        static void Main(string[] args)
        {
            ReleaseUtility.RestoreSettings();
            DoPublishProcess();
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
                return;
            }

            var password = InputUtility.QueryZipFilePassword();
            var license = InputUtility.QueryLicense();
            if (license == null)
            {
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
                Console.WriteLine("(30% completed)");
                Console.WriteLine();

                Console.WriteLine("Publishing images to Docker Hub...");
                var guid = license.LicenseKey.Substring(0, 8);
                _runner.Run(String.Format(PushLicenseTemplate, guid));
                Console.WriteLine("(45% completed)");
                Console.WriteLine();

                _runner.Run(String.Format(PushApiTemplate, guid));
                Console.WriteLine("(60% completed)");
                Console.WriteLine();

                _runner.Run(String.Format(PushAppTemplate, guid));
                Console.WriteLine("(75% completed)");
                Console.WriteLine();

                _runner.Run("docker push msn1368/db-server:dev");
                Console.WriteLine("(90% completed)");
                Console.WriteLine();

                Console.WriteLine("Building customer delivery file...");
                ReleaseUtility.CreateReleaseArchive(license.LicenseKey, password);
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
                ReleaseUtility.RestoreSettings();
            }
        }

        const string PushLicenseTemplate = "docker push msn1368/license-server-{0}:dev";
        const string PushApiTemplate = "docker push msn1368/api-server-{0}:latest";
        const string PushAppTemplate = "docker push msn1368/web-app-{0}:dev";
        static readonly CliRunner _runner = new();
    }
}
