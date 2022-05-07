using System;
using System.Data.SqlClient;
using System.IO;
using SPPC.Framework.Extensions;
using SPPC.Framework.Persistence;
using SPPC.Tadbir.Persistence.DbUpgrade;
using SPPC.Tools.Utility;

namespace SPPC.Tools.SystemDesignerCli
{
    public class RefreshDatabasesCommand : ICliCommand
    {
        public void Execute()
        {
            string sysConnection = DbConnections.SystemConnection;
            var scriptPath = @"..\..\..\res";
            var dbUpgrade = new DbUpgradeUtility(new SqlServerConsole());
            ReportProgress(dbUpgrade, sysConnection, scriptPath);
            var companyConnections = dbUpgrade.GetCompanyConnections(sysConnection);
            foreach (var connection in companyConnections)
            {
                ReportProgress(dbUpgrade, connection, scriptPath);
            }

            RefereshRuntimeDbScripts();
        }

        private static void ReportProgress(DbUpgradeUtility dbUpgrade, string connection, string scriptPath)
        {
            try
            {
                var sqlBuilder = new SqlConnectionStringBuilder(connection);
                Console.WriteLine();
                Console.WriteLine("Performing schema refresh for database '{0}'...", sqlBuilder.InitialCatalog);
                int changes = dbUpgrade.UpgradeDatabase(connection, scriptPath);
                if (changes > 0)
                {
                    Console.WriteLine();
                    Console.WriteLine("Refresh Database command completed without errors.");
                }
                else
                {
                    Console.WriteLine("Database '{0}' is up-to-date.", sqlBuilder.InitialCatalog);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetErrorInfo());
                Console.WriteLine();
                Console.WriteLine("Refresh Database command completed with errors.");
            }
        }

        private static void RefereshRuntimeDbScripts()
        {
            if (File.GetLastWriteTime(_mainDbScriptPath) >
                     File.GetLastWriteTime(_runtimeDbScriptPath))
            {
                File.Copy(_mainDbScriptPath, _runtimeDbScriptPath, true);
            }
        }

        private const string _mainDbScriptPath = @"..\..\..\res\Tadbir_CreateDbObjects.sql";
        private const string _runtimeDbScriptPath = @"..\..\..\src\TadbirNG\SPPC.Tadbir.Web.Api\wwwroot\static\Tadbir_CreateDbObjects.sql";
    }
}
