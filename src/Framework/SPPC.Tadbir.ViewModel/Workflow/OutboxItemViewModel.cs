using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Workflow
{
    /// <summary>
    /// یک ردیف از سوابق کارهای ایجاد شده توسط یک کاربر را نشان می دهد
    /// </summary>
    public class OutboxItemViewModel
    {
        /// <summary>
        /// یک نمونه جدید از این کلاس ایجاد می کند
        /// </summary>
        public OutboxItemViewModel()
        {
        }

        /// <summary>
        /// تاریخ اقدام انجام شده روی یک مستند
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// زمان اقدام انجام شده روی یک مستند
        /// </summary>
        public TimeSpan Time { get; set; }

        /// <summary>
        /// شناسه دیتابیسی مستند مرتبط با کار
        /// </summary>
        public int DocumentId { get; set; }

        /// <summary>
        /// نوع مستند مرتبط با کار
        /// </summary>
        public string DocumentType { get; set; }

        /// <summary>
        /// شماره موجودیت مرتبط با کار
        /// </summary>
        public string EntityNo { get; set; }

        /// <summary>
        /// نوع اقدامی که در نتیجه این سابقه عملیاتی انجام شده است
        /// </summary>
        public string Action { get; set; }
    }
}
