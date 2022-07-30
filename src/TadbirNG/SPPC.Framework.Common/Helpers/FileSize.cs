using System;

namespace SPPC.Framework.Helpers
{
    /// <summary>
    /// اندازه فایل را به واحدهای مختلف تبدیل می کند
    /// </summary>
    public class FileSize
    {
        /// <summary>
        /// اندازه داده شده به کیلوبایت را به بایت تبدیل می کند
        /// </summary>
        /// <param name="kiloBytes">اندازه داده شده به کیلوبایت</param>
        /// <returns>اندازه تقریبی بر حسب بایت</returns>
        public static long FromKiloBytes(double kiloBytes)
        {
            return (long)Math.Round(kiloBytes * KiloSize);
        }

        /// <summary>
        /// اندازه داده شده به مگابایت را به بایت تبدیل می کند
        /// </summary>
        /// <param name="megaBytes">اندازه داده شده به مگابایت</param>
        /// <returns>اندازه تقریبی بر حسب بایت</returns>
        public static long FromMegaBytes(double megaBytes)
        {
            return (long)Math.Round(megaBytes * KiloSize * KiloSize);
        }

        /// <summary>
        /// اندازه داده شده به گیگابایت را به بایت تبدیل می کند
        /// </summary>
        /// <param name="gigaBytes">اندازه داده شده به گیگابایت</param>
        /// <returns>اندازه تقریبی بر حسب بایت</returns>
        public static long FromGigaBytes(double gigaBytes)
        {
            return (long)Math.Round(gigaBytes * KiloSize * KiloSize * KiloSize);
        }

        /// <summary>
        /// اندازه داده شده بر حسب بایت را با توجه به تعداد اعشار داده شده به کیلوبایت تبدیل می کند
        /// </summary>
        /// <param name="bytes">اندازه داده شده بر حسب بایت</param>
        /// <param name="digits">تعداد اعداد اعشار مورد نیاز برای تبدیل</param>
        /// <returns>اندازه تقریبی بر حسب کیلوبایت</returns>
        public static double ToKiloBytes(long bytes, int digits = 2)
        {
            return Math.Round((double)bytes / KiloSize, digits);
        }

        /// <summary>
        /// اندازه داده شده بر حسب بایت را با توجه به تعداد اعشار داده شده به مگابایت تبدیل می کند
        /// </summary>
        /// <param name="bytes">اندازه داده شده بر حسب بایت</param>
        /// <param name="digits">تعداد اعداد اعشار مورد نیاز برای تبدیل</param>
        /// <returns>اندازه تقریبی بر حسب مگابایت</returns>
        public static double ToMegaBytes(long bytes, int digits = 2)
        {
            return Math.Round((double)bytes / (KiloSize * KiloSize), digits);
        }

        /// <summary>
        /// اندازه داده شده بر حسب بایت را با توجه به تعداد اعشار داده شده به گیگابایت تبدیل می کند
        /// </summary>
        /// <param name="bytes">اندازه داده شده بر حسب بایت</param>
        /// <param name="digits">تعداد اعداد اعشار مورد نیاز برای تبدیل</param>
        /// <returns>اندازه تقریبی بر حسب گیگابایت</returns>
        public static double ToGigaBytes(long bytes, int digits = 2)
        {
            return Math.Round((double)bytes / (KiloSize * KiloSize * KiloSize), digits);
        }

        private const int KiloSize = 1024;
    }
}
