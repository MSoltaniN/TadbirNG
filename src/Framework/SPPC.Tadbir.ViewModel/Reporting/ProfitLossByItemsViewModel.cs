using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Reporting
{
    /// <summary>
    /// اطلاعات یکی از ردیف های گزارش سود و زیان مقایسه ای را نگهداری می کند
    /// </summary>
    public class ProfitLossByItemsViewModel
    {
        /// <summary>
        /// گروه محاسباتی گزارش - مانند سود ناخالص، هزینه های عملیاتی و غیره
        /// </summary>
        public string Group { get; set; }

        /// <summary>
        /// حساب ویژه مورد استفاده
        /// </summary>
        public string Account { get; set; }

        #region Item1 Columns

        /// <summary>
        /// مانده حساب ویژه برای اولین مقدار مقایسه ای انتخاب شده در تاریخ شروع دوره گزارشگیری
        /// </summary>
        public decimal? StartBalanceItem1 { get; set; }

        /// <summary>
        /// گردش حساب ویژه برای اولین مقدار مقایسه ای انتخاب شده در دوره گزارشگیری
        /// </summary>
        public decimal? PeriodTurnoverItem1 { get; set; }

        /// <summary>
        /// مانده حساب ویژه برای اولین مقدار مقایسه ای انتخاب شده در تاریخ پایان دوره گزارشگیری
        /// </summary>
        public decimal? EndBalanceItem1 { get; set; }

        /// <summary>
        /// مانده حساب ویژه برای اولین مقدار مقایسه ای انتخاب شده در یک تاریخ مشخص
        /// </summary>
        public decimal? BalanceItem1 { get; set; }

        #endregion

        #region Item2 Columns

        /// <summary>
        /// مانده حساب ویژه برای دومین مقدار مقایسه ای انتخاب شده در تاریخ شروع دوره گزارشگیری
        /// </summary>
        public decimal? StartBalanceItem2 { get; set; }

        /// <summary>
        /// گردش حساب ویژه برای دومین مقدار مقایسه ای انتخاب شده در دوره گزارشگیری
        /// </summary>
        public decimal? PeriodTurnoverItem2 { get; set; }

        /// <summary>
        /// مانده حساب ویژه برای دومین مقدار مقایسه ای انتخاب شده در تاریخ پایان دوره گزارشگیری
        /// </summary>
        public decimal? EndBalanceItem2 { get; set; }

        /// <summary>
        /// مانده حساب ویژه برای دومین مقدار مقایسه ای انتخاب شده در یک تاریخ مشخص
        /// </summary>
        public decimal? BalanceItem2 { get; set; }

        #endregion

        #region Item3 Columns

        /// <summary>
        /// مانده حساب ویژه برای سومین مقدار مقایسه ای انتخاب شده در تاریخ شروع دوره گزارشگیری
        /// </summary>
        public decimal? StartBalanceItem3 { get; set; }

        /// <summary>
        /// گردش حساب ویژه برای سومین مقدار مقایسه ای انتخاب شده در دوره گزارشگیری
        /// </summary>
        public decimal? PeriodTurnoverItem3 { get; set; }

        /// <summary>
        /// مانده حساب ویژه برای سومین مقدار مقایسه ای انتخاب شده در تاریخ پایان دوره گزارشگیری
        /// </summary>
        public decimal? EndBalanceItem3 { get; set; }

        /// <summary>
        /// مانده حساب ویژه برای سومین مقدار مقایسه ای انتخاب شده در یک تاریخ مشخص
        /// </summary>
        public decimal? BalanceItem3 { get; set; }

        #endregion

        #region Item4 Columns

        /// <summary>
        /// مانده حساب ویژه برای چهارمین مقدار مقایسه ای انتخاب شده در تاریخ شروع دوره گزارشگیری
        /// </summary>
        public decimal? StartBalanceItem4 { get; set; }

        /// <summary>
        /// گردش حساب ویژه برای چهارمین مقدار مقایسه ای انتخاب شده در دوره گزارشگیری
        /// </summary>
        public decimal? PeriodTurnoverItem4 { get; set; }

        /// <summary>
        /// مانده حساب ویژه برای چهارمین مقدار مقایسه ای انتخاب شده در تاریخ پایان دوره گزارشگیری
        /// </summary>
        public decimal? EndBalanceItem4 { get; set; }

        /// <summary>
        /// مانده حساب ویژه برای چهارمین مقدار مقایسه ای انتخاب شده در یک تاریخ مشخص
        /// </summary>
        public decimal? BalanceItem4 { get; set; }

        #endregion

        #region Item5 Columns

        /// <summary>
        /// مانده حساب ویژه برای پنجمین مقدار مقایسه ای انتخاب شده در تاریخ شروع دوره گزارشگیری
        /// </summary>
        public decimal? StartBalanceItem5 { get; set; }

        /// <summary>
        /// گردش حساب ویژه برای پنجمین مقدار مقایسه ای انتخاب شده در دوره گزارشگیری
        /// </summary>
        public decimal? PeriodTurnoverItem5 { get; set; }

        /// <summary>
        /// مانده حساب ویژه برای پنجمین مقدار مقایسه ای انتخاب شده در تاریخ پایان دوره گزارشگیری
        /// </summary>
        public decimal? EndBalanceItem5 { get; set; }

        /// <summary>
        /// مانده حساب ویژه برای پنجمین مقدار مقایسه ای انتخاب شده در یک تاریخ مشخص
        /// </summary>
        public decimal? BalanceItem5 { get; set; }

        #endregion
    }
}
