using System;

namespace SPPC.Tadbir.Security
{
    /// <summary>
    /// فلگ های مشترک تعریف شده برای دسترسی های مشاهده ای فهرست های اطلاعاتی را تعریف می کند
    /// </summary>
    [Flags]
    public enum ViewPermissions
    {
        /// <summary>
        /// نداشتن دسترسی مشاهده فهرست اطلاعاتی
        /// </summary>
        None = 0x0,

        /// <summary>
        /// دسترسی مشاهده فهرست اطلاعاتی
        /// </summary>
        View = 0x1,

        /// <summary>
        /// دسترسی فیلتر پیشرفته فهرست اطلاعاتی
        /// </summary>
        Filter = 0x2,

        /// <summary>
        /// دسترسی چاپ فهرست اطلاعاتی
        /// </summary>
        Print = 0x4,

        /// <summary>
        /// دسترسی ارسال فهرست اطلاعاتی به اکسل
        /// </summary>
        Export = 0x8
    }

    /// <summary>
    /// فلگ های تعریف شده برای دسترسی های امنیتی به یک سرفصل حسابداری را تعریف می کند
    /// </summary>
    [Flags]
    public enum AccountPermissions
    {
        /// <summary>
        /// عدم دسترسی به یک سرفصل حسابداری
        /// </summary>
        None = 0x0,

        /// <summary>
        /// دسترسی مشاهده لیست سرفصل های حسابداری یا جزییات یک سرفصل حسابداری
        /// </summary>
        View = 0x1,

        /// <summary>
        /// دسترسی فیلتر سرفصل های حسابداری موجود
        /// </summary>
        Filter = 0x2,

        /// <summary>
        /// دسترسی چاپ سرفصل های حسابداری موجود
        /// </summary>
        Print = 0x4,

        /// <summary>
        /// دسترسی ارسال اطلاعات سرفصل های حسابداری موجود
        /// </summary>
        Export = 0x8,

        /// <summary>
        /// دسترسی ایجاد یک سرفصل حسابداری جدید
        /// </summary>
        Create = 0x10,

        /// <summary>
        /// دسترسی ویرایش یک سرفصل حسابداری موجود
        /// </summary>
        Edit = 0x20,

        /// <summary>
        /// دسترسی حذف یک سرفصل حسابداری موجود
        /// </summary>
        Delete = 0x40,

        /// <summary>
        /// دسترسی کامل به عملیات تعریف شده برای مدیریت سرفصل حسابداری
        /// </summary>
        All = 0x7f
    }

    /// <summary>
    /// فلگ های تعریف شده برای دسترسی های امنیتی به تفصیلی های شناور را تعریف می کند
    /// </summary>
    [Flags]
    public enum DetailAccountPermissions
    {
        /// <summary>
        /// عدم دسترسی به یک تفصیلی شناور
        /// </summary>
        None = 0x0,

        /// <summary>
        /// دسترسی مشاهده لیست تفصیلی های شناور یا جزییات یک تفصیلی شناور
        /// </summary>
        View = 0x1,

        /// <summary>
        /// دسترسی فیلتر تفصیلی های شناور موجود
        /// </summary>
        Filter = 0x2,

        /// <summary>
        /// دسترسی چاپ تفصیلی های شناور موجود
        /// </summary>
        Print = 0x4,

        /// <summary>
        /// دسترسی ارسال اطلاعات تفصیلی های شناور موجود
        /// </summary>
        Export = 0x8,

        /// <summary>
        /// دسترسی ایجاد یک تفصیلی شناور جدید
        /// </summary>
        Create = 0x10,

        /// <summary>
        /// دسترسی ویرایش یک تفصیلی شناور موجود
        /// </summary>
        Edit = 0x20,

        /// <summary>
        /// دسترسی حذف یک تفصیلی شناور موجود
        /// </summary>
        Delete = 0x40,

        /// <summary>
        /// دسترسی کامل به عملیات تعریف شده برای مدیریت تفصیلی شناور
        /// </summary>
        All = 0x7f
    }

    /// <summary>
    /// فلگ های تعریف شده برای دسترسی های امنیتی به مراکز هزینه را تعریف می کند
    /// </summary>
    [Flags]
    public enum CostCenterPermissions
    {
        /// <summary>
        /// عدم دسترسی به مراکز هزینه
        /// </summary>
        None = 0x0,

        /// <summary>
        /// دسترسی مشاهده لیست مراکز هزینه یا جزییات یک مرکز هزینه
        /// </summary>
        View = 0x1,

        /// <summary>
        /// دسترسی فیلتر مراکز هزینه موجود
        /// </summary>
        Filter = 0x2,

        /// <summary>
        /// دسترسی چاپ مراکز هزینه موجود
        /// </summary>
        Print = 0x4,

        /// <summary>
        /// دسترسی ارسال اطلاعات مراکز هزینه موجود
        /// </summary>
        Export = 0x8,

        /// <summary>
        /// دسترسی ایجاد یک مرکز هزینه جدید
        /// </summary>
        Create = 0x10,

        /// <summary>
        /// دسترسی ویرایش یک مرکز هزینه موجود
        /// </summary>
        Edit = 0x20,

        /// <summary>
        /// دسترسی حذف یک مرکز هزینه موجود
        /// </summary>
        Delete = 0x40,

        /// <summary>
        /// دسترسی کامل به عملیات تعریف شده برای مدیریت مرکز هزینه
        /// </summary>
        All = 0x7f
    }

    /// <summary>
    /// فلگ های تعریف شده برای دسترسی های امنیتی به پروژه ها را تعریف می کند
    /// </summary>
    [Flags]
    public enum ProjectPermissions
    {
        /// <summary>
        /// عدم دسترسی به پروژه ها
        /// </summary>
        None = 0x0,

        /// <summary>
        /// دسترسی مشاهده لیست پروژه ها یا جزییات یک پروژه
        /// </summary>
        View = 0x1,

        /// <summary>
        /// دسترسی فیلتر پروژه های موجود
        /// </summary>
        Filter = 0x2,

        /// <summary>
        /// دسترسی چاپ پروژه های موجود
        /// </summary>
        Print = 0x4,

        /// <summary>
        /// دسترسی ارسال اطلاعات پروژه های موجود
        /// </summary>
        Export = 0x8,

        /// <summary>
        /// دسترسی ایجاد یک پروژه جدید
        /// </summary>
        Create = 0x10,

        /// <summary>
        /// دسترسی ویرایش یک پروژه موجود
        /// </summary>
        Edit = 0x20,

        /// <summary>
        /// دسترسی حذف یک پروژه موجود
        /// </summary>
        Delete = 0x40,

        /// <summary>
        /// دسترسی کامل به عملیات تعریف شده برای مدیریت پروژه
        /// </summary>
        All = 0x7f
    }

    /// <summary>
    /// فلگ های تعریف شده برای دسترسی های امنیتی به دوره های مالی را تعریف می کند
    /// </summary>
    [Flags]
    public enum FiscalPeriodPermissions
    {
        /// <summary>
        /// عدم دسترسی به دوره های مالی
        /// </summary>
        None = 0x0,

        /// <summary>
        /// دسترسی مشاهده لیست دوره های مالی یا جزییات یک دوره مالی
        /// </summary>
        View = 0x1,

        /// <summary>
        /// دسترسی فیلتر دوره های مالی موجود
        /// </summary>
        Filter = 0x2,

        /// <summary>
        /// دسترسی چاپ دوره های مالی موجود
        /// </summary>
        Print = 0x4,

        /// <summary>
        /// دسترسی ارسال اطلاعات دوره های مالی موجود
        /// </summary>
        Export = 0x8,

        /// <summary>
        /// دسترسی ایجاد یک دوره مالی جدید
        /// </summary>
        Create = 0x10,

        /// <summary>
        /// دسترسی ویرایش یک دوره مالی موجود
        /// </summary>
        Edit = 0x20,

        /// <summary>
        /// دسترسی حذف یک دوره مالی موجود
        /// </summary>
        Delete = 0x40,

        /// <summary>
        /// دسترسی اعطای دسترسی به یک دوره مالی برای یک یا چند نقش
        /// </summary>
        AssignRoles = 0x80,

        /// <summary>
        /// دسترسی کامل به عملیات تعریف شده برای مدیریت دوره های مالی
        /// </summary>
        All = 0xff
    }

    /// <summary>
    /// فلگ های تعریف شده برای دسترسی های امنیتی به ارزها را تعریف می کند
    /// </summary>
    [Flags]
    public enum CurrencyPermissions
    {
        /// <summary>
        /// عدم دسترسی به ارزها
        /// </summary>
        None = 0x0,

        /// <summary>
        /// دسترسی مشاهده لیست ارزها یا جزییات یک ارز
        /// </summary>
        View = 0x1,

        /// <summary>
        /// دسترسی فیلتر ارزها در لیست
        /// </summary>
        Filter = 0x2,

        /// <summary>
        /// دسترسی چاپ لیست ارزها
        /// </summary>
        Print = 0x4,

        /// <summary>
        /// دسترسی ارسال اطلاعات ارزهای موجود
        /// </summary>
        Export = 0x8,

        /// <summary>
        /// دسترسی ایجاد یک ارز جدید
        /// </summary>
        Create = 0x10,

        /// <summary>
        /// دسترسی ویرایش یک ارز موجود
        /// </summary>
        Edit = 0x20,

        /// <summary>
        /// دسترسی حذف یک ارز موجود
        /// </summary>
        Delete = 0x40,

        /// <summary>
        /// دسترسی تغییر وضعیت یک ارز از فعال به غیرفعال یا بالعکس
        /// </summary>
        ChangeStatus = 0x80,

        /// <summary>
        /// دسترسی کامل به عملیات تعریف شده برای مدیریت ارزها
        /// </summary>
        All = 0xff
    }

    /// <summary>
    /// فلگ های تعریف شده برای دسترسی های امنیتی به اسناد حسابداری را تعریف می کند
    /// </summary>
    [Flags]
    public enum VoucherPermissions
    {
        /// <summary>
        /// عدم دسترسی به اسناد حسابداری
        /// </summary>
        None = 0x0,

        /// <summary>
        /// دسترسی مشاهده اسناد حسابداری
        /// </summary>
        View = 0x1,

        /// <summary>
        /// دسترسی ایجاد سند حسابداری
        /// </summary>
        Create = 0x2,

        /// <summary>
        /// دسترسی اصلاح سند حسابداری
        /// </summary>
        Edit = 0x4,

        /// <summary>
        /// دسترسی حذف سند حسابداری
        /// </summary>
        Delete = 0x8,

        /// <summary>
        /// دسترسی چاپ اسناد حسابداری
        /// </summary>
        Print = 0x10,

        /// <summary>
        /// دسترسی ایجاد آرتیکل مالی
        /// </summary>
        CreateLine = 0x20,

        /// <summary>
        /// دسترسی اصلاح آرتیکل مالی
        /// </summary>
        EditLine = 0x40,

        /// <summary>
        /// دسترسی حذف آرتیکل مالی
        /// </summary>
        DeleteLine = 0x80,

        /// <summary>
        /// دسترسی ثبت سند حسابداری
        /// </summary>
        Check = 0x100,

        /// <summary>
        /// دسترسی برگشت از ثبت سند حسابداری
        /// </summary>
        UndoCheck = 0x200,

        /// <summary>
        /// دسترسی تایید سند حسابداری
        /// </summary>
        Confirm = 0x400,

        /// <summary>
        /// دسترسی برگشت از تایید سند حسابداری
        /// </summary>
        UndoConfirm = 0x800,

        /// <summary>
        /// دسترسی تصویب سند حسابداری
        /// </summary>
        Approve = 0x1000,

        /// <summary>
        /// دسترسی برگشت از تصویب سند حسابداری
        /// </summary>
        UndoApprove = 0x2000,

        /// <summary>
        /// دسترسی ثبت قطعی سند حسابداری
        /// </summary>
        Finalize = 0x4000,

        /// <summary>
        /// دسترسی حرکت روی اسناد حسابداری
        /// </summary>
        Navigate = 0x8000,

        /// <summary>
        /// کلیه دسترسی های تعریف شده برای اسناد حسابداری
        /// </summary>
        All = 0xffff
    }

    /// <summary>
    /// فلگ های تعریف شده برای دسترسی های امنیتی به فرم مدیریت اسناد حسابداری را تعریف می کند
    /// </summary>
    [Flags]
    public enum ManageVouchersPermissions
    {
        /// <summary>
        /// دسترسی مشاهده اسناد حسابداری
        /// </summary>
        View = 0x1,

        /// <summary>
        /// دسترسی فیلتر اسناد حسابداری
        /// </summary>
        Filter = 0x2,

        /// <summary>
        /// دسترسی چاپ اسناد حسابداری
        /// </summary>
        Print = 0x4,

        /// <summary>
        /// دسترسی ارسال اطلاعات اسناد حسابداری
        /// </summary>
        Export = 0x8,

        /// <summary>
        /// دسترسی ثبت گروهی اسناد حسابداری
        /// </summary>
        GroupCheck = 0x10,

        /// <summary>
        /// دسترسی برگشت از ثبت گروهی اسناد حسابداری
        /// </summary>
        GroupUndoCheck = 0x20,

        /// <summary>
        /// دسترسی تأیید / تصویب گروهی اسناد حسابداری
        /// </summary>
        GroupConfirm = 0x40,

        /// <summary>
        /// دسترسی برگشت از تأیید / تصویب گروهی اسناد حسابداری
        /// </summary>
        GroupUndoConfirm = 0x80,

        /// <summary>
        /// دسترسی ثبت قطعی گروهی اسناد حسابداری
        /// </summary>
        GroupFinalize = 0x100,

        /// <summary>
        /// کلیه دسترسی های تعریف شده برای فرم مدیریت اسناد حسابداری
        /// </summary>
        All = 0x1ff,
    }

    /// <summary>
    /// فلگ های تعریف شده برای دسترسی های امنیتی به اسناد پیش نویس را تعریف می کند
    /// </summary>
    [Flags]
    public enum DraftVoucherPermissions
    {
        /// <summary>
        /// عدم دسترسی به اسناد پیش نویس
        /// </summary>
        None = 0x0,

        /// <summary>
        /// دسترسی مشاهده اسناد پیش نویس
        /// </summary>
        View = 0x1,

        /// <summary>
        /// دسترسی ایجاد سند پیش نویس
        /// </summary>
        Create = 0x2,

        /// <summary>
        /// دسترسی اصلاح سند پیش نویس
        /// </summary>
        Edit = 0x4,

        /// <summary>
        /// دسترسی حذف سند پیش نویس
        /// </summary>
        Delete = 0x8,

        /// <summary>
        /// دسترسی چاپ اسناد پیش نویس
        /// </summary>
        Print = 0x10,

        /// <summary>
        /// دسترسی ایجاد آرتیکل مالی
        /// </summary>
        CreateLine = 0x20,

        /// <summary>
        /// دسترسی اصلاح آرتیکل مالی
        /// </summary>
        EditLine = 0x40,

        /// <summary>
        /// دسترسی حذف آرتیکل مالی
        /// </summary>
        DeleteLine = 0x80,

        /// <summary>
        /// دسترسی ثبت سند پیش نویس
        /// </summary>
        Check = 0x100,

        /// <summary>
        /// دسترسی برگشت از ثبت سند پیش نویس
        /// </summary>
        UndoCheck = 0x200,

        /// <summary>
        /// دسترسی حرکت روی اسناد پیش نویس
        /// </summary>
        Navigate = 0x400,

        /// <summary>
        /// دسترسی تبدیل سند پیش نویس به سند عادی
        /// </summary>
        Normalize = 0x800,

        /// <summary>
        /// کلیه دسترسی های تعریف شده برای اسناد پیش نویس
        /// </summary>
        All = 0xfff
    }

    /// <summary>
    /// فلگ های تعریف شده برای دسترسی های امنیتی به فرم مدیریت اسناد پیش نویس را تعریف می کند
    /// </summary>
    [Flags]
    public enum ManageDraftVouchersPermissions
    {
        /// <summary>
        /// دسترسی مشاهده اسناد پیش نویس
        /// </summary>
        View = 0x1,

        /// <summary>
        /// دسترسی فیلتر اسناد پیش نویس
        /// </summary>
        Filter = 0x2,

        /// <summary>
        /// دسترسی چاپ اسناد پیش نویس
        /// </summary>
        Print = 0x4,

        /// <summary>
        /// دسترسی ارسال اطلاعات اسناد پیش نویس
        /// </summary>
        Export = 0x8,

        /// <summary>
        /// دسترسی ثبت گروهی اسناد پیش نویس
        /// </summary>
        GroupCheck = 0x10,

        /// <summary>
        /// دسترسی برگشت از ثبت گروهی اسناد پیش نویس
        /// </summary>
        GroupUndoCheck = 0x20,

        /// <summary>
        /// کلیه دسترسی های تعریف شده برای فرم مدیریت اسناد حسابداری
        /// </summary>
        All = 0x3f
    }

    /// <summary>
    /// فلگ های تعریف شده برای دسترسی های امنیتی به عملیات ویژه اسناد مالی را تعریف می کند
    /// </summary>
    [Flags]
    public enum SpecialVoucherPermissions
    {
        /// <summary>
        /// عدم دسترسی به عملیات ویژه اسناد مالی
        /// </summary>
        None = 0x0,

        /// <summary>
        /// دسترسی به عملیات صدور سند افتتاحیه
        /// </summary>
        IssueOpeningVoucher = 0x1,

        /// <summary>
        /// دسترسی به عملیات صدور سند بستن حسابهای موقت
        /// </summary>
        IssueClosingTempAccountsVoucher = 0x2,

        /// <summary>
        /// دسترسی به عملیات صدور سند اختتامیه
        /// </summary>
        IssueClosingVoucher = 0x4,

        /// <summary>
        /// دسترسی به عملیات برگشت سند اختتامیه
        /// </summary>
        UncheckClosingVoucher = 0x8,

        /// <summary>
        /// دسترسی کامل به عملیات ویژه اسناد مالی
        /// </summary>
        All = 0xf
    }

    /// <summary>
    /// فلگ های تعریف شده برای دسترسی های امنیتی به شعب سازمانی را تعریف می کند
    /// </summary>
    [Flags]
    public enum BranchPermissions
    {
        /// <summary>
        /// عدم دسترسی به شعب سازمانی
        /// </summary>
        None = 0x0,

        /// <summary>
        /// دسترسی مشاهده لیست شعب سازمانی یا جزییات یک شعبه سازمانی
        /// </summary>
        View = 0x1,

        /// <summary>
        /// دسترسی فیلتر شعبه ها در لیست
        /// </summary>
        Filter = 0x2,

        /// <summary>
        /// دسترسی چاپ لیست شعبه ها
        /// </summary>
        Print = 0x4,

        /// <summary>
        /// دسترسی ارسال اطلاعات شعبه های موجود
        /// </summary>
        Export = 0x8,

        /// <summary>
        /// دسترسی ایجاد یک شعبه سازمانی جدید
        /// </summary>
        Create = 0x10,

        /// <summary>
        /// دسترسی ویرایش یک شعبه سازمانی موجود
        /// </summary>
        Edit = 0x20,

        /// <summary>
        /// دسترسی حذف یک شعبه سازمانی موجود
        /// </summary>
        Delete = 0x40,

        /// <summary>
        /// دسترسی اعطای دسترسی به یک شعبه برای یک یا چند نقش
        /// </summary>
        AssignRoles = 0x80,

        /// <summary>
        /// دسترسی کامل به عملیات تعریف شده برای مدیریت شعب سازمانی
        /// </summary>
        All = 0xff
    }

    /// <summary>
    /// فلگ های تعریف شده برای دسترسی های امنیتی به شرکت ها را تعریف می کند
    /// </summary>
    [Flags]
    public enum CompanyPermissions
    {
        /// <summary>
        /// عدم دسترسی به شرکت ها
        /// </summary>
        None = 0x0,

        /// <summary>
        /// دسترسی مشاهده لیست شرکت ها یا جزییات یک شرکت
        /// </summary>
        View = 0x1,

        /// <summary>
        /// دسترسی فیلتر شرکت ها در لیست
        /// </summary>
        Filter = 0x2,

        /// <summary>
        /// دسترسی چاپ لیست شرکت ها
        /// </summary>
        Print = 0x4,

        /// <summary>
        /// دسترسی ارسال اطلاعات شرکت های موجود
        /// </summary>
        Export = 0x8,

        /// <summary>
        /// دسترسی کامل به عملیات تعریف شده برای مدیریت شرکت ها
        /// </summary>
        All = 0xf
    }

    /// <summary>
    /// فلگ های تعریف شده برای دسترسی های امنیتی به کاربران را تعریف می کند
    /// </summary>
    [Flags]
    public enum UserPermissions
    {
        /// <summary>
        /// عدم دسترسی به کاربران
        /// </summary>
        None = 0x0,

        /// <summary>
        /// دسترسی مشاهده لیست کاربران یا جزییات یک کاربر
        /// </summary>
        View = 0x1,

        /// <summary>
        /// دسترسی فیلتر کاربران در لیست
        /// </summary>
        Filter = 0x2,

        /// <summary>
        /// دسترسی چاپ لیست کاربران
        /// </summary>
        Print = 0x4,

        /// <summary>
        /// دسترسی ارسال اطلاعات کاربران موجود
        /// </summary>
        Export = 0x8,

        /// <summary>
        /// دسترسی ایجاد یک کاربر جدید
        /// </summary>
        Create = 0x10,

        /// <summary>
        /// دسترسی ویرایش یک کاربر موجود
        /// </summary>
        Edit = 0x20,

        /// <summary>
        /// دسترسی تخصیص نقش به یک یا چند کاربر
        /// </summary>
        AssignRoles = 0x40,

        /// <summary>
        /// دسترسی کامل به عملیات تعریف شده برای مدیریت کاربران
        /// </summary>
        All = 0x7f
    }

    /// <summary>
    /// فلگ های تعریف شده برای دسترسی های امنیتی به نقش ها را تعریف می کند
    /// </summary>
    [Flags]
    public enum RolePermissions
    {
        /// <summary>
        /// عدم دسترسی به نقش ها
        /// </summary>
        None = 0x0,

        /// <summary>
        /// دسترسی مشاهده لیست نقش ها یا جزییات یک نقش
        /// </summary>
        View = 0x1,

        /// <summary>
        /// دسترسی فیلتر نقش ها در لیست
        /// </summary>
        Filter = 0x2,

        /// <summary>
        /// دسترسی چاپ لیست نقش ها
        /// </summary>
        Print = 0x4,

        /// <summary>
        /// دسترسی ارسال اطلاعات نقش های موجود
        /// </summary>
        Export = 0x8,

        /// <summary>
        /// دسترسی ایجاد یک نقش جدید
        /// </summary>
        Create = 0x10,

        /// <summary>
        /// دسترسی ویرایش یک نقش موجود
        /// </summary>
        Edit = 0x20,

        /// <summary>
        /// دسترسی حذف یک نقش موجود
        /// </summary>
        Delete = 0x40,

        /// <summary>
        /// Indicates permission to add/remove one or more users to/from a role
        /// </summary>
        AssignUsers = 0x80,

        /// <summary>
        /// Indicates permission to allow/disallow access to one or more branches in a role
        /// </summary>
        AssignBranches = 0x100,

        /// <summary>
        /// Indicates permission to allow/disallow access to one or more fiscal periods in a role
        /// </summary>
        AssignFiscalPeriods = 0x200,

        /// <summary>
        /// Indicates all permissions available for managing a role
        /// </summary>
        All = 0x3ff
    }

    /// <summary>
    /// فلگ های تعریف شده برای دسترسی های امنیتی به یک گروه حساب را تعریف می کند
    /// </summary>
    [Flags]
    public enum AccountGroupPermissions
    {
        /// <summary>
        /// عدم دسترسی به گروه های حساب
        /// </summary>
        None = 0x0,

        /// <summary>
        /// دسترسی مشاهده لیست گروه های حساب یا جزییات یک گروه حساب
        /// </summary>
        View = 0x1,

        /// <summary>
        /// دسترسی فیلتر گروه های حساب موجود
        /// </summary>
        Filter = 0x2,

        /// <summary>
        /// دسترسی چاپ گروه های حساب موجود
        /// </summary>
        Print = 0x4,

        /// <summary>
        /// دسترسی ارسال اطلاعات گروه های حساب موجود
        /// </summary>
        Export = 0x8,

        /// <summary>
        /// دسترسی ایجاد یک گروه حساب جدید
        /// </summary>
        Create = 0x10,

        /// <summary>
        /// دسترسی ویرایش یک گروه حساب موجود
        /// </summary>
        Edit = 0x20,

        /// <summary>
        /// دسترسی حذف یک گروه حساب موجود
        /// </summary>
        Delete = 0x40,

        /// <summary>
        /// دسترسی کامل به عملیات تعریف شده برای مدیریت گروه حساب
        /// </summary>
        All = 0x7f
    }

    /// <summary>
    /// فلگ های تعریف شده برای دسترسی های امنیتی به یک مجموعه حساب را تعریف می کند
    /// </summary>
    [Flags]
    public enum AccountCollectionPermissions
    {
        /// <summary>
        /// عدم دسترسی به مجموعه های حساب
        /// </summary>
        None = 0x0,

        /// <summary>
        /// دسترسی مشاهده مجموعه های حساب
        /// </summary>
        View = 0x1,

        /// <summary>
        /// دسترسی ذخیره حساب های یک مجموعه حساب
        /// </summary>
        Save = 0x2,

        /// <summary>
        /// دسترسی کامل به عملیات تعریف شده برای مدیریت مجموعه حساب
        /// </summary>
        All = 0x3
    }

    /// <summary>
    /// فلگ های تعریف شده برای دسترسی های امنیتی به لاگ های عملیاتی شرکتی را تعریف می کند
    /// </summary>
    [Flags]
    public enum OperationLogPermissions
    {
        /// <summary>
        /// عدم دسترسی به لاگ های عملیاتی شرکتی
        /// </summary>
        None = 0x0,

        /// <summary>
        /// دسترسی مشاهده لاگ های عملیاتی شرکتی
        /// </summary>
        View = 0x1,

        /// <summary>
        /// دسترسی فیلتر لاگ های عملیاتی شرکتی موجود
        /// </summary>
        Filter = 0x2,

        /// <summary>
        /// دسترسی چاپ لاگ های عملیاتی شرکتی موجود
        /// </summary>
        Print = 0x4,

        /// <summary>
        /// دسترسی ارسال اطلاعات لاگ های عملیاتی شرکتی موجود
        /// </summary>
        Export = 0x8,

        /// <summary>
        /// دسترسی بایگانی لاگ های عملیاتی شرکتی
        /// </summary>
        Archive = 0x10,

        /// <summary>
        /// دسترسی مشاهده لاگ های شرکتی بایگانی شده
        /// </summary>
        ViewArchive = 0x20,

        /// <summary>
        /// دسترسی کامل به عملیات تعریف شده برای مدیریت لاگ عملیاتی
        /// </summary>
        All = 0x3f
    }

    /// <summary>
    /// فلگ های تعریف شده برای دسترسی های امنیتی به لاگ های سیستمی را تعریف می کند
    /// </summary>
    [Flags]
    public enum SysOperationLogPermissions
    {
        /// <summary>
        /// عدم دسترسی به لاگ های سیستمی
        /// </summary>
        None = 0x0,

        /// <summary>
        /// دسترسی مشاهده لاگ های سیستمی
        /// </summary>
        View = 0x1,

        /// <summary>
        /// دسترسی فیلتر لاگ های سیستمی موجود
        /// </summary>
        Filter = 0x2,

        /// <summary>
        /// دسترسی چاپ لاگ های سیستمی موجود
        /// </summary>
        Print = 0x4,

        /// <summary>
        /// دسترسی ارسال اطلاعات لاگ های سیستمی موجود
        /// </summary>
        Export = 0x8,

        /// <summary>
        /// دسترسی بایگانی لاگ های عملیاتی سیستمی
        /// </summary>
        Archive = 0x10,

        /// <summary>
        /// دسترسی مشاهده لاگ های سیستمی بایگانی شده
        /// </summary>
        ViewArchive = 0x20,

        /// <summary>
        /// دسترسی کامل به عملیات تعریف شده برای مدیریت لاگ سیستمی
        /// </summary>
        All = 0x3f
    }

    /// <summary>
    /// فلگ های تعریف شده برای دسترسی های امنیتی به بخش مدیریت گزارشات را تعریف می کند
    /// </summary>
    [Flags]
    public enum ReportPermissions
    {
        /// <summary>
        /// عدم دسترسی به مدیریت گزارشات
        /// </summary>
        None = 0x0,

        /// <summary>
        /// دسترسی مشاهده فرم مدیریت گزارشات
        /// </summary>
        View = 0x1,

        /// <summary>
        /// دسترسی تغییر فرم
        /// </summary>
        Design = 0x2,

        /// <summary>
        /// دسترسی تغییر فرم گزارش فوری
        /// </summary>
        QuickReportDesign = 0x4,

        /// <summary>
        /// دسترسی کامل به عملیات تعریف شده برای مدیریت گزارشات
        /// </summary>
        All = 0x7
    }

    /// <summary>
    /// فلگ های تعریف شده برای دسترسی های امنیتی به بخش مدیریت گزارشات کاربری را تعریف می کند
    /// </summary>
    [Flags]
    public enum UserReportPermissions
    {
        /// <summary>
        /// عدم دسترسی به مدیریت گزارشات
        /// </summary>
        None = 0x0,

        /// <summary>
        /// دسترسی ایجاد یا اصلاح گزارش ذخیره شده
        /// </summary>
        Save = 0x1,

        /// <summary>
        /// دسترسی حذف گزارش ذخیره شده
        /// </summary>
        Delete = 0x2,

        /// <summary>
        /// دسترسی تعیین گزارش پیش فرض
        /// </summary>
        SetDefault = 0x4,

        /// <summary>
        /// دسترسی کامل به عملیات تعریف شده برای مدیریت گزارشات کاربری
        /// </summary>
        All = 0x7
    }

    /// <summary>
    /// فلگ های تعریف شده برای دسترسی های امنیتی در مدیریت ارتباطات بین مولفه های بردار حساب را تعریف می کند
    /// </summary>
    [Flags]
    public enum AccountRelationPermissions
    {
        /// <summary>
        /// عدم دسترسی به مدیریت ارتباطات
        /// </summary>
        None = 0x0,

        /// <summary>
        /// دسترسی مشاهده ارتباطات موجود بین مولفه های مختلف
        /// </summary>
        ViewRelationships = 0x1,

        /// <summary>
        /// دسترسی ذخیره ارتباطات بین مولفه های مختلف
        /// </summary>
        SaveRelationships = 0x2,

        /// <summary>
        /// دسترسی کامل به عملیات تعریف شده برای مدیریت ارتباطات بردار حساب
        /// </summary>
        All = 0x3
    }

    /// <summary>
    /// فلگ های تعریف شده برای دسترسی های امنیتی در مدیریت تنظیمات برنامه را تعریف می کند
    /// </summary>
    [Flags]
    public enum SettingPermissions
    {
        /// <summary>
        /// عدم دسترسی به تنظیمات برنامه
        /// </summary>
        None = 0x0,

        /// <summary>
        /// دسترسی مشاهده تنظیمات برنامه
        /// </summary>
        ViewSettings = 0x1,

        /// <summary>
        /// دسترسی ذخیره تنظیمات برنامه
        /// </summary>
        SaveSettings = 0x2,

        /// <summary>
        /// دسترسی کامل به عملیات تعریف شده برای مدیریت تنظیمات برنامه
        /// </summary>
        All = 0x3
    }

    /// <summary>
    /// فلگ های تعریف شده برای دسترسی های امنیتی در مدیریت تنظیمات لاگ را تعریف می کند
    /// </summary>
    [Flags]
    public enum LogSettingPermissions
    {
        /// <summary>
        /// عدم دسترسی به تنظیمات لاگ
        /// </summary>
        None = 0x0,

        /// <summary>
        /// دسترسی مشاهده تنظیمات لاگ
        /// </summary>
        ViewSettings = 0x1,

        /// <summary>
        /// دسترسی ذخیره تنظیمات لاگ
        /// </summary>
        SaveSettings = 0x2,

        /// <summary>
        /// دسترسی کامل به عملیات تعریف شده برای مدیریت تنظیمات لاگ
        /// </summary>
        All = 0x3
    }

    /// <summary>
    /// فلگ های تعریف شده برای مجوزهای امنیتی در مدیریت دسترسی به سطرها را تعریف می کند
    /// </summary>
    [Flags]
    public enum RowAccessPermissions
    {
        /// <summary>
        /// عدم دسترسی به تنظیمات دسترسی به سطرها
        /// </summary>
        None = 0x0,

        /// <summary>
        /// دسترسی مشاهده دسترسی به سطرها
        /// </summary>
        ViewRowAccess = 0x1,

        /// <summary>
        /// دسترسی ذخیره دسترسی به سطرها
        /// </summary>
        SaveRowAccess = 0x2,

        /// <summary>
        /// دسترسی کامل به عملیات تعریف شده برای مدیریت دسترسی به سطرها
        /// </summary>
        All = 0x3
    }

    /// <summary>
    /// فلگ های تعریف شده برای دسترسی های امنیتی به نرخ ارزها را تعریف می کند
    /// </summary>
    [Flags]
    public enum CurrencyRatePermissions
    {
        /// <summary>
        /// عدم دسترسی به نرخ ارز
        /// </summary>
        None = 0x0,

        /// <summary>
        /// دسترسی مشاهده لیست نرخ های ارز یا جزییات یک نرخ
        /// </summary>
        View = 0x1,

        /// <summary>
        /// دسترسی فیلتر نرخ های ارز در لیست
        /// </summary>
        Filter = 0x2,

        /// <summary>
        /// دسترسی چاپ لیست نرخ های ارز
        /// </summary>
        Print = 0x4,

        /// <summary>
        /// دسترسی ارسال اطلاعات نرخ های ارز موجود
        /// </summary>
        Export = 0x8,

        /// <summary>
        /// دسترسی ایجاد یک نرخ ارز جدید
        /// </summary>
        Create = 0x10,

        /// <summary>
        /// دسترسی ویرایش یک نرخ ارز موجود
        /// </summary>
        Edit = 0x20,

        /// <summary>
        /// دسترسی حذف یک نرخ ارز موجود
        /// </summary>
        Delete = 0x40,

        /// <summary>
        /// دسترسی کامل به عملیات تعریف شده برای مدیریت نرخ های ارز
        /// </summary>
        All = 0x7f
    }

    /// <summary>
    /// فلگ های تعریف شده برای دسترسی های امنیتی به دفتر روزنامه را تعریف می کند
    /// </summary>
    [Flags]
    public enum JournalPermissions
    {
        /// <summary>
        /// عدم دسترسی به دفتر روزنامه
        /// </summary>
        None = 0x0,

        /// <summary>
        /// دسترسی مشاهده دفتر روزنامه
        /// </summary>
        View = 0x1,

        /// <summary>
        /// دسترسی فیلتر اطلاعات دفتر روزنامه
        /// </summary>
        Filter = 0x2,

        /// <summary>
        /// دسترسی چاپ اطلاعات دفتر روزنامه
        /// </summary>
        Print = 0x4,

        /// <summary>
        /// دسترسی ارسال اطلاعات دفتر روزنامه
        /// </summary>
        Export = 0x8,

        /// <summary>
        /// دسترسی علامتگذاری ردیف های دفتر روزنامه
        /// </summary>
        Mark = 0x10,

        /// <summary>
        /// دسترسی مشاهده دفتر روزنامه به تفکیک شعبه
        /// </summary>
        ByBranch = 0x20,

        /// <summary>
        /// دسترسی کامل به عملیات تعریف شده برای دفتر روزنامه
        /// </summary>
        All = 0x3f
    }

    /// <summary>
    /// فلگ های تعریف شده برای دسترسی های امنیتی به دفتر حساب را تعریف می کند
    /// </summary>
    [Flags]
    public enum AccountBookPermissions
    {
        /// <summary>
        /// عدم دسترسی به دفتر حساب
        /// </summary>
        None = 0x0,

        /// <summary>
        /// دسترسی مشاهده دفتر حساب
        /// </summary>
        View = 0x1,

        /// <summary>
        /// دسترسی فیلتر اطلاعات دفتر حساب
        /// </summary>
        Filter = 0x2,

        /// <summary>
        /// دسترسی چاپ اطلاعات دفتر حساب
        /// </summary>
        Print = 0x4,

        /// <summary>
        /// دسترسی ارسال اطلاعات دفتر حساب
        /// </summary>
        Export = 0x8,

        /// <summary>
        /// دسترسی علامتگذاری ردیف های دفتر حساب
        /// </summary>
        Mark = 0x10,

        /// <summary>
        /// دسترسی مشاهده دفتر حساب به تفکیک شعبه
        /// </summary>
        ByBranch = 0x20,

        /// <summary>
        /// دسترسی کامل به عملیات تعریف شده برای دفتر حساب
        /// </summary>
        All = 0x3f
    }

    /// <summary>
    /// فلگ های تعریف شده برای دسترسی های امنیتی به تراز آزمایشی را تعریف می کند
    /// </summary>
    [Flags]
    public enum TestBalancePermissions
    {
        /// <summary>
        /// عدم دسترسی به تراز آزمایشی
        /// </summary>
        None = 0x0,

        /// <summary>
        /// دسترسی مشاهده تراز آزمایشی
        /// </summary>
        View = 0x1,

        /// <summary>
        /// دسترسی فیلتر اطلاعات تراز آزمایشی
        /// </summary>
        Filter = 0x2,

        /// <summary>
        /// دسترسی چاپ اطلاعات تراز آزمایشی
        /// </summary>
        Print = 0x4,

        /// <summary>
        /// دسترسی ارسال اطلاعات تراز آزمایشی
        /// </summary>
        Export = 0x8,

        /// <summary>
        /// دسترسی مشاهده تراز آزمایشی به تفکیک شعبه
        /// </summary>
        ByBranch = 0x10,

        /// <summary>
        /// دسترسی فیلتر تراز آزمایشی بر حسب رفرنس
        /// </summary>
        FilterByRef = 0x20,

        /// <summary>
        /// دسترسی کامل به عملیات تعریف شده برای تراز آزمایشی
        /// </summary>
        All = 0x3f
    }

    /// <summary>
    /// فلگ های تعریف شده برای دسترسی های امنیتی به دفتر عملیات ارزی را تعریف می کند
    /// </summary>
    [Flags]
    public enum CurrencyBookPermissions
    {
        /// <summary>
        /// عدم دسترسی به دفتر عملیات ارزی
        /// </summary>
        None = 0x0,

        /// <summary>
        /// دسترسی مشاهده دفتر عملیات ارزی
        /// </summary>
        View = 0x1,

        /// <summary>
        /// دسترسی فیلتر اطلاعات دفتر عملیات ارزی
        /// </summary>
        Filter = 0x2,

        /// <summary>
        /// دسترسی چاپ اطلاعات دفتر عملیات ارزی
        /// </summary>
        Print = 0x4,

        /// <summary>
        /// دسترسی ارسال اطلاعات دفتر عملیات ارزی
        /// </summary>
        Export = 0x8,

        /// <summary>
        /// دسترسی علامتگذاری ردیف های دفتر عملیات ارزی
        /// </summary>
        Mark = 0x10,

        /// <summary>
        /// دسترسی مشاهده دفتر عملیات ارزی به تفکیک شعبه
        /// </summary>
        ByBranch = 0x20,

        /// <summary>
        /// دسترسی کامل به عملیات تعریف شده برای دفتر حساب
        /// </summary>
        All = 0x3f
    }

    /// <summary>
    /// فلگ های تعریف شده برای دسترسی های امنیتی به گردش و مانده سطوح شناور را تعریف می کند
    /// </summary>
    [Flags]
    public enum ItemBalancePermissions
    {
        /// <summary>
        /// عدم دسترسی به گردش و مانده سطوح شناور
        /// </summary>
        None = 0x0,

        /// <summary>
        /// دسترسی مشاهده گردش و مانده سطوح شناور
        /// </summary>
        View = 0x1,

        /// <summary>
        /// دسترسی فیلتر اطلاعات گردش و مانده سطوح شناور
        /// </summary>
        Filter = 0x2,

        /// <summary>
        /// دسترسی چاپ اطلاعات گردش و مانده سطوح شناور
        /// </summary>
        Print = 0x4,

        /// <summary>
        /// دسترسی ارسال اطلاعات گردش و مانده سطوح شناور
        /// </summary>
        Export = 0x8,

        /// <summary>
        /// دسترسی مشاهده گردش و مانده سطوح شناور به تفکیک شعبه
        /// </summary>
        ByBranch = 0x10,

        /// <summary>
        /// دسترسی فیلتر گردش و مانده سطوح شناور بر حسب رفرنس
        /// </summary>
        FilterByRef = 0x20,

        /// <summary>
        /// دسترسی کامل به عملیات تعریف شده برای گردش و مانده سطوح شناور
        /// </summary>
        All = 0x3f
    }

    /// <summary>
    /// فلگ های تعریف شده برای دسترسی های امنیتی به مانده به تفکیک حساب را تعریف می کند
    /// </summary>
    [Flags]
    public enum BalanceByAccountPermissions
    {
        /// <summary>
        /// عدم دسترسی به مانده به تفکیک حساب
        /// </summary>
        None = 0x0,

        /// <summary>
        /// دسترسی مشاهده مانده به تفکیک حساب
        /// </summary>
        View = 0x1,

        /// <summary>
        /// دسترسی فیلتر اطلاعات مانده به تفکیک حساب
        /// </summary>
        Filter = 0x2,

        /// <summary>
        /// دسترسی چاپ اطلاعات مانده به تفکیک حساب
        /// </summary>
        Print = 0x4,

        /// <summary>
        /// دسترسی ارسال اطلاعات مانده به تفکیک حساب
        /// </summary>
        Export = 0x8,

        /// <summary>
        /// دسترسی مشاهده مانده به تفکیک حساب به تفکیک شعبه
        /// </summary>
        ViewByBranch = 0x10,

        /// <summary>
        /// دسترسی فیلتر مانده به تفکیک حساب بر حسب رفرنس
        /// </summary>
        FilterByRef = 0x20,

        /// <summary>
        /// دسترسی کامل به عملیات تعریف شده برای مانده به تفکیک حساب
        /// </summary>
        All = 0x3f
    }

    /// <summary>
    /// فلگ های تعریف شده برای دسترسی های امنیتی به کنترل سیستم را تعریف می کند
    /// </summary>
    [Flags]
    public enum SystemIssuePermissions
    {
        /// <summary>
        /// عدم دسترسی به کنترل سیستم
        /// </summary>
        None = 0x0,

        /// <summary>
        /// دسترسی مشاهده کنترل سیستم
        /// </summary>
        View = 0x1
    }

    /// <summary>
    /// فلگ های تعریف شده برای دسترسی های امنیتی به گزارش سود و زیان را تعریف می کند
    /// </summary>
    [Flags]
    public enum ProfitLossPermissions
    {
        /// <summary>
        /// عدم دسترسی به سود و زیان
        /// </summary>
        None = 0x0,

        /// <summary>
        /// دسترسی مشاهده سود و زیان
        /// </summary>
        View = 0x1,

        /// <summary>
        /// دسترسی فیلتر اطلاعات سود و زیان
        /// </summary>
        Filter = 0x2,

        /// <summary>
        /// دسترسی چاپ اطلاعات سود و زیان
        /// </summary>
        Print = 0x4,

        /// <summary>
        /// دسترسی ارسال اطلاعات سود و زیان
        /// </summary>
        Export = 0x8,

        /// <summary>
        /// دسترسی فیلتر اطلاعات سود و زیان بر حسب رفرنس
        /// </summary>
        FilterByRef = 0x10,

        /// <summary>
        /// دسترسی کامل به عملیات تعریف شده برای سود و زیان
        /// </summary>
        All = 0x1f
    }

    /// <summary>
    /// فلگ های تعریف شده برای دسترسی های امنیتی به گزارش ترازنامه را تعریف می کند
    /// </summary>
    [Flags]
    public enum BalanceSheetPermissions
    {
        /// <summary>
        /// عدم دسترسی به ترازنامه
        /// </summary>
        None = 0x0,

        /// <summary>
        /// دسترسی مشاهده ترازنامه
        /// </summary>
        View = 0x1,

        /// <summary>
        /// دسترسی فیلتر اطلاعات ترازنامه
        /// </summary>
        Filter = 0x2,

        /// <summary>
        /// دسترسی چاپ اطلاعات ترازنامه
        /// </summary>
        Print = 0x4,

        /// <summary>
        /// دسترسی ارسال اطلاعات ترازنامه
        /// </summary>
        Export = 0x8,

        /// <summary>
        /// دسترسی فیلتر اطلاعات ترازنامه بر حسب رفرنس
        /// </summary>
        FilterByRef = 0x10,

        /// <summary>
        /// دسترسی کامل به عملیات تعریف شده برای ترازنامه
        /// </summary>
        All = 0x1f
    }

    /// <summary>
    /// فلگ های تعریف شده برای دسترسی های امنیتی به عملیات داشبورد را تعریف می کند
    /// </summary>
    [Flags]
    public enum DashboardPermissions
    {
        /// <summary>
        /// عدم دسترسی به عملیات داشبورد
        /// </summary>
        None = 0x0,

        /// <summary>
        /// دسترسی مدیریت داشبورد
        /// </summary>
        ManageDashboard = 0x1,

        /// <summary>
        /// دسترسی مدیریت ویجت ها
        /// </summary>
        ManageWidgets = 0x2,

        /// <summary>
        /// دسترسی کامل به عملیات داشبورد
        /// </summary>
        All = 0x3
    }

    /// <summary>
    /// فلگ های تعریف شده برای دسترسی های امنیتی به یک دسته چک را تعریف می کند
    /// </summary>
    [Flags]
    public enum CheckBookPermissions
    {
        /// <summary>
        /// عدم دسترسی به اطلاعات دسته چک ها
        /// </summary>
        None = 0x0,

        /// <summary>
        /// دسترسی مشاهده لیست دسته چک ها یا جزییات یک دسته چک
        /// </summary>
        View = 0x1,

        /// <summary>
        /// دسترسی فیلتر دسته چک ها موجود
        /// </summary>
        Filter = 0x2,

        /// <summary>
        /// دسترسی چاپ دسته چک ها موجود
        /// </summary>
        Print = 0x4,

        /// <summary>
        /// دسترسی ارسال اطلاعات دسته چک ها موجود
        /// </summary>
        Export = 0x8,

        /// <summary>
        /// دسترسی ایجاد یک دسته چک جدید
        /// </summary>
        Create = 0x10,

        /// <summary>
        /// دسترسی ویرایش یک دسته چک موجود
        /// </summary>
        Edit = 0x20,

        /// <summary>
        /// دسترسی حذف یک دسته چک موجود
        /// </summary>
        Delete = 0x40,

        /// <summary>
        /// دسترسی کامل به عملیات تعریف شده برای مدیریت دسته چک ها
        /// </summary>
        All = 0x7f
    }

    /// <summary>
    /// فلگ های تعریف شده برای دسترسی های امنیتی به یک برگه چک را تعریف می کند
    /// </summary>
    [Flags]
    public enum CheckBookPagePermissions
    {
        /// <summary>
        /// عدم دسترسی به اطلاعات برگه های چک
        /// </summary>
        None = 0x0,

        /// <summary>
        /// دسترسی مشاهده لیست برگه های چک یا جزییات یک برگه چک
        /// </summary>
        View = 0x1,

        /// <summary>
        /// دسترسی فیلتر برگه های چک موجود
        /// </summary>
        Filter = 0x2,

        /// <summary>
        /// دسترسی چاپ برگه های چک موجود
        /// </summary>
        Print = 0x4,

        /// <summary>
        /// دسترسی ارسال اطلاعات برگه های چک موجود
        /// </summary>
        Export = 0x8,

        /// <summary>
        /// دسترسی ایجاد یک برگه چک جدید
        /// </summary>
        Create = 0x10,

        /// <summary>
        /// دسترسی ویرایش یک برگه چک موجود
        /// </summary>
        Edit = 0x20,

        /// <summary>
        /// دسترسی حذف یک برگه چک موجود
        /// </summary>
        Delete = 0x40,

        /// <summary>
        /// دسترسی کامل به عملیات تعریف شده برای مدیریت برگه های چک
        /// </summary>
        All = 0x7f
    }
}
