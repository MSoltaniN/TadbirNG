using System;
using System.Collections.Generic;
using SPPC.Tadbir.Domain;

namespace SPPC.Tadbir.Model
{
    /// <summary>
    /// مشخصات مورد نیاز برای موجودیت های پایه ای برنامه را نگهداری می کند
    /// </summary>
    public class BaseEntity : FiscalEntity, IBaseEntity
    {
        /// <summary>
        /// محدوده دسترسی به موجودیت پایه را در سطح شعبه های موجود در سازمان مشخص می کند. مقادیر مجاز شامل
        /// "کلیه شعبه ها" (مقدار 0)، "شعبه جاری و زیرمجموعه ها" (مقدار 1) و "شعبه جاری" (مقدار 2) می شود.
        /// </summary>
        public virtual short BranchScope { get; set; }
    }
}
