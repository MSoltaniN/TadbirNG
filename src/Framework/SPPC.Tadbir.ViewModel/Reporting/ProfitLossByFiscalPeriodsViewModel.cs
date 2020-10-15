using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Reporting
{
    /// <summary>
    /// اطلاعات گزارش سود و زیان را برای مقایسه عملکرد چند دوره مالی نگهداری می کند
    /// </summary>
    public class ProfitLossByFiscalPeriodsViewModel
    {
        /// <summary>
        /// گروه محاسباتی گزارش - مانند سود ناخالص، هزینه های عملیاتی و غیره
        /// </summary>
        public string Group { get; set; }

        /// <summary>
        /// حساب ویژه مورد استفاده
        /// </summary>
        public string Account { get; set; }

        #region FiscalPeriod1 Columns

        /// <summary>
        /// مانده حساب ویژه برای اولین دوره مالی انتخاب شده در تاریخ شروع دوره گزارشگیری
        /// </summary>
        public decimal? StartBalanceFiscalPeriod1 { get; set; }

        /// <summary>
        /// گردش حساب ویژه برای اولین دوره مالی انتخاب شده در دوره گزارشگیری
        /// </summary>
        public decimal? PeriodTurnoverFiscalPeriod1 { get; set; }

        /// <summary>
        /// مانده حساب ویژه برای اولین دوره مالی انتخاب شده در تاریخ پایان دوره گزارشگیری
        /// </summary>
        public decimal? EndBalanceFiscalPeriod1 { get; set; }

        /// <summary>
        /// مانده حساب ویژه برای اولین دوره مالی انتخاب شده در یک تاریخ مشخص
        /// </summary>
        public decimal? BalanceFiscalPeriod1 { get; set; }

        #endregion

        #region FiscalPeriod2 Columns

        /// <summary>
        /// مانده حساب ویژه برای دومین دوره مالی انتخاب شده در تاریخ شروع دوره گزارشگیری
        /// </summary>
        public decimal? StartBalanceFiscalPeriod2 { get; set; }

        /// <summary>
        /// گردش حساب ویژه برای دومین دوره مالی انتخاب شده در دوره گزارشگیری
        /// </summary>
        public decimal? PeriodTurnoverFiscalPeriod2 { get; set; }

        /// <summary>
        /// مانده حساب ویژه برای دومین دوره مالی انتخاب شده در تاریخ پایان دوره گزارشگیری
        /// </summary>
        public decimal? EndBalanceFiscalPeriod2 { get; set; }

        /// <summary>
        /// مانده حساب ویژه برای دومین دوره مالی انتخاب شده در یک تاریخ مشخص
        /// </summary>
        public decimal? BalanceFiscalPeriod2 { get; set; }

        #endregion

        #region FiscalPeriod3 Columns

        /// <summary>
        /// مانده حساب ویژه برای سومین دوره مالی انتخاب شده در تاریخ شروع دوره گزارشگیری
        /// </summary>
        public decimal? StartBalanceFiscalPeriod3 { get; set; }

        /// <summary>
        /// گردش حساب ویژه برای سومین دوره مالی انتخاب شده در دوره گزارشگیری
        /// </summary>
        public decimal? PeriodTurnoverFiscalPeriod3 { get; set; }

        /// <summary>
        /// مانده حساب ویژه برای سومین دوره مالی انتخاب شده در تاریخ پایان دوره گزارشگیری
        /// </summary>
        public decimal? EndBalanceFiscalPeriod3 { get; set; }

        /// <summary>
        /// مانده حساب ویژه برای سومین دوره مالی انتخاب شده در یک تاریخ مشخص
        /// </summary>
        public decimal? BalanceFiscalPeriod3 { get; set; }

        #endregion

        #region FiscalPeriod4 Columns

        /// <summary>
        /// مانده حساب ویژه برای چهارمین دوره مالی انتخاب شده در تاریخ شروع دوره گزارشگیری
        /// </summary>
        public decimal? StartBalanceFiscalPeriod4 { get; set; }

        /// <summary>
        /// گردش حساب ویژه برای چهارمین دوره مالی انتخاب شده در دوره گزارشگیری
        /// </summary>
        public decimal? PeriodTurnoverFiscalPeriod4 { get; set; }

        /// <summary>
        /// مانده حساب ویژه برای چهارمین دوره مالی انتخاب شده در تاریخ پایان دوره گزارشگیری
        /// </summary>
        public decimal? EndBalanceFiscalPeriod4 { get; set; }

        /// <summary>
        /// مانده حساب ویژه برای چهارمین دوره مالی انتخاب شده در یک تاریخ مشخص
        /// </summary>
        public decimal? BalanceFiscalPeriod4 { get; set; }

        #endregion

        #region FiscalPeriod5 Columns

        /// <summary>
        /// مانده حساب ویژه برای پنجمین دوره مالی انتخاب شده در تاریخ شروع دوره گزارشگیری
        /// </summary>
        public decimal? StartBalanceFiscalPeriod5 { get; set; }

        /// <summary>
        /// گردش حساب ویژه برای پنجمین دوره مالی انتخاب شده در دوره گزارشگیری
        /// </summary>
        public decimal? PeriodTurnoverFiscalPeriod5 { get; set; }

        /// <summary>
        /// مانده حساب ویژه برای پنجمین دوره مالی انتخاب شده در تاریخ پایان دوره گزارشگیری
        /// </summary>
        public decimal? EndBalanceFiscalPeriod5 { get; set; }

        /// <summary>
        /// مانده حساب ویژه برای پنجمین دوره مالی انتخاب شده در یک تاریخ مشخص
        /// </summary>
        public decimal? BalanceFiscalPeriod5 { get; set; }

        #endregion
    }
}
