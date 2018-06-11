using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Common;
using SPPC.Framework.Mapper;
using SPPC.Framework.Persistence;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Model.Auth;
using SPPC.Tadbir.Model.Core;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Model.Workflow;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.ViewModel.Metadata;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت اطلاعات اسناد مالی و آرتیکل های آنها را پیاده سازی می کند.
    /// </summary>
    public class VoucherRepository : IVoucherRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="unitOfWork">پیاده سازی اینترفیس واحد کاری برای انجام عملیات دیتابیسی </param>
        /// <param name="mapper">نگاشت مورد استفاده برای تبدیل کلاس های مدل اطلاعاتی</param>
        /// <param name="decorator">امکان ضمیمه کردن متادیتا به اطلاعات خوانده شده را فراهم می کند</param>
        public VoucherRepository(IUnitOfWork unitOfWork, IDomainMapper mapper, IMetadataDecorator decorator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _decorator = decorator;
        }

        #region Voucher Operations

        /// <summary>
        /// به روش آسنکرون، کلیه اسناد مالی را که در دوره مالی و شعبه مشخص شده تعریف شده اند، از دیتابیس خوانده و برمی گرداند
        /// </summary>
        /// <param name="fpId">شناسه دیتابیسی یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه دیتابیسی یکی از شعب موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از اسناد مالی تعریف شده در دوره مالی و شعبه مشخص شده</returns>
        public async Task<EntityListViewModel<VoucherViewModel>> GetVouchersAsync(
            int fpId, int branchId, GridOptions gridOptions = null)
        {
            var query = GetVoucherQuery(
                txn => txn.FiscalPeriod.Id == fpId && txn.Branch.Id == branchId, gridOptions);
            var vouchers = await query
                .Select(txn => _mapper.Map<VoucherViewModel>(txn))
                .ToListAsync();
            foreach (var voucher in vouchers)
            {
                await AddWorkItemInfoAsync(voucher);
            }

            return await _decorator.GetDecoratedListAsync<Voucher, VoucherViewModel>(vouchers);
        }

        /// <summary>
        /// به روش آسنکرون، سند مالی با شناسه دیتابیسی مشخص شده را از دیتابیس خوانده و برمی گرداند
        /// </summary>
        /// <param name="voucherId">شناسه دیتابیسی یکی از اسناد مالی موجود</param>
        /// <returns>سند مالی مشخص شده با شناسه دیتابیسی</returns>
        public async Task<EntityItemViewModel<VoucherViewModel>> GetVoucherAsync(int voucherId)
        {
            VoucherViewModel voucherViewModel = null;
            var query = GetVoucherQuery(txn => txn.Id == voucherId);
            var voucher = await query.SingleOrDefaultAsync();
            if (voucher != null)
            {
                voucherViewModel = _mapper.Map<VoucherViewModel>(voucher);
                AddWorkItemInfo(voucherViewModel);
            }

            return await _decorator.GetDecoratedItemAsync<Voucher, VoucherViewModel>(voucherViewModel);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات فراداده ای تعریف شده برای سند مالی را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات فراداده ای تعریف شده برای سند مالی</returns>
        public async Task<EntityItemViewModel<VoucherViewModel>> GetVoucherMetadataAsync()
        {
            return await _decorator.GetDecoratedItemAsync<Voucher, VoucherViewModel>(null);
        }

        /// <summary>
        /// به روش آسنکرون، تعداد اسناد مالی تعریف شده در دوره مالی و شعبه مشخص شده را
        /// از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="fpId">شناسه عددی یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه عددی یکی از شعب موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>تعداد اسناد مالی تعریف شده در دوره مالی و شعبه مشخص شده</returns>
        public async Task<int> GetCountAsync(int fpId, int branchId, GridOptions gridOptions = null)
        {
            var repository = _unitOfWork.GetAsyncRepository<Voucher>();
            var count = await repository
                .GetCountByCriteriaAsync(
                    txn => txn.FiscalPeriod.Id == fpId && txn.Branch.Id == branchId,
                    gridOptions);
            return count;
        }

        /// <summary>
        /// به روش آسنکرون، مدل نمایشی دوره مالی مورد استفاده در یک سند مالی را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="voucher">مدل نمایشی سند مالی مورد نظر</param>
        /// <returns>مدل نمایشی دوره مالی به کار رفته در سند مالی</returns>
        public async Task<FiscalPeriodViewModel> GetVoucherFiscalPeriodAsync(VoucherViewModel voucher)
        {
            Verify.ArgumentNotNull(voucher, "voucher");
            var repository = _unitOfWork.GetAsyncRepository<FiscalPeriod>();
            var fiscalPeriod = await repository.GetByIDAsync(voucher.FiscalPeriodId);
            return _mapper.Map<FiscalPeriodViewModel>(fiscalPeriod);
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
            var repository = _unitOfWork.GetAsyncRepository<Voucher>();
            if (voucher.Id == 0)
            {
                voucherModel = _mapper.Map<Voucher>(voucher);
                UpdateAction(voucherModel);
                repository.Insert(voucherModel, txn => txn.Document, txn => txn.Document.Actions);
            }
            else
            {
                voucherModel = await repository
                    .GetEntityQuery()
                    .Where(txn => txn.Id == voucher.Id)
                    .Include(txn => txn.FiscalPeriod)
                    .Include(txn => txn.Branch)
                    .Include(txn => txn.Document)
                        .ThenInclude(doc => doc.Actions)
                    .SingleOrDefaultAsync();
                if (voucherModel != null)
                {
                    UpdateExistingVoucher(voucherModel, voucher);
                    UpdateAction(voucherModel);
                    repository.Update(voucherModel, txn => txn.Document, txn => txn.Document.Actions);
                }
            }

            await _unitOfWork.CommitAsync();
            return _mapper.Map<VoucherViewModel>(voucherModel);
        }

        /// <summary>
        /// به روش آسنکرون، سند مالی مشخص شده با شناسه دیتابیسی را از دیتابیس حذف می کند
        /// </summary>
        /// <param name="voucherId">شناسه دیتابیسی سند مالی برای حذف</param>
        public async Task DeleteVoucherAsync(int voucherId)
        {
            var repository = _unitOfWork.GetAsyncRepository<Voucher>();
            var voucher = await repository.GetByIDWithTrackingAsync(
                voucherId, txn => txn.Lines, txn => txn.Document);
            if (voucher != null)
            {
                var documentRepository = _unitOfWork.GetAsyncRepository<Document>();
                var document = await documentRepository.GetByIDWithTrackingAsync(
                    voucher.Document.Id, doc => doc.Actions);
                voucher.Lines.Clear();
                document.Actions.Clear();
                repository.Delete(voucher);
                documentRepository.Delete(document);
                await _unitOfWork.CommitAsync();
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
            var repository = _unitOfWork.GetAsyncRepository<Voucher>();
            var duplicates = await repository
                .GetByCriteriaAsync(vch => vch.Id != voucher.Id
                    && vch.No == voucher.No
                    && vch.FiscalPeriod.Id == voucher.FiscalPeriodId
                    && vch.Branch.Id == voucher.BranchId);
            return (duplicates.Count > 0);
        }

        #endregion

        #region Voucher Line Operations

        /// <summary>
        /// به روش آسنکرون، آرتیکل های یک سند مشخص شده با شناسه عددی را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="voucherId">شناسه یکی از اسناد مالی موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>آرتیکل های سندمشخص شده با شناسه عددی</returns>
        public async Task<EntityListViewModel<VoucherLineViewModel>> GetArticlesAsync(
            int voucherId, GridOptions gridOptions = null)
        {
            var query = GetVoucherLinesQuery(voucherId, gridOptions);
            var lines = await query
                .Select(line => _mapper.Map<VoucherLineViewModel>(line))
                .ToListAsync();
            return await _decorator.GetDecoratedListAsync<VoucherLine, VoucherLineViewModel>(lines);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات سطر سند مالی (آرتیکل) مشخص شده با شناسه دیتابیسی را از دیتابیس خوانده و برمی گرداند
        /// </summary>
        /// <param name="articleId">شناسه دیتابیسی آرتیکل موجود</param>
        /// <returns>اطلاعات آرتیکل مشخص شده با شناسه دیتابیسی</returns>
        public async Task<EntityItemViewModel<VoucherLineViewModel>> GetArticleAsync(int articleId)
        {
            VoucherLineViewModel articleViewModel = null;
            var repository = _unitOfWork.GetAsyncRepository<VoucherLine>();
            var query = GetArticleDetailsQuery(repository, art => art.Id == articleId);
            var article = await query.SingleOrDefaultAsync();
            if (article != null)
            {
                articleViewModel = _mapper.Map<VoucherLineViewModel>(article);
            }

            return await _decorator.GetDecoratedItemAsync<VoucherLine, VoucherLineViewModel>(articleViewModel);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات فراداده ای تعریف شده برای آرتیکل سند مالی را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات فراداده ای تعریف شده برای آرتیکل سند مالی</returns>
        public async Task<EntityItemViewModel<VoucherLineViewModel>> GetVoucherLineMetadataAsync()
        {
            return await _decorator.GetDecoratedItemAsync<VoucherLine, VoucherLineViewModel>(null);
        }

        /// <summary>
        /// به روش آسنکرون، تعداد آرتیکل های یک سند مالی را بعد از اعمال فیلتر (در صورت وجود)
        /// از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="voucherId">شناسه یکی از اسناد مالی موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>تعداد آرتیکل های سند مالی بعد از اعمال فیلتر</returns>
        public async Task<int> GetArticleCountAsync(int voucherId, GridOptions gridOptions = null)
        {
            var repository = _unitOfWork.GetAsyncRepository<VoucherLine>();
            var count = await repository.GetCountByCriteriaAsync(
                line => line.Voucher.Id == voucherId, gridOptions);
            return count;
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
            var repository = _unitOfWork.GetAsyncRepository<Account>();
            var account = await repository.GetByIDAsync(accountId, acc => acc.Children);
            if (account != null)
            {
                articleAccount = _mapper.Map<AccountViewModel>(account);
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
            var repository = _unitOfWork.GetAsyncRepository<DetailAccount>();
            var detailAccount = await repository.GetByIDAsync(faccountId, acc => acc.Children);
            if (detailAccount != null)
            {
                articleDetailAccount = _mapper.Map<DetailAccountViewModel>(detailAccount);
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
            var repository = _unitOfWork.GetAsyncRepository<CostCenter>();
            var costCenter = await repository.GetByIDAsync(costCenterId, acc => acc.Children);
            if (costCenter != null)
            {
                articleCostCenter = _mapper.Map<CostCenterViewModel>(costCenter);
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
            var repository = _unitOfWork.GetAsyncRepository<Project>();
            var project = await repository.GetByIDAsync(projectId, acc => acc.Children);
            if (project != null)
            {
                articleProject = _mapper.Map<ProjectViewModel>(project);
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
            var repository = _unitOfWork.GetAsyncRepository<VoucherLine>();
            if (article.Id == 0)
            {
                lineModel = _mapper.Map<VoucherLine>(article);
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

            await _unitOfWork.CommitAsync();
            return _mapper.Map<VoucherLineViewModel>(lineModel);
        }

        /// <summary>
        /// به روش آسنکرون، سطر سند مالی (آرتیکل) مشخص شده با شناسه دیتابیسی را از محل ذخیره حذف می کند
        /// </summary>
        /// <param name="articleId">شناسه دیتابیسی آرتیکل برای حذف</param>
        public async Task DeleteArticleAsync(int articleId)
        {
            var repository = _unitOfWork.GetAsyncRepository<VoucherLine>();
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
                await _unitOfWork.CommitAsync();
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

        private static void UpdateAction(Voucher voucher)
        {
            if (voucher.Id == 0)
            {
                var mainAction = voucher.Document.Actions.First();
                mainAction.Document = voucher.Document;
                mainAction.CreatedDate = DateTime.Now;
                mainAction.ModifiedDate = DateTime.Now;
            }
            else
            {
                var mainAction = voucher.Document.Actions.First();
                mainAction.ModifiedDate = DateTime.Now;
            }
        }

        private static IQueryable<VoucherLine> GetArticleDetailsQuery(
            IRepository<VoucherLine> repository, Expression<Func<VoucherLine, bool>> criteria)
        {
            var query = repository
                .GetEntityQuery()
                .Include(art => art.Account)
                .Include(art => art.DetailAccount)
                .Include(art => art.CostCenter)
                .Include(art => art.Project)
                .Include(art => art.Voucher)
                .Include(art => art.FiscalPeriod)
                .Include(art => art.Currency)
                .Include(art => art.Branch)
                    .ThenInclude(br => br.Company)
                .Where(criteria);
            return query;
        }

        private void UpdateExistingVoucher(Voucher existing, VoucherViewModel voucher)
        {
            var userRepository = _unitOfWork.GetRepository<User>();
            existing.No = voucher.No;
            existing.Date = voucher.Date;
            existing.Description = voucher.Description;
            existing.Document.EntityNo = voucher.No;
            var mainAction = existing.Document.Actions.First();
            mainAction.ModifiedBy = userRepository.GetByID(voucher.Document.Actions.First().ModifiedById);
        }

        private IQueryable<Voucher> GetVoucherQuery(
            Expression<Func<Voucher, bool>> criteria, GridOptions gridOptions = null)
        {
            var repository = _unitOfWork.GetRepository<Voucher>();
            var vouchersQuery = repository
                .GetEntityQuery(gridOptions)
                .Include(txn => txn.Lines)
                .Include(txn => txn.FiscalPeriod)
                .Include(txn => txn.Branch)
                .Include(txn => txn.Document)
                    .ThenInclude(doc => doc.Type)
                .Include(txn => txn.Document)
                    .ThenInclude(doc => doc.Status)
                .Include(txn => txn.Document)
                    .ThenInclude(doc => doc.Actions)
                        .ThenInclude(act => act.CreatedBy)
                .Include(txn => txn.Document)
                    .ThenInclude(doc => doc.Actions)
                        .ThenInclude(act => act.ModifiedBy)
                .Include(txn => txn.Document)
                    .ThenInclude(doc => doc.Actions)
                        .ThenInclude(act => act.ConfirmedBy)
                .Include(txn => txn.Document)
                    .ThenInclude(doc => doc.Actions)
                        .ThenInclude(act => act.ApprovedBy)
                .Where(criteria);
            vouchersQuery = (gridOptions != null)
                ? vouchersQuery
                    .Skip((gridOptions.Paging.PageIndex - 1) * gridOptions.Paging.PageSize)
                    .Take(gridOptions.Paging.PageSize)
                : vouchersQuery;
            return vouchersQuery;
        }

        private IQueryable<VoucherLine> GetVoucherLinesQuery(int voucherId, GridOptions gridOptions = null)
        {
            var repository = _unitOfWork.GetRepository<VoucherLine>();
            var linesQuery = repository
                .GetEntityQuery(gridOptions)
                .Include(line => line.Voucher)
                .Include(line => line.Account)
                .Include(line => line.DetailAccount)
                .Include(line => line.CostCenter)
                .Include(line => line.Project)
                .Include(line => line.Currency)
                .Include(line => line.FiscalPeriod)
                .Include(line => line.Branch)
                .Where(line => line.Voucher.Id == voucherId);
            linesQuery = (gridOptions != null)
                ? linesQuery
                    .Skip((gridOptions.Paging.PageIndex - 1) * gridOptions.Paging.PageSize)
                    .Take(gridOptions.Paging.PageSize)
                : linesQuery;
            return linesQuery;
        }

        private VoucherViewModel AddWorkItemInfo(VoucherViewModel voucher)
        {
            var repository = _unitOfWork.GetRepository<WorkItemDocument>();
            var document = repository
                .GetByCriteria(wid => wid.Document.Id == voucher.Document.Id
                    && wid.DocumentType == DocumentTypeName.Voucher,
                    wid => wid.Document, wid => wid.WorkItem)
                .FirstOrDefault();
            if (document != null)
            {
                voucher.WorkItemId = document.WorkItem.Id;
                voucher.WorkItemTargetId = document.WorkItem.Target.Id;
                voucher.WorkItemAction = document.WorkItem.Action;
            }

            return voucher;
        }

        private async Task<VoucherViewModel> AddWorkItemInfoAsync(VoucherViewModel voucher)
        {
            var repository = _unitOfWork.GetAsyncRepository<WorkItemDocument>();
            var documents = await repository
                .GetByCriteriaAsync(wid => wid.Document.Id == voucher.Document.Id
                    && wid.DocumentType == DocumentTypeName.Voucher,
                    wid => wid.Document, wid => wid.WorkItem);
            var document = documents.FirstOrDefault();
            if (document != null)
            {
                voucher.WorkItemId = document.WorkItem.Id;
                voucher.WorkItemTargetId = document.WorkItem.Target.Id;
                voucher.WorkItemAction = document.WorkItem.Action;
            }

            return voucher;
        }

        private IUnitOfWork _unitOfWork;
        private IDomainMapper _mapper;
        private IMetadataDecorator _decorator;
    }
}
