using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPPC.Tadbir.ViewModel.Finance
{
    public partial class TransactionLineViewModel
    {
        /// <summary>
        /// Gets or sets the identifier of the transaction that contains this line (article) item
        /// </summary>
        public int TransactionId { get; set; }

        /// <summary>
        /// Gets or sets the code of the account that is affected by this article.
        /// </summary>
        public string AccountCode { get; set; }

        /// <summary>
        /// Gets or sets the name of the currency that qualifies the amount of this article
        /// </summary>
        public string CurrencyName { get; set; }
    }
}
