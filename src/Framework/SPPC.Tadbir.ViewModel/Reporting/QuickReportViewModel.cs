using System;
using System.Collections.Generic;
using System.Text;

namespace SPPC.Tadbir.ViewModel.Reporting
{
    public class QuickReportViewModel
    {
        public string ReportTitle { get; set; }

        public int InchValue { get; set; }

        public IList<QuickReportColumnModel> Columns { get; set; }

        public dynamic row { get; set; }
    }

    public class QuickReportColumnModel
    {
        
        public string Name { get; set; }

        public string DefaultText { get; set; }

        public int Index { get; set; }

        public int SortMode { get; set; }

        public int SortOrder { get; set; }

        public int Width { get; set; }

        public bool Enabled { get; set; }

        public int Order { get; set; }

        public string UserText { get; set; }

        public int DataType { get; set; }

        public bool Visible { get; set; }
        
    }
}
