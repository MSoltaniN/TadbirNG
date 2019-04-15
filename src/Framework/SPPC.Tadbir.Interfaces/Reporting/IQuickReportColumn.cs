using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Reporting
{
    /// <summary>
    /// اطلاعات یک ستون از گزارش فوری را نگهداری می کند.
    /// </summary>
    public interface IQuickReportColumn
    {
        /// <summary>
        /// To Be Added (TBA)
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// TBA
        /// </summary>
        string DefaultText { get; set; }

        /// <summary>
        /// TBA
        /// </summary>
        int Index { get; set; }

        /// <summary>
        /// TBA
        /// </summary>
        int SortMode { get; set; }

        /// <summary>
        /// TBA
        /// </summary>
        int SortOrder { get; set; }

        /// <summary>
        /// TBA
        /// </summary>
        int Width { get; set; }

        /// <summary>
        /// TBA
        /// </summary>
        bool Enabled { get; set; }

        /// <summary>
        /// TBA
        /// </summary>
        int Order { get; set; }

        /// <summary>
        /// TBA
        /// </summary>
        string UserText { get; set; }

        /// <summary>
        /// TBA
        /// </summary>
        int DataType { get; set; }

        /// <summary>
        /// TBA
        /// </summary>
        bool Visible { get; set; }
    }

    [Serializable]
    public class QuickReportInfo
    {
        public int ReportId { get; set; }

        public IQuickReportColumn[] QuickReportColumn { get; set; }
    }
}
