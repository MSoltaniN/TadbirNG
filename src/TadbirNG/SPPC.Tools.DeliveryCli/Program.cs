using System;
using SPPC.Framework.Helpers;
using SPPC.Tools.Utility;

namespace SPPC.Tools.DeliveryCli
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!CommonUtility.IsDockerEngineRunning())
            {
                Console.WriteLine(
                    "Please make sure Docker Desktop is running and you are logged into Docker Hub repository.");
                return;
            }

            InputUtility.DisplayBanner();
            _runner.OutputReceived += Runner_OutputReceived;
        }

        private static void Runner_OutputReceived(object sender, OutputReceivedEventArgs e)
        {
            Console.WriteLine(e.Output);
            Console.WriteLine();
        }

        static readonly CliRunner _runner = new();
    }
}
