using System;
using FluentFTP;
using SPPC.Framework.Helpers;

namespace SPPC.Tools.Maintenance
{
    public class FtpProgressEventArgs : EventArgs
    {
        public FtpProgressEventArgs(FtpProgress progress)
        {
            Message = GetMessage(progress);
            IsComplete = progress.Progress == 100
                || progress.ETA == TimeSpan.Zero
                || progress.TransferSpeed == 0.0;
        }

        public string Message { get; }

        public bool IsComplete { get; }

        private static string GetMessage(FtpProgress progress)
        {
            var speed = (long)Math.Round(progress.TransferSpeed) < FileSize.FromMegaBytes(1)
                ? FileSize.ToKiloBytes((long)progress.TransferSpeed)
                : FileSize.ToMegaBytes((long)progress.TransferSpeed);
            if (speed > 0.0)
            {
                var total = progress.TransferredBytes < FileSize.FromMegaBytes(1)
                    ? $"{FileSize.ToKiloBytes(progress.TransferredBytes)} KB"
                    : $"{FileSize.ToMegaBytes(progress.TransferredBytes)} MB";
                var speedInfo = (long)Math.Round(progress.TransferSpeed) < FileSize.FromMegaBytes(1)
                    ? $"{speed} KB/sec"
                    : $"{speed} MB/sec";
                return $"Transferred : {total}, Remaining time : {progress.ETA:hh\\:mm\\:ss}, "
                    + $"Speed : {speedInfo} ({Math.Round(progress.Progress, 2)}%)";
            }
            else
            {
                return String.Empty;
            }
        }
    }
}
