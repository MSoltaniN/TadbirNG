using System.Configuration;
using System.IO;

namespace SPPC.Tools.LicenseManager.Utility
{
    public static class PathConfig
    {
        public static string SolutionRoot => ConfigurationManager.AppSettings["SolutionRootPath"];

        public static string LocalServerRoot => ConfigurationManager.AppSettings["LocalServerRootPath"];

        public static string WebApiRoot => ConfigurationManager.AppSettings["WebApiRootPath"];

        public static string WebEnvRoot => ConfigurationManager.AppSettings["WebEnvRootPath"];

        public static string EditionConfig => ConfigurationManager.AppSettings["EditionConfigPath"];

        public static string ServicePublishWin => @"..\..\Release\net5.0\publish\win-x64";

        public static string RunnerPublish => @"..\..\Release\net5.0-windows\publish\win-x64";

        public static string TadbirRelease => @"..\..\..\..\TadbirNG Release";
    }
}
