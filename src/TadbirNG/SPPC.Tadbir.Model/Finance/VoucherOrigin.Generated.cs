// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.454
//     Template Version: 1.0
//     Generation Date: 2018-11-30 8:12:06 PM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Model.Finance
{
    /// <summary>
    /// اطلاعات یکی از ماخذهای تعریف شده برای یک سند مالی را نگهداری می کند
    /// </summary>
    public partial class VoucherOrigin : CoreEntity
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public VoucherOrigin()
        {
            Name = String.Empty;
            ModifiedDate = DateTime.Now;
        }

        /// <summary>
        /// نام ماخذ ایجاد سند مالی
        /// </summary>
        public virtual string Name { get; set; }
    }
}
