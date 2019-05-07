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
        /// عنوان گزارش فوری
        /// </summary>
        public string ReportTitle { get; set; }

        /// <summary>
        /// مقدار ۱ اینچ به پیکسل
        /// </summary>
        public int InchValue { get; set; }

        /// <summary>
        /// زیان ساخت گزارش
        /// </summary>
        public string ReportLang { get; set; }

        /// <summary>
        /// لیست ستون های گرید
        /// </summary>
        public IList<QuickReportColumnViewModel> Columns { get; set; }
    }
}
