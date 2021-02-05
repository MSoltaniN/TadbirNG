using System;
using System.Collections.Generic;
using SPPC.Tadbir.Persistence.Utility;
using SPPC.Tadbir.ViewModel.Finance;

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
        /// <param name="relations">امکان مدیریت ارتباطات بردار حساب را فراهم می کند</param>
        /// <param name="cache">امکان مدیریت اطلاعات آرتیکل های سند را در حافظه کش فراهم می کند</param>
        public DraftVoucherLineRepository(IRepositoryContext context, ISystemRepository system,
            IRelationRepository relations, ICacheUtility<VoucherLineDetailViewModel> cache)
            : base(context, system, relations, cache)
        {
        }

        internal override int? EntityType
        {
            get { return (int?)EntityTypeId.DraftVoucher; }
        }
    }
}
