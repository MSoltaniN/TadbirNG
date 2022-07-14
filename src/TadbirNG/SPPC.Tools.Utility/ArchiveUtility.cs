using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPPC.Framework.Common;
using SPPC.Framework.Helpers;

namespace SPPC.Tools.Utility
{
    public class ArchiveUtility
    {
        public static void RedirectOutput(bool redirect = true)
        {
            if (redirect)
            {
                _runner.OutputReceived += Runner_OutputReceived;
            }
            else
            {
                _runner.OutputReceived -= Runner_OutputReceived;
            }
        }

        public static void GZip(string sourceFile)
        {
            Verify.ArgumentNotNullOrEmptyString(sourceFile, nameof(sourceFile));

            var currentDir = Environment.CurrentDirectory;
            Environment.CurrentDirectory = GetToolsFolder();
            _runner.Run(String.Format(Constants.GzipTemplate, sourceFile));
            Environment.CurrentDirectory = currentDir;
        }

        public static void GunZip(string sourceFile)
        {
            Verify.ArgumentNotNullOrEmptyString(sourceFile, nameof(sourceFile));

            var currentDir = Environment.CurrentDirectory;
            Environment.CurrentDirectory = GetToolsFolder();
            _runner.Run(String.Format(Constants.GunzipTemplate, sourceFile));
            Environment.CurrentDirectory = currentDir;
        }

        public static void Tar(string tarFile, string sourceFile)
        {
            Verify.ArgumentNotNullOrEmptyString(tarFile, nameof(sourceFile));
            Verify.ArgumentNotNullOrEmptyString(sourceFile, nameof(sourceFile));

            var currentDir = Environment.CurrentDirectory;
            Environment.CurrentDirectory = GetToolsFolder();
            _runner.Run(String.Format(Constants.TarTemplate, tarFile, sourceFile));
            Environment.CurrentDirectory = currentDir;
        }

        public static void UnTar(string tarFile)
        {
            var currentDir = Environment.CurrentDirectory;
            Environment.CurrentDirectory = GetToolsFolder();
            _runner.Run(String.Format(Constants.UntarTemplate, tarFile));
            Environment.CurrentDirectory = currentDir;
        }

        private static string GetToolsFolder()
        {
            var root = Path.GetDirectoryName(
                Path.GetDirectoryName(
                    Path.GetDirectoryName(Environment.CurrentDirectory)));
            return Path.Combine(root, "misc", "tools");
        }

        private static void Runner_OutputReceived(object sender, OutputReceivedEventArgs e)
        {
            if (!String.IsNullOrEmpty(e.Output))
            {
                Console.WriteLine(e.Output.Replace("\n", Environment.NewLine));
            }
        }

        private static readonly CliRunner _runner = new();
    }
}
