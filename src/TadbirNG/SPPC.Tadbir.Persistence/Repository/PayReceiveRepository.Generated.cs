using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Common;
using SPPC.Framework.Extensions;
using SPPC.Framework.Persistence;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model.CashFlow;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.ViewModel.CashFlow;
using SPPC.Tadbir.ViewModel.Finance;

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
        /// <param name="type">نوع فرم مورد نظر برای درخواست جاری - دریافت یا پرداخت</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای دریافت اطلاعات لازم از سمت وب</param>
        /// <returns>فرم دریافت/پرداخت مشخص شده با شناسه عددی</returns>
        public async Task<PayReceiveViewModel> GetPayReceiveAsync(int payReceiveId, int type = 0,
            GridOptions gridOptions = null)
        {
            PayReceiveViewModel item = null;
            var options = gridOptions ?? new GridOptions();
            if (options.Operation != (int)OperationId.Print &&
                options.Operation != (int)OperationId.PrintPreview)
            {
                var repository = UnitOfWork.GetAsyncRepository<PayReceive>();
                var payReceive = await repository.GetByIDAsync(
                    payReceiveId, pr => pr.Accounts, pr => pr.CashAccounts, pr => pr.PayReceiveVoucherLines);
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

        /// <inheritdoc/>
        public async Task<PayReceiveViewModel> SavePayReceiveAsync(PayReceiveViewModel payReceive, int type)
        {
            Verify.ArgumentNotNull(payReceive, nameof(payReceive));
            int entityTypeId = GetEntityTypeId(type);
            string personName = GetCurrentUserFullName();
            PayReceive payReceiveModel;
            var repository = UnitOfWork.GetAsyncRepository<PayReceive>();
            if (payReceive.Id == 0)
            {
                payReceiveModel = Mapper.Map<PayReceive>(payReceive);
                payReceiveModel.TextNo = Int64.Parse(payReceive.TextNo.Trim()).ToString();
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
        /// <param name="type">نوع فرم مورد نظر برای درخواست جاری - دریافت یا پرداخت</param>
        public async Task DeletePayReceiveAsync(int payReceiveId, int type)
        {
            var repository = UnitOfWork.GetAsyncRepository<PayReceive>();
            var payReceive = await repository.GetByIDWithTrackingAsync(payReceiveId, pr => pr.Accounts, pr => pr.CashAccounts);
            if (payReceive != null)
            {
                payReceive.Accounts.Clear();
                payReceive.CashAccounts.Clear();
                int entityTypeId = GetEntityTypeId(type);
                await DeleteAsync(repository, payReceive, OperationId.Delete, entityTypeId);
            }
        }

        /// <inheritdoc/>
        public async Task<bool> IsDuplicateTextNoAsync(PayReceiveViewModel payReceive, int type)
        {
            var repository = UnitOfWork.GetAsyncRepository<PayReceive>();
            return await repository
                .GetEntityQuery()
                .AnyAsync(pr => pr.Id != payReceive.Id
                    && pr.TextNo == payReceive.TextNo
                    && pr.Type == type
                    && pr.FiscalPeriodId == payReceive.FiscalPeriodId
                    && pr.BranchId == payReceive.BranchId);
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
        /// <param name="textNo">شماره فرم دریافت/پرداخت مورد نظر</param>
        /// <param name="type">نوع فرم مورد نظر برای درخواست جاری - دریافت یا پرداخت</param>
        /// <returns>فرم دریافت/پرداخت مشخص شده با شماره</returns>
        public async Task<PayReceiveViewModel> GetPayReceiveByNoAsync(string textNo, int type)
        {
            var byNo = default(PayReceiveViewModel);
            var viewId = GetViewId((int)type);
            var payReceiveByNo = await Repository.GetAllOperationQuery<PayReceive>(
                viewId, pr => pr.Accounts, pr => pr.CashAccounts, pr => pr.PayReceiveVoucherLines)
                .Where(pr => pr.TextNo == textNo.Trim() && pr.Type == (int)type)
                .SingleOrDefaultAsync();

            if (payReceiveByNo != null)
            {
                byNo = Mapper.Map<PayReceiveViewModel>(payReceiveByNo);
                await SetPayReceiveNavigationAsync(byNo, type);
            }

            return byNo;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات اولین فرم دریافت/پرداخت را از نوع مشخص شده خوانده و برمی گرداند
        /// </summary>
        /// <param name="type">نوع فرم مورد نظر برای درخواست جاری - دریافت یا پرداخت</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>اولین فرم دریافت/پرداخت</returns>
        public async Task<PayReceiveViewModel> GetFirstPayReceiveAsync(int type, GridOptions gridOptions = null)
        {
            var payReceives = await GetOrderedPayReceiveItemsAsync(type,
                pr => pr.Type == type, gridOptions);
            var first = payReceives.FirstOrDefault();
            if (first != null)
            {
                await SetPayReceiveNavigationAsync(first, type, gridOptions);
            }

            return first;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات آخرین فرم دریافت/پرداخت را از نوع مشخص شده خوانده و برمی گرداند
        /// </summary>
        /// <param name="type">نوع فرم مورد نظر برای درخواست جاری - دریافت یا پرداخت</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>آخرین فرم دریافت/پرداخت</returns>
        public async Task<PayReceiveViewModel> GetLastPayReceiveAsync(int type, GridOptions gridOptions = null)
        {
            var payReceives = await GetOrderedPayReceiveItemsAsync(type,
                pr => pr.Type == type, gridOptions);
            var last = payReceives.LastOrDefault();
            if (last != null)
            {
                await SetPayReceiveNavigationAsync(last, type, gridOptions);
            }

            return last;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات فرم دریافت/پرداخت بعدی را از نوع مشخص شده خوانده و برمی گرداند
        /// </summary>
        /// <param name="type">نوع فرم مورد نظر برای درخواست جاری - دریافت یا پرداخت</param>
        /// <param name="currentNo">شماره فرم دریافت/پرداخت جاری در برنامه</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>فرم دریافت/پرداخت بعدی</returns>
        public async Task<PayReceiveViewModel> GetNextPayReceiveAsync(string currentNo, int type,
            GridOptions gridOptions = null)
        {
            var payReceives = await GetOrderedPayReceiveItemsAsync(type, pr =>
                Convert.ToInt64(pr.TextNo) > Convert.ToInt64(currentNo) && pr.Type == type, gridOptions);
            var next = payReceives.FirstOrDefault();
            if (next != null)
            {
                await SetPayReceiveNavigationAsync(next, type, gridOptions);
            }

            return next;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات فرم دریافت/پرداخت قبلی را از نوع مشخص شده خوانده و برمی گرداند
        /// </summary>
        /// <param name="type">نوع فرم مورد نظر برای درخواست جاری - دریافت یا پرداخت</param>
        /// <param name="currentNo">شماره فرم دریافت/پرداخت جاری در برنامه</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>فرم دریافت/پرداخت قبلی</returns>
        public async Task<PayReceiveViewModel> GetPreviousPayReceiveAsync(string currentNo, int type,
            GridOptions gridOptions = null)
        {
            var payReceives = await GetOrderedPayReceiveItemsAsync(type, pr =>
                Convert.ToInt64(pr.TextNo) < Convert.ToInt64(currentNo) && pr.Type == type, gridOptions);
            var previous = payReceives.LastOrDefault();
            if (previous != null)
            {
                await SetPayReceiveNavigationAsync(previous, type, gridOptions);
            }

            return previous;
        }

        /// <summary>
        /// به روش آسنکرون، نمونه ای جدید از فرم دریافت/پرداخت می سازد
        /// </summary>
        /// <param name="type">نوع فرم مورد نظر برای درخواست جاری - دریافت یا پرداخت</param>
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
                TextNo = await GetNewPayReceiveNumberAsync(type),
                Type = (short)type
            };

            newPayReceive = await SavePayReceiveAsync(newPayReceive, type);
            await SetPayReceiveNavigationAsync(newPayReceive, type);
            return newPayReceive;
        }

        /// <summary>
        /// به روش آسنکرون، بررسی می کند که آیا برای فرم دریافت/پرداخت داده شده
        /// طرف حساب تعریف شده است یا خیر
        /// </summary>
        /// <param name="payReceiveId">شناسه فرم دریافت/پرداخت مورد نظر</param>
        /// <returns>در صورت وجود آرتیکل حساب مقدار درست و
        /// در غیر این صورت مقدار نادرست برمی گرداند</returns>
        public async Task<bool> HasAccountArticleAsync(int payReceiveId)
        {
            var repository = UnitOfWork.GetAsyncRepository<PayReceiveAccount>();
            return await repository.
                GetEntityQuery()
                .AnyAsync(a => a.PayReceiveId == payReceiveId);
        }

        /// <inheritdoc/>
        public async Task<bool> HasCashAccountArticleAsync(int payReceiveId)
        {
            var repository = UnitOfWork.GetAsyncRepository<PayReceiveCashAccount>();
            return await repository.
                GetEntityQuery()
                .AnyAsync(ca => ca.PayReceiveId == payReceiveId);
        }

        /// <inheritdoc/>
        public async Task<VoucherViewModel> RegisterAsync(int payReceiveId, int voucherId)
        {
            var payReceive = await GetPayReceiveAsync(payReceiveId);
            var voucher = await GetVoucherAsync(voucherId, payReceive.Date);
            if (voucher != null)
            {
                int rowNo = await GetLastVoucherLineRowNoAsync(voucher);
                if (payReceive.Type == (int)PayReceiveType.Receipt)
                {
                    RgisterCashAccountArticles(payReceive, voucher, rowNo);
                    rowNo = rowNo + payReceive.CashAccounts.Count;
                    RegisterAccountArticles(payReceive, voucher, rowNo);
                }
                else
                {
                    RegisterAccountArticles(payReceive, voucher, rowNo);
                    rowNo = rowNo + payReceive.Accounts.Count;
                    RgisterCashAccountArticles(payReceive, voucher, rowNo);
                }

                int entityTypeId = GetEntityTypeId(payReceive.Type);
                await FinalizeActionAsync(OperationId.Register, entityTypeId);
            }

            return Mapper.Map<VoucherViewModel>(voucher);
        }

        /// <inheritdoc/>
        public async Task UndoRegisterAsync(int payReceiveId, int type)
        {
            var repository = UnitOfWork.GetAsyncRepository<PayReceiveVoucherLine>();
            var voucherLineRepository = UnitOfWork.GetAsyncRepository<VoucherLine>();
            var payReceiveVoucherLines = await GetRegisteredArticlesAsync(repository, payReceiveId);
            foreach (var item in payReceiveVoucherLines)
            {
                voucherLineRepository.Delete(item.VoucherLine);
                repository.Delete(item);
            }

            int entityTypeId = GetEntityTypeId(type);
            await FinalizeActionAsync(OperationId.UndoRegister, entityTypeId);
        }

        /// <inheritdoc/>
        public async Task<bool> IsValidVoucherForRegisterAsync(int voucherId, DateTime operationalDate)
        {
            return await Repository
                 .GetAllOperationQuery<Voucher>(ViewId.Voucher)
                 .AnyAsync(v => v.Id == voucherId
                     && v.Date.Date == operationalDate.Date
                     && v.BranchId == UserContext.BranchId
                     && v.StatusId == (int)DocumentStatusId.NotChecked);
        }

        ///<inheritdoc/>
        public async Task<VoucherViewModel> GetVoucherOfRegisterAsync(int PayReceiveId)
        {
            var repository = UnitOfWork.GetAsyncRepository<Voucher>();
            var payReceiveVoucherLineRepository = UnitOfWork.GetAsyncRepository<PayReceiveVoucherLine>();
            var voucherId = await payReceiveVoucherLineRepository
                .GetEntityQuery()
                .Where(prv => prv.PayReceiveId == PayReceiveId)
                .Select(prv => prv.VoucherLine.VoucherId)
                .FirstOrDefaultAsync();
            return await repository
                .GetEntityQuery()
                .Where(v => v.Id == voucherId)
                .Select(v => Mapper.Map<VoucherViewModel>(v))
                .SingleOrDefaultAsync();
        }

        internal override int? EntityType
        {
            get { return (int?)EntityTypeId.Receipt; }
        }

        /// <summary>
        /// آخرین تغییرات موجودیت را از مدل نمایشی به سطر اطلاعاتی موجود کپی می کند
        /// </summary>
        /// <param name="payReceiveViewModel">مدل نمایشی شامل آخرین تغییرات</param>
        /// <param name="payReceive">سطر اطلاعاتی موجود</param>
        protected override void UpdateExisting(PayReceiveViewModel payReceiveViewModel, PayReceive payReceive)
        {
            payReceive.TextNo = Int64.Parse(payReceiveViewModel.TextNo.Trim()).ToString();
            payReceive.Reference = payReceiveViewModel.Reference;
            payReceive.Date = payReceiveViewModel.Date;
            payReceive.CurrencyId = payReceiveViewModel.CurrencyId;
            payReceive.CurrencyRate = payReceiveViewModel.CurrencyRate;
            payReceive.Description = payReceiveViewModel.Description;
        }

        /// <summary>
        /// اطلاعات خلاصه سطر اطلاعاتی داده شده را به صورت یک رشته متنی برمی گرداند
        /// </summary>
        /// <param name="entity">یکی از سطرهای اطلاعاتی موجود</param>
        /// <returns>اطلاعات خلاصه سطر اطلاعاتی داده شده به صورت رشته متنی</returns>
        protected override string GetState(PayReceive entity)
        {
            return entity != null
                ? $"{AppStrings.TextNo} : {entity.TextNo}, " +
                    $"{AppStrings.Reference} : {entity.Reference}, {AppStrings.Date} : {entity.Date}, " +
                    $"{AppStrings.CurrencyRate} : {entity.CurrencyRate}, {AppStrings.Description} : {entity.Description}, "
                : String.Empty;
        }

        private async Task<DateTime> GetLastPayReceiveDateAsync(int type)
        {
            var repository = UnitOfWork.GetAsyncRepository<PayReceive>();
            var lastByDate = await repository
                .GetEntityQuery()
                .Where(pr => pr.FiscalPeriodId == UserContext.FiscalPeriodId
                    && pr.BranchId == UserContext.BranchId
                    && pr.Type == type)
                .OrderByDescending(pr => pr.Id)
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
                .GetAllOperationQuery<PayReceive>(viewId,
                    pr => pr.Accounts, pr => pr.CashAccounts, pr => pr.PayReceiveVoucherLines)
                .Where(criteria)
                .OrderBy(item => Convert.ToInt64(item.TextNo))
                .Select(item => Mapper.Map<PayReceiveViewModel>(item))
                .ApplyQuickFilter(options, false)
                .Apply(options, false)
                .ToListAsync();
        }

        private async Task SetPayReceiveNavigationAsync(PayReceiveViewModel payReceive, int type,
            GridOptions gridOptions = null)
        {
            int nextCount, prevCount;
            int viewId = GetViewId(type);
            var options = gridOptions ?? new GridOptions();
            var query = Repository
                .GetAllOperationQuery<PayReceive>(viewId)
                .Where(pr => Convert.ToInt64(pr.TextNo) < Convert.ToInt64(payReceive.TextNo) &&
                    pr.Type == type);

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
                .Where(pr => Convert.ToInt64(pr.TextNo) > Convert.ToInt64(payReceive.TextNo) &&
                    pr.Type == type);

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

        private async Task<IEnumerable<PayReceiveVoucherLine>> GetRegisteredArticlesAsync(
            IRepository<PayReceiveVoucherLine> repository, int payReceiveId)
        {
            return await repository
                .GetEntityQuery(item => item.VoucherLine)
                .Where(item => item.PayReceiveId == payReceiveId)
                .ToListAsync();
        }

        private int GetEntityTypeId(int type)
        {
            return (int)(type == (int)PayReceiveType.Receipt
                ? EntityTypeId.Receipt
                : EntityTypeId.Payment);
        }

        private int GetViewId(int type)
        {
            return type == (int)PayReceiveType.Payment
                ? ViewId.Payment
                : ViewId.Receipt;
        }

        private async Task<string> GetNewPayReceiveNumberAsync(int type)
        {
            var repository = UnitOfWork.GetAsyncRepository<PayReceive>();
            var lastNumber = await repository
                .GetEntityQuery()
                .Where(pr => pr.FiscalPeriodId == UserContext.FiscalPeriodId
                    && pr.BranchId == UserContext.BranchId
                    && pr.Type == type)
                .Select(pr => Convert.ToInt64(pr.TextNo))
                .OrderByDescending(no => no)
                .FirstOrDefaultAsync();

            return Math.Min(lastNumber + 1, Int64.MaxValue).ToString();
        }

        private async Task<Voucher> GetVoucherAsync(int voucherId, DateTime operationalDate)
        {
            Voucher voucher = null;
            var voucherRepository = UnitOfWork.GetAsyncRepository<Voucher>();
            if (voucherId == 0)
            {
                var subject = SubjectType.Normal;
                string fullName = GetCurrentUserFullName();
                string description = Context.Localize(AppStrings.TreasurySystemicVoucherDefaultDescription);
                int no = await GetLastVoucherNoAsync();
                int dailyNo = await GetNextDailyNoAsync(operationalDate, subject);
                voucher = new Voucher()
                {
                    BranchId = UserContext.BranchId,
                    DailyNo = dailyNo,
                    Date = operationalDate,
                    Description = description,
                    FiscalPeriodId = UserContext.FiscalPeriodId,
                    IsBalanced = true,
                    CreatedById = UserContext.Id,
                    IssuerName = fullName,
                    ModifiedById = UserContext.Id,
                    ModifierName = fullName,
                    No = no + 1,
                    StatusId = (int)DocumentStatusId.NotChecked,
                    SubjectType = (short)subject,
                    Type = (short)VoucherType.NormalVoucher,
                };

                voucherRepository.Insert(voucher);
            }
            else
            {
                voucher = await voucherRepository.GetByIDAsync(voucherId);
            }

            return voucher;
        }

        private string GetArticleDescription(
            PayReceive payReceive, string remarks, string bankOrderNo = null)
        {
            string description = string.Empty;
            string currencyText = String.Empty;
            if (payReceive.CurrencyId > 0)
            {
                currencyText = Context.Localize($" - {AppStrings.Currency} {payReceive.Currency.Name}").ToLower();
            }

            string ArticleText = String.Empty;
            if (!String.IsNullOrWhiteSpace(remarks))
            {
                ArticleText = $" - {remarks.ToLower().Trim()}";
            }

            string payReceiveText = String.Empty;
            if (!String.IsNullOrWhiteSpace(payReceive.Description))
            {
                payReceiveText = $" - {payReceive.Description.ToLower().Trim()}";
            }

            if (payReceive.Type == (int)PayReceiveType.Receipt)
            {
                string bankOrederNoText = String.Empty;
                if (!String.IsNullOrWhiteSpace(bankOrderNo))
                {
                    string template = $" - {Context.Localize(AppStrings.DuringOrderOfBankNo)}";
                    bankOrederNoText = String.Format(template, bankOrderNo).ToLower();
                }

                description = String.Format
                    (Context.Localize(AppStrings.ReceiverCashArticleDescriptionTemplate),
                    payReceive.TextNo, bankOrederNoText, currencyText,
                    ArticleText, payReceiveText);
            }
            else
            {
                description = String.Format
                    (Context.Localize(AppStrings.PayerCashArticleDescriptionTemplate),
                    payReceive.TextNo, currencyText,
                    ArticleText, payReceiveText);
            }

            return description;
        }
        private async Task<int> GetLastVoucherNoAsync(SubjectType type = SubjectType.Normal)
        {
            var repository = UnitOfWork.GetAsyncRepository<Voucher>();
            var lastByNo = await repository
                .GetEntityQuery()
                .Where(voucher => voucher.FiscalPeriodId == UserContext.FiscalPeriodId
                    && voucher.SubjectType == (short)type)
                .OrderByDescending(voucher => voucher.No)
                .FirstOrDefaultAsync();
            return (lastByNo != null) ? lastByNo.No : 0;
        }

        private async Task<int> GetNextDailyNoAsync(DateTime date, SubjectType subject)
        {
            var repository = UnitOfWork.GetAsyncRepository<Voucher>();
            var sameDate = await repository
                .GetByCriteriaAsync(v => v.Date == date
                    && v.FiscalPeriodId == UserContext.FiscalPeriodId
                    && v.SubjectType == (short)subject);
            int lastNo = sameDate
                .OrderByDescending(v => v.DailyNo)
                .Select(v => v.DailyNo)
                .FirstOrDefault();
            return (lastNo + 1);
        }

        private async Task<PayReceive> GetPayReceiveAsync(int payReceiveId)
        {
            var repository = UnitOfWork.GetAsyncRepository<PayReceive>();
            return await repository.GetByIDAsync(payReceiveId,
                pr => pr.Accounts,
                pr => pr.CashAccounts,
                pr => pr.Currency);
        }

        private void RgisterCashAccountArticles(PayReceive payReceive, Voucher voucher, int rowNo)
        {
            var lineRepository = UnitOfWork.GetAsyncRepository<VoucherLine>();
            List<VoucherLine> voucherLines = new List<VoucherLine>();
            foreach (var item in payReceive.CashAccounts)
            {
                var voucherArticle = new VoucherLine
                {
                    AccountId = (int)item.AccountId,
                    DetailAccountId = item.DetailAccountId,
                    CostCenterId = item.CostCenterId,
                    ProjectId = item.ProjectId,
                    RowNo = ++rowNo,
                    SourceAppId = item.SourceAppId,
                    Description = GetArticleDescription(payReceive, item.Remarks, item.BankOrderNo),
                    CreatedById = UserContext.Id,
                    TypeId = (int)VoucherLineType.NormalLine,
                    BranchId = Context.UserContext.BranchId,
                    FiscalPeriodId = Context.UserContext.FiscalPeriodId,
                    Voucher = voucher
                };

                if (payReceive.Type == (int)PayReceiveType.Receipt)
                {
                    voucherArticle.Debit = item.Amount;
                }
                else
                {
                    voucherArticle.Credit = item.Amount;
                }

                voucherLines.Add(voucherArticle);
                lineRepository.Insert(voucherArticle);
            }

            InsertPayReceiveVoucherLineAsync(payReceive.Id, voucherLines);
        }

        private void RegisterAccountArticles(PayReceive payReceive, Voucher voucher, int rowNo)
        {
            var lineRepository = UnitOfWork.GetAsyncRepository<VoucherLine>();
            List<VoucherLine> voucherLines = new List<VoucherLine>();
            foreach (var item in payReceive.Accounts)
            {

                var voucherArticle = new VoucherLine
                {
                    AccountId = (int)item.AccountId,
                    DetailAccountId = item.DetailAccountId,
                    CostCenterId = item.CostCenterId,
                    ProjectId = item.ProjectId,
                    RowNo = ++rowNo,
                    Description = GetArticleDescription(payReceive, item.Remarks),
                    CreatedById = UserContext.Id,
                    TypeId = (int)VoucherLineType.NormalLine,
                    BranchId = Context.UserContext.BranchId,
                    FiscalPeriodId = Context.UserContext.FiscalPeriodId,
                    Voucher = voucher
                };

                if (payReceive.Type == (int)PayReceiveType.Receipt)
                {
                    voucherArticle.Credit = item.Amount;
                }
                else
                {
                    voucherArticle.Debit = item.Amount;
                }

                voucherLines.Add(voucherArticle);
                lineRepository.Insert(voucherArticle);
            }

            InsertPayReceiveVoucherLineAsync(payReceive.Id, voucherLines);
        }

        private async Task<int> GetLastVoucherLineRowNoAsync(Voucher voucher)
        {
            int lastRowNo = 0;
            if (voucher.Id > 0)
            {
                var repository = UnitOfWork.GetAsyncRepository<VoucherLine>();
                var rowNo = await repository
                    .GetEntityQuery()
                    .Where(line => line.VoucherId == voucher.Id)
                    .MaxAsync(line => (int?)line.RowNo);

                lastRowNo = rowNo ?? 0;
            }

            return lastRowNo;
        }

        private void InsertPayReceiveVoucherLineAsync(int payReceiveId, List<VoucherLine> voucherLines)
        {
            var repository = UnitOfWork.GetAsyncRepository<PayReceiveVoucherLine>();
            foreach (var voucherLine in voucherLines)
            {
                var payReceiveVoucherLine = new PayReceiveVoucherLine
                {
                    PayReceiveId = payReceiveId,
                    VoucherLine = voucherLine,
                };

                repository.Insert(payReceiveVoucherLine);
            }
        }

        private ISecureRepository Repository
        {
            get { return _system.Repository; }
        }

        private readonly ISystemRepository _system;
    }
}
