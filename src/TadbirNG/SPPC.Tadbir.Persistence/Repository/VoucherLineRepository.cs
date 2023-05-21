using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Common;
using SPPC.Framework.Extensions;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Extensions;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Utility;
using SPPC.Tadbir.ViewModel.Finance;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت اطلاعات آرتیکل های مالی را پیاده سازی می کند.
    /// </summary>
    public class VoucherLineRepository
        : EntityLoggingRepository<VoucherLine, VoucherLineViewModel>, IVoucherLineRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="system">امکانات مورد نیاز در دیتابیس های سیستمی را فراهم می کند</param>
        public VoucherLineRepository(IRepositoryContext context, ISystemRepository system)
            : base(context, system?.Logger)
        {
            _system = system;
        }

        /// <summary>
        /// به روش آسنکرون، آرتیکل های یک سند مشخص شده با شناسه عددی را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="voucherId">شناسه یکی از اسناد مالی موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>آرتیکل های سندمشخص شده با شناسه عددی</returns>
        public async Task<PagedList<VoucherLineViewModel>> GetArticlesAsync(
            int voucherId, GridOptions gridOptions)
        {
            var query = GetVoucherLinesQuery(voucherId);
            query = Repository.ApplyRowFilter(ref query, ViewId.VoucherLine);
            var lines = await query
                .Select(line => Mapper.Map<VoucherLineViewModel>(line))
                .ToListAsync();
            return new PagedList<VoucherLineViewModel>(lines, gridOptions);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات سطر سند مالی (آرتیکل) مشخص شده با شناسه دیتابیسی را از دیتابیس خوانده و برمی گرداند
        /// </summary>
        /// <param name="articleId">شناسه دیتابیسی آرتیکل موجود</param>
        /// <returns>اطلاعات آرتیکل مشخص شده با شناسه دیتابیسی</returns>
        public async Task<VoucherLineViewModel> GetArticleAsync(int articleId)
        {
            VoucherLineViewModel articleViewModel = null;
            var article = await GetVoucherLineQuery(articleId)
                .SingleOrDefaultAsync();
            if (article != null)
            {
                articleViewModel = Mapper.Map<VoucherLineViewModel>(article);
            }

            return articleViewModel;
        }

        /// <summary>
        /// به روش آسنکرون، تعداد آرتیکل های یک سند مالی را بعد از اعمال فیلتر (در صورت وجود)
        /// از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <typeparam name="TViewModel">نوع مدل نمایشی که برای نمایش اطلاعات موجودیت استفاده می شود</typeparam>
        /// <param name="voucherId">شناسه یکی از اسناد مالی موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>تعداد آرتیکل های سند مالی بعد از اعمال فیلتر</returns>
        public async Task<int> GetArticleCountAsync<TViewModel>(int voucherId, GridOptions gridOptions = null)
            where TViewModel : class, new()
        {
            var repository = UnitOfWork.GetAsyncRepository<VoucherLine>();
            var query = repository.GetEntityQuery()
                .Where(line => line.Voucher.Id == voucherId);
            query = Repository.ApplyRowFilter(ref query, ViewId.VoucherLine);
            var articles = await query
                .Select(line => Mapper.Map<TViewModel>(line))
                .ToListAsync();
            return articles
                .Apply(gridOptions, false)
                .Count();
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات یک سطر سند مالی (آرتیکل) را در دیتابیس ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="lineView">آرتیکل برای ایجاد یا اصلاح</param>
        public async Task<VoucherLineViewModel> SaveArticleAsync(VoucherLineViewModel lineView)
        {
            Verify.ArgumentNotNull(lineView, "lineView");
            var repository = UnitOfWork.GetAsyncRepository<VoucherLine>();
            VoucherLine line;
            if (lineView.Id == 0)
            {
                line = Mapper.Map<VoucherLine>(lineView);
                line.RowNo = await GetNextRowNoAsync(line.VoucherId);
                line.CreatedById = UserContext.Id;
                await InsertAsync(repository, line, OperationId.CreateLine);
                await UpdateVoucherBalanceStatusAsync(lineView.VoucherId);
            }
            else
            {
                line = await repository.GetByIDAsync(lineView.Id);
                if (line != null)
                {
                    await UpdateAsync(repository, line, lineView, OperationId.EditLine);
                    await UpdateVoucherBalanceStatusAsync(lineView.VoucherId);
                }
            }

            return Mapper.Map<VoucherLineViewModel>(line);
        }

        /// <summary>
        /// به روش آسنکرون، علامتگذاری مشخص شده را روی آرتیکل سند اعمال می کند
        /// </summary>
        /// <param name="mark">اطلاعات علامتکذاری آرتیکل</param>
        public async Task SaveArticleMarkAsync(VoucherLineMarkViewModel mark)
        {
            Verify.ArgumentNotNull(mark, nameof(mark));
            var repository = UnitOfWork.GetAsyncRepository<VoucherLine>();
            var line = await repository.GetByIDAsync(mark.Id);
            if (line != null)
            {
                line.Mark = mark.Mark;
                repository.Update(line);
                await UnitOfWork.CommitAsync();
            }
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
                await DeleteAsync(repository, article, OperationId.DeleteLine);
                await UpdateRowNumbersAsync(article);
                await UpdateVoucherBalanceStatusAsync(article.VoucherId);
            }
        }

        /// <summary>
        /// به روش آسنکرون، آرتیکل های مشخص شده با شناسه عددی را حذف می کند
        /// </summary>
        /// <param name="items">مجموعه شناسه های دیتابیسی سطرهای مورد نظر برای حذف</param>
        public async Task DeleteArticlesAsync(IEnumerable<int> items)
        {
            int voucherId = 0;
            var repository = UnitOfWork.GetAsyncRepository<VoucherLine>();
            foreach (int item in items)
            {
                var article = await repository.GetByIDAsync(item);
                if (article != null)
                {
                    voucherId = article.VoucherId;
                    await DeleteNoLogAsync(repository, article);
                }
            }

            if (voucherId > 0)
            {
                await UpdateVoucherBalanceStatusAsync(voucherId);
                await UpdateRowNumbersAsync(voucherId);
            }

            await OnEntityGroupDeleted(items, OperationId.GroupDeleteLines);
        }

        /// <summary>
        /// به روش آسنکرون، تعداد کل آرتیکل های ثبت شده را برمیگرداند
        /// </summary>
        /// <returns>تعداد کل آرتیکل ها</returns>
        public async Task<int> GetAllArticlesCountAsync()
        {
            var repository = UnitOfWork.GetAsyncRepository<VoucherLine>();
            var query = repository.GetEntityQuery();
            return await query.CountAsync();
        }

        /// <summary>
        /// به روش آسنکرون، نوع مفهومی سند را با توجه به شناسه آرتیکل داده شده خوانده و برمی گرداند
        /// </summary>
        /// <param name="articleId">شناسه دیتابیسی آرتیکل مورد نظر</param>
        /// <returns>نوع مفهومی سند مرتبط با شناسه آرتیکل داده شده</returns>
        public async Task<int> GetLineSubjectTypeAsync(int articleId)
        {
            var repository = UnitOfWork.GetAsyncRepository<VoucherLine>();
            return await repository
                .GetEntityQuery(line => line.Voucher)
                .Where(line => line.Id == articleId)
                .Select(line => line.Voucher.SubjectType)
                .FirstOrDefaultAsync();
        }

        /// <inheritdoc/>
        protected override async Task FinalizeActionAsync(VoucherLine entity)
        {
            var voucherRepository = UnitOfWork.GetAsyncRepository<Voucher>();
            var voucher = await voucherRepository.GetByIDAsync(entity.VoucherId);
            if (voucher != null)
            {
                await UnitOfWork.CommitAsync();
                Log.EntityId = entity.Id;
                CopyEntityDataToLog(voucher);
                await TrySaveLogAsync();
            }
        }

        internal override int? EntityType
        {
            get { return (int)EntityTypeId.Voucher; }
        }

        /// <summary>
        /// آخرین تغییرات موجودیت را از مدل نمایشی به سطر اطلاعاتی موجود کپی می کند
        /// </summary>
        /// <param name="lineView">مدل نمایشی شامل آخرین تغییرات</param>
        /// <param name="line">سطر اطلاعاتی موجود</param>
        protected override void UpdateExisting(VoucherLineViewModel lineView, VoucherLine line)
        {
            line.AccountId = lineView.FullAccount.Account.Id;
            line.DetailAccountId = lineView.FullAccount.DetailAccount.Id > 0 ? lineView.FullAccount.DetailAccount.Id : null;
            line.CostCenterId = lineView.FullAccount.CostCenter.Id > 0 ? lineView.FullAccount.CostCenter.Id : null;
            line.ProjectId = lineView.FullAccount.Project.Id > 0 ? lineView.FullAccount.Project.Id : null;
            line.CurrencyId = lineView.CurrencyId;
            line.Debit = lineView.Debit;
            line.Credit = lineView.Credit;
            line.Description = lineView.Description;
            line.CurrencyValue = lineView.CurrencyValue;
        }

        /// <summary>
        /// اطلاعات خلاصه سطر اطلاعاتی داده شده را به صورت یک رشته متنی برمی گرداند
        /// </summary>
        /// <param name="entity">یکی از سطرهای اطلاعاتی موجود</param>
        /// <returns>اطلاعات خلاصه سطر اطلاعاتی داده شده به صورت رشته متنی</returns>
        protected override string GetState(VoucherLine entity)
        {
            var repository = UnitOfWork.GetRepository<Account>();
            var account = repository.GetByID(entity.AccountId);
            return (entity != null)
                ? String.Format(
                    "{0} : {1} , {2} : {3} , {4} : {5} , {6} : {7}",
                    AppStrings.Account, account.FullCode, AppStrings.Debit, entity.Debit,
                    AppStrings.Credit, entity.Credit, AppStrings.Description, entity.Description)
                : null;
        }

        private ISecureRepository Repository
        {
            get { return _system.Repository; }
        }

        private async Task UpdateVoucherBalanceStatusAsync(int voucherId)
        {
            var repository = UnitOfWork.GetAsyncRepository<Voucher>();
            var voucher = await repository.GetByIDAsync(voucherId, v => v.Lines);
            var debitSum = voucher.Lines.Sum(vl => vl.Debit);
            var creditSum = voucher.Lines.Sum(vl => vl.Credit);
            voucher.IsBalanced = debitSum.AlmostEquals(creditSum);
            repository.Update(voucher);
            await UnitOfWork.CommitAsync();
        }

        private IQueryable<VoucherLine> GetVoucherLinesQuery(int voucherId)
        {
            var linesQuery = Repository
                .GetAllOperationQuery<VoucherLine>(ViewId.VoucherLine,
                    line => line.Voucher, line => line.Account, line => line.DetailAccount, line => line.CostCenter,
                    line => line.Project, line => line.Currency, line => line.Branch)
                .Where(line => line.Voucher.Id == voucherId)
                .OrderBy(line => line.RowNo);
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

        private async Task<int> GetNextRowNoAsync(int voucherId)
        {
            var repository = UnitOfWork.GetAsyncRepository<VoucherLine>();
            int lastRowNo = await repository
                .GetEntityQuery()
                .Where(line => line.VoucherId == voucherId)
                .OrderByDescending(line => line.RowNo)
                .Select(line => line.RowNo)
                .FirstOrDefaultAsync();
            return lastRowNo + 1;
        }

        private async Task UpdateRowNumbersAsync(VoucherLine line)
        {
            Verify.ArgumentNotNull(line, nameof(line));
            var repository = UnitOfWork.GetAsyncRepository<VoucherLine>();
            var nextLines = await repository
                .GetEntityQuery()
                .Where(vl => vl.VoucherId == line.VoucherId &&
                    vl.RowNo > line.RowNo)
                .OrderBy(vl => vl.RowNo)
                .ToListAsync();
            foreach (var nextLine in nextLines)
            {
                nextLine.RowNo = nextLine.RowNo - 1;
                repository.Update(nextLine);
            }

            await UnitOfWork.CommitAsync();
        }

        private async Task UpdateRowNumbersAsync(int voucherId)
        {
            var repository = UnitOfWork.GetAsyncRepository<VoucherLine>();
            var lines = await repository
                .GetEntityQuery()
                .Where(vl => vl.VoucherId == voucherId)
                .OrderBy(vl => vl.RowNo)
                .ToListAsync();
            int rowNo = 1;
            foreach (var line in lines)
            {
                line.RowNo = rowNo++;
                repository.Update(line);
            }

            await UnitOfWork.CommitAsync();
        }

        private readonly ISystemRepository _system;
    }
}
