using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Finance
{
    public partial class TransactionViewModel
    {
        /// <summary>
        /// Gets or sets the identifier of the user who first created this transaction
        /// </summary>
        public int CreatorId { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user who last modified this transaction
        /// </summary>
        public int LastModifierId { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the fiscal period in which this financial transaction is defined.
        /// </summary>
        public int FiscalPeriodId { get; set; }

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
