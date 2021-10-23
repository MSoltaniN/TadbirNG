using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using Stimulsoft.Controls;
using Stimulsoft.Base.Drawing;
using Stimulsoft.Report;
using Stimulsoft.Report.Dialogs;
using Stimulsoft.Report.Components;
using System.Globalization;

namespace Reports
{
    public class Report : Stimulsoft.Report.StiReport
    {
        public Report()
        {
            this.InitializeComponent();
        }

        public string PersianDate(DateTime DateTime1)
        {
            PersianCalendar PersianCalendar1 = new PersianCalendar();
            return string.Format(@"{0}/{1}/{2}",
                         PersianCalendar1.GetYear(DateTime1),
                         PersianCalendar1.GetMonth(DateTime1),
                         PersianCalendar1.GetDayOfMonth(DateTime1));
        }

        #region StiReport Designer generated code - do not modify
        #endregion StiReport Designer generated code - do not modify
    }
}