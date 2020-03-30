using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Metadata
{
    /// <summary>
    /// اطلاعات کلی مربوط به موجودیت ها یا فرم های برنامه را نگهداری می کند
    /// </summary>
    public class SourceEntityViewModel
    {
        /// <summary>
        /// شناسه دیتابیسی موجودیت - می تواند بدون مقدار باشد
        /// </summary>
        public int? EntityTypeId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی فرم عملیاتی - می تواند بدون مقدار باشد
        /// </summary>
        public int? SourceId { get; set; }

        /// <summary>
        /// نام موجودیت یا فرم عملیاتی به صورت محلی شده
        /// </summary>
        public string Name { get; set; }
    }
}
