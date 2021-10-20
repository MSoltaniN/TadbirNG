﻿using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Domain
{
    /// <summary>
    /// داده شمارشی برای مشخص کردن سیستم ثبت موجودی
    /// </summary>
    public enum InventoryMode
    {
        /// <summary>
        /// سیستم ثبت ادواری
        /// </summary>
        Periodic = 0,

        /// <summary>
        /// سیستم ثبت دائمی
        /// </summary>
        Perpetual = 1
    }
}
