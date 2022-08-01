using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        public virtual void ConfigureService(string imageRoot, string dockerPath)
        {
            _currentDir = Environment.CurrentDirectory;
            _rootFolder = Path.GetDirectoryName(imageRoot);

            // Extract service image file to a temporary folder, using gzip and tar tools...
            var imageFileName = String.Format($"{ServiceName}.tar.gz");
            var path = Path.Combine(imageRoot, imageFileName);
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
            RestoreImageFile(imageFileName);

            // Load modified image file to Docker...
            Environment.SetEnvironmentVariable("Path", dockerPath, EnvironmentVariableTarget.Process);
            _runner.Run($"docker load -i {imageFileName}");
            Environment.CurrentDirectory = _currentDir;
            File.Delete(Path.Combine(tempPath, imageFileName));
            Directory.Delete(tempPath);
        }

        protected abstract string ServiceName { get; }

        protected abstract ITextTemplate SettingsTemplate { get; }

        protected virtual string AppLayerFolder => "app";

        protected virtual void ConfigureAppLayer(string layerId)
        {
            var root = Path.Combine(Environment.CurrentDirectory, layerId, "app");
            var path = Path.Combine(root, ToolConstants.DevAppSettings);
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            path = Path.Combine(root, ToolConstants.AppSettings);
            File.WriteAllText(path, SettingsTemplate.TransformText());
        }

        protected string RootFolder
        {
            get { return _rootFolder; }
        }

        private string ExtractImageFile(string path)
        {
            var gzFile = Path.GetFileName(path);
            var tarFile = gzFile.Replace(".gz", String.Empty);
            var tempFolder = FileUtility.GetTempFolderPath();
            var tempCopyPath = Path.Combine(tempFolder, Path.GetFileName(path));
            File.Copy(path, tempCopyPath);
            Environment.CurrentDirectory = tempFolder;
            _archive.GunZip(gzFile);
            _archive.UnTar(tarFile);
            File.Delete(tarFile);
            return Environment.CurrentDirectory;
        }

        private static string GetAppLayerFolder(string tempPath)
        {
            string layerId = String.Empty;
            var dirInfo = new DirectoryInfo(tempPath);
            foreach (var item in dirInfo.GetFiles("json", SearchOption.AllDirectories))
            {
                var config = JsonHelper.To<DockerLayerConfig>(File.ReadAllText(item.FullName));
                if (config.Config != null)
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
            var tarPath = Path.Combine(Environment.CurrentDirectory, ToolConstants.LayerTarFile);
            if (File.Exists(tarPath))
            {
                _oldHash = _crypto
                    .CreateHash(File.ReadAllBytes(tarPath))
                    .ToLower();
                _archive.UnTar(ToolConstants.LayerTarFile);
                File.Delete(tarPath);
            }
            else
            {
                Console.WriteLine($"WARNING: Layer file '{ToolConstants.LayerTarFile}' not found.");
            }

            Environment.CurrentDirectory = Path.GetDirectoryName(Environment.CurrentDirectory);
        }

        private void RestoreAppLayer(string layerId)
        {
            Environment.CurrentDirectory = Path.Combine(Environment.CurrentDirectory, layerId);
            _archive.Tar(ToolConstants.LayerTarFile, AppLayerFolder);
            _newHash = _crypto
                .CreateHash(File.ReadAllBytes(Path.Combine(Environment.CurrentDirectory, ToolConstants.LayerTarFile)))
                .ToLower();
            DeleteFolder(Path.Combine(Environment.CurrentDirectory, AppLayerFolder));
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
            _archive.Tar(tarFile, String.Join(" ", items));
            _archive.GZip(tarFile);
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
        protected readonly CliRunner _runner = new();
        private string _rootFolder;
        private readonly ICryptoService _crypto = CryptoService.Default;
        private readonly ArchiveUtility _archive = new(@"..\tools", false);
        private string _currentDir, _oldHash, _newHash;
    }
}
