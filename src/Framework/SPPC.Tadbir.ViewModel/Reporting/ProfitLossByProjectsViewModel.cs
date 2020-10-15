using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Reporting
{
    /// <summary>
    /// اطلاعات گزارش سود و زیان را برای مقایسه عملکرد چند پروژه نگهداری می کند
    /// </summary>
    public class ProfitLossByProjectsViewModel
    {
        /// <summary>
        /// گروه محاسباتی گزارش - مانند سود ناخالص، هزینه های عملیاتی و غیره
        /// </summary>
        public string Group { get; set; }

        /// <summary>
        /// حساب ویژه مورد استفاده
        /// </summary>
        public string Account { get; set; }

        #region Project1 Columns

        /// <summary>
        /// مانده حساب ویژه برای اولین پروژه انتخاب شده در تاریخ شروع دوره گزارشگیری
        /// </summary>
        public decimal? StartBalanceProject1 { get; set; }

        /// <summary>
        /// گردش حساب ویژه برای اولین پروژه انتخاب شده در دوره گزارشگیری
        /// </summary>
        public decimal? PeriodTurnoverProject1 { get; set; }

        /// <summary>
        /// مانده حساب ویژه برای اولین پروژه انتخاب شده در تاریخ پایان دوره گزارشگیری
        /// </summary>
        public decimal? EndBalanceProject1 { get; set; }

        /// <summary>
        /// مانده حساب ویژه برای اولین پروژه انتخاب شده در یک تاریخ مشخص
        /// </summary>
        public decimal? BalanceProject1 { get; set; }

        #endregion

        #region Project2 Columns

        /// <summary>
        /// مانده حساب ویژه برای دومین پروژه انتخاب شده در تاریخ شروع دوره گزارشگیری
        /// </summary>
        public decimal? StartBalanceProject2 { get; set; }

        /// <summary>
        /// گردش حساب ویژه برای دومین پروژه انتخاب شده در دوره گزارشگیری
        /// </summary>
        public decimal? PeriodTurnoverProject2 { get; set; }

        /// <summary>
        /// مانده حساب ویژه برای دومین پروژه انتخاب شده در تاریخ پایان دوره گزارشگیری
        /// </summary>
        public decimal? EndBalanceProject2 { get; set; }

        /// <summary>
        /// مانده حساب ویژه برای دومین پروژه انتخاب شده در یک تاریخ مشخص
        /// </summary>
        public decimal? BalanceProject2 { get; set; }

        #endregion

        #region Project3 Columns

        /// <summary>
        /// مانده حساب ویژه برای سومین پروژه انتخاب شده در تاریخ شروع دوره گزارشگیری
        /// </summary>
        public decimal? StartBalanceProject3 { get; set; }

        /// <summary>
        /// گردش حساب ویژه برای سومین پروژه انتخاب شده در دوره گزارشگیری
        /// </summary>
        public decimal? PeriodTurnoverProject3 { get; set; }

        /// <summary>
        /// مانده حساب ویژه برای سومین پروژه انتخاب شده در تاریخ پایان دوره گزارشگیری
        /// </summary>
        public decimal? EndBalanceProject3 { get; set; }

        /// <summary>
        /// مانده حساب ویژه برای سومین پروژه انتخاب شده در یک تاریخ مشخص
        /// </summary>
        public decimal? BalanceProject3 { get; set; }

        #endregion

        #region Project4 Columns

        /// <summary>
        /// مانده حساب ویژه برای چهارمین پروژه انتخاب شده در تاریخ شروع دوره گزارشگیری
        /// </summary>
        public decimal? StartBalanceProject4 { get; set; }

        /// <summary>
        /// گردش حساب ویژه برای چهارمین پروژه انتخاب شده در دوره گزارشگیری
        /// </summary>
        public decimal? PeriodTurnoverProject4 { get; set; }

        /// <summary>
        /// مانده حساب ویژه برای چهارمین پروژه انتخاب شده در تاریخ پایان دوره گزارشگیری
        /// </summary>
        public decimal? EndBalanceProject4 { get; set; }

        /// <summary>
        /// مانده حساب ویژه برای چهارمین پروژه انتخاب شده در یک تاریخ مشخص
        /// </summary>
        public decimal? BalanceProject4 { get; set; }

        #endregion

        #region Project5 Columns

        /// <summary>
        /// مانده حساب ویژه برای پنجمین پروژه انتخاب شده در تاریخ شروع دوره گزارشگیری
        /// </summary>
        public decimal? StartBalanceProject5 { get; set; }

        /// <summary>
        /// گردش حساب ویژه برای پنجمین پروژه انتخاب شده در دوره گزارشگیری
        /// </summary>
        public decimal? PeriodTurnoverProject5 { get; set; }

        /// <summary>
        /// مانده حساب ویژه برای پنجمین پروژه انتخاب شده در تاریخ پایان دوره گزارشگیری
        /// </summary>
        public decimal? EndBalanceProject5 { get; set; }

        /// <summary>
        /// مانده حساب ویژه برای پنجمین پروژه انتخاب شده در یک تاریخ مشخص
        /// </summary>
        public decimal? BalanceProject5 { get; set; }

        #endregion
    }
}
