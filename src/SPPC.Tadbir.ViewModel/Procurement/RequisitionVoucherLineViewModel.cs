using System;
using System.Collections.Generic;
using SPPC.Tadbir.ViewModel.Core;

namespace SPPC.Tadbir.ViewModel.Procurement
{
    public partial class RequisitionVoucherLineViewModel
    {
        public int VoucherId { get; set; }

        public int FiscalPeriodId { get; set; }

        public int BranchId { get; set; }

        public DocumentViewModel Document { get; set; }
    }
}
