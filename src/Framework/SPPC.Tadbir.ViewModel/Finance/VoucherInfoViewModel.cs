using System;
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

        /// <summary>
        ///
        /// </summary>
        public int LineCount { get; set; }

        /// <summary>
        ///
        /// </summary>
        public bool IsApproved { get; set; }

        /// <summary>
        ///
        /// </summary>
        public bool IsConfirmed { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int IsBalanced { get; set; }
    }
}
