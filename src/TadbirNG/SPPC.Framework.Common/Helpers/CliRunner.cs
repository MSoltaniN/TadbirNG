using System;
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
            _outputBuilder.Clear();
            using (_process = GetCommandProcess(command))
            {
                _process.Start();
                _process.BeginOutputReadLine();
                _process.BeginErrorReadLine();
                _process.WaitForExit(timeout);
            }

            return _outputBuilder.ToString();
        }

        /// <summary>
        /// مجموعه ای از دستورات خط فرمان را به ترتیب اجرا کرده و خروجی دستورات را به طور پیوسته دریافت می کند
        /// </summary>
        /// <param name="batchCommand">رشته متنی چند خطی که در آن هر دستور با تمام آرگومان های مورد نیاز
        /// در یک خط قرار می گیرد</param>
        /// <remarks>مجموعه دستورات داده شده می توانند شامل خطوط خالی یا خطوط توضیحات باشند
        /// که این خطوط نادیده گرفته می شوند.</remarks>
        public void RunBatch(string batchCommand)
        {
            var commands = GetCommands(batchCommand);
            foreach (var command in commands)
            {
                Run(command);
                if (_lastExitCode != 0)
                {
                    break;
                }
            }
        }

        /// <summary>
        /// برنامه خط فرمان در حال اجرا را به صورت عادی (معادل فشردن کلید ترکیبی کنترل و سی) متوقف می کند
        /// </summary>
        public bool Stop()
        {
            if (_process == null)
            {
                return true;
            }

            try
            {
                if (ConsoleImports.AttachConsole((uint)_process.Id))
                {
                    ConsoleImports.SetConsoleCtrlHandler(null, true);
                    if (!ConsoleImports.GenerateConsoleCtrlEvent(ConsoleImports.CTRL_C_EVENT, 0))
                    {
                        return false;
                    }

                    _process.WaitForExit();
                    _lastExitCode = _process.ExitCode;
                }
            }
            finally
            {
                ConsoleImports.SetConsoleCtrlHandler(null, false);
                ConsoleImports.FreeConsole();
            }

            return true;
        }

        /// <summary>
        /// پروسس مربوط به دستور خط فرمان در حال اجرا را به همراه پروسس های زیرمجموعه پیش از اتمام کار متوقف می کند
        /// </summary>
        public void Kill()
        {
            try
            {
                if (!_process.HasExited)
                {
                    _process.Kill(true);
                    RaiseKilledEvent();
                }
            }
            catch
            {
                _process.WaitForExit();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<OutputReceivedEventArgs> OutputReceived;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler Killed;

        private static string[] GetCommands(string batchCommand)
        {
            return batchCommand
                .Split(Environment.NewLine)
                .Where(line => !MustIgnoreLine(line))
                .ToArray();
        }

        private static bool MustIgnoreLine(string line)
        {
            return line.StartsWith(RemarkPrefix) || line == Environment.NewLine;
        }

        private Process GetCommandProcess(string command)
        {
            Verify.ArgumentNotNullOrWhitespace(command, nameof(command));

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
            process.ErrorDataReceived += Process_DataReceived;
            process.OutputDataReceived += Process_DataReceived;
            process.Exited += Process_Exited;
            process.EnableRaisingEvents = true;
            return process;
        }

        private void Process_Exited(object sender, EventArgs e)
        {
            _lastExitCode = (int)_process?.ExitCode;
        }

        private void Process_DataReceived(object sender, DataReceivedEventArgs e)
        {
            _outputBuilder.AppendLine(e.Data);
            RaiseOutputReceivedEvent(e.Data);
        }

        private void RaiseOutputReceivedEvent(string output)
        {
            OutputReceived?.Invoke(this, new OutputReceivedEventArgs(output));
        }

        private void RaiseKilledEvent()
        {
            Killed?.Invoke(this, EventArgs.Empty);
        }

        private const string RemarkPrefix = "#";
        private readonly StringBuilder _outputBuilder = new();
        private Process _process;
        private int _lastExitCode = 0;
    }
}
