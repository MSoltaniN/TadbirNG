using System;
using System.Globalization;
using SPPC.Framework.Common;

namespace SPPC.Framework.Extensions
{
    public static class DateTimeExtensions
    {
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
    }
}
