using System;
using System.Diagnostics;
using System.IO;
using SPPC.Framework.Extensions;

namespace SPPC.Tools.Maintenance
{
    class Program
    {
        static void Main(string[] args)
        {
            DisplayBanner();
            if (args.Length == 0 || args[0] == "backup")
            {
                RunBackup();
            }
        }

        static void DisplayBanner()
        {
            Console.WriteLine();
            Console.WriteLine("============================================================");
            Console.WriteLine("SPPC Server Maintenance Utility (v1.0)");
            Console.WriteLine($"(c) Copyright {DateTime.Now.Year}, SPPC, All Rights Reserved");
            Console.WriteLine("============================================================");
            Console.WriteLine();
        }

        static void RunBackup()
        {
            try
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();

                var backup = new BackupUtility();
                backup.TaskStarted += Backup_TaskStarted;
                backup.TaskFinished += Backup_TaskFinished;
                backup.FtpProgress += Backup_FtpProgress;
                backup.BackupNgTadbirDatabases();
                backup.BackupNgTadbirSites();
                var tempPath = Path.Combine(".", "temp");
                if (Directory.Exists(tempPath))
                {
                    Directory.Delete(tempPath);
                }

                stopwatch.Stop();
                Console.WriteLine("Backup completed.");
                Console.WriteLine($"Elapsed : {stopwatch.Elapsed}");
            }
            catch (Exception ex)
            {
                var message = $"Error occured.{Environment.NewLine}{ex.GetErrorInfo()}{Environment.NewLine}{ex.StackTrace}";
                File.WriteAllText("error.log", message);
            }
        }

        private static void Backup_TaskStarted(object sender, TaskStartedEventArgs e)
        {
            var current = SetConsoleColor(ConsoleColor.DarkGreen);
            Console.WriteLine($"{e.TaskType} {e.TargetType} '{e.Name}'");
            ResetConsoleColor(current);
        }

        private static void Backup_TaskFinished(object sender, TaskFinishedEventArgs e)
        {
            if (e.Succeeded)
            {
                var current = SetConsoleColor(ConsoleColor.DarkGreen);
                Console.WriteLine($"(Succeeded) {e.TaskType} {e.TargetType} '{e.Name}'");
                ResetConsoleColor(current);
                Console.WriteLine();
            }
            else
            {
                var current = SetConsoleColor(ConsoleColor.DarkRed);
                Console.WriteLine($"(Failed) {e.TaskType} {e.TargetType} '{e.Name}'");
                Console.WriteLine(e.Exception.GetErrorInfo());
                Console.WriteLine();
                ResetConsoleColor(current);
            }
        }

        private static void Backup_FtpProgress(object sender, FtpProgressEventArgs e)
        {
            var current = SetConsoleColor(ConsoleColor.Blue);
            Console.Write($"\r{e.Message}    \r");
            ResetConsoleColor(current);
            if (e.IsComplete)
            {
                Console.WriteLine();
            }
        }

        private static ConsoleColor SetConsoleColor(ConsoleColor color)
        {
            var currentColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            return currentColor;
        }

        private static void ResetConsoleColor(ConsoleColor color)
        {
            Console.ForegroundColor = color;
        }
    }
}
