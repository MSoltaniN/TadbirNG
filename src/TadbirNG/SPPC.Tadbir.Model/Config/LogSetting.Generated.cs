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
using SPPC.Tadbir.Model.Metadata;

namespace SPPC.Tadbir.Model.Config
{
    /// <summary>
    /// اطلاعات تنظیمات لاگ را برای یک عملیات یک موجودیت یا یک فرم عملیاتی نگهداری می کند
    /// </summary>
    public partial class LogSetting : PCoreEntity
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public LogSetting()
        {
            ModifiedDate = DateTime.Now;
        }

        /// <summary>
        /// مشخص می کند که آیا لاگ عملیاتی برای عملیات مورد نظر باید ایجاد شود یا نه؟
        /// </summary>
        public virtual bool IsEnabled { get; set; }

        /// <summary>
        /// اطلاعات فراداده ای برای زیرسیستم مورد استفاده در عملیات
        /// </summary>
        public virtual Subsystem Subsystem { get; set; }

        /// <summary>
        /// اطلاعات فراداده ای نوع فرم عملیاتی مورد استفاده
        /// </summary>
        public virtual OperationSourceType SourceType { get; set; }

        /// <summary>
        /// اطلاعات فراداده ای فرم عملیاتی مورد استفاده
        /// </summary>
        public virtual OperationSource Source { get; set; }

        /// <summary>
        /// اطلاعات فراداده ای موجودیت مورد استفاده در عملیات
        /// </summary>
        public virtual EntityType EntityType { get; set; }

        /// <summary>
        /// اطلاعات فراداده ای عملیات انجام شده
        /// </summary>
        public virtual Operation Operation { get; set; }
    }
}
