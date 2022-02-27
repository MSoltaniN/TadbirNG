using System;
using System.Collections.Generic;
using System.Text;
using SPPC.Framework.Helpers;

namespace SPPC.Framework.Licensing
{
    /// <summary>
    /// عملیات مورد نیاز برای خواندن شناسه سخت افزاری یک وسیله را پیاده سازی می کند
    /// </summary>
    public class HardwareKey
    {
        /// <summary>
        /// شناسه سخت افزاری یک وسیله را خوانده و برمی گرداند
        /// </summary>
        /// <returns>شناسه سخت افزاری وسیله مورد نظر</returns>
        /// <remarks>شناسه سخت افزاری با توجه به سیستم عامل در حال اجرا به دست می آید</remarks>
        public static string Query()
        {
            var items = new List<string>();
            var runner = new CliRunner();
            var commands = GetTargetCommands(runner);
            foreach (var command in commands.AllCommands)
            {
                string id = runner.Run(command.Command);
                if (!String.IsNullOrEmpty(id))
                {
                    items.Add(CleanupRunnerResult(id, command.ResultKey));
                }
            }

            string hwKey = String.Join(".", items);
            return Encode(hwKey);
        }

        private static IPlatformCommands GetTargetCommands(CliRunner runner)
        {
            string result = runner.Run(SshCommands.Linux.GetOSName.Command);
            return result.ToLower().Contains("linux")
                ? SshCommands.Linux
                : SshCommands.Windows;
        }

        private static string CleanupRunnerResult(string result, string key)
        {
            return result
                .Replace("\r", String.Empty)
                .Replace("\n", String.Empty)
                .Replace(key, String.Empty)
                .Trim();
        }

        private static string Encode(string value)
        {
            var bytes = Encoding.UTF8.GetBytes(value);
            return Convert.ToBase64String(bytes);
        }
    }
}
