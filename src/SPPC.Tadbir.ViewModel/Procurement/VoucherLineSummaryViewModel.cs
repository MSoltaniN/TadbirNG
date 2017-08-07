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

        public int WarehouseId { get; set; }

        public int ProductId { get; set; }

        public int UomId { get; set; }

        public float OrderedQuantity { get; set; }

        public string RequiredDate { get; set; }

        public string Description { get; set; }

        public DocumentViewModel Document { get; set; }
    }
}
