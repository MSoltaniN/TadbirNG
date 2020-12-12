using System;
using System.Collections.Generic;
using SPPC.Tadbir.Persistence.Utility;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت اطلاعات اسناد پیش نویس و آرتیکل های آنها را پیاده سازی می کند
    /// </summary>
    public class DraftVoucherRepository : VoucherRepository, IDraftVoucherRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="system">امکانات مورد نیاز در دیتابیس های سیستمی را فراهم می کند</param>
        /// <param name="userRepository">امکان خواندن اطلاعات کاربران برنامه را فراهم می کند</param>
        /// <param name="utility">امکانات تکمیلی برای کار با مجموعه های حساب را پیاده سازی می کند</param>
        public DraftVoucherRepository(IRepositoryContext context, ISystemRepository system,
            IUserRepository userRepository, IAccountCollectionUtility utility)
            : base(context, system, userRepository, utility)
        {
        }

        internal override int? EntityType
        {
            get { return (int?)EntityTypeId.DraftVoucher; }
        }
    }
}
