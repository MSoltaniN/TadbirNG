using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Configuration.Models
{
    /// <summary>
    /// تنظیمات یک ستون قابل نمایش و قابل جستجو در فرم عمومی جستجو را نگهداری می کند
    /// </summary>
    public class QuickSearchColumnConfig
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public QuickSearchColumnConfig()
        {
        }

        /// <summary>
        /// نام سیستمی ستون
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// عنوان محلی شده ستون
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// ایندکس ترتیبی نمایش ستون در لیست نتایج جستجو، که از صفر شروع می شود
        /// </summary>
        public int DisplayIndex { get; set; }

        /// <summary>
        /// مشخص می کند که آیا ستون مورد نظر در لیست نتایج نمایش داده می شود یا نه؟
        /// </summary>
        public bool IsDisplayed { get; set; }

        /// <summary>
        /// مشخص می کند که آیا ستون مورد نظر برای جستجوی متنی مورد استفاده قرار می گیرد یا نه؟
        /// </summary>
        public bool IsSearched { get; set; }
    }
}
