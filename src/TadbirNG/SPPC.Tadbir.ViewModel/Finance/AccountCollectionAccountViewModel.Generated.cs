// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.492
//     Template Version: 1.0
//     Generation Date: 01/15/2019 03:16:28 ب.ظ
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;
using SPPC.Framework.Values;

namespace SPPC.Tadbir.ViewModel.Finance
{
    /// <summary>
    /// اطلاعات حساب های زیرمجموعه مجموعه حساب را نگهداری میکند
    /// </summary>
    public partial class AccountCollectionAccountViewModel
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public AccountCollectionAccountViewModel()
        {
        }

        /// <summary>
        /// شناسه دیتابیسی این موجودیت که به صورت خودکار توسط دیتابیس تولید می شود
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// مجموعه حساب
        /// </summary>
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        public int CollectionId { get; set; }
    }
}
