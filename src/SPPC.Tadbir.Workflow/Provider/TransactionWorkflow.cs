using System;
using System.Collections.Generic;
using System.Diagnostics;
using BabakSoft.Platform.Common;
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
        /// <param name="paraph">پاراف متنی که کاربر پیش از اقدام می تواند وارد کند</param>
        public virtual void Prepare(int documentId, string paraph = null)
        {
            var prepare = StateOperation.Prepare(CurrentUserId, documentId, DocumentTypeName.Transaction, paraph);
            using (var client = new DocumentStateClient())
            {
                client.Prepare(prepare);
                client.Close();
            }

            LogOperation(documentId, "Prepare", "prepared");
        }

        /// <summary>
        /// یک سند مالی ثبت نشده و تنظیم شده را در وضعیت عملیاتی بررسی شده قرار می دهد.
        /// </summary>
        /// <param name="documentId">شناسه دیتابیسی مستند مرتبط با سند مالی که باید وضعیتش تغییر کند</param>
        /// <param name="paraph">پاراف متنی که کاربر پیش از اقدام می تواند وارد کند</param>
        public virtual void Review(int documentId, string paraph = null)
        {
            var review = StateOperation.Review(CurrentUserId, documentId, DocumentTypeName.Transaction, paraph);
            using (var client = new DocumentStateClient())
            {
                client.Review(review);
                client.Close();
            }

            LogOperation(documentId, "Review", "reviewed");
        }

        /// <summary>
        /// یک سند مالی ثبت نشده و بررسی شده را برای بررسی مجدد در وضعیت عملیاتی تنظیم شده قرار می دهد.
        /// </summary>
        /// <param name="documentId">شناسه دیتابیسی مستند مرتبط با سند مالی که باید وضعیتش تغییر کند</param>
        /// <param name="paraph">پاراف متنی که کاربر پیش از اقدام می تواند وارد کند</param>
        public virtual void RejectReviewed(int documentId, string paraph = null)
        {
            var reject = StateOperation.RejectReview(CurrentUserId, documentId, DocumentTypeName.Transaction, paraph);
            using (var client = new DocumentStateClient())
            {
                client.Reject(reject);
                client.Close();
            }

            LogOperation(documentId, "RejectReview", "rejected");
        }

        /// <summary>
        /// یک سند مالی ثبت نشده و بررسی شده را در حالت ثبت عادی و وضعیت عملیاتی تایید شده قرار می دهد.
        /// </summary>
        /// <param name="documentId">شناسه دیتابیسی مستند مرتبط با سند مالی که باید وضعیتش تغییر کند</param>
        /// <param name="paraph">پاراف متنی که کاربر پیش از اقدام می تواند وارد کند</param>
        public virtual void Confirm(int documentId, string paraph = null)
        {
            var confirm = StateOperation.Confirm(CurrentUserId, documentId, DocumentTypeName.Transaction, paraph);
            using (var client = new DocumentStateClient())
            {
                client.Confirm(confirm);
                client.Close();
            }

            LogOperation(documentId, "Confirm", "confirmed");
        }

        /// <summary>
        /// یک سند مالی ثبت عادی و تایید شده را در حالت ثبت قطعی و وضعیت عملیاتی تصویب شده قرار می دهد.
        /// </summary>
        /// <param name="documentId">شناسه دیتابیسی مستند مرتبط با سند مالی که باید وضعیتش تغییر کند</param>
        /// <param name="paraph">پاراف متنی که کاربر پیش از اقدام می تواند وارد کند</param>
        public virtual void Approve(int documentId, string paraph = null)
        {
            var approve = StateOperation.Approve(CurrentUserId, documentId, DocumentTypeName.Transaction, paraph);
            using (var client = new DocumentStateClient())
            {
                client.Approve(approve);
                client.Close();
            }

            LogOperation(documentId, "Approve", "approved");
        }

        /// <summary>
        /// مجموعه ای از اسناد مالی پیش نویس را در حالت ثبت نشده و وضعیت عملیاتی تنظیم شده قرار می دهد.
        /// </summary>
        /// <param name="documents">مجموعه شناسه های مستندهایی که باید وضعیتشان تغییر کند</param>
        /// <param name="paraph">پاراف متنی که کاربر پیش از اقدام می تواند وارد کند</param>
        public virtual void PrepareMultiple(IEnumerable<int> documents, string paraph = null)
        {
            Verify.ArgumentNotNull(documents, "documents");
            using (var client = new DocumentStateClient())
            {
                foreach (int transactionId in documents)
                {
                    var prepare = StateOperation.Prepare(CurrentUserId, transactionId, DocumentTypeName.Transaction, paraph);
                    client.Prepare(prepare);
                    LogOperation(transactionId, "Prepare", "prepared");
                }

                client.Close();
            }
        }

        /// <summary>
        /// مجموعه ای از اسناد مالی ثبت نشده و تنظیم شده را در وضعیت عملیاتی بررسی شده قرار می دهد.
        /// </summary>
        /// <param name="documents">مجموعه شناسه های مستندهایی که باید وضعیتشان تغییر کند</param>
        /// <param name="paraph">پاراف متنی که کاربر پیش از اقدام می تواند وارد کند</param>
        public virtual void ReviewMultiple(IEnumerable<int> documents, string paraph = null)
        {
            Verify.ArgumentNotNull(documents, "documents");
            using (var client = new DocumentStateClient())
            {
                foreach (int transactionId in documents)
                {
                    var review = StateOperation.Review(CurrentUserId, transactionId, DocumentTypeName.Transaction, paraph);
                    client.Review(review);
                    LogOperation(transactionId, "Review", "reviewed");
                }

                client.Close();
            }
        }

        /// <summary>
        /// مجموعه ای از اسناد مالی ثبت نشده و بررسی شده را برای بررسی مجدد در وضعیت عملیاتی تنظیم شده قرار می دهد.
        /// </summary>
        /// <param name="documents">مجموعه شناسه های مستندهایی که باید وضعیتشان تغییر کند</param>
        /// <param name="paraph">پاراف متنی که کاربر پیش از اقدام می تواند وارد کند</param>
        public virtual void RejectReviewedMultiple(IEnumerable<int> documents, string paraph = null)
        {
            Verify.ArgumentNotNull(documents, "documents");
            using (var client = new DocumentStateClient())
            {
                foreach (int transactionId in documents)
                {
                    var reject = StateOperation.RejectReview(CurrentUserId, transactionId, DocumentTypeName.Transaction, paraph);
                    client.Reject(reject);
                    LogOperation(transactionId, "Reject", "rejected");
                }

                client.Close();
            }
        }

        /// <summary>
        /// مجموعه ای از اسناد مالی ثبت نشده و بررسی شده را در حالت ثبت عادی و وضعیت عملیاتی تایید شده قرار می دهد.
        /// </summary>
        /// <param name="documents">مجموعه شناسه های مستندهایی که باید وضعیتشان تغییر کند</param>
        /// <param name="paraph">پاراف متنی که کاربر پیش از اقدام می تواند وارد کند</param>
        public virtual void ConfirmMultiple(IEnumerable<int> documents, string paraph = null)
        {
            Verify.ArgumentNotNull(documents, "documents");
            using (var client = new DocumentStateClient())
            {
                foreach (int transactionId in documents)
                {
                    var confirm = StateOperation.Confirm(CurrentUserId, transactionId, DocumentTypeName.Transaction, paraph);
                    client.Confirm(confirm);
                    LogOperation(transactionId, "Confirm", "confirmed");
                }

                client.Close();
            }
        }

        /// <summary>
        /// مجموعه ای از اسناد مالی ثبت عادی و تایید شده را در حالت ثبت قطعی و وضعیت عملیاتی تصویب شده قرار می دهد.
        /// </summary>
        /// <param name="documents">مجموعه شناسه های مستندهایی که باید وضعیتشان تغییر کند</param>
        /// <param name="paraph">پاراف متنی که کاربر پیش از اقدام می تواند وارد کند</param>
        public virtual void ApproveMultiple(IEnumerable<int> documents, string paraph = null)
        {
            Verify.ArgumentNotNull(documents, "documents");
            using (var client = new DocumentStateClient())
            {
                foreach (int transactionId in documents)
                {
                    var approve = StateOperation.Approve(CurrentUserId, transactionId, DocumentTypeName.Transaction, paraph);
                    client.Approve(approve);
                    LogOperation(transactionId, "Approve", "approved");
                }

                client.Close();
            }
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
