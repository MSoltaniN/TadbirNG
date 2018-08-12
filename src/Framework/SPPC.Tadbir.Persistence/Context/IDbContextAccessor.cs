using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// امکان اتصال به دیتابیس های قابل دستیابی در برنامه تدبیر را فراهم می کند
    /// </summary>
    public interface IDbContextAccessor
    {
        /// <summary>
        /// دیتابیس شرکت جاری در برنامه
        /// </summary>
        TadbirContext UserContext { get; }

        /// <summary>
        /// دیتابیس سیستمی برنامه
        /// </summary>
        SystemContext SystemContext { get; }
    }
}
