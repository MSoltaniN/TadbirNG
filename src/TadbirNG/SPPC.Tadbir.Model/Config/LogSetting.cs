// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.785
//     Template Version: 1.0
//     Generation Date: 10/21/1398 03:49:57 ب.ظ
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using SPPC.Tadbir.Model.Metadata;

namespace SPPC.Tadbir.Model.Config
{
    /// <summary>
    /// اطلاعات تنظیمات لاگ را برای یک عملیات یک موجودیت یا یک فرم عملیاتی نگهداری می کند
    /// </summary>
    public partial class LogSetting : PCoreEntity
    {
        /// <summary>
        /// اطلاعات فراداده ای برای زیرسیستم مورد استفاده در عملیات
        /// </summary>
        [Column("SubsystemID")]
        public virtual int SubsystemId { get; set; }

        /// <summary>
        /// اطلاعات فراداده ای نوع فرم عملیاتی مورد استفاده
        /// </summary>
        [Column("SourceTypeID")]
        public virtual int SourceTypeId { get; set; }

        /// <summary>
        /// اطلاعات فراداده ای فرم عملیاتی مورد استفاده
        /// </summary>
        [Column("SourceID")]
        public virtual int? SourceId { get; set; }

        /// <summary>
        /// اطلاعات فراداده ای موجودیت مورد استفاده در عملیات
        /// </summary>
        [Column("EntityTypeID")]
        public virtual int? EntityTypeId { get; set; }

        /// <summary>
        /// اطلاعات فراداده ای عملیات انجام شده
        /// </summary>
        [Column("OperationID")]
        public virtual int OperationId { get; set; }
    }
}