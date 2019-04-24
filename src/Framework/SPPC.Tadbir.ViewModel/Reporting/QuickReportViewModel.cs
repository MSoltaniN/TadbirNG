using System;
using System.Collections.Generic;
using System.Text;

namespace SPPC.Tadbir.ViewModel.Reporting
{
    /// <summary>
    ///
    /// </summary>
    public class QuickReportViewModel
    {
        /// <summary>
        ///
        /// </summary>
        public string ReportTitle { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int InchValue { get; set; }

        /// <summary>
        ///
        /// </summary>
        public IList<QuickReportColumnViewModel> Columns { get; set; }

        /// <summary>
        ///
        /// </summary>
        public dynamic Row { get; set; }
    }
}
