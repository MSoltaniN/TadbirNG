using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Domain
{
    /// <summary>
    /// مشخصات مورد نیاز برای یک موجودیت عملیاتی را تعریف می کند
    /// </summary>
    public interface IOperationalEntity : IFiscalEntity
    {
        /// <summary>
        /// شماره سند عملیاتی که عددی متنی است
        /// </summary>
        string TextNo { get; set; }

        /// <summary>
        /// تاریخ وقوع عملیات سازمانی مربوط به این سند عملیاتی
        /// </summary>
        DateTime Date { get; set; }

        /// <summary>
        /// رفرنس سند عملیاتی که می تواند به عنوان مرجع بین اسناد عملیاتی مرتبط مورد استفاده قرار گیرد
        /// </summary>
        string Reference { get; set; }
    }
}
