using SPPC.Tadbir.ViewModel.Corporate;
using SPPC.Tadbir.ViewModel.Finance;

namespace SPPC.Tadbir.ViewModel.Config
{
    /// <summary>
    /// اطلاعات مربوط به شرکت و شعبه و دوره مالی برای ایجاد اولیه شرکت را نگهداری میکند
    /// </summary>
    public class InitialCompanyViewModel
    {
        /// <summary>
        /// اطلاعات مربوط به یک شرکت را نگهداری میکند
        /// </summary>
        public CompanyDbViewModel Company { get; set; }

        /// <summary>
        /// اطلاعات مربوط به یک شعبه را نگهداری میکند
        /// </summary>
        public BranchViewModel Branch { get; set; }

        /// <summary>
        /// اطلاعات مربوط به یک دوره مالی را نگهداری میکند
        /// </summary>
        public FiscalPeriodViewModel FiscalPeriod { get; set; }
    }
}
