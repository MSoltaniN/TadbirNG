using System;
using System.Collections.Generic;
using System.Text;

namespace SPPC.Tadbir.ViewModel.Finance
{
    /// <summary>
    /// اطلاعات مورد نیاز برای گزارش نتیجه عملیات گروهی را نگهداری می کند
    /// </summary>
    public class GroupActionResultViewModel
    {
        /// <summary>
        /// شناسه موجودیت
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// شماره موجودیت عملیاتی
        /// </summary>
        public int? No { get; set; }

        /// <summary>
        /// نام موجودیت
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// کد کامل موجودیت
        /// </summary>
        public string FullCode { get; set; }

        /// <summary>
        /// پیام خطای عملیات
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// تاریخ موجودیت عملیاتی
        /// </summary>
        public DateTime? Date { get; set; }
    }
}
