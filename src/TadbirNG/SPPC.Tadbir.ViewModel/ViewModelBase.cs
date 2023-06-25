using System;
using SPPC.Framework.Domain;

namespace SPPC.Tadbir.ViewModel
{
    /// <summary>
    /// اطلاعات نمایشی مشترک بین همه مدل های نمایشی را تعریف می کند
    /// </summary>
    public class ViewModelBase : IEntity
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public ViewModelBase()
        {
            RowGuid = Guid.NewGuid();
            ModifiedDate = DateTime.Now;
        }

        /// <summary>
        /// شماره ردیف سطر اطلاعاتی در لیست اطلاعاتی
        /// </summary>
        public int RowNo { get; set; }

        /// <summary>
        /// شناسه یکتای سطر اطلاعاتی
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// شناسه یکتای سراسری برای سطر اطلاعاتی
        /// </summary>
        public Guid RowGuid { get; set; }

        /// <summary>
        /// تاریخ آخرین تغییر سطر اطلاعاتی
        /// </summary>
        public DateTime ModifiedDate { get; set; }
    }
}
