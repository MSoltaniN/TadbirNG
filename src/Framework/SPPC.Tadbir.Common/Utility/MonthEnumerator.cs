using System;
using System.Collections.Generic;
using System.Globalization;

namespace SPPC.Tadbir.Utility
{
    /// <summary>
    /// امکان شمارش ماه های موجود در یک محدوده تاریخی را فراهم می کند
    /// </summary>
    public class MonthEnumerator
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="start">تاریخ ابتدای محدوده تاریخی مورد نظر</param>
        /// <param name="end">تاریخ انتهای محدوده تاریخی مورد نظر</param>
        /// <param name="calendar">تقویم مورد استفاده برای شمارش ماه ها</param>
        public MonthEnumerator(DateTime start, DateTime end, Calendar calendar)
        {
            StartDate = start;
            EndDate = end;
            Calendar = calendar;
        }

        /// <summary>
        /// تاریخ ابتدای محدوده تاریخی مورد نظر
        /// </summary>
        public DateTime StartDate { get; }

        /// <summary>
        /// تاریخ انتهای محدوده تاریخی مورد نظر
        /// </summary>
        public DateTime EndDate { get; }

        /// <summary>
        /// تقویم مورد استفاده برای شمارش ماه ها
        /// </summary>
        public Calendar Calendar { get; }

        /// <summary>
        /// ماه های موجود در محدوده تاریخی جاری را به صورت یک مجموعه قابل پیمایش برمی گرداند
        /// </summary>
        /// <returns></returns>
        public IEnumerable<MonthInfo> GetMonths()
        {
            var months = new List<MonthInfo>();
            var current = StartDate;
            while (current <= EndDate)
            {
                int year = Calendar.GetYear(current);
                int month = Calendar.GetMonth(current);
                int monthDays = Calendar.GetDaysInMonth(year, month);
                string monthName = String.Format("Month{0}", month);
                var monthInfo = new MonthInfo()
                {
                    Start = Calendar.ToDateTime(year, month, 1, 0, 0, 0, 0),
                    End = Calendar.ToDateTime(year, month, monthDays, 0, 0, 0, 0),
                    Name = monthName
                };
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
