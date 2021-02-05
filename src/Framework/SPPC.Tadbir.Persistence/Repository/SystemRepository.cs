using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// امکانات عمومی و پرکاربرد سیستمی را به صورت یکجا در اختیار کلاس های لایه دیتابیس قرار می دهد
    /// </summary>
    public class SystemRepository : ISystemRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="repository">امکان خواندن اطلاعات را با توجه به دسترسی های سطری و شعب فراهم می کند</param>
        /// <param name="metadata">امکان خواندن اطلاعات فراداده ای برنامه را فراهم می کند</param>
        /// <param name="config">امکان خواندن اطلاعات پیکربندی برنامه را فراهم می کند</param>
        /// <param name="logger">امکان خواندن و ایجاد لاگ های عملیاتی را فراهم می کند</param>
        public SystemRepository(ISecureRepository repository, IMetadataRepository metadata, IConfigRepository config,
            IOperationLogRepository logger)
        {
            Repository = repository;
            Metadata = metadata;
            Config = config;
            Logger = logger;
        }

        /// <summary>
        /// امکان خواندن اطلاعات را با توجه به دسترسی های سطری و شعب فراهم می کند
        /// </summary>
        public ISecureRepository Repository { get; }

        /// <summary>
        /// امکان خواندن اطلاعات فراداده ای برنامه را فراهم می کند
        /// </summary>
        public IMetadataRepository Metadata { get; }

        /// <summary>
        /// امکان خواندن اطلاعات پیکربندی برنامه را فراهم می کند
        /// </summary>
        public IConfigRepository Config { get; }

        /// <summary>
        /// امکان خواندن و ایجاد لاگ های عملیاتی را فراهم می کند
        /// </summary>
        public IOperationLogRepository Logger { get; }
    }
}
