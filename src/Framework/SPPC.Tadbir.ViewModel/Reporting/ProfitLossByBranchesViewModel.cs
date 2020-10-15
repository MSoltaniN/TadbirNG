using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Reporting
{
    /// <summary>
    /// اطلاعات گزارش سود و زیان را برای مقایسه عملکرد چند شعبه نگهداری می کند
    /// </summary>
    public class ProfitLossByBranchesViewModel
    {
        /// <summary>
        /// گروه محاسباتی گزارش - مانند سود ناخالص، هزینه های عملیاتی و غیره
        /// </summary>
        public string Group { get; set; }

        /// <summary>
        /// حساب ویژه مورد استفاده
        /// </summary>
        public string Account { get; set; }

        #region Branch1 Columns

        /// <summary>
        /// مانده حساب ویژه برای اولین شعبه انتخاب شده در تاریخ شروع دوره گزارشگیری
        /// </summary>
        public decimal? StartBalanceBranch1 { get; set; }

        /// <summary>
        /// گردش حساب ویژه برای اولین شعبه انتخاب شده در دوره گزارشگیری
        /// </summary>
        public decimal? PeriodTurnoverBranch1 { get; set; }

        /// <summary>
        /// مانده حساب ویژه برای اولین شعبه انتخاب شده در تاریخ پایان دوره گزارشگیری
        /// </summary>
        public decimal? EndBalanceBranch1 { get; set; }

        /// <summary>
        /// مانده حساب ویژه برای اولین شعبه انتخاب شده در یک تاریخ مشخص
        /// </summary>
        public decimal? BalanceBranch1 { get; set; }

        #endregion

        #region Branch2 Columns

        /// <summary>
        /// مانده حساب ویژه برای دومین شعبه انتخاب شده در تاریخ شروع دوره گزارشگیری
        /// </summary>
        public decimal? StartBalanceBranch2 { get; set; }

        /// <summary>
        /// گردش حساب ویژه برای دومین شعبه انتخاب شده در دوره گزارشگیری
        /// </summary>
        public decimal? PeriodTurnoverBranch2 { get; set; }

        /// <summary>
        /// مانده حساب ویژه برای دومین شعبه انتخاب شده در تاریخ پایان دوره گزارشگیری
        /// </summary>
        public decimal? EndBalanceBranch2 { get; set; }

        /// <summary>
        /// مانده حساب ویژه برای دومین شعبه انتخاب شده در یک تاریخ مشخص
        /// </summary>
        public decimal? BalanceBranch2 { get; set; }

        #endregion

        #region Branch3 Columns

        /// <summary>
        /// مانده حساب ویژه برای سومین شعبه انتخاب شده در تاریخ شروع دوره گزارشگیری
        /// </summary>
        public decimal? StartBalanceBranch3 { get; set; }

        /// <summary>
        /// گردش حساب ویژه برای سومین شعبه انتخاب شده در دوره گزارشگیری
        /// </summary>
        public decimal? PeriodTurnoverBranch3 { get; set; }

        /// <summary>
        /// مانده حساب ویژه برای سومین شعبه انتخاب شده در تاریخ پایان دوره گزارشگیری
        /// </summary>
        public decimal? EndBalanceBranch3 { get; set; }

        /// <summary>
        /// مانده حساب ویژه برای سومین شعبه انتخاب شده در یک تاریخ مشخص
        /// </summary>
        public decimal? BalanceBranch3 { get; set; }

        #endregion

        #region Branch4 Columns

        /// <summary>
        /// مانده حساب ویژه برای چهارمین شعبه انتخاب شده در تاریخ شروع دوره گزارشگیری
        /// </summary>
        public decimal? StartBalanceBranch4 { get; set; }

        /// <summary>
        /// گردش حساب ویژه برای چهارمین شعبه انتخاب شده در دوره گزارشگیری
        /// </summary>
        public decimal? PeriodTurnoverBranch4 { get; set; }

        /// <summary>
        /// مانده حساب ویژه برای چهارمین شعبه انتخاب شده در تاریخ پایان دوره گزارشگیری
        /// </summary>
        public decimal? EndBalanceBranch4 { get; set; }

        /// <summary>
        /// مانده حساب ویژه برای چهارمین شعبه انتخاب شده در یک تاریخ مشخص
        /// </summary>
        public decimal? BalanceBranch4 { get; set; }

        #endregion

        #region Branch5 Columns

        /// <summary>
        /// مانده حساب ویژه برای پنجمین شعبه انتخاب شده در تاریخ شروع دوره گزارشگیری
        /// </summary>
        public decimal? StartBalanceBranch5 { get; set; }

        /// <summary>
        /// گردش حساب ویژه برای پنجمین شعبه انتخاب شده در دوره گزارشگیری
        /// </summary>
        public decimal? PeriodTurnoverBranch5 { get; set; }

        /// <summary>
        /// مانده حساب ویژه برای پنجمین شعبه انتخاب شده در تاریخ پایان دوره گزارشگیری
        /// </summary>
        public decimal? EndBalanceBranch5 { get; set; }

        /// <summary>
        /// مانده حساب ویژه برای پنجمین شعبه انتخاب شده در یک تاریخ مشخص
        /// </summary>
        public decimal? BalanceBranch5 { get; set; }

        #endregion
    }
}
