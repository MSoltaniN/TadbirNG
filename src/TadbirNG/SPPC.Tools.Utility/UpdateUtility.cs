using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPPC.Framework.Helpers;
using SPPC.Tools.Model;
using SPPC.Framework.Common;

namespace SPPC.Tools.Utility
{
    public class UpdateUtility
    {
        public UpdateUtility(string imageRoot)
        {
            _imageRoot = FileUtility.GetAbsolutePath(imageRoot);
            _runner = new CliRunner();
            _runner.OutputReceived += Runner_OutputReceived;
        }

        public VersionInfo GetCurrentVersionInfo(string edition)
        {
            Verify.ArgumentNotNullOrEmptyString(edition, nameof(edition));
            var editionTag = DockerUtility.GetEditionTag(edition);
            var currentDir = Environment.CurrentDirectory;
            Environment.CurrentDirectory = _imageRoot;
            var version = new VersionInfo()
            {
                Version = QueryAppVersion()
            };
            version.Services[0] = GetServiceInfo(DockerService.LicenseServerImage);
            version.Services[1] = GetServiceInfo(DockerService.ApiServerImage, editionTag);
            version.Services[2] = GetServiceInfo(DockerService.DbServerImage);
            version.Services[3] = GetServiceInfo(DockerService.WebAppImage);
            Environment.CurrentDirectory = currentDir;
            return version;
        }

        public byte[] GetImageData(string serviceName, string edition = null)
        {
            string suffix = edition != null
                ? $"-{DockerUtility.GetEditionTag(edition)}"
                : String.Empty;
            var imagePath = Path.Combine(_imageRoot, serviceName, $"{serviceName}{suffix}.tar.gz");
            return File.ReadAllBytes(imagePath);
        }

        public void UpdateImageCache()
        {
            var currentDir = Environment.CurrentDirectory;
            Environment.CurrentDirectory = _imageRoot;
            _runner.Run("git pull --progress");
            Environment.CurrentDirectory = currentDir;
        }

        private static ServiceInfo GetServiceInfo(string serviceName, string editionTag = null)
        {
            // NOTE: This method assumes current directory to be root folder of local dockercache clone
            var suffix = editionTag != null ? $"-{editionTag}" : String.Empty;
            var imagePath = Path.Combine(
                Environment.CurrentDirectory, serviceName, $"{serviceName}{suffix}.tar.gz");
            return DockerUtility.GetServiceInfo(imagePath);
        }

        private string QueryAppVersion()
        {
            // NOTE: This method assumes current directory to be root folder of local dockercache clone
            var output = _runner.Run("git show --oneline");
            return output
                .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                .Where(line => line.Contains("UI-"))
                .Select(line => line[(line.IndexOf("UI-") + 3)..])
                .FirstOrDefault();
        }

        private void Runner_OutputReceived(object sender, OutputReceivedEventArgs e)
        {
            if (!String.IsNullOrEmpty(e.Output))
            {
                File.AppendAllText(_logPath, e.Output.Replace("\n", Environment.NewLine));
            }
        }

        private const string _logPath = "update.log";
        private readonly string _imageRoot;
        private readonly CliRunner _runner;
    }
}
