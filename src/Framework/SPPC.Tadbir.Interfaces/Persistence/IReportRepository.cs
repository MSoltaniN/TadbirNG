using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.ViewModel.Auth;
using SPPC.Tadbir.ViewModel.Report;

namespace SPPC.Tadbir.Persistence
{
    public interface IReportRepository
    {
        void SetCurrentContext(UserContextViewModel userContext);

        Task<IList<VoucherSummaryViewModel>> GetVoucherSummaryByDateReportAsync(
            GridOptions gridOptions);

        Task<int> GetVoucherSummaryByDateCountAsync(GridOptions gridOptions);

        Task<StandardVoucherViewModel> GetStandardVoucherFormAsync(
            int voucherId, bool withDetail = false);
    }
}
