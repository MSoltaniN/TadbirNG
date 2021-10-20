using System;
using System.Collections.Generic;
using SPPC.Framework.Helpers;

namespace SPPC.Tadbir.Service
{
    /// <summary>
    /// عملیات مورد نیاز برای خواندن مجموعه ای از اطلاعات موجود به صورت کد و نام را تعریف می کند.
    /// </summary>
    public interface ILookupService
    {
        /// <summary>
        /// حساب های موجود در محیط جاری را به صورت مجموعه ای از کد و نام خوانده و برمی گرداند
        /// </summary>
        /// <returns>مجموعه حساب های محیط جاری</returns>
        IEnumerable<KeyValue> LookupAccounts();

        /// <summary>
        /// تفصیلی های شناور موجود در محیط جاری را به صورت مجموعه ای از کد و نام خوانده و برمی گرداند
        /// </summary>
        /// <returns>مجموعه تفصیلی های شناور محیط جاری</returns>
        IEnumerable<KeyValue> LookupDetailAccounts();

        /// <summary>
        /// مراکز هزینه موجود در محیط جاری را به صورت مجموعه ای از کد و نام خوانده و برمی گرداند
        /// </summary>
        /// <returns>مجموعه مراکز هزینه محیط جاری</returns>
        IEnumerable<KeyValue> LookupCostCenters();

        /// <summary>
        /// پروژه های موجود در محیط جاری را به صورت مجموعه ای از کد و نام خوانده و برمی گرداند
        /// </summary>
        /// <returns>مجموعه پروژه های محیط جاری</returns>
        IEnumerable<KeyValue> LookupProjects();

        /// <summary>
        /// دوره های مالی موجود در یک شرکت را به صورت مجموعه ای از کد و نام خوانده و برمی گرداند
        /// </summary>
        /// <param name="companyId">شناسه دیتابیسی یکی از شرکت های موجود</param>
        /// <returns>مجموعه دوره های مالی موجود در یک شرکت</returns>
        IEnumerable<KeyValue> LookupFiscalPeriods(int companyId);

        /// <summary>
        /// ارزهای موجود را به صورت مجموعه ای از کد و نام خوانده و برمی گرداند
        /// </summary>
        /// <returns>مجموعه ارزهای موجود</returns>
        IEnumerable<KeyValue> LookupCurrencies();
    }
}
