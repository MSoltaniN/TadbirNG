using System.IO;
using SPPC.Tools.Model;
using SPPC.Tools.Transforms;
using SPPC.Tools.Transforms.Templates;

namespace SPPC.Tools.Utility
{
    public class ConfigUtility
    {
        public static void GenerateConfig(IBuildSettings settings)
        {
            // Generate settings for license server...
            ITextTemplate template = new LocalLicenseApiSettings(settings);
            string path = Path.Combine(PathConfig.LocalServerRoot, ConfigConstants.ConfigFile);
            File.WriteAllText(path, template.TransformText());

            // Generate settings and artifacts for api server...
            template = new WebApiSettings(settings);
            path = Path.Combine(PathConfig.WebApiRoot, ConfigConstants.ConfigFile);
            File.WriteAllText(path, template.TransformText());

            // Generate environment files for web app...
        }
    }
}
