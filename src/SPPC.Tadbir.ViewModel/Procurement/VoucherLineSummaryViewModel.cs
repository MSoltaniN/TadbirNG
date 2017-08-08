using System;
using System.Collections.Generic;
using SPPC.Tadbir.ViewModel.Core;

namespace SPPC.Tadbir.ViewModel.Procurement
{
    public class VoucherLineSummaryViewModel
    {
        public VoucherLineSummaryViewModel()
        {
        }

        public int No { get; set; }

        public string WarehouseName { get; set; }

        public string ProductName { get; set; }

        public string UomName { get; set; }

        public double OrderedQuantity { get; set; }

        public string RequiredDate { get; set; }

        public string Description { get; set; }
    }
}
