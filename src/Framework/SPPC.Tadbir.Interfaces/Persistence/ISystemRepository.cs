using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// امکانات دیتابیس های سیستمی را به صورت یکجا در اختیار کلاس های لایه دیتابیس قرار می دهد
    /// </summary>
    public interface ISystemRepository
    {
        /// <summary>
        /// امکان خواندن اطلاعات را با توجه به دسترسی های سطری و شعب فراهم می کند
        /// </summary>
        ISecureRepository Repository { get; }

        /// <summary>
        /// امکان خواندن اطلاعات فراداده ای برنامه را فراهم می کند
        /// </summary>
        IMetadataRepository Metadata { get; }

        /// <summary>
        /// امکان خواندن اطلاعات پیکربندی برنامه را فراهم می کند
        /// </summary>
        IConfigRepository Config { get; }

        /// <summary>
        /// امکان انجام محاسبات مشترک در گزارشات برنامه را فراهم می کند
        /// </summary>
        IReportRepository Report { get; }

        /// <summary>
        /// امکان خواندن و ایجاد لاگ های عملیاتی را فراهم می کند
        /// </summary>
        IOperationLogRepository Logger { get; }
    }
}
