using System;
using System.Collections.Generic;
using System.Text;

namespace SPPC.Tadbir.Model.Finance
{
    public partial class CostCenter
    {
        /// <summary>
        /// Gets a collection of all cost centers that are immediately below this item in the cost center hierarchy
        /// </summary>
        public IList<CostCenter> Children { get; protected set; }
    }
}
