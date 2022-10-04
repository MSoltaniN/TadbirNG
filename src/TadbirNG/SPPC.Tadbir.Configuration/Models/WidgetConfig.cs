using System.Collections.Generic;

namespace SPPC.Tadbir.Configuration.Models
{
    /// <summary>
    /// تنظیمات مورد استفاده در یکی از نمودارهای کاربری در داشبورد را نگهداری می کند
    /// </summary>
    public class WidgetConfig
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public WidgetConfig()
        {
            Parameters = new List<ParameterSummary>();
        }

        /// <summary>
        /// مختصات افقی گوشه بالا و سمت چپ برای ویجت نمودار
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// مختصات عمودی گوشه بالا و سمت چپ برای ویجت نمودار
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// پهنای ویجت نمودار بر حسب واحد مورد استفاده در پکیج نمودار
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// ارتفاع ویجت نمودار بر حسب واحد مورد استفاده در پکیج نمودار
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// مجموعه پارامترهای مورد نیاز در تابع محاسباتی به کار رفته
        /// </summary>
        public IList<ParameterSummary> Parameters { get; }
    }
}
