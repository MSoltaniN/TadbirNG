// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.0.0.0
//     Template Version: 1.0
//     Generation Date: 2017-04-27 12:15:00 PM
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
    /// یک ردیف کار را در کارتابل کاربران یا نقش ها نشان می دهد
    /// </summary>
    public partial class WorkItem : IEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WorkItem"/> class.
        /// </summary>
        public WorkItem()
        {
            this.Number = String.Empty;
            this.Title = String.Empty;
            this.DocumentType = String.Empty;
            this.Remarks = String.Empty;
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
        /// توضیحات یا پاراف متنی مرتبط با کار که می تواند خالی باشد
        /// </summary>
        public virtual string Remarks { get; set; }

        /// <summary>
        /// شناسه یکتای ردیف دیتابیسی که از نوع Guid بوده و به صورت خودکار توسط دیتابیس مقداردهی می شود
        /// </summary>
        public virtual Guid RowGuid { get; set; }

        /// <summary>
        /// تاریخ آخرین تغییر رکورد دیتابیس که به صورت خودکار توسط ابزار دسترسی به داده مقداردهی می شود
        /// </summary>
        public virtual DateTime ModifiedDate { get; set; }

        /// <summary>
        /// کاربر ایجاد کننده کار
        /// </summary>
        public virtual User CreatedBy { get; set; }

        /// <summary>
        /// نقش سازمانی که باید گیرنده کار باشد
        /// </summary>
        public virtual Role Target { get; set; }

        /// <summary>
        /// رکوردهای موجودیت مرتبط با کار
        /// </summary>
        public virtual IList<WorkItemDocument> Documents { get; protected set; }

        private void InitReferences()
        {
            this.Documents = new List<WorkItemDocument>();

            //// IMPORTANT NOTE: DO NOT add initialization statements for one-to-one and many-to-one relationships.
            //// 1. Initializing one-to-one associations causes StackOverflowException (A initializes B and B initializes A)
            //// 2. Initializing many-to-one associations causes most mapping tests to fail, because they will trigger many
            //// unnecessary operations (INSERT and UPDATE) by in-memory SQLite database.
        }
    }
}
