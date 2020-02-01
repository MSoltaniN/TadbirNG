using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Core
{
    public partial class OperationLogViewModel : ViewModelBase
    {
        /// <summary>
        /// نام و نام خانوادگی کاربری که عملیات برنامه توسط او انجام شده
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// نام شعبه ای که عملیات در آن انجام شده
        /// </summary>
        public string BranchName { get; set; }

        /// <summary>
        /// نام دوره مالی که عملیات در آن انجام شده
        /// </summary>
        public string FiscalPeriodName { get; set; }

        /// <summary>
        /// نام محلی شده موجودیت مورد استفاده در عملیات
        /// </summary>
        public string EntityTypeName { get; set; }

        /// <summary>
        /// نام محلی شده فرم مورد استفاده در عملیات
        /// </summary>
        public string SourceName { get; set; }

        /// <summary>
        /// نام محلی شده نمای لیستی مورد استفاده در عملیات
        /// </summary>
        public string SourceListName { get; set; }

        /// <summary>
        /// نام محلی شده عملیات انجام شده
        /// </summary>
        public string OperationName { get; set; }

        /// <summary>
        /// شناسه دیتابیسی دوره مالی که عملیات در آن انجام شده است
        /// </summary>
        public int FiscalPeriodId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی شعبه ای که عملیات در آن انجام شده است
        /// </summary>
        public int BranchId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی عملیات انجام شده
        /// </summary>
        public int OperationId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی فرم عملیاتی به کار رفته
        /// </summary>
        public int? SourceId { get; set; }

        /// <summary>
        /// شناسه نمای اطلاعاتی لیستی به کار رفته حین انجام عملیات
        /// </summary>
        public int? SourceListId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی نوع موجودیتی که عملیات روی آن انجام شده است
        /// </summary>
        public int? EntityTypeId { get; set; }

        /// <summary>
        /// نام شرکتی که عملیات مورد نظر روی دیتابیس آن انجام شده
        /// </summary>
        public string CompanyName { get; set; }
    }
}
