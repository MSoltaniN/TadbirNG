using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SPPC.Framework.Values;
using SPPC.Tadbir.Values;

namespace SPPC.Tadbir.ViewModel.Finance
{
    public partial class TransactionLineViewModel
    {
        /// <summary>
        /// Gets or sets the identifier of the transaction that contains this line (article) item
        /// </summary>
        public int TransactionId { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the corporate branch in which this line (article) item is defined
        /// </summary>
        public int BranchId { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the fiscal period in which this line (article) item is defined
        /// </summary>
        public int FiscalPeriodId { get; set; }

        ///// <summary>
        ///// Gets or sets the account vector that is affected by this article.
        ///// </summary>
        //public FullAccountViewModel FullAccount { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the currency that qualifies monetary values in this article.
        /// </summary>
        [Display(Name = FieldNames.CurrencyTypeField)]
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        public int? CurrencyId { get; set; }

        /// <summary>
        /// Gets or sets the name of the currency that qualifies the amount of this article
        /// </summary>
        [Display(Name = FieldNames.CurrencyTypeField)]
        public string CurrencyName { get; set; }
    }
}
