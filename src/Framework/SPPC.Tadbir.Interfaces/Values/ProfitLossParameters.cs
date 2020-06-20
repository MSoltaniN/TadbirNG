using System;
using System.Collections.Generic;
using SPPC.Framework.Presentation;

namespace SPPC.Tadbir.Values
{
    public class ProfitLossParameters
    {
        public ProfitLossParameters()
        {
        }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public decimal TaxAmount { get; set; }

        public bool UseClosingTempVoucher { get; set; }

        public GridOptions GridOptions { get; set; }
    }
}
