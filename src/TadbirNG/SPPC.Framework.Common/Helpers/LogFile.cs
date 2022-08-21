using System;
using System.IO;
using System.Text;

namespace SPPC.Framework.Helpers
{
    /// <summary>
    /// رویدادهای مهم برنامه را در یک فایل لاگ ثبت می کند.
    /// </summary>
    public class LogFile
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public LogFile()
        {
            AddTimestamp = true;
            BufferSize = (int)FileSize.FromKiloBytes(DefaultBufferSize);
            MaxSize = (int)FileSize.FromMegaBytes(DefaultLogSize);
            var exePath = Environment.GetCommandLineArgs()[0];
            var exeName = Path.GetFileNameWithoutExtension(exePath);
            LogPath = Path.Combine(Path.GetDirectoryName(exePath), $"{exeName}.log");
        }

        /// <summary>
        /// مسیر فایل لاگ مورد نظر که به صورت پیش فرض در مسیر جاری برنامه
        /// و با نام فایل اجرایی برنامه و پسوند لاگ در نظر گرفته می شود
        /// </summary>
        public string LogPath { get; set; }

        /// <summary>
        /// مشخص می کند که تاریخ و زمان وقوع رویداد به ابتدای هر سطر اضافه شود یا نه.
        /// به طور پیش فرض این برچسب زمانی اضافه می شود.
        /// </summary>
        public bool AddTimestamp { get; set; }

        /// <summary>
        /// اندازه بافر مورد نظر به کیلوبایت که بعد از رسیدن تعداد کاراکترها به این تعداد بایت،
        /// نوشتن درون فایل لاگ انجام می شود. به طور پیش فرض 10 کیلوبایت در نظر گرفته می شود.
        /// </summary>
        public int BufferSize { get; set; }

        /// <summary>
        /// حداکثر اندازه فایل لاگ به مگابایت که اندازه فایل همواره بین این مقدار و نصف آن متغیر خواهد بود.
        /// به طور پیش فرض 4 مگابایت در نظر گرفته می شود.
        /// </summary>
        public int MaxSize { get; set; }

        /// <summary>
        /// یک رویداد اطلاعاتی را با پیغام داده شده در فایل لاگ ثبت می کند
        /// </summary>
        /// <param name="message">پیغام اطلاعاتی مورد نظر</param>
        public void LogInformation(string message)
        {
            WriteLog(message);
        }

        /// <summary>
        /// یک رویداد هشدار را با پیغام داده شده در فایل لاگ ثبت می کند
        /// </summary>
        /// <param name="message">پیغام هشدار مورد نظر</param>
        public void LogWarning(string message)
        {
            WriteLog(message, "WARN");
        }

        /// <summary>
        /// یک رویداد خطا را با پیغام داده شده در فایل لاگ ثبت می کند
        /// </summary>
        /// <param name="message">پیغام خطای مورد نظر</param>
        public void LogError(string message)
        {
            WriteLog(message, "ERROR");
        }

        /// <summary>
        /// یک خط خالی در فایل لاگ ایجاد می کند
        /// </summary>
        public void WriteLine()
        {
            WriteLog();
        }

        /// <summary>
        /// رویدادهای نوشته نشده در فایل لاگ را بلافاصله به انتهای فایل اضافه می کند
        /// </summary>
        public void Flush()
        {
            File.AppendAllText(LogPath, _logBuilder.ToString());
            _logBuilder.Clear();
        }

        private void WriteLog(string message = null, string type = "INFO")
        {
            var now = DateTime.Now;
            var log = message ?? Environment.NewLine;
            var timestamp = AddTimestamp
                ? $"[{now.ToShortDateString()} {now.TimeOfDay}] "
                : String.Empty;
            if (log != Environment.NewLine)
            {
                log = $"{timestamp}[{type}] {log}{Environment.NewLine}";
            }

            _logBuilder.Append(log);
            FlushLog();
        }

        private void FlushLog()
        {
            EnsureLogFileExists();
            if (_logBuilder.Length >= BufferSize)
            {
                var chunk = _logBuilder.ToString().Substring(0, BufferSize);
                File.AppendAllText(LogPath, chunk);
                _logBuilder.Remove(0, BufferSize);
            }

            // NOTE: The following logic keeps log file size between MaxSize and half of its size,
            // effectively discarding older content. Always truncating log file to MaxSize will result
            // in continuous truncation, which would be inefficient.
            var logInfo = new FileInfo(LogPath);
            if (logInfo.Length > MaxSize)
            {
                var log = File.ReadAllText(LogPath);
                var truncated = log[((int)logInfo.Length - (DefaultLogSize / 2))..];
                File.WriteAllText(LogPath, truncated);
            }
        }

        private void EnsureLogFileExists()
        {
            if (!File.Exists(LogPath))
            {
                File.WriteAllText(LogPath, String.Empty);
            }
        }

        private const int DefaultBufferSize = 10;           // KiloBytes
        private const int DefaultLogSize = 4;               // MegaBytes
        private readonly StringBuilder _logBuilder = new();
    }
}
