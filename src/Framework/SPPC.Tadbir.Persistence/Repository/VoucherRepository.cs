using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Common;
using SPPC.Framework.Extensions;
using SPPC.Framework.Mapper;
using SPPC.Framework.Persistence;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.ViewModel.Auth;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.ViewModel.Metadata;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت اطلاعات اسناد مالی و آرتیکل های آنها را پیاده سازی می کند.
    /// </summary>
    public class VoucherRepository : SecureRepository, IVoucherRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="unitOfWork">پیاده سازی اینترفیس واحد کاری برای انجام عملیات دیتابیسی </param>
        /// <param name="mapper">نگاشت مورد استفاده برای تبدیل کلاس های مدل اطلاعاتی</param>
        /// <param name="metadataRepository">امکان خواندن متادیتا برای یک موجودیت را فراهم می کند</param>
        public VoucherRepository(IAppUnitOfWork unitOfWork, IDomainMapper mapper, IMetadataRepository metadataRepository)
            : base(unitOfWork, mapper)
        {
            _metadataRepository = metadataRepository;
        }

        #region Voucher Operations

        /// <summary>
        /// به روش آسنکرون، کلیه اسناد مالی را که در دوره مالی و شعبه مشخص شده تعریف شده اند، از دیتابیس خوانده و برمی گرداند
        /// </summary>
        /// <param name="userAccess">
        /// اطلاعات دسترسی کاربر به منابع محدود شده مانند نقش ها، دوره های مالی و شعبه ها
        /// </param>
        /// <param name="fpId">شناسه دیتابیسی یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه دیتابیسی یکی از شعب موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از اسناد مالی تعریف شده در دوره مالی و شعبه مشخص شده</returns>
        public async Task<IList<VoucherViewModel>> GetVouchersAsync(
            UserAccessViewModel userAccess, int fpId, int branchId, GridOptions gridOptions = null)
        {
            var vouchers = await GetAllOperationAsync<Voucher>(
                userAccess, fpId, branchId, gridOptions, v => v.Lines, v => v.FiscalPeriod, v => v.Branch);
            return vouchers
                .Select(item => Mapper.Map<VoucherViewModel>(item))
                .ToList();
        }

        /// <summary>
        /// به روش آسنکرون، سند مالی با شناسه دیتابیسی مشخص شده را از دیتابیس خوانده و برمی گرداند
        /// </summary>
        /// <param name="voucherId">شناسه دیتابیسی یکی از اسناد مالی موجود</param>
        /// <returns>سند مالی مشخص شده با شناسه دیتابیسی</returns>
        public async Task<VoucherViewModel> GetVoucherAsync(int voucherId)
        {
            VoucherViewModel voucherViewModel = null;
            var repository = UnitOfWork.GetAsyncRepository<Voucher>();
            var voucher = await repository.GetByIDAsync(voucherId);
            if (voucher != null)
            {
                voucherViewModel = Mapper.Map<VoucherViewModel>(voucher);
            }

            return voucherViewModel;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات فراداده ای تعریف شده برای سند مالی را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات فراداده ای تعریف شده برای سند مالی</returns>
        public async Task<EntityViewModel> GetVoucherMetadataAsync()
        {
            return await _metadataRepository.GetEntityMetadataAsync<Voucher>();
        }

        /// <summary>
        /// به روش آسنکرون، تعداد اسناد مالی تعریف شده در دوره مالی و شعبه مشخص شده را
        /// از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="userAccess">
        /// اطلاعات دسترسی کاربر به منابع محدود شده مانند نقش ها، دوره های مالی و شعبه ها
        /// </param>
        /// <param name="fpId">شناسه عددی یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه عددی یکی از شعب موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>تعداد اسناد مالی تعریف شده در دوره مالی و شعبه مشخص شده</returns>
        public async Task<int> GetCountAsync(
            UserAccessViewModel userAccess, int fpId, int branchId, GridOptions gridOptions = null)
        {
            return await GetOperationCountAsync<Voucher>(userAccess, fpId, branchId, gridOptions);
        }

        /// <summary>
        /// به روش آسنکرون، مدل نمایشی دوره مالی مورد استفاده در یک سند مالی را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="voucher">مدل نمایشی سند مالی مورد نظر</param>
        /// <returns>مدل نمایشی دوره مالی به کار رفته در سند مالی</returns>
        public async Task<FiscalPeriodViewModel> GetVoucherFiscalPeriodAsync(VoucherViewModel voucher)
        {
            Verify.ArgumentNotNull(voucher, "voucher");
            var repository = UnitOfWork.GetAsyncRepository<FiscalPeriod>();
            var fiscalPeriod = await repository.GetByIDAsync(voucher.FiscalPeriodId);
            return Mapper.Map<FiscalPeriodViewModel>(fiscalPeriod);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات یک سند مالی را در دیتابیس ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="voucher">سند مالی برای ایجاد یا اصلاح</param>
        /// <returns>مدل نمایشی سند ایجاد یا اصلاح شده</returns>
        public async Task<VoucherViewModel> SaveVoucherAsync(VoucherViewModel voucher)
        {
            Verify.ArgumentNotNull(voucher, "voucher");
            Voucher voucherModel = default(Voucher);
            var repository = UnitOfWork.GetAsyncRepository<Voucher>();
            if (voucher.Id == 0)
            {
                voucherModel = Mapper.Map<Voucher>(voucher);
                repository.Insert(voucherModel);
            }
            else
            {
                voucherModel = await repository.GetByIDAsync(voucher.Id, v => v.FiscalPeriod, v => v.Branch);
                if (voucherModel != null)
                {
                    UpdateExistingVoucher(voucherModel, voucher);
                    repository.Update(voucherModel);
                }
            }

            await UnitOfWork.CommitAsync();
            return Mapper.Map<VoucherViewModel>(voucherModel);
        }

        /// <summary>
        /// به روش آسنکرون، سند مالی مشخص شده با شناسه دیتابیسی را از دیتابیس حذف می کند
        /// </summary>
        /// <param name="voucherId">شناسه دیتابیسی سند مالی برای حذف</param>
        public async Task DeleteVoucherAsync(int voucherId)
        {
            var repository = UnitOfWork.GetAsyncRepository<Voucher>();
            var voucher = await repository.GetByIDWithTrackingAsync(voucherId, txn => txn.Lines);
            if (voucher != null)
            {
                voucher.Lines.Clear();
                repository.Delete(voucher);
                await UnitOfWork.CommitAsync();
            }
        }

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا شماره سند مورد نظر تکراری است یا نه
        /// </summary>
        /// <param name="voucher">سند مالی که تکراری بودن شماره آن باید بررسی شود</param>
        /// <returns>مقدار بولی درست در صورت تکراری بودن شماره، در غیر این صورت مقدار بولی نادرست</returns>
        public async Task<bool> IsDuplicateVoucherNoAsync(VoucherViewModel voucher)
        {
            Verify.ArgumentNotNull(voucher, "voucher");
            var repository = UnitOfWork.GetAsyncRepository<Voucher>();
            var duplicates = await repository
                .GetByCriteriaAsync(vch => vch.Id != voucher.Id
                    && vch.No == voucher.No
                    && vch.FiscalPeriod.Id == voucher.FiscalPeriodId
                    && vch.Branch.Id == voucher.BranchId);
            return (duplicates.Count > 0);
        }

        /// <inheritdoc/>
        protected override int ViewId
        {
            // TODO: Remove this hard-coded value later
            get { return 2; }
        }

        #endregion

        #region Voucher Line Operations

        /// <summary>
        /// به روش آسنکرون، آرتیکل های یک سند مشخص شده با شناسه عددی را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="userAccess">
        /// اطلاعات دسترسی کاربر به منابع محدود شده مانند نقش ها، دوره های مالی و شعبه ها
        /// </param>
        /// <param name="voucherId">شناسه یکی از اسناد مالی موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>آرتیکل های سندمشخص شده با شناسه عددی</returns>
        public async Task<IList<VoucherLineViewModel>> GetArticlesAsync(
            UserAccessViewModel userAccess, int voucherId, GridOptions gridOptions = null)
        {
            var query = GetVoucherLinesQuery(voucherId);
            query = ApplyRowFilter(ref query, userAccess);
            var lines = await query
                .Apply(gridOptions)
                .Select(line => Mapper.Map<VoucherLineViewModel>(line))
                .ToListAsync();
            return lines;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات سطر سند مالی (آرتیکل) مشخص شده با شناسه دیتابیسی را از دیتابیس خوانده و برمی گرداند
        /// </summary>
        /// <param name="articleId">شناسه دیتابیسی آرتیکل موجود</param>
        /// <returns>اطلاعات آرتیکل مشخص شده با شناسه دیتابیسی</returns>
        public async Task<VoucherLineViewModel> GetArticleAsync(int articleId)
        {
            VoucherLineViewModel articleViewModel = null;
            var repository = UnitOfWork.GetAsyncRepository<VoucherLine>();
            var article = await repository.GetByIDAsync(articleId);
            if (article != null)
            {
                articleViewModel = Mapper.Map<VoucherLineViewModel>(article);
            }

            return articleViewModel;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات فراداده ای تعریف شده برای آرتیکل سند مالی را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات فراداده ای تعریف شده برای آرتیکل سند مالی</returns>
        public async Task<EntityViewModel> GetVoucherLineMetadataAsync()
        {
            return await _metadataRepository.GetEntityMetadataAsync<VoucherLine>();
        }

        /// <summary>
        /// به روش آسنکرون، تعداد آرتیکل های یک سند مالی را بعد از اعمال فیلتر (در صورت وجود)
        /// از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="userAccess">
        /// اطلاعات دسترسی کاربر به منابع محدود شده مانند نقش ها، دوره های مالی و شعبه ها
        /// </param>
        /// <param name="voucherId">شناسه یکی از اسناد مالی موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>تعداد آرتیکل های سند مالی بعد از اعمال فیلتر</returns>
        public async Task<int> GetArticleCountAsync(
            UserAccessViewModel userAccess, int voucherId, GridOptions gridOptions = null)
        {
            var repository = UnitOfWork.GetAsyncRepository<VoucherLine>();
            var query = repository.GetEntityQuery()
                .Where(line => line.Voucher.Id == voucherId);
            query = ApplyRowFilter(ref query, userAccess);
            return await query
                .Apply(gridOptions)
                .CountAsync();
        }

        /// <summary>
        /// به روش آسنکرون، مدل نمایشی سرفصل حسابداری مشخص شده
        /// را از دیتابیس خوانده و برمی گرداند
        /// </summary>
        /// <param name="accountId">شناسه دیتابیسی یکی از حساب های موجود</param>
        /// <returns>مدل نمایشی سرفصل حسابداری مورد استفاده در آرتیکل</returns>
        public async Task<AccountViewModel> GetArticleAccountAsync(int accountId)
        {
            var articleAccount = default(AccountViewModel);
            var repository = UnitOfWork.GetAsyncRepository<Account>();
            var account = await repository.GetByIDAsync(accountId, acc => acc.Children);
            if (account != null)
            {
                articleAccount = Mapper.Map<AccountViewModel>(account);
            }

            return articleAccount;
        }

        /// <summary>
        /// به روش آسنکرون، مدل نمایشی تفصیلی شناور مشخص شده
        /// را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="faccountId">شناسه دیتابیسی یکی از تفصیلی های شناور موجود</param>
        /// <returns>مدل نمایشی تفصیلی شناور مورد استفاده در آرتیکل</returns>
        public async Task<DetailAccountViewModel> GetArticleDetailAccountAsync(int faccountId)
        {
            var articleDetailAccount = default(DetailAccountViewModel);
            var repository = UnitOfWork.GetAsyncRepository<DetailAccount>();
            var detailAccount = await repository.GetByIDAsync(faccountId, acc => acc.Children);
            if (detailAccount != null)
            {
                articleDetailAccount = Mapper.Map<DetailAccountViewModel>(detailAccount);
            }

            return articleDetailAccount;
        }

        /// <summary>
        /// به روش آسنکرون، مدل نمایشی مرکز هزینه مشخص شده
        /// را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="costCenterId">شناسه دیتابیسی یکی از مراکز هزینه موجود</param>
        /// <returns>مدل نمایشی مرکز هزینه مورد استفاده در آرتیکل</returns>
        public async Task<CostCenterViewModel> GetArticleCostCenterAsync(int costCenterId)
        {
            var articleCostCenter = default(CostCenterViewModel);
            var repository = UnitOfWork.GetAsyncRepository<CostCenter>();
            var costCenter = await repository.GetByIDAsync(costCenterId, acc => acc.Children);
            if (costCenter != null)
            {
                articleCostCenter = Mapper.Map<CostCenterViewModel>(costCenter);
            }

            return articleCostCenter;
        }

        /// <summary>
        /// به روش آسنکرون، مدل نمایشی پروژه مشخص شده
        /// را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="projectId">شناسه دیتابیسی یکی از پروژه های موجود</param>
        /// <returns>مدل نمایشی پروژه مورد استفاده در آرتیکل</returns>
        public async Task<ProjectViewModel> GetArticleProjectAsync(int projectId)
        {
            var articleProject = default(ProjectViewModel);
            var repository = UnitOfWork.GetAsyncRepository<Project>();
            var project = await repository.GetByIDAsync(projectId, acc => acc.Children);
            if (project != null)
            {
                articleProject = Mapper.Map<ProjectViewModel>(project);
            }

            return articleProject;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات یک سطر سند مالی (آرتیکل) را در دیتابیس ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="article">آرتیکل برای ایجاد یا اصلاح</param>
        public async Task<VoucherLineViewModel> SaveArticleAsync(VoucherLineViewModel article)
        {
            Verify.ArgumentNotNull(article, "article");
            VoucherLine lineModel = default(VoucherLine);
            var repository = UnitOfWork.GetAsyncRepository<VoucherLine>();
            if (article.Id == 0)
            {
                lineModel = Mapper.Map<VoucherLine>(article);
                repository.Insert(lineModel);
            }
            else
            {
                lineModel = await repository.GetByIDAsync(article.Id);
                if (lineModel != null)
                {
                    UpdateExistingArticle(lineModel, article);
                    repository.Update(lineModel);
                }
            }

            await UnitOfWork.CommitAsync();
            return Mapper.Map<VoucherLineViewModel>(lineModel);
        }

        /// <summary>
        /// به روش آسنکرون، سطر سند مالی (آرتیکل) مشخص شده با شناسه دیتابیسی را از محل ذخیره حذف می کند
        /// </summary>
        /// <param name="articleId">شناسه دیتابیسی آرتیکل برای حذف</param>
        public async Task DeleteArticleAsync(int articleId)
        {
            var repository = UnitOfWork.GetAsyncRepository<VoucherLine>();
            var article = await repository.GetByIDAsync(articleId);
            if (article != null)
            {
                article.Account = null;
                article.DetailAccount = null;
                article.CostCenter = null;
                article.Project = null;
                article.Branch = null;
                article.Currency = null;
                article.FiscalPeriod = null;
                article.Voucher = null;
                repository.Delete(article);
                await UnitOfWork.CommitAsync();
            }
        }

        #endregion

        private static void UpdateExistingArticle(VoucherLine existing, VoucherLineViewModel article)
        {
            existing.AccountId = article.FullAccount.AccountId ?? 0;
            existing.DetailId = article.FullAccount.DetailId;
            existing.CostCenterId = article.FullAccount.CostCenterId;
            existing.ProjectId = article.FullAccount.ProjectId;
            existing.CurrencyId = article.CurrencyId ?? 0;
            existing.Debit = article.Debit;
            existing.Credit = article.Credit;
            existing.Description = article.Description;
        }

        private static void UpdateExistingVoucher(Voucher existing, VoucherViewModel voucher)
        {
            existing.No = voucher.No;
            existing.Date = voucher.Date;
            existing.Description = voucher.Description;
        }

        private IQueryable<VoucherLine> GetVoucherLinesQuery(int voucherId, GridOptions gridOptions = null)
        {
            var repository = UnitOfWork.GetRepository<VoucherLine>();
            var linesQuery = repository
                .GetEntityQuery(
                    line => line.Voucher, line => line.Account, line => line.DetailAccount, line => line.CostCenter,
                    line => line.Project, line => line.Currency, line => line.FiscalPeriod, line => line.Branch)
                .Where(line => line.Voucher.Id == voucherId)
                .Apply(gridOptions);
            return linesQuery;
        }

        private IMetadataRepository _metadataRepository;
    }
}
