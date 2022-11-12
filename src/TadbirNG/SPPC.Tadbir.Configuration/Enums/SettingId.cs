using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Configuration
{
    /// <summary>
    /// داده شمارشی برای شناسه های انواع تنظیمات برنامه
    /// </summary>
    public enum SettingId
    {
        /// <summary>
        /// تنظیمات نامشخص
        /// </summary>
        None = 0,

        /// <summary>
        /// تنظیمات ارتباطات بردار حساب
        /// </summary>
        AccountRelations = 1,

        /// <summary>
        /// تنظیمات محدوده تاریخی پیش فرض
        /// </summary>
        DateRangeFilter = 2,

        /// <summary>
        /// تنظیمات عددی و ارزی
        /// </summary>
        NumberCurrency = 3,

        /// <summary>
        /// تنظیمات فرم های لیستی
        /// </summary>
        ListFormView = 4,

        /// <summary>
        /// تنظیمات ساختارهای درختی در موجودیت های سلسله مراتبی
        /// </summary>
        ViewTree = 5,

        /// <summary>
        /// تنظیمات جستجوی سریع
        /// </summary>
        QuickSearch = 6,

        /// <summary>
        /// تنظیمات گزارش فوری
        /// </summary>
        QuickReport = 7,

        /// <summary>
        /// تنظیمات پیکربندی سیستم
        /// </summary>
        SystemConfiguration = 8,

        /// <summary>
        /// تنظیمات دائمی تراز آزمایشی و گزارش های مشابه
        /// </summary>
        TestBalance = 9,

        /// <summary>
        /// تنظیمات سفارشی سازی عناوین در گزارش های خاص
        /// </summary>
        FormLabel = 10,

        /// <summary>
        /// تنظیمات کاربری در رابطه با زیرسیستم های مختلف
        /// </summary>
        UserProfile = 11
    }
}
