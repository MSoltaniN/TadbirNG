// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.2.1587
//     Template Version: 1.0
//     Generation Date: 9/19/2023 2:05:44 PM
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
    /// اطلاعات یک برند را نگهداری می کند
    /// </summary>
    public partial class BrandViewModel : ViewModelBase
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public BrandViewModel()
        {
            Name = String.Empty;
            EnName = String.Empty;
            Description = String.Empty;
            SocialLink = String.Empty;
            Website = String.Empty;
            MetaKeyword = String.Empty;
        }
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        [StringLength(64, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string Name { get; set; }
        [StringLength(64, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string EnName { get; set; }
        [StringLength(1024, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string Description { get; set; }
        [StringLength(64, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string SocialLink { get; set; }
        [StringLength(64, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string Website { get; set; }
        [StringLength(64, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string MetaKeyword { get; set; }
        public bool IsActive { get; set; }
    }
}
