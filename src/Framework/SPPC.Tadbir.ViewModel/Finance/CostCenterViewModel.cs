using System;
using System.Collections.Generic;
using System.Text;

namespace SPPC.Tadbir.ViewModel.Finance
{
    public partial class CostCenterViewModel
    {
        /// <summary>
        /// Gets or sets the identifier of the branch in which this cost center is defined.
        /// </summary>
        public int BranchId { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the fiscal period in which this cost center is defined.
        /// </summary>
        public int FiscalPeriodId { get; set; }
    }
}
