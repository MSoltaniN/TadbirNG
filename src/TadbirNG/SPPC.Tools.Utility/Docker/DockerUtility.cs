using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using SPPC.Framework.Helpers;

namespace SPPC.Tools.Utility
{
    public class DockerUtility
    {
        public DockerUtility()
        {
            _containers = GetContainers();
        }

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

        public void WaitForContainer(string name)
        {
            var container = GetContainer(name);
            if (container != null)
            {
                bool running;
                do
                {
                    string command = String.Format($"docker inspect container {container.Name}");
                    var output = _runner.Run(command);
                    output = FixOutput(output);
                    container = JsonHelper.To<DockerContainer[]>(output)[0];
                    running = container.State.Running;
                } while (!running);
            }
        }

        public void ReplaceContainerFile(string name, string sourcePath, string targetPath)
        {
            var container = GetContainer(name);
            if (container != null)
            {
                _runner.Run(String.Format(DeleteFileTemplate, container.Id, targetPath));
                _runner.Run(String.Format(CopyFileTemplate, container.Id, sourcePath, targetPath));
            }
        }

        private DockerContainer GetContainer(string name)
        {
            return _containers
                .Where(cont => cont.Name.Contains(name))
                .FirstOrDefault();
        }

        private DockerContainer[] GetContainers()
        {
            var commandParts = new List<string> { "docker", "inspect", "container" };
            commandParts.AddRange(GetContainerNames());
            var command = String.Join(" ", commandParts);
            var output = _runner.Run(command);
            output = FixOutput(output);
            return JsonHelper.To<DockerContainer[]>(output);
        }

        private IEnumerable<string> GetContainerNames()
        {
            var names = new List<string>();
            var output = _runner.Run("docker ps");
            var lines = output.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            if (lines.Length > 1)
            {
                names.AddRange(lines
                    .Skip(1)
                    .Select(line => line.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries))
                    .Select(lines => lines.Last()));
            }

            return names;
        }

        private static string FixOutput(string output)
        {
            return String.Join(Environment.NewLine, output
                .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                .Where(line => !line.Contains("Error:")));
        }

        private const string DeleteFileTemplate = "docker exec -it {0} rm {1}";
        private const string CopyFileTemplate = "docker cp {1} {0}:/app/{2}";
        private readonly CliRunner _runner = new();
        private readonly DockerContainer[] _containers;
    }
}
