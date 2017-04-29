using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Workflow
{
    /// <summary>
    /// عملیات گردش کار کنترل وضعیت سند مالی را تعریف می کند.
    /// </summary>
    public interface ITransactionWorkflow
    {
        /// <summary>
        /// پیاده سازی مناسب برای ایجاد کلاس ها از اینترفیس ها
        /// </summary>
        object TypeContainer { get; set; }

        /// <summary>
        /// یک سند مالی پیش نویس را در حالت ثبت نشده و وضعیت عملیاتی تنظیم شده قرار می دهد.
        /// </summary>
        /// <param name="transactionId">شناسه دیتابیسی سند مالی که باید حالت و وضعیتش تغییر کند</param>
        /// <param name="userId">شناسه دیتابیسی کاربری که عمل تنظیم را انجام می دهد</param>
        void Prepare(int transactionId, int userId);

        /// <summary>
        /// یک سند مالی ثبت نشده و تنظیم شده را در وضعیت عملیاتی بررسی شده قرار می دهد.
        /// </summary>
        /// <param name="transactionId">شناسه دیتابیسی سند مالی که باید وضعیتش تغییر کند</param>
        /// <param name="userId">شناسه دیتابیسی کاربری که عمل بررسی را انجام می دهد</param>
        void Review(int transactionId, int userId);

        /// <summary>
        /// یک سند مالی ثبت نشده و بررسی شده را برای بررسی مجدد در وضعیت عملیاتی تنظیم شده قرار می دهد.
        /// </summary>
        /// <param name="transactionId">شناسه دیتابیسی سند مالی که باید وضعیتش تغییر کند</param>
        /// <param name="userId">شناسه دیتابیسی کاربری که درخواست بررسی مجدد را ثبت می کند</param>
        void RequestRevision(int transactionId, int userId);

        /// <summary>
        /// یک سند مالی ثبت نشده و بررسی شده را در حالت ثبت عادی و وضعیت عملیاتی تایید شده قرار می دهد.
        /// </summary>
        /// <param name="transactionId">شناسه دیتابیسی سند مالی که باید وضعیتش تغییر کند</param>
        /// <param name="userId">شناسه دیتابیسی کاربری که عمل تایید را انجام می دهد</param>
        void Confirm(int transactionId, int userId);

        /// <summary>
        /// یک سند مالی ثبت عادی و تایید شده را در حالت ثبت قطعی و وضعیت عملیاتی تصویب شده قرار می دهد.
        /// </summary>
        /// <param name="transactionId">شناسه دیتابیسی سند مالی که باید وضعیتش تغییر کند</param>
        /// <param name="userId">شناسه دیتابیسی کاربری که عمل تصویب را انجام می دهد</param>
        void Approve(int transactionId, int userId);

        /// <summary>
        /// مجموعه ای از اسناد مالی پیش نویس را در حالت ثبت نشده و وضعیت عملیاتی تنظیم شده قرار می دهد.
        /// </summary>
        /// <param name="transactions">مجموعه شناسه های مالی اسنادی که باید وضعیتشان تغییر کند</param>
        /// <param name="userId">شناسه دیتابیسی کاربری که عملیات را انجام می دهد</param>
        void PrepareMultiple(IEnumerable<int> transactions, int userId);

        /// <summary>
        /// مجموعه ای از اسناد مالی ثبت نشده و تنظیم شده را در وضعیت عملیاتی بررسی شده قرار می دهد.
        /// </summary>
        /// <param name="transactions">مجموعه شناسه های مالی اسنادی که باید وضعیتشان تغییر کند</param>
        /// <param name="userId">شناسه دیتابیسی کاربری که عملیات را انجام می دهد</param>
        void ReviewMultiple(IEnumerable<int> transactions, int userId);

        /// <summary>
        /// مجموعه ای از اسناد مالی ثبت نشده و بررسی شده را برای بررسی مجدد در وضعیت عملیاتی تنظیم شده قرار می دهد.
        /// </summary>
        /// <param name="transactions">مجموعه شناسه های مالی اسنادی که باید وضعیتشان تغییر کند</param>
        /// <param name="userId">شناسه دیتابیسی کاربری که عملیات را انجام می دهد</param>
        void RequestRevisionMultiple(IEnumerable<int> transactions, int userId);

        /// <summary>
        /// مجموعه ای از اسناد مالی ثبت نشده و بررسی شده را در حالت ثبت عادی و وضعیت عملیاتی تایید شده قرار می دهد.
        /// </summary>
        /// <param name="transactions">مجموعه شناسه های مالی اسنادی که باید وضعیتشان تغییر کند</param>
        /// <param name="userId">شناسه دیتابیسی کاربری که عملیات را انجام می دهد</param>
        void ConfirmMultiple(IEnumerable<int> transactions, int userId);

        /// <summary>
        /// مجموعه ای از اسناد مالی ثبت عادی و تایید شده را در حالت ثبت قطعی و وضعیت عملیاتی تصویب شده قرار می دهد.
        /// </summary>
        /// <param name="transactions">مجموعه شناسه های مالی اسنادی که باید وضعیتشان تغییر کند</param>
        /// <param name="userId">شناسه دیتابیسی کاربری که عملیات را انجام می دهد</param>
        void ApproveMultiple(IEnumerable<int> transactions, int userId);
    }
}
