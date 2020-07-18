using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Finance
{
    /// <summary>
    /// اطلاعات مانده یک حساب را در محدوده تاریخی مشخصی نگهداری می کند
    /// </summary>
    public class StartEndBalanceViewModel
    {
        /// <summary>
        /// شناسه دیتابیسی حساب مورد نظر
        /// </summary>
        public int AccountId { get; set; }

        /// <summary>
        /// مانده ابتدای دوره حساب - بدهکار
        /// </summary>
        public decimal StartBalanceDebit { get; set; }

        /// <summary>
        /// مانده ابتدای دوره حساب - بستانکار
        /// </summary>
        public decimal StartBalanceCredit { get; set; }

        /// <summary>
        /// مانده انتهای دوره حساب - بدهکار
        /// </summary>
        public decimal EndBalanceDebit { get; set; }

        /// <summary>
        /// مانده انتهای دوره حساب - بستانکار
        /// </summary>
        public decimal EndBalanceCredit { get; set; }
    }
}
