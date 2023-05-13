using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPPC.Tadbir.Model.CashFlow
{
    public partial class PayReceive
    {
        /// <summary>
        /// شناسه ارز
        /// </summary>
        public virtual int? CurrencyID { get; set; }
    }
}
