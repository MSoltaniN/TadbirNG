using System;
using System.Collections.Generic;
using SPPC.Tadbir.ViewModel.Core;

namespace SPPC.Tadbir.ViewModel.Procurement
{
    /// <summary>
    /// اطلاعات نمایشی خلاصه برای یک سطر درخواست کالا را نشان می دهد
    /// </summary>
    public class VoucherLineSummaryViewModel
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public VoucherLineSummaryViewModel()
        {
        }

        /// <summary>
        /// شناسه دیتابیسی این موجودیت
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// شماره ردیف سطر درخواست کالا
        /// </summary>
        public int No { get; set; }

        /// <summary>
        /// نام انبار مورد استفاده در این سطر درخواست کالا
        /// </summary>
        public string WarehouseName { get; set; }

        /// <summary>
        /// نام کالای مورد استفاده در این سطر درخواست کالا
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// نام واحد اندازه گیری مورد استفاده در این سطر درخواست کالا
        /// </summary>
        public string UomName { get; set; }

        /// <summary>
        /// تعداد کالای درخواستی
        /// </summary>
        public double OrderedQuantity { get; set; }

        /// <summary>
        /// تاریخ نیاز اعلام شده برای کالای مورد درخواست در این سطر
        /// </summary>
        public string RequiredDate { get; set; }

        /// <summary>
        /// ملاحظات مربوط به این سطر درخواست کالا
        /// </summary>
        public string Description { get; set; }
    }
}
