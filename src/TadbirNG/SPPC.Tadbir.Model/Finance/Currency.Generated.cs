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
using System.Collections.Generic;

namespace SPPC.Tadbir.Model.Finance
{
    /// <summary>
    /// اطلاعات یک ارز محلی یا بین المللی را برای استفاده در تراکنش های ارزی نگهداری می کند
    /// </summary>
    public partial class Currency : BaseEntity
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public Currency()
        {
            Name = String.Empty;
            Code = String.Empty;
            MinorUnit = String.Empty;
            Description = String.Empty;
            ModifiedDate = DateTime.Now;
            Rates = new List<CurrencyRate>();
        }

        /// <summary>
        /// کلید متن چند زبانه برای نام ارز استاندارد
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// نمایه استاندارد بین المللی ارز، که معمولاً سه حرفی است
        /// </summary>
        public virtual string Code { get; set; }

        /// <summary>
        /// کد ارز مرتبط مالیاتی
        /// </summary>
        public virtual int TaxCode { get; set; }

        /// <summary>
        /// کلید متن چند زبانه برای نام ارز جزء مورد استفاده، که برای ارزهای خارجی معمولاً سنت است
        /// </summary>
        public virtual string MinorUnit { get; set; }

        /// <summary>
        /// تعداد ارقام اعشار مورد نیاز برای ارز
        /// </summary>
        public virtual short DecimalCount { get; set; }

        /// <summary>
        /// شرح تکمیلی برای نگهداری جزئیات بیشتر در مورد ارز
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// مشخص می کند که این ارز به عنوان ارز پایه شرکت انتخاب شده یا نه
        /// </summary>
        public virtual bool IsDefaultCurrency { get; set; }
    }
}
