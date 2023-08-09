// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.2.1504
//     Template Version: 1.0
//     Generation Date: 28/01/1402 03:09:10 ب.ظ
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;
using SPPC.Framework.Values;
using SPPC.Tadbir.Resources;

namespace SPPC.Tadbir.ViewModel.CashFlow
{
    /// <summary>
    /// اطلاعات یک منبع یا مصرف را نگهداری می کند.
    /// </summary>
    public partial class SourceAppViewModel : ViewModelBase
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public SourceAppViewModel()
        {
            Code = String.Empty;
            Name = String.Empty;
            Description = String.Empty;
            State = AppStrings.Active;
        }

        /// <summary>
        /// سطح دسترسی شعبه
        /// </summary>
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        public short BranchScope { get; set; }

        /// <summary>
        /// کد منبع یا مصرف
        /// </summary>
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        [StringLength(16, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string Code { get; set; }

        /// <summary>
        /// نام منبع یا مصرف
        /// </summary>
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        [StringLength(256, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string Name { get; set; }

        /// <summary>
        /// شرح منبع یا مصرف
        /// </summary>
        [StringLength(512, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string Description { get; set; }

        /// <summary>
        /// نوع (منابع: صفر ، مصارف: یک) 
        /// </summary>
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        public short Type { get; set; }
    }
}
