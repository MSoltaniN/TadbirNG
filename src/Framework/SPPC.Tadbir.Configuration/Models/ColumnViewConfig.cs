using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Configuration.Models
{
    /// <summary>
    /// اطلاعات تنظیمات نمایشی یک ستون در نمای لیستی را نگهداری می کند
    /// </summary>
    public class ColumnViewConfig
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public ColumnViewConfig()
        {
            Visibility = ColumnVisibility.Visible;
        }

        /// <summary>
        /// نمونه جدیدی از این کلاس با نام مشخص شده می سازد.
        /// </summary>
        /// <param name="name">نام مورد نظر برای ستون</param>
        public ColumnViewConfig(string name)
            : this()
        {
            Name = name;
        }

        /// <summary>
        /// نام ستون که در سرستون لیست اطلاعاتی نمایش داده می شود
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// پهنای ستون بر مبنای واحد مورد استفاده در زیرساخت واسط کاربری برنامه.
        /// در صورت مقدار نداشتن، پهنای ستون به صورت خودکار تنظیم می شود
        /// </summary>
        public int? Width { get; set; }

        /// <summary>
        /// ایندکس ترتیبی ستون که از صفر شروع می شود. در صورت مقدار نداشتن،
        /// ترتیب قرار گرفتن ستون با توجه به مدل نمایشی تنظیم می شود
        /// </summary>
        public int? Index { get; set; }

        /// <summary>
        /// وضعیت نمایشی ستون در نمای لیستی که می تواند یکی از مقادیر
        /// عدم نمایش دائمی، نمایش دائمی، نمایش یا عدم نمایش را داشته باشد
        /// </summary>
        public string Visibility { get; set; }
    }
}
