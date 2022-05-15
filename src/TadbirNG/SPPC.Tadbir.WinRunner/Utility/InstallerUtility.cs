using System;
using System.IO;
using System.Linq;
using Microsoft.Win32;

namespace SPPC.Tadbir.WinRunner.Utility
{
#pragma warning disable CA1416 // Validate platform compatibility
    public class InstallerUtility
    {
        public bool IsAppInstalled()
        {
            string root = String.Empty;
            string path = String.Empty;

            // NOTE: Installation info for TadbirNG should be present in the following Registry key:
            // HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall\TadbirNG
            var key = Registry.LocalMachine.OpenSubKey(
                @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\TadbirNG", false);
            if (key != null && HasRequiredValues(key))
            {
                var value = key.GetValue("InstallLocation");
                root = value?.ToString();
                if (!String.IsNullOrEmpty(root))
                {
                    path = Path.Combine(root, "runner", "SPPC.Tadbir.WinRunner.exe");
                    if (ValidateValues(key, root))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public void RegisterApplication(string root, string dbServer, Version version)
        {
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

        #region Registry Validation

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

        #endregion
    }
#pragma warning restore CA1416 // Validate platform compatibility
}
