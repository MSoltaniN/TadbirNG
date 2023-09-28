using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SPPC.Framework.Domain;

namespace SPPC.Tadbir.Model
{
    /// <summary>
    /// مشخصات مورد نیاز برای یک موجودیت کلی با قابلیت ذخیره، بازیابی و ردگیری در دیتابیس را نگهداری می کند
    /// </summary>
    public class PCoreEntity : IEntity
    {
        /// <summary>
        /// شناسه دیتابیسی این موجودیت که به صورت خودکار توسط دیتابیس تولید می شود
        /// </summary>
        [Key]
        public int Id { get; set; } = 0;

        /// <summary>
        /// شناسه یکتای ردیف دیتابیسی که به صورت خودکار توسط دیتابیس مقداردهی می شود
        /// </summary>
        [Column("rowguid")]
        public Guid RowGuid { get; set; } = System.Guid.NewGuid();

        /// <summary>
        /// تاریخ آخرین تغییر رکورد دیتابیس که به صورت خودکار توسط ابزار دسترسی به داده مقداردهی می شود
        /// </summary>
        public DateTime ModifiedDate { get; set; } = DateTime.Now;
    }
}
