using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SPPC.Tadbir.Values;

namespace SPPC.Tadbir.ViewModel.Finance
{
    /// <summary>
    /// Provides complete details for a transaction line (article), including additional fields from related entities.
    /// </summary>
    public class TransactionLineFullViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionLineFullViewModel"/> class.
        /// </summary>
        public TransactionLineFullViewModel()
        {
            Article = new TransactionLineViewModel();
        }

        /// <summary>
        /// Gets or sets a <see cref="TransactionLineViewModel"/> object containing the main transaction line (article) data.
        /// </summary>
        public TransactionLineViewModel Article { get; set; }

        /// <summary>
        /// Gets or sets the name of the financial account affected by this transaction line (article).
        /// </summary>
        [Display(Name = FieldNames.AccountNameField)]
        public string AccountName { get; set; }

        /// <summary>
        /// Gets or sets the name of fiscal period in which this transaction line (article) is defined.
        /// </summary>
        [Display(Name = Entities.FiscalPeriod)]
        public string FiscalPeriodName { get; set; }

        /// <summary>
        /// Gets or sets the name of company branch in which the fiscal period of this transaction line (article)
        /// is defined.
        /// </summary>
        [Display(Name = Entities.Branch)]
        public string BranchName { get; set; }

        /// <summary>
        /// Gets or sets the name of company in which the branch for this transaction line (article) is defined.
        /// </summary>
        [Display(Name = Entities.Company)]
        public string BranchCompanyName { get; set; }
    }
}
