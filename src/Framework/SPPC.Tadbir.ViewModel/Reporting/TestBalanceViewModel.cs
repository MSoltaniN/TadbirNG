using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Reporting
{
    /// <summary>
    /// اطلاعات گزارش تراز آزمایشی را نگهداری می کند
    /// </summary>
    public class TestBalanceViewModel
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public TestBalanceViewModel()
        {
            Items = new List<TestBalanceItemViewModel>();
            Total = new TestBalanceItemViewModel();
        }

        /// <summary>
        /// مجموعه سطرهای محاسبه شده در گزارش
        /// </summary>
        public List<TestBalanceItemViewModel> Items { get; }

        /// <summary>
        /// سطر پایانی گزارش شامل مقادیر جمع کل محاسبه شده برای ستون های گزارش
        /// </summary>
        public TestBalanceItemViewModel Total { get; set; }

        /// <summary>
        /// سطرهای اطلاعاتی فعلی را با سطرهای داده شده جایگزین می کند
        /// </summary>
        /// <param name="items">مجموعه سطرهای اطلاعاتی جدید برای گزارش</param>
        public void SetBalanceItems(IEnumerable<TestBalanceItemViewModel> items)
        {
            Items.Clear();
            Items.AddRange(items);
        }
    }
}
