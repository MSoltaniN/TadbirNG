using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت اطلاعات آرتیکل های پیش نویس را پیاده سازی می کند
    /// </summary>
    public class DraftVoucherLineRepository : VoucherLineRepository, IDraftVoucherLineRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="system">امکانات مورد نیاز در دیتابیس های سیستمی را فراهم می کند</param>
        public DraftVoucherLineRepository(IRepositoryContext context, ISystemRepository system)
            : base(context, system)
        {
        }

        internal override int? EntityType
        {
            get { return (int?)EntityTypeId.DraftVoucher; }
        }
    }
}
