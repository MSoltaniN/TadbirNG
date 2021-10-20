using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت اطلاعات اسناد پیش نویس و آرتیکل های آنها را تعریف می کند
    /// </summary>
    public interface IDraftVoucherRepository : IVoucherRepository
    {
        /// <summary>
        /// به روش آسنکرون، نوع مفهومی سند مالی را به سند عادی تغییر می دهد
        /// </summary>
        /// <param name="voucherId">شناسه دیتابیسی یکی از اسناد مالی موجود</param>
        Task NormalizeVoucherAsync(int voucherId);

        /// <summary>
        /// به روش آسنکرون، نوع مفهومی اسناد پیش نویس را به سند عادی تغییر می دهد
        /// </summary>
        /// <param name="items">مجموعه شناسه های دیتابیسی سطرهای مورد نظر برای تغییر نوع</param>
        Task NormalizeVouchersAsync(IEnumerable<int> items);
    }
}
