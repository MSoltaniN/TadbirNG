using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Core
{
    public partial class OperationLogViewModel
    {
        public int FiscalPeriodId { get; set; }

        public int BranchId { get; set; }

        public int OperationId { get; set; }

        public int? SourceId { get; set; }

        public int? EntityTypeId { get; set; }

        /// <summary>
        /// نام شرکتی که عملیات مورد نظر روی دیتابیس آن انجام شده
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// نام و نام خانوادگی کاربری که عملیات برنامه توسط او انجام شده
        /// </summary>
        public string UserName { get; set; }
    }
}
