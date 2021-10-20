using System;
using System.Collections.Generic;
using SPPC.Framework.Values;

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
            PageSize = Constants.GridPageSize;
            ColumnViews = new List<ColumnViewConfig>();
        }

        /// <summary>
        /// شناسه دیتابیسی مدل نمایشی مورد استفاده در فرم لیستی
        /// </summary>
        public int ViewId { get; set; }

        /// <summary>
        /// تعداد سطرهای اطلاعاتی نمایش داده شده در هر صفحه از نمای لیستی
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// تنظیمات نمایشی ستون های نمای لیستی
        /// </summary>
        public IList<ColumnViewConfig> ColumnViews { get; private set; }
    }
}
