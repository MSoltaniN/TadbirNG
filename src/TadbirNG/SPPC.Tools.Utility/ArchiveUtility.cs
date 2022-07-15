using System;
using System.IO;
using SPPC.Framework.Common;
using SPPC.Framework.Helpers;

namespace SPPC.Tools.Utility
{
    public class ArchiveUtility
    {
        public ArchiveUtility(string toolsPath, bool redirectOutput = true)
        {
            // NOTE: The following line ONLY works in Windows environment, because Windows versions of
            // gzip and tar are added to misc/tools. Apparently, Linux build server already has gzip in Path.
            //SetToolsPath(toolsPath);
            RedirectOutput(redirectOutput);
        }

        public void GZip(string sourceFile)
        {
            Verify.ArgumentNotNullOrEmptyString(sourceFile, nameof(sourceFile));
            _runner.Run(String.Format(Constants.GzipTemplate, sourceFile));
        }

        public void GunZip(string sourceFile)
        {
            Verify.ArgumentNotNullOrEmptyString(sourceFile, nameof(sourceFile));
            _runner.Run(String.Format(Constants.GunzipTemplate, sourceFile));
        }

        public void Tar(string tarFile, string sourceFile)
        {
            Verify.ArgumentNotNullOrEmptyString(tarFile, nameof(tarFile));
            Verify.ArgumentNotNullOrEmptyString(sourceFile, nameof(sourceFile));
            _runner.Run(String.Format(Constants.TarTemplate, tarFile, sourceFile));
        }

        public void UnTar(string tarFile)
        {
            Verify.ArgumentNotNullOrEmptyString(tarFile, nameof(tarFile));
            _runner.Run(String.Format(Constants.UntarTemplate, tarFile));
        }

        private static void SetToolsPath(string toolsPath)
        {
            Verify.ArgumentNotNullOrEmptyString(toolsPath, nameof(toolsPath));
            var currentPath = Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]);
            var parts = toolsPath.Split('\\');
            foreach (var part in parts)
            {
                if (part == "..")
                {
                    currentPath = Path.GetDirectoryName(currentPath);
                }
                else if (part != ".")
                {
                    currentPath = Path.Combine(currentPath, part);
                }
            }

            Environment.SetEnvironmentVariable("Path", currentPath, EnvironmentVariableTarget.Process);
        }

        private static void Runner_OutputReceived(object sender, OutputReceivedEventArgs e)
        {
            if (!String.IsNullOrEmpty(e.Output))
            {
                Console.WriteLine(e.Output.Replace("\n", Environment.NewLine));
            }
        }

        private void RedirectOutput(bool redirect = true)
        {
            if (redirect)
            {
                _runner.OutputReceived += Runner_OutputReceived;
            }
        }

        private readonly CliRunner _runner = new();
    }
}
