using System;
using System.Collections.Generic;
using SPPC.Tadbir.Values;

namespace SPPC.Tadbir.Metadata.Workflow
{
    /// <summary>
    /// اطلاعات فراداده ای یک اقدام روی مستند اداری را نگهداری می کند
    /// </summary>
    public class StateAction
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public StateAction()
        {
            WorkTitle = WorkItemTitle.ActOnDocument;
        }

        /// <summary>
        /// نام اقدام
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// نام اقدام بعدی
        /// </summary>
        public string NextAction { get; set; }

        /// <summary>
        /// شناسه دیتابیسی وضعیت ثبتی مستند اداری پیش از تکمیل اقدام
        /// </summary>
        public int FromStatus { get; set; }

        /// <summary>
        /// شناسه دیتابیسی وضعیت ثبتی مستند اداری پس از تکمیل اقدام
        /// </summary>
        public int ToStatus { get; set; }

        /// <summary>
        /// وضعیت عملیاتی مستند اداری پیش از تکمیل اقدام
        /// </summary>
        public string FromDocumentStatus { get; set; }

        /// <summary>
        /// وضعیت عملیاتی مستند اداری پس از تکمیل اقدام
        /// </summary>
        public string ToDocumentStatus { get; set; }

        /// <summary>
        /// عنوان کار ایجاد شده در کارتابل مقصد پس از تکمیل اقدام
        /// </summary>
        public string WorkTitle { get; set; }

        /// <summary>
        /// شناسه دیتابیسی نقش سازمانی گیرنده کار ایجاد شده پس از تکمیل اقدام
        /// </summary>
        public int TargetId { get; set; }

        /// <summary>
        /// مشخص می کند که آیا این اقدام مستند را به یکی از وضعیت های قبلی می برد یا نه؟
        /// </summary>
        public bool IsReverse { get; set; }

        /// <summary>
        /// مشخص می کند که آیا این اقدام آخرین اقدام ممکن روی مستند است یا نه؟
        /// </summary>
        public bool IsFinal { get; set; }
    }
}
