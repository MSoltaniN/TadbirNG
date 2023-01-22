using System;
using System.IO;
using System.Linq;
using System.Text;
using SPPC.Tools.Model;

namespace SPPC.Tools.Utility
{
    public class ScriptUtility
    {
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

        public static void ReplaceSysScript(string newScript)
        {
            var lines = newScript.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            var path = Path.Combine(PathConfig.ResourceRoot, SysCreateScriptName);
            var allScript = File.ReadAllText(path, Encoding.UTF8);
            var prologue = allScript[0..allScript.IndexOf(lines.First())];
            var lastLine = lines.Last();
            int epilogueStart = allScript.IndexOf(lastLine) + lastLine.Length + Environment.NewLine.Length;
            var epilogue = allScript[epilogueStart..];

            allScript = $"{prologue}{newScript}{epilogue}";
            File.WriteAllText(path, allScript, Encoding.UTF8);
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
    }
}
