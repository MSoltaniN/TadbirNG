// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.0.0.0
//     Template Version: 1.0
//     Generation Date: 2018-02-26 2:03:57 PM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using SPPC.Framework.Domain;

namespace SPPC.Tadbir.Model.Metadata
{
    /// <summary>
    /// اطلاعات فراداده ای یک موجودیت در برنامه را نگهداری می کند
    /// </summary>
    public partial class Entity : IEntity
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس ایجاد می کند.
        /// </summary>
        public Entity()
        {
            Name = String.Empty;
            ModifiedDate = DateTime.Now;
            InitReferences();
        }

        /// <summary>
        /// شناسه دیتابیسی این موجودیت که به صورت خودکار توسط دیتابیس تولید می شود
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        /// نام موجودیت به صورت غیر محلی شده - به زبان انگلیسی
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// مشخص می کند که موجودیت ساختار سلسله مراتبی یا درختی دارد یا نه
        /// </summary>
        public virtual bool IsHierarchy { get; set; }

        /// <summary>
        /// مشخص می کند که موجودیت امکان تعامل با کارتابل را دارد یا نه
        /// </summary>
        public virtual bool IsCartableIntegrated { get; set; }

        /// <summary>
        /// شناسه یکتای ردیف دیتابیسی که به صورت خودکار توسط دیتابیس مقداردهی می شود
        /// </summary>
        public virtual Guid RowGuid { get; set; }

        /// <summary>
        /// تاریخ آخرین تغییر رکورد دیتابیس که به صورت خودکار توسط ابزار دسترسی به داده مقداردهی می شود
        /// </summary>
        public virtual DateTime ModifiedDate { get; set; }

        /// <summary>
        /// مجموعه ای از ویژگی های تعریف شده برای موجودیت
        /// </summary>
        public virtual IList<Property> Properties { get; protected set; }

        private void InitReferences()
        {
            Properties = new List<Property>();
        }
    }
}
