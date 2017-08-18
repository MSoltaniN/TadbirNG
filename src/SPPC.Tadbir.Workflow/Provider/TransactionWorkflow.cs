using System;
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
        /// <param name="documentId">شناسه دیتابیسی مستند مرتبط با سند مالی که باید حالت و وضعیتش تغییر کند</param>
        /// <param name="entityId">شناسه دیتابیسی موجودیت</param>
        /// <param name="paraph">پاراف متنی که کاربر پیش از اقدام می تواند وارد کند</param>
        public virtual void Prepare(int entityId, int documentId, string paraph = null)
        {
            var prepare = StateOperation.Prepare(
                CurrentUserId, entityId, documentId, DocumentTypeName.Transaction, paraph);
            using (var client = new DocumentStateClient())
            {
                client.Prepare(prepare);
                client.Close();
            }

            LogOperation(entityId, "Prepare", "prepared");
        }

        /// <summary>
        /// یک سند مالی ثبت نشده و تنظیم شده را در وضعیت عملیاتی بررسی شده قرار می دهد.
        /// </summary>
        /// <param name="documentId">شناسه دیتابیسی مستند مرتبط با سند مالی که باید وضعیتش تغییر کند</param>
        /// <param name="entityId">شناسه دیتابیسی موجودیت</param>
        /// <param name="paraph">پاراف متنی که کاربر پیش از اقدام می تواند وارد کند</param>
        public virtual void Review(int entityId, int documentId, string paraph = null)
        {
            var review = StateOperation.Review(
                CurrentUserId, entityId, documentId, DocumentTypeName.Transaction, paraph);
            using (var client = new DocumentStateClient())
            {
                client.Review(review);
                client.Close();
            }

            LogOperation(entityId, "Review", "reviewed");
        }

        /// <summary>
        /// یک سند مالی ثبت نشده و بررسی شده را برای بررسی مجدد در وضعیت عملیاتی تنظیم شده قرار می دهد.
        /// </summary>
        /// <param name="documentId">شناسه دیتابیسی مستند مرتبط با سند مالی که باید وضعیتش تغییر کند</param>
        /// <param name="entityId">شناسه دیتابیسی موجودیت</param>
        /// <param name="paraph">پاراف متنی که کاربر پیش از اقدام می تواند وارد کند</param>
        public virtual void RejectReviewed(int entityId, int documentId, string paraph = null)
        {
            var reject = StateOperation.RejectReview(
                CurrentUserId, entityId, documentId, DocumentTypeName.Transaction, paraph);
            using (var client = new DocumentStateClient())
            {
                client.Reject(reject);
                client.Close();
            }

            LogOperation(entityId, "RejectReview", "rejected");
        }

        /// <summary>
        /// یک سند مالی ثبت نشده و بررسی شده را در حالت ثبت عادی و وضعیت عملیاتی تایید شده قرار می دهد.
        /// </summary>
        /// <param name="documentId">شناسه دیتابیسی مستند مرتبط با سند مالی که باید وضعیتش تغییر کند</param>
        /// <param name="entityId">شناسه دیتابیسی موجودیت</param>
        /// <param name="paraph">پاراف متنی که کاربر پیش از اقدام می تواند وارد کند</param>
        public virtual void Confirm(int entityId, int documentId, string paraph = null)
        {
            var confirm = StateOperation.Confirm(
                CurrentUserId, entityId, documentId, DocumentTypeName.Transaction, paraph);
            using (var client = new DocumentStateClient())
            {
                client.Confirm(confirm);
                client.Close();
            }

            LogOperation(entityId, "Confirm", "confirmed");
        }

        /// <summary>
        /// یک سند مالی ثبت عادی و تایید شده را در حالت ثبت قطعی و وضعیت عملیاتی تصویب شده قرار می دهد.
        /// </summary>
        /// <param name="documentId">شناسه دیتابیسی مستند مرتبط با سند مالی که باید وضعیتش تغییر کند</param>
        /// <param name="entityId">شناسه دیتابیسی موجودیت</param>
        /// <param name="paraph">پاراف متنی که کاربر پیش از اقدام می تواند وارد کند</param>
        public virtual void Approve(int entityId, int documentId, string paraph = null)
        {
            var approve = StateOperation.Approve(
                CurrentUserId, entityId, documentId, DocumentTypeName.Transaction, paraph);
            using (var client = new DocumentStateClient())
            {
                client.Approve(approve);
                client.Close();
            }

            LogOperation(entityId, "Approve", "approved");
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
