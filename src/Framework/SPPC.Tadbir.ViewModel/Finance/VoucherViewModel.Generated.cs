// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.431
//     Template Version: 1.0
//     Generation Date: 2018-10-21 11:38:30 AM
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
    /// سند مالی که اطلاعات پولی مرتبط با یک پیشامد مالی را در سازمان نگهداری می کند
    /// </summary>
    public partial class VoucherViewModel
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public VoucherViewModel()
        {
            No = String.Empty;
            Date = DateTime.Now;
            Reference = String.Empty;
            Description = String.Empty;
        }

        /// <summary>
        /// شناسه دیتابیسی این موجودیت که به صورت خودکار توسط دیتابیس تولید می شود
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
        public DateTime Date { get; set; }

        /// <summary>
        /// رفرنس سند عملیاتی که می تواند به عنوان مرجع بین اسناد عملیاتی مرتبط مورد استفاده قرار گیرد
        /// </summary>
        [StringLength(64, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string Reference { get; set; }

        /// <summary>
        /// شرح سند مالی که جزئیات بیشتری را در مورد پیشامد مالی ارائه می دهد
        /// </summary>
        [Display(Name = FieldNames.DescriptionField)]
        [StringLength(512, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string Description { get; set; }
    }
}
