// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.449
//     Template Version: 1.0
//     Generation Date: 2018-11-28 12:34:23 PM
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
    /// اطلاعات یکی از گروه های حساب را نگهداری می کند
    /// </summary>
    public partial class AccountGroupViewModel : ViewModelBase
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public AccountGroupViewModel()
        {
            Name = String.Empty;
            Category = String.Empty;
            Description = String.Empty;
        }

        /// <summary>
        /// نام یا عنوان نمایشی گروه حساب به زبان پیش فرض برنامه
        /// </summary>
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        [StringLength(64, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string Name { get; set; }

        /// <summary>
        /// ماهیت گروه با توجه به مورد استفاده در گزارش های مالی 
        /// </summary>
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        [StringLength(64, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string Category { get; set; }

        /// <summary>
        /// شرح یا ملاحظات گروه حساب به زبان پیش فرض برنامه
        /// </summary>
        [StringLength(256, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string Description { get; set; }
    }
}
