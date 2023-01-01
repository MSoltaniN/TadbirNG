using System.Collections.Generic;
using System.Linq;
using SPPC.Framework.Extensions;
using SPPC.Framework.Presentation;

namespace SPPC.Tadbir.Utility
{
    /// <summary>
    /// کلاس کمکی برای نگهداری یک فهرست اطلاعاتی صفحه بندی شده و تعداد کل سطرهای آن
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagedList<T>
    {
        /// <summary>
        /// یک نمونه جدید از این کلاس می سازد
        /// </summary>
        /// <param name="all">فهرست کامل سطرهای اطلاعاتی بدون صفحه بندی</param>
        /// <param name="gridOptions">گزینه های موجود برای مرتب سازی، فیلتر و صفحه بندی</param>
        public PagedList(IEnumerable<T> all = null, GridOptions gridOptions = null)
        {
            SetItems(all ?? new List<T>(), gridOptions ?? new GridOptions());
        }

        /// <summary>
        /// تعداد کل سطرهای اطلاعاتی خوانده شده
        /// </summary>
        public int TotalCount { get; private set; }

        /// <summary>
        /// فهرست اطلاعاتی پس از اعمال تمام گزینه های نمای لیستی
        /// </summary>
        public List<T> Items { get; private set; }

        private void SetItems(IEnumerable<T> all, GridOptions gridOptions)
        {
            TotalCount = all
                .Apply(gridOptions, false)
                .ApplyQuickFilter(gridOptions, false)
                .Count();
            Items = new List<T>(all
                .Apply(gridOptions)
                .ApplyQuickFilter(gridOptions));
        }
    }
}
