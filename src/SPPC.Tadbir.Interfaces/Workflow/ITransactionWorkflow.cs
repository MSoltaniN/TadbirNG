using System;
using System.Collections.Generic;
using SPPC.Tadbir.Service;

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
        /// اطلاعات امنیتی کاربر جاری در برنامه
        /// </summary>
        ISecurityContextManager ContextManager { get; set; }

        /// <summary>
        /// یک سند مالی پیش نویس را در حالت ثبت نشده و وضعیت عملیاتی تنظیم شده قرار می دهد.
        /// </summary>
        /// <param name="transactionId">شناسه دیتابیسی سند مالی که باید حالت و وضعیتش تغییر کند</param>
        void Prepare(int transactionId);

        /// <summary>
        /// یک سند مالی ثبت نشده و تنظیم شده را در وضعیت عملیاتی بررسی شده قرار می دهد.
        /// </summary>
        /// <param name="transactionId">شناسه دیتابیسی سند مالی که باید وضعیتش تغییر کند</param>
        void Review(int transactionId);

        /// <summary>
        /// یک سند مالی ثبت نشده و بررسی شده را برای بررسی مجدد در وضعیت عملیاتی تنظیم شده قرار می دهد.
        /// </summary>
        /// <param name="transactionId">شناسه دیتابیسی سند مالی که باید وضعیتش تغییر کند</param>
        void RequestRevision(int transactionId);

        /// <summary>
        /// یک سند مالی ثبت نشده و بررسی شده را در حالت ثبت عادی و وضعیت عملیاتی تایید شده قرار می دهد.
        /// </summary>
        /// <param name="transactionId">شناسه دیتابیسی سند مالی که باید وضعیتش تغییر کند</param>
        void Confirm(int transactionId);

        /// <summary>
        /// یک سند مالی ثبت عادی و تایید شده را در حالت ثبت قطعی و وضعیت عملیاتی تصویب شده قرار می دهد.
        /// </summary>
        /// <param name="transactionId">شناسه دیتابیسی سند مالی که باید وضعیتش تغییر کند</param>
        void Approve(int transactionId);

        /// <summary>
        /// مجموعه ای از اسناد مالی پیش نویس را در حالت ثبت نشده و وضعیت عملیاتی تنظیم شده قرار می دهد.
        /// </summary>
        /// <param name="transactions">مجموعه شناسه های مالی اسنادی که باید وضعیتشان تغییر کند</param>
        void PrepareMultiple(IEnumerable<int> transactions);

        /// <summary>
        /// مجموعه ای از اسناد مالی ثبت نشده و تنظیم شده را در وضعیت عملیاتی بررسی شده قرار می دهد.
        /// </summary>
        /// <param name="transactions">مجموعه شناسه های مالی اسنادی که باید وضعیتشان تغییر کند</param>
        void ReviewMultiple(IEnumerable<int> transactions);

        /// <summary>
        /// مجموعه ای از اسناد مالی ثبت نشده و بررسی شده را برای بررسی مجدد در وضعیت عملیاتی تنظیم شده قرار می دهد.
        /// </summary>
        /// <param name="transactions">مجموعه شناسه های مالی اسنادی که باید وضعیتشان تغییر کند</param>
        void RequestRevisionMultiple(IEnumerable<int> transactions);

        /// <summary>
        /// مجموعه ای از اسناد مالی ثبت نشده و بررسی شده را در حالت ثبت عادی و وضعیت عملیاتی تایید شده قرار می دهد.
        /// </summary>
        /// <param name="transactions">مجموعه شناسه های مالی اسنادی که باید وضعیتشان تغییر کند</param>
        void ConfirmMultiple(IEnumerable<int> transactions);

        /// <summary>
        /// مجموعه ای از اسناد مالی ثبت عادی و تایید شده را در حالت ثبت قطعی و وضعیت عملیاتی تصویب شده قرار می دهد.
        /// </summary>
        /// <param name="transactions">مجموعه شناسه های مالی اسنادی که باید وضعیتشان تغییر کند</param>
        void ApproveMultiple(IEnumerable<int> transactions);
    }
}
