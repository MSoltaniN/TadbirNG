using System;
using System.ComponentModel.DataAnnotations;

namespace SPPC.Tadbir.ViewModel.Finance
{
    /// <summary>
    /// اطلاعات نمایشی یک بردار حساب را نشان می دهد
    /// </summary>
    public partial class FullAccountViewModel
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public FullAccountViewModel()
        {
        }

        /// <summary>
        /// شناسه دیتابیسی این موجودیت
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// شناسه دیتابیسی مولفه سرفصل مالی از بردار حساب
        /// </summary>
        public int AccountId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی مولفه تفصیلی شناور از بردار حساب
        /// </summary>
        public int DetailId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی مولفه مرکز هزینه از بردار حساب
        /// </summary>
        public int CostCenterId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی مولفه پروژه از بردار حساب
        /// </summary>
        public int ProjectId { get; set; }
    }
}
