using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel
{
    /// <summary>
    /// اطلاعات کلی یک ارز معتبر و رسمی را نگهداری می کند
    /// </summary>
    public class CurrencyInfo
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public CurrencyInfo()
        {
            Currency = new CurrencyDetail();
        }

        /// <summary>
        /// نام کشوری که از ارز استفاده می کند
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// اطلاعات جزئی برای ارز
        /// </summary>
        public CurrencyDetail Currency { get; set; }
    }
}
