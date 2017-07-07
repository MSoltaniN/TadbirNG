using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Finance
{
    /// <summary>
    /// اطلاعات خلاصه مربوط به یک سند مالی را نشان می دهد.
    /// </summary>
    /// <remarks>این کلاس نمایشی برای استفاده در گردش کار نمونه در نظر گرفته شده و اطلاعات آن
    /// در آینده قابل گسترش است.</remarks>
    public class TransactionSummaryViewModel
    {
        /// <summary>
        /// شناسه دیتابیسی سند مورد نظر
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// شماره سند مالی که می تواند شامل اعداد و حروف باشد
        /// </summary>
        public virtual string No { get; set; }

        /// <summary>
        /// جمع مقادیر بدهکار در آرتیکل های سند
        /// </summary>
        public decimal DebitSum { get; set; }

        /// <summary>
        /// جمع مقادیر بستانکار در آرتیکل های سند
        /// </summary>
        public decimal CreditSum { get; set; }

        /// <summary>
        /// وضعیت عملیاتی سند
        /// </summary>
        public string OperationalStatus { get; set; }
    }
}
