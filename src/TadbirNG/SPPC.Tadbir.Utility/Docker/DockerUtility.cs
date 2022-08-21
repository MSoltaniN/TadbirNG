using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using SPPC.Framework.Cryptography;
using SPPC.Tadbir.Utility.Model;

namespace SPPC.Tadbir.Utility.Docker
{
    public class DockerUtility
    {
        public static bool IsDockerEngineRunning()
        {
            // This is a simplified approach for detecting docker engine status. A better approach may be preferred.
            // NOTE: vmmem may only be available under WSL2. In that case, only check for "Docker Desktop.exe"
            var requiredProcesses = Environment.OSVersion.Platform == PlatformID.Win32NT
                ? new string[] { "Docker Desktop.exe", "vmmem" }
                : new string[] { "dockerd", "docker-compose" };
            return Process
                .GetProcesses()
                .Where(proc => requiredProcesses.Contains(proc.ProcessName))
                .Any();
        }

        public static ServiceInfo GetServiceInfo(string imagePath)
        {
            var serviceName = Path
                .GetFileName(imagePath)
                .Replace(".tar.gz", String.Empty)
                .Replace("-std", String.Empty)
                .Replace("-pro", String.Empty)
                .Replace("-ent", String.Empty);
            var imageData = File.ReadAllBytes(imagePath);
            var crypto = CryptoService.Default;
            return new ServiceInfo()
            {
                Name = serviceName,
                Sha256 = crypto
                    .CreateHash(imageData)
                    .ToLower(),
                Size = imageData.Length
            };
        }

        public static string GetEditionTag(string edition)
        {
            return edition == Edition.Standard
                ? Edition.StandardTag
                : edition?
                    .Substring(0, 3)
                    .ToLower();
        }

        public static string GetEdition(string editionTag)
        {
            return editionTag == Edition.StandardTag
                ? Edition.Standard
                : (editionTag == Edition.ProfessionalTag
                    ? Edition.Professional
                    : Edition.Enterprise);
        }
    }
}
