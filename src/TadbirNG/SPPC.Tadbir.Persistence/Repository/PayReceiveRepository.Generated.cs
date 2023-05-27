using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Common;
using SPPC.Framework.Extensions;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model.CashFlow;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Resources;
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
        public PayReceiveRepository(IRepositoryContext context, ISystemRepository system)
            : base(context, system.Logger)
        {
            _system = system;
        }

        /// <summary>
        /// به روش آسنکرون، فرم دریافت/پرداخت با شناسه عددی مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="payReceiveId">شناسه عددی یکی از فرم های دریافت یا پرداخت موجود</param>
        /// <param name="type">مشخص می کند که درخواست جاری از نوع پرداختی یا دریافتی می باشد</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای دریافت اطلاعات لازم از سمت وب</param>
        /// <returns>فرم دریافت/پرداخت مشخص شده با شناسه عددی</returns>
        public async Task<PayReceiveViewModel> GetPayReceiveAsync(int payReceiveId, int type = 0,
            GridOptions gridOptions = null)
        {
            PayReceiveViewModel item = new PayReceiveViewModel();
            var options = gridOptions ?? new GridOptions();
            if (options.Operation != (int)OperationId.Print &&
                options.Operation != (int)OperationId.PrintPreview)
            {
                var repository = UnitOfWork.GetAsyncRepository<PayReceive>();
                var payReceive = await repository.GetByIDAsync(payReceiveId);
                if (payReceive != null)
                {
                    item = Mapper.Map<PayReceiveViewModel>(payReceive);
                }
            }
            else
            {
                int entityTypeId = GetEntityTypeId(type);
                await ReadAsync(options, null, entityTypeId);
            }

            return item;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات یک فرم دریافت/پرداخت را ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="payReceive">فرم دریافت/پرداخت مورد نظر برای ایجاد یا اصلاح</param>
        /// <returns>اطلاعات نمایشی فرم دریافت/پرداخت ایجاد یا اصلاح شده</returns>
        public async Task<PayReceiveViewModel> SavePayReceiveAsync(PayReceiveViewModel payReceive)
        {
            Verify.ArgumentNotNull(payReceive, nameof(payReceive));
            int entityTypeId = GetEntityTypeId(payReceive.Type);
            string personName = GetCurrentUserFullName(); ;
            PayReceive payReceiveModel;
            var repository = UnitOfWork.GetAsyncRepository<PayReceive>();
            if (payReceive.Id == 0)
            {
                payReceiveModel = Mapper.Map<PayReceive>(payReceive);
                payReceiveModel.IssuedById = UserContext.Id;
                payReceiveModel.ModifiedById = UserContext.Id;
                payReceiveModel.IssuedByName =
                    payReceiveModel.ModifiedByName = personName;
                payReceiveModel.CreatedDate = DateTime.Now;
                await InsertAsync(repository, payReceiveModel, OperationId.Create, entityTypeId);
            }
            else
            {
                payReceiveModel = await repository.GetByIDAsync(payReceive.Id);
                if (payReceiveModel != null)
                {
                    payReceiveModel.ModifiedById = UserContext.Id;
                    payReceiveModel.ModifiedByName = personName;
                    await UpdateAsync(repository, payReceiveModel, payReceive, OperationId.Edit, entityTypeId);
                }
            }

            return Mapper.Map<PayReceiveViewModel>(payReceiveModel);
        }

        /// <summary>
        /// به روش آسنکرون، فرم دریافت/پرداخت مشخص شده با شناسه عددی را حذف می کند
        /// </summary>
        /// <param name="payReceiveId">شناسه عددی فرم دریافت/پرداخت مورد نظر برای حذف</param>
        /// <param name="type">مشخص می کند که درخواست جاری از نوع پرداختی یا دریافتی می باشد</param>
        public async Task DeletePayReceiveAsync(int payReceiveId, int type)
        {
            var repository = UnitOfWork.GetAsyncRepository<PayReceive>();
            var payReceive = await repository.GetByIDAsync(payReceiveId);
            if (payReceive != null)
            {
                int entityTypeId = GetEntityTypeId(type);
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
        /// و در غیر این صورت با مقدار نادرست پر می شود</param>
        public async Task SetPayReceiveConfirmationAsync(int payReceiveId, bool isConfirmed)
        {
            var repository = UnitOfWork.GetAsyncRepository<PayReceive>();
            var payReceive = await repository.GetByIDAsync(payReceiveId);
            if (payReceive != null)
            {
                payReceive.ConfirmedById = isConfirmed ? UserContext.Id : null;
                payReceive.ConfirmedByName = isConfirmed ? GetCurrentUserFullName() : null;
                repository.Update(payReceive);
                int entityTypeId = GetEntityTypeId(payReceive.Type);
                OnDocumentConfirmation(isConfirmed, entityTypeId);
                await FinalizeActionAsync(payReceive);
            }
        }

        /// <summary>
        /// به روش آسنکرون، وضعیت تصویب فرم دریافت/پرداخت مشخص شده را تغییر می دهد
        /// </summary>
        /// <param name="payReceiveId">شناسه دیتابیسی فرم دریافت/پرداخت مورد نظر</param>
        /// <param name="isApproved"> در صورت تصویب فرم دریافت/پرداخت با مقدار درست 
        /// و در غیر این صورت با مقدار نادرست پر می شود</param>
        public async Task SetPayReceiveApprovalAsync(int payReceiveId, bool isApproved)
        {
            var repository = UnitOfWork.GetAsyncRepository<PayReceive>();
            var payReceive = await repository.GetByIDAsync(payReceiveId);
            if (payReceive != null)
            {
                payReceive.ApprovedById = isApproved ? UserContext.Id : null;
                payReceive.ApprovedByName = isApproved ? GetCurrentUserFullName() : null;
                repository.Update(payReceive);
                int entityTypeId = GetEntityTypeId(payReceive.Type);
                OnDocumentApproval(isApproved, entityTypeId);
                await FinalizeActionAsync(payReceive);
            }
        }

        /// <summary>
        /// به روش آسنکرون، فرم دریافت/پرداخت با شماره مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="payReceiveNo">شماره فرم دریافت/پرداخت مورد نظر</param>
        /// <param name="type">مشخص می کند که درخواست جاری از نوع پرداختی یا دریافتی می باشد</param>
        /// <returns>فرم دریافت/پرداخت مشخص شده با شماره</returns>
        public async Task<PayReceiveViewModel> GetPayReceiveNoAsync(string payReceiveNo, int type)
        {
            var byNo = default(PayReceiveViewModel);
            var viewId = GetViewId((int)type);
            var payReceiveByNo = await Repository.GetAllOperationQuery<PayReceive>(viewId)
                .Where(pr => pr.PayReceiveNo == payReceiveNo.Trim() && pr.Type == (int)type)
                .SingleOrDefaultAsync();

            if (payReceiveByNo != null)
            {
                byNo = Mapper.Map<PayReceiveViewModel>(payReceiveByNo);
            }

            return byNo;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات اولین فرم دریافت/پرداخت را از نوع مشخص شده خوانده و برمی گرداند
        /// </summary>
        /// <param name="type">مشخص می کند که درخواست جاری از نوع پرداختی یا دریافتی می باشد</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>اولین فرم دریافت/پرداخت</returns>
        public async Task<PayReceiveViewModel> GetFirstPayReceiveAsync(int type, GridOptions gridOptions = null)
        {
            var payReceives = await GetOrderedPayReceiveItemsAsync(type,
                pr => pr.Type == type, gridOptions);
            var first = payReceives.FirstOrDefault();
            if (first != null)
            {
                await SetPayReceiveNavigationAsync(first, gridOptions);
            }

            return first;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات آخرین فرم دریافت/پرداخت را از نوع مشخص شده خوانده و برمی گرداند
        /// </summary>
        /// <param name="type">مشخص می کند که درخواست جاری از نوع پرداختی یا دریافتی می باشد</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>آخرین فرم دریافت/پرداخت</returns>
        public async Task<PayReceiveViewModel> GetLastPayReceiveAsync(int type, GridOptions gridOptions = null)
        {
            var payReceives = await GetOrderedPayReceiveItemsAsync(type,
                pr => pr.Type == type, gridOptions);
            var last = payReceives.LastOrDefault();
            if (last != null)
            {
                await SetPayReceiveNavigationAsync(last, gridOptions);
            }

            return last;
        }
        /// <summary>
        /// به روش آسنکرون، اطلاعات فرم دریافت/پرداخت بعدی را از نوع مشخص شده خوانده و برمی گرداند
        /// </summary>
        /// <param name="type">مشخص می کند که درخواست جاری از نوع پرداختی یا دریافتی می باشد</param>
        /// <param name="currentNo">شماره فرم دریافت/پرداخت جاری در برنامه</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>فرم دریافت/پرداخت بعدی</returns>
        public async Task<PayReceiveViewModel> GetNextPayReceiveAsync(string currentNo, int type,
            GridOptions gridOptions = null)
        {
            var payReceives = await GetOrderedPayReceiveItemsAsync(type, pr =>
                String.Compare(pr.PayReceiveNo, currentNo) > 0 && pr.Type == type, gridOptions);
            var next = payReceives.FirstOrDefault();
            if (next != null)
            {
                await SetPayReceiveNavigationAsync(next, gridOptions);
            }

            return next;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات فرم دریافت/پرداخت قبلی را از نوع مشخص شده خوانده و برمی گرداند
        /// </summary>
        /// <param name="type">مشخص می کند که درخواست جاری از نوع پرداختی یا دریافتی می باشد</param>
        /// <param name="currentNo">شماره فرم دریافت/پرداخت جاری در برنامه</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>فرم دریافت/پرداخت قبلی</returns>
        public async Task<PayReceiveViewModel> GetPreviousPayReceiveAsync(string currentNo, int type,
            GridOptions gridOptions = null)
        {
            var payReceives = await GetOrderedPayReceiveItemsAsync(type, pr =>
                String.Compare(pr.PayReceiveNo, currentNo) < 0 && pr.Type == type, gridOptions);
            var previous = payReceives.LastOrDefault();
            if (previous != null)
            {
                await SetPayReceiveNavigationAsync(previous, gridOptions);
            }

            return previous;
        }

        /// <summary>
        /// به روش آسنکرون، نمونه ای جدید از فرم دریافت/پرداخت می سازد
        /// </summary>
        /// <param name="type">مشخص می کند که درخواست جاری از نوع پرداختی یا دریافتی می باشد</param>
        /// <returns>فرم دریافت/پرداخت جدید</returns>
        public async Task<PayReceiveViewModel> GetNewPayReceiveAsync(int type)
        {
            string personName = GetCurrentUserFullName();
            var newPayReceive = new PayReceiveViewModel()
            {
                CreatedDate = DateTime.Now,
                IssuedById = UserContext.Id,
                ModifiedById = UserContext.Id,
                IssuedByName = personName,
                ModifiedByName = personName,
                BranchId = UserContext.BranchId,
                FiscalPeriodId = UserContext.FiscalPeriodId,
                Date = await GetLastPayReceiveDateAsync(type),
                Type = (short)type
            };

            await SetPayReceiveNavigationAsync(newPayReceive);
            return newPayReceive;
        }

        private async Task<DateTime> GetLastPayReceiveDateAsync(int type)
        {
            var repository = UnitOfWork.GetAsyncRepository<PayReceive>();
            var lastByDate = await repository
                .GetEntityQuery()
                .Where(pr => pr.FiscalPeriodId == UserContext.FiscalPeriodId
                    && pr.BranchId == UserContext.BranchId
                    && pr.Type == type)
                .OrderByDescending(pr => pr.Date)
                .FirstOrDefaultAsync();
            if (lastByDate == null)
            {
                var periodRepository = UnitOfWork.GetAsyncRepository<FiscalPeriod>();
                var fiscalPeriod = await periodRepository.GetByIDAsync(UserContext.FiscalPeriodId);
                return fiscalPeriod != null
                    ? fiscalPeriod.StartDate
                    : DateTime.Now;
            }

            return lastByDate.Date;
        }

        private async Task<IEnumerable<PayReceiveViewModel>> GetOrderedPayReceiveItemsAsync(int type,
            Expression<Func<PayReceive, bool>> criteria = null, GridOptions gridOptions = null)
        {
            var viewId = GetViewId(type);
            var options = gridOptions ?? new GridOptions();
            return await Repository
                .GetAllOperationQuery<PayReceive>(viewId)
                .Where(criteria)
                .OrderBy(item => item.PayReceiveNo)
                .Select(item => Mapper.Map<PayReceiveViewModel>(item))
                .ApplyQuickFilter(options)
                .Apply(options)
                .ToListAsync();
        }

        private async Task SetPayReceiveNavigationAsync(PayReceiveViewModel payReceive,
            GridOptions gridOptions = null)
        {
            int nextCount, prevCount;
            int viewId = GetViewId(payReceive.Type);
            var options = gridOptions ?? new GridOptions();
            var query = Repository
                .GetAllOperationQuery<PayReceive>(viewId)
                .Where(pr => String.Compare(pr.PayReceiveNo, payReceive.PayReceiveNo) < 0 &&
                    pr.Type == payReceive.Type);

            if (!options.IsEmpty)
            {
                var items = await query.ToListAsync();
                prevCount = items
                    .Select(item => Mapper.Map<PayReceiveViewModel>(item))
                    .ApplyQuickFilter(options, false)
                    .Apply(options, false)
                    .Count();
            }
            else
            {
                prevCount = await query.CountAsync();
            }

            query = Repository
                .GetAllOperationQuery<PayReceive>(viewId)
                .Where(pr => String.Compare(pr.PayReceiveNo, payReceive.PayReceiveNo) > 0 &&
                    pr.Type == payReceive.Type);

            if (!options.IsEmpty)
            {
                var items = await query.ToListAsync();
                nextCount = items
                    .Select(item => Mapper.Map<PayReceiveViewModel>(item))
                    .ApplyQuickFilter(options, false)
                    .Apply(options, false)
                    .Count();
            }
            else
            {
                nextCount = await query.CountAsync();
            }

            payReceive.HasNext = nextCount > 0;
            payReceive.HasPrevious = prevCount > 0;
        }

        private int GetEntityTypeId(int type)
        {
            return (int)(type == (int)PayReceiveType.Receival
                ? EntityTypeId.Receival
                : EntityTypeId.Payment);
        }

        private int GetViewId(int type)
        {
            return type == (int)PayReceiveType.Payment
                ? ViewId.Payment
                : ViewId.Receival;
        }

        internal override int? EntityType
        {
            get { return (int?)EntityTypeId.Receival; }
        }

        /// <summary>
        /// آخرین تغییرات موجودیت را از مدل نمایشی به سطر اطلاعاتی موجود کپی می کند
        /// </summary>
        /// <param name="payReceiveViewModel">مدل نمایشی شامل آخرین تغییرات</param>
        /// <param name="payReceive">سطر اطلاعاتی موجود</param>
        protected override void UpdateExisting(PayReceiveViewModel payReceiveViewModel, PayReceive payReceive)
        {
            payReceive.PayReceiveNo = payReceiveViewModel.PayReceiveNo;
            payReceive.Reference = payReceiveViewModel.Reference;
            payReceive.Date = payReceiveViewModel.Date;
            payReceive.CurrencyId = payReceiveViewModel.CurrencyId;
            payReceive.CurrencyRate = payReceiveViewModel.CurrencyRate;
            payReceive.Description = payReceiveViewModel.Description;
            payReceive.ModifiedByName = payReceiveViewModel.ModifiedByName;
            payReceive.ModifiedById = payReceiveViewModel.ModifiedById;
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
    }
}
