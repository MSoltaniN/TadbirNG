using System;
using System.Collections.Generic;
using System.Text;

namespace SPPC.Tadbir.Configuration.Models
{
    /// <summary>
    /// تنظیمات صفحه را نگهداری می کند
    /// </summary>
    public class ReportPageSetting
    {
        /// <summary>
        /// فلگ تنظیم اتوماتیک ستون ها
        /// </summary>
        public bool ColumnFitPage;

        /// <summary>
        /// حاشیه پایین
        /// </summary>
        public float MarginBottom;

        /// <summary>
        /// حاشیه چپ
        /// </summary>
        public float MarginLeft;

        /// <summary>
        /// حاشیه راست
        /// </summary>
        public float MarginRight;

        /// <summary>
        /// حاشیه بالا
        /// </summary>
        public float MarginTop;

        /// <summary>
        /// جهت صفحه
        /// </summary>
        public string PageOrientation;

        /// <summary>
        /// اندازه صفحه
        /// </summary>
        public string PageSize;
    }
}
