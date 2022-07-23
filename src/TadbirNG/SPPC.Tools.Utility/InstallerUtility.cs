using System;
using System.Collections.Generic;
using System.Data;
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
        }

        public static void CopyFiles(string path, IBuildSettings settings, bool createShortcut = true)
        {
            var config = CryptoService.Default.Encrypt(JsonHelper.From(settings));
            File.WriteAllText(Path.Combine(path, "config"), config);
            File.Copy(Path.Combine(ChecksumRoot, "version"), Path.Combine(path, "version"));
            CopyFilesIfMissing(Path.Combine(ChecksumRoot, "runner"), Path.Combine(path, "runner"));
            CopyFilesIfMissing(Path.Combine(ChecksumRoot, "service"), Path.Combine(path, "service"));
            if (createShortcut)
            {
                // Create shortcut to main Runner executable on user's Desktop folder...
                string exePath = Path.Combine(path, "runner", "Tadbir.exe");
                CreateDesktopShortcut(exePath, AppTitle, AppTitle);
            }
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

        public static bool RunService()
        {
            var runner = new CliRunner();
            var output = runner.Run("sc start sppckeysrv");
            var lines = output.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            return !lines[0].Contains("FAILED");
        }

        public static void ConfigureDockerService(string root, string service, IBuildSettings settings)
        {
            var setup = GetServiceSetup(service, settings);
            setup.ConfigureService(root, DockerPath);
        }

        public static bool IsAppRegistered()
        {
            var runner = new CliRunner();
            var output = runner.Run("sc query sppckeysrv");
            var lines = output.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            return !lines[0].Contains("FAILED");
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
            switch (service)
            {
                case DockerService.LicenseServer:
                    setup = new LicenseServiceSetup(settings);
                    break;
                case DockerService.ApiServer:
                    setup = new ApiServiceSetup(settings);
                    break;
                case DockerService.WebApp:
                    setup = new AppServiceSetup(settings);
                    break;
                case DockerService.DbServer:
                    setup = new DbServiceSetup(settings);
                    break;
                default:
                    break;
            }

            return setup;
        }

        private static int GetFolderSize(string root)
        {
            var directoryInfo = new DirectoryInfo(root);
            var bytesCount = directoryInfo
                .GetFiles("*.*", new EnumerationOptions() { RecurseSubdirectories = true })
                .Select(fi => fi.Length)
                .Sum();
            return (int)Math.Round((double)bytesCount / 1024);
        }

        private const string AppTitle = "سیستم جدید تدبیر";
        private const string ChecksumRoot = "..";
        private const string DbLoginQuery = @"
SELECT [name]
FROM [sys].[server_principals]
WHERE [is_disabled] = 0 AND [type] = 'S'";
    }
}
