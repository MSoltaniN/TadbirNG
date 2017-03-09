using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SPPC.Framework.Values;
using SPPC.Tadbir.Values;

namespace SPPC.Tadbir.ViewModel.Finance
{
    /// <summary>
    /// Provides complete details for a financial transaction, including additional fields from related entities.
    /// </summary>
    public class TransactionFullViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionFullViewModel"/> class.
        /// </summary>
        public TransactionFullViewModel()
        {
            Transaction = new TransactionViewModel();
            Lines = new List<TransactionLineViewModel>();
        }

        /// <summary>
        /// Gets or sets a <see cref="TransactionViewModel"/> object containing the main transaction data.
        /// </summary>
        public TransactionViewModel Transaction { get; set; }

        /// <summary>
        /// Gets or sets the name of fiscal period in which this transaction is defined.
        /// </summary>
        [Display(Name = Entities.FiscalPeriod)]
        public string FiscalPeriodName { get; set; }

        /// <summary>
        /// Gets or sets the name of company branch in which the fiscal period of this transaction is defined.
        /// </summary>
        [Display(Name = Entities.Branch)]
        public string FiscalPeriodBranchName { get; set; }

        /// <summary>
        /// Gets or sets the name of company in which the branch for this transaction is defined.
        /// </summary>
        [Display(Name = Entities.Company)]
        public string FiscalPeriodBranchCompanyName { get; set; }

        /// <summary>
        /// Gets or sets a collection of lines in this transaction.
        /// </summary>
        public IList<TransactionLineViewModel> Lines { get; private set; }
    }
}
