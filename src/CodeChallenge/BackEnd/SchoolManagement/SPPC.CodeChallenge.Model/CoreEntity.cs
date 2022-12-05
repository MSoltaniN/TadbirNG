using System;

namespace SPPC.CodeChallenge.Model
{
    /// <summary>
    /// مشخصات مورد نیاز برای یک موجودیت کلی با قابلیت ذخیره، بازیابی و ردگیری در دیتابیس را نگهداری می کند
    /// </summary>
    public class CoreEntity
    {
        /// <summary>
        /// شناسه دیتابیسی این موجودیت که به صورت خودکار توسط دیتابیس تولید می شود
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// شناسه یکتای ردیف دیتابیسی که به صورت خودکار توسط دیتابیس مقداردهی می شود
        /// </summary>
        public Guid RowGuid { get; set; }

        /// <summary>
        /// تاریخ آخرین تغییر رکورد دیتابیس که به صورت خودکار توسط ابزار دسترسی به داده مقداردهی می شود
        /// </summary>
        public DateTime ModifiedDate { get; set; }
    }
}
