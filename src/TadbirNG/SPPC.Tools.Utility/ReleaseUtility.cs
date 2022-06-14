using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Ionic.Zip;
using Ionic.Zlib;
using SPPC.Framework.Common;
using SPPC.Framework.Cryptography;
using SPPC.Framework.Helpers;
using SPPC.Licensing.Model;
using SPPC.Tadbir.Configuration.Models;
using SPPC.Tools.Model;
using SPPC.Tools.Transforms;
using SPPC.Tools.Transforms.Templates;

namespace SPPC.Tools.Utility
{
    public class ReleaseUtility
    {
        public static void GenerateSettings(LicenseModel license)
        {
            var settings = BuildSettings.Docker;
            settings.Key = GetInstanceKey(license);
            settings.Version = VersionInfo.GetAppVersion();
            GenerateSettingsWithBackup(PathConfig.LocalServerRoot, new LocalLicenseApiSettings(settings));
            GenerateSettingsWithBackup(PathConfig.WebApiRoot, new WebApiSettings(settings));
            GenerateEnvironmentWithBackup(PathConfig.WebEnvRoot, new TsInstanceFromValues(settings));
            CreateLicenseFilesWithBackup(license);
            GenerateDockerComposeWithBackup(PathConfig.SolutionRoot,
                new DockerCompose(license.LicenseKey), new DockerComposeOverride(license.LicenseKey));
        }

        public static void RestoreSettings()
        {
            RestoreFile(Path.Combine(PathConfig.LocalServerRoot, ConfigFile));
            ////RestoreFile(Path.Combine(PathConfig.LocalServerRoot, DevConfigFile));
            RestoreFile(Path.Combine(PathConfig.WebApiRoot, ConfigFile));
            ////RestoreFile(Path.Combine(PathConfig.WebApiRoot, DevConfigFile));
            RestoreFile(Path.Combine(PathConfig.WebApiRoot, WebRoot, "license"));
            RestoreFile(Path.Combine(PathConfig.WebApiRoot, WebRoot, String.Format($"license{DevSuffix}")));
            RestoreFile(Path.Combine(PathConfig.WebApiRoot, WebRoot, "edition"));
            RestoreFile(Path.Combine(PathConfig.WebApiRoot, WebRoot, String.Format($"edition{DevSuffix}")));
            RestoreFile(Path.Combine(PathConfig.WebEnvRoot, EnvFile));
            RestoreFile(Path.Combine(PathConfig.WebEnvRoot, DevEnvFile));
            RestoreFile(Path.Combine(PathConfig.SolutionRoot, ComposeFile));
            RestoreFile(Path.Combine(PathConfig.SolutionRoot, OverrideFile));
        }

        public static void CreateReleaseArchive(string licenseKey, string password)
        {
            // Create a folder named after customer license in TadbirNG Release folder
            PrepareReleaseFolder(licenseKey);

            // Copy Service and Runner files in corresponding folders
            CopyProgramFiles(licenseKey);

            // Generate customer-specific docker-compose files inside runner folder
            GenerateDockerCompose(licenseKey);

            // Calculate checksums and create checksum files
            CreateChecksumFiles(licenseKey);

            // Create id file for release version
            CreateIdFile(licenseKey);

            // Make a password-protected ZIP file from folder structure inside release folder
            CreateZipArchive(licenseKey, password);

            // Delete source media inside release folder
            CleanupReleaseFiles(licenseKey);
        }

        #region Settings Generation

        private static void GenerateSettingsWithBackup(string settingsRoot, ITextTemplate template)
        {
            BackupFile(Path.Combine(settingsRoot, ConfigFile));

            string appSettings = template.TransformText();
            File.WriteAllText(Path.Combine(settingsRoot, ConfigFile), appSettings);
        }

        private static void GenerateEnvironmentWithBackup(string envRoot, ITextTemplate template)
        {
            BackupFile(Path.Combine(envRoot, EnvFile));
            BackupFile(Path.Combine(envRoot, DevEnvFile));

            string appSettings = template.TransformText();
            File.WriteAllText(Path.Combine(envRoot, EnvFile), appSettings);
            File.WriteAllText(Path.Combine(envRoot, DevEnvFile), appSettings);
        }

        private static void GenerateDockerComposeWithBackup(
            string root, ITextTemplate compose, ITextTemplate @override)
        {
            BackupFile(Path.Combine(root, ComposeFile));
            BackupFile(Path.Combine(root, OverrideFile));

            File.WriteAllText(Path.Combine(root, ComposeFile), compose.TransformText());
            File.WriteAllText(Path.Combine(root, OverrideFile), @override.TransformText());
        }

        private static void CreateLicenseFilesWithBackup(LicenseModel license)
        {
            BackupFile(Path.Combine(PathConfig.WebApiRoot, WebRoot, "license"));
            BackupFile(Path.Combine(
                PathConfig.WebApiRoot, WebRoot, String.Format($"license{DevSuffix}")));
            string licenseData = GetLicenseData(license);
            File.WriteAllText(Path.Combine(PathConfig.WebApiRoot, WebRoot, "license"), licenseData);
            File.WriteAllText(Path.Combine(
                PathConfig.WebApiRoot, WebRoot, String.Format($"license{DevSuffix}")), licenseData);

            BackupFile(Path.Combine(PathConfig.WebApiRoot, WebRoot, "edition"));
            BackupFile(Path.Combine(
                PathConfig.WebApiRoot, WebRoot, String.Format($"edition{DevSuffix}")));
            string editionData = GetEditionData(license.Edition);
            File.WriteAllText(Path.Combine(PathConfig.WebApiRoot, WebRoot, "edition"), editionData);
            File.WriteAllText(Path.Combine(
                PathConfig.WebApiRoot, WebRoot, String.Format($"edition{DevSuffix}")), editionData);
        }

        private static string GetLicenseData(LicenseModel license)
        {
            var licenseData = new LicenseViewModel()
            {
                CustomerName = license.Customer.CompanyName,
                ContactName = String.Format(
                    "{0} {1}", license.Customer.ContactFirstName, license.Customer.ContactLastName),
                Edition = license.Edition,
                UserCount = license.UserCount,
                ActiveModules = license.ActiveModules,
                StartDate = license.StartDate,
                EndDate = license.EndDate
            };
            return JsonHelper.From(licenseData);
        }

        private static string GetEditionData(string edition)
        {
            var allConfig = JsonHelper.To<EditionsConfig>(File.ReadAllText(PathConfig.EditionConfig));
            return JsonHelper.From(Reflector.GetProperty(allConfig, edition));
        }

        private static string GetInstanceKey(LicenseModel license)
        {
            var crypto = new CryptoService(new CertificateManager());
            var instance = new InstanceModel()
            {
                CustomerKey = license.CustomerKey,
                LicenseKey = license.LicenseKey
            };
            return crypto.Encrypt(JsonHelper.From(instance, false));
        }

        private static void BackupFile(string path)
        {
            if (File.Exists(path))
            {
                string fileName = Path.GetFileName(path);
                string newPath = Path.Combine(Path.GetDirectoryName(path), String.Format($"old_{fileName}"));
                File.Move(path, newPath);
            }
        }

        private static void RestoreFile(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            string fileName = Path.GetFileName(path);
            string newPath = Path.Combine(Path.GetDirectoryName(path), String.Format($"old_{fileName}"));
            if (File.Exists(newPath))
            {
                File.Move(newPath, path);
            }
        }

        #endregion

        #region Zip Archive Creation

        private static void PrepareReleaseFolder(string licenseKey)
        {
            EnsureDirectoryExists(PathConfig.TadbirRelease);
            EnsureDirectoryExists(Path.Combine(PathConfig.TadbirRelease, licenseKey));
            EnsureDirectoryExists(Path.Combine(PathConfig.TadbirRelease, licenseKey, "service"));
            EnsureDirectoryExists(Path.Combine(PathConfig.TadbirRelease, licenseKey, "runner"));
        }

        private static void EnsureDirectoryExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        private static void CopyProgramFiles(string licenseKey)
        {
            // WARNING: This method attempts to copy around 225 MB of program files synchronously
            // On a normal disk drive (non-SSD) this may take some time.
            string path = Path.Combine(PathConfig.TadbirRelease, licenseKey, "service");
            var dirInfo = new DirectoryInfo(PathConfig.ServicePublishWin);
            var files = dirInfo.GetFiles();
            Array.ForEach(files,
                file => File.Copy(file.FullName, Path.Combine(path, file.Name)));

            path = Path.Combine(PathConfig.TadbirRelease, licenseKey, "runner");
            dirInfo = new DirectoryInfo(PathConfig.RunnerPublishWin);
            files = dirInfo.GetFiles();
            Array.ForEach(files,
                file => File.Copy(file.FullName, Path.Combine(path, file.Name)));
        }

        private static void GenerateDockerCompose(string licenseKey)
        {
            var template = new DockerCompose(licenseKey) as ITextTemplate;
            File.WriteAllText(Path.Combine(
                PathConfig.TadbirRelease, licenseKey, "runner", ComposeFile), template.TransformText());
            template = new DockerComposeOverride(licenseKey);
            File.WriteAllText(Path.Combine(
                PathConfig.TadbirRelease, licenseKey, "runner", OverrideFile), template.TransformText());
        }

        private static void CreateChecksumFiles(string licenseKey)
        {
            var root = Path.Combine(PathConfig.TadbirRelease, licenseKey);
            CreateChecksumFile(Path.Combine(root, "service"));
            CreateChecksumFile(Path.Combine(root, "runner"));
        }

        private static void CreateChecksumFile(string path)
        {
            var dirInfo = new DirectoryInfo(path);
            using var sha = SHA256.Create();
            var checksum = new Dictionary<string, string>();
            foreach (var file in dirInfo.GetFiles())
            {
                using var stream = new FileStream(file.FullName, FileMode.Open, FileAccess.Read);
                string key = Transform
                    .ToHexString(sha.ComputeHash(Encoding.ASCII.GetBytes(file.Name)))
                    .ToLower();
                string value = Transform
                    .ToHexString(sha.ComputeHash(stream))
                    .ToLower();
                checksum.Add(key, value);
            }

            var checksums = JsonHelper.From(checksum, false);
            var parent = Path.GetDirectoryName(path);
            var fileName = String.Format($"{Path.GetFileName(path)}.sha");
            File.WriteAllText(Path.Combine(parent, fileName), checksums);
        }

        private static void CreateIdFile(string licenseKey)
        {
            string path = Path.Combine(PathConfig.TadbirRelease, licenseKey);
            var version = VersionInfo.GetAppVersion();
            File.WriteAllText(Path.Combine(path, String.Format($"v{version}")), licenseKey);
        }

        private static void CreateZipArchive(string licenseKey, string password)
        {
            Verify.ArgumentNotNullOrEmptyString(password, nameof(password));
            string path = Path.Combine(PathConfig.TadbirRelease,
                String.Format($"TadbirNG_v{VersionInfo.GetAppVersion()}.zip"));
            if (!File.Exists(path))
            {
                var zipFile = new ZipFile()
                {
                    CompressionLevel = CompressionLevel.BestCompression,
                    CompressionMethod = CompressionMethod.BZip2,
                    Encryption = EncryptionAlgorithm.WinZipAes128,
                    FlattenFoldersOnExtract = true,
                    Name = path,
                    Password = password
                };
                using (zipFile)
                {
                    zipFile.AddDirectory(Path.Combine(PathConfig.TadbirRelease, licenseKey));
                    zipFile.Save();
                }

                string newPath = Path.Combine(PathConfig.TadbirRelease, licenseKey, Path.GetFileName(path));
                File.Move(path, newPath);
            }
        }

        private static void CleanupReleaseFiles(string licenseKey)
        {
            string path = Path.Combine(PathConfig.TadbirRelease, licenseKey);
            var filesToDelete = new DirectoryInfo(path)
                .GetFiles("*.*", SearchOption.AllDirectories)
                .Where(file => !file.Name.EndsWith(".zip", StringComparison.CurrentCultureIgnoreCase))
                .ToArray();
            Array.ForEach(filesToDelete, file => File.Delete(file.FullName));
            Directory.Delete(Path.Combine(path, "runner"));
            Directory.Delete(Path.Combine(path, "service"));
        }

        #endregion

        private const string ConfigFile = "appSettings.json";
        private const string DevConfigFile = "appSettings.Development.json";
        private const string EnvFile = "environment.prod.ts";
        private const string DevEnvFile = "environment.ts";
        private const string ComposeFile = "docker-compose.yml";
        private const string OverrideFile = "docker-compose.override.yml";
        private const string DevSuffix = ".Development.json";
        private const string WebRoot = "wwwroot";
    }
}
