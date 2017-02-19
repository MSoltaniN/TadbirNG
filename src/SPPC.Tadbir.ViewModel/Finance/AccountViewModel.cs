using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPPC.Tadbir.ViewModel.Finance
{
    public partial class AccountViewModel
    {
        /// <summary>
        /// Gets or sets the identifier of the fiscal period in which this financial account is defined.
        /// </summary>
        public int FiscalPeriodId { get; set; }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return Name;
        }
    }
}
