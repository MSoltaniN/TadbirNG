﻿using System;
using System.Collections.Generic;
using SPPC.Tadbir.Service;

namespace SPPC.Tadbir.Workflow
{
    /// <summary>
    /// عملیات گردش کار کنترل وضعیت سند مالی را تعریف می کند.
    /// </summary>
    public interface IVoucherWorkflow
    {
        /// <summary>
        /// اطلاعات امنیتی کاربر جاری در برنامه
        /// </summary>
        ISecurityContextManager ContextManager { get; }

        bool ValidateAction(string documentType, string status, string action);

        /// <summary>
        /// یک سند مالی پیش نویس را در حالت ثبت نشده و وضعیت عملیاتی تنظیم شده قرار می دهد.
        /// </summary>
        /// <param name="documentId">شناسه دیتابیسی مستند مرتبط با سند مالی که باید حالت و وضعیتش تغییر کند</param>
        /// <param name="entityId">شناسه دیتابیسی موجودیت</param>
        /// <param name="paraph">پاراف متنی که کاربر پیش از اقدام می تواند وارد کند</param>
        void Prepare(int entityId, int documentId, string paraph = null);

        /// <summary>
        /// یک سند مالی ثبت نشده و تنظیم شده را در وضعیت عملیاتی بررسی شده قرار می دهد.
        /// </summary>
        /// <param name="documentId">شناسه دیتابیسی مستند مرتبط با سند مالی که باید وضعیتش تغییر کند</param>
        /// <param name="entityId">شناسه دیتابیسی موجودیت</param>
        /// <param name="paraph">پاراف متنی که کاربر پیش از اقدام می تواند وارد کند</param>
        void Review(int entityId, int documentId, string paraph = null);

        /// <summary>
        /// یک سند مالی ثبت نشده و بررسی شده را برای بررسی مجدد در وضعیت عملیاتی تنظیم شده قرار می دهد.
        /// </summary>
        /// <param name="documentId">شناسه دیتابیسی مستند مرتبط با سند مالی که باید وضعیتش تغییر کند</param>
        /// <param name="entityId">شناسه دیتابیسی موجودیت</param>
        /// <param name="paraph">پاراف متنی که کاربر پیش از اقدام می تواند وارد کند</param>
        void Reject(int entityId, int documentId, string paraph = null);

        /// <summary>
        /// یک سند مالی ثبت نشده و بررسی شده را در حالت ثبت عادی و وضعیت عملیاتی تایید شده قرار می دهد.
        /// </summary>
        /// <param name="documentId">شناسه دیتابیسی مستند مرتبط با سند مالی که باید وضعیتش تغییر کند</param>
        /// <param name="entityId">شناسه دیتابیسی موجودیت</param>
        /// <param name="paraph">پاراف متنی که کاربر پیش از اقدام می تواند وارد کند</param>
        void Confirm(int entityId, int documentId, string paraph = null);

        /// <summary>
        /// یک سند مالی ثبت عادی و تایید شده را در حالت ثبت قطعی و وضعیت عملیاتی تصویب شده قرار می دهد.
        /// </summary>
        /// <param name="documentId">شناسه دیتابیسی مستند مرتبط با سند مالی که باید وضعیتش تغییر کند</param>
        /// <param name="entityId">شناسه دیتابیسی موجودیت</param>
        /// <param name="paraph">پاراف متنی که کاربر پیش از اقدام می تواند وارد کند</param>
        void Approve(int entityId, int documentId, string paraph = null);
    }
}
