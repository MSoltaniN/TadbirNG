﻿using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Configuration.Models
{
    /// <summary>
    /// اطلاعات تنظیمات نمایشی یک ستون از نمای لیستی را برای یک اندازه صفحه نمایش خاص نگهداری می کند
    /// </summary>
    public class ColumnViewDeviceConfig : ICloneable
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public ColumnViewDeviceConfig()
        {
            Visibility = ColumnVisibility.Default;
        }

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
        /// ایندکس ترتیبی استفاده شده برای طراحی و نمایش زمان اجرای لیست که استفاده داخلی دارد
        /// </summary>
        public int DesignIndex { get; set; }

        /// <summary>
        /// وضعیت نمایشی ستون در نمای لیستی که می تواند یکی از مقادیر
        /// عدم نمایش دائمی، نمایش دائمی، نمایش یا عدم نمایش را داشته باشد
        /// </summary>
        public string Visibility { get; set; }

        /// <summary>
        /// عنوان نمایشی ستون در نمای لیستی که بصورت فارسی یا انگلیسی است و برای تمایش در فرم تنظیمات بکار میرود
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// آبجکت جدیدی با مقادیر متناظر با آبجکت فعلی ساخته و برمی گرداند
        /// </summary>
        /// <returns>آبجکت جدیدی با مقادیر متناظر با آبجکت فعلی</returns>
        public object Clone()
        {
            return new ColumnViewDeviceConfig()
            {
                DesignIndex = DesignIndex,
                Index = Index,
                Title = Title,
                Visibility = Visibility,
                Width = Width
            };
        }
    }
}
