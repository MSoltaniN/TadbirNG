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

        /// <summary>
        /// نمونه جدیدی با مقادیر این نمونه ایجاد کرده و برمی گرداند
        /// </summary>
        /// <returns>نمونه جدید با مقادیر متناظر این نمونه</returns>
        public BalanceSheetItemViewModel GetCopy()
        {
            return (BalanceSheetItemViewModel)MemberwiseClone();
        }

        /// <summary>
        /// تابع کمکی که عمل جمع یک سطر با این آبجکت را فراهم می کند
        /// </summary>
        /// <param name="right">عبارت سمت راست عملگر جمع</param>
        /// <returns>همین آبجکت را پس از انجام عمل جمع برمی گرداند</returns>
        public BalanceSheetItemViewModel Add(BalanceSheetItemViewModel right)
        {
            AssetsBalance += right.AssetsBalance ?? 0.0M;
            AssetsPreviousBalance += right.AssetsPreviousBalance ?? 0.0M;
            LiabilitiesBalance += right.LiabilitiesBalance ?? 0.0M;
            LiabilitiesPreviousBalance += right.LiabilitiesPreviousBalance ?? 0.0M;
            return this;
        }

        /// <summary>
        /// عملگر جمع را برای سطر گزارش ترازنامه پیاده سازی می کند
        /// </summary>
        /// <param name="left">عبارت سمت چپ عملگر جمع</param>
        /// <param name="right">عبارت سمت راست عملگر جمع</param>
        /// <returns>آبجکت جدیدی با مقادیر جمع شده</returns>
        public static BalanceSheetItemViewModel operator +(
            BalanceSheetItemViewModel left, BalanceSheetItemViewModel right)
        {
            return new BalanceSheetItemViewModel()
            {
                AssetsBalance = (left.AssetsBalance ?? 0.0M) + (right.AssetsBalance ?? 0.0M),
                AssetsPreviousBalance = (left.AssetsPreviousBalance ?? 0.0M)
                    + (right.AssetsPreviousBalance ?? 0.0M),
                LiabilitiesBalance = (left.LiabilitiesBalance ?? 0.0M)
                    + (right.LiabilitiesBalance ?? 0.0M),
                LiabilitiesPreviousBalance = (left.LiabilitiesPreviousBalance ?? 0.0M)
                    + (right.LiabilitiesPreviousBalance ?? 0.0M),
            };
        }
    }
}
