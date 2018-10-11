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
        /// دسترسی کامل به عملیات تعریف شده برای مدیریت ارزها
        /// </summary>
        All = 0xf
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
    /// Provides flag values for permissions currently defined for managing a financial voucher.
    /// </summary>
    [Flags]
    public enum VoucherPermissions
    {
        /// <summary>
        /// Indicates no permission for managing a voucher
        /// </summary>
        None = 0x0,

        /// <summary>
        /// Indicates permission to view voucher list or details of a voucher
        /// </summary>
        View = 0x1,

        /// <summary>
        /// Indicates permission to create a new voucher
        /// </summary>
        Create = 0x2,

        /// <summary>
        /// Indicates permission to edit an existing voucher
        /// </summary>
        Edit = 0x4,

        /// <summary>
        /// Indicates permission to delete an existing voucher
        /// </summary>
        Delete = 0x8,

        /// <summary>
        /// دسترسی تنظیم سند (تغییر وضعیت عملیاتی سند به وضعیت تنظیم شده)
        /// </summary>
        Prepare = 0x10,

        /// <summary>
        /// دسترسی بررسی سند (تغییر وضعیت عملیاتی سند به وضعیت بررسی شده)
        /// </summary>
        Review = 0x20,

        /// <summary>
        /// دسترسی تایید سند (تغییر وضعیت عملیاتی سند به وضعیت تایید شده)
        /// </summary>
        Confirm = 0x40,

        /// <summary>
        /// دسترسی تصویب سند (تغییر وضعیت عملیاتی سند به وضعیت تصویب شده)
        /// </summary>
        Approve = 0x80,

        /// <summary>
        /// دسترسی ثبت سند
        /// </summary>
        Check = 0x100,

        /// <summary>
        /// دسترسی برگشت سند
        /// </summary>
        Uncheck = 0x200,

        /// <summary>
        /// Indicates all permissions available for managing a voucher
        /// </summary>
        All = 0x3ff
    }

    /// <summary>
    /// فلگ های تعریف شده برای دسترسی های امنیتی به واحدهای سازمانی را تعریف می کند
    /// </summary>
    [Flags]
    public enum BusinessUnitPermissions
    {
        /// <summary>
        /// عدم دسترسی به واحدهای سازمانی
        /// </summary>
        None = 0x0,

        /// <summary>
        /// دسترسی مشاهده لیست واحهای سازمانی یا جزییات یک واحد سازمانی
        /// </summary>
        View = 0x1,

        /// <summary>
        /// دسترسی ایجاد یک واحد سازمانی جدید
        /// </summary>
        Create = 0x2,

        /// <summary>
        /// دسترسی ویرایش یک واحد سازمانی موجود
        /// </summary>
        Edit = 0x4,

        /// <summary>
        /// دسترسی حذف یک واحد سازمانی موجود
        /// </summary>
        Delete = 0x8,

        /// <summary>
        /// دسترسی کامل به عملیات تعریف شده برای مدیریت واحدهای سازمانی
        /// </summary>
        All = 0xf
    }

    /// <summary>
    /// فلگ های تعریف شده برای دسترسی های امنیتی به شرکای تجاری را تعریف می کند
    /// </summary>
    [Flags]
    public enum BusinessPartnerPermissions
    {
        /// <summary>
        /// عدم دسترسی به شرکای تجاری
        /// </summary>
        None = 0x0,

        /// <summary>
        /// دسترسی مشاهده لیست شرکای تجاری یا جزییات یک شریک تجاری
        /// </summary>
        View = 0x1,

        /// <summary>
        /// دسترسی ایجاد یک شریک تجاری جدید
        /// </summary>
        Create = 0x2,

        /// <summary>
        /// دسترسی ویرایش یک شریک تجاری موجود
        /// </summary>
        Edit = 0x4,

        /// <summary>
        /// دسترسی حذف یک شریک تجاری موجود
        /// </summary>
        Delete = 0x8,

        /// <summary>
        /// دسترسی کامل به عملیات تعریف شده برای مدیریت شرکای تجاری
        /// </summary>
        All = 0xf
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
    /// Provides flag values for permissions currently defined for managing a product inventory.
    /// </summary>
    [Flags]
    public enum ProductInventoryPermissions
    {
        /// <summary>
        /// Indicates no permission for managing a product inventory
        /// </summary>
        None = 0x0,

        /// <summary>
        /// Indicates permission to view product inventory list or details of a product inventory
        /// </summary>
        View = 0x1,

        /// <summary>
        /// Indicates permission to create a new product inventory
        /// </summary>
        Create = 0x2,

        /// <summary>
        /// Indicates permission to edit an existing product inventory
        /// </summary>
        Edit = 0x4,

        /// <summary>
        /// Indicates permission to delete an existing product inventory
        /// </summary>
        Delete = 0x8,

        /// <summary>
        /// Indicates all permissions available for managing a product inventory
        /// </summary>
        All = 0xf
    }

    /// <summary>
    /// مقادیر فلگ مربوط به دسترسی های درخواست کالا را تعریف می کند
    /// </summary>
    [Flags]
    public enum RequisitionPermissions
    {
        /// <summary>
        /// عدم دسترسی به عملیات درخواست کالا
        /// </summary>
        None = 0x0,

        /// <summary>
        /// دسترسی مشاهده فهرست درخواست های کالا یا جزییات یک درخواست کالا
        /// </summary>
        View = 0x1,

        /// <summary>
        /// دسترسی ایجاد درخواست کالای جدید
        /// </summary>
        Create = 0x2,

        /// <summary>
        /// دسترسی اصلاح درخواست کالای موجود
        /// </summary>
        Edit = 0x4,

        /// <summary>
        /// دسترسی حذف درخواست کالای موجود
        /// </summary>
        Delete = 0x8,

        /// <summary>
        /// دسترسی تنظیم درخواست کالا
        /// </summary>
        Prepare = 0x10,

        /// <summary>
        /// دسترسی تایید درخواست کالا
        /// </summary>
        Confirm = 0x20,

        /// <summary>
        /// دسترسی تصویب درخواست کالا
        /// </summary>
        Approve = 0x40,

        /// <summary>
        /// دسترسی به تمام عملیات درخواست کالا
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
}
