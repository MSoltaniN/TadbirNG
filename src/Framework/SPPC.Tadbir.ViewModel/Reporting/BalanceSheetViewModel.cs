using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Reporting
{
    /// <summary>
    /// اطلاعات کلی گزارش ترازنامه را نگهداری می کند
    /// </summary>
    public class BalanceSheetViewModel
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public BalanceSheetViewModel()
        {
            Items = new List<BalanceSheetItemViewModel>();
        }

        /// <summary>
        /// مجموعه سطرهای محاسبه شده برای گزارش
        /// </summary>
        public List<BalanceSheetItemViewModel> Items { get; }
    }
}
