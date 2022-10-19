using SPPC.Framework.Extensions;
using SPPC.Framework.Persistence;
using SPPC.Tadbir.Configuration;
using SPPC.Tadbir.Persistence.DbUpgrade;
using SPPC.Tools.Model;
using System;
using System.Configuration;
using System.IO;
using System.Text.RegularExpressions;

namespace SPPC.Tools.SystemDesignerCli
{
    public class RefreshDBScriptsCommand : ICliCommand
    {
        public void Execute()
        {
            var scriptPath = @"..\..\..\res";
            ReportProgress(scriptPath);
        }

        private static void ReportProgress(string scriptPath)
        {
            try
            {
                Console.WriteLine("Refresh Database Scripts ...");
                Console.WriteLine();
                RefereshRuntimeDbScripts();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetErrorInfo());
                Console.WriteLine();
                Console.WriteLine("Refresh Database Scripts completed with errors.");
            }
        }

        private static void RefereshRuntimeDbScripts()
        {
            foreach (var fileItem in Directory.EnumerateFiles(_TadbirUtilityDbScriptPath))
            {
                String fileName = Path.GetFileName(fileItem);

                if (File.Exists(Path.Combine(_mainDbScriptPath, fileName)))
                {
                    File.Copy(Path.Combine(_mainDbScriptPath, fileName), Path.Combine(_TadbirUtilityDbScriptPath, fileName), true);
                    ReplaceDefByParams(Path.Combine(_TadbirUtilityDbScriptPath, fileName));
                }
            }
        }

        private static void ReplaceDefByParams(string filePath)
        {
            var dbParams = SysParameterUtility.AllParameters.Db;
            var dockerDB = SysParameterUtility.AllParameters.Docker.Db;
            String temp = File.ReadAllText(filePath);

            temp = Regex.Replace(temp, RegExpression(dbParams.LoginName), "@LoginName");
            temp = Regex.Replace(temp, RegExpression(dbParams.Password), "@Password");
            temp = Regex.Replace(temp, RegExpression(dbParams.SysDbName), "@SysDbName");
            temp = Regex.Replace(temp, RegExpression(dbParams.AdminUserName), "@AdminUserName");
            temp = Regex.Replace(temp, RegExpression(dbParams.AdminPasswordHash), "@AdminPasswordHash");
            temp = Regex.Replace(temp, RegExpression(dbParams.AdminFirstName), "@AdminFirstName");
            temp = Regex.Replace(temp, RegExpression(dbParams.AdminLastName), "@AdminLastName");
            temp = Regex.Replace(temp, RegExpression(dbParams.FirstCompanyName), "@FirstCompanyName");
            temp = Regex.Replace(temp, RegExpression(dbParams.FirstDbName), "@FirstDbName");
            temp = Regex.Replace(temp, RegExpression(dockerDB.Name), "@DbServerName");

            File.WriteAllText(filePath, temp);
        }

        private static string RegExpression(string input)
        {
            //for pick up text from spaces,brackets[] or single qutation mark ' or semi colon ;
            return string.Format(@"(?<=\[){0}(?=\])|(?<='){0}(?=')|(?<=\s){0}(?=\s)|(?<=:){0}|{0}(?=;)", input);
        }

        private const string _mainDbScriptPath = @"..\..\..\res";
        private const string _TadbirUtilityDbScriptPath = @"..\..\..\src\TadbirNG\SPPC.Tadbir.Utility\Scripts";
    }
}
