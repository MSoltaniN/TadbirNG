using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Win32;
using SPPC.Framework.Common;
using SPPC.Framework.Helpers;

namespace SPPC.Tadbir.WinRunner.Utility
{
#pragma warning disable CA1416 // Validate platform compatibility
    public class InstallerUtility
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

        public static List<string> GetDbServers()
        {
            var servers = new List<string>();
            var key = Registry.LocalMachine.OpenSubKey(
                @"SOFTWARE\Microsoft\Microsoft SQL Server\Instance Names\SQL");
            if (key != null)
            {
                foreach (var value in key.GetValueNames())
                {
                    if (value == "MSSQLSERVER")
                    {
                        servers.Add(Environment.MachineName);
                    }
                    else
                    {
                        servers.Add(String.Format($"{Environment.MachineName}\\{value}"));
                    }
                }
            }

            return servers;
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

        public static void CopyFiles(string path, bool createShortcut = true)
        {
            CopyFilesIfMissing(ChecksumRoot, path);
            CopyFilesIfMissing(Path.Combine(ChecksumRoot, "runner"), Path.Combine(path, "runner"));
            CopyFilesIfMissing(Path.Combine(ChecksumRoot, "service"), Path.Combine(path, "service"));
            if (createShortcut)
            {
                // Create shortcut to main Runner executable on user's Desktop folder...
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
                string binPath = Path.Combine(path, "service", "SPPC.Framework.KeyServer.exe");
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

        public static bool IsAppRegistered()
        {
            // NOTE: Installation info for TadbirNG should be present in the following Registry key:
            // HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\TadbirNG
            var key = Registry.LocalMachine.OpenSubKey(
                @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\TadbirNG", false);
            if (key != null && HasRequiredValues(key))
            {
                var value = key.GetValue("InstallLocation");
                string root = value?.ToString();
                if (!String.IsNullOrEmpty(root))
                {
                    if (ValidateValues(key, root))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static void RegisterApplication(string root, string dbServer, Version version)
        {
            var appGuid = new Guid("{E9DAA9A7-BB68-472B-8051-5E2ED9386F3C}");
            using var parent = Registry.LocalMachine.OpenSubKey(
                    @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall", true);
            var key = parent.OpenSubKey("TadbirNG", true) ??
                parent.CreateSubKey("TadbirNG");
            string path = Path.Combine(root, "runner", "SPPC.Tadbir.WinRunner.exe");
            key.SetValue("DbServer", dbServer, RegistryValueKind.String);
            key.SetValue("DisplayIcon", path, RegistryValueKind.String);
            key.SetValue("DisplayName", "TadbirNG", RegistryValueKind.String);
            key.SetValue("DisplayVersion", version.ToString(3), RegistryValueKind.String);
            key.SetValue("EstimatedSize", GetFolderSize(root), RegistryValueKind.DWord);
            key.SetValue("InstallDate", DateTime.Now.ToShortDateString(), RegistryValueKind.String);
            key.SetValue("InstallLocation", root, RegistryValueKind.String);
            key.SetValue("MajorVersion", version.Major, RegistryValueKind.DWord);
            key.SetValue("MinorVersion", version.Minor, RegistryValueKind.DWord);
            key.SetValue("NoModify", 1, RegistryValueKind.DWord);
            key.SetValue("NoRepair", 1, RegistryValueKind.DWord);
            key.SetValue("Publisher", "SPPC, LLC", RegistryValueKind.String);
            key.SetValue("UninstallPath", path, RegistryValueKind.String);
            key.SetValue("VersionMajor", version.Major, RegistryValueKind.DWord);
            key.SetValue("VersionMinor", version.Minor, RegistryValueKind.DWord);
            key.Close();
        }

        public static Version GetAppVersion()
        {
            var version = new DirectoryInfo(ChecksumRoot)
                .GetFiles()
                .Where(file => file.Name.StartsWith("v"))
                .FirstOrDefault();
            if (version != null)
            {
                return new Version(version.Name.Replace("v", String.Empty));
            }

            return null;
        }

        public static string GetCustomerSuffix()
        {
            var version = new DirectoryInfo(ChecksumRoot)
                .GetFiles()
                .Where(file => file.Name.StartsWith("v"))
                .FirstOrDefault();
            if (version != null)
            {
                return File
                    .ReadAllText(version.FullName)
                    .Substring(0, 8);
            }

            return null;
        }

        private static void EnsureDirectoryExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        private static bool VerifyChecksums(string root, string checksumFile)
        {
            if (!File.Exists(checksumFile))
            {
                return false;
            }

            bool verified = true;
            var dirInfo = new DirectoryInfo(root);
            using var sha1 = SHA1.Create();
            var checksums = JsonHelper.To<Dictionary<string, string>>(File.ReadAllText(checksumFile));
            foreach (var file in dirInfo.GetFiles())
            {
                using var stream = new FileStream(file.FullName, FileMode.Open, FileAccess.Read);
                string key = Transform
                    .ToHexString(sha1.ComputeHash(Encoding.ASCII.GetBytes(file.Name)))
                    .ToLower();
                if (checksums.TryGetValue(key, out string checksum))
                {
                    string value = Transform
                        .ToHexString(sha1.ComputeHash(stream))
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

        private static bool ValidateValues(RegistryKey key, string root)
        {
            if (!Directory.Exists(root))
            {
                return false;
            }

            string path = Path.Combine(root, "runner", "SPPC.Tadbir.WinRunner.exe");
            if(!VerifyStringValue(key, "DisplayIcon", path))
            {
                return false;
            }

            if (!VerifyStringValue(key, "UninstallPath", path))
            {
                return false;
            }

            if (!VerifyStringValue(key, "DisplayName", "TadbirNG"))
            {
                return false;
            }

            if (!VerifyStringValue(key, "Publisher", "SPPC, LLC"))
            {
                return false;
            }

            object value = key.GetValue("DisplayVersion");
            string version = value?.ToString();
            if (String.IsNullOrEmpty(version))
            {
                return false;
            }

            if (!VerifyAppVersion(key, version))
            {
                return false;
            }

            if (!VerifyIntegerValue(key, "EstimatedSize", GetFolderSize(root)))
            {
                return false;
            }

            if (!VerifyInstallOptions(key))
            {
                return false;
            }

            return true;
        }

        private static bool HasRequiredValues(RegistryKey key)
        {
            var currentValues = key.GetValueNames();
            return GetRequiredValues()
                .All(value => currentValues.Contains(value));
        }

        private static string[] GetRequiredValues()
        {
            return new[]
            {
                "DbServer", "DisplayIcon", "DisplayName", "DisplayVersion", "EstimatedSize", "InstallDate",
                "InstallLocation", "MajorVersion", "MinorVersion", "NoModify", "NoRepair", "Publisher",
                "UninstallPath", "VersionMajor", "VersionMinor"
            };
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

        private static bool VerifyStringValue(RegistryKey key, string name, string expectedValue)
        {
            object value = key.GetValue(name);
            if (value == null || value.ToString() != expectedValue)
            {
                return false;
            }

            return true;
        }

        private static bool VerifyIntegerValue(RegistryKey key, string name, int expectedValue)
        {
            object value = key.GetValue(name);
            if (value == null || Convert.ToInt32(value) != expectedValue)
            {
                return false;
            }

            return true;
        }

        private static bool VerifyAppVersion(RegistryKey key, string version)
        {
            string claimedVersion = String.Format(
                $"{key.GetValue("MajorVersion")?.ToString()}.{key.GetValue("MinorVersion")?.ToString()}");
            if (!version.StartsWith(claimedVersion))
            {
                return false;
            }

            claimedVersion = String.Format(
                $"{key.GetValue("VersionMajor")?.ToString()}.{key.GetValue("VersionMinor")?.ToString()}");
            if (!version.StartsWith(claimedVersion))
            {
                return false;
            }

            return true;
        }

        private static bool VerifyInstallOptions(RegistryKey key)
        {
            if(!VerifyIntegerValue(key, "NoModify", 1))
            {
                return false;
            }

            if (!VerifyIntegerValue(key, "NoRepair", 1))
            {
                return false;
            }

            return true;
        }

        private const string ChecksumRoot = "_temp_";   // For testing only (correct path is ..)
    }
#pragma warning restore CA1416 // Validate platform compatibility
}
