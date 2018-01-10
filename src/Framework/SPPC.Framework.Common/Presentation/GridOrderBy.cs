using System;
using System.Collections.Generic;

namespace SPPC.Framework.Presentation
{
    /// <summary>
    /// اطلاعات مورد نیاز برای عمل مرتب سازی بر حسب یک فیلد اطلاعاتی را نگهداری می کند
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
        /// جهت مرتب سازی که می تواند صعودی یا نزولی باشد. پیش فرض این مشخصه مرتب سازی صعودی است.
        /// </summary>
        public string Direction { get; set; }

        /// <summary>
        /// نمایش متنی برای این نمونه را برمی گرداند
        /// </summary>
        /// <returns>نمایش متنی برای این نمونه</returns>
        public override string ToString()
        {
            return String.Format("[{0}] {1}", FieldName, Direction);
        }
    }
}
