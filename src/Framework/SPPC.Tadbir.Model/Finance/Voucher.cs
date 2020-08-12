﻿using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Model.Finance
{
    public partial class Voucher
    {
        /// <summary>
        /// شناسه دیتابیسی وضعیت ثبتی این سند مالی
        /// </summary>
        public virtual int StatusId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی مستند اداری مرتبط با این سند مالی
        /// </summary>
        public virtual int? DocumentId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی مأخذ این سند مالی
        /// </summary>
        public virtual int VoucherOriginId { get; set; }
    }
}