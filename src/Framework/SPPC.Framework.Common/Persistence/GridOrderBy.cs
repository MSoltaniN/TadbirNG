using System;
using System.Collections.Generic;
using System.Text;

namespace SPPC.Framework.Persistence
{
    /// <summary>
    /// اطلاعات مورد نیاز برای عمل مرتب سازی اطلاعات بر حسب یک فیلد اطلاعاتی را نگهداری می کند
    /// </summary>
    public class GridOrderBy
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public GridOrderBy()
        {
            Direction = OrderBy.Ascending;
        }

        /// <summary>
        /// نام فیلد اطلاعاتی مورد استفاده در مرتب سازی
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// جهت مرتب سازی اطلاعات که می تواند صعودی یا نزولی باشد. پیش فرض این مشخصه مرتب سازی صعودی است.
        /// </summary>
        public string Direction { get; set; }

        /// <summary>
        /// مقادیر این نمونه را به صورت متنی بر می گرداند
        /// </summary>
        /// <returns>مقادیر این نمونه به صورت متنی</returns>
        public override string ToString()
        {
            return String.Format("[{0}] {1}", FieldName, Direction);
        }
    }
}
