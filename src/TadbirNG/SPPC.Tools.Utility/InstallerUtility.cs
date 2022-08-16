using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Data.SqlClient;
using SPPC.Framework.Common;
using SPPC.Framework.Cryptography;
using SPPC.Framework.Helpers;
using SPPC.Framework.Persistence;
using SPPC.Tadbir.Configuration;
using SPPC.Tools.Model;

namespace SPPC.Tools.Utility
{
    public class InstallerUtility
    {
        public static string DockerPath { get; set; }

        public static bool VerifyChecksums()
        {
            string root = Path.Combine(ChecksumRoot, "runner");
            string checksumFile = Path.Combine(ChecksumRoot, "runner.sha");
            if (!VerifyChecksums(root, checksumFile))
            {
                return false;
            }

            root = Path.Combine(ChecksumRoot, "service");
            checksumFile = Path.Combine(ChecksumRoot, "service.sha");
            return VerifyChecksums(root, checksumFile);
        }

        public static string[] GetDbLoginNames(string server, string password)
        {
            var loginNames = Array.Empty<string>();
            try
            {
                var csBuilder = new SqlConnectionStringBuilder()
                {
                    DataSource = server,
                    InitialCatalog = "master",
                    UserID = "sa",
                    Password = password
                };
                var dal = new SqlDataLayer(csBuilder.ConnectionString);
                var result = dal.Query(DbLoginQuery, CommandType.Text, 120);
                loginNames = result?.Rows
                    .Cast<DataRow>()
                    .Select(row => row[0].ToString())
                    .Prepend("Default")
                    .ToArray();
            }
            catch
            {
            }

            return loginNames;
        }

        public static void CreateInstallationPath(string path)
        {
            var items = new List<string> { path };
            var dirName = Path.GetDirectoryName(path);
            while (dirName != null)
            {
                items.Add(dirName);
                dirName = Path.GetDirectoryName(dirName);
            }

            items.Reverse();
            foreach (var item in items)
            {
                EnsureDirectoryExists(item);
            }

            EnsureDirectoryExists(Path.Combine(path, "runner"));
            EnsureDirectoryExists(Path.Combine(path, "service"));
            EnsureDirectoryExists(Path.Combine(path, "setup"));
            EnsureDirectoryExists(Path.Combine(path, "tools"));
        }

        public static void CopyFiles(string path, IBuildSettings settings, bool createShortcut = true)
        {
            var config = CryptoService.Default.Encrypt(JsonHelper.From(settings));
            File.WriteAllText(Path.Combine(path, "config"), config);
            File.Copy(Path.Combine(ChecksumRoot, "version"), Path.Combine(path, "version"));
            File.Copy(Path.Combine(ChecksumRoot, "license"), Path.Combine(path, "license"));
            CopyFilesIfMissing(Path.Combine(ChecksumRoot, "runner"), Path.Combine(path, "runner"));
            CopyFilesIfMissing(Path.Combine(ChecksumRoot, "service"), Path.Combine(path, "service"));
            CopyFilesIfMissing(Path.Combine(ChecksumRoot, "setup"), Path.Combine(path, "setup"));
            CopyFilesIfMissing(Path.Combine(ChecksumRoot, "tools"), Path.Combine(path, "tools"));
            if (createShortcut)
            {
                // Create shortcut to main Runner executable on user's Desktop folder...
                string exePath = Path.Combine(path, "runner", "Tadbir.exe");
                CreateDesktopShortcut(exePath, AppTitle, AppTitle);
            }
        }

        public static void DeleteFiles()
        {
            Array.ForEach(new DirectoryInfo("..").GetFiles("*.*", SearchOption.TopDirectoryOnly),
                fi => File.Delete(fi.FullName));
            var path = Path.Combine("..", "runner");
            FileUtility.DeleteFolder(path);
            path = Path.Combine("..", "service");
            FileUtility.DeleteFolder(path);
            path = Path.Combine("..", "tools");
            FileUtility.DeleteFolder(path);
            DeleteDesktopShortcut(AppTitle);
        }

        public static bool InstallService(string path)
        {
            bool installed = true;
            var runner = new CliRunner();
            var output = runner.Run("sc query sppckeysrv");
            var lines = output.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            if (!lines[0].Contains("SERVICE_NAME: sppckeysrv"))
            {
                string template =
                    "sc create sppckeysrv type= own start= auto error= normal displayname= \"SPPC Key Server\" binpath= \"{0}\"";
                string binPath = Path.Combine(path, "service", "KeyServer.exe");
                output = runner.Run(String.Format(template, binPath));
                lines = output.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
                installed = !lines[0].Contains("FAILED");
            }

            return installed;
        }

        public static bool UninstallService()
        {
            // NOTE: Because SC (Service Control Manager) locks service executable for a while after
            // deleting the service, we need some delay here, because service folder must be deleted.
            var runner = new CliRunner();
            var output = runner.Run("sc delete sppckeysrv");
            var lines = output.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            return !lines[0].Contains("FAILED");
        }

        public static bool StartService()
        {
            bool started = false;
            var runner = new CliRunner();
            var output = runner.Run("sc start sppckeysrv");
            var lines = output.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            if (!lines[0].Contains("FAILED"))
            {
                do
                {
                    output = runner.Run("sc query sppckeysrv");
                    started = output
                        .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                        .Where(line => line.Contains("STATE") && line.Contains("RUNNING"))
                        .Any();
                } while (!started);
            }

            return started;
        }

        public static bool StopService()
        {
            bool stopped = false;
            var runner = new CliRunner();
            var output = runner.Run("sc stop sppckeysrv");
            var lines = output.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            if (!lines[0].Contains("FAILED"))
            {
                do
                {
                    output = runner.Run("sc query sppckeysrv");
                    stopped = output
                        .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                        .Where(line => line.Contains("STATE") && line.Contains("STOPPED"))
                        .Any();
                } while (!stopped);
            }

            return stopped;
        }

        public static void ConfigureDockerService(string root, string service, IBuildSettings settings)
        {
            var setup = GetServiceSetup(service, settings);
            setup.ConfigureService(root);
        }

        public static void RemoveDockerServices()
        {
            var currentDir = Environment.CurrentDirectory;
            Environment.CurrentDirectory = FileUtility.GetAbsolutePath(@"..\runner");
            var runner = new CliRunner();
            var output = runner.Run("docker-compose -f docker-compose.override.yml -f docker-compose.yml down");
            var imageName = $"{SysParameterUtility.GetImageFullName(SysParameterUtility.WebApp)}";
            output = runner.Run(String.Format(ToolConstants.DockerRemoveImageCommand, imageName));
            imageName = $"{SysParameterUtility.GetImageFullName(SysParameterUtility.LicenseServer)}";
            output = runner.Run(String.Format(ToolConstants.DockerRemoveImageCommand, imageName));
            imageName = $"{SysParameterUtility.GetImageFullName(SysParameterUtility.DbServer)}";
            output = runner.Run(String.Format(ToolConstants.DockerRemoveImageCommand, imageName));
            imageName = $"{SysParameterUtility.GetImageFullName(SysParameterUtility.ApiServer, Edition.StandardTag)}";
            output = runner.Run(String.Format(ToolConstants.DockerRemoveImageCommand, imageName));
            imageName = $"{SysParameterUtility.GetImageFullName(SysParameterUtility.ApiServer, Edition.ProfessionalTag)}";
            output = runner.Run(String.Format(ToolConstants.DockerRemoveImageCommand, imageName));
            imageName = $"{SysParameterUtility.GetImageFullName(SysParameterUtility.ApiServer, Edition.EnterpriseTag)}";
            output = runner.Run(String.Format(ToolConstants.DockerRemoveImageCommand, imageName));
            output = runner.Run(String.Format(
                ToolConstants.DockerRemoveVolumeCommand, $"runner_productdata_{SysParameterUtility.DbServer.Name}"));
            output = runner.Run(String.Format(
                ToolConstants.DockerRemoveVolumeCommand, $"runner_productdata_{SysParameterUtility.LicenseServer.Name}"));
            Environment.CurrentDirectory = currentDir;
        }

        public static bool IsAppRegistered()
        {
            var runner = new CliRunner();
            var output = runner.Run("sc query sppckeysrv");
            var lines = output.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            return !lines[0].Contains("FAILED");
        }

        public static bool IsAppRunning()
        {
            return Process
                .GetProcessesByName("Tadbir.exe")
                .Any();
        }

        public static string GetInstanceKey()
        {
            string key = null;
            var versionFile = new DirectoryInfo(ChecksumRoot)
                .GetFiles()
                .Where(file => file.Name.StartsWith("v"))
                .Select(file => file.FullName)
                .FirstOrDefault();
            if (versionFile != null)
            {
                key = File.ReadAllText(versionFile);
            }

            return key;
        }

        private static void CreateDesktopShortcut(string path, string name, string description)
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            string linkPath = Path.Combine(desktopPath, String.Format($"{name}.lnk"));
            if (File.Exists(linkPath))
            {
                File.Delete(linkPath);
            }

            var link = (IShellLink)new ShellLink();
            link.SetDescription(description);
            link.SetPath(path);
            link.SetWorkingDirectory(Path.GetDirectoryName(path));

            var file = (IPersistFile)link;
            file.Save(linkPath, false);
        }

        private static void DeleteDesktopShortcut(string name)
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            string linkPath = Path.Combine(desktopPath, String.Format($"{name}.lnk"));
            if (File.Exists(linkPath))
            {
                File.Delete(linkPath);
            }
        }

        private static void EnsureDirectoryExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        private static void CopyFilesIfMissing(string fromPath, string toPath)
        {
            Array.ForEach(new DirectoryInfo(fromPath).GetFiles(), file =>
            {
                if (!File.Exists(Path.Combine(toPath, file.Name)))
                {
                    File.Copy(file.FullName, Path.Combine(toPath, file.Name));
                }
            });
        }

        private static bool VerifyChecksums(string root, string checksumFile)
        {
            if (!File.Exists(checksumFile))
            {
                return false;
            }

            bool verified = true;
            var dirInfo = new DirectoryInfo(root);
            using var sha = SHA256.Create();
            var checksums = JsonHelper.To<Dictionary<string, string>>(File.ReadAllText(checksumFile));
            foreach (var file in dirInfo.GetFiles())
            {
                using var stream = new FileStream(file.FullName, FileMode.Open, FileAccess.Read);
                string key = Transform
                    .ToHexString(sha.ComputeHash(Encoding.ASCII.GetBytes(file.Name)))
                    .ToLower();
                if (checksums.TryGetValue(key, out string checksum))
                {
                    string value = Transform
                        .ToHexString(sha.ComputeHash(stream))
                        .ToLower();
                    if (value != checksum)
                    {
                        verified = false;
                        break;
                    }
                }
                else
                {
                    verified = false;
                    break;
                }
            }

            return verified;
        }

        private static DockerServiceSetup GetServiceSetup(string service, IBuildSettings settings)
        {
            DockerServiceSetup setup = null;
            if (service == SysParameterUtility.LicenseServer.ImageName)
            {
                setup = new LicenseServiceSetup(settings);
            }
            else if (service == SysParameterUtility.ApiServer.ImageName)
            {
                setup = new ApiServiceSetup(settings);
            }
            else if (service == SysParameterUtility.WebApp.ImageName)
            {
                setup = new AppServiceSetup(settings);
            }
            else if (service == SysParameterUtility.DbServer.ImageName)
            {
                setup = new DbServiceSetup(settings);
            }

            return setup;
        }

        private const string AppTitle = "سیستم جدید تدبیر";
        private const string ChecksumRoot = "..";
        private const string DbLoginQuery = @"
SELECT [name]
FROM [sys].[server_principals]
WHERE [is_disabled] = 0 AND [type] = 'S'";
    }
}
