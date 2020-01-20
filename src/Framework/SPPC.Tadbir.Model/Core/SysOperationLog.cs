using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Model.Core
{
    public partial class SysOperationLog
    {
        /// <summary>
        /// شناسه دیتابیسی عملیات انجام شده
        /// </summary>
        public int OperationId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی نوع موجودیتی که عملیات روی آن انجام شده است
        /// </summary>
        public int? EntityTypeId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی فرم عملیاتی به کار رفته
        /// </summary>
        public int? SourceId { get; set; }

        /// <summary>
        /// شناسه نمای اطلاعاتی لیستی به کار رفته حین انجام عملیات
        /// </summary>
        public int? SourceListId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی شرکتی که هنگام انجام عملیات، کاربر وارد آن شده بوده است
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی کاربر انجام دهنده عملیات
        /// </summary>
        public int UserId { get; set; }
    }
}
