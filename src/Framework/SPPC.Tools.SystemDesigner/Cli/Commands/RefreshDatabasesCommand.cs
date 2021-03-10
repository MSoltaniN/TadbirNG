using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using SPPC.Framework.Helpers;
using SPPC.Framework.Persistence;
using SPPC.Tools.Model;

namespace SPPC.Tools.SystemDesigner.Cli
{
    public class RefreshDatabasesCommand : ICliCommand
    {
        public void Execute()
        {
            string sysConnection = GetSysConnectionString();
            if (sysConnection.IndexOf("130.185.76.7") == -1)
            {
                UpdateDatabaseFromScript(sysConnection, "SysUpdateScriptPath");
                var companyConnections = GetCompanyConnectionStrings(sysConnection);
                foreach (string connection in companyConnections)
                {
                    UpdateDatabaseFromScript(connection, "UpdateScriptPath");
                }
            }

            RefereshRuntimeCreateTadbirDBScripts();
        }

        private string GetSysConnectionString()
        {
            string path = @"..\..\..\src\Framework\SPPC.Tadbir.Web.Api\appsettings.Development.json";
            var appSettings = JsonHelper.To<AppSettingsModel>(File.ReadAllText(path));
            return appSettings.ConnectionStrings.TadbirSysApi;
        }

        private string[] GetCompanyConnectionStrings(string sysConnection)
        {
            var connections = new List<string>();
            var dal = new SqlDataLayer(sysConnection);
            var result = dal.Query("SELECT DbName, Server, UserName, Password FROM [Config].[CompanyDb]");
            foreach (DataRow row in result.Rows)
            {
                var company = new CompanyDbModel()
                {
                    DbName = row[0].ToString(),
                    Server = row[1].ToString(),
                    UserName = row[2].ToString(),
                    Password = row[3].ToString()
                };
               connections.Add(BuildConnectionString(company));
            }

            return connections.ToArray();
        }

        private string BuildConnectionString(CompanyDbModel company)
        {
            var builder = new StringBuilder();
            builder.AppendFormat("Server={0};Database={1};", company.Server, company.DbName);
            if (!String.IsNullOrEmpty(company.UserName) && !String.IsNullOrEmpty(company.Password))
            {
                builder.AppendFormat("User ID={0};Password={1};Trusted_Connection=False;MultipleActiveResultSets=True",
                    company.UserName, company.Password);
            }
            else
            {
                builder.Append("Trusted_Connection=True;MultipleActiveResultSets=True");
            }

            return builder.ToString();
        }

        private void UpdateDatabaseFromScript(string connection, string configKey)
        {
            var version = GetDatabaseVersion(connection);
            var sqlBuilder = new SqlConnectionStringBuilder(connection);
            var scriptFile = ConfigurationManager.AppSettings[configKey];
            var blocks = GetRequiredBlocks(version, File.ReadAllText(scriptFile));
            if (blocks.Count > 0)
            {
                BuildTempScriptFile(blocks.Values.Cast<string>().ToList());
                DoRefreshDatabase(connection);
                File.Delete(_tempScript);
            }
            else
            {
                Console.WriteLine("Database '{0}' is up-to-date.", sqlBuilder.InitialCatalog);
            }

        }

        private Version GetDatabaseVersion(string connection)
        {
            var dal = new SqlDataLayer(connection);
            var result = dal.QueryScalar("SELECT Number FROM [Core].[Version]");
            return new Version(result.ToString());
        }

        private Dictionary<Version, string> GetRequiredBlocks(Version version, string script)
        {
            var blocks = new Dictionary<Version, string>();
            var rx = new Regex(_scriptBlockRegex);
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
                        blocks.Add(ver, script.Substring(match.Index, nextMatch.Index - match.Index));
                    }
                    else
                    {
                        blocks.Add(ver, script.Substring(match.Index) + string.Format(" \n UPDATE [Core].[Version] SET Number = '{0}'", ver.ToString()));
                    }
                }
            }

            return blocks;
        }

        private void BuildTempScriptFile(List<string> blocks)
        {
            var builder = new StringBuilder();
            builder.AppendLine("BEGIN TRANSACTION;");
            builder.AppendLine();
            Array.ForEach(blocks.ToArray(), block => builder.AppendLine(block));
            builder.AppendLine();
            builder.AppendLine("COMMIT;");
            File.AppendAllText(_tempScript, builder.ToString());
        }

        private void DoRefreshDatabase(string connection)
        {
            var sqlBuilder = new SqlConnectionStringBuilder(connection);
            Console.WriteLine();
            Console.WriteLine("Performing schema refresh for database '{0}'...", sqlBuilder.InitialCatalog);
            string utilityPath = ConfigurationManager.AppSettings["CmdUtilityPath"];
            var psi = new ProcessStartInfo()
            {
                Arguments = String.Format(
                    _argsTemplate, sqlBuilder.DataSource, sqlBuilder.InitialCatalog, _tempScript),
                ErrorDialog = false,
                FileName = utilityPath,
                UseShellExecute = false,
                WindowStyle = ProcessWindowStyle.Minimized
            };
            var process = new Process() { StartInfo = psi };
            process.Start();
            process.WaitForExit();
            if (process.ExitCode == 0)
            {
                Console.WriteLine();
                Console.WriteLine("Refresh Database command completed without errors.");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Refresh Database command completed with error(s).");
            }
        }

        private void RefereshRuntimeCreateTadbirDBScripts()
        {
            if (File.GetLastWriteTime(_createTadbirDBScriptFilePath) >
                     File.GetLastWriteTime(_runetimeCreateTadbirDBScriptFilePath))
            {
                File.Copy(_createTadbirDBScriptFilePath, _runetimeCreateTadbirDBScriptFilePath,true);
            }
        }

        private const string _argsTemplate = @"-S {0} -d {1} -i {2} -b -E -I -j";
        private const string _scriptBlockRegex = @"-- (\d{1,}).(\d{1,}).(\d{1,})";
        private const string _tempScript = "Update.sql";
        private const string _createTadbirDBScriptFilePath = @"..\..\..\res\Tadbir_CreateDbObjects.sql";
        private const string _runetimeCreateTadbirDBScriptFilePath = @"..\..\..\src\Framework\SPPC.Tadbir.Web.Api\wwwroot\static\Tadbir_CreateDbObjects.sql";
    }
}
