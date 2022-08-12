using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Win32;

namespace SPPC.Tadbir.Setup
{
#pragma warning disable CA1416 // Validate platform compatibility
    public class WinUtility
    {
        public static List<string> GetDbServers()
        {
            var servers = new List<string>();
            var key = Registry.LocalMachine.OpenSubKey(
                @"SOFTWARE\Microsoft\Microsoft SQL Server\Instance Names\SQL");
            if (key != null)
            {
                foreach (var value in key.GetValueNames())
                {
                    if (value == "MSSQLSERVER")
                    {
                        servers.Add(Environment.MachineName);
                    }
                    else
                    {
                        servers.Add(String.Format($"{Environment.MachineName}\\{value}"));
                    }
                }
            }

            return servers;
        }

        public static string GetDockerExePath()
        {
            string exePath = String.Empty;
            var key = Registry.LocalMachine.OpenSubKey(
                @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\Docker Desktop");
            if (key != null)
            {
                var location = key.GetValue("InstallLocation")?.ToString();
                if (location != null)
                {
                    exePath = Path.Combine(location, "resources", "bin");
                }
            }

            return exePath;
        }
    }
#pragma warning restore CA1416 // Validate platform compatibility
}
