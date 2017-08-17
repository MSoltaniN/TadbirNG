// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.0.0.0
//     Template Version: 1.0
//     Generation Date: 2017-04-27 12:33:04 PM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;
using SPPC.Framework.Values;
using SPPC.Tadbir.Values;

namespace SPPC.Tadbir.ViewModel.Finance
{
    /// <summary>
    /// Represents a financial transaction that provides monetary information for a business event
    /// </summary>
    public partial class TransactionViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionViewModel"/> class.
        /// </summary>
        public TransactionViewModel()
        {
            this.No = String.Empty;
            this.Date = "1390/09/09";
            this.Description = String.Empty;
        }

        /// <summary>
        /// شناسه دیتابیسی این موجودیت
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// شماره سند مالی که می تواند شامل اعداد و حروف باشد
        /// </summary>
        [Display(Name = FieldNames.NumberField)]
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        [StringLength(64, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string No { get; set; }

        /// <summary>
        /// تاریخ وقوع پیشامد مالی در عملیات روزمره کسب و کار
        /// </summary>
        [Display(Name = FieldNames.DateField)]
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        public string Date { get; set; }

        /// <summary>
        /// شرح سند مالی که جزئیات بیشتری را در مورد پیشامد مالی ارائه می دهد
        /// </summary>
        [Display(Name = FieldNames.DescriptionField)]
        [StringLength(512, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string Description { get; set; }
    }
}
