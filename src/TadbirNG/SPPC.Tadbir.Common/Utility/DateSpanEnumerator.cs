using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace SPPC.Tadbir.Utility
{
    /// <summary>
    /// امکان شمارش فواصل زمانی موجود در یک محدوده تاریخی را فراهم می کند
    /// </summary>
    public class DateSpanEnumerator
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="start">تاریخ ابتدای محدوده تاریخی مورد نظر</param>
        /// <param name="end">تاریخ انتهای محدوده تاریخی مورد نظر</param>
        /// <param name="calendar">تقویم مورد استفاده برای شمارش ماه ها</param>
        public DateSpanEnumerator(DateTime start, DateTime end, Calendar calendar)
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
        /// تقویم مورد استفاده برای شمارش فواصل زمانی
        /// </summary>
        public Calendar Calendar { get; }

        /// <summary>
        /// ماه های موجود در محدوده تاریخی جاری را به صورت یک مجموعه قابل پیمایش برمی گرداند
        /// </summary>
        /// <returns>مجموعه ماه های موجود در محدوده تاریخی جاری</returns>
        public IEnumerable<DateSpanInfo> GetMonths()
        {
            var months = new List<DateSpanInfo>();
            var current = StartDate;
            while (current <= EndDate)
            {
                int year = Calendar.GetYear(current);
                int month = Calendar.GetMonth(current);
                int monthDays = Calendar.GetDaysInMonth(year, month);
                string monthName = String.Format("Month{0}", month);
                var monthInfo = new DateSpanInfo()
                {
                    Start = Calendar.ToDateTime(year, month, 1, 0, 0, 0, 0),
                    End = Calendar.ToDateTime(year, month, monthDays, 0, 0, 0, 0),
                    Name = monthName
                };
                months.Add(monthInfo);

                if (month < Calendar.GetMonthsInYear(year))
                {
                    month++;
                }
                else
                {
                    year++;
                    month = 1;
                }

                current = Calendar.ToDateTime(year, month, 1, 0, 0, 0, 0);
            }

            if (months.Count > 0)
            {
                var first = months[0];
                var last = months[^1];
                first.Start = StartDate;
                last.End = EndDate;
            }

            return months;
        }

        /// <summary>
        /// هفته های موجود در محدوده تاریخی جاری را به صورت یک مجموعه قابل پیمایش برمی گرداند
        /// </summary>
        /// <returns>مجموعه هفته های موجود در محدوده تاریخی جاری</returns>
        public IEnumerable<DateSpanInfo> GetWeeks()
        {
            var weeks = new List<DateSpanInfo>();
            int weekNo = Calendar.GetWeekOfYear(
                StartDate, CalendarWeekRule.FirstDay, GetFirstWeekDay(Calendar));
            int endOfWeekOffset = GetEndOfWeekOffset(Calendar, Calendar.GetDayOfWeek(StartDate));
            weeks.Add(new DateSpanInfo()
            {
                Start = StartDate,
                End = Calendar.AddDays(StartDate, endOfWeekOffset),
                Name = $"{weekNo++}"
            });
            var current = Calendar.AddDays(StartDate, endOfWeekOffset + 1);
            while (current <= EndDate)
            {
                weeks.Add(new DateSpanInfo()
                {
                    Start = current,
                    End = Calendar.AddDays(current, 6),
                    Name = $"{weekNo++}"
                });
                current = Calendar.AddWeeks(current, 1);
            }

            var lastWeek = weeks.Last();
            if (lastWeek.End > EndDate)
            {
                lastWeek.End = EndDate;
            }

            return weeks;
        }

        private static DayOfWeek GetFirstWeekDay(Calendar calendar)
        {
            var firstDay = DayOfWeek.Monday;
            if (calendar is PersianCalendar)
            {
                firstDay = DayOfWeek.Saturday;
            }

            return firstDay;
        }

        private static int GetStartOfWeekOffset(Calendar calendar, DayOfWeek dayOfWeek)
        {
            int offset;
            if (calendar is PersianCalendar)
            {
                offset = dayOfWeek == DayOfWeek.Saturday
                    ? 0
                    : (int)dayOfWeek + 1;
            }
            else    // Currently, this means Gregorian calendar (only two calendars currently supported)
            {
                offset = dayOfWeek == DayOfWeek.Sunday
                    ? 6
                    : (int)dayOfWeek - 1;
            }

            return offset;
        }

        private static int GetEndOfWeekOffset(Calendar calendar, DayOfWeek dayOfWeek)
        {
            return 6 - GetStartOfWeekOffset(calendar, dayOfWeek);
        }

        private static DateTime GetStartOfWeek(Calendar calendar, DateTime date)
        {
            var dayOfWeek = calendar.GetDayOfWeek(date);
            int offset = GetStartOfWeekOffset(calendar, dayOfWeek);
            return calendar.AddDays(date, -offset);
        }
    }
}
