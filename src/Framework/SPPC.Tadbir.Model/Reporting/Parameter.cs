using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Model.Reporting
{
    public partial class Parameter
    {
        /// <summary>
        /// شناسه دیتابیسی گزارش سیستمی یا کاربری مرتبط
        /// </summary>
        public virtual int ReportId { get; set; }
    }
}
