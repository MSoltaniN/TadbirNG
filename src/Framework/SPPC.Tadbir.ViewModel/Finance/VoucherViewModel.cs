﻿using System;
using System.Collections.Generic;
using SPPC.Tadbir.ViewModel.Core;

namespace SPPC.Tadbir.ViewModel.Finance
{
    public partial class VoucherViewModel
    {
        /// <summary>
        /// شناسه دیتابیسی دوره مالی که این سرفصل حسابداری در آن تعریف شده است
        /// </summary>
        public int FiscalPeriodId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی شعبه سازمانی که این سرفصل حسابداری در آن تعریف شده است
        /// </summary>
        public int BranchId { get; set; }

        /// <summary>
        /// جمع مقادیر بدهکار در آرتیکل های سند
        /// </summary>
        public decimal DebitSum { get; set; }

        /// <summary>
        /// جمع مقادیر بستانکار در آرتیکل های سند
        /// </summary>
        public decimal CreditSum { get; set; }
    }
}
