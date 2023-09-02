using System;
using System.Collections.Generic;
using System.Text;

namespace SPPC.Tadbir.ViewModel.Reporting
{
    /// <summary>
    /// لیستی از اعداد
    /// </summary>
    public class NumberListViewModel : ViewModelBase
    {
        /// <summary>
        /// شماره
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// شناسه دیتابیسی شعبه‌ شماره سند مفقودی
        /// </summary>
        public int BranchId { get; set; }
    }
}
