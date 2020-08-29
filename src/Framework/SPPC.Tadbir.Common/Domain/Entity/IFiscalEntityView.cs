using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Domain
{
    /// <summary>
    /// مشخصات مورد نیاز برای یک نمای اطلاعاتی محدود به شرکت، شعبه و دوره مالی را تعریف می کند
    /// </summary>
    public interface IFiscalEntityView
    {
        /// <summary>
        /// شناسه دیتابیسی دوره مالی که موجودیت اصلی این نمای اطلاعاتی در آن تعریف شده است
        /// </summary>
        int FiscalPeriodId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی شعبه سازمانی که موجودیت اصلی این نمای اطلاعاتی در آن تعریف شده است
        /// </summary>
        int BranchId { get; set; }
    }
}
