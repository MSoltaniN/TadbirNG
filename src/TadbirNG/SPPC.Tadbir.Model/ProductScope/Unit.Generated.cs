// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.2.1587
//     Template Version: 1.0
//     Generation Date: 09/26/2023 4:41:04 PM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SPPC.Tadbir.Model.ProductScope
{
    /// <summary>
    /// اطلاعات یک واحد را نگهدارری می کند
    /// </summary>
    [Table("Unit", Schema = "ProductScope")]
    public partial class Unit : BaseEntity
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public Unit()
        {
            Name = String.Empty;
            EnName = String.Empty;
            Description = String.Empty;
            Symbol = String.Empty;
        }

        /// <summary>
        /// نام واحد
        /// </summary>
        [MaxLength(64)]
        public virtual string Name { get; set; }

        /// <summary>
        /// نام لاتین واحد
        /// </summary>
        [MaxLength(64)]
        public virtual string? EnName { get; set; }

        /// <summary>
        /// شرح تکمیلی واحد
        /// </summary>
        [MaxLength(1024)]
        public virtual string? Description { get; set; }

        /// <summary>
        /// آدرس تصویر نماد واحد
        /// </summary>
        [MaxLength(1024)]
        public virtual string? Symbol { get; set; }

        /// <summary>
        /// وضعیت
        /// </summary>
        public virtual short? Status { get; set; }

        /// <summary>
        /// وضعیت فعال یا غیرفعال
        /// </summary>
        public virtual bool? IsActive { get; set; }
    }
}
