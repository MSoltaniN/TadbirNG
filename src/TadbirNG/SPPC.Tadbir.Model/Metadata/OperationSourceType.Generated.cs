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

namespace SPPC.Tadbir.Model.Metadata
{
    /// <summary>
    /// اطلاعات فراداده ای را برای یکی از انواع عملیات در برنامه نگهداری می کند
    /// </summary>
    public partial class OperationSourceType : CoreEntity
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public OperationSourceType()
        {
            Name = String.Empty;
            ModifiedDate = DateTime.Now;
        }

        /// <summary>
        /// کلید متن چندزبانه برای نام یکی از انواع فرم عملیاتی
        /// </summary>
        public virtual string Name { get; set; }
    }
}