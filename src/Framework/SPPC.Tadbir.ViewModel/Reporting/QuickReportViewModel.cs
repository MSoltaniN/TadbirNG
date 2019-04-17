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
        public IList<QuickReportColumnModel> Columns { get; set; }

        /// <summary>
        ///
        /// </summary>
        public dynamic Row { get; set; }
    }

    /// <summary>
    ///
    /// </summary>
    public class QuickReportColumnModel
    {
        /// <summary>
        ///
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string DefaultText { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int SortMode { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int SortOrder { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        ///
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string UserText { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int DataType { get; set; }

        /// <summary>
        ///
        /// </summary>
        public bool Visible { get; set; }
    }
}
