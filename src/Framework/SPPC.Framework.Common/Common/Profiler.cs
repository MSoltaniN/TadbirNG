using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace SPPC.Framework.Common
{
    /// <summary>
    /// Utility class that provides basic performance profiling services
    /// </summary>
    public static class Profiler
    {
        /// <summary>
        /// Creates a new instance of this class and initializes a new profiling session
        /// </summary>
        /// <param name="useCase">Short description of the use case in a single profiling session</param>
        static Profiler()
        {
            _stopwatch = new Stopwatch();
            _report = new StringBuilder();
            _totalElapsed = TimeSpan.Zero;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="title"></param>
        public static void NewSession(string title)
        {
            _report.AppendFormat("[{0}] New profiling session started.", DateTime.Now);
            _report.AppendLine();
            _report.AppendFormat("Use case : {0}", title);
            _report.AppendLine();
            _report.AppendLine();
        }

        /// <summary>
        /// Starts timing for an operation
        /// </summary>
        public static void Start()
        {
            if (_stopwatch.Elapsed == TimeSpan.Zero)
            {
                _stopwatch.Start();
            }
            else
            {
                _stopwatch.Restart();
            }
        }

        /// <summary>
        /// Ends last timing operation and logs the information about operation result
        /// </summary>
        /// <param name="info">Short description of last operation result</param>
        public static void Report(string info)
        {
            _stopwatch.Stop();
            _totalElapsed += _stopwatch.Elapsed;
            string message = String.Format("[{0}] {1} Elapsed : {2}", _id++, info, _stopwatch.Elapsed);
            _report.AppendLine(message);
            _report.AppendLine();
        }

        /// <summary>
        /// Ends this profiling session and logs session profiling results to a log file
        /// </summary>
        public static void End()
        {
            _report.AppendFormat("Total elapsed : {0}", _totalElapsed);
            _report.AppendLine();
            _report.AppendFormat("[{0}] Profiling session finished.", DateTime.Now);
            _report.AppendLine();
            _report.AppendLine("-----------------------------------------------------------------------------");
            File.AppendAllText(_logFile, _report.ToString());
        }

        private const string _logFile = "profile.log";
        private static readonly Stopwatch _stopwatch;
        private static readonly StringBuilder _report;
        private static TimeSpan _totalElapsed;
        private static int _id = 1;
    }
}
