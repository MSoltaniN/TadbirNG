using System;

namespace SPPC.Tadbir.ViewModel.Reporting
{
    /// <summary>
    /// اطلاعات مورد نیاز برای گزارش دفتر عملیاتی ارز
    /// </summary>
    public class CurrencyBookParamViewModel
    {
        /// <summary>
        /// گزارش به تفکیک شعبه است یا خیر؟
        /// </summary>
        public bool ByBranch { get; set; }

        /// <summary>
        /// تاریخ شروع گزارش
        /// </summary>
        public DateTime From { get; set; }

        /// <summary>
        /// تاریخ پایان گزارش
        /// </summary>
        public DateTime To { get; set; }

        /// <summary>
        /// شناسه دیتابیسی حساب انتخاب شده برای گزارش گیری
        /// </summary>
        public int? AccountId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی تفصیلی شناور انتخاب شده برای گزارش گیری
        /// </summary>
        public int? FAccountId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی مرکز هزینه انتخاب شده برای گزارش گیری
        /// </summary>
        public int? CCenterId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی پروژه انتخاب شده برای گزارش گیری
        /// </summary>
        public int? ProjectId { get; set; }

        /// <summary>
        /// در حالت کلیه ارزها سطرهای بدون ارز هم آورده شود یا خیر؟
        /// </summary>
        public bool CurrFree { get; set; } = false;
    }
}
