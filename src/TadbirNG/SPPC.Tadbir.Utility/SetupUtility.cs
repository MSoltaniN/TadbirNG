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
using SPPC.Framework.Utility;
using SPPC.Tadbir.Configuration;
using SPPC.Tadbir.Utility.Docker;
using SPPC.Tadbir.Utility.Model;
using SPPC.Tadbir.Utility.Templates;

namespace SPPC.Tadbir.Utility
{
    public class SetupUtility
    {
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
            _log.WriteLine();
            _log.LogInformation("Setup started.");
            _log.LogInformation("Creating installation path...");
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
            _log.LogInformation("Done.");
        }

        public static void CopyFiles(string path, IBuildSettings settings, bool createShortcut = true)
        {
            _log.LogInformation("Copying program files...");
            var config = CryptoService.Default.Encrypt(JsonHelper.From(settings));
            File.WriteAllText(Path.Combine(path, "config"), config);
            File.Copy(Path.Combine(ChecksumRoot, "version"), Path.Combine(path, "version"));
            File.Copy(Path.Combine(ChecksumRoot, "license"), Path.Combine(path, "license"));
            CopyFilesIfMissing(Path.Combine(ChecksumRoot, "runner"), Path.Combine(path, "runner"));
            CopyFilesIfMissing(Path.Combine(ChecksumRoot, "service"), Path.Combine(path, "service"));
            CopyFilesIfMissing(Path.Combine(ChecksumRoot, "setup"), Path.Combine(path, "setup"));
            CopyFilesIfMissing(Path.Combine(ChecksumRoot, "tools"), Path.Combine(path, "tools"));
            AdjustDockerCompose(path, settings.DbServerName);
            if (createShortcut)
            {
                // Create shortcut to main Runner executable on user's Desktop folder...
                _log.LogInformation("Creating desktop shortcut...");
                string exePath = Path.Combine(path, "runner", "Tadbir.exe");
                CreateDesktopShortcut(exePath, AppTitle, AppTitle);
            }

            _log.LogInformation("Done.");
        }

        public static void DeleteFiles()
        {
            _log.LogInformation("Deleting program files...");
            Array.ForEach(new DirectoryInfo("..").GetFiles("*.*", SearchOption.TopDirectoryOnly),
                fi => File.Delete(fi.FullName));
            var path = Path.Combine("..", "runner");
            FileUtility.DeleteFolder(path, _log);
            path = Path.Combine("..", "service");
            FileUtility.DeleteFolder(path, _log);
            path = Path.Combine("..", "tools");
            FileUtility.DeleteFolder(path, _log);
            _log.LogInformation("Deleting desktop shortcut...");
            DeleteDesktopShortcut(AppTitle);
            _log.LogInformation("Done.");
        }

        public static bool InstallService(string path)
        {
            _log.LogInformation("Installing service...");
            string binPath = Path.Combine(path, "service", "KeyServer.exe");
            bool succeeded = WinServiceUtility.Install(
                SysParameterUtility.Service.Name, SysParameterUtility.Service.DisplayName, binPath);
            if (succeeded)
            {
                _log.LogInformation("Done.");
            }
            else
            {
                _log.LogError("Error occured while installing service.");
            }

            return succeeded;
        }

        public static bool UninstallService()
        {
            _log.LogInformation("Uninstalling service...");
            bool succeeded = WinServiceUtility.Uninstall(SysParameterUtility.Service.Name);
            if (succeeded)
            {
                _log.LogInformation("Done.");
            }
            else
            {
                _log.LogError("Error occured while uninstalling service.");
            }

            return succeeded;
        }

        public static bool StartService()
        {
            _log.LogInformation("Starting service...");
            bool succeeded = WinServiceUtility.Start(SysParameterUtility.Service.Name);
            if (succeeded)
            {
                _log.LogInformation("Done.");
            }
            else
            {
                _log.LogError("Error occured while starting service.");
            }

            return succeeded;
        }

        public static bool StopService()
        {
            _log.LogInformation("Stopping service...");
            bool succeeded = WinServiceUtility.Stop(SysParameterUtility.Service.Name);
            if (succeeded)
            {
                _log.LogInformation("Done.");
            }
            else
            {
                _log.LogError("Error occured while stopping service.");
            }

            return succeeded;
        }

        public static void ConfigureDockerService(string root, string service, IBuildSettings settings)
        {
            _log.LogInformation($"Configuring {service}...");
            var setup = GetServiceSetup(service, settings);
            setup.ConfigureService(root);
            _log.LogInformation($"Done.");
        }

        public static void ConfigureDbService(string root, IBuildSettings settings)
        {
            var service = SysParameterUtility.DbServer.ImageName;
            _log.LogInformation($"Configuring {service}...");
            if (settings.DbServerName == SysParameterUtility.DbServer.Name)
            {
                var setup = GetServiceSetup(service, settings);
                setup.ConfigureService(root);
            }
            else
            {
                ConfigureDbServer(settings);
            }

            _log.LogInformation($"Done.");
        }

        public static void RemoveDockerServices()
        {
            _log.LogInformation("Removing Docker services...");
            var currentDir = Environment.CurrentDirectory;
            Environment.CurrentDirectory = FileUtility.GetAbsolutePath(@"..\runner");
            var runner = new CliRunner();
            RunAndLogCommand(runner, "docker-compose -f docker-compose.override.yml -f docker-compose.yml down");
            var imageName = $"{SysParameterUtility.GetImageFullName(SysParameterUtility.WebApp)}";
            RunAndLogCommand(runner, String.Format(ToolConstants.DockerRemoveImageCommand, imageName));
            imageName = $"{SysParameterUtility.GetImageFullName(SysParameterUtility.LicenseServer)}";
            RunAndLogCommand(runner, String.Format(ToolConstants.DockerRemoveImageCommand, imageName));
            imageName = $"{SysParameterUtility.GetImageFullName(SysParameterUtility.DbServer)}";
            RunAndLogCommand(runner, String.Format(ToolConstants.DockerRemoveImageCommand, imageName));
            imageName = $"{SysParameterUtility.GetImageFullName(SysParameterUtility.ApiServer, Edition.StandardTag)}";
            RunAndLogCommand(runner, String.Format(ToolConstants.DockerRemoveImageCommand, imageName));
            imageName = $"{SysParameterUtility.GetImageFullName(SysParameterUtility.ApiServer, Edition.ProfessionalTag)}";
            RunAndLogCommand(runner, String.Format(ToolConstants.DockerRemoveImageCommand, imageName));
            imageName = $"{SysParameterUtility.GetImageFullName(SysParameterUtility.ApiServer, Edition.EnterpriseTag)}";
            RunAndLogCommand(runner, String.Format(ToolConstants.DockerRemoveImageCommand, imageName));
            RunAndLogCommand(runner, String.Format(
                ToolConstants.DockerRemoveVolumeCommand, $"runner_productdata_{SysParameterUtility.DbServer.Name}"));
            RunAndLogCommand(runner, String.Format(
                ToolConstants.DockerRemoveVolumeCommand, $"runner_productdata_{SysParameterUtility.LicenseServer.Name}"));
            Environment.CurrentDirectory = currentDir;
            _log.LogInformation("Done.");
        }

        public static bool IsAppRegistered()
        {
            var runner = new CliRunner();
            var output = runner.Run($"sc query {SysParameterUtility.Service.Name}");
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

        public static void FlushLogFile()
        {
            _log.Flush();
        }

        public static void FinalizeSetup(string path)
        {
            string logPath = Path.Combine(path, "runner", Path.GetFileName(_log.LogPath));
            File.Copy(_log.LogPath, logPath, true);
        }

        private static void AdjustDockerCompose(string installPath, string dbServer)
        {
            var composePath = Path.Combine(installPath, "runner", "docker-compose.yml");
            var overridePath = Path.Combine(installPath, "runner", "docker-compose.override.yml");
            var nameTag = File.ReadAllLines(composePath)
                .Where(line => line.Contains(SysParameterUtility.ApiServer.ImageName))
                .Select(line => line[line.IndexOf(SysParameterUtility.ApiServer.ImageName)..])
                .FirstOrDefault();
            if (nameTag != null)
            {
                var editionTag = nameTag
                    .Replace(SysParameterUtility.ApiServer.ImageName, String.Empty)
                    .Replace(":", String.Empty);
                var template = new DockerCompose(editionTag, dbServer) as ITextTemplate;
                File.WriteAllText(composePath, template.TransformText());
                template = new DockerComposeOverride(editionTag, dbServer);
                File.WriteAllText(overridePath, template.TransformText());
            }
        }

        private static void ConfigureDbServer(IBuildSettings settings)
        {
            var connBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = settings.DbServerName.Replace(
                    BuildSettingValues.DockerHostInternalUrl, Environment.MachineName),
                InitialCatalog = "master",
                UserID = "sa",
                Password = settings.SaPassword,
                IntegratedSecurity = false
            };
            var sqlConsole = new SqlServerConsole() { ConnectionString = connBuilder.ConnectionString };
            var utility = new DbSetupUtility(sqlConsole, settings);
            utility.ConfigureDatabase();
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

        private static void RunAndLogCommand(CliRunner runner, string command)
        {
            var output = runner.Run(command);
            Array.ForEach(output.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries),
                line => _log.LogInformation(line));
        }

        private const string AppTitle = "سیستم جدید تدبیر";
        private const string ChecksumRoot = "..";
        private const string DbLoginQuery = @"
SELECT [name]
FROM [sys].[server_principals]
WHERE [is_disabled] = 0 AND [type] = 'S'";
        private static readonly LogFile _log = new();
    }
}
