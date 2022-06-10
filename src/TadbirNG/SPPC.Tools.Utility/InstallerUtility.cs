using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;
using System.Text;
using SPPC.Framework.Common;
using SPPC.Framework.Helpers;

namespace SPPC.Tools.Utility
{
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
            return false;
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

        public static void CreateDesktopShortcut(string path, string name, string description)
        {
            IShellLink link = (IShellLink)new ShellLink();

            // Setup shortcut information
            link.SetDescription(description);
            link.SetPath(path);

            // Save it
            IPersistFile file = (IPersistFile)link;
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            file.Save(Path.Combine(desktopPath, String.Format($"{name}.lnk")), false);
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

        private static int GetFolderSize(string root)
        {
            var directoryInfo = new DirectoryInfo(root);
            var bytesCount = directoryInfo
                .GetFiles("*.*", new EnumerationOptions() { RecurseSubdirectories = true })
                .Select(fi => fi.Length)
                .Sum();
            return (int)Math.Round((double)bytesCount / 1024);
        }

        private const string ChecksumRoot = "..";
    }
}
