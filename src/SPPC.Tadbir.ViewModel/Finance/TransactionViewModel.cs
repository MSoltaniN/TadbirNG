using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Finance
{
    public partial class TransactionViewModel
    {
        /// <summary>
        /// Gets or sets the sum of all debit values in transaction lines (articles).
        /// </summary>
        public decimal DebitSum { get; set; }

        /// <summary>
        /// Gets or sets the sum of all credit values in transaction lines (articles).
        /// </summary>
        public decimal CreditSum { get; set; }
    }
}
