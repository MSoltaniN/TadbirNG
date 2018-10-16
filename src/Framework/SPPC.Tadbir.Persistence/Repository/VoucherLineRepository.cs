using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Common;
using SPPC.Framework.Extensions;
using SPPC.Framework.Mapper;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.ViewModel.Auth;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.ViewModel.Metadata;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت اطلاعات آرتیکل های مالی را پیاده سازی می کند.
    /// </summary>
    public class VoucherLineRepository : LoggingRepository<VoucherLine, VoucherLineViewModel>, IVoucherLineRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="unitOfWork">پیاده سازی اینترفیس واحد کاری برای انجام عملیات دیتابیسی </param>
        /// <param name="mapper">نگاشت مورد استفاده برای تبدیل کلاس های مدل اطلاعاتی</param>
        /// <param name="metadata">امکان خواندن متادیتا برای یک موجودیت را فراهم می کند</param>
        /// <param name="log">امکان ایجاد لاگ های عملیاتی را در دیتابیس سیستمی برنامه فراهم می کند</param>
        /// <param name="repository">امکان فیلتر اطلاعات روی سطرها و شعبه ها را فراهم می کند</param>
        public VoucherLineRepository(
            IAppUnitOfWork unitOfWork, IDomainMapper mapper, IMetadataRepository metadata,
            IOperationLogRepository log, ISecureRepository repository)
            : base(unitOfWork, mapper, metadata, log)
        {
            _repository = repository;
        }

        /// <summary>
        /// به روش آسنکرون، آرتیکل های یک سند مشخص شده با شناسه عددی را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="voucherId">شناسه یکی از اسناد مالی موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>آرتیکل های سندمشخص شده با شناسه عددی</returns>
        public async Task<IList<VoucherLineViewModel>> GetArticlesAsync(int voucherId, GridOptions gridOptions = null)
        {
            var query = GetVoucherLinesQuery(voucherId);
            query = _repository.ApplyRowFilter(ref query, ViewName.VoucherLine);
            var lines = await query
                .Select(line => Mapper.Map<VoucherLineViewModel>(line))
                .Apply(gridOptions)
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
            var article = await GetVoucherLineQuery(articleId)
                .Where(line => line.Id == articleId)
                .SingleOrDefaultAsync();
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
        public async Task<ViewViewModel> GetVoucherLineMetadataAsync()
        {
            return await Metadata.GetViewMetadataAsync<VoucherLine>();
        }

        /// <summary>
        /// به روش آسنکرون، تعداد آرتیکل های یک سند مالی را بعد از اعمال فیلتر (در صورت وجود)
        /// از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="userContext">
        /// اطلاعات دسترسی کاربر به منابع محدود شده مانند نقش ها، دوره های مالی و شعبه ها
        /// </param>
        /// <param name="voucherId">شناسه یکی از اسناد مالی موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>تعداد آرتیکل های سند مالی بعد از اعمال فیلتر</returns>
        public async Task<int> GetArticleCountAsync(
            UserContextViewModel userContext, int voucherId, GridOptions gridOptions = null)
        {
            var repository = UnitOfWork.GetAsyncRepository<VoucherLine>();
            var query = repository.GetEntityQuery()
                .Where(line => line.Voucher.Id == voucherId);
            query = _repository.ApplyRowFilter(ref query, ViewName.VoucherLine);
            return await query
                .Apply(gridOptions, false)
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
        /// <param name="lineView">آرتیکل برای ایجاد یا اصلاح</param>
        public async Task<VoucherLineViewModel> SaveArticleAsync(VoucherLineViewModel lineView)
        {
            Verify.ArgumentNotNull(lineView, "lineView");
            VoucherLine line = default(VoucherLine);
            var repository = UnitOfWork.GetAsyncRepository<VoucherLine>();
            if (lineView.Id == 0)
            {
                line = Mapper.Map<VoucherLine>(lineView);
                await InsertAsync(repository, line);
            }
            else
            {
                line = await repository.GetByIDAsync(lineView.Id);
                if (line != null)
                {
                    await UpdateAsync(repository, line, lineView);
                }
            }

            return Mapper.Map<VoucherLineViewModel>(line);
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
                await DeleteAsync(repository, article);
            }
        }

        /// <summary>
        /// اطلاعات محیطی کاربر جاری برنامه را برای ایجاد لاگ های عملیاتی تنظیم می کند
        /// </summary>
        /// <param name="userContext">اطلاعات دسترسی کاربر به منابع محدود شده مانند نقش ها، دوره های مالی و شعبه ها</param>
        public void SetCurrentContext(UserContextViewModel userContext)
        {
            _repository.SetCurrentContext(userContext);
            SetLoggingContext(userContext);
        }

        /// <summary>
        /// آخرین تغییرات موجودیت را از مدل نمایشی به سطر اطلاعاتی موجود کپی می کند
        /// </summary>
        /// <param name="lineView">مدل نمایشی شامل آخرین تغییرات</param>
        /// <param name="line">سطر اطلاعاتی موجود</param>
        protected override void UpdateExisting(VoucherLineViewModel lineView, VoucherLine line)
        {
            line.AccountId = lineView.FullAccount.Account.Id;
            line.DetailId = lineView.FullAccount.DetailAccount.Id;
            line.CostCenterId = lineView.FullAccount.CostCenter.Id;
            line.ProjectId = lineView.FullAccount.Project.Id;
            line.CurrencyId = lineView.CurrencyId ?? 0;
            line.Debit = lineView.Debit;
            line.Credit = lineView.Credit;
            line.Description = lineView.Description;
        }

        /// <summary>
        /// اطلاعات خلاصه سطر اطلاعاتی داده شده را به صورت یک رشته متنی برمی گرداند
        /// </summary>
        /// <param name="entity">یکی از سطرهای اطلاعاتی موجود</param>
        /// <returns>اطلاعات خلاصه سطر اطلاعاتی داده شده به صورت رشته متنی</returns>
        protected override string GetState(VoucherLine entity)
        {
            return (entity != null)
                ? String.Format(
                    @"Account : {1}{0}Detail Account : {2}{0}Cost Center : {3}{0}Project : {4}
Currency : {5}{0}Debit : {6}{0}Credit : {7}{0}Description : {8}",
                    Environment.NewLine, entity.AccountId, entity.DetailId, entity.CostCenterId, entity.ProjectId,
                    entity.CurrencyId, entity.Debit, entity.Credit, entity.Description)
                : null;
        }

        private IQueryable<VoucherLine> GetVoucherLinesQuery(int voucherId)
        {
            var repository = UnitOfWork.GetRepository<VoucherLine>();
            var linesQuery = repository
                .GetEntityQuery(
                    line => line.Voucher, line => line.Account, line => line.DetailAccount, line => line.CostCenter,
                    line => line.Project, line => line.Currency)
                .Where(line => line.Voucher.Id == voucherId);
            return linesQuery;
        }

        private IQueryable<VoucherLine> GetVoucherLineQuery(int articleId)
        {
            var repository = UnitOfWork.GetRepository<VoucherLine>();
            var lineQuery = repository
                .GetEntityQuery(
                    line => line.Voucher, line => line.Account, line => line.DetailAccount, line => line.CostCenter,
                    line => line.Project, line => line.Currency)
                .Where(line => line.Id == articleId);
            return lineQuery;
        }

        private readonly ISecureRepository _repository;
    }
}
