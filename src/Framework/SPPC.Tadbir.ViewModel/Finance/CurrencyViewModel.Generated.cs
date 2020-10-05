// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.655
//     Template Version: 1.0
//     Generation Date: 04/22/1398 03:58:02 ب.ظ
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
    /// Represents a local or international currency used for registering monetary transactions
    /// </summary>
    public partial class CurrencyViewModel
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public CurrencyViewModel()
        {
            Name = String.Empty;
            Code = String.Empty;
            MinorUnit = String.Empty;
            Description = String.Empty;
            IsActive = true;
        }

        /// <summary>
        /// شناسه دیتابیسی این موجودیت که به صورت خودکار توسط دیتابیس تولید می شود
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// محدوده دسترسی به ارز را در سطح شعبه های موجود در سازمان مشخص می کند. مقادیر مجاز شامل
        /// "کلیه شعبه ها" (مقدار 0)، "شعبه جاری و زیرمجموعه ها" (مقدار 1) و "شعبه جاری" (مقدار 2) می شود.
        /// </summary>
        public short BranchScope { get; set; }

        /// <summary>
        /// کلید متن چند زبانه برای نام ارز استاندارد
        /// </summary>
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        [StringLength(64, MinimumLength = 0, ErrorMessage = ValidationMessages.TextFieldHasLengthRange)]
        public string Name { get; set; }

        /// <summary>
        /// نمایه استاندارد بین المللی ارز، که معمولاً سه حرفی است
        /// </summary>
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        [StringLength(8, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string Code { get; set; }

        /// <summary>
        /// کد ارز مرتبط مالیاتی
        /// </summary>
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        public virtual int TaxCode { get; set; }

        /// <summary>
        /// نام ارز جزء مورد استفاده، که برای ارزهای خارجی معمولاً سنت است
        /// </summary>
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        [StringLength(16, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string MinorUnit { get; set; }

        /// <summary>
        /// تعداد ارقام اعشار مورد نیاز برای ارز
        /// </summary>
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        public short DecimalCount { get; set; }

        /// <summary>
        /// مشخص می کند که آیا ارز مورد نظر در برنامه فعال است یا نه؟
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// شرح تکمیلی برای نگهداری جزئیات بیشتر در مورد ارز
        /// </summary>
        [StringLength(512, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string Description { get; set; }

        /// <summary>
        /// آیا این ارز، ارز پایه میباشد؟
        /// </summary>
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        public virtual bool IsDefaultCurrency { get; set; }

    }
}
