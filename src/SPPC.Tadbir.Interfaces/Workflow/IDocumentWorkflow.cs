using System;
using System.Collections.Generic;
using SPPC.Tadbir.Service;

namespace SPPC.Tadbir.Workflow
{
    /// <summary>
    /// عملیات مورد نیاز در گردش کار تصویب مستند اداری را تعریف می کند
    /// </summary>
    public interface IDocumentWorkflow
    {
        /// <summary>
        /// اطلاعات امنیتی کاربر جاری برنامه
        /// </summary>
        ISecurityContext CurrentContext { get; set; }

        /// <summary>
        /// با توجه به تنظیمات جاری سیستم، انجام یک اقدام روی یک مستند اداری را اعتبارسنجی می کند
        /// </summary>
        /// <param name="documentType">نوع مستند اداری</param>
        /// <param name="status">وضعیت فعلی مستند اداری هنگام اقدام</param>
        /// <param name="action">نوع اقدام</param>
        /// <returns>اگر اقدام مورد نظر روی مستند اداری مجاز باشد مقدار "درست" و در غیر این صورت
        /// مقدار "نادرست" برمی گرداند</returns>
        bool ValidateAction(string documentType, string status, string action);

        /// <summary>
        /// یک مستند اداری را از وضعیت فعلی به وضعیت تنظیم شده می برد
        /// </summary>
        /// <param name="entityId">شناسه موجودیت مرتبط با مستند اداری</param>
        /// <param name="documentId">شناسه مستند اداری</param>
        /// <param name="documentType">نوع مستند اداری</param>
        /// <param name="paraph">پاراف متنی اختیاری برای گیرنده کار</param>
        void Prepare(int entityId, int documentId, string documentType, string paraph);

        /// <summary>
        /// یک مستند اداری را از وضعیت فعلی به وضعیت بررسی شده می برد
        /// </summary>
        /// <param name="entityId">شناسه موجودیت مرتبط با مستند اداری</param>
        /// <param name="documentId">شناسه مستند اداری</param>
        /// <param name="documentType">نوع مستند اداری</param>
        /// <param name="paraph">پاراف متنی اختیاری برای گیرنده کار</param>
        void Review(int entityId, int documentId, string documentType, string paraph);

        /// <summary>
        /// یک مستند اداری را از وضعیت فعلی به وضعیت رد شده می برد
        /// </summary>
        /// <param name="entityId">شناسه موجودیت مرتبط با مستند اداری</param>
        /// <param name="documentId">شناسه مستند اداری</param>
        /// <param name="documentType">نوع مستند اداری</param>
        /// <param name="paraph">پاراف متنی اختیاری برای گیرنده کار</param>
        void Reject(int entityId, int documentId, string documentType, string paraph);

        /// <summary>
        /// یک مستند اداری را از وضعیت فعلی به وضعیت تایید شده می برد
        /// </summary>
        /// <param name="entityId">شناسه موجودیت مرتبط با مستند اداری</param>
        /// <param name="documentId">شناسه مستند اداری</param>
        /// <param name="documentType">نوع مستند اداری</param>
        /// <param name="paraph">پاراف متنی اختیاری برای گیرنده کار</param>
        void Confirm(int entityId, int documentId, string documentType, string paraph);

        /// <summary>
        /// یک مستند اداری را از وضعیت فعلی به وضعیت تصویب شده می برد
        /// </summary>
        /// <param name="entityId">شناسه موجودیت مرتبط با مستند اداری</param>
        /// <param name="documentId">شناسه مستند اداری</param>
        /// <param name="documentType">نوع مستند اداری</param>
        /// <param name="paraph">پاراف متنی اختیاری برای گیرنده کار</param>
        void Approve(int entityId, int documentId, string documentType, string paraph);
    }
}
