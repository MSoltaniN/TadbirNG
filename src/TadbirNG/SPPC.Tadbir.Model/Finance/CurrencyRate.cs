﻿using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Model.Finance
{
    public partial class CurrencyRate
    {
        /// <summary>
        /// شناسه دیتابیسی ارز مرتبط با این نرخ
        /// </summary>
        public virtual int CurrencyId { get; set; }
    }
}
