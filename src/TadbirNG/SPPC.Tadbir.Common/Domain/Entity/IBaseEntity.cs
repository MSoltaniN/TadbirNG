using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Domain
{
    /// <summary>
    /// مشخصات مورد نیاز برای موجودیت های پایه ای برنامه را تعریف می کند
    /// </summary>
    public interface IBaseEntity : IFiscalEntity
    {
        /// <summary>
        /// محدوده دسترسی به حساب را در سطح شعبه های موجود در سازمان مشخص می کند. مقادیر مجاز شامل
        /// "کلیه شعبه ها" (مقدار 0)، "شعبه جاری و زیرمجموعه ها" (مقدار 1) و "شعبه جاری" (مقدار 2) می شود.
        /// </summary>
        short BranchScope { get; set; }
    }
}
