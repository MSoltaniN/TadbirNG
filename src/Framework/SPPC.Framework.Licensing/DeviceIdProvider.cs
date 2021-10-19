using System;
using System.Collections.Generic;
using System.Text;
using Chilkat;
using DeviceId;
using DeviceId.Encoders;
using DeviceId.Formatters;
using SPPC.Framework.Helpers;

namespace SPPC.Framework.Licensing
{
    /// <summary>
    /// عملیات مورد نیاز برای خواندن شناسه سخت افزاری یک وسیله را پیاده سازی می کند
    /// </summary>
    public class DeviceIdProvider : IDeviceIdProvider
    {
        /// <summary>
        /// شناسه سخت افزاری وسیله جاری را خوانده و برمی گرداند
        /// </summary>
        /// <returns>شناسه سخت افزاری وسیله جاری</returns>
        public string GetDeviceId()
        {
            var hwId = new DeviceIdBuilder()
                    .AddProcessorId()
                    .AddMotherboardSerialNumber()
                    .AddSystemUUID()
                    .AddSystemDriveSerialNumber()
                    .UseFormatter(new StringDeviceIdFormatter(new PlainTextDeviceIdComponentEncoder()))
                    .ToString();
            return Encode(hwId);
        }

        /// <summary>
        /// شناسه سخت افزاری یک وسیله را از راه دور خوانده و برمی گرداند
        /// </summary>
        /// <returns>شناسه سخت افزاری وسیله مورد نظر</returns>
        /// <remarks>برای اتصال از راه دور از پروتکل اس اس اچ استفاده می شود</remarks>
        public string GetRemoteDeviceId(RemoteConnection connection)
        {
            var ssh = new Ssh();
            var commands = Environment.OSVersion.Platform == PlatformID.Win32NT
                ? SshCommands.Windows
                : SshCommands.Linux;
            string key;
            if (ssh.Connect(connection.Domain, connection.Port)
                && ssh.AuthenticatePw(connection.User, connection.Password))
            {
                var items = new List<string>();
                foreach (var command in commands.AllCommands)
                {
                    string id = GetCommandResult(ssh, command);
                    if (!String.IsNullOrEmpty(id))
                    {
                        items.Add(id);
                    }
                }

                string hwKey = String.Join(".", items);
                key = Encode(hwKey);
            }
            else
            {
                key = String.Empty;
            }

            return key;
        }

        private static string Encode(string value)
        {
            var bytes = Encoding.UTF8.GetBytes(value);
            return Convert.ToBase64String(bytes);
        }

        private static string GetCommandResult(Ssh ssh, SshCommand command)
        {
            string result = ssh.QuickCommand(command.Command, "ansi");
            if (!ssh.LastMethodSuccess)
            {
                Console.WriteLine(ssh.LastErrorText);
            }

            if (!String.IsNullOrEmpty(result))
            {
                result = result
                    .Replace(@"\r", String.Empty)
                    .Replace(@"\n", String.Empty)
                    .Replace(command.ResultKey, String.Empty)
                    .Trim();
            }

            return result;
        }
    }
}
