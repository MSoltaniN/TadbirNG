// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.655
//     Template Version: 1.0
//     Generation Date: 04/22/1398 04:57:02 ب.ظ
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
    /// اطلاعات تکمیلی را برای نرخ روزانه یا لحظه ای یکی از ارزهای تعریف شده نگهداری می کند
    /// </summary>
    public partial class CurrencyRateViewModel
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public CurrencyRateViewModel()
        {
        }

        /// <summary>
        /// شناسه دیتابیسی این موجودیت که به صورت خودکار توسط دیتابیس تولید می شود
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// تاریخ ثبت نرخ روزانه یا لحظه ای ارز
        /// </summary>
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        public DateTime Date { get; set; }

        /// <summary>
        /// زمان ثبت نرخ روزانه یا لحظه ای ارز
        /// </summary>
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        public TimeSpan Time { get; set; }

        /// <summary>
        /// ضریب تبدیل به ارز پایه شرکت - این عدد برابر است با مقدار ارز پایه معادل با یک واحد ارز مرتبط
        /// </summary>
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        public decimal Multiplier { get; set; }
    }
}
