using System;
using System.Collections.Generic;
using BabakSoft.Platform.Common;
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
        /// <param name="transactionId">شناسه دیتابیسی سند مالی که باید حالت و وضعیتش تغییر کند</param>
        /// <param name="paraph">پاراف متنی که کاربر پیش از اقدام می تواند وارد کند</param>
        public override void Prepare(int transactionId, string paraph = null)
        {
            var prepare = StateOperation.Prepare(CurrentUserId, transactionId, DocumentType.Transaction, paraph);
            InvokeServiceOperation(prepare);
            LogOperation(transactionId, "Prepare", "prepared");
        }

        /// <summary>
        /// یک سند مالی ثبت نشده و تنظیم شده را در وضعیت عملیاتی بررسی شده قرار می دهد.
        /// </summary>
        /// <param name="transactionId">شناسه دیتابیسی سند مالی که باید وضعیتش تغییر کند</param>
        /// <param name="paraph">پاراف متنی که کاربر پیش از اقدام می تواند وارد کند</param>
        public override void Review(int transactionId, string paraph = null)
        {
            var review = StateOperation.Review(CurrentUserId, transactionId, DocumentType.Transaction, paraph);
            InvokeServiceOperation(review);
            LogOperation(transactionId, "Review", "reviewed");
        }

        /// <summary>
        /// یک سند مالی ثبت نشده و بررسی شده را برای بررسی مجدد در وضعیت عملیاتی تنظیم شده قرار می دهد.
        /// </summary>
        /// <param name="transactionId">شناسه دیتابیسی سند مالی که باید وضعیتش تغییر کند</param>
        /// <param name="paraph">پاراف متنی که کاربر پیش از اقدام می تواند وارد کند</param>
        public override void RejectReviewed(int transactionId, string paraph = null)
        {
            var reject = StateOperation.RejectReview(CurrentUserId, transactionId, DocumentType.Transaction, paraph);
            InvokeServiceOperation(reject);
            LogOperation(transactionId, "Reject", "rejected");
        }

        /// <summary>
        /// یک سند مالی ثبت نشده و بررسی شده را در حالت ثبت عادی و وضعیت عملیاتی تایید شده قرار می دهد.
        /// </summary>
        /// <param name="transactionId">شناسه دیتابیسی سند مالی که باید وضعیتش تغییر کند</param>
        /// <param name="paraph">پاراف متنی که کاربر پیش از اقدام می تواند وارد کند</param>
        public override void Confirm(int transactionId, string paraph = null)
        {
            var confirm = StateOperation.Confirm(CurrentUserId, transactionId, DocumentType.Transaction, paraph);
            InvokeServiceOperation(confirm);
            LogOperation(transactionId, "Confirm", "confirmed");
        }

        /// <summary>
        /// یک سند مالی ثبت عادی و تایید شده را در حالت ثبت قطعی و وضعیت عملیاتی تصویب شده قرار می دهد.
        /// </summary>
        /// <param name="transactionId">شناسه دیتابیسی سند مالی که باید وضعیتش تغییر کند</param>
        /// <param name="paraph">پاراف متنی که کاربر پیش از اقدام می تواند وارد کند</param>
        public override void Approve(int transactionId, string paraph = null)
        {
            var approve = StateOperation.Approve(CurrentUserId, transactionId, DocumentType.Transaction, paraph);
            InvokeServiceOperation(approve);
            LogOperation(transactionId, "Approve", "approved");
        }

        /// <summary>
        /// مجموعه ای از اسناد مالی پیش نویس را در حالت ثبت نشده و وضعیت عملیاتی تنظیم شده قرار می دهد.
        /// </summary>
        /// <param name="transactions">مجموعه شناسه های مالی اسنادی که باید وضعیتشان تغییر کند</param>
        /// <param name="paraph">پاراف متنی که کاربر پیش از اقدام می تواند وارد کند</param>
        public override void PrepareMultiple(IEnumerable<int> transactions, string paraph = null)
        {
            Verify.ArgumentNotNull(transactions, "transactions");
            foreach (int transactionId in transactions)
            {
                var prepare = StateOperation.Prepare(CurrentUserId, transactionId, DocumentType.Transaction, paraph);
                InvokeServiceOperation(prepare);
                LogOperation(transactionId, "Prepare", "prepared");
            }
        }

        /// <summary>
        /// مجموعه ای از اسناد مالی ثبت نشده و تنظیم شده را در وضعیت عملیاتی بررسی شده قرار می دهد.
        /// </summary>
        /// <param name="transactions">مجموعه شناسه های مالی اسنادی که باید وضعیتشان تغییر کند</param>
        /// <param name="paraph">پاراف متنی که کاربر پیش از اقدام می تواند وارد کند</param>
        public override void ReviewMultiple(IEnumerable<int> transactions, string paraph = null)
        {
            Verify.ArgumentNotNull(transactions, "transactions");
            foreach (int transactionId in transactions)
            {
                var review = StateOperation.Review(CurrentUserId, transactionId, DocumentType.Transaction, paraph);
                InvokeServiceOperation(review);
                LogOperation(transactionId, "Review", "reviewed");
            }
        }

        /// <summary>
        /// مجموعه ای از اسناد مالی ثبت نشده و بررسی شده را برای بررسی مجدد در وضعیت عملیاتی تنظیم شده قرار می دهد.
        /// </summary>
        /// <param name="transactions">مجموعه شناسه های مالی اسنادی که باید وضعیتشان تغییر کند</param>
        /// <param name="paraph">پاراف متنی که کاربر پیش از اقدام می تواند وارد کند</param>
        public override void RejectReviewedMultiple(IEnumerable<int> transactions, string paraph = null)
        {
            Verify.ArgumentNotNull(transactions, "transactions");
            foreach (int transactionId in transactions)
            {
                var reject = StateOperation.RejectReview(CurrentUserId, transactionId, DocumentType.Transaction, paraph);
                InvokeServiceOperation(reject);
                LogOperation(transactionId, "Reject", "rejected");
            }
        }

        /// <summary>
        /// مجموعه ای از اسناد مالی ثبت نشده و بررسی شده را در حالت ثبت عادی و وضعیت عملیاتی تایید شده قرار می دهد.
        /// </summary>
        /// <param name="transactions">مجموعه شناسه های مالی اسنادی که باید وضعیتشان تغییر کند</param>
        /// <param name="paraph">پاراف متنی که کاربر پیش از اقدام می تواند وارد کند</param>
        public override void ConfirmMultiple(IEnumerable<int> transactions, string paraph = null)
        {
            Verify.ArgumentNotNull(transactions, "transactions");
            foreach (int transactionId in transactions)
            {
                var confirm = StateOperation.Confirm(CurrentUserId, transactionId, DocumentType.Transaction, paraph);
                InvokeServiceOperation(confirm);
                LogOperation(transactionId, "Confirm", "confirmed");
            }
        }

        /// <summary>
        /// مجموعه ای از اسناد مالی ثبت عادی و تایید شده را در حالت ثبت قطعی و وضعیت عملیاتی تصویب شده قرار می دهد.
        /// </summary>
        /// <param name="transactions">مجموعه شناسه های مالی اسنادی که باید وضعیتشان تغییر کند</param>
        /// <param name="paraph">پاراف متنی که کاربر پیش از اقدام می تواند وارد کند</param>
        public override void ApproveMultiple(IEnumerable<int> transactions, string paraph = null)
        {
            Verify.ArgumentNotNull(transactions, "transactions");
            foreach (int transactionId in transactions)
            {
                var approve = StateOperation.Approve(CurrentUserId, transactionId, DocumentType.Transaction, paraph);
                InvokeServiceOperation(approve);
                LogOperation(transactionId, "Approve", "approved");
            }
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
