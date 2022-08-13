using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SPPC.Framework.Cryptography;
using SPPC.Framework.Helpers;
using SPPC.Licensing.Model;
using SPPC.Tools.Model;
using SPPC.Tools.Transforms;
using SPPC.Tools.Transforms.Templates;

namespace SPPC.Tools.Utility
{
    public class ReleaseUtility
    {
        public static void UpdateImageCache(CliRunner runner)
        {
            var cacheRoot = FileUtility.GetAbsolutePath(PathConfig.DockerCacheRoot);
            var currentDir = Environment.CurrentDirectory;
            Environment.CurrentDirectory = cacheRoot;
            runner.Run(ToolConstants.GitPullCommand);
            Environment.CurrentDirectory = currentDir;
        }

        public static void CopyProgramFiles(string licenseKey, string edition)
        {
            // WARNING: This method attempts to copy around 1.5 GB of program files synchronously
            // On a normal disk drive (non-SSD) this may take some time.
            PrepareReleaseFolder(licenseKey);
            CopyDockerFiles(licenseKey, edition);

            string path = Path.Combine(PathConfig.TadbirRelease, licenseKey, "service");
            var files = new DirectoryInfo(PathConfig.ServicePublishWin)
                .GetFiles();
            Array.ForEach(files,
                file => File.Copy(file.FullName, Path.Combine(path, file.Name)));

            path = Path.Combine(PathConfig.TadbirRelease, licenseKey, "setup");
            files = new DirectoryInfo(PathConfig.SetupPublishWin)
                .GetFiles();
            Array.ForEach(files,
                file => File.Copy(file.FullName, Path.Combine(path, file.Name)));

            path = Path.Combine(PathConfig.TadbirRelease, licenseKey, "runner");
            files = new DirectoryInfo(PathConfig.RunnerPublishWin)
                .GetFiles();
            Array.ForEach(files,
                file => File.Copy(file.FullName, Path.Combine(path, file.Name)));

            path = Path.Combine(PathConfig.TadbirRelease, licenseKey, "tools");
            files = new DirectoryInfo(PathConfig.ToolsFolder)
                .GetFiles();
            Array.ForEach(files,
                file => File.Copy(file.FullName, Path.Combine(path, file.Name)));
        }

        public static void GenerateSettings(LicenseModel license)
        {
            var crypto = CryptoService.Default;

            // Inside the main package folder, save public part of customer license in encrypted base64 format...
            string path = Path.Combine(PathConfig.TadbirRelease, license.LicenseKey, "license");
            File.WriteAllText(path, crypto.Encrypt(
                JsonHelper.From(
                    LicenseFactory.FromModel(license))));

            // Inside the main package folder, save instance key in encrypted base64 format...
            path = Path.Combine(PathConfig.TadbirRelease, license.LicenseKey, $"v{VersionUtility.GetReleaseVersion()}");
            File.WriteAllText(path, InstanceFactory.CryptoFromLicense(license));

            // Inside the main package folder, make version information file, using base images...
            path = Path.Combine(PathConfig.TadbirRelease, license.LicenseKey, "version");
            File.WriteAllText(path, GetVersionInfoData(license.LicenseKey));
        }

        public static void CreateReleaseArchive(string licenseKey, string edition, string password)
        {
            // Generate customer-specific docker-compose files inside runner folder
            GenerateDockerCompose(licenseKey, DockerUtility.GetEditionTag(edition));

            // Calculate checksums and create checksum files
            CreateChecksumFiles(licenseKey);

            // Make a password-protected ZIP file from folder structure inside release folder
            CreateZipArchive(licenseKey, password);

            // Delete source media inside release folder
            CleanupReleaseFiles(licenseKey);
        }

        #region Settings Generation

        private static string GetVersionInfoData(string licenseKey)
        {
            var versionInfo = new VersionInfo
            {
                Version = VersionUtility.GetReleaseVersion()
            };
            versionInfo.Services.Add(GetServiceInfo(licenseKey, DockerService.LicenseServerImage));
            versionInfo.Services.Add(GetServiceInfo(licenseKey, DockerService.ApiServerImage));
            versionInfo.Services.Add(GetServiceInfo(licenseKey, DockerService.DbServerImage));
            versionInfo.Services.Add(GetServiceInfo(licenseKey, DockerService.WebAppImage));

            return JsonHelper.From(versionInfo);
        }

        private static ServiceInfo GetServiceInfo(string licenseKey, string serviceImage)
        {
            var imagePath = Path.Combine(
                PathConfig.TadbirRelease, licenseKey, "docker", $"{serviceImage}.tar.gz");
            return DockerUtility.GetServiceInfo(imagePath);
        }

        #endregion

        #region Zip Archive Creation

        private static void PrepareReleaseFolder(string licenseKey)
        {
            // Create a folder named after customer license in TadbirNG Release folder
            EnsureDirectoryExists(PathConfig.TadbirRelease);
            EnsureDirectoryExists(Path.Combine(PathConfig.TadbirRelease, licenseKey));
            EnsureDirectoryExists(Path.Combine(PathConfig.TadbirRelease, licenseKey, "docker"));
            EnsureDirectoryExists(Path.Combine(PathConfig.TadbirRelease, licenseKey, "runner"));
            EnsureDirectoryExists(Path.Combine(PathConfig.TadbirRelease, licenseKey, "service"));
            EnsureDirectoryExists(Path.Combine(PathConfig.TadbirRelease, licenseKey, "setup"));
            EnsureDirectoryExists(Path.Combine(PathConfig.TadbirRelease, licenseKey, "tools"));
        }

        private static void EnsureDirectoryExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        private static void CopyDockerFiles(string licenseKey, string edition)
        {
            string path = Path.Combine(PathConfig.TadbirRelease, licenseKey, "docker");
            var cacheRoot = FileUtility.GetAbsolutePath(PathConfig.DockerCacheRoot);
            var root = Path.Combine(cacheRoot, DockerService.LicenseServerImage);
            File.Copy(Path.Combine(root, $"{DockerService.LicenseServerImage}.tar.gz"),
                Path.Combine(path, $"{DockerService.LicenseServerImage}.tar.gz"));
            root = Path.Combine(cacheRoot, DockerService.WebAppImage);
            File.Copy(Path.Combine(root, $"{DockerService.WebAppImage}.tar.gz"),
                Path.Combine(path, $"{DockerService.WebAppImage}.tar.gz"));
            root = Path.Combine(cacheRoot, DockerService.DbServerImage);
            File.Copy(Path.Combine(root, $"{DockerService.DbServerImage}.tar.gz"),
                Path.Combine(path, $"{DockerService.DbServerImage}.tar.gz"));
            var editionTag = DockerUtility.GetEditionTag(edition);
            root = Path.Combine(cacheRoot, DockerService.ApiServerImage);
            File.Copy(Path.Combine(root, $"{DockerService.ApiServerImage}-{editionTag}.tar.gz"),
                Path.Combine(path, $"{DockerService.ApiServerImage}.tar.gz"));
        }

        private static void GenerateDockerCompose(string licenseKey, string editionTag)
        {
            var template = new DockerCompose(editionTag) as ITextTemplate;
            File.WriteAllText(Path.Combine(
                PathConfig.TadbirRelease, licenseKey, "runner", ComposeFile), template.TransformText());
            template = new DockerComposeOverride(editionTag);
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
            var crypto = CryptoService.Default;
            var dirInfo = new DirectoryInfo(path);
            var checksum = new Dictionary<string, string>();
            foreach (var file in dirInfo.GetFiles())
            {
                string key = crypto
                    .CreateHash(Encoding.ASCII.GetBytes(file.Name))
                    .ToLower();
                string value = crypto
                    .CreateHash(File.ReadAllBytes(file.FullName))
                    .ToLower();
                checksum.Add(key, value);
            }

            var checksums = JsonHelper.From(checksum, false);
            var parent = Path.GetDirectoryName(path);
            var fileName = $"{Path.GetFileName(path)}.sha";
            File.WriteAllText(Path.Combine(parent, fileName), checksums);
        }

        private static void CreateZipArchive(string licenseKey, string password)
        {
            var zipFileName = $"TadbirNG_v{VersionUtility.GetReleaseVersion()}.zip";
            var zipPath = Path.Combine(PathConfig.TadbirRelease, zipFileName);
            var folderPath = Path.Combine(PathConfig.TadbirRelease, licenseKey);
            ArchiveUtility.Zip(zipPath, folderPath, password);
            string newPath = Path.Combine(folderPath, zipFileName);
            File.Move(zipPath, newPath);
        }

        private static void CleanupReleaseFiles(string licenseKey)
        {
            string path = Path.Combine(PathConfig.TadbirRelease, licenseKey);
            var filesToDelete = new DirectoryInfo(path)
                .GetFiles("*.*", SearchOption.AllDirectories)
                .Where(file => !file.Name.EndsWith(".zip", StringComparison.CurrentCultureIgnoreCase))
                .ToArray();
            Array.ForEach(filesToDelete, file => File.Delete(file.FullName));
            Directory.Delete(Path.Combine(path, "docker"));
            Directory.Delete(Path.Combine(path, "runner"));
            Directory.Delete(Path.Combine(path, "service"));
            Directory.Delete(Path.Combine(path, "setup"));
            Directory.Delete(Path.Combine(path, "tools"));
        }

        #endregion

        private const string ComposeFile = "docker-compose.yml";
        private const string OverrideFile = "docker-compose.override.yml";
    }
}
