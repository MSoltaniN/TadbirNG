using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel.Reporting;

namespace SPPC.Tadbir.Persistence
{
    public class ProfitLossRepository : IProfitLossRepository
    {
        public async Task<ProfitLossViewModel> GetProfitLossAsync(ProfitLossParameters parameters)
        {
            var profitLoss = new ProfitLossViewModel();
            profitLoss.Items.AddRange(await GetGrossProfitItemsAsync(parameters));
            profitLoss.Items.AddRange(await GetOperationalCostItemsAsync(parameters));
            profitLoss.Items.AddRange(await GetOtherCostRevenueItemsAsync(parameters));
            profitLoss.Items.AddRange(await GetNetProfitItemsAsync(parameters));
            return profitLoss;
        }

        private async Task<IEnumerable<ProfitLossItemViewModel>> GetGrossProfitItemsAsync(
            ProfitLossParameters parameters)
        {
            throw new NotImplementedException();
        }

        private async Task<IEnumerable<ProfitLossItemViewModel>> GetOperationalCostItemsAsync(
            ProfitLossParameters parameters)
        {
            throw new NotImplementedException();
        }

        private async Task<IEnumerable<ProfitLossItemViewModel>> GetOtherCostRevenueItemsAsync(
            ProfitLossParameters parameters)
        {
            throw new NotImplementedException();
        }

        private async Task<IEnumerable<ProfitLossItemViewModel>> GetNetProfitItemsAsync(
            ProfitLossParameters parameters)
        {
            throw new NotImplementedException();
        }
    }
}
