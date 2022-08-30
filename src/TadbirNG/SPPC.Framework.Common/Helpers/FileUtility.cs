using System;
using System.IO;
using System.Linq;
using System.Threading;

namespace SPPC.Framework.Helpers
{
    /// <summary>
    /// عملیات کمکی مورد استفاده هنگام کار با فایل ها و فولدرها را پیاده سازی می کند
    /// </summary>
    public class FileUtility
    {
        /// <summary>
        /// یک فولدر موقتی در مسیر فایل های موقتی کاربر جاری ایجاد کرده و مسیر کامل آن را برمی گرداند
        /// </summary>
        /// <returns>مسیر کامل فولدر موقتی ایجاد شده</returns>
        public static string GetTempFolderPath()
        {
            var tempFile = Path.GetTempFileName();
            var tempFolder = String.Format($"bla-{Path.GetFileName(tempFile)}");
            File.Delete(tempFile);
            var tempPath = Path.Combine(Path.GetDirectoryName(tempFile), tempFolder);
            if (!Directory.Exists(tempPath))
            {
                Directory.CreateDirectory(tempPath);
            }

            return tempPath;
        }

        /// <summary>
        /// نسخه پشتیبانی از فایل داده شده در مسیر موقتی داده شده ایجاد می کند
        /// </summary>
        /// <param name="filePath">مسیر نسبی یا مطلق فایل مورد نظر برای تهیه نسخه پشتیبان</param>
        /// <param name="tempPath">مسیر موقتی مورد نظر برای ذخیره نسخه پشتیبان فایل</param>
        public static void BackupFile(string filePath, string tempPath)
        {
            if (File.Exists(filePath))
            {
                File.Copy(filePath, Path.Combine(tempPath, Path.GetFileName(filePath)));
            }
        }

        /// <summary>
        /// فایل پشتیبان داده شده را از مسیر موقت به مسیر اصلی کپی کرده و نسخه پشتیبان را پاک می کند
        /// </summary>
        /// <param name="filePath">مسیر فایل مورد نظر برای بازیابی از نسخه پشتیبان</param>
        /// <param name="tempPath">مسیر نسخه پشتیبان از فایل مورد نظر</param>
        public static void RestoreFile(string filePath, string tempPath)
        {
            string backupPath = Path.Combine(tempPath, Path.GetFileName(filePath));
            if (File.Exists(backupPath))
            {
                File.Copy(backupPath, filePath, true);
                File.Delete(backupPath);
            }
        }

        /// <summary>
        /// نسخه پشتیبانی از فایل های داده شده در مسیر موقتی داده شده ایجاد می کند
        /// </summary>
        /// <param name="files">مسیر نسبی یا مطلق فایل های مورد نظر برای تهیه نسخه پشتیبان</param>
        /// <param name="tempPath">مسیر موقتی مورد نظر برای ذخیره نسخه پشتیبان فایل ها</param>
        public static void BackupFiles(string[] files, string tempPath)
        {
            Array.ForEach(files, file => BackupFile(file, tempPath));
        }

        /// <summary>
        /// فایل های پشتیبان داده شده را از مسیر موقت به مسیر اصلی کپی کرده و نسخه های پشتیبان را پاک می کند
        /// </summary>
        /// <param name="files">مسیر فایل های مورد نظر برای بازیابی از نسخه پشتیبان</param>
        /// <param name="tempPath">مسیر نسخه پشتیبان از فایل های مورد نظر</param>
        public static void RestoreFiles(string[] files, string tempPath)
        {
            Array.ForEach(files, file => RestoreFile(file, tempPath));
        }

        /// <summary>
        /// مسیر نسبی داده شده را نسبت به یک مسیر خاص یا فولدر جاری برنامه به مسیر مطلق تبدیل می کند
        /// </summary>
        /// <param name="relativePath">مسیر نسبی مورد نظر برای تبدیل</param>
        /// <param name="relativeToPath">مسیر مورد نظر که مسیر نسبی بر مبنای آن تبدیل می شود.
        /// در صورت مقدار نداشتن، مسیر نسبی بر مبنای فولدر جاری برنامه تبدیل می شود.</param>
        /// <returns></returns>
        public static string GetAbsolutePath(string relativePath, string relativeToPath = null)
        {
            var absolutePath = relativeToPath ?? Environment.CurrentDirectory;
            var parts = relativePath.Split(Path.DirectorySeparatorChar);
            foreach (var part in parts)
            {
                if (part == "..")
                {
                    absolutePath = Path.GetDirectoryName(absolutePath);
                }
                else if (part != ".")
                {
                    absolutePath = Path.Combine(absolutePath, part);
                }
            }

            return absolutePath;
        }

        /// <summary>
        /// اندازه تقریبی فایل های موجود در مسیر داده شده را بر حسب کیلوبایت محاسبه کرده
        /// .و به صورت مقدار صحیح برمی گرداند
        /// </summary>
        /// <param name="root">مسیر مورد نظر برای محاسبه اندازه تقریبی فایل ها</param>
        /// <returns>اندازه تقریبی فایل های موجود در مسیر داده شده</returns>
        public static int GetFolderSize(string root)
        {
            var directoryInfo = new DirectoryInfo(root);
            var bytesCount = directoryInfo
                .GetFiles("*.*", new EnumerationOptions() { RecurseSubdirectories = true })
                .Select(fi => fi.Length)
                .Sum();
            return (int)Math.Round((double)bytesCount / 1024);
        }

        /// <summary>
        /// فایل ها و فولدرهای موجود در مسیر داده شده و تمام مسیرهای زیرمجموعه آن را پاک می کند
        /// </summary>
        /// <param name="path">مسیر اصلی فولدر مورد نظر</param>
        /// <param name="logFile">فایل لاگ مورد نظر برای ثبت خطاهای احتمالی</param>
        /// <remarks>
        /// چون گاهی فایل ها و فولدرها توسط برنامه های در حال اجرا یا سیستم عامل قفل می شوند، این تابع
        /// به تعداد مشخص و بعد از مدتی تاخیر زمانی برای پاک کردن فایل ها و فولدرها تلاش دوباره می کند.
        /// </remarks>
        public static void DeleteFolder(string path, LogFile logFile = null)
        {
            var dirInfo = new DirectoryInfo(path);
            foreach (var file in dirInfo.GetFiles("*.*", SearchOption.AllDirectories))
            {
                DeleteFileWithRetry(file.FullName, logFile);
            }

            DeleteFolderRecursive(path, logFile);
        }

        private static void DeleteFolderRecursive(string path, LogFile logFile = null)
        {
            var dirInfo = new DirectoryInfo(path);
            foreach (var folder in dirInfo.GetDirectories("*.*", SearchOption.TopDirectoryOnly))
            {
                DeleteFolderRecursive(folder.FullName, logFile);
            }

            DeleteFolderWithRetry(path, logFile);
        }

        private static void DeleteFileWithRetry(string path, LogFile logFile = null)
        {
            bool deleted;
            int retries = 1;
            do
            {
                try
                {
                    File.Delete(path);
                    deleted = true;
                }
                catch
                {
                    // NOTE: The following delay may be too long or too short,
                    // but files may remain locked for a while.
                    logFile?.LogError($"Error deleting file '{path}'. Retrying ({retries++} of {MaxRetries})...");
                    deleted = false;
                    Thread.Sleep(TimeSpan.FromSeconds(RetryTimeout));
                }
            } while (!deleted && retries <= MaxRetries);
        }

        private static void DeleteFolderWithRetry(string path, LogFile logFile = null)
        {
            bool deleted;
            int retries = 1;
            do
            {
                try
                {
                    Directory.Delete(path);
                    deleted = true;
                }
                catch
                {
                    // NOTE: The following delay may be too long or too short,
                    // but folders may remain locked for a while.
                    logFile?.LogError($"Error deleting folder '{path}'. Retrying ({retries++} of {MaxRetries})...");
                    deleted = false;
                    Thread.Sleep(TimeSpan.FromSeconds(RetryTimeout));
                }
            } while (!deleted && retries <= MaxRetries);
        }

        private const int RetryTimeout = 5;
        private const int MaxRetries = 15;
    }
}
