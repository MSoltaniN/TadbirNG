using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Model.Core
{
    public partial class SystemError
    {
        /// <summary>
        /// شناسه دیتابیسی شرکتی که خطای سیستمی هنگام کار با اطلاعات آن روی داده است
        /// </summary>
        public virtual int? CompanyId { get; set; }
    }
}
