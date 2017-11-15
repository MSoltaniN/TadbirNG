﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.0.0.0
//     Template Version: 1.0
//     Generation Date: 2017-08-02 7:42:27 PM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using SPPC.Framework.Domain;

namespace SPPC.Tadbir.Model.Inventory
{
    /// <summary>
    /// یک واحد اندازه گیری کالا را در سیستم کنترل موجودی نشان می دهد
    /// </summary>
    public partial class UnitOfMeasurement : IEntity
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس ایجاد می کند.
        /// </summary>
        public UnitOfMeasurement()
        {
            this.Name = String.Empty;
            this.ModifiedDate = DateTime.Now;
            InitReferences();
        }

        /// <summary>
        /// شناسه دیتابیسی این موجودیت که به صورت خودکار توسط دیتابیس تولید می شود
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        /// نام واحد اندازه گیری
        /// </summary>
        public virtual string Name { get; set; }

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
        }
    }
}
