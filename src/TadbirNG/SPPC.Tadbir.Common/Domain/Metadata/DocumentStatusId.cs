﻿using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Domain
{
    /// <summary>
    /// مقادیر سیستمی را برای وضعیت ثبتی یک مستند اداری تعریف می کند
    /// </summary>
    public enum DocumentStatusId
    {
        /// <summary>
        /// وضعیت نامشخص برای مستند اداری
        /// </summary>
        Unspecified = 0,

        /// <summary>
        /// وضعیت ثبت نشده برای مستند اداری
        /// </summary>
        NotChecked = 1,

        /// <summary>
        /// وضعیت ثبت عادی برای مستند اداری
        /// </summary>
        Checked = 2,

        /// <summary>
        /// وضعیت ثبت قطعی برای مستند اداری
        /// </summary>
        Finalized = 3
    }
}
