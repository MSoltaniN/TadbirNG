using System;
using System.Collections.Generic;
using System.Text;

namespace SPPC.Tadbir.Model.Finance
{
    public partial class Account
    {
        /// <summary>
        /// Gets a collection of all accounts that are immediately below this item in the account hierarchy
        /// </summary>
        public IList<Account> Children { get; protected set; }
    }
}
