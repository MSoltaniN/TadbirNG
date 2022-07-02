using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using SPPC.Framework.Cryptography;
using SPPC.Tools.Model;

namespace SPPC.Tools.Utility
{
    public class ChecksumUtility
    {
        public static string CalculateChecksum(string serviceName)
        {
            var files = GetServiceFiles(serviceName);
            if (!Directory.Exists(TempFolder))
            {
                Directory.CreateDirectory(TempFolder);
            }

            CopyServiceFiles(serviceName, files);
            ZipFile.CreateFromDirectory(TempFolder, ZipFileName, CompressionLevel.Optimal, false);
            var crypto = new CryptoService(new CertificateManager());
            string checksum = new CryptoService(new CertificateManager())
                .CreateHash(File.ReadAllBytes(ZipFileName))
                .ToLower();
            Array.ForEach(new DirectoryInfo(TempFolder).GetFiles("*.*", SearchOption.TopDirectoryOnly), file =>
                File.Delete(file.FullName));
            Directory.Delete(TempFolder);
            File.Delete(ZipFileName);
            return checksum;
        }

        private static IEnumerable<string> GetServiceFiles(string serviceName)
        {
            IEnumerable<string> files = null;
            switch (serviceName)
            {
                case DockerService.ApiServer:
                    files = GetApiServerFiles();
                    break;
                case DockerService.LicenseServer:
                    files = GetLicenseServerFiles();
                    break;
                case DockerService.DbServer:
                    files = GetDbServerFiles();
                    break;
                case DockerService.WebApp:
                    files = GetWebAppFiles();
                    break;
                default:
                    break;
            }

            return files;
        }

        private static void CopyServiceFiles(string serviceName, IEnumerable<string> files)
        {
            switch (serviceName)
            {
                case DockerService.ApiServer:
                case DockerService.LicenseServer:
                case DockerService.DbServer:
                    Array.ForEach(files.ToArray(), file =>
                        File.Copy(file, Path.Combine(TempFolder, Path.GetFileName(file))));
                    break;
                case DockerService.WebApp:
                    CopyWebAppFiles(files);
                    break;
            }
        }

        private static void CopyWebAppFiles(IEnumerable<string> files)
        {
            var indexTsFiles = files
                .Where(file => file.Contains("index.ts"))
                .OrderBy(file => file);
            Array.ForEach(files.Except(indexTsFiles).ToArray(), file =>
                File.Copy(file, Path.Combine(TempFolder, Path.GetFileName(file))));
            int counter = 1;
            Array.ForEach(indexTsFiles.ToArray(), file =>
                File.Copy(file, Path.Combine(TempFolder, String.Format($"index.ts.{counter++}"))));
        }

        private static IEnumerable<string> GetApiServerFiles()
        {
            var root = PathConfig.WebApiRoot;
            var files = new DirectoryInfo(root)
                .GetFiles("*.cs", SearchOption.AllDirectories)
                .Select(fi => fi.FullName)
                .Where(path => !path.Contains("obj"))
                .ToList();
            files.Add(Path.Combine(root, "SPPC.Tadbir.Web.Api.csproj"));
            files.Add(Path.Combine(root, "Dockerfile"));
            root = Path.Combine(root, "wwwroot", "static");
            files.AddRange(new DirectoryInfo(root)
                .GetFiles("*.*", SearchOption.TopDirectoryOnly)
                .Select(fi => fi.FullName));
            return files;
        }

        private static IEnumerable<string> GetLicenseServerFiles()
        {
            var root = PathConfig.LocalServerRoot;
            var files = new DirectoryInfo(root)
                .GetFiles("*.cs", SearchOption.AllDirectories)
                .Select(fi => fi.FullName)
                .Where(path => !path.Contains("obj"))
                .ToList();
            files.Add(Path.Combine(root, "SPPC.Licensing.Local.Web.csproj"));
            files.Add(Path.Combine(root, "Dockerfile"));
            return files;
        }

        private static IEnumerable<string> GetDbServerFiles()
        {
            var root = PathConfig.ResourceRoot;
            var files = new List<string> { Path.Combine(root, "Dockerfile") };
            var fixedFiles = new string[]
            {
                "TadbirSys_CreateDbObjects.sql", "TadbirSys_QRTemplates.sql",
                "SetupDefaultLogin.sql", "Docker_FirstCompany.sql",
                "Tadbir_CreateDbObjects.sql", "Docker_StatesAndCities.sql"
            };
            files.AddRange(fixedFiles
                .Select(f => Path.Combine(root, f)));
            return files;
        }

        private static IEnumerable<string> GetWebAppFiles()
        {
            var root = PathConfig.WebAppRoot;
            var files = new DirectoryInfo(root)
                .GetFiles("*.cs", SearchOption.TopDirectoryOnly)
                .Select(fi => fi.FullName)
                .ToList();

            root = Path.Combine(root, "ClientApp");
            var fixedFiles = new string[]
            {
                "nginx.conf", "Dockerfile", "angular.json", "package.json", "tsconfig.json"
            };
            files.AddRange(fixedFiles
                .Select(f => Path.Combine(root, f)));
            root = Path.Combine(root, "src");
            files.AddRange(GetFiles(root, "*.html", false));
            files.AddRange(GetFiles(root, "*.css", false));
            files.AddRange(GetFiles(root, "*.ts", false));
            root = Path.Combine(root, "app");
            files.AddRange(GetFiles(root, "*.html"));
            files.AddRange(GetFiles(root, "*.css"));
            files.AddRange(GetFiles(root, "*.ts"));
            return files;
        }

        private static IEnumerable<string> GetFiles(string folder, string pattern, bool recursive = true)
        {
            var option = recursive
                ? SearchOption.AllDirectories
                : SearchOption.TopDirectoryOnly;
            return new DirectoryInfo(folder)
                .GetFiles(pattern, option)
                .Select(fi => fi.FullName);
        }

        private const string ZipFileName = "items.zip";
        private const string TempFolder = "temp";
    }
}
