using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Finance
{
    /// <summary>
    /// اطلاعات نمایشی مولفه های فرعی حساب که با یک مولفه اصلی مرتبط شده اند
    /// </summary>
    public class AccountItemRelationsViewModel
    {
        /// <summary>
        /// شناسه دیتابیسی مولفه اصلی حساب
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// مجموعه ای از شناسه های دیتابیسی مولفه های مرتبط شده با مولفه اصلی
        /// </summary>
        public IList<int> RelatedItemIds { get; set; }
    }
}
