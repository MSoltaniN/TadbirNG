﻿using System;
using System.Collections.Generic;
using SPPC.Tadbir.Values;

namespace SPPC.Tadbir.Workflow
{
    /// <summary>
    /// عملیات گردش کار کنترل وضعیت سند مالی را با استفاده از یک گردش کار متوالی
    /// (Sequential) پیاده سازی می کند.
    /// </summary>
    public class TransactionBasicWorkflow : TransactionWorkflow, ITransactionWorkflow
    {
        /// <summary>
        /// یک سند مالی پیش نویس را در حالت ثبت نشده و وضعیت عملیاتی تنظیم شده قرار می دهد.
        /// </summary>
        /// <param name="documentId">شناسه دیتابیسی سند مالی که باید حالت و وضعیتش تغییر کند</param>
        /// <param name="entityId">شناسه دیتابیسی موجودیت</param>
        /// <param name="paraph">پاراف متنی که کاربر پیش از اقدام می تواند وارد کند</param>
        public override void Prepare(int entityId, int documentId, string paraph = null)
        {
            var prepare = StateOperation.Prepare(
                CurrentUserId, entityId, documentId, DocumentTypeName.Transaction, paraph);
            InvokeServiceOperation(prepare);
            LogOperation(entityId, "Prepare", "prepared");
        }

        /// <summary>
        /// یک سند مالی ثبت نشده و تنظیم شده را در وضعیت عملیاتی بررسی شده قرار می دهد.
        /// </summary>
        /// <param name="documentId">شناسه دیتابیسی سند مالی که باید وضعیتش تغییر کند</param>
        /// <param name="entityId">شناسه دیتابیسی موجودیت</param>
        /// <param name="paraph">پاراف متنی که کاربر پیش از اقدام می تواند وارد کند</param>
        public override void Review(int entityId, int documentId, string paraph = null)
        {
            var review = StateOperation.Review(
                CurrentUserId, entityId, documentId, DocumentTypeName.Transaction, paraph);
            InvokeServiceOperation(review);
            LogOperation(entityId, "Review", "reviewed");
        }

        /// <summary>
        /// یک سند مالی ثبت نشده و بررسی شده را برای بررسی مجدد در وضعیت عملیاتی تنظیم شده قرار می دهد.
        /// </summary>
        /// <param name="documentId">شناسه دیتابیسی سند مالی که باید وضعیتش تغییر کند</param>
        /// <param name="entityId">شناسه دیتابیسی موجودیت</param>
        /// <param name="paraph">پاراف متنی که کاربر پیش از اقدام می تواند وارد کند</param>
        public override void RejectReviewed(int entityId, int documentId, string paraph = null)
        {
            var reject = StateOperation.RejectReview(
                CurrentUserId, entityId, documentId, DocumentTypeName.Transaction, paraph);
            InvokeServiceOperation(reject);
            LogOperation(entityId, "Reject", "rejected");
        }

        /// <summary>
        /// یک سند مالی ثبت نشده و بررسی شده را در حالت ثبت عادی و وضعیت عملیاتی تایید شده قرار می دهد.
        /// </summary>
        /// <param name="documentId">شناسه دیتابیسی سند مالی که باید وضعیتش تغییر کند</param>
        /// <param name="entityId">شناسه دیتابیسی موجودیت</param>
        /// <param name="paraph">پاراف متنی که کاربر پیش از اقدام می تواند وارد کند</param>
        public override void Confirm(int entityId, int documentId, string paraph = null)
        {
            var confirm = StateOperation.Confirm(
                CurrentUserId, entityId, documentId, DocumentTypeName.Transaction, paraph);
            InvokeServiceOperation(confirm);
            LogOperation(entityId, "Confirm", "confirmed");
        }

        /// <summary>
        /// یک سند مالی ثبت عادی و تایید شده را در حالت ثبت قطعی و وضعیت عملیاتی تصویب شده قرار می دهد.
        /// </summary>
        /// <param name="documentId">شناسه دیتابیسی سند مالی که باید وضعیتش تغییر کند</param>
        /// <param name="entityId">شناسه دیتابیسی موجودیت</param>
        /// <param name="paraph">پاراف متنی که کاربر پیش از اقدام می تواند وارد کند</param>
        public override void Approve(int entityId, int documentId, string paraph = null)
        {
            var approve = StateOperation.Approve(
                CurrentUserId, entityId, documentId, DocumentTypeName.Transaction, paraph);
            InvokeServiceOperation(approve);
            LogOperation(entityId, "Approve", "approved");
        }

        private static void InvokeServiceOperation(StateOperation operation)
        {
            using (var client = new DocumentStateBasicClient())
            {
                client.DoRequest(operation);
                client.Close();
            }
        }
    }
}
