using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Configuration.Models
{
    /// <summary>
    /// تنظیمات یکی از ستون های گزارش فوری را نگهداری می کند
    /// </summary>
    public class QuickReportColumnConfig
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public QuickReportColumnConfig()
        {
            UserTitleMap = new Dictionary<string, string>();
            WidthMap = new Dictionary<string, int>();
        }

        /// <summary>
        /// نام سیستمی ستون
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// کلید متن چندزبانه برای عنوان پیش فرض ستون
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// پهنای تنظیم شده برای ستون
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// شماره ترتیبی تنظیم شده برای ستون که از صفر شروع می شود
        /// </summary>
        public int DisplayIndex { get; set; }

        /// <summary>
        /// متن کاربری تنظیم شده برای ستون که بجای متن پیش فرض استفاده می شود
        /// </summary>
        public string UserTitle { get; set; }

        /// <summary>
        /// مشخص می کند که آیا ستون مورد نظر نشان داده می شود یا نه؟
        /// </summary>
        public bool Visible { get; set; }

        /// <summary>
        /// مشخص کننده نوع ستون برای استفاده خاص مثلا money یا number یا date
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// گروه بندی برای ستون
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// دیکشنری برای نگهداری متن های کاربری عنوان ستون در زبان های مختلف
        /// </summary>
        public IDictionary<string, string> UserTitleMap { get; }

        /// <summary>
        /// دیکشنری برای نگهداری اندازه های کاربری عرض ستون در زبان های مختلف
        /// </summary>
        public IDictionary<string, int> WidthMap { get; }
    }
}
