using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Workflow
{
    /// <summary>
    /// یک ردیف از سوابق اقدامات انجام شده روی یک مستند را نشان می دهد
    /// </summary>
    public class HistoryItemViewModel
    {
        /// <summary>
        /// یک نمونه جدید از این کلاس ایجاد می کند
        /// </summary>
        public HistoryItemViewModel()
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
        /// نام و نام خانوادگی کاربر اقدام کننده روی مستند
        /// </summary>
        public string UserFullName { get; set; }

        /// <summary>
        /// وضعیت ثبتی مستند پس از اقدام
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// وضعیت عملیاتی مستند پس از اقدام
        /// </summary>
        public string OperationalStatus { get; set; }
    }
}
