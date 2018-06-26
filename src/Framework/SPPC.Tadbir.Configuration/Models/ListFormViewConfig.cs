using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Configuration.Models
{
    /// <summary>
    /// اطلاعات تنظیمات نمایشی مورد استفاده در لیست های اطلاعاتی را نگهداری می کند
    /// </summary>
    public class ListFormViewConfig
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public ListFormViewConfig()
        {
            DefaultColumnView = new ColumnViewConfig();
            ColumnViews = new List<ColumnViewConfig>();
        }

        /// <summary>
        /// تعداد سطرهای اطلاعاتی نمایش داده شده در هر صفحه از نمای لیستی
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// تنظیمات نمایشی پیش فرض برای تمام ستون های نمای لیستی
        /// </summary>
        public ColumnViewConfig DefaultColumnView { get; set; }

        /// <summary>
        /// تنظیمات نمایشی ستون های نمای لیستی
        /// </summary>
        public IList<ColumnViewConfig> ColumnViews { get; private set; }
    }
}
