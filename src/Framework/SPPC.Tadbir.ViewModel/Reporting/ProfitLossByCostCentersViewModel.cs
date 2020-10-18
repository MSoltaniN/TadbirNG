using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Reporting
{
    /// <summary>
    /// اطلاعات گزارش سود و زیان را برای مقایسه عملکرد چند مرکز هزینه نگهداری می کند
    /// </summary>
    public class ProfitLossByCostCentersViewModel
    {
        /// <summary>
        /// گروه محاسباتی گزارش - مانند سود ناخالص، هزینه های عملیاتی و غیره
        /// </summary>
        public string Group { get; set; }

        /// <summary>
        /// حساب ویژه مورد استفاده
        /// </summary>
        public string Account { get; set; }

        #region CostCenter1 Columns

        /// <summary>
        /// مانده حساب ویژه برای اولین مرکز هزینه انتخاب شده در تاریخ شروع دوره گزارشگیری
        /// </summary>
        public decimal? StartBalanceCostCenter1 { get; set; }

        /// <summary>
        /// گردش حساب ویژه برای اولین مرکز هزینه انتخاب شده در دوره گزارشگیری
        /// </summary>
        public decimal? PeriodTurnoverCostCenter1 { get; set; }

        /// <summary>
        /// مانده حساب ویژه برای اولین مرکز هزینه انتخاب شده در تاریخ پایان دوره گزارشگیری
        /// </summary>
        public decimal? EndBalanceCostCenter1 { get; set; }

        /// <summary>
        /// مانده حساب ویژه برای اولین مرکز هزینه انتخاب شده در یک تاریخ مشخص
        /// </summary>
        public decimal? BalanceCostCenter1 { get; set; }

        #endregion

        #region CostCenter2 Columns

        /// <summary>
        /// مانده حساب ویژه برای دومین مرکز هزینه انتخاب شده در تاریخ شروع دوره گزارشگیری
        /// </summary>
        public decimal? StartBalanceCostCenter2 { get; set; }

        /// <summary>
        /// گردش حساب ویژه برای دومین مرکز هزینه انتخاب شده در دوره گزارشگیری
        /// </summary>
        public decimal? PeriodTurnoverCostCenter2 { get; set; }

        /// <summary>
        /// مانده حساب ویژه برای دومین مرکز هزینه انتخاب شده در تاریخ پایان دوره گزارشگیری
        /// </summary>
        public decimal? EndBalanceCostCenter2 { get; set; }

        /// <summary>
        /// مانده حساب ویژه برای دومین مرکز هزینه انتخاب شده در یک تاریخ مشخص
        /// </summary>
        public decimal? BalanceCostCenter2 { get; set; }

        #endregion

        #region CostCenter3 Columns

        /// <summary>
        /// مانده حساب ویژه برای سومین مرکز هزینه انتخاب شده در تاریخ شروع دوره گزارشگیری
        /// </summary>
        public decimal? StartBalanceCostCenter3 { get; set; }

        /// <summary>
        /// گردش حساب ویژه برای سومین مرکز هزینه انتخاب شده در دوره گزارشگیری
        /// </summary>
        public decimal? PeriodTurnoverCostCenter3 { get; set; }

        /// <summary>
        /// مانده حساب ویژه برای سومین مرکز هزینه انتخاب شده در تاریخ پایان دوره گزارشگیری
        /// </summary>
        public decimal? EndBalanceCostCenter3 { get; set; }

        /// <summary>
        /// مانده حساب ویژه برای سومین مرکز هزینه انتخاب شده در یک تاریخ مشخص
        /// </summary>
        public decimal? BalanceCostCenter3 { get; set; }

        #endregion

        #region CostCenter4 Columns

        /// <summary>
        /// مانده حساب ویژه برای چهارمین مرکز هزینه انتخاب شده در تاریخ شروع دوره گزارشگیری
        /// </summary>
        public decimal? StartBalanceCostCenter4 { get; set; }

        /// <summary>
        /// گردش حساب ویژه برای چهارمین مرکز هزینه انتخاب شده در دوره گزارشگیری
        /// </summary>
        public decimal? PeriodTurnoverCostCenter4 { get; set; }

        /// <summary>
        /// مانده حساب ویژه برای چهارمین مرکز هزینه انتخاب شده در تاریخ پایان دوره گزارشگیری
        /// </summary>
        public decimal? EndBalanceCostCenter4 { get; set; }

        /// <summary>
        /// مانده حساب ویژه برای چهارمین مرکز هزینه انتخاب شده در یک تاریخ مشخص
        /// </summary>
        public decimal? BalanceCostCenter4 { get; set; }

        #endregion

        #region CostCenter5 Columns

        /// <summary>
        /// مانده حساب ویژه برای پنجمین مرکز هزینه انتخاب شده در تاریخ شروع دوره گزارشگیری
        /// </summary>
        public decimal? StartBalanceCostCenter5 { get; set; }

        /// <summary>
        /// گردش حساب ویژه برای پنجمین مرکز هزینه انتخاب شده در دوره گزارشگیری
        /// </summary>
        public decimal? PeriodTurnoverCostCenter5 { get; set; }

        /// <summary>
        /// مانده حساب ویژه برای پنجمین مرکز هزینه انتخاب شده در تاریخ پایان دوره گزارشگیری
        /// </summary>
        public decimal? EndBalanceCostCenter5 { get; set; }

        /// <summary>
        /// مانده حساب ویژه برای پنجمین مرکز هزینه انتخاب شده در یک تاریخ مشخص
        /// </summary>
        public decimal? BalanceCostCenter5 { get; set; }

        #endregion
    }
}
