using System;
using System.Collections.Generic;
using SPPC.Tadbir.Domain;

namespace SPPC.Tadbir.ViewModel.Finance
{
    /// <summary>
    ///
    /// </summary>
    public class VoucherInfoViewModel : IFiscalEntityView
    {
        /// <summary>
        ///
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int No { get; set; }

        /// <summary>
        ///
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// شناسه دیتابیسی دوره مالی که موجودیت اصلی این نمای اطلاعاتی در آن تعریف شده است
        /// </summary>
        public int FiscalPeriodId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی شعبه سازمانی که موجودیت اصلی این نمای اطلاعاتی در آن تعریف شده است
        /// </summary>
        public int BranchId { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int SubjectType { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int OriginId { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int StatusId { get; set; }

        public int LineCount { get; set; }

        public bool IsApproved { get; set; }

        public bool IsConfirmed { get; set; }

        public int IsBalanced { get; set; }
    }
}
