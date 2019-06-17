using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Configuration.Models
{
    /// <summary>
    /// تنظیمات فرم عمومی جستجو را برای یکی از نماهای اطلاعاتی برنامه نگهداری می کند
    /// </summary>
    public class QuickSearchConfig
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public QuickSearchConfig()
        {
            Columns = new List<QuickSearchColumnConfig>();
        }

        /// <summary>
        /// شناسه دیتابیسی نمای اطلاعاتی
        /// </summary>
        public int ViewId { get; set; }

        /// <summary>
        /// روش جستجو در فیلدهای متنی : از ابتدا، از انتها یا از میان
        /// </summary>
        public string SearchMode { get; set; }

        /// <summary>
        /// مجموعه‌ای از تنظیمات ستون ها در نمای اطلاعاتی
        /// </summary>
        public List<QuickSearchColumnConfig> Columns { get; }
    }
}
