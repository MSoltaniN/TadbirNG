using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Finance
{
    public partial class CurrencyViewModel
    {
        /// <summary>
        /// شناسه دیتابیسی شعبه ای که ارز در آن تعریف شده است
        /// </summary>
        public int BranchId { get; set; }

        /// <summary>
        /// نام شعبه ای که ارز در آن تعریف شده است
        /// </summary>
        public string BranchName { get; set; }
    }
}
