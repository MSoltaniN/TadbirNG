// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.372
//     Template Version: 1.0
//     Generation Date: 2018-08-13 9:42:34 AM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using SPPC.Tadbir.Model.Auth;
using SPPC.Tadbir.Model.Config;

namespace SPPC.Tadbir.Model.Core
{
    /// <summary>
    /// اطلاعات سوابق عملیاتی برنامه را در هر شرکت نگهداری می کند
    /// </summary>
    public partial class OperationLog : CoreEntity
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public OperationLog()
        {
            View = String.Empty;
            Action = String.Empty;
            BeforeState = String.Empty;
            AfterState = String.Empty;
            ModifiedDate = DateTime.Now;
            InitReferences();
        }

        /// <summary>
        /// تاریخ انجام عملیات
        /// </summary>
        public virtual DateTime Date { get; set; }

        /// <summary>
        /// زمان انجام عملیات
        /// </summary>
        public virtual TimeSpan Time { get; set; }

        /// <summary>
        /// نام نمای اطلاعاتی مورد استفاده در عملیات
        /// </summary>
        public virtual string View { get; set; }

        /// <summary>
        /// نوع عملیات انجام شده
        /// </summary>
        public virtual string Action { get; set; }

        /// <summary>
        /// مشخص می کند که عملیات انجام شده موفقیت آمیز بوده یا نه
        /// </summary>
        public virtual bool Succeeded { get; set; }

        /// <summary>
        /// متن خطای ایجاد شده حین عملیات
        /// </summary>
        public virtual string FailReason { get; set; }

        /// <summary>
        /// وضعیت اطلاعات پیش از انجام عملیات
        /// </summary>
        public virtual string BeforeState { get; set; }

        /// <summary>
        /// وضعیت اطلاعات پس از انجام عملیات
        /// </summary>
        public virtual string AfterState { get; set; }

        /// <summary>
        /// شناسه دوره مالی فعال در برنامه هنگام انجام عملیات
        /// </summary>
        public virtual int FiscalPeriodId { get; set; }

        /// <summary>
        /// شناسه شعبه سازمانی فعال در برنامه هنگام انجام عملیات
        /// </summary>
        public virtual int BranchId { get; set; }

        /// <summary>
        /// کاربری که عملیات توسط او در برنامه انجام شده
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// شرکتی که عملیات روی دیتابیس آن انجام شده
        /// </summary>
        public virtual CompanyDb Company { get; set; }

        private void InitReferences()
        {
            User = new User();
            Company = new CompanyDb();
        }
    }
}