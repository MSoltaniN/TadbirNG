﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SPPC.Tadbir.Model.Finance
{
    public partial class DetailAccount
    {
        /// <summary>
        /// مجموعه ای از تفصیلی های شناور زیرشاخه (فرزند) در ساختار درختی
        /// </summary>
        public virtual IList<DetailAccount> Children { get; protected set; }

        /// <summary>
        /// شناسه دیتابیسی ارزپایه یا پیش فرض این تفصیلی شناور
        /// </summary>
        public virtual int? CurrencyId { get; set; }
    }
}
