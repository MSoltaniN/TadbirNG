using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Workflow
{
    public partial class WorkItemViewModel
    {
        /// <summary>
        /// شناسه دیتابیسی کاربری که این کار توسط او ایجاد شده است
        /// </summary>
        public int CreatedById { get; set; }

        /// <summary>
        /// شناسه دیتابیسی نقش گیرنده این کار
        /// </summary>
        public int TargetId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی مستند مرتبط (ضمیمه) این کار
        /// </summary>
        public int DocumentId { get; set; }

        /// <summary>
        /// وضعیت ثبت سند مالی (مقادیر ممکن عبارتند از : پیش نویس، ثبت نشده، ثبت عادی
        /// و ثبت قطعی)
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// وضعیت عملیاتی سند مالی (مقادیر ممکن عبارتند از : ایجاد شده، تنظیم شده، بررسی شده
        /// ، تایید شده و تصویب شده)
        /// </summary>
        public string OperationalStatus { get; set; }

        /// <summary>
        /// نوع اقدامی که منجر به ایجاد این کار شده است
        /// </summary>
        public string PreviousAction { get; set; }
    }
}
