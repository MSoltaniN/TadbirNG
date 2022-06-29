using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SPPC.Framework.Common;
using SPPC.Framework.Cryptography;
using SPPC.Framework.Helpers;
using SPPC.Tools.Model;
using SPPC.Tools.Transforms;

namespace SPPC.Tools.Utility
{
    public abstract class DockerServiceSetup
    {
        public DockerServiceSetup(IBuildSettings settings)
        {
            _settings = settings;
        }

        public void ConfigureService()
        {
            _currentDir = Environment.CurrentDirectory;

            // Extract service image file to a temporary folder, using gzip and tar tools...
            var imageFileName = String.Format($"{ServiceName}.tar.gz");
            var path = Path.Combine(Constants.Root, imageFileName);
            var tempPath = ExtractImageFile(path);

            // Find the layer folder that contains Web service settings...
            var appLayerFolder = GetAppLayerFolder(tempPath);

            // Extract layer tar file inside layer folder and delete tar file...
            ExtractLayerFile(appLayerFolder);

            // Cleanup redundant build files (old_appSettings.json and appSettings.Development.json) and
            // replace appSettings.json with customer-specific settings...
            ConfigureAppLayer(appLayerFolder);

            // Create layer.tar file using tar tool and recursively delete app folder (from previous extract)...
            RestoreAppLayer(appLayerFolder);

            // Compute new app layer hash and replace in image config...
            ReplaceLayerHash();

            // Add all items in temporary folder to license-server.tar.gz and cleanup temp folder...
            Console.WriteLine($"Restoring service image file ({imageFileName})...");
            RestoreImageFile(imageFileName);

            // Load modified image file to Docker...
            _runner.Run(String.Format($"docker load -i {imageFileName}"));
            Environment.CurrentDirectory = _currentDir;
            File.Delete(Path.Combine(tempPath, imageFileName));
            Directory.Delete(tempPath);
        }

        protected abstract string ServiceName { get; }

        protected abstract ITextTemplate SettingsTemplate { get; }

        protected virtual void ConfigureAppLayer(string layerId)
        {
            var root = Path.Combine(Environment.CurrentDirectory, layerId, "app");
            var path = Path.Combine(root, Constants.OldAppSettings);
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            path = Path.Combine(root, Constants.DevAppSettings);
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            path = Path.Combine(root, Constants.AppSettings);
            File.WriteAllText(path, SettingsTemplate.TransformText());
        }

        private string ExtractImageFile(string path)
        {
            var gzFile = Path.GetFileName(path);
            var tarFile = gzFile.Replace(".gz", String.Empty);
            var tempCopyPath = GetTempCopyPath(path);
            File.Copy(path, tempCopyPath);
            Environment.CurrentDirectory = Path.GetDirectoryName(tempCopyPath);
            _runner.Run(String.Format(Constants.GunzipTemplate, Path.GetFileName(path)));
            _runner.Run(String.Format(Constants.UntarTemplate, tarFile));
            File.Delete(tarFile);
            return Environment.CurrentDirectory;
        }

        private static string GetTempCopyPath(string path)
        {
            var tempFile = Path.GetTempFileName();
            var tempFolder = String.Format($"bla-{Path.GetFileName(tempFile)}");
            File.Delete(tempFile);
            var tempPath = Path.Combine(Path.GetDirectoryName(tempFile), tempFolder);
            if (!Directory.Exists(tempPath))
            {
                Directory.CreateDirectory(tempPath);
            }

            return Path.Combine(tempPath, Path.GetFileName(path));
        }

        private static string GetAppLayerFolder(string tempPath)
        {
            string layerId = String.Empty;
            var dirInfo = new DirectoryInfo(tempPath);
            foreach (var item in dirInfo.GetFiles("json", SearchOption.AllDirectories))
            {
                var config = JsonHelper.To<DockerLayerConfig>(File.ReadAllText(item.FullName));
                if (config.Config?.Entrypoint != null)
                {
                    layerId = config.Id;
                    break;
                }
            }

            return layerId;
        }

        private void ExtractLayerFile(string layerId)
        {
            Environment.CurrentDirectory = Path.Combine(Environment.CurrentDirectory, layerId);
            var tarPath = Path.Combine(Environment.CurrentDirectory, Constants.LayerTarFile);
            if (File.Exists(tarPath))
            {
                _oldHash = _crypto
                    .CreateHash(File.ReadAllBytes(tarPath))
                    .ToLower();
                _runner.Run(String.Format(Constants.UntarTemplate, Constants.LayerTarFile));
                File.Delete(tarPath);
            }
            else
            {
                Console.WriteLine($"WARNING: Layer file '{Constants.LayerTarFile}' not found.");
            }

            Environment.CurrentDirectory = Path.GetDirectoryName(Environment.CurrentDirectory);
        }

        private void RestoreAppLayer(string layerId)
        {
            Environment.CurrentDirectory = Path.Combine(Environment.CurrentDirectory, layerId);
            _runner.Run(String.Format(Constants.TarTemplate, Constants.LayerTarFile, "app"));
            _newHash = _crypto
                .CreateHash(File.ReadAllBytes(Path.Combine(Environment.CurrentDirectory, Constants.LayerTarFile)))
                .ToLower();
            DeleteFolder(Path.Combine(Environment.CurrentDirectory, "app"));
            Environment.CurrentDirectory = Path.GetDirectoryName(Environment.CurrentDirectory);
        }

        private void ReplaceLayerHash()
        {
            var dirInfo = new DirectoryInfo(Environment.CurrentDirectory);
            var jsonFile = dirInfo
                .GetFiles("*.json", SearchOption.TopDirectoryOnly)
                .Where(file => file.Name != "manifest.json")
                .FirstOrDefault();
            if (jsonFile != null)
            {
                var content = File.ReadAllText(jsonFile.FullName);
                File.WriteAllText(jsonFile.FullName, content.Replace(_oldHash, _newHash));
            }
        }

        private static void DeleteFolder(string path)
        {
            var dirInfo = new DirectoryInfo(path);
            foreach (var file in dirInfo.GetFiles("*.*", SearchOption.AllDirectories))
            {
                File.Delete(file.FullName);
            }

            DeleteFolderRecursive(path);
        }

        private static void DeleteFolderRecursive(string path)
        {
            var dirInfo = new DirectoryInfo(path);
            foreach (var folder in dirInfo.GetDirectories("*.*", SearchOption.TopDirectoryOnly))
            {
                DeleteFolderRecursive(folder.FullName);
            }

            Directory.Delete(path);
        }

        private void RestoreImageFile(string imageFile)
        {
            var items = new List<string>();
            var dirInfo = new DirectoryInfo(Environment.CurrentDirectory);
            items.AddRange(dirInfo
                .GetDirectories("*.*", SearchOption.TopDirectoryOnly)
                .Select(dir => dir.Name));
            items.AddRange(dirInfo
                .GetFiles("*.*", SearchOption.TopDirectoryOnly)
                .Select(file => file.Name));
            var tarFile = imageFile.Replace(".gz", String.Empty);
            _runner.Run(String.Format(Constants.TarTemplate, tarFile, String.Join(" ", items)));
            _runner.Run(String.Format(Constants.GzipTemplate, tarFile));
            CleanUpFolder(Environment.CurrentDirectory, imageFile);
        }

        private static void CleanUpFolder(string path, string except = null)
        {
            var dirInfo = new DirectoryInfo(path);
            var folders = dirInfo
                .GetDirectories("*.*", SearchOption.TopDirectoryOnly)
                .Select(dir => dir.FullName)
                .ToArray();
            Array.ForEach(folders, folder => DeleteFolder(folder));
            var files = dirInfo
                .GetFiles("*.*", SearchOption.TopDirectoryOnly)
                .Select(file => file.FullName)
                .Except(new string[] { Path.Combine(Environment.CurrentDirectory, except) })
                .ToArray();
            Array.ForEach(files, file => File.Delete(file));
        }

        protected readonly IBuildSettings _settings;
        private readonly ICryptoService _crypto = new CryptoService(new CertificateManager());
        private readonly CliRunner _runner = new();
        private string _currentDir, _oldHash, _newHash;
    }
}
