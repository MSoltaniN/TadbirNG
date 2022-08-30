using System;
using System.IO;
using System.Linq;
using SPPC.Framework.Utility;
using SPPC.Tadbir.Configuration;
using SPPC.Tadbir.Utility.Model;

namespace SPPC.Tadbir.Utility.Docker
{
    public class AppServiceSetup : DockerServiceSetup
    {
        public AppServiceSetup(IBuildSettings settings)
            : base(settings)
        {
        }

        protected override string ServiceName => SysParameterUtility.WebApp.ImageName;

        protected override ITextTemplate SettingsTemplate => null;

        protected override string AppLayerFolder => "usr";

        protected override void ConfigureAppLayer(string layerId)
        {
            var path = Path.Combine(Environment.CurrentDirectory, layerId, AppLayerFolder, "share", "nginx", "html");
            var mainFile = new DirectoryInfo(path)
                .GetFiles("*.*", SearchOption.TopDirectoryOnly)
                .Where(fi => fi.Name.StartsWith("main")
                    && fi.Name.EndsWith(".js"))
                .Select(fi => fi.FullName)
                .FirstOrDefault();
            if (!String.IsNullOrEmpty(mainFile))
            {
                var apiServerUrl = BuildSettings.DockerLocal.WebApiUrl;
                var licenseServerUrl = BuildSettings.DockerLocal.LocalServerUrl;
                var contents = File.ReadAllText(mainFile);
                contents = contents
                    .Replace(BuildSettingValues.DummyInstanceKey, _settings.Key)
                    .Replace(apiServerUrl, _settings.WebApiUrl)
                    .Replace(licenseServerUrl, _settings.LocalServerUrl);
                File.WriteAllText(mainFile, contents);
            }
        }
    }
}
