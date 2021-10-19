using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Finance
{
    /// <summary>
    /// اطلاعات نمایشی بردار حساب را نگهداری می کند
    /// </summary>
    public class FullAccountViewModel
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public FullAccountViewModel()
        {
            Account = new AccountItemBriefViewModel();
            DetailAccount = new AccountItemBriefViewModel();
            CostCenter = new AccountItemBriefViewModel();
            Project = new AccountItemBriefViewModel();
        }

        /// <summary>
        /// مولفه سرفصل حسابداری
        /// </summary>
        public AccountItemBriefViewModel Account { get; set; }

        /// <summary>
        /// مولفه تفصیلی شناور
        /// </summary>
        public AccountItemBriefViewModel DetailAccount { get; set; }

        /// <summary>
        /// مولفه مرکز هزینه
        /// </summary>
        public AccountItemBriefViewModel CostCenter { get; set; }

        /// <summary>
        /// مولفه پروژه
        /// </summary>
        public AccountItemBriefViewModel Project { get; set; }
    }
}
