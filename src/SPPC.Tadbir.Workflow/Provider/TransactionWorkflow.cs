﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using SPPC.Tadbir.Service;
using SPPC.Tadbir.Values;

namespace SPPC.Tadbir.Workflow
{
    /// <summary>
    /// عملیات گردش کار کنترل وضعیت سند مالی را با استفاده از یک ماشین حالت پیاده سازی می کند.
    /// </summary>
    public class TransactionWorkflow : ITransactionWorkflow
    {
        /// <summary>
        /// اطلاعات امنیتی کاربر جاری در برنامه
        /// </summary>
        public ISecurityContextManager ContextManager { get; set; }

        /// <summary>
        /// یک سند مالی پیش نویس را در حالت ثبت نشده و وضعیت عملیاتی تنظیم شده قرار می دهد.
        /// </summary>
        /// <param name="transactionId">شناسه دیتابیسی سند مالی که باید حالت و وضعیتش تغییر کند</param>
        public virtual void Prepare(int transactionId)
        {
            var prepare = StateOperation.Prepare(CurrentUserId, transactionId, DocumentType.Transaction);
            using (var client = new DocumentStateClient())
            {
                client.Prepare(prepare);
                client.Close();
            }

            LogOperation(transactionId, "Prepare", "prepared");
        }

        /// <summary>
        /// یک سند مالی ثبت نشده و تنظیم شده را در وضعیت عملیاتی بررسی شده قرار می دهد.
        /// </summary>
        /// <param name="transactionId">شناسه دیتابیسی سند مالی که باید وضعیتش تغییر کند</param>
        public virtual void Review(int transactionId)
        {
            var review = StateOperation.Review(CurrentUserId, transactionId, DocumentType.Transaction);
            using (var client = new DocumentStateClient())
            {
                client.Review(review);
                client.Close();
            }

            LogOperation(transactionId, "Review", "reviewed");
        }

        /// <summary>
        /// یک سند مالی ثبت نشده و بررسی شده را برای بررسی مجدد در وضعیت عملیاتی تنظیم شده قرار می دهد.
        /// </summary>
        /// <param name="transactionId">شناسه دیتابیسی سند مالی که باید وضعیتش تغییر کند</param>
        public virtual void RejectReviewed(int transactionId)
        {
            var reject = StateOperation.RejectReview(CurrentUserId, transactionId, DocumentType.Transaction);
            using (var client = new DocumentStateClient())
            {
                client.Reject(reject);
                client.Close();
            }

            LogOperation(transactionId, "RejectReview", "rejected");
        }

        /// <summary>
        /// یک سند مالی ثبت نشده و بررسی شده را در حالت ثبت عادی و وضعیت عملیاتی تایید شده قرار می دهد.
        /// </summary>
        /// <param name="transactionId">شناسه دیتابیسی سند مالی که باید وضعیتش تغییر کند</param>
        public virtual void Confirm(int transactionId)
        {
            var confirm = StateOperation.Confirm(CurrentUserId, transactionId, DocumentType.Transaction);
            using (var client = new DocumentStateClient())
            {
                client.Confirm(confirm);
                client.Close();
            }

            LogOperation(transactionId, "Confirm", "confirmed");
        }

        /// <summary>
        /// یک سند مالی ثبت عادی و تایید شده را در حالت ثبت قطعی و وضعیت عملیاتی تصویب شده قرار می دهد.
        /// </summary>
        /// <param name="transactionId">شناسه دیتابیسی سند مالی که باید وضعیتش تغییر کند</param>
        public virtual void Approve(int transactionId)
        {
            var approve = StateOperation.Approve(CurrentUserId, transactionId, DocumentType.Transaction);
            using (var client = new DocumentStateClient())
            {
                client.Approve(approve);
                client.Close();
            }

            LogOperation(transactionId, "Approve", "approved");
        }

        /// <summary>
        /// مجموعه ای از اسناد مالی پیش نویس را در حالت ثبت نشده و وضعیت عملیاتی تنظیم شده قرار می دهد.
        /// </summary>
        /// <param name="transactions">مجموعه شناسه های مالی اسنادی که باید وضعیتشان تغییر کند</param>
        public void PrepareMultiple(IEnumerable<int> transactions)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// مجموعه ای از اسناد مالی ثبت نشده و تنظیم شده را در وضعیت عملیاتی بررسی شده قرار می دهد.
        /// </summary>
        /// <param name="transactions">مجموعه شناسه های مالی اسنادی که باید وضعیتشان تغییر کند</param>
        public void ReviewMultiple(IEnumerable<int> transactions)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// مجموعه ای از اسناد مالی ثبت نشده و بررسی شده را برای بررسی مجدد در وضعیت عملیاتی تنظیم شده قرار می دهد.
        /// </summary>
        /// <param name="transactions">مجموعه شناسه های مالی اسنادی که باید وضعیتشان تغییر کند</param>
        public void RejectReviewedMultiple(IEnumerable<int> transactions)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// مجموعه ای از اسناد مالی ثبت نشده و بررسی شده را در حالت ثبت عادی و وضعیت عملیاتی تایید شده قرار می دهد.
        /// </summary>
        /// <param name="transactions">مجموعه شناسه های مالی اسنادی که باید وضعیتشان تغییر کند</param>
        public void ConfirmMultiple(IEnumerable<int> transactions)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// مجموعه ای از اسناد مالی ثبت عادی و تایید شده را در حالت ثبت قطعی و وضعیت عملیاتی تصویب شده قرار می دهد.
        /// </summary>
        /// <param name="transactions">مجموعه شناسه های مالی اسنادی که باید وضعیتشان تغییر کند</param>
        public void ApproveMultiple(IEnumerable<int> transactions)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// شناسه دیتابیسی کاربر جاری برنامه
        /// </summary>
        protected int CurrentUserId
        {
            get { return ContextManager.CurrentContext.User.Id; }
        }

        /// <summary>
        /// لاگ مربوط به عملیات را در نمای خروجی ویژوال استودیو ایجاد می کند.
        /// </summary>
        /// <param name="transactionId">شناسه دیتابیسی سند مالی مورد اقدام</param>
        /// <param name="title">نام انگلیسی اقدام جاری</param>
        /// <param name="completedText"></param>
        protected void LogOperation(int transactionId, string title, string completedText)
        {
            Debug.WriteLine(
                "{0}=================================================================={0}" +
                "{1}: Transaction '[id]={2}' is {3} by user '[id]={4}'.{0}" +
                "==================================================================",
                Environment.NewLine, title, transactionId, completedText, CurrentUserId);
        }
    }
}
