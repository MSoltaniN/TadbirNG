﻿using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Finance
{
    public partial class TransactionViewModel
    {
        /// <summary>
        /// Gets or sets the identifier of the user who first created this transaction
        /// </summary>
        public int CreatedById { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user who last modified this transaction
        /// </summary>
        public int ModifiedById { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the fiscal period in which this financial transaction is defined.
        /// </summary>
        public int FiscalPeriodId { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the branch in which this financial transaction is defined.
        /// </summary>
        public int BranchId { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the current work item for this transaction, if any.
        /// </summary>
        public int WorkItemId { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the target role of current work item for this transaction, if any.
        /// </summary>
        public int WorkItemTargetId { get; set; }

        /// <summary>
        /// Gets or sets the document action of current work item for this transaction, if any.
        /// </summary>
        public string WorkItemAction { get; set; }

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
