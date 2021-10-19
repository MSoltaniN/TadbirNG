using System;
using System.Globalization;
using SPPC.Framework.Common;

namespace SPPC.Framework.Extensions
{
    /// <summary>
    /// امکانات تکمیلی را برای کلاس مرتبط با تقویم فارسی پیاده سازی می کند
    /// </summary>
    public static class PersianCalendarExtensions
    {
        /// <summary>
        /// تاریخ شروع ماه مشخص شده را در سال جاری برمی گرداند
        /// </summary>
        /// <param name="calendar">نمونه جاری از کلاس تقویم فارسی</param>
        /// <param name="month">شماره ماه مورد نظر که از یک شروع می شود</param>
        /// <returns>تاریخ شروع ماه مشخص شده</returns>
        public static DateTime GetStartOfMonth(this PersianCalendar calendar, int month)
        {
            Verify.ArgumentNotNull(calendar, "calendar");
            int currentYear = calendar.GetYear(DateTime.Now);
            return JalaliDateTime.
                Parse(String.Format("{0}/{1:D2}/01", currentYear, month))
                .ToGregorian()
                .Date;
        }

        /// <summary>
        /// تاریخ پایان ماه مشخص شده را در سال جاری برمی گرداند
        /// </summary>
        /// <param name="calendar">نمونه جاری از کلاس تقویم فارسی</param>
        /// <param name="month">شماره ماه مورد نظر که از یک شروع می شود</param>
        /// <returns>تاریخ پایان ماه مشخص شده</returns>
        public static DateTime GetEndOfMonth(this PersianCalendar calendar, int month)
        {
            Verify.ArgumentNotNull(calendar, "calendar");
            int currentYear = calendar.GetYear(DateTime.Now);
            DateTime startOfMonth = calendar.GetStartOfMonth(month);
            return startOfMonth + TimeSpan.FromDays(calendar.GetDaysInMonth(currentYear, month) - 1);
        }

        /// <summary>
        /// تاریخ شروع ماه مشخص شده را در سال مشخص شده برمی گرداند
        /// </summary>
        /// <param name="calendar">نمونه جاری از کلاس تقویم فارسی</param>
        /// <param name="year">سال مورد نظر</param>
        /// <param name="month">شماره ماه مورد نظر که از یک شروع می شود</param>
        /// <returns>تاریخ شروع ماه مشخص شده</returns>
        public static DateTime GetStartOfMonth(this PersianCalendar calendar, int year, int month)
        {
            Verify.ArgumentNotNull(calendar, "calendar");
            if (year < calendar.GetYear(calendar.MinSupportedDateTime)
                || year > calendar.GetYear(calendar.MaxSupportedDateTime))
            {
                throw ExceptionBuilder.NewArgumentOutOfRangeException(nameof(year));
            }

            return JalaliDateTime.
                Parse(String.Format("{0}/{1:D2}/01", year, month))
                .ToGregorian()
                .Date;
        }

        /// <summary>
        /// تاریخ پایان ماه مشخص شده را در سال مشخص شده برمی گرداند
        /// </summary>
        /// <param name="calendar">نمونه جاری از کلاس تقویم فارسی</param>
        /// <param name="month">شماره ماه مورد نظر که از یک شروع می شود</param>
        /// <param name="year">سال مورد نظر</param>
        /// <returns>تاریخ پایان ماه مشخص شده</returns>
        public static DateTime GetEndOfMonth(this PersianCalendar calendar, int year, int month)
        {
            Verify.ArgumentNotNull(calendar, "calendar");
            if (year < calendar.GetYear(calendar.MinSupportedDateTime)
                || year > calendar.GetYear(calendar.MaxSupportedDateTime))
            {
                throw ExceptionBuilder.NewArgumentOutOfRangeException(nameof(year));
            }

            DateTime startOfMonth = calendar.GetStartOfMonth(year, month);
            return startOfMonth + TimeSpan.FromDays(calendar.GetDaysInMonth(year, month) - 1);
        }
    }
}
