using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace SPPC.Tools.Model
{
    /// <summary>
    /// امکان خواندن اطلاعات نسخه برنامه و سرویس را فراهم می کند
    /// </summary>
    public static class VersionInfo
    {
        /// <summary>
        /// نسخه سرویس وب را از فایل مربوطه خوانده و برمی گرداند
        /// </summary>
        /// <returns>نسخه سرویس وب</returns>
        public static string GetApiVersion()
        {
            string apiVersion = GetVersionFromFile(_apiVersionPath);
            return apiVersion;
        }

        /// <summary>
        /// نسخه برنامه وب را از فایل مربوطه خوانده و برمی گرداند
        /// </summary>
        /// <returns>نسخه برنامه وب</returns>
        public static string GetAppVersion()
        {
            string appVersion = GetVersionFromFile(_appVersionPath);
            return appVersion;
        }

        private static string GetVersionFromFile(string path)
        {
            string version = String.Empty;
            string contents = File.ReadAllText(path);
            var regex = new Regex(_versionRegex);
            var match = regex.Matches(contents)
                .Cast<Match>()
                .FirstOrDefault();
            if (match != null)
            {
                version = match.Groups[0].Value;
            }

            return version;
        }

        private static readonly string _apiVersionPath = Path.Combine(PathConfig.SolutionRoot, "FrameworkSolutionInfo.cs");
        private static readonly string _appVersionPath = Path.Combine(PathConfig.SolutionRoot, "TadbirSolutionInfo.cs");
        private const string _versionRegex = @"(\d{1,}).(\d{1,}).(\d{1,})";
    }
}
