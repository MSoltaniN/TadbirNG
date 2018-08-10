using System;
using System.Collections.Generic;
using SPPC.Tadbir.Domain;

namespace SPPC.Tadbir.Model
{
    /// <summary>
    /// مشخصات مورد نیاز برای یک موجودیت عملیاتی را نگهداری می کند
    /// </summary>
    public class OperationalEntity : FiscalEntity, IOperationalEntity
    {
        /// <summary>
        /// شماره سند عملیاتی که می تواند شامل اعداد و حروف باشد
        /// </summary>
        public string No { get; set; }

        /// <summary>
        /// تاریخ وقوع عملیات سازمانی مربوط به این سند عملیاتی
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// رفرنس سند عملیاتی که می تواند به عنوان مرجع بین اسناد عملیاتی مرتبط مورد استفاده قرار گیرد
        /// </summary>
        public string Reference { get; set; }
    }
}
