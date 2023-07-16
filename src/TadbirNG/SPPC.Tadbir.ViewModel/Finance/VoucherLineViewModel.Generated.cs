// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.584
//     Template Version: 1.0
//     Generation Date: 02/30/1398 03:58:27 ب.ظ
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
    /// آرتیکل سند مالی که اطلاعات مربوط به بخشی از یک پیشامد مالی را نگهداری می کند
    /// </summary>
    public partial class VoucherLineViewModel
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public VoucherLineViewModel()
        {
            FullAccount = new FullAccountViewModel();
            Description = String.Empty;
            FollowupNo = String.Empty;            
        }

        /// <summary>
        /// مبلغ بدهکار برای این آرتیکل مالی
        /// </summary>
        [Display(Name = FieldNames.DebitField)]
        public decimal Debit { get; set; }

        /// <summary>
        /// مبلغ بستانکار برای این آرتیکل مالی
        /// </summary>
        [Display(Name = FieldNames.CreditField)]
        public decimal Credit { get; set; }

        /// <summary>
        /// شرح آرتیکل سند مالی برای قرار دادن اطلاعات تکمیلی مرتبط با این بخش
        /// </summary>
        [Display(Name = FieldNames.DescriptionField)]
        [StringLength(1024, ErrorMessage = ValidationMessages.TextFieldHasLengthRange)]
        public string Description { get; set; }

        /// <summary>
        /// مقدار مورد استفاده در عملیات مرتبط با آرتیکل
        /// </summary>
        public decimal? Amount { get; set; }

        /// <summary>
        /// شماره پیگیری مورد استفاده برای پیگیری مغایرت های بانکی
        /// </summary>
        [StringLength(64, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string FollowupNo { get; set; }

        /// <summary>
        /// مبلغ ارز در تراکنش های ارزی
        /// </summary>
        public decimal? CurrencyValue { get; set; }

        /// <summary>
        /// علامتگذاری کاربر روی آرتیکل
        /// </summary>
        public string Mark { get; set; }

        /// <summary>
        /// شناسه نوع آرتیکل (مانند آرتیکل عادی، مالیات و عوارض و ...)
        /// </summary>
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        public short LineTypeId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی منابع و مصارف مرتبط با این آرتیکل
        /// </summary>
        public int? SourceAppId { get; set; }
    }
}
