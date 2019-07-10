using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Reporting
{
    /// <summary>
    ///
    /// </summary>
    public class QuickReportColumnViewModel
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
        public int Width { get; set; }

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
        public bool Visible { get; set; }

        /// <summary>
        /// مشخص کننده نوع ستون برای استفاده خاص مثلا money یا number یا date
        /// </summary>
        public string Type { get; set; }
    }
}
