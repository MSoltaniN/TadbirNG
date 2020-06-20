using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Reporting
{
    public class ProfitLossViewModel
    {
        public ProfitLossViewModel()
        {
            Items = new List<ProfitLossItemViewModel>();
        }

        public List<ProfitLossItemViewModel> Items { get; }

        /// <summary>
        /// سطرهای اطلاعاتی گزارش را با مجوعه سطرهای اطلاعاتی داده شده جایگزین می کند
        /// </summary>
        /// <param name="items">سطرهای اطلاعاتی مورد نظر برای جایگزینی در مدل نمایشی</param>
        public void SetItems(IEnumerable<ProfitLossItemViewModel> items)
        {
            Items.Clear();
            Items.AddRange(items);
        }
    }
}
