using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel.Reporting;

namespace SPPC.Tadbir.Persistence
{
    public interface IProfitLossRepository
    {
        Task<ProfitLossViewModel> GetProfitLossAsync(ProfitLossParameters parameters);
    }
}
