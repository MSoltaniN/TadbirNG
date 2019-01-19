using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Reporting
{
    public partial class ReportViewModel
    {
        public int? ParentId { get; set; }

        /// <summary>
        /// یک دیکشنری از متن های چند زبانه مورد نیاز در گزارش
        /// </summary>
        public Dictionary<string, string> ResourceMap { get; private set; }
    }
}
