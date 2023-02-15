using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SPPC.Tools.Model;

namespace SPPC.Tools.Utility
{
    public class ScriptUtility
    {
        public const string CreateScriptName = "Tadbir_CreateDbObjects.sql";

        public const string UpdateScriptName = "Tadbir_UpdateDbObjects.sql";

        public const string SysCreateScriptName = "TadbirSys_CreateDbObjects.sql";

        public const string SysUpdateScriptName = "TadbirSys_UpdateDbObjects.sql";

        public static void AddVersionMarker(StringBuilder builder)
        {
            var marker = $"-- {VersionUtility.GetApiVersion()}";
            var path = Path.Combine(PathConfig.ResourceRoot, SysUpdateScriptName);
            var allScript = File.ReadAllText(path, Encoding.UTF8);
            builder.AppendLine();
            if (!allScript.Contains(marker))
            {
                builder.AppendLine(marker);
            }
        }

        public static void ReplaceScript(string newScript)
        {
            OverwriteScriptBlock(CreateScriptName, newScript);
        }

        public static void ReplaceSysScript(string newScript)
        {
            OverwriteScriptBlock(SysCreateScriptName, newScript);
        }

        public static string GetNullableValue(string nullable, bool isUnicode = true)
        {
            return String.IsNullOrEmpty(nullable)
                ? "NULL"
                : (isUnicode
                    ? $"N'{nullable}'"
                    : $"'{nullable}'");
        }

        public static string GetNullableValue(int? nullable)
        {
            return nullable.HasValue
                ? $"{nullable.Value}"
                : "NULL";
        }

        public static int GetDbBoolean(bool value)
        {
            return value ? 1 : 0;
        }

        private static void OverwriteScriptBlock(string scriptFile, string newScript)
        {
            var lines = newScript.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            var path = Path.Combine(PathConfig.ResourceRoot, scriptFile);
            var allScript = File.ReadAllText(path, Encoding.UTF8);
            var prologue = allScript[0..allScript.IndexOf(lines.First())];
            var lastLine = lines.Last();
            int epilogueStart = allScript.IndexOf(lastLine) + lastLine.Length + Environment.NewLine.Length;
            var epilogue = allScript[epilogueStart..];

            allScript = $"{prologue}{newScript}{epilogue}";
            File.WriteAllText(path, allScript, Encoding.UTF8);
        }

        public static string GetInsertScripts<TModel>(
            IEnumerable<TModel> scriptables, ToScriptDelegate<TModel> toScript)
            where TModel : class, new()
        {
            if (!scriptables.Any())
            {
                return String.Empty;
            }

            var scriptBuilder = new StringBuilder();
            if (scriptables.Count() == 1)
            {
                scriptBuilder.AppendLine(toScript(scriptables.First()));
            }
            else
            {
                scriptBuilder.Append(toScript(scriptables.First(), true, false));
                foreach (var setting in scriptables
                    .Skip(1)
                    .Take(scriptables.Count() - 2))
                {
                    scriptBuilder.Append(toScript(setting, false, false));
                }

                scriptBuilder.Append(toScript(scriptables.Last(), false, true));
            }

            return scriptBuilder.ToString();
        }

        public delegate string ToScriptDelegate<TModel>(
            TModel model, bool withIdentityOn = true, bool withIdentityOff = true);
    }
}
