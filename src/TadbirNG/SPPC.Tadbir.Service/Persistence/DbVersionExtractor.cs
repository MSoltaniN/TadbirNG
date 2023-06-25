using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using SPPC.Tadbir.Common;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// امکانات مورد نیاز برای استخراج آخرین نسخه دیتابیس های مختلف برنامه را پیاده سازی می کند
    /// </summary>
    public class DbVersionExtractor : IDbVersionExtractor
    {
        /// <summary>
        /// آخرین نسخه دیتابیس سیستمی را از اسکریپت ارتقاء متناظر به دست می آورد
        /// </summary>
        /// <param name="scriptRoot">مسیر کامل پوشه ای که اسکریپت های ارتقاء دیتابیس درون آن قرار دارد</param>
        /// <returns>آخرین نسخه دیتابیس سیستمی</returns>
        public string GetSystemDbVersion(string scriptRoot)
        {
            var scriptPath = Path.Combine(scriptRoot, ScriptConstants.SysDbUpdateScript);
            return GetLatestDbVersion(scriptPath);
        }

        /// <summary>
        /// آخرین نسخه دیتابیس شرکتی را از اسکریپت ارتقاء متناظر به دست می آورد
        /// </summary>
        /// <param name="scriptRoot">مسیر کامل پوشه ای که اسکریپت های ارتقاء دیتابیس درون آن قرار دارد</param>
        /// <returns>آخرین نسخه دیتابیس شرکتی</returns>
        public string GetCompanyDbVersion(string scriptRoot)
        {
            var scriptPath = Path.Combine(scriptRoot, ScriptConstants.DbUpdateScript);
            return GetLatestDbVersion(scriptPath);
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
    }
}
