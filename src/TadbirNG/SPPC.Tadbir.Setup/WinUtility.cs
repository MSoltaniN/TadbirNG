using System;
using System.Collections.Generic;
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
    }
#pragma warning restore CA1416 // Validate platform compatibility
}
