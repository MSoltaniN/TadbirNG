using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Model.Finance
{
    public partial class TransactionLine
    {
        /// <summary>
        /// Gets or sets the unique identifier of the account vector affected by this transaction line
        /// </summary>
        public virtual int FullAccountId { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the currency that qualifies monetary value in this transaction line
        /// </summary>
        public virtual int CurrencyId { get; set; }
    }
}
