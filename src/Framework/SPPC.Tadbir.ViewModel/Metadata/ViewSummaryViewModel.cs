using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Metadata
{
    /// <summary>
    /// اطلاعات خلاصه را برای یکی از نماهای اطلاعاتی نگهداری می کند
    /// </summary>
    public class ViewSummaryViewModel
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public ViewSummaryViewModel()
        {
        }

        /// <summary>
        /// شناسه دیتابیسی نمای اطلاعاتی
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// نام محلی شده نمای اطلاعاتی
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// آدرس وب برای خواندن رکوردهای موجود با فیلتر سطر و شعبه
        /// </summary>
        public string SearchUrl { get; set; }
    }
}
