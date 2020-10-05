using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Domain
{
    /// <summary>
    /// شناسه نماهای اطلاعاتی اصلی مورد استفاده در برنامه را تعریف می کند
    /// </summary>
    public sealed class ViewId
    {
        private ViewId()
        {
        }

        /// <summary>
        /// نمای اطلاعاتی سرفصل های حسابداری
        /// </summary>
        public const int Account = 1;

        /// <summary>
        /// نمای اطلاعاتی اسناد مالی
        /// </summary>
        public const int Voucher = 2;

        /// <summary>
        /// نمای اطلاعاتی آرتیکل های مالی
        /// </summary>
        public const int VoucherLine = 3;

        /// <summary>
        /// نمای اطلاعاتی کاربران
        /// </summary>
        public const int User = 4;

        /// <summary>
        /// نمای اطلاعاتی نقش های امنیتی
        /// </summary>
        public const int Role = 5;

        /// <summary>
        /// نمای اطلاعاتی تفصیلی های شناور
        /// </summary>
        public const int DetailAccount = 6;

        /// <summary>
        /// نمای اطلاعاتی مراکز هزینه
        /// </summary>
        public const int CostCenter = 7;

        /// <summary>
        /// نمای اطلاعاتی پروژه ها
        /// </summary>
        public const int Project = 8;

        /// <summary>
        /// نمای اطلاعاتی دوره های مالی یک شرکت
        /// </summary>
        public const int FiscalPeriod = 9;

        /// <summary>
        /// نمای اطلاعاتی شعبه های یک شرکت
        /// </summary>
        public const int Branch = 10;

        /// <summary>
        /// نمای اطلاعاتی شرکت ها
        /// </summary>
        public const int Company = 11;

        /// <summary>
        /// نمای اطلاعاتی گروه های حساب
        /// </summary>
        public const int AccountGroup = 12;

        /// <summary>
        /// نمای اطلاعاتی مجموعه حساب ها
        /// </summary>
        public const int AccountCollectionAccount = 14;

        /// <summary>
        /// نمای اطلاعاتی ارزها
        /// </summary>
        public const int Currency = 30;

        /// <summary>
        /// نمای اطلاعاتی نرخ های یک ارز
        /// </summary>
        public const int CurrencyRate = 31;
    }
}
