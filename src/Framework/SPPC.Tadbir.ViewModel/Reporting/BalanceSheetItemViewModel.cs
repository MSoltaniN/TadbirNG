using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Reporting
{
    /// <summary>
    /// اطلاعات یکی از سطرهای گزارش ترازنامه را نگهداری می کند
    /// </summary>
    public class BalanceSheetItemViewModel
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public BalanceSheetItemViewModel()
        {
        }

        /// <summary>
        /// نام حساب دارایی یا عنوان کلی در گزارش
        /// </summary>
        public string Assets { get; set; }

        /// <summary>
        /// مانده یکی از حساب های دارایی یا جمع مقادیر مانده
        /// </summary>
        public decimal? AssetsBalance { get; set; }

        /// <summary>
        /// مانده یکی از حساب های دارایی یا جمع مقادیر مانده در دوره مالی قبل
        /// </summary>
        public decimal? AssetsPreviousBalance { get; set; }

        /// <summary>
        /// نام حساب بدهی یا عنوان کلی در گزارش
        /// </summary>
        public string Liabilities { get; set; }

        /// <summary>
        /// مانده یکی از حساب های بدهی یا جمع مقادیر مانده
        /// </summary>
        public decimal? LiabilitiesBalance { get; set; }

        /// <summary>
        /// مانده یکی از حساب های بدهی یا جمع مقادیر مانده در دوره مالی قبل
        /// </summary>
        public decimal? LiabilitiesPreviousBalance { get; set; }
    }
}
