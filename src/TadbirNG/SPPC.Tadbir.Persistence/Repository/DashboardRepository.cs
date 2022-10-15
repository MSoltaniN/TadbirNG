using System.Threading.Tasks;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Model.Reporting;
using SPPC.Tadbir.Persistence.Utility;
using SPPC.Tadbir.ViewModel.Reporting;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای تهیه اطلاعات خلاصه در داشبورد را پیاده سازی می کند
    /// </summary>
    public partial class DashboardRepository : EntityLoggingRepository<Widget, WidgetViewModel>, IDashboardRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="system"></param>
        /// <param name="report"></param>
        public DashboardRepository(IRepositoryContext context, ISystemRepository system,
            IReportDirectUtility report)
            : base(context, system?.Logger)
        {
            _report = report;
            Config = system.Config;
        }

        private async Task<int> GetUnbalancedVoucherCountAsync()
        {
            var repository = UnitOfWork.GetAsyncRepository<Voucher>();
            return await repository.GetCountByCriteriaAsync(
                v => v.FiscalPeriodId == UserContext.FiscalPeriodId && !v.IsBalanced);
        }
    }
}
