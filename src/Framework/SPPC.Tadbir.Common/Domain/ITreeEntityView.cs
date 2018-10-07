using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Domain
{
    /// <summary>
    /// مشخصات مورد نیاز برای یک نمای اطلاعاتی با ساختار درختی را تعریف می کند
    /// </summary>
    public interface ITreeEntityView
    {
        /// <summary>
        /// شناسه دیتابیسی رکورد والد (پدر) این رکورد در ساختار درختی
        /// </summary>
        int? ParentId { get; set; }

        /// <summary>
        /// کد شناسایی جزئی این رکورد در ساختار درختی
        /// </summary>
        string Code { get; set; }

        /// <summary>
        /// کد شناسایی کامل این رکورد تا سطح جاری
        /// </summary>
        string FullCode { get; set; }

        /// <summary>
        /// شماره سطح این رکورد در ساختار درختی
        /// </summary>
        short Level { get; set; }
    }
}
