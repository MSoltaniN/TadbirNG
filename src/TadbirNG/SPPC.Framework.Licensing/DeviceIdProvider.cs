using System;
using System.Collections.Generic;
using System.Text;
using Renci.SshNet;
using SPPC.Framework.Helpers;

namespace SPPC.Framework.Licensing
{
    /// <summary>
    /// عملیات مورد نیاز برای خواندن شناسه سخت افزاری یک وسیله را پیاده سازی می کند
    /// </summary>
    public class DeviceIdProvider : IDeviceIdProvider
    {
        /// <summary>
        /// شناسه سخت افزاری یک وسیله را از راه دور خوانده و برمی گرداند
        /// </summary>
        /// <returns>شناسه سخت افزاری وسیله مورد نظر</returns>
        /// <remarks>برای اتصال از راه دور از پروتکل اس اس اچ استفاده می شود</remarks>
        public string GetRemoteDeviceId(RemoteConnection connection)
        {
            var commands = Environment.OSVersion.Platform == PlatformID.Win32NT
                ? SshCommands.Windows
                : SshCommands.Linux;
            string key = String.Empty;
            var sshConnection = new ConnectionInfo(
                connection.Domain, connection.Port, connection.User, new PasswordAuthenticationMethod(
                    connection.User, connection.Password));
            using (var sshClient = new SshClient(sshConnection))
            {
                var items = new List<string>();
                sshClient.Connect();
                foreach (var command in commands.AllCommands)
                {
                    string id = GetCommandResult(sshClient, command);
                    if (!String.IsNullOrEmpty(id))
                    {
                        items.Add(id);
                    }
                }

                string hwKey = String.Join(".", items);
                key = Encode(hwKey);
            }

            return key;
        }

        private static string Encode(string value)
        {
            var bytes = Encoding.UTF8.GetBytes(value);
            return Convert.ToBase64String(bytes);
        }

        private static string GetCommandResult(SshClient ssh, SshCommand command)
        {
            string result = ssh
                .CreateCommand(command.Command)
                .Execute();
            if (!String.IsNullOrEmpty(result))
            {
                result = result
                    .Replace("\r", String.Empty)
                    .Replace("\n", String.Empty)
                    .Replace(command.ResultKey, String.Empty)
                    .Trim();
            }

            return result;
        }
    }
}
