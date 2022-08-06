using System.IO;

namespace SPPC.Tools.Model
{
    public static class PathConfig
    {
        public static string ResourceRoot => Path.Combine("..", "..", "..", "res");

        public static string SolutionRoot => Path.Combine("..", "..", "..", "src", "TadbirNG");

        public static string LocalServerRoot => Path.Combine(SolutionRoot, "SPPC.Licensing.Local.Web");

        public static string WebApiRoot => Path.Combine(SolutionRoot, "SPPC.Tadbir.Web.Api");

        public static string WebAppRoot => Path.Combine(SolutionRoot, "SPPC.Tadbir.Web");

        public static string WebEnvRoot => Path.Combine(WebAppRoot, "ClientApp", "src", "environments");

        public static string DockerCacheRoot => Path.Combine("..", "..", "..", "..", "dockercache");

        public static string ComposePath => Path.Combine(SolutionRoot, "docker-compose.yml");

        public static string OverridePath => Path.Combine(SolutionRoot, "docker-compose.override.yml");

        public static string EditionConfig => Path.Combine(SolutionRoot, "SPPC.Tools.LicenseManager", "edition-config.json");

        public static string ServicePublishWin => Path.Combine("..", "..", "Release", "net5.0", "publish", "win-x64");

        public static string RunnerPublishWin => Path.Combine("..", "..", "Release", "net5.0-windows", "publish", "win-x64");

        public static string TadbirRelease => Path.Combine("..", "..", "..", "..", "TadbirNG Release");

        public static string ToolsFolder => Path.Combine("..", "..", "..", "misc", "tools");
    }
}
