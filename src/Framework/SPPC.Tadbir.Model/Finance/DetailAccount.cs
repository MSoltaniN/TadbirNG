using System;
using System.Collections.Generic;
using System.Text;

namespace SPPC.Tadbir.Model.Finance
{
    public partial class DetailAccount
    {
        /// <summary>
        /// Gets a collection of all detail accounts that are immediately below this item in the detail account hierarchy
        /// </summary>
        public IList<DetailAccount> Children { get; protected set; }
    }
}
