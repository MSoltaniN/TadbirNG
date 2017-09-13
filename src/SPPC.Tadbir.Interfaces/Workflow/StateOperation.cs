using System;
using System.Runtime.Serialization;
using SPPC.Tadbir.Values;

namespace SPPC.Tadbir.Workflow
{
    /// <summary>
    /// اطلاعات مورد نیاز برای درخواست تغییر وضعیت یک موجودیت عملیاتی را نگهداری می کند.
    /// </summary>
    [DataContract(Name = "StateOperation", Namespace = "http://schemas.datacontract.org/2004/07/SPPC.Tadbir.Workflow")]
    [Serializable]
    public class StateOperation
    {
        /// <summary>
        /// شناسه دیتابیسی کاربری که تغییر وضعیت به نام او ثبت می شود
        /// </summary>
        [DataMember]
        public int CreatedById { get; set; }

        /// <summary>
        /// شناسه دیتابیسی نقش سازمانی که باید اقدام بعدی را روی موجودیت عملیاتی انجام دهد
        /// </summary>
        [DataMember]
        public int TargetId { get; set; }

        /// <summary>
        /// عنوان مورد استفاده برای کار ایجاد شده در کارتابل گیرنده
        /// </summary>
        [DataMember]
        public string Title { get; set; }

        /// <summary>
        /// شناسه دیتابیسی موجودیت عملیاتی که اقدام جاری برای آن انجام می شود
        /// </summary>
        [DataMember]
        public int EntityId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی مستند مرتبط با موجودیت عملیاتی که اقدام جاری برای آن انجام می شود
        /// </summary>
        [DataMember]
        public int DocumentId { get; set; }

        /// <summary>
        /// نوع موجودیت عملیاتی در سیستم
        /// </summary>
        [DataMember]
        public string DocumentType { get; set; }

        /// <summary>
        /// شناسه دیتابیسی وضعیت ثبتی موجودیت بعد از تکمیل اقدام جاری
        /// </summary>
        [DataMember]
        public int StatusId { get; set; }

        /// <summary>
        /// وضعیت عملیاتی موجودیت پیش از انجام اقدام جاری
        /// </summary>
        [DataMember]
        public string CurrentStatus { get; set; }

        /// <summary>
        /// وضعیت عملیاتی موجودیت پس از تکمیل اقدام جاری
        /// </summary>
        [DataMember]
        public string NewStatus { get; set; }

        /// <summary>
        /// نوع اقدامی که در نتیجه عملیات روی مستند انجام می شود
        /// </summary>
        [DataMember]
        public string Action { get; set; }

        /// <summary>
        /// نوع اقدام بعدی که پس از عملیات می تواند روی مستند انجام شود.
        /// </summary>
        [DataMember]
        public string NextAction { get; set; }

        /// <summary>
        /// پاراف متنی اختیاری که کاربر پیش از اقدام می تواند وارد کند
        /// </summary>
        [DataMember]
        public string Remarks { get; set; }

        /// <summary>
        /// مقداری که مشخص می کند آیا اقدام جاری تنظیم مستند است یا نه
        /// </summary>
        public bool IsPrepare
        {
            get { return (Action == DocumentActionName.Prepare && CurrentStatus == DocumentStatusName.Created); }
        }

        /// <summary>
        /// مقداری که مشخص می کند آیا اقدام جاری بررسی مستند است یا نه
        /// </summary>
        public bool IsReview
        {
            get { return (Action == DocumentActionName.Review && CurrentStatus == DocumentStatusName.Prepared); }
        }

        /// <summary>
        /// مقداری که مشخص می کند آیا اقدام جاری بررسی مجدد مستند است یا نه
        /// </summary>
        public bool IsReject
        {
            get { return (Action == DocumentActionName.Reject && CurrentStatus == DocumentStatusName.Reviewed); }
        }

        /// <summary>
        /// مقداری که مشخص می کند آیا اقدام جاری تایید مستند است یا نه
        /// </summary>
        public bool IsConfirm
        {
            get { return (Action == DocumentActionName.Confirm && CurrentStatus == DocumentStatusName.Reviewed); }
        }

        /// <summary>
        /// مقداری که مشخص می کند آیا اقدام جاری تصویب مستند است یا نه
        /// </summary>
        public bool IsApprove
        {
            get { return (Action == DocumentActionName.Approve && CurrentStatus == DocumentStatusName.Confirmed); }
        }

        /// <summary>
        /// مقداری که مشخص می کند آیا اقدام جاری با توجه به وضعیت های فعلی مستند، مجاز است یا نه
        /// </summary>
        public bool IsSupported
        {
            get { return (IsPrepare || IsReview || IsReject || IsConfirm || IsApprove); }
        }

        /// <summary>
        /// تابع سازنده برای ایجاد یک اقدام جدید از نوع تنظیم
        /// </summary>
        /// <param name="userId">شناسه دیتابیسی کاربر تنظیم کننده</param>
        /// <param name="entityId">شناسه دیتابیسی موجودیت</param>
        /// <param name="documentId">شناسه دیتابیسی مستند مورد نظر برای تنظیم</param>
        /// <param name="documentType">نوع مستند مورد نظر برای تنظیم</param>
        /// <param name="paraph">پاراف متنی که کاربر پیش از اقدام می تواند وارد کند</param>
        /// <returns></returns>
        public static StateOperation Prepare(
            int userId, int entityId, int documentId, string documentType, string paraph = null)
        {
            return new StateOperation()
            {
                CreatedById = userId,
                DocumentId = documentId,
                DocumentType = documentType,
                Action = DocumentActionName.Prepare,
                Remarks = paraph
            };
        }

        /// <summary>
        /// تابع سازنده برای ایجاد یک اقدام جدید از نوع بررسی
        /// </summary>
        /// <param name="userId">شناسه دیتابیسی کاربر بررسی کننده</param>
        /// <param name="entityId">شناسه دیتابیسی موجودیت</param>
        /// <param name="documentId">شناسه دیتابیسی مستند مورد نظر برای بررسی</param>
        /// <param name="documentType">نوع مستند مورد نظر برای بررسی</param>
        /// <param name="paraph">پاراف متنی که کاربر پیش از اقدام می تواند وارد کند</param>
        /// <returns></returns>
        public static StateOperation Review(
            int userId, int entityId, int documentId, string documentType, string paraph = null)
        {
            return new StateOperation()
            {
                CreatedById = userId,
                DocumentId = documentId,
                DocumentType = documentType,
                Action = DocumentActionName.Review,
                Remarks = paraph
            };
        }

        /// <summary>
        /// تابع سازنده برای ایجاد یک اقدام جدید از نوع بررسی مجدد
        /// </summary>
        /// <param name="userId">شناسه دیتابیسی کاربر اقدام کننده</param>
        /// <param name="entityId">شناسه دیتابیسی موجودیت</param>
        /// <param name="documentId">شناسه دیتابیسی مستند مورد نظر برای اقدام</param>
        /// <param name="documentType">نوع مستند مورد نظر برای اقدام</param>
        /// <param name="paraph">پاراف متنی که کاربر پیش از اقدام می تواند وارد کند</param>
        /// <returns></returns>
        public static StateOperation RejectReview(
            int userId, int entityId, int documentId, string documentType, string paraph = null)
        {
            return new StateOperation()
            {
                CreatedById = userId,
                DocumentId = documentId,
                DocumentType = documentType,
                Action = DocumentActionName.Reject,
                Remarks = paraph
            };
        }

        /// <summary>
        /// تابع سازنده برای ایجاد یک اقدام جدید از نوع تایید
        /// </summary>
        /// <param name="userId">شناسه دیتابیسی کاربر تایید کننده</param>
        /// <param name="entityId">شناسه دیتابیسی موجودیت</param>
        /// <param name="documentId">شناسه دیتابیسی مستند مورد نظر برای تایید</param>
        /// <param name="documentType">نوع مستند مورد نظر برای تایید</param>
        /// <param name="paraph">پاراف متنی که کاربر پیش از اقدام می تواند وارد کند</param>
        /// <returns></returns>
        public static StateOperation Confirm(
            int userId, int entityId, int documentId, string documentType, string paraph = null)
        {
            return new StateOperation()
            {
                CreatedById = userId,
                DocumentId = documentId,
                DocumentType = documentType,
                Action = DocumentActionName.Confirm,
                Remarks = paraph
            };
        }

        /// <summary>
        /// تابع سازنده برای ایجاد یک اقدام جدید از نوع تصویب
        /// </summary>
        /// <param name="userId">شناسه دیتابیسی کاربر تصویب کننده</param>
        /// <param name="entityId">شناسه دیتابیسی موجودیت</param>
        /// <param name="documentId">شناسه دیتابیسی مستند مورد نظر برای تصویب</param>
        /// <param name="documentType">نوع مستند مورد نظر برای تصویب</param>
        /// <param name="paraph">پاراف متنی که کاربر پیش از اقدام می تواند وارد کند</param>
        /// <returns></returns>
        public static StateOperation Approve(
            int userId, int entityId, int documentId, string documentType, string paraph = null)
        {
            return new StateOperation()
            {
                CreatedById = userId,
                DocumentId = documentId,
                DocumentType = documentType,
                Action = DocumentActionName.Approve,
                Remarks = paraph
            };
        }
    }
}
