// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.2.1587
//     Template Version: 1.0
//     Generation Date: 09/26/2023 4:41:05 PM
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
    /// اطلاعات یک واحد را نگهدارری می کند
    /// </summary>
    public partial class UnitViewModel : ViewModelBase
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public UnitViewModel()
        {
            Name = String.Empty;
            EnName = String.Empty;
            Description = String.Empty;
            Symbol = String.Empty;
        }

        /// <summary>
        /// نام واحد
        /// </summary>
        [StringLength(64, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string Name { get; set; }

        /// <summary>
        /// نام لاتین واحد
        /// </summary>
        [StringLength(64, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string EnName { get; set; }

        /// <summary>
        /// شرح تکمیلی واحد
        /// </summary>
        [StringLength(64, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string Description { get; set; }

        /// <summary>
        /// آدرس تصویر نماد واحد
        /// </summary>
        [StringLength(64, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string Symbol { get; set; }

        /// <summary>
        /// وضعیت
        /// </summary>
        public short Status { get; set; }

        /// <summary>
        /// وضعیت فعال یا غیرفعال
        /// </summary>
        public bool IsActive { get; set; }
    }
}