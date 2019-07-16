using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using BabakSoft.Platform.Data;
using SPPC.Framework.Helpers;

namespace SPPC.Framework.Tools.ProjectCLI
{
    public class RefreshDatabasesCommand : ICliCommand
    {
        public void Execute()
        {
            var version = GetSysDatabaseVersion();
            var scriptFile = ConfigurationManager.AppSettings["UpdateScriptPath"];
            var blocks = GetRequiredBlocks(version, File.ReadAllText(scriptFile));
            if (blocks.Count > 0)
            {
                BuildTempScriptFile(blocks.Values.Cast<string>().ToList());
                DoRefreshDatabase();
                File.Delete(_tempScript);
            }
            else
            {
                Console.WriteLine("Databases are up-to-date.");
            }
        }

        private Version GetSysDatabaseVersion()
        {
            var sysDal = new SqlDataLayer(GetSysConnectionString(), ProviderType.SqlClient);
            var result = sysDal.QueryScalar("SELECT Number FROM [Core].[Version]");
            return new Version(result.ToString());
        }

        private string GetSysConnectionString()
        {
            string path = @"..\..\..\src\Framework\SPPC.Tadbir.Web.Api\appsettings.Development.json";
            var appSettings = JsonHelper.To<AppSettingsModel>(File.ReadAllText(path));
            return appSettings.ConnectionStrings.TadbirSysApi;
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
                        blocks.Add(ver, script.Substring(match.Index));
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

        private void DoRefreshDatabase()
        {
            Console.WriteLine();
            Console.WriteLine("Performing database schema refresh...");
            var sqlBuilder = new SqlConnectionStringBuilder(GetSysConnectionString());
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

        private const string _argsTemplate = @"-S {0} -d {1} -i {2} -b -E -I -j";
        private const string _scriptBlockRegex = @"-- (\d{1,}).(\d{1,}).(\d{1,})";
        private const string _tempScript = "Update.sql";
    }
}
