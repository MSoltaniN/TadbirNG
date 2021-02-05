using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Persistence.Utility
{
    /// <summary>
    /// انواع عملیات به روزرسانی حافظه کش را تعریف می کند
    /// </summary>
    public enum CacheOperation
    {
        /// <summary>
        /// اضافه کردن یک رکورد موجودیت به حافظه کش
        /// </summary>
        Add = 1,

        /// <summary>
        /// به روزرسانی یک رکورد موجودیت در حافظه کش
        /// </summary>
        Update = 2,

        /// <summary>
        /// حذف یک رکورد موجودیت از حافظه کش
        /// </summary>
        Delete = 3
    }
}
