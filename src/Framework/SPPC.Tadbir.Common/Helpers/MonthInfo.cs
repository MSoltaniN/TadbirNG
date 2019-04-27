using System;
using System.Collections.Generic;
using System.Text;

namespace SPPC.Tadbir.Helpers
{
    public class MonthInfo
    {
        public MonthInfo()
        {
        }

        public string Name { get; set; }

        public DateTime Date { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public override string ToString()
        {
            return String.Format(
                "Month : {1}{0}From {2} to {3}{0}Date : {4}",
                Environment.NewLine, Name, Start, End, Date);
        }
    }
}
