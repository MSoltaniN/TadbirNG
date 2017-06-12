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
        public int DocumentId { get; set; }

        /// <summary>
        /// نوع موجودیت عملیاتی در سیستم
        /// </summary>
        [DataMember]
        public string DocumentType { get; set; }

        /// <summary>
        /// وضعیت ثبتی موجودیت بعد از تکمیل اقدام جاری
        /// </summary>
        [DataMember]
        public string Status { get; set; }

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
        /// مقداری که مشخص می کند آیا اقدام جاری تنظیم مستند است یا نه
        /// </summary>
        public bool IsPrepare
        {
            get { return (Action == DocumentAction.Prepare && CurrentStatus == DocumentStatus.Created); }
        }

        /// <summary>
        /// مقداری که مشخص می کند آیا اقدام جاری بررسی مستند است یا نه
        /// </summary>
        public bool IsReview
        {
            get { return (Action == DocumentAction.Review && CurrentStatus == DocumentStatus.Prepared); }
        }

        /// <summary>
        /// مقداری که مشخص می کند آیا اقدام جاری بررسی مجدد مستند است یا نه
        /// </summary>
        public bool IsReject
        {
            get { return (Action == DocumentAction.Reject && CurrentStatus == DocumentStatus.Reviewed); }
        }

        /// <summary>
        /// مقداری که مشخص می کند آیا اقدام جاری تایید مستند است یا نه
        /// </summary>
        public bool IsConfirm
        {
            get { return (Action == DocumentAction.Confirm && CurrentStatus == DocumentStatus.Reviewed); }
        }

        /// <summary>
        /// مقداری که مشخص می کند آیا اقدام جاری تصویب مستند است یا نه
        /// </summary>
        public bool IsApprove
        {
            get { return (Action == DocumentAction.Approve && CurrentStatus == DocumentStatus.Confirmed); }
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
        /// <param name="documentId">شناسه دیتابیسی مستند مورد نظر برای تنظیم</param>
        /// <param name="documentType">نوع مستند مورد نظر برای تنظیم</param>
        /// <returns></returns>
        public static StateOperation Prepare(int userId, int documentId, string documentType)
        {
            return new StateOperation()
            {
                CreatedById = userId,
                TargetId = SystemRoles.LeadAccountant,
                Title = WorkItemTitle.ReviewDocument,
                DocumentId = documentId,
                DocumentType = documentType,
                Status = TransactionStatus.Unchecked,
                CurrentStatus = DocumentStatus.Created,
                NewStatus = DocumentStatus.Prepared,
                Action = DocumentAction.Prepare,
                NextAction = DocumentAction.Review
            };
        }

        /// <summary>
        /// تابع سازنده برای ایجاد یک اقدام جدید از نوع بررسی
        /// </summary>
        /// <param name="userId">شناسه دیتابیسی کاربر بررسی کننده</param>
        /// <param name="documentId">شناسه دیتابیسی مستند مورد نظر برای بررسی</param>
        /// <param name="documentType">نوع مستند مورد نظر برای بررسی</param>
        /// <returns></returns>
        public static StateOperation Review(int userId, int documentId, string documentType)
        {
            return new StateOperation()
            {
                CreatedById = userId,
                TargetId = SystemRoles.CFOAssistant,
                Title = WorkItemTitle.ConfirmDocument,
                DocumentId = documentId,
                DocumentType = documentType,
                Status = TransactionStatus.NormalCheck,
                CurrentStatus = DocumentStatus.Prepared,
                NewStatus = DocumentStatus.Reviewed,
                Action = DocumentAction.Review,
                NextAction = DocumentAction.Confirm
            };
        }

        /// <summary>
        /// تابع سازنده برای ایجاد یک اقدام جدید از نوع بررسی مجدد
        /// </summary>
        /// <param name="userId">شناسه دیتابیسی کاربر اقدام کننده</param>
        /// <param name="documentId">شناسه دیتابیسی مستند مورد نظر برای اقدام</param>
        /// <param name="documentType">نوع مستند مورد نظر برای اقدام</param>
        /// <returns></returns>
        public static StateOperation RejectReview(int userId, int documentId, string documentType)
        {
            return new StateOperation()
            {
                CreatedById = userId,
                TargetId = SystemRoles.LeadAccountant,
                Title = WorkItemTitle.ReviewDocument,
                DocumentId = documentId,
                DocumentType = documentType,
                Status = TransactionStatus.Unchecked,
                CurrentStatus = DocumentStatus.Reviewed,
                NewStatus = DocumentStatus.Prepared,
                Action = DocumentAction.Reject,
                NextAction = DocumentAction.Review
            };
        }

        /// <summary>
        /// تابع سازنده برای ایجاد یک اقدام جدید از نوع تایید
        /// </summary>
        /// <param name="userId">شناسه دیتابیسی کاربر تایید کننده</param>
        /// <param name="documentId">شناسه دیتابیسی مستند مورد نظر برای تایید</param>
        /// <param name="documentType">نوع مستند مورد نظر برای تایید</param>
        /// <returns></returns>
        public static StateOperation Confirm(int userId, int documentId, string documentType)
        {
            return new StateOperation()
            {
                CreatedById = userId,
                TargetId = SystemRoles.CFO,
                Title = WorkItemTitle.ApproveDocument,
                DocumentId = documentId,
                DocumentType = documentType,
                Status = TransactionStatus.NormalCheck,
                CurrentStatus = DocumentStatus.Reviewed,
                NewStatus = DocumentStatus.Confirmed,
                Action = DocumentAction.Confirm,
                NextAction = DocumentAction.Approve
            };
        }

        /// <summary>
        /// تابع سازنده برای ایجاد یک اقدام جدید از نوع تصویب
        /// </summary>
        /// <param name="userId">شناسه دیتابیسی کاربر تصویب کننده</param>
        /// <param name="documentId">شناسه دیتابیسی مستند مورد نظر برای تصویب</param>
        /// <param name="documentType">نوع مستند مورد نظر برای تصویب</param>
        /// <returns></returns>
        public static StateOperation Approve(int userId, int documentId, string documentType)
        {
            return new StateOperation()
            {
                CreatedById = userId,
                TargetId = 0,
                Title = WorkItemTitle.DocumentApproved,
                DocumentId = documentId,
                DocumentType = documentType,
                Status = TransactionStatus.FinalCheck,
                CurrentStatus = DocumentStatus.Confirmed,
                NewStatus = DocumentStatus.Approved,
                Action = DocumentAction.Approve,
                NextAction = String.Empty
            };
        }
    }
}
