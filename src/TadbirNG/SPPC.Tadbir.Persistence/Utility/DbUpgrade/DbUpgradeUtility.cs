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
using SPPC.Tadbir.Configuration;
using SPPC.Tadbir.Model.Config;

namespace SPPC.Tadbir.Persistence.DbUpgrade
{
    /// <summary>
    /// عملیات مورد نیاز برای ارتقاء ساختار یک دیتابیس را پیاده سازی می کند
    /// </summary>
    public class DbUpgradeUtility : IDbUpgrade
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
        /// دیتابیس مشخص شده با رشته اتصال را از نظر نیاز به ارتقاء بررسی می کند
        /// </summary>
        /// <param name="connection">رشته اتصال به دیتابیس مورد نظر برای ارتقاء</param>
        /// <param name="scriptPath">مسیر کامل پوشه ای که فایل اسکریپت به روزرسانی در آن قرار دارد</param>
        /// <returns>در صورت نیاز دیتابیس به ارتقاء مقدار بولی "درست" و در غیر این صورت مقدار
        /// بولی "نادرست" را برمی گرداند</returns>
        public bool NeedsUpgrade(string connection, string scriptPath)
        {
            Verify.ArgumentNotNullOrEmptyString(connection, nameof(connection));
            Verify.ArgumentNotNullOrEmptyString(scriptPath, nameof(scriptPath));
            var versions = new List<Version>();
            var dbVersion = GetDatabaseVersion(connection);
            var script = LoadUpdateScript(connection, scriptPath);
            var regex = new Regex(DbUpgradeConstants.ScriptBlockRegex);
            foreach (Match match in regex.Matches(script))
            {
                versions.Add(new Version(
                    Int32.Parse(match.Groups[1].Value),
                    Int32.Parse(match.Groups[2].Value),
                    Int32.Parse(match.Groups[3].Value)));
            }

            var latestVersion = versions
                .OrderByDescending(ver => ver)
                .FirstOrDefault();
            return latestVersion > dbVersion;
        }

        /// <summary>
        /// دیتابیس مشخص شده با رشته اتصال را با استفاده از یک اسکریپت ارتقاء می دهد
        /// </summary>
        /// <param name="connection">رشته اتصال به دیتابیس مورد نظر برای ارتقاء</param>
        /// <param name="scriptPath">مسیر کامل پوشه ای که فایل اسکریپت به روزرسانی در آن قرار دارد</param>
        /// <returns>تعداد تغییرات اعمال شده روی دیتابیس مورد نظر برای ارتقاء</returns>
        public int UpgradeDatabase(string connection, string scriptPath)
        {
            Verify.ArgumentNotNullOrEmptyString(connection, nameof(connection));
            Verify.ArgumentNotNullOrEmptyString(scriptPath, nameof(scriptPath));
            var version = GetDatabaseVersion(connection);
            var updateScript = LoadUpdateScript(connection, scriptPath);
            var blocks = GetScriptBlocks(version, updateScript);
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

        private static string LoadUpdateScript(string connection, string scriptFolder)
        {
            string updateScript = String.Empty;
            var dbConfig = SysParameterUtility.AllParameters.Db;
            var sqlBuilder = new SqlConnectionStringBuilder(connection);
            var scriptFileName = sqlBuilder.InitialCatalog == dbConfig.SysDbName
                ? DbUpgradeConstants.SysDbUpgradeScript
                : DbUpgradeConstants.DbUpgradeScript;
            var scriptPath = Path.Combine(scriptFolder, scriptFileName);
            if (File.Exists(scriptPath))
            {
                updateScript = File.ReadAllText(scriptPath, Encoding.UTF8);
            }

            return updateScript;
        }

        private static string BuildConnectionString(CompanyDb company)
        {
            var dbConfig = SysParameterUtility.AllParameters.Db;
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
                builder.UserID = dbConfig.LoginName;
                builder.Password = dbConfig.Password;
            }

            return builder.ConnectionString;
        }

        private static Dictionary<Version, string> GetScriptBlocks(Version version, string script)
        {
            var blocks = new Dictionary<Version, string>();
            var regex = new Regex(DbUpgradeConstants.ScriptBlockRegex);
            foreach (Match match in regex.Matches(script))
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
            var dbVersion = new Version("1.0.0.0");
            _dbConsole.ConnectionString = connection;
            var result = _dbConsole.ExecuteQuery(DbUpgradeQuery.FetchDbVersion);
            if (result.Rows.Count > 0)
            {
                dbVersion = new Version(result.Rows[0][0].ToString());
            }

            return dbVersion;
        }

        private readonly ISqlConsole _dbConsole;
    }
}
