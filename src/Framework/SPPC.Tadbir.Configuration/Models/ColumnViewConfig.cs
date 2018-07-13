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
            Name = "Column";
            Large = new ColumnViewDeviceConfig();
            Medium = new ColumnViewDeviceConfig();
            Small = new ColumnViewDeviceConfig();
            ExtraSmall = new ColumnViewDeviceConfig();
            ExtraLarge = new ColumnViewDeviceConfig();
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
        /// تنظیمات نمایشی ستون برای صفحه نمایش بزرگ، مانند صفحه نمایش کامپیوتر شخصی یا لپ تاپ
        /// </summary>
        public ColumnViewDeviceConfig Large { get; set; }

        /// <summary>
        /// تنظیمات نمایشی ستون برای صفحه نمایش متوسط، مانند صفحه نمایش نت بوک و تبلت های بزرگ
        /// </summary>
        public ColumnViewDeviceConfig Medium { get; set; }

        /// <summary>
        /// تنظیمات نمایشی ستون برای صفحه نمایش کوچک، مانند صفحه نمایش تبلت
        /// </summary>
        public ColumnViewDeviceConfig Small { get; set; }

        /// <summary>
        /// تنظیمات نمایشی ستون برای صفحه نمایش خیلی کوچک، مانند صفحه نمایش تلفن همراه
        /// </summary>
        public ColumnViewDeviceConfig ExtraSmall { get; set; }
        
        /// <summary>
        /// تنظیمات نمایشی ستون برای صفحه نمایش  های خیلی بزرگ
        /// </summary>
        public ColumnViewDeviceConfig ExtraLarge { get; set; }
    }
}
