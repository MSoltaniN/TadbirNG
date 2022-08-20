using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using SPPC.Framework.Helpers;
using SPPC.Tadbir.Utility.Docker;
using SPPC.Tools.Utility;

namespace SPPC.Tools.BuildServer
{
    class Program
    {
        static void Main(string[] args)
        {
            RunBuildServer();
        }

        private static void RunBuildServer()
        {
            InitBuildEnvironment();
            DisplayBanner();
            if (!DockerUtility.IsDockerEngineRunning())
            {
                Console.WriteLine("ERROR: Build process needs Docker engine to be up and running.");
                return;
            }

            while (true)
            {
                BuildAndPublish();
                Console.WriteLine();
                Console.WriteLine("Waiting for the next hourly build...");
                Thread.Sleep((int)TimeSpan.FromHours(1).TotalMilliseconds);
            }
        }

        private static void InitBuildEnvironment()
        {
            FlushLimit = (int)FileSize.FromKiloBytes(10.0);
            MaxLogSize = (int)FileSize.FromMegaBytes(4.0);
            _runner.OutputReceived += Runner_OutputReceived;
            var exePath = Environment.GetCommandLineArgs()[0];
            _logPath = Path.Combine(Path.GetDirectoryName(exePath), "build.log");
            _utility = new BuildUtility()
            {
                OutputRedirector = Runner_OutputReceived,
                OutputProvider = ShowMessage
            };
        }

        public static void DisplayBanner()
        {
            Console.WriteLine();
            Console.WriteLine("============================================================");
            Console.WriteLine("TadbirNG Build Server (v1.2)");
            Console.WriteLine($"(c) Copyright {DateTime.Now.Year}, SPPC, All Rights Reserved");
            Console.WriteLine("============================================================");
            Console.WriteLine();
        }

        private static void BuildAndPublish()
        {
            try
            {
                _stopwatch.Start();
                _utility.RunBuildProcess();
                _utility.RunPublishProcess();
                _stopwatch.Stop();
                var elapsed = String.Format($"Elapsed time : {_stopwatch.Elapsed}");
                ShowMessage();
                ShowMessage(elapsed);
                _stopwatch.Reset();
            }
            finally
            {
                File.AppendAllText(_logPath, _logBuilder.ToString());
            }
        }

        private static void Runner_OutputReceived(object sender, OutputReceivedEventArgs e)
        {
            if (!String.IsNullOrEmpty(e.Output))
            {
                ShowMessage(e.Output.Replace("\n", Environment.NewLine));
                FlushToLogFile();
            }
        }

        private static void ShowMessage(string message = null)
        {
            var msg = message ?? Environment.NewLine;
            if (!String.IsNullOrEmpty(message))
            {
                var timestamp = String.Format($"[{DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}] ");
                Console.Write(timestamp);
                _logBuilder.Append(timestamp);
            }

            Console.WriteLine(msg);
            _logBuilder.AppendLine(msg);
        }

        private static void FlushToLogFile()
        {
            if (_logBuilder.Length >= FlushLimit)
            {
                var chunk = _logBuilder.ToString().Substring(0, FlushLimit);
                File.AppendAllText(_logPath, chunk);
                _logBuilder.Remove(0, FlushLimit);
            }

            // NOTE: The following logic keeps log file size between 2 and 4 MB, effectively discarding older content
            // Always truncating log file to 4 MB will result in continuous truncation, which would be inefficient.
            var logInfo = new FileInfo(_logPath);
            if (logInfo.Length > MaxLogSize)
            {
                var log = File.ReadAllText(_logPath);
                var truncated = log[((int)logInfo.Length - (MaxLogSize / 2))..];
                File.WriteAllText(_logPath, truncated);
            }
        }

        private static int FlushLimit;
        private static int MaxLogSize;
        private static readonly CliRunner _runner = new();
        private static readonly StringBuilder _logBuilder = new();
        private static readonly Stopwatch _stopwatch = new();
        private static BuildUtility _utility;
        private static string _logPath;
    }
}
