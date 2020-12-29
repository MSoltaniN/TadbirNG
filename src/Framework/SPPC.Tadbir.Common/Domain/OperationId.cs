using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// کد عملیاتی مورد استفاده در مدیریت رویدادها
    /// </summary>
    public enum OperationId
    {
        /// <summary>
        /// کد عملیاتی نامشخص
        /// </summary>
        None = 0,

        /// <summary>
        /// کد عملیاتی مشاهده
        /// </summary>
        View = 1,

        /// <summary>
        /// کد عملیاتی ایجاد موجودیت اصلی
        /// </summary>
        Create = 2,

        /// <summary>
        /// کد عملیاتی اصلاح موجودیت اصلی
        /// </summary>
        Edit = 3,

        /// <summary>
        /// کد عملیاتی حذف موجودیت اصلی
        /// </summary>
        Delete = 4,

        /// <summary>
        /// کد عملیاتی فیلتر پیشرفته
        /// </summary>
        Filter = 5,

        /// <summary>
        /// کد عملیاتی چاپ
        /// </summary>
        Print = 6,

        /// <summary>
        /// کد عملیاتی ذخیره تغییرات
        /// </summary>
        Save = 7,

        /// <summary>
        /// کد عملیاتی بایگانی اطلاعات
        /// </summary>
        Archive = 8,

        /// <summary>
        /// کد عملیاتی تنظیم به عنوان پیش فرض
        /// </summary>
        SetDefault = 9,

        /// <summary>
        /// کد عملیاتی طراحی گزارش چاپی
        /// </summary>
        Design = 10,

        /// <summary>
        /// کد عملیاتی ثبت موجودیت عملیاتی
        /// </summary>
        Check = 11,

        /// <summary>
        /// کد عملیاتی برگشت از ثبت موجودیت عملیاتی
        /// </summary>
        UndoCheck = 12,

        /// <summary>
        /// کد عملیاتی تأیید موجودیت عملیاتی
        /// </summary>
        Confirm = 13,

        /// <summary>
        /// کد عملیاتی برگشت از تأیید موجودیت عملیاتی
        /// </summary>
        UndoConfirm = 14,

        /// <summary>
        /// کد عملیاتی تصویب موجودیت عملیاتی
        /// </summary>
        Approve = 15,

        /// <summary>
        /// کد عملیاتی برگشت از تصویب موجودیت عملیاتی
        /// </summary>
        UndoApprove = 16,

        /// <summary>
        /// کد عملیاتی ثبت قطعی موجودیت عملیاتی
        /// </summary>
        Finalize = 17,

        /// <summary>
        /// کد عملیاتی برگشت از ثبت قطعی موجودیت عملیاتی
        /// </summary>
        UndoFinalize = 18,

        /// <summary>
        /// کد عملیاتی نشانه گذاری آرتیکل موجودیت عملیاتی
        /// </summary>
        Mark = 19,

        /// <summary>
        /// کد عملیاتی طراحی گزارش فوری
        /// </summary>
        QuickReportDesign = 20,

        /// <summary>
        /// کد عملیاتی حذف گروهی موجودیت اصلی
        /// </summary>
        GroupDelete = 21,

        /// <summary>
        /// کد عملیاتی ورود ناموفق به برنامه
        /// </summary>
        FailedLogin = 22,

        /// <summary>
        /// کد عملیاتی ورود به شرکت
        /// </summary>
        CompanyLogin = 23,

        /// <summary>
        /// کد عملیاتی انتخاب دوره مالی
        /// </summary>
        SwitchFiscalPeriod = 24,

        /// <summary>
        /// کد عملیاتی انتخاب شعبه
        /// </summary>
        SwitchBranch = 25,

        /// <summary>
        /// کد عملیاتی تخصیص نقش به کاربر
        /// </summary>
        AssignRole = 26,

        /// <summary>
        /// کد عملیاتی تخصیص کاربر به نقش
        /// </summary>
        AssignUser = 27,

        /// <summary>
        /// کد عملیاتی تعیین دسترسی به شعبه
        /// </summary>
        BranchAccess = 28,

        /// <summary>
        /// کد عملیاتی تعیین دسترسی به دوره مالی
        /// </summary>
        FiscalPeriodAccess = 29,

        /// <summary>
        /// کد عملیاتی مشاهده بایگانی
        /// </summary>
        ViewArchive = 30,

        /// <summary>
        /// کد عملیاتی تغییر تقویم پیش فرض
        /// </summary>
        CalendarChange = 31,

        /// <summary>
        /// کد عملیاتی تغییر ارز پایه
        /// </summary>
        CurrencyChange = 32,

        /// <summary>
        /// کد عملیاتی تغییر تعداد اعشار
        /// </summary>
        DecimalCountChange = 33,

        /// <summary>
        /// کد عملیاتی تغییر تنظیمات کدینگ پیش فرض
        /// </summary>
        DefaultCodingChange = 34,

        /// <summary>
        /// کد عملیاتی دسترسی نقش به ساختار اطلاعاتی
        /// </summary>
        RoleAccess = 35,

        /// <summary>
        /// کد عملیاتی ایجاد آرتیکل موجودیت عملیاتی
        /// </summary>
        CreateLine = 36,

        /// <summary>
        /// کد عملیاتی اصلاح آرتیکل موجودیت عملیاتی
        /// </summary>
        EditLine = 37,

        /// <summary>
        /// کد عملیاتی حذف آرتیکل موجودیت عملیاتی
        /// </summary>
        DeleteLine = 38,

        /// <summary>
        /// کد عملیاتی حذف گروهی آرتیکل موجودیت عملیاتی
        /// </summary>
        GroupDeleteLines = 39,

        /// <summary>
        /// کد عملیاتی ایجاد نرخ ارز
        /// </summary>
        CreateRate = 40,

        /// <summary>
        /// کد عملیاتی اصلاح نرخ ارز
        /// </summary>
        EditRate = 41,

        /// <summary>
        /// کد عملیاتی حذف نرخ ارز
        /// </summary>
        DeleteRate = 42,

        /// <summary>
        /// کد عملیاتی چاپ نرخ های ارز
        /// </summary>
        PrintRates = 43,

        /// <summary>
        /// کد عملیاتی حذف گروهی نرخ های ارز
        /// </summary>
        GroupDeleteRates = 44,

        /// <summary>
        /// کد عملیاتی مشاهده نرخ های ارز
        /// </summary>
        ViewRates = 45,

        /// <summary>
        /// کد عملیاتی ثبت گروهی اسناد
        /// </summary>
        GroupCheck = 46,

        /// <summary>
        /// کد عملیاتی برگشت از ثبت گروهی اسناد
        /// </summary>
        GroupUndoCheck = 47,

        /// <summary>
        /// کد عملیاتی ثبت قطعی گروهی اسناد
        /// </summary>
        GroupFinalize = 48,

        /// <summary>
        /// کد عملیاتی برگشت از ثبت قطعی گروهی اسناد
        /// </summary>
        GroupUndoFinalize = 49,

        /// <summary>
        /// کد عملیاتی تایید گروهی اسناد
        /// </summary>
        GroupConfirm = 50,

        /// <summary>
        /// کد عملیاتی رفع تایید گروهی اسناد
        /// </summary>
        GroupUndoConfirm = 51,

        /// <summary>
        /// کد عملیاتی تبدیل به سند عادی
        /// </summary>
        Normalize = 52,

        /// <summary>
        /// کد عملیاتی تبدیل گروهی به سند عادی
        /// </summary>
        GroupNormalize = 53,

        /// <summary>
        /// کد عملیاتی ارسال اطلاعات لیست به فایل
        /// </summary>
        Export = 54,

        /// <summary>
        /// کد عملیاتی ارسال اطلاعات نرخ های ارز به فایل
        /// </summary>
        ExportRates = 55,

        /// <summary>
        /// کد عملیاتی فیلتر پیشرفته نرخ های ارز
        /// </summary>
        FilterRates = 56,
    }
}
