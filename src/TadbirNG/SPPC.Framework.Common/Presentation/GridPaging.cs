using System;
using SPPC.Framework.Values;

namespace SPPC.Framework.Presentation
{
    /// <summary>
    /// اطلاعات مورد نیاز برای صفحه بندی یک نمای جدولی را نگهداری می کند
    /// </summary>
    public class GridPaging
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public GridPaging()
        {
            PageIndex = 1;
            PageSize = Constants.GridPageSize;
        }

        /// <summary>
        /// نمونه کمکی برای غیرفعال کردن صفحه بندی در یک نمای جدولی
        /// </summary>
        public static GridPaging NoPaging
        {
            get
            {
                return new GridPaging()
                {
                    PageIndex = 1,
                    PageSize = Int32.MaxValue
                };
            }
        }

        /// <summary>
        /// شماره صفحه مورد نظر از نمای جدولی که از یک شروع می شود
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// تعداد سطرهای مورد نیاز در هر صفحه از نمای جدولی
        /// </summary>
        public int PageSize { get; set; }
    }
}
