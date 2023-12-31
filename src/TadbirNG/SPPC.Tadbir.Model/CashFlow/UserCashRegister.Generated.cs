// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.2.1482
//     Template Version: 1.0
//     Generation Date: 03/12/1401 07:11:31 ب.ظ
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Model.CashFlow
{
    /// <summary>
    /// اطلاعات کاربران تخصیص یافته به صندوق را نگهداری می کند
    /// </summary>
    public partial class UserCashRegister : CoreEntity
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public UserCashRegister()
        {
            ModifiedDate = DateTime.Now;
        }

        /// <summary>
        /// شناسه دیتابیسی کاربر تخصیص یافته به صندوق
        /// </summary>
        public virtual int UserId { get; set; }

        /// <summary>
        /// صندوقی که کاربر به آن تخصیص داده شده
        /// </summary>
        public virtual CashRegister CashRegister { get; set; }
    }
}
