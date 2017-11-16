using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace SPPC.Framework.Common
{
    /// <summary>
    /// Represents a date and time value in Jalali (Persian) calendar.
    /// </summary>
    public class JalaliDateTime
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JalaliDateTime"/> class that is initialized to the current
        /// system date and time.
        /// </summary>
        public JalaliDateTime()
        {
            JalaliDateTime now = JalaliDateTime.Now;
            _year = now.Year;
            _month = now.Month;
            _day = now.Day;
            SetWeekday();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JalaliDateTime"/> class that is initialized to a date in
        /// Jalali calendar specified by year, month and day values.
        /// </summary>
        /// <param name="year">Year value in Jalali calendar</param>
        /// <param name="month">One-based index of the month in Jalali calendar</param>
        /// <param name="day">One-based index of the day in Jalali calendar</param>
        public JalaliDateTime(int year, int month, int day)
        {
            _year = year;
            _month = month;
            _day = day;
            SetWeekday();
        }

        /// <summary>
        /// Gets the year component of the Jalali date encapsulated by this object.
        /// </summary>
        public int Year
        {
            get { return _year; }
        }

        /// <summary>
        /// Gets the month component of the Jalali date encapsulated by this object.
        /// </summary>
        public int Month
        {
            get { return _month; }
        }

        /// <summary>
        /// Gets the day component of the Jalali date encapsulated by this object.
        /// </summary>
        public int Day
        {
            get { return _day; }
        }

        /// <summary>
        /// Gets or sets the hour component of the Jalali date encapsulated by this object.
        /// </summary>
        public int Hour { get; set; }

        /// <summary>
        /// Gets or sets the minute component of the Jalali date encapsulated by this object.
        /// </summary>
        public int Minute { get; set; }

        /// <summary>
        /// Gets the local name of the month component of the Jalali date encapsulated by this object.
        /// </summary>
        public string MonthName
        {
            get { return _monthNames[_month - 1]; }
        }

        /// <summary>
        /// Gets the local name of the day of week in the Jalali date encapsulated by this object.
        /// </summary>
        public string Weekday
        {
            get { return _dayNames[_weekday - 1]; }
        }

        /// <summary>
        /// Gets a <see cref="JalaliDateTime"/> object initialized to the current system date and time in Jalali calendar.
        /// </summary>
        public static JalaliDateTime Now
        {
            get { return (FromDateTime(DateTime.Now)); }
        }

        /// <summary>
        /// Attempts to parse the given string as a date value in Jalali calendar.
        /// </summary>
        /// <param name="dateTime">String representation of a date value</param>
        /// <param name="value">A <see cref="JalaliDateTime"/> object parsed from given string</param>
        /// <returns>True if given string could be parsed as a date value in Jalali calendar; otherwise returns false.</returns>
        /// <remarks>If parsing fails, given <see cref="JalaliDateTime"/> value is initialized to <c>JalaliDateTime.Now</c>.</remarks>
        public static bool TryParse(string dateTime, out JalaliDateTime value)
        {
            value = null;
            bool parsed = true;
            string rx = @"1(\d{3})[-|/](\d{1,2})[-|/](\d{1,2})";
            var match = Regex.Match(dateTime, rx);
            if (match == null || match.Groups.Count < 4)
            {
                parsed = false;
            }
            else
            {
                int year = 1000 + Int32.Parse(match.Groups[1].Value);
                int month = Int32.Parse(match.Groups[2].Value);
                int day = Int32.Parse(match.Groups[3].Value);
                if ((month > 12) || (day > 31) || (month > 6 && month <= 12 && day > 30))
                {
                    parsed = false;
                }
                else
                {
                    value = new JalaliDateTime(year, month, day);
                }
            }

            return parsed;
        }

        public static JalaliDateTime Parse(string dateTime)
        {
            JalaliDateTime jalali = null;
            if (!TryParse(dateTime, out jalali))
            {
                throw ExceptionBuilder.NewFormatException("String does not represent a valid Jalali date value.");
            }

            return jalali;
        }

        /// <summary>
        /// Initializes a <see cref="JalaliDateTime"/> object from the given <see cref="System.DateTime"/> object.
        /// </summary>
        /// <param name="date">A DateTime value that should be converted to the equivalent value in Jalali calendar.</param>
        /// <returns>Equivalent of the given DateTime value in Jalali calendar</returns>
        public static JalaliDateTime FromDateTime(DateTime date)
        {
            PersianCalendar calendar = new PersianCalendar();
            int year = calendar.GetYear(date);
            int month = calendar.GetMonth(date);
            int day = calendar.GetDayOfMonth(date);

            JalaliDateTime jalali = new JalaliDateTime(year, month, day);
            jalali.Hour = date.Hour;
            jalali.Minute = date.Minute;
            return jalali;
        }

        /// <summary>
        /// Returns a <see cref="System.DateTime"/> object representing the equivalent of this object in Gregorian calendar.
        /// </summary>
        /// <returns>Equivalent of DateTime value of this object in Gregorian calendar</returns>
        public DateTime ToGregorian()
        {
            PersianCalendar calendar = new PersianCalendar();
            DateTime date = calendar.ToDateTime(_year, _month, _day, Hour, Minute, 0, 0);
            return date;
        }

        /// <summary>
        /// Returns the local string representation of the DateTime value of this object.
        /// </summary>
        /// <returns>Local string representation of the underlying date</returns>
        public override string ToString()
        {
            string toString = String.Empty;
            toString = String.Format("{0} {1} {2} {3} ساعت {4}:{5}",
                Weekday, Day, MonthName, Year, Hour, Minute);
            return toString;
        }

        public string ToString(string format)
        {
            string toString = ToShortDateString();
            if (format == "wdmy")
            {
                toString = String.Format("{0} {1} {2} {3}", Weekday, Day, MonthName, Year);
            }
            else if (format == "dmy")
            {
                toString = String.Format("{0} {1} {2}", Day, MonthName, Year);
            }
            else if (format == "dmyw")
            {
                toString = String.Format("{0} {1} {2} ({3})", Day, MonthName, Year, Weekday);
            }

            return toString;
        }

        /// <summary>
        /// Returns a string representation of the Jalali date using common persian string format (YYYY/MM/DD).
        /// </summary>
        /// <returns>Short date representation of this Jalali date object.</returns>
        public string ToShortDateString()
        {
            string result = String.Format("{0}/{1}/{2}", Year, Month, Day);
            return result;
        }

        private void SetWeekday()
        {
            DateTime date = ToGregorian();
            int weekday = (int)date.DayOfWeek;
            _weekday = (weekday < 6) ? (weekday + 2) : (weekday - 5);
        }

        private string[] _monthNames = new string[]
        {
            "فروردین", "اردیبهشت", "خرداد", "تیر",
            "مرداد", "شهریور", "مهر", "آبان",
            "آذر", "دی", "بهمن", "اسفند"
        };

        private string[] _dayNames = new string[]
        {
            "شنبه", "یکشنبه", "دوشنبه", "سه شنبه",
            "چهارشنبه", "پنج شنبه", "جمعه"
        };

        private int _year;
        private int _month;
        private int _day;
        private int _weekday;
    }
}
