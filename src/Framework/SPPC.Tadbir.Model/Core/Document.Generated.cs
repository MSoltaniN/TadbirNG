﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.0.0.0
//     Template Version: 1.0
//     Generation Date: 2017-08-03 6:50:35 PM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using SPPC.Framework.Domain;

namespace SPPC.Tadbir.Model.Core
{
    /// <summary>
    /// یک مستند اداری مورد استفاده در گردش های کاری سازمان را نشان می دهد
    /// </summary>
    public partial class Document : IEntity
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس ایجاد می کند.
        /// </summary>
        public Document()
        {
            this.No = String.Empty;
            this.OperationalStatus = String.Empty;
            this.ModifiedDate = DateTime.Now;
            InitReferences();
        }

        /// <summary>
        /// شناسه دیتابیسی این موجودیت که به صورت خودکار توسط دیتابیس تولید می شود
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        /// شماره موجودیت عملیاتی مرتبط با این مستند اداری
        /// </summary>
        public virtual string EntityNo { get; set; }

        /// <summary>
        /// شماره یکتای مشخص کننده این مستند اداری در گردش کار
        /// </summary>
        public virtual string No { get; set; }

        /// <summary>
        /// وضعیت عملیاتی مستند اداری
        /// </summary>
        public virtual string OperationalStatus { get; set; }

        /// <summary>
        /// نوع مستند اداری
        /// </summary>
        public virtual DocumentType Type { get; set; }

        /// <summary>
        /// وضعیت ثبتی مستند اداری
        /// </summary>
        public virtual DocumentStatus Status { get; set; }

        /// <summary>
        /// مجموعه ای از اقدامات انجام شده روی مستند اداری
        /// </summary>
        public virtual IList<DocumentAction> Actions { get; protected set; }

        /// <summary>
        /// شناسه یکتای ردیف دیتابیسی که به صورت خودکار توسط دیتابیس مقداردهی می شود
        /// </summary>
        public virtual Guid RowGuid { get; set; }

        /// <summary>
        /// تاریخ آخرین تغییر رکورد دیتابیس که به صورت خودکار توسط ابزار دسترسی به داده مقداردهی می شود
        /// </summary>
        public virtual DateTime ModifiedDate { get; set; }

        private void InitReferences()
        {
            Type = new DocumentType();
            Status = new DocumentStatus();
            Actions = new List<DocumentAction>();
        }
    }
}
