using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Reporting
{
    public class ProfitLossItemViewModel
    {
        public ProfitLossItemViewModel()
        {
        }

        public string Category { get; set; }

        public string Account { get; set; }

        public decimal StartBalance { get; set; }

        public decimal PeriodTurnover { get; set; }

        public decimal EndBalance { get; set; }

        public decimal Balance { get; set; }

        public string BranchName { get; set; }
    }
}
