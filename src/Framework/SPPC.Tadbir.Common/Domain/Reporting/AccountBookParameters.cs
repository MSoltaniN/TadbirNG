using System;
using SPPC.Framework.Presentation;

namespace SPPC.Tadbir.Domain
{
    /// <summary>
    /// اطلاعات پارامترهای مورد نیاز در گزارش دفتر حساب را نگهداری می کند
    /// </summary>
    public class AccountBookParameters
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public AccountBookParameters()
        {
        }

        /// <summary>
        /// نوع نمایش انتخاب شده برای گزارش
        /// </summary>
        public AccountBookMode Mode { get; set; }

        /// <summary>
        /// شناسه مولفه حساب انتخاب شده برای دفترگیری
        /// </summary>
        public int ViewId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی حساب انتخاب شده برای دفترگیری
        /// </summary>
        public int ItemId { get; set; }

        /// <summary>
        /// تاریخ ابتدای دوره گزارشگیری
        /// </summary>
        public DateTime FromDate { get; set; }

        /// <summary>
        /// تاریخ انتهای دوره گزارشگیری
        /// </summary>
        public DateTime ToDate { get; set; }

        /// <summary>
        /// مشخص می کند که آیا تفکیک شعبه مورد نیاز است یا نه؟
        /// </summary>
        public bool IsByBranch { get; set; }

        /// <summary>
        /// گزینه های برنامه برای فیلتر، مرتب سازی و صفحه بندی اطلاعات
        /// </summary>
        public GridOptions GridOptions { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public AccountBookParameters GetCopy()
        {
            return (AccountBookParameters)MemberwiseClone();
        }
    }
}
