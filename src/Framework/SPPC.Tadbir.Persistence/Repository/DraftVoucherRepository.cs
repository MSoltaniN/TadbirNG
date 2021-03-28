using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model.Finance;
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
        /// <param name="report"></param>
        public DraftVoucherRepository(IRepositoryContext context, ISystemRepository system,
            IUserRepository userRepository, IAccountCollectionUtility utility,
            IReportDirectUtility report)
            : base(context, system, userRepository, utility, report)
        {
        }

        /// <summary>
        /// به روش آسنکرون، نوع مفهومی سند مالی را به سند عادی تغییر می دهد
        /// </summary>
        /// <param name="voucherId">شناسه دیتابیسی یکی از اسناد مالی موجود</param>
        public async Task NormalizeVoucherAsync(int voucherId)
        {
            var repository = UnitOfWork.GetAsyncRepository<Voucher>();
            var voucher = await repository.GetByIDAsync(voucherId);
            if (voucher != null && voucher.SubjectType == (short)SubjectType.Draft)
            {
                int lastNo = await GetLastVoucherNoAsync();
                voucher.SubjectType = (short)SubjectType.Normal;
                voucher.No = lastNo + 1;
                repository.Update(voucher);
                OnEntityAction(OperationId.Normalize);
                await FinalizeActionAsync(voucher);
            }
        }

        /// <summary>
        /// به روش آسنکرون، نوع مفهومی اسناد پیش نویس را به سند عادی تغییر می دهد
        /// </summary>
        /// <param name="items">مجموعه شناسه های دیتابیسی سطرهای مورد نظر برای تغییر نوع</param>
        public async Task NormalizeVouchersAsync(IEnumerable<int> items)
        {
            var repository = UnitOfWork.GetAsyncRepository<Voucher>();
            int lastNo = await GetLastVoucherNoAsync();
            int currentNo = lastNo + 1;
            foreach (int item in items)
            {
                var voucher = await repository.GetByIDAsync(item);
                if (voucher != null && voucher.SubjectType == (short)SubjectType.Draft)
                {
                    voucher.SubjectType = (short)SubjectType.Normal;
                    voucher.No = currentNo;
                    repository.Update(voucher);
                    currentNo++;
                }
            }

            await UnitOfWork.CommitAsync();
            await OnEntityGroupChangeAsync(items, OperationId.GroupNormalize);
        }

        internal override int? EntityType
        {
            get { return (int?)EntityTypeId.DraftVoucher; }
        }
    }
}
