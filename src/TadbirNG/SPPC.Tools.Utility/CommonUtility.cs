using System;
using System.Diagnostics;
using System.Linq;

namespace SPPC.Tools.Utility
{
    public class CommonUtility
    {
        public static bool IsDockerEngineRunning()
        {
            // This is a simplified approach for detecting docker engine status. A better approach may be preferred.
            // NOTE: vmmem may only be available under WSL2. In that case, only check for "Docker Desktop.exe"
            var requiredProcesses = new string[] { "Docker Desktop.exe", "vmmem" };
            return Process
                .GetProcesses()
                .Where(proc => requiredProcesses.Contains(proc.ProcessName))
                .Any();
        }
    }
}
