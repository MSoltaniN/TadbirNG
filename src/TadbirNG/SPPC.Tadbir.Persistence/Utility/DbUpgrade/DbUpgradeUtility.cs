using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using SPPC.Framework.Common;
using SPPC.Framework.Persistence;
using SPPC.Tadbir.Model.Config;

namespace SPPC.Tadbir.Persistence.DbUpgrade
{
    /// <summary>
    /// عملیات مورد نیاز برای ارتقاء ساختار یک دیتابیس را پیاده سازی می کند
    /// </summary>
    public class DbUpgradeUtility
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="dbConsole">امکان اجرای مستقیم دستورات دیتابیسی را فراهم می کند</param>
        public DbUpgradeUtility(ISqlConsole dbConsole)
        {
            _dbConsole = dbConsole;
        }

        /// <summary>
        /// دیتابیس مشخص شده با رشته اتصال را با استفاده از یک اسکریپت ارتقاء می دهد
        /// </summary>
        /// <param name="connection">رشته اتصال به دیتابیس مورد نظر برای ارتقاء</param>
        /// <param name="scriptPath">مسیر کامل پوشه ای که فایل اسکریپت به روزرسانی در آن قرار دارد</param>
        /// <returns>تعداد تغییرات اعمال شده روی دیتابیس مورد نظر برای ارتقاء</returns>
        public int UpgradeDatabase(string connection, string scriptPath)
        {
            var sqlBuilder = new SqlConnectionStringBuilder(connection);
            var script = sqlBuilder.InitialCatalog == DbUpgradeConstants.SysDbName
                ? DbUpgradeConstants.SysDbUpgradeScript
                : DbUpgradeConstants.DbUpgradeScript;
            var scriptFile = Path.Combine(scriptPath, script);
            var version = GetDatabaseVersion(connection);
            var blocks = GetScriptBlocks(version, File.ReadAllText(scriptFile, Encoding.UTF8));
            if (blocks.Count > 0)
            {
                RunScriptBlocks(blocks);
            }

            return blocks.Count;
        }

        /// <summary>
        /// رشته های اتصال به دیتابیس را برای کلیه شرکت های فعال در برنامه تدبیر خوانده و برمی گرداند
        /// </summary>
        /// <param name="sysConnection">رشته اتصال به دیتابیس سیستمی تدبیر</param>
        /// <returns>مجموعه ای از رشته های اتصال به شرکت های فعال</returns>
        public IEnumerable<string> GetCompanyConnections(string sysConnection)
        {
            var connections = new List<string>();
            _dbConsole.ConnectionString = sysConnection;
            var result = _dbConsole.ExecuteQuery(DbUpgradeQuery.ActiveCompanies);
            foreach (DataRow row in result.Rows)
            {
                var company = new CompanyDb()
                {
                    DbName = row["DbName"].ToString(),
                    Server = row["Server"].ToString(),
                    UserName = row["UserName"].ToString(),
                    Password = row["Password"].ToString()
                };
                connections.Add(BuildConnectionString(company));
            }

            return connections;
        }

        private static string BuildConnectionString(CompanyDb company)
        {
            var builder = new SqlConnectionStringBuilder
            {
                DataSource = company.Server,
                InitialCatalog = company.DbName,
                IntegratedSecurity = false,
                MultipleActiveResultSets = true
            };
            if (!String.IsNullOrEmpty(company.UserName) && !String.IsNullOrEmpty(company.Password))
            {
                builder.UserID = company.UserName;
                builder.Password = company.Password;
            }
            else
            {
                builder.UserID = DbUpgradeConstants.SysLoginName;
                builder.Password = DbUpgradeConstants.SysLoginPassword;
            }

            return builder.ConnectionString;
        }

        private static Dictionary<Version, string> GetScriptBlocks(Version version, string script)
        {
            var blocks = new Dictionary<Version, string>();
            var rx = new Regex(DbUpgradeConstants.ScriptBlockRegex);
            foreach (Match match in rx.Matches(script))
            {
                var ver = new Version(
                    Int32.Parse(match.Groups[1].Value),
                    Int32.Parse(match.Groups[2].Value),
                    Int32.Parse(match.Groups[3].Value));
                if (ver > version)
                {
                    var nextMatch = match.NextMatch();
                    if (nextMatch.Success)
                    {
                        blocks.Add(ver, script[match.Index..nextMatch.Index]);
                    }
                    else
                    {
                        blocks.Add(ver, script[match.Index..] + Environment.NewLine
                            + String.Format(DbUpgradeQuery.UpdateDbVersion, ver.ToString()));
                    }
                }
            }

            return blocks;
        }

        private int RunScriptBlocks(Dictionary<Version, string> blocks)
        {
            var sorted = blocks.OrderBy(entry => entry.Key);
            var scriptBuilder = new StringBuilder();
            scriptBuilder.AppendLine("BEGIN TRANSACTION;");
            scriptBuilder.AppendLine();
            Array.ForEach(sorted.ToArray(), block => scriptBuilder.AppendLine(block.Value));
            scriptBuilder.AppendLine();
            scriptBuilder.AppendLine("COMMIT;");

            _dbConsole.ExecuteNonQuery(scriptBuilder.ToString());
            return sorted.Count();
        }

        private Version GetDatabaseVersion(string connection)
        {
            Verify.ArgumentNotNullOrEmptyString(connection, nameof(connection));
            _dbConsole.ConnectionString = connection;
            var result = _dbConsole.ExecuteQuery(DbUpgradeQuery.FetchDbVersion);
            if (result.Rows.Count > 0)
            {
                return new Version(result.Rows[0][0].ToString());
            }

            return new Version(result.ToString());
        }

        private readonly ISqlConsole _dbConsole;
    }
}
