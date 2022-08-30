using System;

namespace SPPC.Tadbir.Configuration.Models
{
    /// <summary>
    /// تنظیمات مورد استفاده در یکی از نمودارهای کاربری در داشبورد را نگهداری می کند
    /// </summary>
    public class ChartConfig
    {
        /// <summary>
        /// مختصات افقی گوشه بالا و سمت چپ برای ویجت نمودار
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// مختصات عمودی گوشه بالا و سمت چپ برای ویجت نمودار
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// پهنای ویجت نمودار بر حسب پیکسل
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// ارتفاع ویجت نمودار بر حسب پیکسل
        /// </summary>
        public int Height { get; set; }
    }
}
