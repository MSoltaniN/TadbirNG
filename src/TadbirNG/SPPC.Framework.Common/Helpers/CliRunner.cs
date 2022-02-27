﻿using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using SPPC.Framework.Common;

namespace SPPC.Framework.Helpers
{
    /// <summary>
    /// امکان اجرای برنامه های خط فرمان را فراهم می کند
    /// </summary>
    public class CliRunner
    {
        /// <summary>
        /// دستور خط فرمان داده شده را به صورت مستقیم اجرا می کند
        /// </summary>
        /// <param name="command">دستور خط فرمان مورد نظر که شامل نام فایل اجرایی و آرگومان های مورد نیاز است</param>
        /// <param name="timeout">زمان انتظار مورد نظر برای تکمیل اجرای دستور خط فرمان</param>
        /// <returns>نتیجه اجرای دستور خط فرمان که شامل خروجی نهایی دستور یا پیغام خطای احتمالی است</returns>
        /// <remarks>در صورتی که زمان انتظار داده نشود، این متد به صورت نامحدود در انتظار تکمیل دستور باقی می ماند</remarks>
        public string Run(string command, int timeout = -1)
        {
            Verify.ArgumentNotNullOrWhitespace(command, nameof(command));

            _outputBuilder.Clear();
            var items = command.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var fileName = items.FirstOrDefault();
            var arguments = String.Join(' ', items.Skip(1));
            var psi = new ProcessStartInfo()
            {
                FileName = fileName,
                Arguments = arguments,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };

            var process = new Process() { StartInfo = psi };
            process.ErrorDataReceived += Process_ErrorDataReceived;
            process.OutputDataReceived += Process_ErrorDataReceived;
            process.EnableRaisingEvents = true;

            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit(timeout);

            return _outputBuilder.ToString();
        }

        private void Process_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            _outputBuilder.AppendLine(e.Data);
        }

        private readonly StringBuilder _outputBuilder = new();
    }
}