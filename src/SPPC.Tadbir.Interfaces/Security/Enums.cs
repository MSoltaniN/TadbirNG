using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Security
{
    /// <summary>
    /// Provides flag values for permissions currently defined for managing a financial account.
    /// </summary>
    [Flags]
    public enum AccountPermissions
    {
        /// <summary>
        /// Indicates no permission for managing an account
        /// </summary>
        None = 0x0,

        /// <summary>
        /// Indicates permission to view account list or details of an account
        /// </summary>
        View = 0x1,

        /// <summary>
        /// Indicates permission to create a new account
        /// </summary>
        Create = 0x2,

        /// <summary>
        /// Indicates permission to edit an existing account
        /// </summary>
        Edit = 0x4,

        /// <summary>
        /// Indicates permission to delete an existing account
        /// </summary>
        Delete = 0x8,

        /// <summary>
        /// Indicates all permissions available for managing an account
        /// </summary>
        All = 0xf
    }

    /// <summary>
    /// Provides flag values for permissions currently defined for managing a financial transaction.
    /// </summary>
    [Flags]
    public enum TransactionPermissions
    {
        /// <summary>
        /// Indicates no permission for managing a transaction
        /// </summary>
        None = 0x0,

        /// <summary>
        /// Indicates permission to view transaction list or details of a transaction
        /// </summary>
        View = 0x1,

        /// <summary>
        /// Indicates permission to create a new transaction
        /// </summary>
        Create = 0x2,

        /// <summary>
        /// Indicates permission to edit an existing transaction
        /// </summary>
        Edit = 0x4,

        /// <summary>
        /// Indicates permission to delete an existing transaction
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
        /// Indicates all permissions available for managing a transaction
        /// </summary>
        All = 0xff
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
        /// Indicates all permissions available for managing a user
        /// </summary>
        All = 0x7
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
        /// Indicates all permissions available for managing a role
        /// </summary>
        All = 0x3f
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
}
