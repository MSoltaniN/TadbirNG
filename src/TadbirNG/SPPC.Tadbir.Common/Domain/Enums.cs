using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Domain
{
    /// <summary>
    /// مقادیر تعریف شده برای محدوده دسترسی به موجودیت ها در سطح شعبه های سازمانی را تعریف می کند
    /// </summary>
    public enum BranchScope
    {
        /// <summary>
        /// دسترسی در سطح کلیه شعب
        /// </summary>
        AllBranches = 0,

        /// <summary>
        /// دسترسی در سطح شعبه جاری و تمام شعب زیرمجموعه
        /// </summary>
        CurrentBranchAndChildren = 1,

        /// <summary>
        /// دسترسی در سطح شعبه جاری
        /// </summary>
        CurrentBranch = 2
    }

    /// <summary>
    /// نوع انتخاب حساب برای اضافه کردن در مجموعه حساب
    /// </summary>
    public enum TypeLevel
    {
        /// <summary>
        /// تمام حساب ها
        /// </summary>
        AllAccounts = 0,

        /// <summary>
        /// حساب های در سطح آخر
        /// </summary>
        LeafAccounts = 1,

        /// <summary>
        /// حساب های غیر از سطح آخر
        /// </summary>
        NonLeafAccounts = 2
    }

    /// <summary>
    /// انواع سیستمی اسناد را تعریف می کند
    /// </summary>
    public enum VoucherType
    {
        /// <summary>
        /// نوع سیستمی سند عادی
        /// </summary>
        NormalVoucher = 0,
    }

    /// <summary>
    /// انواع مفهومی سند مالی را تعریف می کند
    /// </summary>
    public enum SubjectType
    {
        /// <summary>
        /// نوع مفهومی سند عادی
        /// </summary>
        Normal = 0,

        /// <summary>
        /// نوع مفهومی سند پیش نویس
        /// </summary>
        Draft = 1,

        /// <summary>
        /// نوع مفهومی سند بودجه
        /// </summary>
        Budgeting = 2,

        /// <summary>
        /// فیلد کمکی برای در نظر گرفتن همه انواع مفهومی سند در لیست ها و گزارش ها
        /// </summary>
        All = 3
    }

    /// <summary>
    /// داده شمارشی برای انواع آرتیکل های سند مالی
    /// </summary>
    public enum VoucherLineType
    {
        /// <summary>
        /// آرتیکل عادی
        /// </summary>
        NormalLine = 0,

        /// <summary>
        /// آرتیکل مالیات و عوارض
        /// </summary>
        TaxAndToll = 1,

        /// <summary>
        /// آرتیکل اصلاحی
        /// </summary>
        Revised = 2
    }

    /// <summary>
    /// حالات ممکن برای محدودیت ثبت مالی را در سرفصل های حسابداری تعریف می کند
    /// </summary>
    public enum TurnoverMode : short
    {
        /// <summary>
        /// نداشتن محدودیت برای ثبت مالی
        /// </summary>
        Unlimited = -1,

        /// <summary>
        /// محدودیت بدهکار در طول دوره
        /// </summary>
        DebtorDuringPeriod = 0,

        /// <summary>
        /// محدودیت بستانکار در طول دوره
        /// </summary>
        CreditorDuringPeriod = 1,

        /// <summary>
        /// محدودیت بدهکار در انتهای دوره
        /// </summary>
        DebtorEndPeriod = 2,

        /// <summary>
        /// محدودیت بستانکار در انتهای دوره
        /// </summary>
        CreditorEndPeriod = 3
    }

    /// <summary>
    /// داده شمارشی برای انواع تقویم مورد استفاده در برنامه
    /// </summary>
    public enum CalendarType
    {
        /// <summary>
        /// تقویم شمسی یا جلالی
        /// </summary>
        Jalali = 0,

        /// <summary>
        /// تقویم میلادی
        /// </summary>
        Gregorian = 1,

        /// <summary>
        /// تقویم پیش فرض در زبان جاری برنامه
        /// </summary>
        Default = 2
    }

    /// <summary>
    /// داده شمارشی برای سطوح مختلف کنترل و مدیریت تنظیمات
    /// </summary>
    public enum ConfigType
    {
        /// <summary>
        /// سطح دسترسی برای تنظیمات حساس برنامه که معمولاً با رمز خاصی قابل مشاهده و تغییر است
        /// </summary>
        SuperAdmin = 1,

        /// <summary>
        /// سطح دسترسی برای تنظیمات راهبری که در اختیار تمام کاربران نقش راهبر سیستم قرار می گیرد
        /// </summary>
        Admin = 2,

        /// <summary>
        /// سطح دسترسی عمومی که در اختیار تمام کاربران برنامه قرار دارد
        /// </summary>
        User = 3
    }

    /// <summary>
    /// داده شمارشی برای سطوح مختلف اعمال تنظیمات در برنامه
    /// </summary>
    public enum ConfigScopeType
    {
        /// <summary>
        /// اعمال تنظیمات در سطح کل برنامه
        /// </summary>
        Global = 0,

        /// <summary>
        /// اعمال تنظیمات در سطح زیرسیستم
        /// </summary>
        Subsystem = 1,

        /// <summary>
        /// اعمال تنظیمات در سطح موجودیت یا نمای اطلاعاتی
        /// </summary>
        Entity = 2,

        /// <summary>
        /// اعمال تنظیمات در سطح فرم یا گزارش - یکسان برای تمام نماهای فرم یا گزارش
        /// </summary>
        Form = 3
    }

    /// <summary>
    /// داده شمارشی برای واحد زمانی مورد نیاز برای نمایش گرافیکی ویجت نموداری
    /// </summary>
    public enum WidgetDateUnit
    {
        /// <summary>
        /// واحد زمانی نامشخص
        /// </summary>
        None = 0,

        /// <summary>
        /// نمایش نمودار زمانی به صورت روزانه
        /// </summary>
        Daily = 1,

        /// <summary>
        /// نمایش نمودار زمانی به صورت هفتگی
        /// </summary>
        Weekly = 2,

        /// <summary>
        /// نمایش نمودار زمانی به صورت ماهانه
        /// </summary>
        Monthly = 3,

        /// <summary>
        /// نمایش نمودار زمانی به صورت فصلی
        /// </summary>
        Quarterly = 4
    }

    /// <summary>
    /// داده شمارشی برای وضعیت برگه دسته چک
    /// </summary>
    public enum CheckBookPageState:short
    {
        /// <summary>
        /// وضعیت نامشخص برای برگه چک
        /// </summary>
        None = 0,

        /// <summary>
        /// برگه چک سفید
        /// </summary>
        Blank = 1,

        /// <summary>
        ///برگه چک استفاده شده
        /// </summary>
        Used = 2,

        /// <summary>
        /// برگه چک ابطال شده
        /// </summary>
        Canceled = 3
    }

    /// <summary>
    /// انواع منابع یا مصارف را تعریف می کند
    /// </summary>
    public enum SourceAppType
    {
        /// <summary>
        /// نوع منبع
        /// </summary>
        Source = 0,

        /// <summary>
        /// نوع مصرف
        /// </summary>
        Application = 1,

        /// <summary>
        /// همه منابع/مصارف
        /// </summary>
        Both = 2
    }

    /// <summary>
    /// نوع فرم دریافت/پرداخت
    /// </summary>
    public enum PayReceiveType
    {
        /// <summary>
        /// دریافت 
        /// </summary>
        Receipt = 0,

        /// <summary>
        /// پرداخت
        /// </summary>
        Payment = 1,
    }

    /// <summary>
    /// وضعیت فعال بودن فرم‌های اطلاعات پایه
    /// </summary>
    public enum ActiveState
    {
        /// <summary>
        /// وضعیت فعال
        /// </summary>
        Active = 0,

        /// <summary>
        /// وضعیت غیر فعال
        /// </summary>
        Inactive = 1,

        /// <summary>
        /// همه وضعیت‌ها
        /// </summary>
        All = 2
    }
}
