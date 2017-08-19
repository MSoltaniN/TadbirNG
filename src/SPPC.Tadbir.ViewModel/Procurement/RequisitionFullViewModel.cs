using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Procurement
{
    public class RequisitionFullViewModel
    {
        public RequisitionFullViewModel()
        {
            Lines = new List<VoucherLineSummaryViewModel>();
        }

        public RequisitionVoucherViewModel Voucher { get; set; }

        public IList<VoucherLineSummaryViewModel> Lines { get; private set; }
    }
}
