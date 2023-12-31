﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SPPC.Tadbir.ViewModel.Finance
{
    public partial class AccountOwnerViewModel
    {
        /// <summary>
        /// شناسه دیتابیسی حساب مرتبط
        /// </summary>
        public int AccountId { get; set; }

        /// <summary>
        /// مجموعه صاحبان حساب
        /// </summary>
        public IList<AccountHolderViewModel> AccountHolders { get; set; }
    }
}
