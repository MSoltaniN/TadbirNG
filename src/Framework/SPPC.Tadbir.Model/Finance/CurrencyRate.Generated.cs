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
using System.Collections.Generic;
using SPPC.Tadbir.Model.Corporate;

namespace SPPC.Tadbir.Model.Finance
{
    /// <summary>
    /// اطلاعات تکمیلی را برای نرخ روزانه یا لحظه ای یکی از ارزهای تعریف شده نگهداری می کند
    /// </summary>
    public partial class CurrencyRate : CoreEntity
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public CurrencyRate()
        {
            Date = DateTime.Now;
            ModifiedDate = DateTime.Now;
        }

        /// <summary>
        /// تاریخ ثبت نرخ روزانه یا لحظه ای ارز
        /// </summary>
        public virtual DateTime Date { get; set; }

        /// <summary>
        /// زمان ثبت نرخ روزانه یا لحظه ای ارز
        /// </summary>
        public virtual TimeSpan Time { get; set; }

        /// <summary>
        /// ضریب تبدیل به ارز پایه شرکت - این عدد برابر است با مقدار ارز پایه معادل با یک واحد ارز مرتبط
        /// </summary>
        public virtual double Multiplier { get; set; }

        /// <summary>
        /// محدوده دسترسی به نرخ ارز را در سطح شعبه های موجود در سازمان مشخص می کند. مقادیر مجاز شامل
        /// "کلیه شعبه ها" (مقدار 0)، "شعبه جاری و زیرمجموعه ها" (مقدار 1) و "شعبه جاری" (مقدار 2) می شود.
        /// </summary>
        public virtual short BranchScope { get; set; }

        /// <summary>
        /// شرح تکمیلی برای نگهداری جزئیات بیشتر در مورد نرخ ارز
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// ارزی که نرخ روزانه یا لحظه ای برای ان ثبت می شود
        /// </summary>
        public virtual Currency Currency { get; set; }

        /// <summary>
        /// شعبه ای که نرخ روزانه ارز برای استفاده در عملیات آن ثبت می شود
        /// </summary>
        public Branch Branch { get; set; }
    }
}
