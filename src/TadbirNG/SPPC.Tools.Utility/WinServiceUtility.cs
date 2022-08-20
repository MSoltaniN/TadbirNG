using System;
using System.Linq;
using System.Text;
using SPPC.Framework.Helpers;

namespace SPPC.Tools.Utility
{
    public class WinServiceUtility
    {
        public static bool Install(
            string name, string displayName, string binPath, string typeMode = "own",
            string startMode = "auto", string errorMode = "normal")
        {
            bool installed = true;
            var runner = new CliRunner();
            var output = runner.Run($"sc query {name}");
            var lines = output.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            if (!lines[0].Contains($"SERVICE_NAME: {name}"))
            {
                output = runner.Run(
                    GetCreateCommand(name, displayName, binPath, typeMode, startMode, errorMode));
                lines = output.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
                installed = !lines[0].Contains("FAILED");
            }

            return installed;
        }

        public static bool Uninstall(string name)
        {
            var runner = new CliRunner();
            var output = runner.Run($"sc delete {name}");
            var lines = output.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            return !lines[0].Contains("FAILED");
        }

        public static bool Start(string name)
        {
            bool started = false;
            var runner = new CliRunner();
            var output = runner.Run($"sc start {name}");
            var lines = output.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            if (!lines[0].Contains("FAILED"))
            {
                do
                {
                    output = runner.Run($"sc query {name}");
                    started = output
                        .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                        .Where(line => line.Contains("STATE") && line.Contains("RUNNING"))
                        .Any();
                } while (!started);
            }

            return started;
        }

        public static bool Stop(string name)
        {
            bool stopped = false;
            var runner = new CliRunner();
            var output = runner.Run($"sc stop {name}");
            var lines = output.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            if (!lines[0].Contains("FAILED"))
            {
                do
                {
                    output = runner.Run($"sc query {name}");
                    stopped = output
                        .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                        .Where(line => line.Contains("STATE") && line.Contains("STOPPED"))
                        .Any();
                } while (!stopped);
            }

            return stopped;
        }

        private static string GetCreateCommand(
            string name, string displayName, string binPath, string typeMode, string startMode, string errorMode)
        {
            var cmdBuilder = new StringBuilder($"sc create {name}");
            cmdBuilder.Append($" type= {typeMode} start= {startMode} error= {errorMode}");
            cmdBuilder.Append($" displayname= \"{displayName}\"");
            cmdBuilder.Append($" binpath= \"{binPath}\"");
            return cmdBuilder.ToString();
        }
    }
}
