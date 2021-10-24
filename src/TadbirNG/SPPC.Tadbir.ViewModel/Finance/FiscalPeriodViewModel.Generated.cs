// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.898
//     Template Version: 1.0
//     Generation Date: 2020-05-19 3:25:43 PM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;
using SPPC.Framework.Values;
using SPPC.Tadbir.Domain;

namespace SPPC.Tadbir.ViewModel.Finance
{
    /// <summary>
    /// Represents a date period used for partitioning financial events of a business unit
    /// </summary>
    public partial class FiscalPeriodViewModel
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public FiscalPeriodViewModel()
        {
            Name = String.Empty;
            Description = String.Empty;
            InventoryMode = (int)Domain.InventoryMode.Perpetual;
        }

        /// <summary>
        /// شناسه دیتابیسی این موجودیت که به صورت خودکار توسط دیتابیس تولید می شود
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of this fiscal period
        /// </summary>
        [Display(Name = FieldNames.NameField)]
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        [StringLength(64, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string Name { get; set; }

        /// <summary>
        /// Date when business activities of this period starts
        /// </summary>
        [Display(Name = FieldNames.StartDateField)]
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Date when business activities of this period ends
        /// </summary>
        [Display(Name = FieldNames.EndDateField)]
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        public DateTime EndDate { get; set; }

        /// <summary>
        /// نوع سیستم ثبت موجودی که می تواند دائمی (با مقدار 1) یا ادواری (با مقدار 0) باشد
        /// </summary>
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        public int InventoryMode { get; set; }

        /// <summary>
        /// Detail information related to this fiscal period
        /// </summary>
        [Display(Name = FieldNames.DescriptionField)]
        [StringLength(512, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string Description { get; set; }
    }
}