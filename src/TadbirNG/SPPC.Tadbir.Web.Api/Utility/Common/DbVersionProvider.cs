using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using SPPC.Tadbir.Common;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// امکان خواندن آخرین نسخه را برای دیتابیس های برنامه فراهم می کند
    /// </summary>
    public class DbVersionProvider : IDbVersionProvider
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="paths">مسیرهای فایل های کاربردی مورد نیاز در سرویس وب را فراهم می کند</param>
        public DbVersionProvider(IApiPathProvider paths)
        {
            _paths = paths;
        }

        /// <summary>
        /// نسخه جاری دیتابیس سیستمی برنامه را برمی گرداند
        /// </summary>
        public string SystemDbVersion
        {
            get { return GetSystemDbVersion(); }
        }

        /// <summary>
        /// نسخه جاری دیتابیس شرکتی برنامه را برمی گرداند
        /// </summary>
        public string CompanyDbVersion
        {
            get { return GetCompanyDbVersion(); }
        }

        private static string GetLatestDbVersion(string path)
        {
            var latestVersion = "1.0.0";
            var script = File.ReadAllText(path);
            var regex = new Regex(ScriptConstants.ScriptBlockRegex);
            var match = regex
                .Matches(script)
                .LastOrDefault();
            if (match != null)
            {
                latestVersion = $"{match.Groups[1].Value}.{match.Groups[2].Value}.{match.Groups[3].Value}";
            }

            return latestVersion;
        }

        private string GetSystemDbVersion()
        {
            var scriptPath = Path.Combine(_paths.ScriptRoot, ScriptConstants.SysDbUpdateScript);
            return GetLatestDbVersion(scriptPath);
        }

        private string GetCompanyDbVersion()
        {
            var scriptPath = Path.Combine(_paths.ScriptRoot, ScriptConstants.SysDbUpdateScript);
            return GetLatestDbVersion(scriptPath);
        }

        private readonly IApiPathProvider _paths;
    }
}
