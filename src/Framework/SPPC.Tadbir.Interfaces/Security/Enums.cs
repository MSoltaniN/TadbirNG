using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Security
{
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
        /// دسترسی ایجاد یک سرفصل حسابداری جدید
        /// </summary>
        Create = 0x2,

        /// <summary>
        /// دسترسی ویرایش یک سرفصل حسابداری موجود
        /// </summary>
        Edit = 0x4,

        /// <summary>
        /// دسترسی حذف یک سرفصل حسابداری موجود
        /// </summary>
        Delete = 0x8,

        /// <summary>
        /// دسترسی کامل به عملیات تعریف شده برای مدیریت سرفصل حسابداری
        /// </summary>
        All = 0xf
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
        /// دسترسی ایجاد یک تفصیلی شناور جدید
        /// </summary>
        Create = 0x2,

        /// <summary>
        /// دسترسی ویرایش یک تفصیلی شناور موجود
        /// </summary>
        Edit = 0x4,

        /// <summary>
        /// دسترسی حذف یک تفصیلی شناور موجود
        /// </summary>
        Delete = 0x8,

        /// <summary>
        /// دسترسی کامل به عملیات تعریف شده برای مدیریت تفصیلی های شناور
        /// </summary>
        All = 0xf
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
        /// دسترسی ایجاد یک مرکز هزینه جدید
        /// </summary>
        Create = 0x2,

        /// <summary>
        /// دسترسی ویرایش یک مرکز هزینه موجود
        /// </summary>
        Edit = 0x4,

        /// <summary>
        /// دسترسی حذف یک مرکز هزینه موجود
        /// </summary>
        Delete = 0x8,

        /// <summary>
        /// دسترسی کامل به عملیات تعریف شده برای مدیریت مراکز هزینه
        /// </summary>
        All = 0xf
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
        /// دسترسی ایجاد یک پروژه جدید
        /// </summary>
        Create = 0x2,

        /// <summary>
        /// دسترسی ویرایش یک پروژه موجود
        /// </summary>
        Edit = 0x4,

        /// <summary>
        /// دسترسی حذف یک پروژه موجود
        /// </summary>
        Delete = 0x8,

        /// <summary>
        /// دسترسی کامل به عملیات تعریف شده برای مدیریت پروژه ها
        /// </summary>
        All = 0xf
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
        /// دسترسی ایجاد یک گروه حساب جدید
        /// </summary>
        Create = 0x2,

        /// <summary>
        /// دسترسی ویرایش یک گروه حساب موجود
        /// </summary>
        Edit = 0x4,

        /// <summary>
        /// دسترسی حذف یک گروه حساب موجود
        /// </summary>
        Delete = 0x8,

        /// <summary>
        /// دسترسی کامل به عملیات تعریف شده برای مدیریت گروه حساب
        /// </summary>
        All = 0xf
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
        /// دسترسی مشاهده مجموعه حساب
        /// </summary>
        View = 0x1,

        /// <summary>
        /// دسترسی ایجاد یک مجموعه حساب جدید
        /// </summary>
        Create = 0x2,
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
        /// دسترسی مدیریت ارتباطات بین مولفه های مختلف
        /// </summary>
        ManageRelationships = 0x2,
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
        /// دسترسی مدیریت دسترسی به سطرها
        /// </summary>
        ManageRowAccess = 0x2,
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
        /// دسترسی مدیریت تنظیمات برنامه
        /// </summary>
        ManageSettings = 0x2,
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
        /// دسترسی مدیریت تنظیمات لاگ
        /// </summary>
        ManageSettings = 0x2,
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
        /// دسترسی ایجاد یک ارز جدید
        /// </summary>
        Create = 0x2,

        /// <summary>
        /// دسترسی ویرایش یک ارز موجود
        /// </summary>
        Edit = 0x4,

        /// <summary>
        /// دسترسی حذف یک ارز موجود
        /// </summary>
        Delete = 0x8,

        /// <summary>
        /// دسترسی جستجوی ارزها در لیست
        /// </summary>
        Lookup = 0x10,

        /// <summary>
        /// دسترسی فیلتر ارزها در لیست
        /// </summary>
        Filter = 0x20,

        /// <summary>
        /// دسترسی چاپ لیست ارزها
        /// </summary>
        Print = 0x40,

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
        /// دسترسی ایجاد یک نرخ ارز جدید
        /// </summary>
        Create = 0x2,

        /// <summary>
        /// دسترسی ویرایش یک نرخ ارز موجود
        /// </summary>
        Edit = 0x4,

        /// <summary>
        /// دسترسی حذف یک نرخ ارز موجود
        /// </summary>
        Delete = 0x8,

        /// <summary>
        /// دسترسی جستجوی نرخ های ارز در لیست
        /// </summary>
        Lookup = 0x10,

        /// <summary>
        /// دسترسی فیلتر نرخ های ارز در لیست
        /// </summary>
        Filter = 0x20,

        /// <summary>
        /// دسترسی چاپ لیست نرخ های ارز
        /// </summary>
        Print = 0x40,

        /// <summary>
        /// دسترسی کامل به عملیات تعریف شده برای مدیریت ارزها
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
        /// دسترسی ایجاد یک دوره مالی جدید
        /// </summary>
        Create = 0x2,

        /// <summary>
        /// دسترسی ویرایش یک دوره مالی موجود
        /// </summary>
        Edit = 0x4,

        /// <summary>
        /// دسترسی حذف یک دوره مالی موجود
        /// </summary>
        Delete = 0x8,

        /// <summary>
        /// دسترسی اعطای دسترسی به یک دوره مالی برای یک یا چند نقش
        /// </summary>
        AssignRoles = 0x10,

        /// <summary>
        /// دسترسی کامل به عملیات تعریف شده برای مدیریت دوره های مالی
        /// </summary>
        All = 0x1f
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
        /// دسترسی ایجاد یک شعبه سازمانی جدید
        /// </summary>
        Create = 0x2,

        /// <summary>
        /// دسترسی ویرایش یک شعبه سازمانی موجود
        /// </summary>
        Edit = 0x4,

        /// <summary>
        /// دسترسی حذف یک شعبه سازمانی موجود
        /// </summary>
        Delete = 0x8,

        /// <summary>
        /// دسترسی اعطای دسترسی به یک شعبه برای یک یا چند نقش
        /// </summary>
        AssignRoles = 0x10,

        /// <summary>
        /// دسترسی کامل به عملیات تعریف شده برای مدیریت شعب سازمانی
        /// </summary>
        All = 0x1f
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
        /// دسترسی بایگانی لاگ های عملیاتی شرکتی
        /// </summary>
        Archive = 0x2,

        /// <summary>
        /// دسترسی برگشت از بایگانی لاگ های عملیاتی شرکتی
        /// </summary>
        Recover = 0x4,

        /// <summary>
        /// دسترسی مشاهده لاگ های شرکتی بایگانی شده
        /// </summary>
        ViewArchive = 0x8,

        /// <summary>
        /// دسترسی حذف لاگ های عملیاتی شرکتی از بایگانی
        /// </summary>
        Delete = 0x10
    }

    /// <summary>
    /// فلگ های تعریف شده برای دسترسی های امنیتی به لاگ های عملیاتی سیستمی را تعریف می کند
    /// </summary>
    [Flags]
    public enum SysOperationLogPermissions
    {
        /// <summary>
        /// عدم دسترسی به لاگ های عملیاتی سیستمی
        /// </summary>
        None = 0x0,

        /// <summary>
        /// دسترسی مشاهده لاگ های عملیاتی سیستمی
        /// </summary>
        View = 0x1,

        /// <summary>
        /// دسترسی بایگانی لاگ های عملیاتی سیستمی
        /// </summary>
        Archive = 0x2,

        /// <summary>
        /// دسترسی برگشت از بایگانی لاگ های عملیاتی سیستمی
        /// </summary>
        Recover = 0x4,

        /// <summary>
        /// دسترسی مشاهده لاگ های سیستمی بایگانی شده
        /// </summary>
        ViewArchive = 0x8,

        /// <summary>
        /// دسترسی حذف لاگ های عملیاتی سیستمی از بایگانی
        /// </summary>
        Delete = 0x10
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
        SetDefault = 0x4
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
        /// دسترسی حرکت روی اسناد حسابداری
        /// </summary>
        Navigate = 0x10,

        /// <summary>
        /// دسترسی جستجوی پیشرفته اسناد حسابداری
        /// </summary>
        Lookup = 0x20,

        /// <summary>
        /// دسترسی فیلتر پیشرفته اسناد حسابداری
        /// </summary>
        Filter = 0x40,

        /// <summary>
        /// دسترسی چاپ اسناد حسابداری
        /// </summary>
        Print = 0x80,

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
        /// دسترسی برگشت از ثبت قطعی سند حسابداری
        /// </summary>
        UndoFinalize = 0x8000,

        /// <summary>
        /// کلیه دسترسی های تعریف شده برای اسناد حسابداری
        /// </summary>
        All = 0xffff
    }

    /// <summary>
    /// Provides flag values for permissions currently defined for managing an application user.
    /// </summary>
    [Flags]
    public enum UserPermissions
    {
        /// <summary>
        /// Indicates no permission for managing a user
        /// </summary>
        None = 0x0,

        /// <summary>
        /// Indicates permission to view user list or details of a user
        /// </summary>
        View = 0x1,

        /// <summary>
        /// Indicates permission to create a new user
        /// </summary>
        Create = 0x2,

        /// <summary>
        /// Indicates permission to edit an existing user
        /// </summary>
        Edit = 0x4,

        /// <summary>
        /// Indicates permission to assign one or more roles to a user
        /// </summary>
        AssignRoles = 0x8,

        /// <summary>
        /// Indicates all permissions available for managing a user
        /// </summary>
        All = 0xf
    }

    /// <summary>
    /// Provides flag values for permissions currently defined for managing an application role.
    /// </summary>
    [Flags]
    public enum RolePermissions
    {
        /// <summary>
        /// Indicates no permission for managing a role
        /// </summary>
        None = 0x0,

        /// <summary>
        /// Indicates permission to view role list or details of a role
        /// </summary>
        View = 0x1,

        /// <summary>
        /// Indicates permission to create a new role
        /// </summary>
        Create = 0x2,

        /// <summary>
        /// Indicates permission to edit an existing role
        /// </summary>
        Edit = 0x4,

        /// <summary>
        /// Indicates permission to delete an existing role
        /// </summary>
        Delete = 0x8,

        /// <summary>
        /// Indicates permission to add/remove one or more users to/from a role
        /// </summary>
        AssignUsers = 0x10,

        /// <summary>
        /// Indicates permission to allow/disallow access to one or more branches in a role
        /// </summary>
        AssignBranches = 0x20,

        /// <summary>
        /// Indicates permission to allow/disallow access to one or more fiscal periods in a role
        /// </summary>
        AssignFiscalPeriods = 0x40,

        /// <summary>
        /// Indicates all permissions available for managing a role
        /// </summary>
        All = 0x7f
    }

    /// <summary>
    /// Provides flag values for permissions currently defined for managing a company.
    /// </summary>
    [Flags]
    public enum CompanyPermissions
    {
        /// <summary>
        /// Indicates no permission for managing a company
        /// </summary>
        None = 0x0,

        /// <summary>
        /// Indicates permission to view company list or details of a company
        /// </summary>
        View = 0x1,

        /// <summary>
        /// Indicates permission to create a new company
        /// </summary>
        Create = 0x2,

        /// <summary>
        /// Indicates permission to edit an existing company
        /// </summary>
        Edit = 0x4,

        /// <summary>
        /// Indicates permission to delete an existing company
        /// </summary>
        Delete = 0x8,

        /// <summary>
        /// Indicates all permissions available for managing a company
        /// </summary>
        All = 0xf
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
        /// دسترسی جستجوی اطلاعات دفتر روزنامه
        /// </summary>
        Lookup = 0x2,

        /// <summary>
        /// دسترسی فیلتر اطلاعات دفتر روزنامه
        /// </summary>
        Filter = 0x4,

        /// <summary>
        /// دسترسی چاپ اطلاعات دفتر روزنامه
        /// </summary>
        Print = 0x8,

        /// <summary>
        /// دسترسی علامتگذاری ردیف های دفتر روزنامه
        /// </summary>
        Mark = 0x10,

        /// <summary>
        /// دسترسی مشاهده دفتر روزنامه به تفکیک شعبه
        /// </summary>
        ByBranch = 0x20
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
        /// دسترسی جستجوی اطلاعات دفتر حساب
        /// </summary>
        Lookup = 0x2,

        /// <summary>
        /// دسترسی فیلتر اطلاعات دفتر حساب
        /// </summary>
        Filter = 0x4,

        /// <summary>
        /// دسترسی چاپ اطلاعات دفتر حساب
        /// </summary>
        Print = 0x8,

        /// <summary>
        /// دسترسی علامتگذاری ردیف های دفتر حساب
        /// </summary>
        Mark = 0x10,

        /// <summary>
        /// دسترسی مشاهده دفتر حساب به تفکیک شعبه
        /// </summary>
        ByBranch = 0x20
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
        /// دسترسی جستجوی اطلاعات تراز آزمایشی
        /// </summary>
        Lookup = 0x2,

        /// <summary>
        /// دسترسی فیلتر اطلاعات تراز آزمایشی
        /// </summary>
        Filter = 0x4,

        /// <summary>
        /// دسترسی چاپ اطلاعات تراز آزمایشی
        /// </summary>
        Print = 0x8,

        /// <summary>
        /// دسترسی مشاهده تراز آزمایشی به تفکیک شعبه
        /// </summary>
        ByBranch = 0x10
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
        /// دسترسی جستجوی اطلاعات گردش و مانده سطوح شناور
        /// </summary>
        Lookup = 0x2,

        /// <summary>
        /// دسترسی فیلتر اطلاعات گردش و مانده سطوح شناور
        /// </summary>
        Filter = 0x4,

        /// <summary>
        /// دسترسی چاپ اطلاعات گردش و مانده سطوح شناور
        /// </summary>
        Print = 0x8,

        /// <summary>
        /// دسترسی مشاهده گردش و مانده سطوح شناور به تفکیک شعبه
        /// </summary>
        ByBranch = 0x10
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
        /// دسترسی جستجوی اطلاعات دفتر عملیات ارزی
        /// </summary>
        Lookup = 0x2,

        /// <summary>
        /// دسترسی فیلتر اطلاعات دفتر عملیات ارزی
        /// </summary>
        Filter = 0x4,

        /// <summary>
        /// دسترسی چاپ اطلاعات دفتر عملیات ارزی
        /// </summary>
        Print = 0x8,

        /// <summary>
        /// دسترسی علامتگذاری ردیف های دفتر عملیات ارزی
        /// </summary>
        Mark = 0x10,

        /// <summary>
        /// دسترسی مشاهده دفتر عملیات ارزی به تفکیک شعبه
        /// </summary>
        ByBranch = 0x20
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
        /// دسترسی جستجوی اطلاعات مانده به تفکیک حساب
        /// </summary>
        Lookup = 0x2,

        /// <summary>
        /// دسترسی فیلتر اطلاعات مانده به تفکیک حساب
        /// </summary>
        Filter = 0x4,

        /// <summary>
        /// دسترسی چاپ اطلاعات مانده به تفکیک حساب
        /// </summary>
        Print = 0x8,

        /// <summary>
        /// دسترسی مشاهده مانده به تفکیک حساب به تفکیک شعبه
        /// </summary>
        ViewByBranch = 0x10
    }
}
