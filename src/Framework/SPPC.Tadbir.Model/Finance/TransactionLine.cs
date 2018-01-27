using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Model.Finance
{
    public partial class TransactionLine
    {
        /// <summary>
        /// Gets or sets the unique identifier of the account affected by this transaction line
        /// </summary>
        public virtual int AccountId { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the currency that qualifies monetary value in this transaction line
        /// </summary>
        public virtual int CurrencyId { get; set; }
    }
}
