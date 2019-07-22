using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel
{
    /// <summary>
    /// اطلاعات جزئی یک ارز معتبر و رسمی را نگهداری می کند
    /// </summary>
    public class CurrencyDetail
    {
        /// <summary>
        /// کلید متن چندزبانه برای نام ارز
        /// </summary>
        public string NameKey { get; set; }

        /// <summary>
        /// نام محلی شده ارز
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// نمایه بین المللی شناخته شده برای ارز
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// کلید متن چندزبانه برای نام ارز جزء
        /// </summary>
        public string MinorUnitKey { get; set; }

        /// <summary>
        /// نام محلی شده ارز جزء
        /// </summary>
        public string MinorUnit { get; set; }

        /// <summary>
        /// تعداد ارقام اعشار برای ارز
        /// </summary>
        public int DecimalCount { get; set; }
    }
}