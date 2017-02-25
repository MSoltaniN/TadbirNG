using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        /// Gets or sets the identifier of the account that is affected by this article.
        /// </summary>
        [Display(Name = FieldNames.AccountField)]
        public int AccountId { get; set; }

        /// <summary>
        /// Gets or sets the code of the account that is affected by this article.
        /// </summary>
        public string AccountCode { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the currency that qualifies monetary values in this article.
        /// </summary>
        [Display(Name = FieldNames.CurrencyTypeField)]
        public int CurrencyId { get; set; }

        /// <summary>
        /// Gets or sets the name of the currency that qualifies the amount of this article
        /// </summary>
        public string CurrencyName { get; set; }
    }
}
