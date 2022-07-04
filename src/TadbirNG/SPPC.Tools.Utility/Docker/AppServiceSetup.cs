using System;
using System.IO;
using System.Linq;
using SPPC.Tools.Model;
using SPPC.Tools.Transforms;

namespace SPPC.Tools.Utility
{
    public class AppServiceSetup : DockerServiceSetup
    {
        public AppServiceSetup(IBuildSettings settings)
            : base(settings)
        {
        }

        protected override string ServiceName => "web-app";

        protected override ITextTemplate SettingsTemplate => null;

        protected override string AppLayerFolder => "usr";

        protected override void ConfigureAppLayer(string layerId)
        {
            var path = Path.Combine(Environment.CurrentDirectory, layerId, "usr", "share", "nginx", "html");
            var mainFile = new DirectoryInfo(path)
                .GetFiles("*.*", SearchOption.TopDirectoryOnly)
                .Where(fi => fi.Name.StartsWith("main")
                    && fi.Name.EndsWith(".js"))
                .Select(fi => fi.FullName)
                .FirstOrDefault();
            if (!String.IsNullOrEmpty(mainFile))
            {
                var apiServerUrl = String.Format(
                    $"http://localhost:{BuildSettingValues.DefaultApiPort}");
                var licenseServerUrl = String.Format(
                    $"http://localhost:{BuildSettingValues.DefaultLicenseApiPort}");
                var contents = File.ReadAllText(mainFile);
                contents = contents
                    .Replace(DefaultKey, _settings.Key)
                    .Replace(apiServerUrl, _settings.WebApiUrl)
                    .Replace(licenseServerUrl, _settings.LocalServerUrl);
                File.WriteAllText(mainFile, contents);
            }
        }

        private const string DefaultKey = "AppInstanceKey";
    }
}
