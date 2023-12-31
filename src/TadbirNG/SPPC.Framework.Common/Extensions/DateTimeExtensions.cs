﻿using System;
using System.Globalization;
using SPPC.Framework.Common;

namespace SPPC.Framework.Extensions
{
    /// <summary>
    /// عملیات تکمیلی مورد نیاز برای کار با مقادیر تاریخ را به کلاس موجود در دات نت اضافه می کند
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// مقدار جاری تاریخ را در آبجکت مورد نظر به صورت رشته متنی با فرمت تاریخ مختصر برمی گرداند
        /// </summary>
        /// <param name="dateTime">آبجکت مورد نظر</param>
        /// <param name="cultureAware">مشخص می کند که آیا تبدیل تاریخ با توجه به زبان جاری محیط مورد نیاز است یا نه</param>
        /// <returns>تاریخ در آبجکت مورد نظر به صورت رشته متنی با فرمت تاریخ مختصر</returns>
        public static string ToShortDateString(this DateTime dateTime, bool cultureAware)
        {
            Verify.ArgumentNotNull(dateTime, nameof(dateTime));
            if (cultureAware)
            {
                return dateTime.ToShortDateString();
            }

            var enCulture = new CultureInfo("en");
            return dateTime.ToString("yyyy-MM-dd", enCulture);
        }

        /// <summary>
        /// یک مقدار تاریخ با فرمت متنی را پردازش کرده و به صورت یک نمونه جدید از کلاس تاریخ برمی گرداند
        /// </summary>
        /// <param name="dateTime">آبجکتی که این متد توسط آن فراخوانی می شود</param>
        /// <param name="value">تاریخ مورد نظر با فرمت متنی</param>
        /// <param name="cultureAware">مشخص می کند که آیا تبدیل تاریخ با توجه به زبان جاری محیط مورد نیاز است یا نه</param>
        /// <returns>نمونه کلاس تاریخ پردازش شده از فرمت متنی</returns>
        /// <remarks>
        /// متد متناظر در کتابخانه دات نت از نوع استاتیک است و نیاز به نمونه کلاس تاریخ ندارد،
        /// ولی چون در دات نت امکان ایجاد متدهای توسعه یافته برای متدهای استاتیک وجود ندارد،
        /// ناچاریم برای استفاده از این متد حتماً یک نمونه کلاس تاریخی داشته باشیم.
        /// </remarks>
        public static DateTime Parse(this DateTime dateTime, string value, bool cultureAware = true)
        {
            Verify.ArgumentNotNull(dateTime, nameof(dateTime));
            Verify.ArgumentNotNullOrWhitespace(value, nameof(value));
            if (cultureAware)
            {
                return DateTime.Parse(value);
            }

            var enCulture = new CultureInfo("en");
            return DateTime.Parse(value, enCulture);
        }

        /// <summary>
        /// مقایسه تاریخ بین آبجکت جاری و آبجکت دیگری را بدون در نظر گرفتن ساعت انجام می دهد.
        /// </summary>
        /// <param name="dateTime">آبجکتی که این متد توسط آن فراخوانی می شود</param>
        /// <param name="other">آبجکتی که فقط بخش تاریخ آن با آبجکت جاری مقایسه می شود</param>
        /// <returns>اگر تاریخ جاری بزرگتر باشد مقدار یک، اگر مساوی باشد مقدار صفر و
        /// اگر کوچکتر باشد مقدار منفی یک را برمی گرداند</returns>
        public static int CompareWith(this DateTime dateTime, DateTime other)
        {
            Verify.ArgumentNotNull(dateTime);
            return dateTime.Date.CompareTo(other.Date);
        }

        /// <summary>
        /// مشخص می کند که آیا تاریخ جاری بین دو تاریخ داده شده قرار می گیرد یا نه؟
        /// </summary>
        /// <param name="dateTime">آبجکتی که این متد توسط آن فراخوانی می شود</param>
        /// <param name="from">ابتدای محدوده تاریخی مورد نظر</param>
        /// <param name="to">انتهای محدوده تاریخی مورد نظر</param>
        /// <returns>اگر تاریخ جاری بین دو تاریخ داده شده باشد مقدار "درست" و
        /// در غیر این صورت مقدار "نادرست" را برمی گرداند</returns>
        /// <remarks>این متد مقادیر ساعت را در تمام آبجکت های تاریخ نادیده می گیرد و
        /// تاریخ ابتدا و انتها را هم جزو محدوده در نظر می گیرد.</remarks>
        public static bool IsBetween(this DateTime dateTime, DateTime from, DateTime to)
        {
            Verify.ArgumentNotNull(dateTime);
            return dateTime.CompareWith(from) >= 0
                && dateTime.CompareWith(to) <= 0;
        }

        /// <summary>
        /// مشخص می کند که روز مشخص در تاریخ جاری روز خاص سال کبیسه هست یا نه؟
        /// </summary>
        /// <param name="dateTime">آبجکتی که این متد توسط آن فراخوانی می شود</param>
        /// <returns>اگر روز مشخص در تاریخ جاری روز کبیسه باشد مقدار "درست" و
        /// در غیر این صورت مقدار "نادرست" را برمی گرداند</returns>
        public static bool IsLeapDay(this DateTime dateTime)
        {
            var calendar = new GregorianCalendar();
            return calendar.IsLeapDay(dateTime.Year, dateTime.Month, dateTime.Day);
        }
    }
}
