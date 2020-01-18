using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Reporting
{
    /// <summary>
    /// اطلاعات گزارش مانده به تفکیک حساب را نگهداری می کند
    /// </summary>
    public class BalanceByAccountViewModel
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public BalanceByAccountViewModel()
        {
            Items = new List<BalanceByAccountItemViewModel>();
            Total = new BalanceByAccountItemViewModel();
        }

        /// <summary>
        /// مجموعه سطرهای محاسبه شده در گزارش
        /// </summary>
        public List<BalanceByAccountItemViewModel> Items { get; }

        /// <summary>
        /// سطر پایانی گزارش شامل مقادیر جمع کل محاسبه شده برای ستون های گزارش
        /// </summary>
        public BalanceByAccountItemViewModel Total { get; set; }

        /// <summary>
        /// سطرهای اطلاعاتی فعلی را با سطرهای داده شده جایگزین می کند
        /// </summary>
        /// <param name="items">مجموعه سطرهای اطلاعاتی جدید برای گزارش</param>
        public void SetItems(IEnumerable<BalanceByAccountItemViewModel> items)
        {
            Items.Clear();
            Items.AddRange(items);
        }
    }
}
