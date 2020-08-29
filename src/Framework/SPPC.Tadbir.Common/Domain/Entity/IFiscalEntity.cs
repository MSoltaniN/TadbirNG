using System;
using System.Collections.Generic;
using SPPC.Framework.Domain;

namespace SPPC.Tadbir.Domain
{
    /// <summary>
    /// مشخصات مورد نیاز برای یک موجودیت محدود به دوره مالی و شعبه را تعریف می کند
    /// </summary>
    public interface IFiscalEntity : IEntity
    {
        /// <summary>
        /// شناسه یکتای دوره مالی که این موجودیت در آن تعریف می شود
        /// </summary>
        int FiscalPeriodId { get; set; }

        /// <summary>
        /// شناسه یکتای شعبه سازمانی که این موجودیت در آن تعریف می شود
        /// </summary>
        int BranchId { get; set; }
    }
}
