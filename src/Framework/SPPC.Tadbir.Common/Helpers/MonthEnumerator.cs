using System;
using System.Collections.Generic;
using System.Globalization;

namespace SPPC.Tadbir.Helpers
{
    public class MonthEnumerator
    {
        public MonthEnumerator(DateTime start, DateTime end, Calendar calendar)
        {
            StartDate = start;
            EndDate = end;
            Calendar = calendar;
        }

        public DateTime StartDate { get; }

        public DateTime EndDate { get; }

        public Calendar Calendar { get; }

        public IEnumerable<MonthInfo> GetMonths()
        {
            var months = new List<MonthInfo>();
            var current = StartDate;
            while (current <= EndDate)
            {
                int year = Calendar.GetYear(current);
                int month = Calendar.GetMonth(current);
                int monthDays = Calendar.GetDaysInMonth(year, month);
                string monthName = month.ToString();
                var monthInfo = new MonthInfo()
                {
                    Start = Calendar.ToDateTime(year, month, 1, 0, 0, 0, 0),
                    End = Calendar.ToDateTime(year, month, monthDays, 0, 0, 0, 0),
                    Name = monthName
                };
                monthInfo.Date = monthInfo.End;
                months.Add(monthInfo);

                if (month < Calendar.GetMonthsInYear(year))
                {
                    month = month + 1;
                }
                else
                {
                    year++;
                    month = 1;
                }

                current = Calendar.AddMonths(current, 1);
            }

            if (months.Count > 0)
            {
                var first = months[0];
                var last = months[months.Count - 1];
                first.Start = StartDate;
                last.End = EndDate;
            }

            return months;
        }
    }
}
