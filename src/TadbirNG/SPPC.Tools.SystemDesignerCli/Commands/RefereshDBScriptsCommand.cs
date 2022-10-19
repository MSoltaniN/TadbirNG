using System;
using System.IO;
using System.Text.RegularExpressions;
using SPPC.Framework.Extensions;
using SPPC.Tadbir.Configuration;
using SPPC.Tools.Model;

namespace SPPC.Tools.SystemDesignerCli
{
    public class RefreshDBScriptsCommand : ICliCommand
    {
        static RefreshDBScriptsCommand()
        {
            DbScriptPath = Path.Combine(PathConfig.SolutionRoot, "SPPC.Tadbir.Utility", "Scripts");
        }

        public void Execute()
        {
            try
            {
                Console.WriteLine("Refreshing database scripts ...");
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
            foreach (var fileItem in Directory.EnumerateFiles(DbScriptPath))
            {
                string fileName = Path.GetFileName(fileItem);
                if (File.Exists(Path.Combine(PathConfig.ResourceRoot, fileName)))
                {
                    File.Copy(
                        Path.Combine(PathConfig.ResourceRoot, fileName),
                        Path.Combine(DbScriptPath, fileName), true);
                    ReplaceValuesByParameters(Path.Combine(DbScriptPath, fileName));
                }
            }
        }

        private static void ReplaceValuesByParameters(string filePath)
        {
            var dbParams = SysParameterUtility.AllParameters.Db;
            var dockerDB = SysParameterUtility.AllParameters.Docker.Db;
            string script = File.ReadAllText(filePath);

            script = Regex.Replace(script, GetRegex(dbParams.LoginName), "@LoginName");
            script = Regex.Replace(script, GetRegex(dbParams.Password), "@Password");
            script = Regex.Replace(script, GetRegex(dbParams.SysDbName), "@SysDbName");
            script = Regex.Replace(script, GetRegex(dbParams.AdminUserName), "@AdminUserName");
            script = Regex.Replace(script, GetRegex(dbParams.AdminPasswordHash), "@AdminPasswordHash");
            script = Regex.Replace(script, GetRegex(dbParams.AdminFirstName), "@AdminFirstName");
            script = Regex.Replace(script, GetRegex(dbParams.AdminLastName), "@AdminLastName");
            script = Regex.Replace(script, GetRegex(dbParams.FirstCompanyName), "@FirstCompanyName");
            script = Regex.Replace(script, GetRegex(dbParams.FirstDbName), "@FirstDbName");
            script = Regex.Replace(script, GetRegex(dockerDB.Name), "@DbServerName");

            File.WriteAllText(filePath, script);
        }

        private static string GetRegex(string input)
        {
            // Pick up text between spaces, brackets ([]) or quotation marks ('') or before colon (:) and semi-colon (;)
            return String.Format(@"(?<=\[){0}(?=\])|(?<='){0}(?=')|(?<=\s){0}(?=\s)|(?<=:){0}|{0}(?=;)", input);
        }

        private static readonly string DbScriptPath;
    }
}
