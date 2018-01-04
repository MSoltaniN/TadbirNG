using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Workflow
{
    /// <summary>
    /// عملیات مرتبط با ردگیری  گردش های کاری را تعریف می کند.
    /// </summary>
    public interface IWorkflowTracker
    {
        /// <summary>
        /// ویرایش گردش کاری را برای گردش کار متناظر با یک مستند برمی گرداند
        /// </summary>
        /// <param name="documentId">شناسه دیتابیسی مستند موجود</param>
        /// <param name="documentType">نوع مستند مورد نظر</param>
        /// <returns></returns>
        string TrackDocumentWorkflowEdition(int documentId, string documentType);
    }
}
