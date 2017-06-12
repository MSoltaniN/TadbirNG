// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.0.0.0
//     Template Version: 1.0
//     Generation Date: 2017-05-01 3:13:25 PM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using SPPC.Tadbir.Model.Auth;
using SwForAll.Platform.Domain;

namespace SPPC.Tadbir.Model.Workflow
{
    /// <summary>
    /// یک رکورد از سوابق عملیات انجام شده روی یک موجودیت را نشان می دهد.
    /// </summary>
    public partial class WorkItemHistory : IEntity
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس ایجاد می کند.
        /// </summary>
        public WorkItemHistory()
        {
            this.Number = String.Empty;
            this.Title = String.Empty;
            this.DocumentType = String.Empty;
            this.Remarks = String.Empty;
            this.Status = String.Empty;
            this.OperationalStatus = String.Empty;
            this.ModifiedDate = DateTime.Now;
            InitReferences();
        }

        /// <summary>
        /// شناسه دیتابیسی این موجودیت که به صورت خودکار توسط دیتابیس تولید می شود
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        /// شماره سری کار که می تواند شامل اعداد و حروف باشد
        /// </summary>
        public virtual string Number { get; set; }

        /// <summary>
        /// تاریخ ایجاد کار که به صورت خودکار مقداردهی می شود
        /// </summary>
        public virtual DateTime Date { get; set; }

        /// <summary>
        /// زمان ایجاد کار که به صورت خودکار مقداردهی می شود
        /// </summary>
        public virtual TimeSpan Time { get; set; }

        /// <summary>
        /// عنوان یا موضوع کار که در چارچوب یک گردش کار به صورت خودکار مقداردهی می شود
        /// </summary>
        public virtual string Title { get; set; }

        /// <summary>
        /// نوع موجودیت مرتبط با کار
        /// </summary>
        public virtual string DocumentType { get; set; }

        /// <summary>
        /// شناسه دیتابیسی موجودیت مرتبط با کار
        /// </summary>
        public virtual int DocumentId { get; set; }

        /// <summary>
        /// نوع اقدامی که در نتیجه این سابقه عملیاتی انجام شده است
        /// </summary>
        public virtual string Action { get; set; }

        /// <summary>
        /// توضیحات یا پاراف متنی مرتبط با کار که می تواند خالی باشد
        /// </summary>
        public virtual string Remarks { get; set; }

        /// <summary>
        /// شناسه یکتای ردیف دیتابیسی که به صورت خودکار توسط دیتابیس مقداردهی می شود
        /// </summary>
        public virtual Guid RowGuid { get; set; }

        /// <summary>
        /// تاریخ آخرین تغییر رکورد دیتابیس که به صورت خودکار توسط ابزار دسترسی به داده مقداردهی می شود
        /// </summary>
        public virtual DateTime ModifiedDate { get; set; }

        /// <summary>
        /// وضعیت ثبتی موجودیت عملیاتی
        /// </summary>
        public virtual string Status { get; set; }

        /// <summary>
        /// وضعیت موجودیت عملیاتی در گردش کار (مقادیر ممکن عبارتند از : ایجاد شده، تنظیم شده، بررسی شده، تایید شده و تصویب شده)
        /// </summary>
        public virtual string OperationalStatus { get; set; }

        /// <summary>
        /// کاربر ایجاد کننده کار
        /// </summary>
        public virtual User User { get; set; }

        private void InitReferences()
        {
            User = new User();
        }
    }
}
