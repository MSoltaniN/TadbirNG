using System;
using System.Collections.Generic;
using System.Text;
using SPPC.Framework.Values;

namespace SPPC.Framework.Persistence
{
    /// <summary>
    /// اطلاعات مورد نیاز برای صفحه بندی اطلاعات در یک لیست اطلاعاتی را نگهداری می کند
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
        /// شماره صفحه مورد نظر از اطلاعات که از یک شروع می شود
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// تعداد سطرهای اطلاعاتی مورد نظر در هر صفحه از اطلاعات
        /// </summary>
        public int PageSize { get; set; }
    }
}
