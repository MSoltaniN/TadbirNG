using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Finance
{
    /// <summary>
    /// اطلاعات یک نقطه قابل نمایش در نمودار ستونی را نگهداری می کند
    /// </summary>
    public class DashboardChartPointViewModel
    {
        /// <summary>
        /// مقدار قابل نمایش در محور افقی نمودار
        /// </summary>
        public string XValue { get; set; }

        /// <summary>
        /// مقدار قابل نمایش در محور عمودی نمودار
        /// </summary>
        public decimal YValue { get; set; }
    }
}
