// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.2.1596
//     Template Version: 1.0
//     Generation Date: 10/9/2023 4:32:54 PM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;
using SPPC.Framework.Values;

namespace SPPC.Tadbir.ViewModel.ProductScope
{
    /// <summary>
    /// خصوصیت های کالاها را در خود نگهداری میکند
    /// </summary>
    public partial class AttributeViewModel : ViewModelBase
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public AttributeViewModel()
        {
            Name = String.Empty;
            EnName = String.Empty;
            Description = String.Empty;
        }

        /// <summary>
        /// نام فارسی خصوصیت
        /// </summary>
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        [StringLength(64, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string Name { get; set; }

        /// <summary>
        /// نام لاتین خصوصیت
        /// </summary>
        [StringLength(64, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string EnName { get; set; }

        /// <summary>
        /// شرح خصوصیت
        /// </summary>
        [StringLength(1024, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string Description { get; set; }

        /// <summary>
        /// نوع خصوصیت
        /// </summary>
        public short Type { get; set; }

        /// <summary>
        ///  وضعیت فعال یا غیر فعال
        /// </summary>
        public bool IsActive { get; set; }
    }
}
