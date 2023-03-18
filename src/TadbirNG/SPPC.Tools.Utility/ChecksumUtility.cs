using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using SPPC.Framework.Cryptography;
using SPPC.Tadbir.Configuration;
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
            if (serviceName == SysParameterUtility.ApiServer.Name)
            {
                files = GetApiServerFiles();
            }
            else if (serviceName == SysParameterUtility.LicenseServer.Name)
            {
                files = GetLicenseServerFiles();
            }
            else if (serviceName == SysParameterUtility.DbServer.Name)
            {
                files = GetDbServerFiles();
            }
            else if (serviceName == SysParameterUtility.WebApp.Name)
            {
                files = GetWebAppFiles();
            }

            return files;
        }

        private static void CopyServiceFiles(string serviceName, IEnumerable<string> files)
        {
            if (serviceName == SysParameterUtility.WebApp.Name)
            {
                CopyWebAppFiles(files);
            }
            else
            {
                Array.ForEach(files.ToArray(), file =>
                    File.Copy(file, Path.Combine(TempFolder, Path.GetFileName(file))));
            }
        }

        private static void CopyWebAppFiles(IEnumerable<string> files)
        {
            if (files.Any())
            {
                var sorted = files.OrderBy(file => Path.GetFileName(file));
                var lastFile = sorted.First();
                File.Copy(lastFile, Path.Combine(TempFolder, Path.GetFileName(lastFile)));
                int counter = 1;
                foreach (var file in sorted.Skip(1))
                {
                    if (Path.GetFileName(file) == Path.GetFileName(lastFile))
                    {
                        File.Copy(file, Path.Combine(TempFolder, Path.GetFileName($"{file}.{counter++}")));
                    }
                    else
                    {
                        lastFile = file;
                        counter = 1;
                        File.Copy(file, Path.Combine(TempFolder, Path.GetFileName(file)));
                    }
                }
            }
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
            files.AddRange(new DirectoryInfo(root)
                .GetFiles("*.sql", SearchOption.TopDirectoryOnly)
                .Select(scriptFile => scriptFile.FullName));
            return files;
        }

        private static IEnumerable<string> GetWebAppFiles()
        {
            var root = PathConfig.WebAppRoot;
            var files = new DirectoryInfo(root)
                .GetFiles("*.cs", SearchOption.TopDirectoryOnly)
                .Select(fi => fi.FullName)
                .ToList();

            files.Add(Path.Combine(root, "SPPC.Tadbir.Web.csproj"));
            root = Path.Combine(root, "ClientApp");
            var fixedFiles = new string[]
            {
                "nginx.conf", "Dockerfile", "angular.json", "package.json", "tsconfig.json"
            };
            files.AddRange(fixedFiles
                .Select(f => Path.Combine(root, f)));
            var sourceRoot = Path.Combine(root, "src");
            files.AddRange(GetFiles(sourceRoot, "*.html", false));
            files.AddRange(GetFiles(sourceRoot, "*.css", false));
            files.AddRange(GetFiles(sourceRoot, "*.ts", false));
            files.AddRange(GetFiles(sourceRoot, "*.json", false));
            root = Path.Combine(sourceRoot, "app");
            files.AddRange(GetFiles(root, "*.html"));
            files.AddRange(GetFiles(root, "*.css"));
            files.AddRange(GetFiles(root, "*.ts"));
            root = Path.Combine(sourceRoot, "assets");
            files.AddRange(GetFiles(root, "*.ico", false));
            files.AddRange(GetFiles(root, "*.png", false));
            root = Path.Combine(sourceRoot, "assets", "i18n");
            files.AddRange(GetFiles(root, "*.json", false));
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
