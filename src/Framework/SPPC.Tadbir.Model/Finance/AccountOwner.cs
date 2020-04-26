using System;
using System.Collections.Generic;
using System.Text;

namespace SPPC.Tadbir.Model.Finance
{
    public partial class AccountOwner
    {
        /// <summary>
        /// شناسه دیتابیسی حساب مرتبط
        /// </summary>
        public int AccountID { get; set; }

        /// <summary>
        /// مجموعه صاحبان حساب
        /// </summary>
        public IList<AccountHolder> AccountHolders { get; set; }
    }
}
