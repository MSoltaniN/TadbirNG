using System;
using System.Collections.Generic;
using SPPC.Tadbir.Metadata.Workflow;

namespace SPPC.Tadbir.Repository
{
    /// <summary>
    /// عملیات مورد نیاز برای خواندن اطلاعات فراداده ای از محل ذخیره را تعریف می کند
    /// </summary>
    public interface IMetadataRepository
    {
        /// <summary>
        /// اطلاعات فراداده ای مربوط به گردش های کاری تغییر وضعیت را خوانده و برمی گرداند
        /// </summary>
        /// <param name="documentType">نوع مستند اداری</param>
        /// <returns>اطلاعات فراداده ای گردش کار تغییر وضعیت</returns>
        StateWorkflow GetStateWorkflow(string documentType);
    }
}
