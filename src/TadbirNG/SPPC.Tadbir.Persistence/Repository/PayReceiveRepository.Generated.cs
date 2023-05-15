using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Common;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model.CashFlow;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Utility;
using SPPC.Tadbir.ViewModel.CashFlow;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت دریافت ها و پرداخت ها را پیاده سازی می کند
    /// </summary>
    public class PayReceiveRepository : EntityLoggingRepository<PayReceive, PayReceiveViewModel>, IPayReceiveRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="system">امکانات مورد نیاز در دیتابیس های سیستمی را فراهم می کند</param>
        /// <param name="userRepository">امکان خواندن اطلاعات کاربران برنامه را فراهم می کند</param>
        public PayReceiveRepository(IRepositoryContext context, ISystemRepository system,
            IUserRepository userRepository)
            : base(context, system.Logger)
        {
            _system = system;
            _userRepository = userRepository;
        }

        /// <summary>
        /// به روش آسنکرون، دریافت و پرداخت با شناسه عددی مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="payReceiveId">شناسه عددی یکی از دریافت ها و پرداخت ها موجود</param>
        /// <returns>دریافت و پرداخت مشخص شده با شناسه عددی</returns>
        public async Task<PayReceiveViewModel> GetPayReceiveAsync(int payReceiveId)
        {
            PayReceiveViewModel item = null;
            var repository = UnitOfWork.GetAsyncRepository<PayReceive>();
            var payReceive = await repository.GetByIDAsync(payReceiveId);
            if (payReceive != null)
            {
                item = Mapper.Map<PayReceiveViewModel>(payReceive);
            }

            return item;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات یک دریافت و پرداخت را ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="payReceive">دریافت و پرداخت مورد نظر برای ایجاد یا اصلاح</param>
        /// <returns>اطلاعات نمایشی دریافت و پرداخت ایجاد یا اصلاح شده</returns>
        public async Task<PayReceiveViewModel> SavePayReceiveAsync(PayReceiveViewModel payReceive)
        {
            Verify.ArgumentNotNull(payReceive, nameof(payReceive));
            var currPersonName = await _userRepository.GetCurrentUserDisplayNameAsync();
            int entityTypeId = (int)(payReceive.Type == (int)PayReceiveType.Receival
                ? EntityTypeId.Receival
                : EntityTypeId.Payment);

            PayReceive payReceiveModel;
            var repository = UnitOfWork.GetAsyncRepository<PayReceive>();
            if (payReceive.Id == 0)
            {
                payReceiveModel = Mapper.Map<PayReceive>(payReceive);
                payReceiveModel.IssuedById = UserContext.Id;
                payReceiveModel.ModifiedById = UserContext.Id;
                payReceiveModel.IssuedByName =
                    payReceiveModel.ModifiedByName = currPersonName;
                payReceiveModel.CreatedDate = DateTime.Now;
                await InsertAsync(repository, payReceiveModel, OperationId.Create, entityTypeId);
            }
            else
            {
                payReceiveModel = await repository.GetByIDAsync(payReceive.Id);
                if (payReceiveModel != null)
                {
                    payReceiveModel.ModifiedById = UserContext.Id;
                    payReceiveModel.ModifiedByName = currPersonName;
                    await UpdateAsync(repository, payReceiveModel, payReceive, OperationId.Edit, entityTypeId);
                }
            }

            return Mapper.Map<PayReceiveViewModel>(payReceiveModel);
        }

        /// <summary>
        /// به روش آسنکرون، دریافت و پرداخت مشخص شده با شناسه عددی را حذف می کند
        /// </summary>
        /// <param name="payReceiveId">شناسه عددی دریافت و پرداخت مورد نظر برای حذف</param>
        /// <param name="type">نوع فرم دریافت/پرداخت</param>
        public async Task DeletePayReceiveAsync(int payReceiveId, int type)
        {
            var repository = UnitOfWork.GetAsyncRepository<PayReceive>();
            var payReceive = await repository.GetByIDAsync(payReceiveId);
            if (payReceive != null)
            {
                int entityTypeId = (int)(type == (int)PayReceiveType.Receival
                    ? EntityTypeId.Receival
                    : EntityTypeId.Payment);
                await DeleteAsync(repository, payReceive, OperationId.Delete, entityTypeId);
            }
        }

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا شماره فرم دریافت/پرداخت مورد نظر تکراری است یا نه
        /// </summary>
        /// <param name="payReceive">اطلاعات نمایشی فرم دریافت/پرداخت مورد نظر</param>
        /// <returns>در صورت تکراری بودن شماره فرم دریافت/پرداخت مقدار درست و
        /// در غیر اینصورت نادرست برمی گرداند</returns>
        public async Task<bool> IsDuplicatePayReceiveNo(PayReceiveViewModel payReceive)
        {
            var repository = UnitOfWork.GetAsyncRepository<PayReceive>();
            return await repository
                .GetEntityQuery()
                .AnyAsync(pr => payReceive.Id != pr.Id 
                && payReceive.PayReceiveNo == pr.PayReceiveNo
                && payReceive.Type == pr.Type
                && payReceive.FiscalPeriodId == pr.FiscalPeriodId
                && payReceive.BranchId == pr.BranchId);
        }

        /// <summary>
        /// به روش آسنکرون، وضعیت تایید فرم دریافت/پرداخت مشخص شده را تغییر می دهد
        /// </summary>
        /// <param name="payReceiveId">شناسه دیتابیسی فرم دریافت/پرداخت مورد نظر</param>
        /// <param name="isConfirmed"> در صورت تایید فرم دریافت/پرداخت با مقدار درست 
        /// و در غیر این صورت با مقدار نادرست پر می شود.</param>
        public async Task SetPayReceiveConfirmationAsync(int payReceiveId, bool isConfirmed)
        {
            var repository = UnitOfWork.GetAsyncRepository<PayReceive>();
            var payReceive= await repository.GetByIDAsync(payReceiveId);
            if (payReceive != null) 
            {
                payReceive.ConfirmedById = isConfirmed ? UserContext.Id : null;
                payReceive.ConfirmedByName = isConfirmed ? GetCurrentUserFullName() : null;
                repository.Update(payReceive);
                int entityTypeId = (int)(payReceive.Type == (int)PayReceiveType.Receival
                ? EntityTypeId.Receival
                : EntityTypeId.Payment);
                OnDocumentConfirmation(isConfirmed, entityTypeId);
                await FinalizeActionAsync(payReceive);
            }
        }

        internal override int? EntityType
        {
            get { return (int?)EntityTypeId.Payment; }
        }

        /// <summary>
        /// آخرین تغییرات موجودیت را از مدل نمایشی به سطر اطلاعاتی موجود کپی می کند
        /// </summary>
        /// <param name="payReceiveViewModel">مدل نمایشی شامل آخرین تغییرات</param>
        /// <param name="payReceive">سطر اطلاعاتی موجود</param>
        protected override void UpdateExisting(PayReceiveViewModel payReceiveViewModel, PayReceive payReceive)
        {
            payReceive.IssuedById = payReceiveViewModel.IssuedById;
            payReceive.ModifiedById = payReceiveViewModel.ModifiedById;
            payReceive.ConfirmedById = payReceiveViewModel.ConfirmedById;
            payReceive.ApprovedById = payReceiveViewModel.ApprovedById;
            payReceive.PayReceiveNo = payReceiveViewModel.PayReceiveNo;
            payReceive.Reference = payReceiveViewModel.Reference;
            payReceive.Date = payReceiveViewModel.Date;
            payReceive.CurrencyId = payReceiveViewModel.CurrencyId;
            payReceive.CurrencyRate = payReceiveViewModel.CurrencyRate;
            payReceive.Description = payReceiveViewModel.Description;
            payReceive.CreatedDate = payReceiveViewModel.CreatedDate;
            payReceive.IssuedByName = payReceiveViewModel.IssuedByName;
            payReceive.ModifiedByName = payReceiveViewModel.ModifiedByName;
            payReceive.ConfirmedByName = payReceiveViewModel.ConfirmedByName;
            payReceive.ApprovedByName = payReceiveViewModel.ApprovedByName;
        }

        /// <summary>
        /// اطلاعات خلاصه سطر اطلاعاتی داده شده را به صورت یک رشته متنی برمی گرداند
        /// </summary>
        /// <param name="entity">یکی از سطرهای اطلاعاتی موجود</param>
        /// <returns>اطلاعات خلاصه سطر اطلاعاتی داده شده به صورت رشته متنی</returns>
        protected override string GetState(PayReceive entity)
        {
            return entity != null
                ? $"{AppStrings.PayReceiveNo} : {entity.PayReceiveNo}, " +
                $"{AppStrings.Reference} : {entity.Reference}, {AppStrings.Date} : {entity.Date}, " +
                $"{AppStrings.CurrencyRate} : {entity.CurrencyRate}, {AppStrings.Description} : {entity.Description}, "
                : String.Empty;
        }

        private ISecureRepository Repository
        {
            get { return _system.Repository; }
        }

        private readonly ISystemRepository _system;
        private readonly IUserRepository _userRepository;
    }
}
