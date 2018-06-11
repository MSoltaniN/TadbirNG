using System;
using System.Collections.Generic;
using SPPC.Tadbir.Service;
using SPPC.Tadbir.Values;

namespace SPPC.Tadbir.Workflow
{
    /// <summary>
    /// عملیات گردش کار کنترل وضعیت سند مالی را با استفاده از یک نمودار گردشی
    /// (Flow chart) پیاده سازی می کند.
    /// </summary>
    /// <remarks>
    /// در این پیاده سازی، اسناد مالی با مبلغ کل کم (در حال حاضر حداکثر 1 میلیون ریال) به کارتابل معاون مالی
    /// و اسناد مالی با مبلغ کل زیاد (در حال حاضر بیشتر از 1 میلیون ریال) مستقیما به کارتابل مدیر مالی می روند.
    /// </remarks>
    public class VoucherDecisionWorkflow : VoucherWorkflow, IVoucherWorkflow
    {
        public VoucherDecisionWorkflow(ISecurityContextManager contextManager)
            : base(contextManager)
        {
        }

        /// <summary>
        /// یک سند مالی پیش نویس را در حالت ثبت نشده و وضعیت عملیاتی تنظیم شده قرار می دهد.
        /// </summary>
        /// <param name="entityId">شناسه دیتابیسی موجودیت</param>
        /// <param name="documentId">شناسه دیتابیسی سند مالی که باید حالت و وضعیتش تغییر کند</param>
        /// <param name="paraph">پاراف متنی که کاربر پیش از اقدام می تواند وارد کند</param>
        public override void Prepare(int entityId, int documentId, string paraph = null)
        {
            var prepare = StateOperation.Prepare(
                CurrentUserId, entityId, documentId, DocumentTypeName.Voucher, paraph);
            using (var client = new DocumentStateDecisionClient())
            {
                client.Prepare(prepare);
                client.Close();
            }

            LogOperation(entityId, "Prepare", "prepared");
        }

        /// <summary>
        /// یک سند مالی ثبت نشده و تنظیم شده را در وضعیت عملیاتی بررسی شده قرار می دهد.
        /// </summary>
        /// <param name="entityId">شناسه دیتابیسی موجودیت</param>
        /// <param name="documentId">شناسه دیتابیسی سند مالی که باید وضعیتش تغییر کند</param>
        /// <param name="paraph">پاراف متنی که کاربر پیش از اقدام می تواند وارد کند</param>
        public override void Review(int entityId, int documentId, string paraph = null)
        {
            var review = StateOperation.Review(
                CurrentUserId, entityId, documentId, DocumentTypeName.Voucher, paraph);
            using (var client = new DocumentStateDecisionClient())
            {
                client.Review(review);
                client.Close();
            }

            LogOperation(entityId, "Review", "reviewed");
        }

        /// <summary>
        /// یک سند مالی ثبت نشده و بررسی شده را برای بررسی مجدد در وضعیت عملیاتی تنظیم شده قرار می دهد.
        /// </summary>
        /// <param name="entityId">شناسه دیتابیسی موجودیت</param>
        /// <param name="documentId">شناسه دیتابیسی سند مالی که باید وضعیتش تغییر کند</param>
        /// <param name="paraph">پاراف متنی که کاربر پیش از اقدام می تواند وارد کند</param>
        public override void Reject(int entityId, int documentId, string paraph = null)
        {
            var reject = StateOperation.RejectReview(
                CurrentUserId, entityId, documentId, DocumentTypeName.Voucher, paraph);
            using (var client = new DocumentStateDecisionClient())
            {
                client.Reject(reject);
                client.Close();
            }

            LogOperation(entityId, "RejectReview", "rejected");
        }

        /// <summary>
        /// یک سند مالی ثبت نشده و بررسی شده را در حالت ثبت عادی و وضعیت عملیاتی تایید شده قرار می دهد.
        /// </summary>
        /// <param name="entityId">شناسه دیتابیسی موجودیت</param>
        /// <param name="documentId">شناسه دیتابیسی سند مالی که باید وضعیتش تغییر کند</param>
        /// <param name="paraph">پاراف متنی که کاربر پیش از اقدام می تواند وارد کند</param>
        public override void Confirm(int entityId, int documentId, string paraph = null)
        {
            var confirm = StateOperation.Confirm(
                CurrentUserId, entityId, documentId, DocumentTypeName.Voucher, paraph);
            using (var client = new DocumentStateDecisionClient())
            {
                client.Confirm(confirm);
                client.Close();
            }

            LogOperation(entityId, "Confirm", "confirmed");
        }

        /// <summary>
        /// یک سند مالی ثبت عادی و تایید شده را در حالت ثبت قطعی و وضعیت عملیاتی تصویب شده قرار می دهد.
        /// </summary>
        /// <param name="entityId">شناسه دیتابیسی موجودیت</param>
        /// <param name="documentId">شناسه دیتابیسی سند مالی که باید وضعیتش تغییر کند</param>
        /// <param name="paraph">پاراف متنی که کاربر پیش از اقدام می تواند وارد کند</param>
        public override void Approve(int entityId, int documentId, string paraph = null)
        {
            var approve = StateOperation.Approve(
                CurrentUserId, entityId, documentId, DocumentTypeName.Voucher, paraph);
            using (var client = new DocumentStateDecisionClient())
            {
                client.Approve(approve);
                client.Close();
            }

            LogOperation(entityId, "Approve", "approved");
        }
    }
}
