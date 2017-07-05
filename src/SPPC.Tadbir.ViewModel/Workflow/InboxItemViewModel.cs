using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Workflow
{
    /// <summary>
    /// این موجودیت یک ردیف کار را در کارتابل ورودی کاربران نشان می دهد
    /// </summary>
    public class InboxItemViewModel
    {
        /// <summary>
        /// یک نمونه جدید از این کلاس ایجاد می کند
        /// </summary>
        public InboxItemViewModel()
        {
        }

        /// <summary>
        /// شناسه دیتابیسی این موجودیت
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// نام و نام خانوادگی کاربر ایجاد کننده کار
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// شماره سری کار که می تواند شامل اعداد و حروف باشد
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// تاریخ ایجاد کار که به صورت خودکار مقداردهی می شود
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// زمان ایجاد کار که به صورت خودکار مقداردهی می شود
        /// </summary>
        public TimeSpan Time { get; set; }

        /// <summary>
        /// عنوان یا موضوع کار که در چارچوب یک گردش کار به صورت خودکار مقداردهی می شود
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// شناسه دیتابیسی موجودیت مرتبط با کار
        /// </summary>
        public int DocumentId { get; set; }

        /// <summary>
        /// نوع موجودیت مرتبط با کار
        /// </summary>
        public string DocumentType { get; set; }

        /// <summary>
        /// شماره موجودیت مرتبط با کار
        /// </summary>
        public string DocumentNo { get; set; }

        /// <summary>
        /// وضعیت عملیاتی موجودیت مرتبط یا کار
        /// </summary>
        public string DocumentStatus { get; set; }

        /// <summary>
        /// پاراف متنی اختیاری که کاربر پیش از اقدام می تواند وارد کند
        /// </summary>
        public string Remarks { get; set; }
    }
}
