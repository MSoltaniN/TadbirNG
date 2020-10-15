using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Reporting
{
    /// <summary>
    /// اطلاعات کلی گزارش سود و زیان را نگهداری می کند
    /// </summary>
    public class ProfitLossViewModel
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public ProfitLossViewModel()
        {
            Items = new List<ProfitLossItemViewModel>();
        }

        /// <summary>
        /// مجموعه سطرهای محاسبه شده برای گزارش
        /// </summary>
        public List<ProfitLossItemViewModel> Items { get; }

        /// <summary>
        /// مجموعه سطرهای محاسبه شده برای گزارش مقایسه ای چند مرکز هزینه
        /// </summary>
        public List<ProfitLossByCostCentersViewModel> ItemsByCostCenters { get; }

        /// <summary>
        /// مجموعه سطرهای محاسبه شده برای گزارش مقایسه ای چند پروژه
        /// </summary>
        public List<ProfitLossByProjectsViewModel> ItemsByProjects { get; }

        /// <summary>
        /// مجموعه سطرهای محاسبه شده برای گزارش مقایسه ای چند شعبه
        /// </summary>
        public List<ProfitLossByBranchesViewModel> ItemsByBranches { get; }

        /// <summary>
        /// مجموعه سطرهای محاسبه شده برای گزارش مقایسه ای چند دوره مالی
        /// </summary>
        public List<ProfitLossByFiscalPeriodsViewModel> ItemsByFiscalPeriods { get; }

        /// <summary>
        /// سطرهای اطلاعاتی گزارش را با مجموعه سطرهای اطلاعاتی داده شده جایگزین می کند
        /// </summary>
        /// <param name="items">سطرهای اطلاعاتی مورد نظر برای جایگزینی در مدل نمایشی</param>
        public void SetItems(IEnumerable<ProfitLossItemViewModel> items)
        {
            Items.Clear();
            Items.AddRange(items);
        }
    }
}
